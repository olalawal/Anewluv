using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Services.Contracts.ServiceResponse
{
    public interface IProfileStatus
    {

        DateTime RequestCreateDate { get; set; }
        string Profileid1 { get; set; }
        string Profileid2 { get; set; }
        string Email { get; set; }
       // string OrderNumber { get; set; }
        //long AccountNumber { get; set; }
        DateTime UpdateDate { get; set; }
       // string LabAccession { get; set; }
       // string SpecimenID { get; set; }
        string Username1 { get; set; }
        string Username2 { get; set; }
        //string DonorFirstName { get; set; }
       // string DonorMiddleInitial { get; set; }
       // string DonorLastName { get; set; }
        profilestatusEnum  Status { get; set; }
        DateTime CreationDate { get; set; }
        DateTime DeactivationDate { get; set; }
       // long CollectionSiteId { get; set; }
        //string CollectionSitePhone { get; set; }
        //string CollectionSiteAddress1 { get; set; }
        //string CollectionSiteAddress2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipOrPostal { get; set; }
        string Country { get; set; }
        //string CustomerAccountReference { get; set; }
        DateTime BirthDate { get; set; }
        //int TestCodeOrdered { get; set; }
       // int ReasonForTest { get; set; }

    }
}
