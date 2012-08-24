using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//to do do away with this when we go to code first , we would pull this from entities 
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Data
{
    public class MemberRepositoryBase
    {

        protected AnewluvContext  _datingcontext;
        //protected PostalData2Entities _postaldatacontext;

        public MemberRepositoryBase(AnewluvContext datingcontext)
        {
             _datingcontext = datingcontext;
            // _postaldatacontext = postaldatacontext;
        }

    }
}
