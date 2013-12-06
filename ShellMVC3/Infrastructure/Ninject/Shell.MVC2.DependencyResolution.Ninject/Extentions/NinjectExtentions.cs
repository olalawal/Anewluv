using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Activation;

namespace Ninject.Activation
{
    public static class customNinjectExtentions
    {
        /// <summary>
        /// This is the overload we use, basically make sure the IunitOfWork is requesting for  something and if it is find out 
        /// what service called for the UnitOfwork and send back true or false if the requested service matches the one that called it.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="entityNamespace"></param>
        /// <returns></returns>
        public static bool IsInjectingToRepositoryDataSourceOfNamespace(this IRequest request, string entityNamespace)
        {
            if (request.Service.FullName == "Nmedia.DataAccess.Interfaces.IUnitOfWork" | request.Service.FullName == "")
            {
                var ns = request.ParentRequest.Service.FullName;
                return ns == entityNamespace;
            }

            return false;
        }

        public static string IsInjectingToRepositoryDataSourceOfNamespace(this IRequest request)
        {
            if (request.ParentRequest.Service.GetGenericTypeDefinition() == typeof(Nmedia.DataAccess.Interfaces.IContext))
            {
                return request.ParentRequest.Service.GetGenericArguments().First().Namespace;

            }

            return "";
        }
    }
}
