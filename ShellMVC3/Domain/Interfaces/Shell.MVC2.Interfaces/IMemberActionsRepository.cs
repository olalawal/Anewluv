using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dating.Server.Data;

  
   
  //  using System.Data;
   // using System.Linq;

using System.Data.Objects;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace Shell.MVC2.Interfaces
{
    public  interface IMemberActionsRepository
    {
   

        #region "Interest Methods"


             
      int getwhoiaminterestedincount(string profileid);
             
      int getwhoisinterestedinmecount(string profileid);
       
      int getwhoisinterestedinmenewcount(string profileid);
        
       List<MemberSearchViewModel> getinterests(string profileid, int? Page, int? NumberPerPage);

       List<MemberSearchViewModel> getwhoisinterestedinme(string profileid, int? Page, int? NumberPerPage);

       List<MemberSearchViewModel> getwhoisinterestedinmenew(string profileid, int? Page, int? NumberPerPage);

       List<MemberSearchViewModel> getmutualinterests(string profileid, string targetprofileid);
             
        bool checkinterest(string profileid, string targetprofileid);
       
        bool addinterest(string profileid, string targetprofileid);
     
        bool removeinterestbyprofileid(string profileid, string interestprofile_id);
              
        bool removeinterestbyinterestprofileid(string interestprofile_id, string profileid);
     
        bool restoreinterestbyprofileid(string profileid, string interestprofile_id);
      
        bool restoreinterestbyinterestprofileid(string interestprofile_id, string profileid);
        
        bool removeinterestsbyprofileidandscreennames(string profileid, List<String> screennames);
      
        bool restoreinterestsbyprofileidandscreennames(string profileid, List<String> screennames);
            
        bool updateinterestviewstatus(string profileid, string targetprofileid);
     


        #endregion
                    
        #region "peek methods"
         
      int getwhoipeekedatcount(string profileid);
          
      int getwhopeekedatmecount(string profileid);
          
      int getwhoeekedatmenewcount(string profileid);
    
       List<MemberSearchViewModel> getwhopeekedatme(string profileid, int? Page, int? NumberPerPage);
   
         List <MemberSearchViewModel> getwhopeekedatmenew(string profileid, int? Page, int? NumberPerPage);   
    
       List<MemberSearchViewModel> getwhoipeekedat(string profileid, int? Page, int? NumberPerPage);
 
      List<MemberSearchViewModel> getmutualpeeks(string profileid, string targetprofileid);
           
        bool checkpeek(string profileid, string targetprofileid);
 
        bool addpeek(string profileid, string targetprofileid);
      
        bool removepeekbyprofileid(string profileid, string peekprofile_id);
            
        bool removepeekbypeekprofileid( string peekprofile_id,string profileid);
       
        bool restorepeekbyprofileid(string profileid, string peekprofile_id);
      
        bool restorepeekbypeekprofileid(string peekprofile_id,string profileid);
            
        bool removepeeksbyprofileidandscreennames(string profileid, List<String> screennames);
            
        bool restorepeeksbyprofileidandscreennames(string profileid, List<String> screennames);

        bool updatepeekviewstatus(string profileid, string targetprofileid);
      
        #endregion 

        #region "block methods"

         int getwhoiblockedcount(string profileid);

       List<MemberSearchViewModel> getwhoiblocked(string profileid, int? Page, int? NumberPerPage);
   
                   
      List<MemberSearchViewModel> getmutualblocks(string profileid, string targetprofileid);
  
        bool checkblock(string profileid, string targetprofileid);
     
        bool addblock(string profileid, string targetprofileid);    

        bool removeblock(string profileid, string blockprofile_id);
     
        bool restoreblock(string profileid, string blockprofile_id);
     
        bool removeblocksbyscreennames(string profileid, List<String> screennames);
    
        bool restoreblocksbyscreennames(string profileid, List<String> screennames);
        
        bool updateblockreviewstatus(string profileid,string targetprofileid, string reviewerid);
     
        #endregion   

        #region "Like methods"
     
      int getwhoilikecount(string profileid);
       
      int getwholikesmecount(string profileid);
           
      int getwhoislikesmenewcount(string profileid);
    
       List <MemberSearchViewModel> getwholikesmenew(string profileid, int? Page, int? NumberPerPage); 
      
        List <MemberSearchViewModel> getwholikesme(string profileid, int? Page, int? NumberPerPage);
                
       List<MemberSearchViewModel> getwhoilike(string profileid, int? Page, int? NumberPerPage);
       
      List<MemberSearchViewModel> getmutuallikes(string profileid, string targetprofileid);
             
        bool checklike(string profileid, string targetprofileid);
      
        bool addlike(string profileid, string targetprofileid);
    
        bool removelikebyprofileid(string profileid, string likeprofile_id) ;       

        bool removelikebylikeprofileid( string likeprofile_id,string profileid);
       
        bool restorelikebyprofileid(string profileid, string likeprofile_id);
    
        bool restorelikebylikeprofileid(string likeprofile_id,string profileid);
       
        bool removelikesbyprofileidandscreennames(string profileid, List<String> screennames);

        bool restorelikesbyprofileidandscreennames(string profileid, List<String> screennames);
    
        bool updatelikeviewstatus(string profileid, string targetprofileid);
       

        #endregion

  

    }


   
}
