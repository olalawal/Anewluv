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


             
      int getwhoiaminterestedincount(int profileid);
             
      int getwhoisinterestedinmecount(int profileid);
       
      int getwhoisinterestedinmenewcount(int profileid);
        
       List<MemberSearchViewModel> getinterests(int profileid, int? Page, int? NumberPerPage);

       List<MemberSearchViewModel> getwhoisinterestedinme(int profileid, int? Page, int? NumberPerPage);

       List<MemberSearchViewModel> getwhoisinterestedinmenew(int profileid, int? Page, int? NumberPerPage);

       List<MemberSearchViewModel> getmutualinterests(int profileid, string targetprofileid);
             
        bool checkinterest(int profileid, string targetprofileid);
       
        bool addinterest(int profileid, string targetprofileid);
     
        bool removeinterestbyprofileid(int profileid, string interestprofile_id);
              
        bool removeinterestbyinterestprofileid(string interestprofile_id, int profileid);
     
        bool restoreinterestbyprofileid(int profileid, string interestprofile_id);
      
        bool restoreinterestbyinterestprofileid(string interestprofile_id, int profileid);
        
        bool removeinterestsbyprofileidandscreennames(int profileid, List<String> screennames);
      
        bool restoreinterestsbyprofileidandscreennames(int profileid, List<String> screennames);
            
        bool updateinterestviewstatus(int profileid, string targetprofileid);
     


        #endregion
                    
        #region "peek methods"
         
      int getwhoipeekedatcount(int profileid);
          
      int getwhopeekedatmecount(int profileid);
          
      int getwhoeekedatmenewcount(int profileid);
    
       List<MemberSearchViewModel> getwhopeekedatme(int profileid, int? Page, int? NumberPerPage);
   
         List <MemberSearchViewModel> getwhopeekedatmenew(int profileid, int? Page, int? NumberPerPage);   
    
       List<MemberSearchViewModel> getwhoipeekedat(int profileid, int? Page, int? NumberPerPage);
 
      List<MemberSearchViewModel> getmutualpeeks(int profileid, string targetprofileid);
           
        bool checkpeek(int profileid, string targetprofileid);
 
        bool addpeek(int profileid, string targetprofileid);
      
        bool removepeekbyprofileid(int profileid, string peekprofile_id);
            
        bool removepeekbypeekprofileid( string peekprofile_id,int profileid);
       
        bool restorepeekbyprofileid(int profileid, string peekprofile_id);
      
        bool restorepeekbypeekprofileid(string peekprofile_id,int profileid);
            
        bool removepeeksbyprofileidandscreennames(int profileid, List<String> screennames);
            
        bool restorepeeksbyprofileidandscreennames(int profileid, List<String> screennames);

        bool updatepeekviewstatus(int profileid, string targetprofileid);
      
        #endregion 

        #region "block methods"

         int getwhoiblockedcount(int profileid);

       List<MemberSearchViewModel> getwhoiblocked(int profileid, int? Page, int? NumberPerPage);
   
                   
      List<MemberSearchViewModel> getmutualblocks(int profileid, string targetprofileid);
  
        bool checkblock(int profileid, string targetprofileid);
     
        bool addblock(int profileid, string targetprofileid);    

        bool removeblock(int profileid, string blockprofile_id);
     
        bool restoreblock(int profileid, string blockprofile_id);
     
        bool removeblocksbyscreennames(int profileid, List<String> screennames);
    
        bool restoreblocksbyscreennames(int profileid, List<String> screennames);
        
        bool updateblockreviewstatus(int profileid,string targetprofileid, string reviewerid);
     
        #endregion   

        #region "Like methods"
     
      int getwhoilikecount(int profileid);
       
      int getwholikesmecount(int profileid);
           
      int getwhoislikesmenewcount(int profileid);
    
       List <MemberSearchViewModel> getwholikesmenew(int profileid, int? Page, int? NumberPerPage); 
      
        List <MemberSearchViewModel> getwholikesme(int profileid, int? Page, int? NumberPerPage);
                
       List<MemberSearchViewModel> getwhoilike(int profileid, int? Page, int? NumberPerPage);
       
      List<MemberSearchViewModel> getmutuallikes(int profileid, string targetprofileid);
             
        bool checklike(int profileid, string targetprofileid);
      
        bool addlike(int profileid, string targetprofileid);
    
        bool removelikebyprofileid(int profileid, string likeprofile_id) ;       

        bool removelikebylikeprofileid( string likeprofile_id,int profileid);
       
        bool restorelikebyprofileid(int profileid, string likeprofile_id);
    
        bool restorelikebylikeprofileid(string likeprofile_id,int profileid);
       
        bool removelikesbyprofileidandscreennames(int profileid, List<String> screennames);

        bool restorelikesbyprofileidandscreennames(int profileid, List<String> screennames);
    
        bool updatelikeviewstatus(int profileid, string targetprofileid);
       

        #endregion

  

    }


   
}
