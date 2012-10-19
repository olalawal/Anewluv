using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

using Shell.MVC2.Interfaces;
using Shell.MVC2.Services.Contracts;

namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersMapperService" in both code and config file together.
    public class MembersMapperService : IMembersMapperService
    {
        private IMembersMapperRepository _mapmembermapperrepo;
        private string _apikey;


        public MembersMapperService(IMembersMapperRepository mapmembermapperrepo)
            {
                _mapmembermapperrepo = mapmembermapperrepo;
            }



    }
}
