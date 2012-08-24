using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomLoggingModel
{
 

    public abstract class EnumWrapper<TEnum> where TEnum : struct, IConvertible 
    { 
        public EnumWrapper() 
        { 
            if (!typeof(TEnum).IsEnum) 
            throw new ArgumentException("Not an enum");
        } 
        public TEnum Enum { get; set; }
        public int Value { get { return Convert.ToInt32(Enum);
        }
            set { Enum = (TEnum)(object)value; }          
        } 
        
        public static implicit operator int(EnumWrapper<TEnum> w) 
        {
            if (w == null)  
                return Convert.ToInt32(default(TEnum));
            else
                return w.Value;
        } 
    } 
}
