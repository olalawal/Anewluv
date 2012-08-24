
namespace Dating.Server.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Dating.Server.Data.Models;


    // Implements application logic using the AnewLuvFTSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial  class DatingService : LinqToEntitiesDomainService<AnewLuvFTSEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'abusers' query.
        public IQueryable<abuser> GetAbusers()
        {
            return this.ObjectContext.abusers;
        }

        public void InsertAbuser(abuser abuser)
        {
            if ((abuser.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(abuser, EntityState.Added);
            }
            else
            {
                this.ObjectContext.abusers.AddObject(abuser);
            }
        }

        public void UpdateAbuser(abuser currentabuser)
        {
            this.ObjectContext.abusers.AttachAsModified(currentabuser, this.ChangeSet.GetOriginal(currentabuser));
        }

        public void DeleteAbuser(abuser abuser)
        {
            if ((abuser.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(abuser, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.abusers.Attach(abuser);
                this.ObjectContext.abusers.DeleteObject(abuser);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'abusereports' query.
        public IQueryable<abusereport> GetAbusereports()
        {
            return this.ObjectContext.abusereports;
        }

        public void InsertAbusereport(abusereport abusereport)
        {
            if ((abusereport.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(abusereport, EntityState.Added);
            }
            else
            {
                this.ObjectContext.abusereports.AddObject(abusereport);
            }
        }

        public void UpdateAbusereport(abusereport currentabusereport)
        {
            this.ObjectContext.abusereports.AttachAsModified(currentabusereport, this.ChangeSet.GetOriginal(currentabusereport));
        }

        public void DeleteAbusereport(abusereport abusereport)
        {
            if ((abusereport.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(abusereport, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.abusereports.Attach(abusereport);
                this.ObjectContext.abusereports.DeleteObject(abusereport);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'abusetypes' query.
        public IQueryable<abusetype> GetAbusetypes()
        {
            return this.ObjectContext.abusetypes;
        }

        public void InsertAbusetype(abusetype abusetype)
        {
            if ((abusetype.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(abusetype, EntityState.Added);
            }
            else
            {
                this.ObjectContext.abusetypes.AddObject(abusetype);
            }
        }

        public void UpdateAbusetype(abusetype currentabusetype)
        {
            this.ObjectContext.abusetypes.AttachAsModified(currentabusetype, this.ChangeSet.GetOriginal(currentabusetype));
        }

        public void DeleteAbusetype(abusetype abusetype)
        {
            if ((abusetype.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(abusetype, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.abusetypes.Attach(abusetype);
                this.ObjectContext.abusetypes.DeleteObject(abusetype);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CommunicationQuotas' query.
        public IQueryable<CommunicationQuota> GetCommunicationQuotas()
        {
            return this.ObjectContext.CommunicationQuotas;
        }

        public void InsertCommunicationQuota(CommunicationQuota communicationQuota)
        {
            if ((communicationQuota.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(communicationQuota, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CommunicationQuotas.AddObject(communicationQuota);
            }
        }

        public void UpdateCommunicationQuota(CommunicationQuota currentCommunicationQuota)
        {
            this.ObjectContext.CommunicationQuotas.AttachAsModified(currentCommunicationQuota, this.ChangeSet.GetOriginal(currentCommunicationQuota));
        }

        public void DeleteCommunicationQuota(CommunicationQuota communicationQuota)
        {
            if ((communicationQuota.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(communicationQuota, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CommunicationQuotas.Attach(communicationQuota);
                this.ObjectContext.CommunicationQuotas.DeleteObject(communicationQuota);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaAppearance_Bodytypes' query.
        public IQueryable<CriteriaAppearance_Bodytypes> GetCriteriaAppearance_Bodytypes()
        {
            return this.ObjectContext.CriteriaAppearance_Bodytypes;
        }

        public void InsertCriteriaAppearance_Bodytypes(CriteriaAppearance_Bodytypes criteriaAppearance_Bodytypes)
        {
            if ((criteriaAppearance_Bodytypes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_Bodytypes, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_Bodytypes.AddObject(criteriaAppearance_Bodytypes);
            }
        }

        public void UpdateCriteriaAppearance_Bodytypes(CriteriaAppearance_Bodytypes currentCriteriaAppearance_Bodytypes)
        {
            this.ObjectContext.CriteriaAppearance_Bodytypes.AttachAsModified(currentCriteriaAppearance_Bodytypes, this.ChangeSet.GetOriginal(currentCriteriaAppearance_Bodytypes));
        }

        public void DeleteCriteriaAppearance_Bodytypes(CriteriaAppearance_Bodytypes criteriaAppearance_Bodytypes)
        {
            if ((criteriaAppearance_Bodytypes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_Bodytypes, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_Bodytypes.Attach(criteriaAppearance_Bodytypes);
                this.ObjectContext.CriteriaAppearance_Bodytypes.DeleteObject(criteriaAppearance_Bodytypes);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaAppearance_Ethnicity' query.
        public IQueryable<CriteriaAppearance_Ethnicity> GetCriteriaAppearance_Ethnicity()
        {
            return this.ObjectContext.CriteriaAppearance_Ethnicity;
        }

        public void InsertCriteriaAppearance_Ethnicity(CriteriaAppearance_Ethnicity criteriaAppearance_Ethnicity)
        {
            if ((criteriaAppearance_Ethnicity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_Ethnicity, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_Ethnicity.AddObject(criteriaAppearance_Ethnicity);
            }
        }

        public void UpdateCriteriaAppearance_Ethnicity(CriteriaAppearance_Ethnicity currentCriteriaAppearance_Ethnicity)
        {
            this.ObjectContext.CriteriaAppearance_Ethnicity.AttachAsModified(currentCriteriaAppearance_Ethnicity, this.ChangeSet.GetOriginal(currentCriteriaAppearance_Ethnicity));
        }

        public void DeleteCriteriaAppearance_Ethnicity(CriteriaAppearance_Ethnicity criteriaAppearance_Ethnicity)
        {
            if ((criteriaAppearance_Ethnicity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_Ethnicity, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_Ethnicity.Attach(criteriaAppearance_Ethnicity);
                this.ObjectContext.CriteriaAppearance_Ethnicity.DeleteObject(criteriaAppearance_Ethnicity);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaAppearance_EyeColor' query.
        public IQueryable<CriteriaAppearance_EyeColor> GetCriteriaAppearance_EyeColor()
        {
            return this.ObjectContext.CriteriaAppearance_EyeColor;
        }

        public void InsertCriteriaAppearance_EyeColor(CriteriaAppearance_EyeColor criteriaAppearance_EyeColor)
        {
            if ((criteriaAppearance_EyeColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_EyeColor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_EyeColor.AddObject(criteriaAppearance_EyeColor);
            }
        }

        public void UpdateCriteriaAppearance_EyeColor(CriteriaAppearance_EyeColor currentCriteriaAppearance_EyeColor)
        {
            this.ObjectContext.CriteriaAppearance_EyeColor.AttachAsModified(currentCriteriaAppearance_EyeColor, this.ChangeSet.GetOriginal(currentCriteriaAppearance_EyeColor));
        }

        public void DeleteCriteriaAppearance_EyeColor(CriteriaAppearance_EyeColor criteriaAppearance_EyeColor)
        {
            if ((criteriaAppearance_EyeColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_EyeColor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_EyeColor.Attach(criteriaAppearance_EyeColor);
                this.ObjectContext.CriteriaAppearance_EyeColor.DeleteObject(criteriaAppearance_EyeColor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaAppearance_HairColor' query.
        public IQueryable<CriteriaAppearance_HairColor> GetCriteriaAppearance_HairColor()
        {
            return this.ObjectContext.CriteriaAppearance_HairColor;
        }

        public void InsertCriteriaAppearance_HairColor(CriteriaAppearance_HairColor criteriaAppearance_HairColor)
        {
            if ((criteriaAppearance_HairColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_HairColor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_HairColor.AddObject(criteriaAppearance_HairColor);
            }
        }

        public void UpdateCriteriaAppearance_HairColor(CriteriaAppearance_HairColor currentCriteriaAppearance_HairColor)
        {
            this.ObjectContext.CriteriaAppearance_HairColor.AttachAsModified(currentCriteriaAppearance_HairColor, this.ChangeSet.GetOriginal(currentCriteriaAppearance_HairColor));
        }

        public void DeleteCriteriaAppearance_HairColor(CriteriaAppearance_HairColor criteriaAppearance_HairColor)
        {
            if ((criteriaAppearance_HairColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaAppearance_HairColor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaAppearance_HairColor.Attach(criteriaAppearance_HairColor);
                this.ObjectContext.CriteriaAppearance_HairColor.DeleteObject(criteriaAppearance_HairColor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Diet' query.
        public IQueryable<CriteriaCharacter_Diet> GetCriteriaCharacter_Diet()
        {
            return this.ObjectContext.CriteriaCharacter_Diet;
        }

        public void InsertCriteriaCharacter_Diet(CriteriaCharacter_Diet criteriaCharacter_Diet)
        {
            if ((criteriaCharacter_Diet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Diet, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Diet.AddObject(criteriaCharacter_Diet);
            }
        }

        public void UpdateCriteriaCharacter_Diet(CriteriaCharacter_Diet currentCriteriaCharacter_Diet)
        {
            this.ObjectContext.CriteriaCharacter_Diet.AttachAsModified(currentCriteriaCharacter_Diet, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Diet));
        }

        public void DeleteCriteriaCharacter_Diet(CriteriaCharacter_Diet criteriaCharacter_Diet)
        {
            if ((criteriaCharacter_Diet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Diet, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Diet.Attach(criteriaCharacter_Diet);
                this.ObjectContext.CriteriaCharacter_Diet.DeleteObject(criteriaCharacter_Diet);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Drinks' query.
        public IQueryable<CriteriaCharacter_Drinks> GetCriteriaCharacter_Drinks()
        {
            return this.ObjectContext.CriteriaCharacter_Drinks;
        }

        public void InsertCriteriaCharacter_Drinks(CriteriaCharacter_Drinks criteriaCharacter_Drinks)
        {
            if ((criteriaCharacter_Drinks.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Drinks, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Drinks.AddObject(criteriaCharacter_Drinks);
            }
        }

        public void UpdateCriteriaCharacter_Drinks(CriteriaCharacter_Drinks currentCriteriaCharacter_Drinks)
        {
            this.ObjectContext.CriteriaCharacter_Drinks.AttachAsModified(currentCriteriaCharacter_Drinks, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Drinks));
        }

        public void DeleteCriteriaCharacter_Drinks(CriteriaCharacter_Drinks criteriaCharacter_Drinks)
        {
            if ((criteriaCharacter_Drinks.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Drinks, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Drinks.Attach(criteriaCharacter_Drinks);
                this.ObjectContext.CriteriaCharacter_Drinks.DeleteObject(criteriaCharacter_Drinks);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Exercise' query.
        public IQueryable<CriteriaCharacter_Exercise> GetCriteriaCharacter_Exercise()
        {
            return this.ObjectContext.CriteriaCharacter_Exercise;
        }

        public void InsertCriteriaCharacter_Exercise(CriteriaCharacter_Exercise criteriaCharacter_Exercise)
        {
            if ((criteriaCharacter_Exercise.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Exercise, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Exercise.AddObject(criteriaCharacter_Exercise);
            }
        }

        public void UpdateCriteriaCharacter_Exercise(CriteriaCharacter_Exercise currentCriteriaCharacter_Exercise)
        {
            this.ObjectContext.CriteriaCharacter_Exercise.AttachAsModified(currentCriteriaCharacter_Exercise, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Exercise));
        }

        public void DeleteCriteriaCharacter_Exercise(CriteriaCharacter_Exercise criteriaCharacter_Exercise)
        {
            if ((criteriaCharacter_Exercise.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Exercise, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Exercise.Attach(criteriaCharacter_Exercise);
                this.ObjectContext.CriteriaCharacter_Exercise.DeleteObject(criteriaCharacter_Exercise);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Hobby' query.
        public IQueryable<CriteriaCharacter_Hobby> GetCriteriaCharacter_Hobby()
        {
            return this.ObjectContext.CriteriaCharacter_Hobby;
        }

        public void InsertCriteriaCharacter_Hobby(CriteriaCharacter_Hobby criteriaCharacter_Hobby)
        {
            if ((criteriaCharacter_Hobby.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Hobby, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Hobby.AddObject(criteriaCharacter_Hobby);
            }
        }

        public void UpdateCriteriaCharacter_Hobby(CriteriaCharacter_Hobby currentCriteriaCharacter_Hobby)
        {
            this.ObjectContext.CriteriaCharacter_Hobby.AttachAsModified(currentCriteriaCharacter_Hobby, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Hobby));
        }

        public void DeleteCriteriaCharacter_Hobby(CriteriaCharacter_Hobby criteriaCharacter_Hobby)
        {
            if ((criteriaCharacter_Hobby.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Hobby, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Hobby.Attach(criteriaCharacter_Hobby);
                this.ObjectContext.CriteriaCharacter_Hobby.DeleteObject(criteriaCharacter_Hobby);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_HotFeature' query.
        public IQueryable<CriteriaCharacter_HotFeature> GetCriteriaCharacter_HotFeature()
        {
            return this.ObjectContext.CriteriaCharacter_HotFeature;
        }

        public void InsertCriteriaCharacter_HotFeature(CriteriaCharacter_HotFeature criteriaCharacter_HotFeature)
        {
            if ((criteriaCharacter_HotFeature.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_HotFeature, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_HotFeature.AddObject(criteriaCharacter_HotFeature);
            }
        }

        public void UpdateCriteriaCharacter_HotFeature(CriteriaCharacter_HotFeature currentCriteriaCharacter_HotFeature)
        {
            this.ObjectContext.CriteriaCharacter_HotFeature.AttachAsModified(currentCriteriaCharacter_HotFeature, this.ChangeSet.GetOriginal(currentCriteriaCharacter_HotFeature));
        }

        public void DeleteCriteriaCharacter_HotFeature(CriteriaCharacter_HotFeature criteriaCharacter_HotFeature)
        {
            if ((criteriaCharacter_HotFeature.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_HotFeature, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_HotFeature.Attach(criteriaCharacter_HotFeature);
                this.ObjectContext.CriteriaCharacter_HotFeature.DeleteObject(criteriaCharacter_HotFeature);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Humor' query.
        public IQueryable<CriteriaCharacter_Humor> GetCriteriaCharacter_Humor()
        {
            return this.ObjectContext.CriteriaCharacter_Humor;
        }

        public void InsertCriteriaCharacter_Humor(CriteriaCharacter_Humor criteriaCharacter_Humor)
        {
            if ((criteriaCharacter_Humor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Humor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Humor.AddObject(criteriaCharacter_Humor);
            }
        }

        public void UpdateCriteriaCharacter_Humor(CriteriaCharacter_Humor currentCriteriaCharacter_Humor)
        {
            this.ObjectContext.CriteriaCharacter_Humor.AttachAsModified(currentCriteriaCharacter_Humor, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Humor));
        }

        public void DeleteCriteriaCharacter_Humor(CriteriaCharacter_Humor criteriaCharacter_Humor)
        {
            if ((criteriaCharacter_Humor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Humor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Humor.Attach(criteriaCharacter_Humor);
                this.ObjectContext.CriteriaCharacter_Humor.DeleteObject(criteriaCharacter_Humor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_PoliticalView' query.
        public IQueryable<CriteriaCharacter_PoliticalView> GetCriteriaCharacter_PoliticalView()
        {
            return this.ObjectContext.CriteriaCharacter_PoliticalView;
        }

        public void InsertCriteriaCharacter_PoliticalView(CriteriaCharacter_PoliticalView criteriaCharacter_PoliticalView)
        {
            if ((criteriaCharacter_PoliticalView.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_PoliticalView, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_PoliticalView.AddObject(criteriaCharacter_PoliticalView);
            }
        }

        public void UpdateCriteriaCharacter_PoliticalView(CriteriaCharacter_PoliticalView currentCriteriaCharacter_PoliticalView)
        {
            this.ObjectContext.CriteriaCharacter_PoliticalView.AttachAsModified(currentCriteriaCharacter_PoliticalView, this.ChangeSet.GetOriginal(currentCriteriaCharacter_PoliticalView));
        }

        public void DeleteCriteriaCharacter_PoliticalView(CriteriaCharacter_PoliticalView criteriaCharacter_PoliticalView)
        {
            if ((criteriaCharacter_PoliticalView.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_PoliticalView, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_PoliticalView.Attach(criteriaCharacter_PoliticalView);
                this.ObjectContext.CriteriaCharacter_PoliticalView.DeleteObject(criteriaCharacter_PoliticalView);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Religion' query.
        public IQueryable<CriteriaCharacter_Religion> GetCriteriaCharacter_Religion()
        {
            return this.ObjectContext.CriteriaCharacter_Religion;
        }

        public void InsertCriteriaCharacter_Religion(CriteriaCharacter_Religion criteriaCharacter_Religion)
        {
            if ((criteriaCharacter_Religion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Religion, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Religion.AddObject(criteriaCharacter_Religion);
            }
        }

        public void UpdateCriteriaCharacter_Religion(CriteriaCharacter_Religion currentCriteriaCharacter_Religion)
        {
            this.ObjectContext.CriteriaCharacter_Religion.AttachAsModified(currentCriteriaCharacter_Religion, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Religion));
        }

        public void DeleteCriteriaCharacter_Religion(CriteriaCharacter_Religion criteriaCharacter_Religion)
        {
            if ((criteriaCharacter_Religion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Religion, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Religion.Attach(criteriaCharacter_Religion);
                this.ObjectContext.CriteriaCharacter_Religion.DeleteObject(criteriaCharacter_Religion);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_ReligiousAttendance' query.
        public IQueryable<CriteriaCharacter_ReligiousAttendance> GetCriteriaCharacter_ReligiousAttendance()
        {
            return this.ObjectContext.CriteriaCharacter_ReligiousAttendance;
        }

        public void InsertCriteriaCharacter_ReligiousAttendance(CriteriaCharacter_ReligiousAttendance criteriaCharacter_ReligiousAttendance)
        {
            if ((criteriaCharacter_ReligiousAttendance.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_ReligiousAttendance, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_ReligiousAttendance.AddObject(criteriaCharacter_ReligiousAttendance);
            }
        }

        public void UpdateCriteriaCharacter_ReligiousAttendance(CriteriaCharacter_ReligiousAttendance currentCriteriaCharacter_ReligiousAttendance)
        {
            this.ObjectContext.CriteriaCharacter_ReligiousAttendance.AttachAsModified(currentCriteriaCharacter_ReligiousAttendance, this.ChangeSet.GetOriginal(currentCriteriaCharacter_ReligiousAttendance));
        }

        public void DeleteCriteriaCharacter_ReligiousAttendance(CriteriaCharacter_ReligiousAttendance criteriaCharacter_ReligiousAttendance)
        {
            if ((criteriaCharacter_ReligiousAttendance.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_ReligiousAttendance, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_ReligiousAttendance.Attach(criteriaCharacter_ReligiousAttendance);
                this.ObjectContext.CriteriaCharacter_ReligiousAttendance.DeleteObject(criteriaCharacter_ReligiousAttendance);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Sign' query.
        public IQueryable<CriteriaCharacter_Sign> GetCriteriaCharacter_Sign()
        {
            return this.ObjectContext.CriteriaCharacter_Sign;
        }

        public void InsertCriteriaCharacter_Sign(CriteriaCharacter_Sign criteriaCharacter_Sign)
        {
            if ((criteriaCharacter_Sign.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Sign, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Sign.AddObject(criteriaCharacter_Sign);
            }
        }

        public void UpdateCriteriaCharacter_Sign(CriteriaCharacter_Sign currentCriteriaCharacter_Sign)
        {
            this.ObjectContext.CriteriaCharacter_Sign.AttachAsModified(currentCriteriaCharacter_Sign, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Sign));
        }

        public void DeleteCriteriaCharacter_Sign(CriteriaCharacter_Sign criteriaCharacter_Sign)
        {
            if ((criteriaCharacter_Sign.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Sign, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Sign.Attach(criteriaCharacter_Sign);
                this.ObjectContext.CriteriaCharacter_Sign.DeleteObject(criteriaCharacter_Sign);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaCharacter_Smokes' query.
        public IQueryable<CriteriaCharacter_Smokes> GetCriteriaCharacter_Smokes()
        {
            return this.ObjectContext.CriteriaCharacter_Smokes;
        }

        public void InsertCriteriaCharacter_Smokes(CriteriaCharacter_Smokes criteriaCharacter_Smokes)
        {
            if ((criteriaCharacter_Smokes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Smokes, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Smokes.AddObject(criteriaCharacter_Smokes);
            }
        }

        public void UpdateCriteriaCharacter_Smokes(CriteriaCharacter_Smokes currentCriteriaCharacter_Smokes)
        {
            this.ObjectContext.CriteriaCharacter_Smokes.AttachAsModified(currentCriteriaCharacter_Smokes, this.ChangeSet.GetOriginal(currentCriteriaCharacter_Smokes));
        }

        public void DeleteCriteriaCharacter_Smokes(CriteriaCharacter_Smokes criteriaCharacter_Smokes)
        {
            if ((criteriaCharacter_Smokes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaCharacter_Smokes, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaCharacter_Smokes.Attach(criteriaCharacter_Smokes);
                this.ObjectContext.CriteriaCharacter_Smokes.DeleteObject(criteriaCharacter_Smokes);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_EducationLevel' query.
        public IQueryable<CriteriaLife_EducationLevel> GetCriteriaLife_EducationLevel()
        {
            return this.ObjectContext.CriteriaLife_EducationLevel;
        }

        public void InsertCriteriaLife_EducationLevel(CriteriaLife_EducationLevel criteriaLife_EducationLevel)
        {
            if ((criteriaLife_EducationLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_EducationLevel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_EducationLevel.AddObject(criteriaLife_EducationLevel);
            }
        }

        public void UpdateCriteriaLife_EducationLevel(CriteriaLife_EducationLevel currentCriteriaLife_EducationLevel)
        {
            this.ObjectContext.CriteriaLife_EducationLevel.AttachAsModified(currentCriteriaLife_EducationLevel, this.ChangeSet.GetOriginal(currentCriteriaLife_EducationLevel));
        }

        public void DeleteCriteriaLife_EducationLevel(CriteriaLife_EducationLevel criteriaLife_EducationLevel)
        {
            if ((criteriaLife_EducationLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_EducationLevel, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_EducationLevel.Attach(criteriaLife_EducationLevel);
                this.ObjectContext.CriteriaLife_EducationLevel.DeleteObject(criteriaLife_EducationLevel);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_EmploymentStatus' query.
        public IQueryable<CriteriaLife_EmploymentStatus> GetCriteriaLife_EmploymentStatus()
        {
            return this.ObjectContext.CriteriaLife_EmploymentStatus;
        }

        public void InsertCriteriaLife_EmploymentStatus(CriteriaLife_EmploymentStatus criteriaLife_EmploymentStatus)
        {
            if ((criteriaLife_EmploymentStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_EmploymentStatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_EmploymentStatus.AddObject(criteriaLife_EmploymentStatus);
            }
        }

        public void UpdateCriteriaLife_EmploymentStatus(CriteriaLife_EmploymentStatus currentCriteriaLife_EmploymentStatus)
        {
            this.ObjectContext.CriteriaLife_EmploymentStatus.AttachAsModified(currentCriteriaLife_EmploymentStatus, this.ChangeSet.GetOriginal(currentCriteriaLife_EmploymentStatus));
        }

        public void DeleteCriteriaLife_EmploymentStatus(CriteriaLife_EmploymentStatus criteriaLife_EmploymentStatus)
        {
            if ((criteriaLife_EmploymentStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_EmploymentStatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_EmploymentStatus.Attach(criteriaLife_EmploymentStatus);
                this.ObjectContext.CriteriaLife_EmploymentStatus.DeleteObject(criteriaLife_EmploymentStatus);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_HaveKids' query.
        public IQueryable<CriteriaLife_HaveKids> GetCriteriaLife_HaveKids()
        {
            return this.ObjectContext.CriteriaLife_HaveKids;
        }

        public void InsertCriteriaLife_HaveKids(CriteriaLife_HaveKids criteriaLife_HaveKids)
        {
            if ((criteriaLife_HaveKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_HaveKids, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_HaveKids.AddObject(criteriaLife_HaveKids);
            }
        }

        public void UpdateCriteriaLife_HaveKids(CriteriaLife_HaveKids currentCriteriaLife_HaveKids)
        {
            this.ObjectContext.CriteriaLife_HaveKids.AttachAsModified(currentCriteriaLife_HaveKids, this.ChangeSet.GetOriginal(currentCriteriaLife_HaveKids));
        }

        public void DeleteCriteriaLife_HaveKids(CriteriaLife_HaveKids criteriaLife_HaveKids)
        {
            if ((criteriaLife_HaveKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_HaveKids, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_HaveKids.Attach(criteriaLife_HaveKids);
                this.ObjectContext.CriteriaLife_HaveKids.DeleteObject(criteriaLife_HaveKids);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_IncomeLevel' query.
        public IQueryable<CriteriaLife_IncomeLevel> GetCriteriaLife_IncomeLevel()
        {
            return this.ObjectContext.CriteriaLife_IncomeLevel;
        }

        public void InsertCriteriaLife_IncomeLevel(CriteriaLife_IncomeLevel criteriaLife_IncomeLevel)
        {
            if ((criteriaLife_IncomeLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_IncomeLevel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_IncomeLevel.AddObject(criteriaLife_IncomeLevel);
            }
        }

        public void UpdateCriteriaLife_IncomeLevel(CriteriaLife_IncomeLevel currentCriteriaLife_IncomeLevel)
        {
            this.ObjectContext.CriteriaLife_IncomeLevel.AttachAsModified(currentCriteriaLife_IncomeLevel, this.ChangeSet.GetOriginal(currentCriteriaLife_IncomeLevel));
        }

        public void DeleteCriteriaLife_IncomeLevel(CriteriaLife_IncomeLevel criteriaLife_IncomeLevel)
        {
            if ((criteriaLife_IncomeLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_IncomeLevel, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_IncomeLevel.Attach(criteriaLife_IncomeLevel);
                this.ObjectContext.CriteriaLife_IncomeLevel.DeleteObject(criteriaLife_IncomeLevel);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_LivingSituation' query.
        public IQueryable<CriteriaLife_LivingSituation> GetCriteriaLife_LivingSituation()
        {
            return this.ObjectContext.CriteriaLife_LivingSituation;
        }

        public void InsertCriteriaLife_LivingSituation(CriteriaLife_LivingSituation criteriaLife_LivingSituation)
        {
            if ((criteriaLife_LivingSituation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_LivingSituation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_LivingSituation.AddObject(criteriaLife_LivingSituation);
            }
        }

        public void UpdateCriteriaLife_LivingSituation(CriteriaLife_LivingSituation currentCriteriaLife_LivingSituation)
        {
            this.ObjectContext.CriteriaLife_LivingSituation.AttachAsModified(currentCriteriaLife_LivingSituation, this.ChangeSet.GetOriginal(currentCriteriaLife_LivingSituation));
        }

        public void DeleteCriteriaLife_LivingSituation(CriteriaLife_LivingSituation criteriaLife_LivingSituation)
        {
            if ((criteriaLife_LivingSituation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_LivingSituation, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_LivingSituation.Attach(criteriaLife_LivingSituation);
                this.ObjectContext.CriteriaLife_LivingSituation.DeleteObject(criteriaLife_LivingSituation);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_LookingFor' query.
        public IQueryable<CriteriaLife_LookingFor> GetCriteriaLife_LookingFor()
        {
            return this.ObjectContext.CriteriaLife_LookingFor;
        }

        public void InsertCriteriaLife_LookingFor(CriteriaLife_LookingFor criteriaLife_LookingFor)
        {
            if ((criteriaLife_LookingFor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_LookingFor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_LookingFor.AddObject(criteriaLife_LookingFor);
            }
        }

        public void UpdateCriteriaLife_LookingFor(CriteriaLife_LookingFor currentCriteriaLife_LookingFor)
        {
            this.ObjectContext.CriteriaLife_LookingFor.AttachAsModified(currentCriteriaLife_LookingFor, this.ChangeSet.GetOriginal(currentCriteriaLife_LookingFor));
        }

        public void DeleteCriteriaLife_LookingFor(CriteriaLife_LookingFor criteriaLife_LookingFor)
        {
            if ((criteriaLife_LookingFor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_LookingFor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_LookingFor.Attach(criteriaLife_LookingFor);
                this.ObjectContext.CriteriaLife_LookingFor.DeleteObject(criteriaLife_LookingFor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_MaritalStatus' query.
        public IQueryable<CriteriaLife_MaritalStatus> GetCriteriaLife_MaritalStatus()
        {
            return this.ObjectContext.CriteriaLife_MaritalStatus;
        }

        public void InsertCriteriaLife_MaritalStatus(CriteriaLife_MaritalStatus criteriaLife_MaritalStatus)
        {
            if ((criteriaLife_MaritalStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_MaritalStatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_MaritalStatus.AddObject(criteriaLife_MaritalStatus);
            }
        }

        public void UpdateCriteriaLife_MaritalStatus(CriteriaLife_MaritalStatus currentCriteriaLife_MaritalStatus)
        {
            this.ObjectContext.CriteriaLife_MaritalStatus.AttachAsModified(currentCriteriaLife_MaritalStatus, this.ChangeSet.GetOriginal(currentCriteriaLife_MaritalStatus));
        }

        public void DeleteCriteriaLife_MaritalStatus(CriteriaLife_MaritalStatus criteriaLife_MaritalStatus)
        {
            if ((criteriaLife_MaritalStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_MaritalStatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_MaritalStatus.Attach(criteriaLife_MaritalStatus);
                this.ObjectContext.CriteriaLife_MaritalStatus.DeleteObject(criteriaLife_MaritalStatus);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_Profession' query.
        public IQueryable<CriteriaLife_Profession> GetCriteriaLife_Profession()
        {
            return this.ObjectContext.CriteriaLife_Profession;
        }

        public void InsertCriteriaLife_Profession(CriteriaLife_Profession criteriaLife_Profession)
        {
            if ((criteriaLife_Profession.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_Profession, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_Profession.AddObject(criteriaLife_Profession);
            }
        }

        public void UpdateCriteriaLife_Profession(CriteriaLife_Profession currentCriteriaLife_Profession)
        {
            this.ObjectContext.CriteriaLife_Profession.AttachAsModified(currentCriteriaLife_Profession, this.ChangeSet.GetOriginal(currentCriteriaLife_Profession));
        }

        public void DeleteCriteriaLife_Profession(CriteriaLife_Profession criteriaLife_Profession)
        {
            if ((criteriaLife_Profession.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_Profession, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_Profession.Attach(criteriaLife_Profession);
                this.ObjectContext.CriteriaLife_Profession.DeleteObject(criteriaLife_Profession);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CriteriaLife_WantsKids' query.
        public IQueryable<CriteriaLife_WantsKids> GetCriteriaLife_WantsKids()
        {
            return this.ObjectContext.CriteriaLife_WantsKids;
        }

        public void InsertCriteriaLife_WantsKids(CriteriaLife_WantsKids criteriaLife_WantsKids)
        {
            if ((criteriaLife_WantsKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_WantsKids, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CriteriaLife_WantsKids.AddObject(criteriaLife_WantsKids);
            }
        }

        public void UpdateCriteriaLife_WantsKids(CriteriaLife_WantsKids currentCriteriaLife_WantsKids)
        {
            this.ObjectContext.CriteriaLife_WantsKids.AttachAsModified(currentCriteriaLife_WantsKids, this.ChangeSet.GetOriginal(currentCriteriaLife_WantsKids));
        }

        public void DeleteCriteriaLife_WantsKids(CriteriaLife_WantsKids criteriaLife_WantsKids)
        {
            if ((criteriaLife_WantsKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(criteriaLife_WantsKids, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CriteriaLife_WantsKids.Attach(criteriaLife_WantsKids);
                this.ObjectContext.CriteriaLife_WantsKids.DeleteObject(criteriaLife_WantsKids);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'databaseerrors' query.
        public IQueryable<databaseerror> GetDatabaseerrors()
        {
            return this.ObjectContext.databaseerrors;
        }

        public void InsertDatabaseerror(databaseerror databaseerror)
        {
            if ((databaseerror.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(databaseerror, EntityState.Added);
            }
            else
            {
                this.ObjectContext.databaseerrors.AddObject(databaseerror);
            }
        }

        public void UpdateDatabaseerror(databaseerror currentdatabaseerror)
        {
            this.ObjectContext.databaseerrors.AttachAsModified(currentdatabaseerror, this.ChangeSet.GetOriginal(currentdatabaseerror));
        }

        public void DeleteDatabaseerror(databaseerror databaseerror)
        {
            if ((databaseerror.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(databaseerror, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.databaseerrors.Attach(databaseerror);
                this.ObjectContext.databaseerrors.DeleteObject(databaseerror);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'emailerrors' query.
        public IQueryable<emailerror> GetEmailerrors()
        {
            return this.ObjectContext.emailerrors;
        }

        public void InsertEmailerror(emailerror emailerror)
        {
            if ((emailerror.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(emailerror, EntityState.Added);
            }
            else
            {
                this.ObjectContext.emailerrors.AddObject(emailerror);
            }
        }

        public void UpdateEmailerror(emailerror currentemailerror)
        {
            this.ObjectContext.emailerrors.AttachAsModified(currentemailerror, this.ChangeSet.GetOriginal(currentemailerror));
        }

        public void DeleteEmailerror(emailerror emailerror)
        {
            if ((emailerror.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(emailerror, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.emailerrors.Attach(emailerror);
                this.ObjectContext.emailerrors.DeleteObject(emailerror);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'favorites' query.
        public IQueryable<favorite> GetFavorites()
        {
            return this.ObjectContext.favorites;
        }

        public void InsertFavorite(favorite favorite)
        {
            if ((favorite.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(favorite, EntityState.Added);
            }
            else
            {
                this.ObjectContext.favorites.AddObject(favorite);
            }
        }

        public void UpdateFavorite(favorite currentfavorite)
        {
            this.ObjectContext.favorites.AttachAsModified(currentfavorite, this.ChangeSet.GetOriginal(currentfavorite));
        }

        public void DeleteFavorite(favorite favorite)
        {
            if ((favorite.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(favorite, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.favorites.Attach(favorite);
                this.ObjectContext.favorites.DeleteObject(favorite);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Friends' query.
        public IQueryable<Friend> GetFriends()
        {
            return this.ObjectContext.Friends;
        }

        public void InsertFriend(Friend friend)
        {
            if ((friend.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(friend, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Friends.AddObject(friend);
            }
        }

        public void UpdateFriend(Friend currentFriend)
        {
            this.ObjectContext.Friends.AttachAsModified(currentFriend, this.ChangeSet.GetOriginal(currentFriend));
        }

        public void DeleteFriend(Friend friend)
        {
            if ((friend.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(friend, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Friends.Attach(friend);
                this.ObjectContext.Friends.DeleteObject(friend);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'genders' query.
        public IQueryable<gender> GetGenders()
        {
            return this.ObjectContext.genders;
        }

        public void InsertGender(gender gender)
        {
            if ((gender.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gender, EntityState.Added);
            }
            else
            {
                this.ObjectContext.genders.AddObject(gender);
            }
        }

        public void UpdateGender(gender currentgender)
        {
            this.ObjectContext.genders.AttachAsModified(currentgender, this.ChangeSet.GetOriginal(currentgender));
        }

        public void DeleteGender(gender gender)
        {
            if ((gender.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gender, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.genders.Attach(gender);
                this.ObjectContext.genders.DeleteObject(gender);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Heights' query.
        public IQueryable<Height> GetHeights()
        {
            return this.ObjectContext.Heights;
        }

        public void InsertHeight(Height height)
        {
            if ((height.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(height, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Heights.AddObject(height);
            }
        }

        public void UpdateHeight(Height currentHeight)
        {
            this.ObjectContext.Heights.AttachAsModified(currentHeight, this.ChangeSet.GetOriginal(currentHeight));
        }

        public void DeleteHeight(Height height)
        {
            if ((height.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(height, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Heights.Attach(height);
                this.ObjectContext.Heights.DeleteObject(height);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Hotlists' query.
        public IQueryable<Hotlist> GetHotlists()
        {
            return this.ObjectContext.Hotlists;
        }

        public void InsertHotlist(Hotlist hotlist)
        {
            if ((hotlist.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(hotlist, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Hotlists.AddObject(hotlist);
            }
        }

        public void UpdateHotlist(Hotlist currentHotlist)
        {
            this.ObjectContext.Hotlists.AttachAsModified(currentHotlist, this.ChangeSet.GetOriginal(currentHotlist));
        }

        public void DeleteHotlist(Hotlist hotlist)
        {
            if ((hotlist.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(hotlist, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Hotlists.Attach(hotlist);
                this.ObjectContext.Hotlists.DeleteObject(hotlist);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Interests' query.
        public IQueryable<Interest> GetInterests()
        {
            return this.ObjectContext.Interests;
        }

        public void InsertInterest(Interest interest)
        {
            if ((interest.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(interest, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Interests.AddObject(interest);
            }
        }

        public void UpdateInterest(Interest currentInterest)
        {
            this.ObjectContext.Interests.AttachAsModified(currentInterest, this.ChangeSet.GetOriginal(currentInterest));
        }

        public void DeleteInterest(Interest interest)
        {
            if ((interest.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(interest, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Interests.Attach(interest);
                this.ObjectContext.Interests.DeleteObject(interest);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Likes' query.
        public IQueryable<Like> GetLikes()
        {
            return this.ObjectContext.Likes;
        }

        public void InsertLike(Like like)
        {
            if ((like.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(like, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Likes.AddObject(like);
            }
        }

        public void UpdateLike(Like currentLike)
        {
            this.ObjectContext.Likes.AttachAsModified(currentLike, this.ChangeSet.GetOriginal(currentLike));
        }

        public void DeleteLike(Like like)
        {
            if ((like.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(like, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Likes.Attach(like);
                this.ObjectContext.Likes.DeleteObject(like);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Mailboxblocks' query.
        public IQueryable<Mailboxblock> GetMailboxblocks()
        {
            return this.ObjectContext.Mailboxblocks;
        }

        public void InsertMailboxblock(Mailboxblock mailboxblock)
        {
            if ((mailboxblock.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxblock, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Mailboxblocks.AddObject(mailboxblock);
            }
        }

        public void UpdateMailboxblock(Mailboxblock currentMailboxblock)
        {
            this.ObjectContext.Mailboxblocks.AttachAsModified(currentMailboxblock, this.ChangeSet.GetOriginal(currentMailboxblock));
        }

        public void DeleteMailboxblock(Mailboxblock mailboxblock)
        {
            if ((mailboxblock.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxblock, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Mailboxblocks.Attach(mailboxblock);
                this.ObjectContext.Mailboxblocks.DeleteObject(mailboxblock);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MailboxFolders' query.
        public IQueryable<MailboxFolder> GetMailboxFolders()
        {
            return this.ObjectContext.MailboxFolders;
        }

        public void InsertMailboxFolder(MailboxFolder mailboxFolder)
        {
            if ((mailboxFolder.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxFolder, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MailboxFolders.AddObject(mailboxFolder);
            }
        }

        public void UpdateMailboxFolder(MailboxFolder currentMailboxFolder)
        {
            this.ObjectContext.MailboxFolders.AttachAsModified(currentMailboxFolder, this.ChangeSet.GetOriginal(currentMailboxFolder));
        }

        public void DeleteMailboxFolder(MailboxFolder mailboxFolder)
        {
            if ((mailboxFolder.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxFolder, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MailboxFolders.Attach(mailboxFolder);
                this.ObjectContext.MailboxFolders.DeleteObject(mailboxFolder);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MailboxFolderTypes' query.
        public IQueryable<MailboxFolderType> GetMailboxFolderTypes()
        {
            return this.ObjectContext.MailboxFolderTypes;
        }

        public void InsertMailboxFolderType(MailboxFolderType mailboxFolderType)
        {
            if ((mailboxFolderType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxFolderType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MailboxFolderTypes.AddObject(mailboxFolderType);
            }
        }

        public void UpdateMailboxFolderType(MailboxFolderType currentMailboxFolderType)
        {
            this.ObjectContext.MailboxFolderTypes.AttachAsModified(currentMailboxFolderType, this.ChangeSet.GetOriginal(currentMailboxFolderType));
        }

        public void DeleteMailboxFolderType(MailboxFolderType mailboxFolderType)
        {
            if ((mailboxFolderType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxFolderType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MailboxFolderTypes.Attach(mailboxFolderType);
                this.ObjectContext.MailboxFolderTypes.DeleteObject(mailboxFolderType);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MailboxMessages' query.
        public IQueryable<MailboxMessage> GetMailboxMessages()
        {
            return this.ObjectContext.MailboxMessages;
        }

        public void InsertMailboxMessage(MailboxMessage mailboxMessage)
        {
            if ((mailboxMessage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxMessage, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MailboxMessages.AddObject(mailboxMessage);
            }
        }

        public void UpdateMailboxMessage(MailboxMessage currentMailboxMessage)
        {
            this.ObjectContext.MailboxMessages.AttachAsModified(currentMailboxMessage, this.ChangeSet.GetOriginal(currentMailboxMessage));
        }

        public void DeleteMailboxMessage(MailboxMessage mailboxMessage)
        {
            if ((mailboxMessage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxMessage, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MailboxMessages.Attach(mailboxMessage);
                this.ObjectContext.MailboxMessages.DeleteObject(mailboxMessage);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MailboxMessagesFolders' query.
        public IQueryable<MailboxMessagesFolder> GetMailboxMessagesFolders()
        {
            return this.ObjectContext.MailboxMessagesFolders;
        }

        public void InsertMailboxMessagesFolder(MailboxMessagesFolder mailboxMessagesFolder)
        {
            if ((mailboxMessagesFolder.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxMessagesFolder, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MailboxMessagesFolders.AddObject(mailboxMessagesFolder);
            }
        }

        public void UpdateMailboxMessagesFolder(MailboxMessagesFolder currentMailboxMessagesFolder)
        {
            this.ObjectContext.MailboxMessagesFolders.AttachAsModified(currentMailboxMessagesFolder, this.ChangeSet.GetOriginal(currentMailboxMessagesFolder));
        }

        public void DeleteMailboxMessagesFolder(MailboxMessagesFolder mailboxMessagesFolder)
        {
            if ((mailboxMessagesFolder.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mailboxMessagesFolder, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MailboxMessagesFolders.Attach(mailboxMessagesFolder);
                this.ObjectContext.MailboxMessagesFolders.DeleteObject(mailboxMessagesFolder);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MembersInRoles' query.
        public IQueryable<MembersInRole> GetMembersInRoles()
        {
            return this.ObjectContext.MembersInRoles;
        }

        public void InsertMembersInRole(MembersInRole membersInRole)
        {
            if ((membersInRole.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(membersInRole, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MembersInRoles.AddObject(membersInRole);
            }
        }

        public void UpdateMembersInRole(MembersInRole currentMembersInRole)
        {
            this.ObjectContext.MembersInRoles.AttachAsModified(currentMembersInRole, this.ChangeSet.GetOriginal(currentMembersInRole));
        }

        public void DeleteMembersInRole(MembersInRole membersInRole)
        {
            if ((membersInRole.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(membersInRole, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MembersInRoles.Attach(membersInRole);
                this.ObjectContext.MembersInRoles.DeleteObject(membersInRole);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'photos' query.
        public IQueryable<photo> GetPhotos()
        {
            return this.ObjectContext.photos;
        }

        public void InsertPhoto(photo photo)
        {
            if ((photo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.photos.AddObject(photo);
            }
        }

        public void UpdatePhoto(photo currentphoto)
        {
            this.ObjectContext.photos.AttachAsModified(currentphoto, this.ChangeSet.GetOriginal(currentphoto));
        }

        public void DeletePhoto(photo photo)
        {
            if ((photo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.photos.Attach(photo);
                this.ObjectContext.photos.DeleteObject(photo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PhotoAlbums' query.
        public IQueryable<PhotoAlbum> GetPhotoAlbums()
        {
            return this.ObjectContext.PhotoAlbums;
        }

        public void InsertPhotoAlbum(PhotoAlbum photoAlbum)
        {
            if ((photoAlbum.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoAlbum, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PhotoAlbums.AddObject(photoAlbum);
            }
        }

        public void UpdatePhotoAlbum(PhotoAlbum currentPhotoAlbum)
        {
            this.ObjectContext.PhotoAlbums.AttachAsModified(currentPhotoAlbum, this.ChangeSet.GetOriginal(currentPhotoAlbum));
        }

        public void DeletePhotoAlbum(PhotoAlbum photoAlbum)
        {
            if ((photoAlbum.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoAlbum, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PhotoAlbums.Attach(photoAlbum);
                this.ObjectContext.PhotoAlbums.DeleteObject(photoAlbum);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PhotoRejectionReasons' query.
        public IQueryable<PhotoRejectionReason> GetPhotoRejectionReasons()
        {
            return this.ObjectContext.PhotoRejectionReasons;
        }

        public void InsertPhotoRejectionReason(PhotoRejectionReason photoRejectionReason)
        {
            if ((photoRejectionReason.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoRejectionReason, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PhotoRejectionReasons.AddObject(photoRejectionReason);
            }
        }

        public void UpdatePhotoRejectionReason(PhotoRejectionReason currentPhotoRejectionReason)
        {
            this.ObjectContext.PhotoRejectionReasons.AttachAsModified(currentPhotoRejectionReason, this.ChangeSet.GetOriginal(currentPhotoRejectionReason));
        }

        public void DeletePhotoRejectionReason(PhotoRejectionReason photoRejectionReason)
        {
            if ((photoRejectionReason.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoRejectionReason, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PhotoRejectionReasons.Attach(photoRejectionReason);
                this.ObjectContext.PhotoRejectionReasons.DeleteObject(photoRejectionReason);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PhotoReviewStatus' query.
        public IQueryable<PhotoReviewStatu> GetPhotoReviewStatus()
        {
            return this.ObjectContext.PhotoReviewStatus;
        }

        public void InsertPhotoReviewStatu(PhotoReviewStatu photoReviewStatu)
        {
            if ((photoReviewStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoReviewStatu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PhotoReviewStatus.AddObject(photoReviewStatu);
            }
        }

        public void UpdatePhotoReviewStatu(PhotoReviewStatu currentPhotoReviewStatu)
        {
            this.ObjectContext.PhotoReviewStatus.AttachAsModified(currentPhotoReviewStatu, this.ChangeSet.GetOriginal(currentPhotoReviewStatu));
        }

        public void DeletePhotoReviewStatu(PhotoReviewStatu photoReviewStatu)
        {
            if ((photoReviewStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoReviewStatu, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PhotoReviewStatus.Attach(photoReviewStatu);
                this.ObjectContext.PhotoReviewStatus.DeleteObject(photoReviewStatu);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PhotoStatus' query.
        public IQueryable<PhotoStatu> GetPhotoStatus()
        {
            return this.ObjectContext.PhotoStatus;
        }

        public void InsertPhotoStatu(PhotoStatu photoStatu)
        {
            if ((photoStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoStatu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PhotoStatus.AddObject(photoStatu);
            }
        }

        public void UpdatePhotoStatu(PhotoStatu currentPhotoStatu)
        {
            this.ObjectContext.PhotoStatus.AttachAsModified(currentPhotoStatu, this.ChangeSet.GetOriginal(currentPhotoStatu));
        }

        public void DeletePhotoStatu(PhotoStatu photoStatu)
        {
            if ((photoStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoStatu, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PhotoStatus.Attach(photoStatu);
                this.ObjectContext.PhotoStatus.DeleteObject(photoStatu);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PhotoTypes' query.
        public IQueryable<PhotoType> GetPhotoTypes()
        {
            return this.ObjectContext.PhotoTypes;
        }

        public void InsertPhotoType(PhotoType photoType)
        {
            if ((photoType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PhotoTypes.AddObject(photoType);
            }
        }

        public void UpdatePhotoType(PhotoType currentPhotoType)
        {
            this.ObjectContext.PhotoTypes.AttachAsModified(currentPhotoType, this.ChangeSet.GetOriginal(currentPhotoType));
        }

        public void DeletePhotoType(PhotoType photoType)
        {
            if ((photoType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(photoType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PhotoTypes.Attach(photoType);
                this.ObjectContext.PhotoTypes.DeleteObject(photoType);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'profiles' query.
        public IQueryable<profile> GetProfiles()
        {
            return this.ObjectContext.profiles;
        }

        public void InsertProfile(profile profile)
        {
            if ((profile.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profile, EntityState.Added);
            }
            else
            {
                this.ObjectContext.profiles.AddObject(profile);
            }
        }

        public void UpdateProfile(profile currentprofile)
        {
            this.ObjectContext.profiles.AttachAsModified(currentprofile, this.ChangeSet.GetOriginal(currentprofile));
        }

        public void DeleteProfile(profile profile)
        {
            if ((profile.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profile, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.profiles.Attach(profile);
                this.ObjectContext.profiles.DeleteObject(profile);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileDatas' query.
        public IQueryable<ProfileData> GetProfileDatas()
        {
            return this.ObjectContext.ProfileDatas;
        }

        public void InsertProfileData(ProfileData profileData)
        {
            if ((profileData.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileDatas.AddObject(profileData);
            }
        }

        public void UpdateProfileData(ProfileData currentProfileData)
        {
            this.ObjectContext.ProfileDatas.AttachAsModified(currentProfileData, this.ChangeSet.GetOriginal(currentProfileData));
        }

        public void DeleteProfileData(ProfileData profileData)
        {
            if ((profileData.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileDatas.Attach(profileData);
                this.ObjectContext.ProfileDatas.DeleteObject(profileData);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileData_Ethnicity' query.
        public IQueryable<ProfileData_Ethnicity> GetProfileData_Ethnicity()
        {
            return this.ObjectContext.ProfileData_Ethnicity;
        }

        public void InsertProfileData_Ethnicity(ProfileData_Ethnicity profileData_Ethnicity)
        {
            if ((profileData_Ethnicity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_Ethnicity, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileData_Ethnicity.AddObject(profileData_Ethnicity);
            }
        }

        public void UpdateProfileData_Ethnicity(ProfileData_Ethnicity currentProfileData_Ethnicity)
        {
            this.ObjectContext.ProfileData_Ethnicity.AttachAsModified(currentProfileData_Ethnicity, this.ChangeSet.GetOriginal(currentProfileData_Ethnicity));
        }

        public void DeleteProfileData_Ethnicity(ProfileData_Ethnicity profileData_Ethnicity)
        {
            if ((profileData_Ethnicity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_Ethnicity, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileData_Ethnicity.Attach(profileData_Ethnicity);
                this.ObjectContext.ProfileData_Ethnicity.DeleteObject(profileData_Ethnicity);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileData_Hobby' query.
        public IQueryable<ProfileData_Hobby> GetProfileData_Hobby()
        {
            return this.ObjectContext.ProfileData_Hobby;
        }

        public void InsertProfileData_Hobby(ProfileData_Hobby profileData_Hobby)
        {
            if ((profileData_Hobby.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_Hobby, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileData_Hobby.AddObject(profileData_Hobby);
            }
        }

        public void UpdateProfileData_Hobby(ProfileData_Hobby currentProfileData_Hobby)
        {
            this.ObjectContext.ProfileData_Hobby.AttachAsModified(currentProfileData_Hobby, this.ChangeSet.GetOriginal(currentProfileData_Hobby));
        }

        public void DeleteProfileData_Hobby(ProfileData_Hobby profileData_Hobby)
        {
            if ((profileData_Hobby.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_Hobby, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileData_Hobby.Attach(profileData_Hobby);
                this.ObjectContext.ProfileData_Hobby.DeleteObject(profileData_Hobby);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileData_HotFeature' query.
        public IQueryable<ProfileData_HotFeature> GetProfileData_HotFeature()
        {
            return this.ObjectContext.ProfileData_HotFeature;
        }

        public void InsertProfileData_HotFeature(ProfileData_HotFeature profileData_HotFeature)
        {
            if ((profileData_HotFeature.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_HotFeature, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileData_HotFeature.AddObject(profileData_HotFeature);
            }
        }

        public void UpdateProfileData_HotFeature(ProfileData_HotFeature currentProfileData_HotFeature)
        {
            this.ObjectContext.ProfileData_HotFeature.AttachAsModified(currentProfileData_HotFeature, this.ChangeSet.GetOriginal(currentProfileData_HotFeature));
        }

        public void DeleteProfileData_HotFeature(ProfileData_HotFeature profileData_HotFeature)
        {
            if ((profileData_HotFeature.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_HotFeature, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileData_HotFeature.Attach(profileData_HotFeature);
                this.ObjectContext.ProfileData_HotFeature.DeleteObject(profileData_HotFeature);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileData_LookingFor' query.
        public IQueryable<ProfileData_LookingFor> GetProfileData_LookingFor()
        {
            return this.ObjectContext.ProfileData_LookingFor;
        }

        public void InsertProfileData_LookingFor(ProfileData_LookingFor profileData_LookingFor)
        {
            if ((profileData_LookingFor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_LookingFor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileData_LookingFor.AddObject(profileData_LookingFor);
            }
        }

        public void UpdateProfileData_LookingFor(ProfileData_LookingFor currentProfileData_LookingFor)
        {
            this.ObjectContext.ProfileData_LookingFor.AttachAsModified(currentProfileData_LookingFor, this.ChangeSet.GetOriginal(currentProfileData_LookingFor));
        }

        public void DeleteProfileData_LookingFor(ProfileData_LookingFor profileData_LookingFor)
        {
            if ((profileData_LookingFor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileData_LookingFor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileData_LookingFor.Attach(profileData_LookingFor);
                this.ObjectContext.ProfileData_LookingFor.DeleteObject(profileData_LookingFor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileEmailUpdateFreqencies' query.
        public IQueryable<ProfileEmailUpdateFreqency> GetProfileEmailUpdateFreqencies()
        {
            return this.ObjectContext.ProfileEmailUpdateFreqencies;
        }

        public void InsertProfileEmailUpdateFreqency(ProfileEmailUpdateFreqency profileEmailUpdateFreqency)
        {
            if ((profileEmailUpdateFreqency.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileEmailUpdateFreqency, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileEmailUpdateFreqencies.AddObject(profileEmailUpdateFreqency);
            }
        }

        public void UpdateProfileEmailUpdateFreqency(ProfileEmailUpdateFreqency currentProfileEmailUpdateFreqency)
        {
            this.ObjectContext.ProfileEmailUpdateFreqencies.AttachAsModified(currentProfileEmailUpdateFreqency, this.ChangeSet.GetOriginal(currentProfileEmailUpdateFreqency));
        }

        public void DeleteProfileEmailUpdateFreqency(ProfileEmailUpdateFreqency profileEmailUpdateFreqency)
        {
            if ((profileEmailUpdateFreqency.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileEmailUpdateFreqency, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileEmailUpdateFreqencies.Attach(profileEmailUpdateFreqency);
                this.ObjectContext.ProfileEmailUpdateFreqencies.DeleteObject(profileEmailUpdateFreqency);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileGeoDataLoggers' query.
        public IQueryable<ProfileGeoDataLogger> GetProfileGeoDataLoggers()
        {
            return this.ObjectContext.ProfileGeoDataLoggers;
        }

        public void InsertProfileGeoDataLogger(ProfileGeoDataLogger profileGeoDataLogger)
        {
            if ((profileGeoDataLogger.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileGeoDataLogger, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileGeoDataLoggers.AddObject(profileGeoDataLogger);
            }
        }

        public void UpdateProfileGeoDataLogger(ProfileGeoDataLogger currentProfileGeoDataLogger)
        {
            this.ObjectContext.ProfileGeoDataLoggers.AttachAsModified(currentProfileGeoDataLogger, this.ChangeSet.GetOriginal(currentProfileGeoDataLogger));
        }

        public void DeleteProfileGeoDataLogger(ProfileGeoDataLogger profileGeoDataLogger)
        {
            if ((profileGeoDataLogger.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileGeoDataLogger, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileGeoDataLoggers.Attach(profileGeoDataLogger);
                this.ObjectContext.ProfileGeoDataLoggers.DeleteObject(profileGeoDataLogger);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'profileOpenIDStores' query.
        public IQueryable<profileOpenIDStore> GetProfileOpenIDStores()
        {
            return this.ObjectContext.profileOpenIDStores;
        }

        public void InsertProfileOpenIDStore(profileOpenIDStore profileOpenIDStore)
        {
            if ((profileOpenIDStore.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileOpenIDStore, EntityState.Added);
            }
            else
            {
                this.ObjectContext.profileOpenIDStores.AddObject(profileOpenIDStore);
            }
        }

        public void UpdateProfileOpenIDStore(profileOpenIDStore currentprofileOpenIDStore)
        {
            this.ObjectContext.profileOpenIDStores.AttachAsModified(currentprofileOpenIDStore, this.ChangeSet.GetOriginal(currentprofileOpenIDStore));
        }

        public void DeleteProfileOpenIDStore(profileOpenIDStore profileOpenIDStore)
        {
            if ((profileOpenIDStore.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileOpenIDStore, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.profileOpenIDStores.Attach(profileOpenIDStore);
                this.ObjectContext.profileOpenIDStores.DeleteObject(profileOpenIDStore);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileRatings' query.
        public IQueryable<ProfileRating> GetProfileRatings()
        {
            return this.ObjectContext.ProfileRatings;
        }

        public void InsertProfileRating(ProfileRating profileRating)
        {
            if ((profileRating.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileRating, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileRatings.AddObject(profileRating);
            }
        }

        public void UpdateProfileRating(ProfileRating currentProfileRating)
        {
            this.ObjectContext.ProfileRatings.AttachAsModified(currentProfileRating, this.ChangeSet.GetOriginal(currentProfileRating));
        }

        public void DeleteProfileRating(ProfileRating profileRating)
        {
            if ((profileRating.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileRating, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileRatings.Attach(profileRating);
                this.ObjectContext.ProfileRatings.DeleteObject(profileRating);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileRatingTrackers' query.
        public IQueryable<ProfileRatingTracker> GetProfileRatingTrackers()
        {
            return this.ObjectContext.ProfileRatingTrackers;
        }

        public void InsertProfileRatingTracker(ProfileRatingTracker profileRatingTracker)
        {
            if ((profileRatingTracker.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileRatingTracker, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileRatingTrackers.AddObject(profileRatingTracker);
            }
        }

        public void UpdateProfileRatingTracker(ProfileRatingTracker currentProfileRatingTracker)
        {
            this.ObjectContext.ProfileRatingTrackers.AttachAsModified(currentProfileRatingTracker, this.ChangeSet.GetOriginal(currentProfileRatingTracker));
        }

        public void DeleteProfileRatingTracker(ProfileRatingTracker profileRatingTracker)
        {
            if ((profileRatingTracker.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileRatingTracker, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileRatingTrackers.Attach(profileRatingTracker);
                this.ObjectContext.ProfileRatingTrackers.DeleteObject(profileRatingTracker);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'profilestatuses' query.
        public IQueryable<profilestatus> GetProfilestatuses()
        {
            return this.ObjectContext.profilestatuses;
        }

        public void InsertProfilestatus(profilestatus profilestatus)
        {
            if ((profilestatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profilestatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.profilestatuses.AddObject(profilestatus);
            }
        }

        public void UpdateProfilestatus(profilestatus currentprofilestatus)
        {
            this.ObjectContext.profilestatuses.AttachAsModified(currentprofilestatus, this.ChangeSet.GetOriginal(currentprofilestatus));
        }

        public void DeleteProfilestatus(profilestatus profilestatus)
        {
            if ((profilestatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profilestatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.profilestatuses.Attach(profilestatus);
                this.ObjectContext.profilestatuses.DeleteObject(profilestatus);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileViews' query.
        public IQueryable<ProfileView> GetProfileViews()
        {
            return this.ObjectContext.ProfileViews;
        }

        public void InsertProfileView(ProfileView profileView)
        {
            if ((profileView.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileView, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileViews.AddObject(profileView);
            }
        }

        public void UpdateProfileView(ProfileView currentProfileView)
        {
            this.ObjectContext.ProfileViews.AttachAsModified(currentProfileView, this.ChangeSet.GetOriginal(currentProfileView));
        }

        public void DeleteProfileView(ProfileView profileView)
        {
            if ((profileView.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileView, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileViews.Attach(profileView);
                this.ObjectContext.ProfileViews.DeleteObject(profileView);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProfileVisiblitySettings' query.
        public IQueryable<ProfileVisiblitySetting> GetProfileVisiblitySettings()
        {
            return this.ObjectContext.ProfileVisiblitySettings;
        }

        public void InsertProfileVisiblitySetting(ProfileVisiblitySetting profileVisiblitySetting)
        {
            if ((profileVisiblitySetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileVisiblitySetting, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProfileVisiblitySettings.AddObject(profileVisiblitySetting);
            }
        }

        public void UpdateProfileVisiblitySetting(ProfileVisiblitySetting currentProfileVisiblitySetting)
        {
            this.ObjectContext.ProfileVisiblitySettings.AttachAsModified(currentProfileVisiblitySetting, this.ChangeSet.GetOriginal(currentProfileVisiblitySetting));
        }

        public void DeleteProfileVisiblitySetting(ProfileVisiblitySetting profileVisiblitySetting)
        {
            if ((profileVisiblitySetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profileVisiblitySetting, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProfileVisiblitySettings.Attach(profileVisiblitySetting);
                this.ObjectContext.ProfileVisiblitySettings.DeleteObject(profileVisiblitySetting);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ratings' query.
        public IQueryable<Rating> GetRatings()
        {
            return this.ObjectContext.Ratings;
        }

        public void InsertRating(Rating rating)
        {
            if ((rating.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rating, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Ratings.AddObject(rating);
            }
        }

        public void UpdateRating(Rating currentRating)
        {
            this.ObjectContext.Ratings.AttachAsModified(currentRating, this.ChangeSet.GetOriginal(currentRating));
        }

        public void DeleteRating(Rating rating)
        {
            if ((rating.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rating, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Ratings.Attach(rating);
                this.ObjectContext.Ratings.DeleteObject(rating);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Roles' query.
        public IQueryable<Role> GetRoles()
        {
            return this.ObjectContext.Roles;
        }

        public void InsertRole(Role role)
        {
            if ((role.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(role, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Roles.AddObject(role);
            }
        }

        public void UpdateRole(Role currentRole)
        {
            this.ObjectContext.Roles.AttachAsModified(currentRole, this.ChangeSet.GetOriginal(currentRole));
        }

        public void DeleteRole(Role role)
        {
            if ((role.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(role, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Roles.Attach(role);
                this.ObjectContext.Roles.DeleteObject(role);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings' query.
        public IQueryable<SearchSetting> GetSearchSettings()
        {
            return this.ObjectContext.SearchSettings;
        }

        public void InsertSearchSetting(SearchSetting searchSetting)
        {
            if ((searchSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSetting, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings.AddObject(searchSetting);
            }
        }

        public void UpdateSearchSetting(SearchSetting currentSearchSetting)
        {
            this.ObjectContext.SearchSettings.AttachAsModified(currentSearchSetting, this.ChangeSet.GetOriginal(currentSearchSetting));
        }

        public void DeleteSearchSetting(SearchSetting searchSetting)
        {
            if ((searchSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSetting, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings.Attach(searchSetting);
                this.ObjectContext.SearchSettings.DeleteObject(searchSetting);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_BodyTypes' query.
        public IQueryable<SearchSettings_BodyTypes> GetSearchSettings_BodyTypes()
        {
            return this.ObjectContext.SearchSettings_BodyTypes;
        }

        public void InsertSearchSettings_BodyTypes(SearchSettings_BodyTypes searchSettings_BodyTypes)
        {
            if ((searchSettings_BodyTypes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_BodyTypes, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_BodyTypes.AddObject(searchSettings_BodyTypes);
            }
        }

        public void UpdateSearchSettings_BodyTypes(SearchSettings_BodyTypes currentSearchSettings_BodyTypes)
        {
            this.ObjectContext.SearchSettings_BodyTypes.AttachAsModified(currentSearchSettings_BodyTypes, this.ChangeSet.GetOriginal(currentSearchSettings_BodyTypes));
        }

        public void DeleteSearchSettings_BodyTypes(SearchSettings_BodyTypes searchSettings_BodyTypes)
        {
            if ((searchSettings_BodyTypes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_BodyTypes, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_BodyTypes.Attach(searchSettings_BodyTypes);
                this.ObjectContext.SearchSettings_BodyTypes.DeleteObject(searchSettings_BodyTypes);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Diet' query.
        public IQueryable<SearchSettings_Diet> GetSearchSettings_Diet()
        {
            return this.ObjectContext.SearchSettings_Diet;
        }

        public void InsertSearchSettings_Diet(SearchSettings_Diet searchSettings_Diet)
        {
            if ((searchSettings_Diet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Diet, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Diet.AddObject(searchSettings_Diet);
            }
        }

        public void UpdateSearchSettings_Diet(SearchSettings_Diet currentSearchSettings_Diet)
        {
            this.ObjectContext.SearchSettings_Diet.AttachAsModified(currentSearchSettings_Diet, this.ChangeSet.GetOriginal(currentSearchSettings_Diet));
        }

        public void DeleteSearchSettings_Diet(SearchSettings_Diet searchSettings_Diet)
        {
            if ((searchSettings_Diet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Diet, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Diet.Attach(searchSettings_Diet);
                this.ObjectContext.SearchSettings_Diet.DeleteObject(searchSettings_Diet);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Drinks' query.
        public IQueryable<SearchSettings_Drinks> GetSearchSettings_Drinks()
        {
            return this.ObjectContext.SearchSettings_Drinks;
        }

        public void InsertSearchSettings_Drinks(SearchSettings_Drinks searchSettings_Drinks)
        {
            if ((searchSettings_Drinks.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Drinks, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Drinks.AddObject(searchSettings_Drinks);
            }
        }

        public void UpdateSearchSettings_Drinks(SearchSettings_Drinks currentSearchSettings_Drinks)
        {
            this.ObjectContext.SearchSettings_Drinks.AttachAsModified(currentSearchSettings_Drinks, this.ChangeSet.GetOriginal(currentSearchSettings_Drinks));
        }

        public void DeleteSearchSettings_Drinks(SearchSettings_Drinks searchSettings_Drinks)
        {
            if ((searchSettings_Drinks.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Drinks, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Drinks.Attach(searchSettings_Drinks);
                this.ObjectContext.SearchSettings_Drinks.DeleteObject(searchSettings_Drinks);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_EducationLevel' query.
        public IQueryable<SearchSettings_EducationLevel> GetSearchSettings_EducationLevel()
        {
            return this.ObjectContext.SearchSettings_EducationLevel;
        }

        public void InsertSearchSettings_EducationLevel(SearchSettings_EducationLevel searchSettings_EducationLevel)
        {
            if ((searchSettings_EducationLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_EducationLevel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_EducationLevel.AddObject(searchSettings_EducationLevel);
            }
        }

        public void UpdateSearchSettings_EducationLevel(SearchSettings_EducationLevel currentSearchSettings_EducationLevel)
        {
            this.ObjectContext.SearchSettings_EducationLevel.AttachAsModified(currentSearchSettings_EducationLevel, this.ChangeSet.GetOriginal(currentSearchSettings_EducationLevel));
        }

        public void DeleteSearchSettings_EducationLevel(SearchSettings_EducationLevel searchSettings_EducationLevel)
        {
            if ((searchSettings_EducationLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_EducationLevel, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_EducationLevel.Attach(searchSettings_EducationLevel);
                this.ObjectContext.SearchSettings_EducationLevel.DeleteObject(searchSettings_EducationLevel);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_EmploymentStatus' query.
        public IQueryable<SearchSettings_EmploymentStatus> GetSearchSettings_EmploymentStatus()
        {
            return this.ObjectContext.SearchSettings_EmploymentStatus;
        }

        public void InsertSearchSettings_EmploymentStatus(SearchSettings_EmploymentStatus searchSettings_EmploymentStatus)
        {
            if ((searchSettings_EmploymentStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_EmploymentStatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_EmploymentStatus.AddObject(searchSettings_EmploymentStatus);
            }
        }

        public void UpdateSearchSettings_EmploymentStatus(SearchSettings_EmploymentStatus currentSearchSettings_EmploymentStatus)
        {
            this.ObjectContext.SearchSettings_EmploymentStatus.AttachAsModified(currentSearchSettings_EmploymentStatus, this.ChangeSet.GetOriginal(currentSearchSettings_EmploymentStatus));
        }

        public void DeleteSearchSettings_EmploymentStatus(SearchSettings_EmploymentStatus searchSettings_EmploymentStatus)
        {
            if ((searchSettings_EmploymentStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_EmploymentStatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_EmploymentStatus.Attach(searchSettings_EmploymentStatus);
                this.ObjectContext.SearchSettings_EmploymentStatus.DeleteObject(searchSettings_EmploymentStatus);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Ethnicity' query.
        public IQueryable<SearchSettings_Ethnicity> GetSearchSettings_Ethnicity()
        {
            return this.ObjectContext.SearchSettings_Ethnicity;
        }

        public void InsertSearchSettings_Ethnicity(SearchSettings_Ethnicity searchSettings_Ethnicity)
        {
            if ((searchSettings_Ethnicity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Ethnicity, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Ethnicity.AddObject(searchSettings_Ethnicity);
            }
        }

        public void UpdateSearchSettings_Ethnicity(SearchSettings_Ethnicity currentSearchSettings_Ethnicity)
        {
            this.ObjectContext.SearchSettings_Ethnicity.AttachAsModified(currentSearchSettings_Ethnicity, this.ChangeSet.GetOriginal(currentSearchSettings_Ethnicity));
        }

        public void DeleteSearchSettings_Ethnicity(SearchSettings_Ethnicity searchSettings_Ethnicity)
        {
            if ((searchSettings_Ethnicity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Ethnicity, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Ethnicity.Attach(searchSettings_Ethnicity);
                this.ObjectContext.SearchSettings_Ethnicity.DeleteObject(searchSettings_Ethnicity);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Exercise' query.
        public IQueryable<SearchSettings_Exercise> GetSearchSettings_Exercise()
        {
            return this.ObjectContext.SearchSettings_Exercise;
        }

        public void InsertSearchSettings_Exercise(SearchSettings_Exercise searchSettings_Exercise)
        {
            if ((searchSettings_Exercise.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Exercise, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Exercise.AddObject(searchSettings_Exercise);
            }
        }

        public void UpdateSearchSettings_Exercise(SearchSettings_Exercise currentSearchSettings_Exercise)
        {
            this.ObjectContext.SearchSettings_Exercise.AttachAsModified(currentSearchSettings_Exercise, this.ChangeSet.GetOriginal(currentSearchSettings_Exercise));
        }

        public void DeleteSearchSettings_Exercise(SearchSettings_Exercise searchSettings_Exercise)
        {
            if ((searchSettings_Exercise.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Exercise, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Exercise.Attach(searchSettings_Exercise);
                this.ObjectContext.SearchSettings_Exercise.DeleteObject(searchSettings_Exercise);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_EyeColor' query.
        public IQueryable<SearchSettings_EyeColor> GetSearchSettings_EyeColor()
        {
            return this.ObjectContext.SearchSettings_EyeColor;
        }

        public void InsertSearchSettings_EyeColor(SearchSettings_EyeColor searchSettings_EyeColor)
        {
            if ((searchSettings_EyeColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_EyeColor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_EyeColor.AddObject(searchSettings_EyeColor);
            }
        }

        public void UpdateSearchSettings_EyeColor(SearchSettings_EyeColor currentSearchSettings_EyeColor)
        {
            this.ObjectContext.SearchSettings_EyeColor.AttachAsModified(currentSearchSettings_EyeColor, this.ChangeSet.GetOriginal(currentSearchSettings_EyeColor));
        }

        public void DeleteSearchSettings_EyeColor(SearchSettings_EyeColor searchSettings_EyeColor)
        {
            if ((searchSettings_EyeColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_EyeColor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_EyeColor.Attach(searchSettings_EyeColor);
                this.ObjectContext.SearchSettings_EyeColor.DeleteObject(searchSettings_EyeColor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Genders' query.
        public IQueryable<SearchSettings_Genders> GetSearchSettings_Genders()
        {
            return this.ObjectContext.SearchSettings_Genders;
        }

        public void InsertSearchSettings_Genders(SearchSettings_Genders searchSettings_Genders)
        {
            if ((searchSettings_Genders.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Genders, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Genders.AddObject(searchSettings_Genders);
            }
        }

        public void UpdateSearchSettings_Genders(SearchSettings_Genders currentSearchSettings_Genders)
        {
            this.ObjectContext.SearchSettings_Genders.AttachAsModified(currentSearchSettings_Genders, this.ChangeSet.GetOriginal(currentSearchSettings_Genders));
        }

        public void DeleteSearchSettings_Genders(SearchSettings_Genders searchSettings_Genders)
        {
            if ((searchSettings_Genders.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Genders, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Genders.Attach(searchSettings_Genders);
                this.ObjectContext.SearchSettings_Genders.DeleteObject(searchSettings_Genders);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_HairColor' query.
        public IQueryable<SearchSettings_HairColor> GetSearchSettings_HairColor()
        {
            return this.ObjectContext.SearchSettings_HairColor;
        }

        public void InsertSearchSettings_HairColor(SearchSettings_HairColor searchSettings_HairColor)
        {
            if ((searchSettings_HairColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_HairColor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_HairColor.AddObject(searchSettings_HairColor);
            }
        }

        public void UpdateSearchSettings_HairColor(SearchSettings_HairColor currentSearchSettings_HairColor)
        {
            this.ObjectContext.SearchSettings_HairColor.AttachAsModified(currentSearchSettings_HairColor, this.ChangeSet.GetOriginal(currentSearchSettings_HairColor));
        }

        public void DeleteSearchSettings_HairColor(SearchSettings_HairColor searchSettings_HairColor)
        {
            if ((searchSettings_HairColor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_HairColor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_HairColor.Attach(searchSettings_HairColor);
                this.ObjectContext.SearchSettings_HairColor.DeleteObject(searchSettings_HairColor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_HaveKids' query.
        public IQueryable<SearchSettings_HaveKids> GetSearchSettings_HaveKids()
        {
            return this.ObjectContext.SearchSettings_HaveKids;
        }

        public void InsertSearchSettings_HaveKids(SearchSettings_HaveKids searchSettings_HaveKids)
        {
            if ((searchSettings_HaveKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_HaveKids, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_HaveKids.AddObject(searchSettings_HaveKids);
            }
        }

        public void UpdateSearchSettings_HaveKids(SearchSettings_HaveKids currentSearchSettings_HaveKids)
        {
            this.ObjectContext.SearchSettings_HaveKids.AttachAsModified(currentSearchSettings_HaveKids, this.ChangeSet.GetOriginal(currentSearchSettings_HaveKids));
        }

        public void DeleteSearchSettings_HaveKids(SearchSettings_HaveKids searchSettings_HaveKids)
        {
            if ((searchSettings_HaveKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_HaveKids, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_HaveKids.Attach(searchSettings_HaveKids);
                this.ObjectContext.SearchSettings_HaveKids.DeleteObject(searchSettings_HaveKids);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Hobby' query.
        public IQueryable<SearchSettings_Hobby> GetSearchSettings_Hobby()
        {
            return this.ObjectContext.SearchSettings_Hobby;
        }

        public void InsertSearchSettings_Hobby(SearchSettings_Hobby searchSettings_Hobby)
        {
            if ((searchSettings_Hobby.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Hobby, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Hobby.AddObject(searchSettings_Hobby);
            }
        }

        public void UpdateSearchSettings_Hobby(SearchSettings_Hobby currentSearchSettings_Hobby)
        {
            this.ObjectContext.SearchSettings_Hobby.AttachAsModified(currentSearchSettings_Hobby, this.ChangeSet.GetOriginal(currentSearchSettings_Hobby));
        }

        public void DeleteSearchSettings_Hobby(SearchSettings_Hobby searchSettings_Hobby)
        {
            if ((searchSettings_Hobby.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Hobby, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Hobby.Attach(searchSettings_Hobby);
                this.ObjectContext.SearchSettings_Hobby.DeleteObject(searchSettings_Hobby);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_HotFeature' query.
        public IQueryable<SearchSettings_HotFeature> GetSearchSettings_HotFeature()
        {
            return this.ObjectContext.SearchSettings_HotFeature;
        }

        public void InsertSearchSettings_HotFeature(SearchSettings_HotFeature searchSettings_HotFeature)
        {
            if ((searchSettings_HotFeature.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_HotFeature, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_HotFeature.AddObject(searchSettings_HotFeature);
            }
        }

        public void UpdateSearchSettings_HotFeature(SearchSettings_HotFeature currentSearchSettings_HotFeature)
        {
            this.ObjectContext.SearchSettings_HotFeature.AttachAsModified(currentSearchSettings_HotFeature, this.ChangeSet.GetOriginal(currentSearchSettings_HotFeature));
        }

        public void DeleteSearchSettings_HotFeature(SearchSettings_HotFeature searchSettings_HotFeature)
        {
            if ((searchSettings_HotFeature.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_HotFeature, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_HotFeature.Attach(searchSettings_HotFeature);
                this.ObjectContext.SearchSettings_HotFeature.DeleteObject(searchSettings_HotFeature);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Humor' query.
        public IQueryable<SearchSettings_Humor> GetSearchSettings_Humor()
        {
            return this.ObjectContext.SearchSettings_Humor;
        }

        public void InsertSearchSettings_Humor(SearchSettings_Humor searchSettings_Humor)
        {
            if ((searchSettings_Humor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Humor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Humor.AddObject(searchSettings_Humor);
            }
        }

        public void UpdateSearchSettings_Humor(SearchSettings_Humor currentSearchSettings_Humor)
        {
            this.ObjectContext.SearchSettings_Humor.AttachAsModified(currentSearchSettings_Humor, this.ChangeSet.GetOriginal(currentSearchSettings_Humor));
        }

        public void DeleteSearchSettings_Humor(SearchSettings_Humor searchSettings_Humor)
        {
            if ((searchSettings_Humor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Humor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Humor.Attach(searchSettings_Humor);
                this.ObjectContext.SearchSettings_Humor.DeleteObject(searchSettings_Humor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_IncomeLevel' query.
        public IQueryable<SearchSettings_IncomeLevel> GetSearchSettings_IncomeLevel()
        {
            return this.ObjectContext.SearchSettings_IncomeLevel;
        }

        public void InsertSearchSettings_IncomeLevel(SearchSettings_IncomeLevel searchSettings_IncomeLevel)
        {
            if ((searchSettings_IncomeLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_IncomeLevel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_IncomeLevel.AddObject(searchSettings_IncomeLevel);
            }
        }

        public void UpdateSearchSettings_IncomeLevel(SearchSettings_IncomeLevel currentSearchSettings_IncomeLevel)
        {
            this.ObjectContext.SearchSettings_IncomeLevel.AttachAsModified(currentSearchSettings_IncomeLevel, this.ChangeSet.GetOriginal(currentSearchSettings_IncomeLevel));
        }

        public void DeleteSearchSettings_IncomeLevel(SearchSettings_IncomeLevel searchSettings_IncomeLevel)
        {
            if ((searchSettings_IncomeLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_IncomeLevel, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_IncomeLevel.Attach(searchSettings_IncomeLevel);
                this.ObjectContext.SearchSettings_IncomeLevel.DeleteObject(searchSettings_IncomeLevel);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_LivingStituation' query.
        public IQueryable<SearchSettings_LivingStituation> GetSearchSettings_LivingStituation()
        {
            return this.ObjectContext.SearchSettings_LivingStituation;
        }

        public void InsertSearchSettings_LivingStituation(SearchSettings_LivingStituation searchSettings_LivingStituation)
        {
            if ((searchSettings_LivingStituation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_LivingStituation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_LivingStituation.AddObject(searchSettings_LivingStituation);
            }
        }

        public void UpdateSearchSettings_LivingStituation(SearchSettings_LivingStituation currentSearchSettings_LivingStituation)
        {
            this.ObjectContext.SearchSettings_LivingStituation.AttachAsModified(currentSearchSettings_LivingStituation, this.ChangeSet.GetOriginal(currentSearchSettings_LivingStituation));
        }

        public void DeleteSearchSettings_LivingStituation(SearchSettings_LivingStituation searchSettings_LivingStituation)
        {
            if ((searchSettings_LivingStituation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_LivingStituation, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_LivingStituation.Attach(searchSettings_LivingStituation);
                this.ObjectContext.SearchSettings_LivingStituation.DeleteObject(searchSettings_LivingStituation);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Location' query.
        public IQueryable<SearchSettings_Location> GetSearchSettings_Location()
        {
            return this.ObjectContext.SearchSettings_Location;
        }

        public void InsertSearchSettings_Location(SearchSettings_Location searchSettings_Location)
        {
            if ((searchSettings_Location.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Location, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Location.AddObject(searchSettings_Location);
            }
        }

        public void UpdateSearchSettings_Location(SearchSettings_Location currentSearchSettings_Location)
        {
            this.ObjectContext.SearchSettings_Location.AttachAsModified(currentSearchSettings_Location, this.ChangeSet.GetOriginal(currentSearchSettings_Location));
        }

        public void DeleteSearchSettings_Location(SearchSettings_Location searchSettings_Location)
        {
            if ((searchSettings_Location.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Location, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Location.Attach(searchSettings_Location);
                this.ObjectContext.SearchSettings_Location.DeleteObject(searchSettings_Location);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_LookingFor' query.
        public IQueryable<SearchSettings_LookingFor> GetSearchSettings_LookingFor()
        {
            return this.ObjectContext.SearchSettings_LookingFor;
        }

        public void InsertSearchSettings_LookingFor(SearchSettings_LookingFor searchSettings_LookingFor)
        {
            if ((searchSettings_LookingFor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_LookingFor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_LookingFor.AddObject(searchSettings_LookingFor);
            }
        }

        public void UpdateSearchSettings_LookingFor(SearchSettings_LookingFor currentSearchSettings_LookingFor)
        {
            this.ObjectContext.SearchSettings_LookingFor.AttachAsModified(currentSearchSettings_LookingFor, this.ChangeSet.GetOriginal(currentSearchSettings_LookingFor));
        }

        public void DeleteSearchSettings_LookingFor(SearchSettings_LookingFor searchSettings_LookingFor)
        {
            if ((searchSettings_LookingFor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_LookingFor, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_LookingFor.Attach(searchSettings_LookingFor);
                this.ObjectContext.SearchSettings_LookingFor.DeleteObject(searchSettings_LookingFor);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_MaritalStatus' query.
        public IQueryable<SearchSettings_MaritalStatus> GetSearchSettings_MaritalStatus()
        {
            return this.ObjectContext.SearchSettings_MaritalStatus;
        }

        public void InsertSearchSettings_MaritalStatus(SearchSettings_MaritalStatus searchSettings_MaritalStatus)
        {
            if ((searchSettings_MaritalStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_MaritalStatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_MaritalStatus.AddObject(searchSettings_MaritalStatus);
            }
        }

        public void UpdateSearchSettings_MaritalStatus(SearchSettings_MaritalStatus currentSearchSettings_MaritalStatus)
        {
            this.ObjectContext.SearchSettings_MaritalStatus.AttachAsModified(currentSearchSettings_MaritalStatus, this.ChangeSet.GetOriginal(currentSearchSettings_MaritalStatus));
        }

        public void DeleteSearchSettings_MaritalStatus(SearchSettings_MaritalStatus searchSettings_MaritalStatus)
        {
            if ((searchSettings_MaritalStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_MaritalStatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_MaritalStatus.Attach(searchSettings_MaritalStatus);
                this.ObjectContext.SearchSettings_MaritalStatus.DeleteObject(searchSettings_MaritalStatus);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_NigerianState' query.
        public IQueryable<SearchSettings_NigerianState> GetSearchSettings_NigerianState()
        {
            return this.ObjectContext.SearchSettings_NigerianState;
        }

        public void InsertSearchSettings_NigerianState(SearchSettings_NigerianState searchSettings_NigerianState)
        {
            if ((searchSettings_NigerianState.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_NigerianState, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_NigerianState.AddObject(searchSettings_NigerianState);
            }
        }

        public void UpdateSearchSettings_NigerianState(SearchSettings_NigerianState currentSearchSettings_NigerianState)
        {
            this.ObjectContext.SearchSettings_NigerianState.AttachAsModified(currentSearchSettings_NigerianState, this.ChangeSet.GetOriginal(currentSearchSettings_NigerianState));
        }

        public void DeleteSearchSettings_NigerianState(SearchSettings_NigerianState searchSettings_NigerianState)
        {
            if ((searchSettings_NigerianState.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_NigerianState, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_NigerianState.Attach(searchSettings_NigerianState);
                this.ObjectContext.SearchSettings_NigerianState.DeleteObject(searchSettings_NigerianState);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_PoliticalView' query.
        public IQueryable<SearchSettings_PoliticalView> GetSearchSettings_PoliticalView()
        {
            return this.ObjectContext.SearchSettings_PoliticalView;
        }

        public void InsertSearchSettings_PoliticalView(SearchSettings_PoliticalView searchSettings_PoliticalView)
        {
            if ((searchSettings_PoliticalView.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_PoliticalView, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_PoliticalView.AddObject(searchSettings_PoliticalView);
            }
        }

        public void UpdateSearchSettings_PoliticalView(SearchSettings_PoliticalView currentSearchSettings_PoliticalView)
        {
            this.ObjectContext.SearchSettings_PoliticalView.AttachAsModified(currentSearchSettings_PoliticalView, this.ChangeSet.GetOriginal(currentSearchSettings_PoliticalView));
        }

        public void DeleteSearchSettings_PoliticalView(SearchSettings_PoliticalView searchSettings_PoliticalView)
        {
            if ((searchSettings_PoliticalView.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_PoliticalView, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_PoliticalView.Attach(searchSettings_PoliticalView);
                this.ObjectContext.SearchSettings_PoliticalView.DeleteObject(searchSettings_PoliticalView);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Profession' query.
        public IQueryable<SearchSettings_Profession> GetSearchSettings_Profession()
        {
            return this.ObjectContext.SearchSettings_Profession;
        }

        public void InsertSearchSettings_Profession(SearchSettings_Profession searchSettings_Profession)
        {
            if ((searchSettings_Profession.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Profession, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Profession.AddObject(searchSettings_Profession);
            }
        }

        public void UpdateSearchSettings_Profession(SearchSettings_Profession currentSearchSettings_Profession)
        {
            this.ObjectContext.SearchSettings_Profession.AttachAsModified(currentSearchSettings_Profession, this.ChangeSet.GetOriginal(currentSearchSettings_Profession));
        }

        public void DeleteSearchSettings_Profession(SearchSettings_Profession searchSettings_Profession)
        {
            if ((searchSettings_Profession.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Profession, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Profession.Attach(searchSettings_Profession);
                this.ObjectContext.SearchSettings_Profession.DeleteObject(searchSettings_Profession);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Religion' query.
        public IQueryable<SearchSettings_Religion> GetSearchSettings_Religion()
        {
            return this.ObjectContext.SearchSettings_Religion;
        }

        public void InsertSearchSettings_Religion(SearchSettings_Religion searchSettings_Religion)
        {
            if ((searchSettings_Religion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Religion, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Religion.AddObject(searchSettings_Religion);
            }
        }

        public void UpdateSearchSettings_Religion(SearchSettings_Religion currentSearchSettings_Religion)
        {
            this.ObjectContext.SearchSettings_Religion.AttachAsModified(currentSearchSettings_Religion, this.ChangeSet.GetOriginal(currentSearchSettings_Religion));
        }

        public void DeleteSearchSettings_Religion(SearchSettings_Religion searchSettings_Religion)
        {
            if ((searchSettings_Religion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Religion, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Religion.Attach(searchSettings_Religion);
                this.ObjectContext.SearchSettings_Religion.DeleteObject(searchSettings_Religion);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_ReligiousAttendance' query.
        public IQueryable<SearchSettings_ReligiousAttendance> GetSearchSettings_ReligiousAttendance()
        {
            return this.ObjectContext.SearchSettings_ReligiousAttendance;
        }

        public void InsertSearchSettings_ReligiousAttendance(SearchSettings_ReligiousAttendance searchSettings_ReligiousAttendance)
        {
            if ((searchSettings_ReligiousAttendance.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_ReligiousAttendance, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_ReligiousAttendance.AddObject(searchSettings_ReligiousAttendance);
            }
        }

        public void UpdateSearchSettings_ReligiousAttendance(SearchSettings_ReligiousAttendance currentSearchSettings_ReligiousAttendance)
        {
            this.ObjectContext.SearchSettings_ReligiousAttendance.AttachAsModified(currentSearchSettings_ReligiousAttendance, this.ChangeSet.GetOriginal(currentSearchSettings_ReligiousAttendance));
        }

        public void DeleteSearchSettings_ReligiousAttendance(SearchSettings_ReligiousAttendance searchSettings_ReligiousAttendance)
        {
            if ((searchSettings_ReligiousAttendance.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_ReligiousAttendance, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_ReligiousAttendance.Attach(searchSettings_ReligiousAttendance);
                this.ObjectContext.SearchSettings_ReligiousAttendance.DeleteObject(searchSettings_ReligiousAttendance);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_ShowMe' query.
        public IQueryable<SearchSettings_ShowMe> GetSearchSettings_ShowMe()
        {
            return this.ObjectContext.SearchSettings_ShowMe;
        }

        public void InsertSearchSettings_ShowMe(SearchSettings_ShowMe searchSettings_ShowMe)
        {
            if ((searchSettings_ShowMe.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_ShowMe, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_ShowMe.AddObject(searchSettings_ShowMe);
            }
        }

        public void UpdateSearchSettings_ShowMe(SearchSettings_ShowMe currentSearchSettings_ShowMe)
        {
            this.ObjectContext.SearchSettings_ShowMe.AttachAsModified(currentSearchSettings_ShowMe, this.ChangeSet.GetOriginal(currentSearchSettings_ShowMe));
        }

        public void DeleteSearchSettings_ShowMe(SearchSettings_ShowMe searchSettings_ShowMe)
        {
            if ((searchSettings_ShowMe.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_ShowMe, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_ShowMe.Attach(searchSettings_ShowMe);
                this.ObjectContext.SearchSettings_ShowMe.DeleteObject(searchSettings_ShowMe);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Sign' query.
        public IQueryable<SearchSettings_Sign> GetSearchSettings_Sign()
        {
            return this.ObjectContext.SearchSettings_Sign;
        }

        public void InsertSearchSettings_Sign(SearchSettings_Sign searchSettings_Sign)
        {
            if ((searchSettings_Sign.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Sign, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Sign.AddObject(searchSettings_Sign);
            }
        }

        public void UpdateSearchSettings_Sign(SearchSettings_Sign currentSearchSettings_Sign)
        {
            this.ObjectContext.SearchSettings_Sign.AttachAsModified(currentSearchSettings_Sign, this.ChangeSet.GetOriginal(currentSearchSettings_Sign));
        }

        public void DeleteSearchSettings_Sign(SearchSettings_Sign searchSettings_Sign)
        {
            if ((searchSettings_Sign.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Sign, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Sign.Attach(searchSettings_Sign);
                this.ObjectContext.SearchSettings_Sign.DeleteObject(searchSettings_Sign);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Smokes' query.
        public IQueryable<SearchSettings_Smokes> GetSearchSettings_Smokes()
        {
            return this.ObjectContext.SearchSettings_Smokes;
        }

        public void InsertSearchSettings_Smokes(SearchSettings_Smokes searchSettings_Smokes)
        {
            if ((searchSettings_Smokes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Smokes, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Smokes.AddObject(searchSettings_Smokes);
            }
        }

        public void UpdateSearchSettings_Smokes(SearchSettings_Smokes currentSearchSettings_Smokes)
        {
            this.ObjectContext.SearchSettings_Smokes.AttachAsModified(currentSearchSettings_Smokes, this.ChangeSet.GetOriginal(currentSearchSettings_Smokes));
        }

        public void DeleteSearchSettings_Smokes(SearchSettings_Smokes searchSettings_Smokes)
        {
            if ((searchSettings_Smokes.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Smokes, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Smokes.Attach(searchSettings_Smokes);
                this.ObjectContext.SearchSettings_Smokes.DeleteObject(searchSettings_Smokes);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_SortByType' query.
        public IQueryable<SearchSettings_SortByType> GetSearchSettings_SortByType()
        {
            return this.ObjectContext.SearchSettings_SortByType;
        }

        public void InsertSearchSettings_SortByType(SearchSettings_SortByType searchSettings_SortByType)
        {
            if ((searchSettings_SortByType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_SortByType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_SortByType.AddObject(searchSettings_SortByType);
            }
        }

        public void UpdateSearchSettings_SortByType(SearchSettings_SortByType currentSearchSettings_SortByType)
        {
            this.ObjectContext.SearchSettings_SortByType.AttachAsModified(currentSearchSettings_SortByType, this.ChangeSet.GetOriginal(currentSearchSettings_SortByType));
        }

        public void DeleteSearchSettings_SortByType(SearchSettings_SortByType searchSettings_SortByType)
        {
            if ((searchSettings_SortByType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_SortByType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_SortByType.Attach(searchSettings_SortByType);
                this.ObjectContext.SearchSettings_SortByType.DeleteObject(searchSettings_SortByType);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_Tribe' query.
        public IQueryable<SearchSettings_Tribe> GetSearchSettings_Tribe()
        {
            return this.ObjectContext.SearchSettings_Tribe;
        }

        public void InsertSearchSettings_Tribe(SearchSettings_Tribe searchSettings_Tribe)
        {
            if ((searchSettings_Tribe.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Tribe, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_Tribe.AddObject(searchSettings_Tribe);
            }
        }

        public void UpdateSearchSettings_Tribe(SearchSettings_Tribe currentSearchSettings_Tribe)
        {
            this.ObjectContext.SearchSettings_Tribe.AttachAsModified(currentSearchSettings_Tribe, this.ChangeSet.GetOriginal(currentSearchSettings_Tribe));
        }

        public void DeleteSearchSettings_Tribe(SearchSettings_Tribe searchSettings_Tribe)
        {
            if ((searchSettings_Tribe.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_Tribe, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_Tribe.Attach(searchSettings_Tribe);
                this.ObjectContext.SearchSettings_Tribe.DeleteObject(searchSettings_Tribe);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SearchSettings_WantKids' query.
        public IQueryable<SearchSettings_WantKids> GetSearchSettings_WantKids()
        {
            return this.ObjectContext.SearchSettings_WantKids;
        }

        public void InsertSearchSettings_WantKids(SearchSettings_WantKids searchSettings_WantKids)
        {
            if ((searchSettings_WantKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_WantKids, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SearchSettings_WantKids.AddObject(searchSettings_WantKids);
            }
        }

        public void UpdateSearchSettings_WantKids(SearchSettings_WantKids currentSearchSettings_WantKids)
        {
            this.ObjectContext.SearchSettings_WantKids.AttachAsModified(currentSearchSettings_WantKids, this.ChangeSet.GetOriginal(currentSearchSettings_WantKids));
        }

        public void DeleteSearchSettings_WantKids(SearchSettings_WantKids searchSettings_WantKids)
        {
            if ((searchSettings_WantKids.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(searchSettings_WantKids, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SearchSettings_WantKids.Attach(searchSettings_WantKids);
                this.ObjectContext.SearchSettings_WantKids.DeleteObject(searchSettings_WantKids);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SecurityQuestions' query.
        public IQueryable<SecurityQuestion> GetSecurityQuestions()
        {
            return this.ObjectContext.SecurityQuestions;
        }

        public void InsertSecurityQuestion(SecurityQuestion securityQuestion)
        {
            if ((securityQuestion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityQuestion, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SecurityQuestions.AddObject(securityQuestion);
            }
        }

        public void UpdateSecurityQuestion(SecurityQuestion currentSecurityQuestion)
        {
            this.ObjectContext.SecurityQuestions.AttachAsModified(currentSecurityQuestion, this.ChangeSet.GetOriginal(currentSecurityQuestion));
        }

        public void DeleteSecurityQuestion(SecurityQuestion securityQuestion)
        {
            if ((securityQuestion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityQuestion, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SecurityQuestions.Attach(securityQuestion);
                this.ObjectContext.SecurityQuestions.DeleteObject(securityQuestion);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ShowMes' query.
        public IQueryable<ShowMe> GetShowMes()
        {
            return this.ObjectContext.ShowMes;
        }

        public void InsertShowMe(ShowMe showMe)
        {
            if ((showMe.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(showMe, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ShowMes.AddObject(showMe);
            }
        }

        public void UpdateShowMe(ShowMe currentShowMe)
        {
            this.ObjectContext.ShowMes.AttachAsModified(currentShowMe, this.ChangeSet.GetOriginal(currentShowMe));
        }

        public void DeleteShowMe(ShowMe showMe)
        {
            if ((showMe.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(showMe, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ShowMes.Attach(showMe);
                this.ObjectContext.ShowMes.DeleteObject(showMe);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SortByTypes' query.
        public IQueryable<SortByType> GetSortByTypes()
        {
            return this.ObjectContext.SortByTypes;
        }

        public void InsertSortByType(SortByType sortByType)
        {
            if ((sortByType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sortByType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SortByTypes.AddObject(sortByType);
            }
        }

        public void UpdateSortByType(SortByType currentSortByType)
        {
            this.ObjectContext.SortByTypes.AttachAsModified(currentSortByType, this.ChangeSet.GetOriginal(currentSortByType));
        }

        public void DeleteSortByType(SortByType sortByType)
        {
            if ((sortByType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sortByType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SortByTypes.Attach(sortByType);
                this.ObjectContext.SortByTypes.DeleteObject(sortByType);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SystemPageSettings' query.
        public IQueryable<SystemPageSetting> GetSystemPageSettings()
        {
            return this.ObjectContext.SystemPageSettings;
        }

        public void InsertSystemPageSetting(SystemPageSetting systemPageSetting)
        {
            if ((systemPageSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemPageSetting, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SystemPageSettings.AddObject(systemPageSetting);
            }
        }

        public void UpdateSystemPageSetting(SystemPageSetting currentSystemPageSetting)
        {
            this.ObjectContext.SystemPageSettings.AttachAsModified(currentSystemPageSetting, this.ChangeSet.GetOriginal(currentSystemPageSetting));
        }

        public void DeleteSystemPageSetting(SystemPageSetting systemPageSetting)
        {
            if ((systemPageSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemPageSetting, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SystemPageSettings.Attach(systemPageSetting);
                this.ObjectContext.SystemPageSettings.DeleteObject(systemPageSetting);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'User_Logtime' query.
        public IQueryable<User_Logtime> GetUser_Logtime()
        {
            return this.ObjectContext.User_Logtime;
        }

        public void InsertUser_Logtime(User_Logtime user_Logtime)
        {
            if ((user_Logtime.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(user_Logtime, EntityState.Added);
            }
            else
            {
                this.ObjectContext.User_Logtime.AddObject(user_Logtime);
            }
        }

        public void UpdateUser_Logtime(User_Logtime currentUser_Logtime)
        {
            this.ObjectContext.User_Logtime.AttachAsModified(currentUser_Logtime, this.ChangeSet.GetOriginal(currentUser_Logtime));
        }

        public void DeleteUser_Logtime(User_Logtime user_Logtime)
        {
            if ((user_Logtime.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(user_Logtime, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.User_Logtime.Attach(user_Logtime);
                this.ObjectContext.User_Logtime.DeleteObject(user_Logtime);
            }
        }
    }
}


