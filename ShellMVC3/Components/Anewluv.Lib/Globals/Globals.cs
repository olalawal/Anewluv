using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nmedia.Infrastructure.Domain.Data.errorlog;

 namespace Anewluv.Lib
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
