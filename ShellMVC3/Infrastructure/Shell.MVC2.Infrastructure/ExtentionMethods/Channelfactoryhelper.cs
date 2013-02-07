using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Threading;

namespace Shell.MVC2.Infrastructure
{
   public class Channelfactoryhelper
    {


        /// <summary>
        /// Delegate type of the service method to perform.
        /// </summary>
        /// <param name="proxy">The service proxy.</param>
        /// <typeparam name="T">The type of service to use.</typeparam>
        public delegate void UseServiceDelegate<in T>(T proxy);

        /// <summary>
        /// Wraps using a WCF service.
        /// </summary>
        /// <typeparam name="T">The type of service to use.</typeparam>
        public static class Service<T>
        {
            /// <summary>
            /// A dictionary to hold looked-up endpoint names.
            /// </summary>
            private static readonly IDictionary<Type, string> cachedEndpointNames = new Dictionary<Type, string>();

            /// <summary>
            /// A dictionary to hold created channel factories.
            /// </summary>
            private static readonly IDictionary<string, ChannelFactory<T>> cachedFactories =
                new Dictionary<string, ChannelFactory<T>>();

            /// <summary>
            /// Locking object.
            /// </summary>
            private static readonly object cacheLocker = new object();
            /// <summary>
            /// Uses the specified code block.
            /// </summary>
            /// <param name="codeBlock">The code block.</param>
          public static void Use(UseServiceDelegate<T> codeBlock)
            {
                dynamic factory = GetChannelFactory();
                dynamic proxy = (IClientChannel)factory.CreateChannel();
                dynamic success = false;

                Exception mostRecentEx = null;
                for (int i = 0; i <= 4; i++)
                {
                    //        ' Attempt a maximum of 5 times 
                    try
                    {
                        using (proxy)
                        {
                            codeBlock((T)proxy);
                        }

                        success = true;
                    }
                    catch (ChannelTerminatedException cte)
                    {
                        mostRecentEx = cte;
                        proxy.Abort();
                        //  delay (backoff) and retry 
                        Thread.Sleep(1000 * (i + 1));

                        // The following is thrown when a remote endpoint could not be found or reached.  The endpoint may not be found or 
                        // reachable because the remote endpoint is down, the remote endpoint is unreachable, or because the remote network is unreachable.
                    }
                    catch (EndpointNotFoundException enfe)
                    {
                        mostRecentEx = enfe;
                        proxy.Abort();
                        //  delay (backoff) and retry 
                        Thread.Sleep(1000 * (i + 1));

                        // The following exception that is thrown when a server is too busy to accept a message.
                    }
                    catch (ServerTooBusyException stbe)
                    {
                        mostRecentEx = stbe;
                        proxy.Abort();

                        //  delay (backoff) and retry 
                        Thread.Sleep(1000 * (i + 1));

                    }
                    catch (CommunicationException commProblem)
                    {
                        //TO DO log this here
                    }
                    catch (Exception generatedExceptionName)
                    {
                        // rethrow any other exception not defined here
                        // You may want to define a custom Exception class to pass information such as failure count, and failure type
                        proxy.Abort();
                        //TO DO log it here using the logger
                        throw;
                    }
                    finally
                    {
                        if (!success)
                        {
                            proxy.Abort();
                        }
                    }

                }
                if (mostRecentEx != null)
                {
                    proxy.Abort();
                    throw new Exception("WCF call failed after 5 retries.", mostRecentEx);
                }


            }

     


            /// <summary>
            /// Gets the channel factory.
            /// </summary>
            /// <returns>The channel factory.</returns>
            private static ChannelFactory<T> GetChannelFactory()
            {
                lock (cacheLocker)
                {
                    var endpointName = GetEndpointName();

                    if (cachedFactories.ContainsKey(endpointName))
                    {
                        return cachedFactories[endpointName];
                    }

                    var factory = new ChannelFactory<T>(endpointName);

                    cachedFactories.Add(endpointName, factory);
                    return factory;
                }
            }

            /// <summary>
            /// Gets the name of the endpoint.
            /// </summary>
            /// <returns>The name of the endpoint.</returns>
            private static string GetEndpointName()
            {
                var type = typeof(T);
                var fullName = type.FullName;
                var name = type.Name;
                lock (cacheLocker)
                {
                    if (cachedEndpointNames.ContainsKey(type))
                    {
                        return cachedEndpointNames[type];
                    }

               

                   ////var serviceModel = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["system.serviceModel"] as ServiceModelSectionGroup;

                   // if ((serviceModel != null) && !string.IsNullOrEmpty(fullName))
                   // {
                   //     foreach (var endpointName in serviceModel.Client.Endpoints.Cast<ChannelEndpointElement>().Where(endpoint => fullName.EndsWith(endpoint.Contract)).Select(endpoint => endpoint.Name))
                   //     {
                   //         cachedEndpointNames.Add(type, endpointName);
                   //         return endpointName;
                   //     }
                   // }

                    cachedEndpointNames.Add(type, name);
                    return fullName;
                }

                throw new InvalidOperationException("Could not find endpoint element for type '" + fullName + "' in the ServiceModel client configuration section. This might be because no configuration file was found for your application, or because no endpoint element matching this name could be found in the client element.");
            }
        }
    }
}

//Code that dooes not use caching 

public delegate void UseServiceDelegate<T>(T proxy);

public static class Service<T>
{
    public static ChannelFactory<T> _channelFactory = new ChannelFactory<T>("");

    public static void Use(UseServiceDelegate<T> codeBlock)
    {
        IClientChannel proxy = (IClientChannel)_channelFactory.CreateChannel();
        bool success = false;


        Exception mostRecentEx = null;
        for (int i = 0; i < 5; i++)  // Attempt a maximum of 5 times 
        {
            try
            {
                codeBlock((T)proxy);
                proxy.Close();
                success = true;
            }

            // The following is typically thrown on the client when a channel is terminated due to the server closing the connection.
            catch (ChannelTerminatedException cte)
            {
                mostRecentEx = cte;
                proxy.Abort();
                //  delay (backoff) and retry 
                Thread.Sleep(1000 * (i + 1));
            }

            // The following is thrown when a remote endpoint could not be found or reached.  The endpoint may not be found or 
            // reachable because the remote endpoint is down, the remote endpoint is unreachable, or because the remote network is unreachable.
            catch (EndpointNotFoundException enfe)
            {
                mostRecentEx = enfe;
                proxy.Abort();
                //  delay (backoff) and retry 
                Thread.Sleep(1000 * (i + 1));
            }

            // The following exception that is thrown when a server is too busy to accept a message.
            catch (ServerTooBusyException stbe)
            {
                mostRecentEx = stbe;
                proxy.Abort();

                //  delay (backoff) and retry 
                Thread.Sleep(1000 * (i + 1));
            }

            catch (Exception)
            {
                // rethrow any other exception not defined here
                // You may want to define a custom Exception class to pass information such as failure count, and failure type
                proxy.Abort();
                throw;
            }
        }
        if (mostRecentEx != null)
        {
            proxy.Abort();
            throw new Exception("WCF call failed after 5 retries.", mostRecentEx);
        }

    }
}

