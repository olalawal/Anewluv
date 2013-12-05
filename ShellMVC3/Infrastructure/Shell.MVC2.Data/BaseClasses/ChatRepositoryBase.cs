using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Chat;
using Anewluv.Domain;
//to do do away with this when we go to code first , we would pull this from entities 


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
