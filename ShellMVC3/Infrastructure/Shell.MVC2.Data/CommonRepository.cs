using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure;
using Shell.MVC2.Interfaces;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.errorlog;
using Anewluv.Lib;
using Anewluv.Domain.Data.ViewModels;




namespace Shell.MVC2.Data
{
    public class CommonRepository :  ICommonRepository
    {
        // private AnewluvContext _db;
        //TO DO move from ria servives
       // private IGeoRepository _georepository;
       // private IMemberRepository _memberrepository;


        public CommonRepository()           
        {
           // _georepository = georepository;
            //_memberrepository = memberrepository;
        }



        public   string getNETJSONdatefromISO(DateValidateModel date)
        {
            try
            {
                return Serialization.datetimetojson(DateTime.Parse(date.IsoDate));
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(logapplicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }
        }
            




      

    }
}
