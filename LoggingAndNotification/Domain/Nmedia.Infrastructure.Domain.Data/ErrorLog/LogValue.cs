using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.Errorlog
{
  
    public class logvalue
    {
        private string mName;
        private string mType;
        private string mValue;

        public logvalue()
        {
        }

        [DataMember]
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        [DataMember]
        public string LogValueType
        {
            get { return mType; }
            set { mType = value; }
        }

        [DataMember]
        public string CurrentValue
        {
            get { return mValue; }
            set { mValue = value; }
        }
    }

}
