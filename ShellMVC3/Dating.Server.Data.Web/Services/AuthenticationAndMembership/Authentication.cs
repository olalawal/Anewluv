namespace Dating.Server.Data.Services
{


    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    // Authentication.cs
    // Copied from RIA Services Essentials on CodePlex http://riaservices.codeplex.com
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Security;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;


    /// <summary>
    /// A set of utility methods around the authentication scenario.
    /// </summary>
    /// 
    [EnableClientAccess()]
    public sealed class Authentication
    {
        private Authentication()
        {
        }


        // Accounts for base64 overhead
        static internal UserBase GetUser(Type authServiceType, DomainServiceContext context)
        {
            UserBase user = null;

            DomainService authService = DomainService.Factory.CreateDomainService(authServiceType, context);

            if (authService != null)
            {
                try
                {
                    DomainServiceDescription serviceDescription = DomainServiceDescription.GetDescription(authServiceType);
                    DomainOperationEntry queryOperation = serviceDescription.GetQueryMethod("GetUser");

                    QueryDescription query = new QueryDescription(queryOperation);
                    //  IEnumerable<ValidationResult> validationErrors = null;
                    // int totalCount = 

                    //mingh not be ndded 

                    //IEnumerable queryResult = authService.Query(query, validationErrors, totalCount);
                    //if ((validationErrors != null) && validationErrors.Any())
                    //{
                    //    throw new InvalidOperationException(validationErrors.First().ErrorMessage);
                    //}

                    //if (queryResult != null)
                    //{
                    //    user = queryResult.OfType<UserBase>().FirstOrDefault();
                    //}
                }
                finally
                {
                    DomainService.Factory.ReleaseDomainService(authService);
                }
            }

            return user;
        }

        /// <summary>
        /// Gets the current user object using a RIA Services Authentication service.
        /// </summary>
        /// <typeparam name="TUser">The type of the User object in use within the application.</typeparam>
        /// <typeparam name="TAuthenticationService">The type of the Authentication service implementation in the application.</typeparam>
        /// <param name="context">The DomainServiceContext that provides access to services.</param>
        /// <returns>The user object if one can be created successfully; null otherwise.</returns>
        public static TUser GetUser<TUser, TAuthenticationService>(DomainServiceContext context)
            where TUser : UserBase
            where TAuthenticationService : DomainService, IAuthentication<TUser>
        {
            DomainServiceContext authServiceContext = new DomainServiceContext(context, DomainOperationType.Query);
            return (TUser)GetUser(typeof(TAuthenticationService), authServiceContext);
        }



        //<EnableClientAccess()> _
        //Public Class AuthenticationService
        //    Inherits AuthenticationBase(Of User)
        //    Private _service As New DatingService

        //    Protected Overrides Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
        //        ' Code here that tests only if the password is valid for the given 
        //        ' username using your custom DB calls via the domain service you 
        //        ' implemented above 
        //    End Function

        //    Protected Overrides Function GetAuthenticatedUser(ByVal pricipal As IPrincipal) As User
        //        ' principal.Identity.Name will be the username for the user 
        //        ' you're trying to authenticate. Here's one way to implement 
        //        ' this: 
        //        Dim user As User = Nothing
        //        '  If Me._service.DoesUserExist(principal.Identity.Name) Then
        //        ' DoesUserExist() is a call 
        //        ' added in my domain service 
        //        ' UserProfile is an entity in my DB 
        //        'Dim profile As UserProfile = Me._service.GetUserProfile(principal.Identity.Name)
        //        '  user.Name = profile.UserName
        //        '   user.ScreenName = principal.Identity.AuthenticationType
        //        '  End If
        //        Return user
        //    End Function

        //    Public Overrides Sub Initialize(ByVal context As DomainServiceContext)
        //        Me._service.Initialize(context)
        //        MyBase.Initialize(context)
        //    End Sub

        //    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        //        If disposing Then
        //            Me._service.Dispose()
        //        End If
        //        MyBase.Dispose(disposing)
        //    End Sub
        //End Class

    }
}