using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//to do do away with this when we go to code first , we would pull this from entities 
using Shell.MVC2.Domain.Entities.Anewluv;
using Dating.Server.Data.Models;

namespace Shell.MVC2.Data.Infrastructure
{
    public class GeoRepositoryBase
    {

        protected  PostalData2Entities  _postalcontext;
        //protected PostalData2Entities _postaldatacontext;

        public GeoRepositoryBase(PostalData2Entities postalcontext)
        {
            _postalcontext = postalcontext;
            // _postaldatacontext = postaldatacontext;
        }

    }
}
