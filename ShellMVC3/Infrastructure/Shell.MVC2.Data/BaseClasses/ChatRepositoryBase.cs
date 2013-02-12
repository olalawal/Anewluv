using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//to do do away with this when we go to code first , we would pull this from entities 
using Shell.MVC2.Domain.Entities.Anewluv.Chat;
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Data
{
    public class ChatRepositoryBase
    {

        protected ChatContext   _chatcontext;
        protected AnewluvContext _datingcontex;
        //protected PostalData2Entities _postaldatacontext;

        public ChatRepositoryBase(ChatContext chatcontext,AnewluvContext datingcontext)
        {
            _chatcontext = chatcontext;
            _datingcontex = datingcontext;
            // _postaldatacontext = postaldatacontext;
        }

    }
}
