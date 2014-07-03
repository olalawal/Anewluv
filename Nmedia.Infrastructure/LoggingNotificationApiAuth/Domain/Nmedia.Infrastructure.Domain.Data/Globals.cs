using  Nmedia.Infrastructure.Domain.Data.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nmedia.Infrastructure.Domain.Data
{
    public class globals
    {

        public static enviromentEnum getenviroment
        {
            get
            {
                //enviromentEnum currentenviroment = enviromentEnum.dev;
                return enviromentEnum.dev;
            }
        }




    }
}
