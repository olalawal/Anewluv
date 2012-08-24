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

namespace Shell.WCF.Logging
{
  
    public class LogValue
    {
        private string mName;
        private string mType;
        private string mValue;

        public LogValue()
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
