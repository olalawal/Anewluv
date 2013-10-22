using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Anewluv.DataAccess.Interfaces;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Anewluv.DataExtentionMethods
{
    public static class mediaextentionmethods
    {

        public static List<photo> getallphotosbyusername(this IRepository<photo> repo, ProfileModel model)
        {
            return repo.Find().OfType<photo>().Where(o => o.profilemetadata.profile.username == model.username 
                                    && o.photostatus.id != 4 && o.photostatus.id != 5).ToList();

        }


    }
}
