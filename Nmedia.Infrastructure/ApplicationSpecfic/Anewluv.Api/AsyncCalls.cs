using GeoData.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Api
{
    public static class AsyncCalls
    {


        public static async Task<List<countrypostalcode>> getcountryandpostalcodestatuslist()
        {

            Task<List<countrypostalcode>> returnedTaskTResult = Api.GeoService.getcountryandpostalcodestatuslist();
            List<countrypostalcode> result = await returnedTaskTResult;

            return result;

            // IsApiKeyValid = await 


        }

    }
}
