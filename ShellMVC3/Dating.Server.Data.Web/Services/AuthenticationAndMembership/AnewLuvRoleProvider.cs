
namespace Dating.Server.Data.Services
{

    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
    using Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Dating.Server.Data;
    using Dating.Server.Data.Services;
    using Dating.Server.Data.Models;
    using System.Data.EntityClient;
    using System.Collections.ObjectModel;
    using System.Security.Principal;


    public class AnewLuvRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {

            using (AnewLuvFTSEntities context = new AnewLuvFTSEntities())
            {
                //use the encyrption service in common
                //Dim user = context.profiles.Where(Function(u) u.UserName = username.FirstOrDefault())

                // Dim User As profile = (From o In context.profiles Where o.ProfileID = username)
               IQueryable<Role> myQuery = default(IQueryable<Role>);
                //Dim ctx As New Entities()

                 // join f in  context.[usersInroles]
                  //      on x.ProfileID  equals f.profileID

                myQuery = (from x in context.MembersInRoles 
                     .Where(p => p.ProfileID  == username && ( p.RoleStartDate <= DateTime.Now && DateTime.Now >= p.RoleExpireDate))
                        select new Role 
                        {
                         RoleName = x.Role.RoleName                            
                        });


                //var qry = from p in context.profiles 
                //          from r in _entities.Roles
                //          from u in _entities.Users
                //          where u.Username == username
                //          group r by
                //          new
                //          {
                              
                //              r.RoleID
                //          };

                //return qry.ToArray(); 



                if (myQuery.Count() > 0)
                {
                    //get all the users roles by looping throe query results 
                    string[] ret = new string[myQuery.Count()];
                    int i = 0;
                    foreach (var item in myQuery)
                    {

                        ret[i] = item.RoleName;
                        i++;
                    }
                    return ret;

                }
                else
                {
                    return new string[0];
                }


                // your code here 





            }
        }


        public override bool IsUserInRole(string username, string roleName)
        {


            using (AnewLuvFTSEntities context = new AnewLuvFTSEntities())
            {
                //use the encyrption service in common
                //Dim user = context.profiles.Where(Function(u) u.UserName = username.FirstOrDefault())

                // Dim User As profile = (From o In context.profiles Where o.ProfileID = username)
                IQueryable<Role> myQuery = default(IQueryable<Role>);
                //Dim ctx As New Entities()

                // join f in  context.[usersInroles]
                //      on x.ProfileID  equals f.profileID

                myQuery = (from x in context.MembersInRoles
                     .Where(p => p.ProfileID == username && p.Role.RoleName == roleName && (p.RoleStartDate <= DateTime.Now && DateTime.Now >= p.RoleExpireDate))
                           select new Role
                           {
                               RoleName = x.Role.RoleName
                           });


                //var qry = from p in context.profiles 
                //          from r in _entities.Roles
                //          from u in _entities.Users
                //          where u.Username == username
                //          group r by
                //          new
                //          {

                //              r.RoleID
                //          };

                //return qry.ToArray(); 



                if (myQuery.Count() > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


                // your code here 





            }
        }


        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        // Other overrides not implemented
        #region "Not Implemented Overrides"
       
            
       public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}