using System;

public class Class1
{
	public Class1()
	{

         _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                  db.GetRepository<Country_PostalCode_List>().Find()

             }
             catch (Exception ex)
             {

                    logger = new ErroLogging(applicationEnum.MemberService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

         //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                    }
                        catch (Exception ex)
             {
  transaction.Rollback();
                    logger = new ErroLogging(applicationEnum.MemberService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

                }
            }



         _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                  db.GetRepository<Country_PostalCode_List>().Find()

             }
             catch (Exception ex)
             {
                   transaction.Rollback();
                 //instantiate logger here so it does not break anything else.
                 logger = new ErroLogging(applicationEnum.MemberService);
                 //int profileid = Convert.ToInt32(viewerprofileid);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                 //throw convertedexcption;
             }


         }

        addd
             
             db.Add(NewPhoto);           
            int i = db.Commit();
            transaction.Commit();

        update

              db.Update(myProfile);
                           int i = db.Commit();
                           transaction.Commit();


        //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                            transaction.Rollback();


              return  db.GetRepository<profiledata>().Find().Single(p=>p.profilemetadata.photos.ToList().Any(z=>z.id == model.photoid)).gender.description;
           
	}
}


counts

  int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 count = (
            from f in db.GetRepository<block>().Find()
            where (f.profile_id ==   id && f.deletedbymemberdate == null)
            select f).Count();
                 // ?? operator example.
                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;
                 return defaultvalue;






 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in  db.GetRepository<block>().Find()
                    where (p.blockprofile_id == id)
                    join f in  db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;




 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in  db.GetRepository<block>().Find()
                    where (p.blockprofile_id == id && p.viewdate == null)
                    join f in  db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;


   int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (whoisinterestedinme.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoisinterestedinme, pageint ?? 1, numberperpageint ?? 4) : whoisinterestedinme.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                 return Api.MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();


             }
             catch (Exception ex)
             {

                 logger = new ErroLogging(applicationEnum.MemberService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                 Api.DisposeMemberMapperService();
             }







check like
  try
                    {


                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        return db.GetRepository<like>().Find().Any(r => r.profile_id == id && r.likeprofile_id == targetid);


                    }


add like
{
 var id = Convert.ToInt32(profileid);
                         var targetid = Convert.ToInt32(targetprofileid);

                       //check  like first  
                         //if this was a like being restored just do that part
                        var existinglike = db.GetRepository<like>().FindSingle(r => r.profile_id == id && r.likeprofile_id == targetid);

                        //just  update it if we have one already
                        if (existinglike != null)
                        {
                            existinglike.deletedbymemberdate = null; ;
                            existinglike.modificationdate = DateTime.Now;
                            db.Update(existinglike);

                        }
                        else
                        {
                            //like = this. db.GetRepository<like>().Find().Where(p => p.profileid == profileid).FirstOrDefault();
                            //update the profile status to 2
                            like.profile_id = id;
                            like.likeprofile_id = targetid;
                            like.mutual = false;  // not dealing with this calulatin yet
                            like.creationdate = DateTime.Now;
                            //handele the update using EF
                            // this. db.GetRepository<profile>().Find().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                            db.Add(like);
                            
                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

}

remove

    try
                    {

                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                        var like = db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.likeprofile_id == intertargetid).FirstOrDefault();
                        //update the profile status to 2

                        like.deletedbymemberdate = DateTime.Now;
                        like.modificationdate = DateTime.Now;
   db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }


remove by ooopsite

try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                       var like = db.GetRepository<like>().Find().Where(p => p.profile_id == targetid && p.likeprofile_id == id).FirstOrDefault();
                        //update the profile status to 2

                        like.deletedbylikedate = DateTime.Now;
                        like.modificationdate = DateTime.Now;
                        db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }


restore by like profile id

         try
                    {


                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                        var like = db.GetRepository<like>().FindSingle(p => p.profile_id == id && p.likeprofile_id == targetid);
                        //update the profile status to 2

                        like.deletedbymemberdate = null;
                        like.modificationdate = DateTime.Now;

                        db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }


restore by like profile id opposite



                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                        var like = db.GetRepository<like>().FindSingle(p => p.profile_id == targetid && p.likeprofile_id == id);
                        //update the profile status to 2
                        //update the profile status to 2


                        like.deletedbylikedate = null;
                        like.modificationdate = DateTime.Now;

                        db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;



restore defualt

      try
                    {
                        var id = Convert.ToInt32(profileid);
                        // likes = this. db.GetRepository<like>().Find().Where(p => p.profileid == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        like like = new like();
                        foreach (string value in screennames)
                        {

                          
          

                           int? likeprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                           var  currentlike = db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                            like.deletedbymemberdate =null;
                            like.modificationdate = DateTime.Now;
                            db.Update(currentlike);
                           
                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }

restore opposite
try
                    {
                        var id = Convert.ToInt32(profileid);
                        // likes = this. db.GetRepository<like>().Find().Where(p => p.profileid == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        like like = new like();
                        foreach (string value in screennames)
                        {

                           int? likeprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                           var  currentlike = db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                          like.deletedbymemberdate = null;
                            like.modificationdate = DateTime.Now;
                            db.Update(currentlike);
                           
                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }



update view

 try
                    {

                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        var like =  db.GetRepository<like>().Find().Where(p => p.likeprofile_id == targetid && p.profile_id == id).FirstOrDefault();
                        //update the profile status to 2            
                        if (like.viewdate == null)
                        {
                            like.viewdate = DateTime.Now;
                            like.modificationdate = DateTime.Now;
                            db.Update(like);

                            int i = db.Commit();
                            transaction.Commit();
                        }
                        return true;
                    }


