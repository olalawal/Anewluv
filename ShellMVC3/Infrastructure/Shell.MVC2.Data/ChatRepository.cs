using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Data
{
   public  class ChatRepository : MemberRepositoryBase  
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();
   
       
      

       public ChatRepository(AnewluvContext datingcontext) 
           :base(datingcontext)
       {
          
       }

       
           
     

     
    }
}
