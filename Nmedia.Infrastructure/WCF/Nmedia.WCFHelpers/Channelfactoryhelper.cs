using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Threading;
using System.ServiceModel.Channels;
using System.Net;
using System.ServiceModel.Activation;
using System.Web.Configuration;



namespace Nmedia.WCFHelpers
{

    /// <summary>
    /// This delegate describes the method on the interface to be called.
    /// </summary>
    /// <typeparam name="T">This is the type of the interface</typeparam>
    /// <param name="proxy">This is the method.</param>
    public delegate void UseServiceDelegate<T>(T proxy);
    /// <summary>
    /// This delegate describes the method on the interface to be called.
    /// </summary>
    /// <typeparam name="T">This is the type of the interface</typeparam>
    /// <param name="proxy">This is the method.</param>
    /// <param name="obj">This is any object which may be used to identify execution instance.</param>
    public delegate void UseServiceDelegateWithAsyncReturn<T>(T proxy, object obj);

    public class WCFHelpers
    {
        /// <summary>
        /// A dictionary to hold looked-up endpoint names.
        /// </summary>
        public static readonly IDictionary<Type, string> cachedEndpointNames = new Dictionary<Type, string>();
        /// <summary>
        /// Locking object.
        /// </summary>
        public static readonly object cacheLocker = new object();

        /// <summary>
        /// Gets the name of the endpoint.
        /// </summary>
        /// <returns>The name of the endpoint. using the contract name and aslo caches the result for future use</returns>
        public static string GetEndpointName<T>()
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

                Configuration appConfig = GetDefaultConfig(); //ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ServiceModelSectionGroup serviceModel = ServiceModelSectionGroup.GetSectionGroup(appConfig);
                // BindingsSection oBinding = serviceModel2.Bindings;

                //var serviceModel = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["system.serviceModel"] as ServiceModelSectionGroup;

                if ((serviceModel != null) && !string.IsNullOrEmpty(fullName))
                {
                    foreach (var endpointName in serviceModel.Client.Endpoints.Cast<ChannelEndpointElement>().Where(endpoint => fullName.EndsWith(endpoint.Contract)).Select(endpoint => endpoint.Name))
                    {
                        cachedEndpointNames.Add(type, endpointName);
                        return endpointName;
                    }
                }

                //Old code where we get it from calling web config
                // cachedEndpointNames.Add(type, name);
                // return fullName;
            }

            throw new InvalidOperationException("Could not find endpoint element for type '" + fullName + "' in the ServiceModel client configuration section. This might be because no configuration file was found for your application, or because no endpoint element matching this name could be found in the client element.");
        }

        public static Configuration GetDefaultConfig()
        {
            Configuration cfg;
            System.Web.HttpContext ctx = System.Web.HttpContext.Current;
            //WCF services hosted in IIS... 
            VirtualPathExtension p = null;
            // try    
            //  {        
            //      p = OperationContext.Current.Host.Extensions.Find<VirtualPathExtension>(); 
            //  }   
            //  catch (Exception ex) 
            //   {
            //      throw;
            //  }    
            if (ctx != null)
            {
                cfg = WebConfigurationManager.OpenWebConfiguration(ctx.Request.ApplicationPath);

            }
            else if (p != null)
            {
                cfg = WebConfigurationManager.OpenWebConfiguration(p.VirtualPath);

            }
            else
            {
                //cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            return cfg;
        }

        /// <summary>
        /// Gets the channel factory.
        /// </summary>
        /// <returns>The channel factory.</returns>
        public static ChannelFactory GetChannelFactory<T>(IDictionary<string, ChannelFactory<T>> mycachedFactories)
        {
            try
            {
                lock (WCFHelpers.cacheLocker)
                {
                    var endpointName = WCFHelpers.GetEndpointName<T>();

                    if (mycachedFactories.ContainsKey(endpointName))
                    {
                        return mycachedFactories[endpointName];
                    }

                    var factory = new ChannelFactory<T>(endpointName);

                    mycachedFactories.Add(endpointName, factory);
                    return factory;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                //find a way to communicate that we user the factory so we cannot log errors or anything
                throw;
            }


        }

    }

    public class Channelfactoryhelper
    {


        /// <summary>
        /// WCF proxys do not clean up properly if they throw an exception. This method ensures that the service 
        /// proxy is handeled correctly. Do not call TService.Close() or TService.Abort() within the action lambda.
        /// </summary>
        /// <typeparam name="TIServiceContract">The type of the service contract to use</typeparam>
        /// <param name="action">Action to perform with the client instance.</param>
        /// <param name="endpointName">Name of the endpoint to use</param>
        // [System.Diagnostics.DebuggerStepThrough]
        public static void UsingContract<TIServiceContract>(
              Action<TIServiceContract> action)
        {
            Exception mostRecentEx = null;
            var cf = new ChannelFactory<TIServiceContract>(WCFHelpers.GetEndpointName<TIServiceContract>());
            var channel = cf.CreateChannel();
            var clientChannel = (IClientChannel)channel;

            for (int i = 0; i <= 4; i++)
            {
                //        ' Attempt a maximum of 5 times 

                bool success = false;
                try
                {
                    action(channel);
                    if (clientChannel.State != CommunicationState.Faulted)
                    {
                        clientChannel.Close();
                        success = true;
                    }
                }
                catch (ChannelTerminatedException cte)
                {
                    mostRecentEx = cte;
                    clientChannel.Abort();
                    //  delay (backoff) and retry 
                    Thread.Sleep(1000 * (i + 1));
                    // The following is thrown when a remote endpoint could not be found or reached.  The endpoint may not be found or 
                    // reachable because the remote endpoint is down, the remote endpoint is unreachable, or because the remote network is unreachable.
                }
                catch (EndpointNotFoundException enfe)
                {
                    mostRecentEx = enfe;
                    clientChannel.Abort();
                    //  delay (backoff) and retry 
                    Thread.Sleep(1000 * (i + 1));
                    // The following exception that is thrown when a server is too busy to accept a message.
                }
                catch (ServerTooBusyException stbe)
                {
                    mostRecentEx = stbe;
                    clientChannel.Abort();
                    //  delay (backoff) and retry 
                    Thread.Sleep(1000 * (i + 1));
                }
                catch (CommunicationException commProblem)
                {
                    //TO DO log this here
                    //instantiate logger here so it does not break anything else.
                    // throw;
                    clientChannel.Abort();
                    throw;
                }
                catch (TimeoutException e)
                {
                    clientChannel.Abort();

                }
                catch (Exception generatedExceptionName)
                {
                    // rethrow any other exception not defined here
                    // You may want to define a custom Exception class to pass information such as failure count, and failure type
                    clientChannel.Abort();
                    //TO DO log it here using the logger
                    throw;
                }


                finally
                {
                    if (!success) clientChannel.Abort();
                }
                //if we have success it means no error we can stop attempty to connect
                //leave the loop
                if (success) break;
            }

            if (mostRecentEx != null)
            {
                clientChannel.Abort();
                throw new Exception("WCF call failed after 5 retries.", mostRecentEx);
            }

        }

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

            public static string Authheader = "";
            public static string ApiKey = "";

            /// <summary>
            /// A dictionary to hold created channel factories.
            /// </summary>
            private static readonly IDictionary<string, ChannelFactory<T>> cachedFactories =
            new Dictionary<string, ChannelFactory<T>>();


            /// <summary>
            /// Uses the specified code block.
            /// </summary>
            /// <param name="codeBlock">The code block.</param>
            public static void Use(UseServiceDelegate<T> codeBlock)
            {

                dynamic factory = WCFHelpers.GetChannelFactory(cachedFactories);
                // dynamic proxy = (IClientChannel)factory.CreateChannel();
                //test 
                IClientChannel proxy = (IClientChannel)factory.CreateChannel();
                dynamic success = false;
                //  using (OperationContextScope contextScope = new OperationContextScope(proxy))    {  
                //var appHeader = new MessageHeader<string>("appID1");
                //string HeaderAppString = "application";           
                MessageHeader header
              = MessageHeader.CreateHeader(
              "My-CustomHeader",
              "http://myurl",
              "Custom Header."
              );

                Exception mostRecentEx = null;
                for (int i = 0; i <= 4; i++)
                {
                    //        ' Attempt a maximum of 5 times 
                    try
                    {
                        using (OperationContextScope contextScope = new OperationContextScope(proxy))
                        {

                            //for rest endpoint
                            HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
                            // httpRequestProperty.Headers.Add("Apikey", ApiKey);
                            // httpRequestProperty.Headers.Add(HttpRequestHeader.UserAgent, "my user agent");
                            // httpRequestProperty.Headers.Add(HttpRequestHeader.Authorization, Authheader);
                            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                            //for soad
                            //MessageHeader auth = MessageHeader.CreateHeader("Authorization","", Authheader);
                            //MessageHeader apikey = MessageHeader.CreateHeader("Apikey", "", ApiKey);
                            // OperationContext.Current.OutgoingMessageHeaders.Add(auth);
                            // OperationContext.Current.OutgoingMessageHeaders.Add(apikey);
                            codeBlock((T)proxy);
                        }

                        if (proxy.State != CommunicationState.Faulted)
                        {
                            proxy.Close();
                            success = true;
                        }
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
                        //instantiate logger here so it does not break anything else.
                        // throw;
                        proxy.Abort();
                    }
                    catch (TimeoutException e)
                    {
                        proxy.Abort();

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

                    //if we have success it means no error we can stop attempty to connect
                    //leave the loop
                    if (success) break;
                }


                if (mostRecentEx != null)
                {
                    proxy.Abort();
                    throw new Exception("WCF call failed after 5 retries.", mostRecentEx);
                }


            }

        }






    }

    #region "asych test code"


    /// <summary>
    /// Helper class for creating proxies at the client end for the exposed services.
    /// Usage:  ProxyHelper<IService>.Use(serviceProxy =>
    ///        {
    ///            returnObject = serviceProxy.SomeMethod(parameters);
    ///        },ChannelName);
    /// </summary>
    /// <typeparam name="T">This is the type of the interface.</typeparam>
    public class LamdaProxyHelper<T>
    {
        /// <summary>
        /// This is the channel proxy
        /// </summary>
        IClientChannel proxy = null;
        /// <summary>
        /// This is the callback method for an async call back.
        /// </summary>
        AsyncCallback callBack = null;
        /// <summary>
        /// This is the method to be executed.
        /// </summary>
        UseServiceDelegate<T> codeBlock = null;

        /// <summary>
        /// This is the store of the channel.
        /// </summary>
        private static IDictionary<string, ChannelFactory<T>> channelPool
            = new Dictionary<string, ChannelFactory<T>>();

        UseServiceDelegateWithAsyncReturn<T> codeBlockWithAsyncReturn = null;



        /// <summary>
        /// Invokes the method on the WCF interface with the given end point to 
        /// create a channel
        /// Usage
        /// new ProxyHelper<InterfaceName>().Use(serviceProxy =>
        ///         {
        ///             value = serviceProxy.MethodName(params....);
        ///         }, "WCFEndPoint");
        /// </summary>
        /// <param name="codeBlock">The WCF interface method of interface of type T
        /// </param>
        /// <param name="WCFEndPoint">The end point.</param>
        public void Use(UseServiceDelegate<T> codeBlock)
        {
            try
            {
                dynamic factory = WCFHelpers.GetChannelFactory(channelPool);
                IClientChannel proxy = (IClientChannel)factory.CreateChannel();

                //Create an instance of proxy
                // this.proxy = WCFHelpers.GetChannelFactory<T>(channelPool) as IClientChannel;
                if (this.proxy != null)
                {
                    //open the proxy
                    this.proxy.Open();
                    //Call the method
                    codeBlock((T)this.proxy);
                    this.proxy.Close();
                }
            }
            catch (CommunicationException communicationException)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw communicationException;
            }
            catch (TimeoutException timeoutException)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw timeoutException;
            }
            catch (Exception ex)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw ex;
            }
        }

        /// <summary>
        /// This method is called when the proxy is called using an
        /// async method
        /// </summary>
        /// <param name="ar">The result</param>
        private void AsyncResult(IAsyncResult ar)
        {
            //end the invocation
            this.codeBlock.EndInvoke(ar);
            //close the proxy
            this.proxy.Close();
            //callback the method
            this.callBack(ar);
        }

        /// <summary>
        /// Invokes the method on the WCF interface with the given end point to 
        /// create a channel
        /// Usage
        /// new ProxyHelper<InterfaceName>().Use(serviceProxy =>
        ///         {
        ///             value = serviceProxy.MethodName(params....);
        ///         }, "WCFEndPoint",callBackMethodName,id);
        /// </summary>
        /// <param name="codeBlock">The WCF interface method of interface of type T
        /// </param>
        /// <param name="WCFEndPoint">The end point.</param>
        /// <param name="obj">The object instance used to identify in callback</param>
        public void UseAsync(UseServiceDelegate<T> codeBlock, AsyncCallback callBack, object obj)
        {
            try
            {
                dynamic factory = WCFHelpers.GetChannelFactory(channelPool);
                IClientChannel proxy = (IClientChannel)factory.CreateChannel();
                //this.proxy = WCFHelpers.GetChannelFactory<T>(channelPool) as IClientChannel;
                if (this.proxy != null)
                {

                    this.proxy.Open();
                    this.callBack = callBack;
                    this.codeBlock = codeBlock;
                    IAsyncResult result = codeBlock.BeginInvoke((T)this.proxy, AsyncResult, obj);
                }
            }
            catch (CommunicationException communicationException)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw communicationException;
            }
            catch (TimeoutException timeoutException)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw timeoutException;
            }
            catch (Exception ex)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw ex;
            }
        }

        /// <summary>
        /// This method calls the WCF Service in a new thread. The calling of other method for result is the 
        /// responcibility of the client code
        /// </summary>
        /// <param name="codeBlock">The method on the WCF service to be called</param>
        /// <param name="WCFEndPoint">This is the WCF end point</param>
        /// <param name="obj">This is any object which may help in exeution of the async parameters</param>
        public void UseAsyncWithReturnValue(UseServiceDelegateWithAsyncReturn<T> codeBlock, object obj)
        {
            try
            {
                dynamic factory = WCFHelpers.GetChannelFactory(channelPool);
                IClientChannel proxy = (IClientChannel)factory.CreateChannel();
                // this.proxy = WCFHelpers.GetChannelFactory<T>(channelPool) as IClientChannel;
                if (this.proxy != null)
                {
                    this.codeBlockWithAsyncReturn = codeBlock;
                    new Thread(() =>
                    {   //Create a new thread and on the new thread call the methos
                        codeBlock((T)this.proxy, obj);
                        this.proxy.Close();
                    }).Start();

                }
            }
            catch (CommunicationException communicationException)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw communicationException;
            }
            catch (TimeoutException timeoutException)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw timeoutException;
            }
            catch (Exception ex)
            {
                if (this.proxy != null)
                {
                    this.proxy.Abort();
                }
                throw ex;
            }

        }
    }
    #endregion
}


