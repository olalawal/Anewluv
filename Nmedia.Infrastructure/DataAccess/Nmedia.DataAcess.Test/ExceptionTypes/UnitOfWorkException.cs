using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nmedia.DataAccess.Test 
{
   

        [Serializable]
        public class UnitOfWorkException : Exception
        {
            public override string Message
            {
                get
                {
                    return "The parameter must be EFUnitOfWork";
                }
            }
        }
    
}
