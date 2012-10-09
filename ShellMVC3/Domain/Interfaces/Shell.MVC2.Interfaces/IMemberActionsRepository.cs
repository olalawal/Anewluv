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

       List<MemberSearchViewModel> getmutualinterests(int profileid, int targetprofileid);
             
        bool checkinterest(int profileid, int targetprofileid);
       
        bool addinterest(int profileid, int targetprofileid);
     
        bool removeinterestbyprofileid(int profileid, int interestprofile_id);
              
        bool removeinterestbyinterestprofileid(int interestprofile_id, int profileid);
     
        bool restoreinterestbyprofileid(int profileid, int interestprofile_id);
      
        bool restoreinterestbyinterestprofileid(int interestprofile_id, int profileid);
        
        bool removeinterestsbyprofileidandscreennames(int profileid, List<String> screennames);
      
        bool restoreinterestsbyprofileidandscreennames(int profileid, List<String> screennames);
            
        bool updateinterestviewstatus(int profileid, int targetprofileid);
     


        #endregion
                    
        #region "peek methods"
         
      int getwhoipeekedatcount(int profileid);
          
      int getwhopeekedatmecount(int profileid);
          
      int getwhoeekedatmenewcount(int profileid);
    
       List<MemberSearchViewModel> getwhopeekedatme(int profileid, int? Page, int? NumberPerPage);
   
         List <MemberSearchViewModel> getwhopeekedatmenew(int profileid, int? Page, int? NumberPerPage);   
    
       List<MemberSearchViewModel> getwhoipeekedat(int profileid, int? Page, int? NumberPerPage);
 
      List<MemberSearchViewModel> getmutualpeeks(int profileid, int targetprofileid);
           
        bool checkpeek(int profileid, int targetprofileid);
 
        bool addpeek(int profileid, int targetprofileid);
      
        bool removepeekbyprofileid(int profileid, int peekprofile_id);
            
        bool removepeekbypeekprofileid( int peekprofile_id,int profileid);
       
        bool restorepeekbyprofileid(int profileid, int peekprofile_id);
      
        bool restorepeekbypeekprofileid(string peekprofile_id,int profileid);
            
        bool removepeeksbyprofileidandscreennames(int profileid, List<String> screennames);
            
        bool restorepeeksbyprofileidandscreennames(int profileid, List<String> screennames);

        bool updatepeekviewstatus(int profileid, int targetprofileid);
      
        #endregion 

        #region "block methods"

         int getwhoiblockedcount(int profileid);

       List<MemberSearchViewModel> getwhoiblocked(int profileid, int? Page, int? NumberPerPage);
   
                   
      List<MemberSearchViewModel> getmutualblocks(int profileid, int targetprofileid);
  
        bool checkblock(int profileid, int targetprofileid);
     
        bool addblock(int profileid, int targetprofileid);    

        bool removeblock(int profileid, int blockprofile_id);
     
        bool restoreblock(int profileid, int blockprofile_id);
     
        bool removeblocksbyscreennames(int profileid, List<String> screennames);
    
        bool restoreblocksbyscreennames(int profileid, List<String> screennames);
        
        bool updateblockreviewstatus(int profileid,int targetprofileid, int reviewerid);
     
        #endregion   

        #region "Like methods"
     
      int getwhoilikecount(int profileid);
       
      int getwholikesmecount(int profileid);
           
      int getwhoislikesmenewcount(int profileid);
    
       List <MemberSearchViewModel> getwholikesmenew(int profileid, int? Page, int? NumberPerPage); 
      
        List <MemberSearchViewModel> getwholikesme(int profileid, int? Page, int? NumberPerPage);
                
       List<MemberSearchViewModel> getwhoilike(int profileid, int? Page, int? NumberPerPage);
       
      List<MemberSearchViewModel> getmutuallikes(int profileid, int targetprofileid);
             
        bool checklike(int profileid, int targetprofileid);
      
        bool addlike(int profileid, int targetprofileid);
    
        bool removelikebyprofileid(int profileid, int likeprofile_id) ;       

        bool removelikebylikeprofileid( int likeprofile_id,int profileid);
       
        bool restorelikebyprofileid(int profileid, int likeprofile_id);
    
        bool restorelikebylikeprofileid(int likeprofile_id,int profileid);
       
        bool removelikesbyprofileidandscreennames(int profileid, List<String> screennames);

        bool restorelikesbyprofileidandscreennames(int profileid, List<String> screennames);
    
        bool updatelikeviewstatus(int profileid, int targetprofileid);
       

        #endregion

  

    }


   
}
