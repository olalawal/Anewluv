using Nmedia.Infrastructure.Domain.Data.errorlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nmedia.Infrastructure.Domain.Data
{
    public class globals
    {

        public static logenviromentEnum getenviroment
        {
            get
            {
                //logenviromentEnum currentenviroment = logenviromentEnum.dev;
                return logenviromentEnum.dev;
            }
        }




    }
}
