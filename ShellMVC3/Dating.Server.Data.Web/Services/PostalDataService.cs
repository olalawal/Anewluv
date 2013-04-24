
namespace Dating.Server.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Dating.Server.Data.Models;


    // Implements application logic using the PostalData2Entities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial  class PostalDataService : LinqToEntitiesDomainService<PostalData2Entities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Afghanistan' query.
        public IQueryable<Afghanistan> GetAfghanistan()
        {
            return this.ObjectContext.Afghanistan;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'AmericanSamoa' query.
        public IQueryable<AmericanSamoa> GetAmericanSamoa()
        {
            return this.ObjectContext.AmericanSamoa;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Andorra' query.
        public IQueryable<Andorra> GetAndorra()
        {
            return this.ObjectContext.Andorra;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Angola' query.
        public IQueryable<Angola> GetAngola()
        {
            return this.ObjectContext.Angola;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'AntiguaandBarbuda' query.
        public IQueryable<AntiguaandBarbuda> GetAntiguaandBarbuda()
        {
            return this.ObjectContext.AntiguaandBarbuda;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Argentina' query.
        public IQueryable<Argentina> GetArgentina()
        {
            return this.ObjectContext.Argentina;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Australia' query.
        public IQueryable<Australia> GetAustralia()
        {
            return this.ObjectContext.Australia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Austria' query.
        public IQueryable<Austria> GetAustria()
        {
            return this.ObjectContext.Austria;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Azerbaijan' query.
        public IQueryable<Azerbaijan> GetAzerbaijan()
        {
            return this.ObjectContext.Azerbaijan;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bahamas' query.
        public IQueryable<Bahamas> GetBahamas()
        {
            return this.ObjectContext.Bahamas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bahrain' query.
        public IQueryable<Bahrain> GetBahrain()
        {
            return this.ObjectContext.Bahrain;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bangladesh' query.
        public IQueryable<Bangladesh> GetBangladesh()
        {
            return this.ObjectContext.Bangladesh;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Barbados' query.
        public IQueryable<Barbados> GetBarbados()
        {
            return this.ObjectContext.Barbados;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Belgium' query.
        public IQueryable<Belgium> GetBelgium()
        {
            return this.ObjectContext.Belgium;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Belize' query.
        public IQueryable<Belize> GetBelize()
        {
            return this.ObjectContext.Belize;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bermuda' query.
        public IQueryable<Bermuda> GetBermuda()
        {
            return this.ObjectContext.Bermuda;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Brazil' query.
        public IQueryable<Brazil> GetBrazil()
        {
            return this.ObjectContext.Brazil;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'BritishVirginIslands' query.
        public IQueryable<BritishVirginIslands> GetBritishVirginIslands()
        {
            return this.ObjectContext.BritishVirginIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bulgaria' query.
        public IQueryable<Bulgaria> GetBulgaria()
        {
            return this.ObjectContext.Bulgaria;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Canada' query.
        public IQueryable<Canada> GetCanada()
        {
            return this.ObjectContext.Canada;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CapeVerde' query.
        public IQueryable<CapeVerde> GetCapeVerde()
        {
            return this.ObjectContext.CapeVerde;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CaymanIslands' query.
        public IQueryable<CaymanIslands> GetCaymanIslands()
        {
            return this.ObjectContext.CaymanIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Chile' query.
        public IQueryable<Chile> GetChile()
        {
            return this.ObjectContext.Chile;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'China' query.
        public IQueryable<China> GetChina()
        {
            return this.ObjectContext.China;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Colombia' query.
        public IQueryable<Colombia> GetColombia()
        {
            return this.ObjectContext.Colombia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Costa_Rica' query.
        public IQueryable<Costa_Rica> GetCosta_Rica()
        {
            return this.ObjectContext.Costa_Rica;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Country_PostalCode_List' query.
        public IQueryable<Country_PostalCode_List> GetCountry_PostalCode_List()
        {
            return this.ObjectContext.Country_PostalCode_List;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Country_PostalCode_Region' query.
        public IQueryable<Country_PostalCode_Region> GetCountry_PostalCode_Region()
        {
            return this.ObjectContext.Country_PostalCode_Region;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CountryCodes' query.
        public IQueryable<CountryCodes> GetCountryCodes()
        {
            return this.ObjectContext.CountryCodes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Croatia' query.
        public IQueryable<Croatia> GetCroatia()
        {
            return this.ObjectContext.Croatia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Cuba' query.
        public IQueryable<Cuba> GetCuba()
        {
            return this.ObjectContext.Cuba;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CustomRegions' query.
        public IQueryable<CustomRegions> GetCustomRegions()
        {
            return this.ObjectContext.CustomRegions;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Cyprus' query.
        public IQueryable<Cyprus> GetCyprus()
        {
            return this.ObjectContext.Cyprus;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CzechRepublic' query.
        public IQueryable<CzechRepublic> GetCzechRepublic()
        {
            return this.ObjectContext.CzechRepublic;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Denmark' query.
        public IQueryable<Denmark> GetDenmark()
        {
            return this.ObjectContext.Denmark;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'DominicanRepublic' query.
        public IQueryable<DominicanRepublic> GetDominicanRepublic()
        {
            return this.ObjectContext.DominicanRepublic;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Egypt' query.
        public IQueryable<Egypt> GetEgypt()
        {
            return this.ObjectContext.Egypt;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Eritrea' query.
        public IQueryable<Eritrea> GetEritrea()
        {
            return this.ObjectContext.Eritrea;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FalklandIslands' query.
        public IQueryable<FalklandIslands> GetFalklandIslands()
        {
            return this.ObjectContext.FalklandIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Fiji' query.
        public IQueryable<Fiji> GetFiji()
        {
            return this.ObjectContext.Fiji;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Finland' query.
        public IQueryable<Finland> GetFinland()
        {
            return this.ObjectContext.Finland;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'France' query.
        public IQueryable<France> GetFrance()
        {
            return this.ObjectContext.France;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FrenchGuiana' query.
        public IQueryable<FrenchGuiana> GetFrenchGuiana()
        {
            return this.ObjectContext.FrenchGuiana;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FrenchPolynesia' query.
        public IQueryable<FrenchPolynesia> GetFrenchPolynesia()
        {
            return this.ObjectContext.FrenchPolynesia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Germany' query.
        public IQueryable<Germany> GetGermany()
        {
            return this.ObjectContext.Germany;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ghana' query.
        public IQueryable<Ghana> GetGhana()
        {
            return this.ObjectContext.Ghana;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Gibraltar' query.
        public IQueryable<Gibraltar> GetGibraltar()
        {
            return this.ObjectContext.Gibraltar;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Greenland' query.
        public IQueryable<Greenland> GetGreenland()
        {
            return this.ObjectContext.Greenland;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guam' query.
        public IQueryable<Guam> GetGuam()
        {
            return this.ObjectContext.Guam;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guatemala' query.
        public IQueryable<Guatemala> GetGuatemala()
        {
            return this.ObjectContext.Guatemala;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guernsey' query.
        public IQueryable<Guernsey> GetGuernsey()
        {
            return this.ObjectContext.Guernsey;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guyana' query.
        public IQueryable<Guyana> GetGuyana()
        {
            return this.ObjectContext.Guyana;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Haiti' query.
        public IQueryable<Haiti> GetHaiti()
        {
            return this.ObjectContext.Haiti;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Honduras' query.
        public IQueryable<Honduras> GetHonduras()
        {
            return this.ObjectContext.Honduras;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'HongKong' query.
        public IQueryable<HongKong> GetHongKong()
        {
            return this.ObjectContext.HongKong;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Hungary' query.
        public IQueryable<Hungary> GetHungary()
        {
            return this.ObjectContext.Hungary;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Iceland' query.
        public IQueryable<Iceland> GetIceland()
        {
            return this.ObjectContext.Iceland;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'India' query.
        public IQueryable<India> GetIndia()
        {
            return this.ObjectContext.India;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Indonesia' query.
        public IQueryable<Indonesia> GetIndonesia()
        {
            return this.ObjectContext.Indonesia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Iraq' query.
        public IQueryable<Iraq> GetIraq()
        {
            return this.ObjectContext.Iraq;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ireland' query.
        public IQueryable<Ireland> GetIreland()
        {
            return this.ObjectContext.Ireland;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'IsleOfMan' query.
        public IQueryable<IsleOfMan> GetIsleOfMan()
        {
            return this.ObjectContext.IsleOfMan;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Israel' query.
        public IQueryable<Israel> GetIsrael()
        {
            return this.ObjectContext.Israel;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Italy' query.
        public IQueryable<Italy> GetItaly()
        {
            return this.ObjectContext.Italy;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'IvoryCoast' query.
        public IQueryable<IvoryCoast> GetIvoryCoast()
        {
            return this.ObjectContext.IvoryCoast;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Jamaica' query.
        public IQueryable<Jamaica> GetJamaica()
        {
            return this.ObjectContext.Jamaica;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Japan' query.
        public IQueryable<Japan> GetJapan()
        {
            return this.ObjectContext.Japan;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Jersey' query.
        public IQueryable<Jersey> GetJersey()
        {
            return this.ObjectContext.Jersey;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Jordan' query.
        public IQueryable<Jordan> GetJordan()
        {
            return this.ObjectContext.Jordan;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Kenya' query.
        public IQueryable<Kenya> GetKenya()
        {
            return this.ObjectContext.Kenya;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Kiribati' query.
        public IQueryable<Kiribati> GetKiribati()
        {
            return this.ObjectContext.Kiribati;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Lebanon' query.
        public IQueryable<Lebanon> GetLebanon()
        {
            return this.ObjectContext.Lebanon;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Liberia' query.
        public IQueryable<Liberia> GetLiberia()
        {
            return this.ObjectContext.Liberia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Liechtenstein' query.
        public IQueryable<Liechtenstein> GetLiechtenstein()
        {
            return this.ObjectContext.Liechtenstein;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Luxembourg' query.
        public IQueryable<Luxembourg> GetLuxembourg()
        {
            return this.ObjectContext.Luxembourg;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Macedonia' query.
        public IQueryable<Macedonia> GetMacedonia()
        {
            return this.ObjectContext.Macedonia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Madagascar' query.
        public IQueryable<Madagascar> GetMadagascar()
        {
            return this.ObjectContext.Madagascar;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Malaysia' query.
        public IQueryable<Malaysia> GetMalaysia()
        {
            return this.ObjectContext.Malaysia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Malta' query.
        public IQueryable<Malta> GetMalta()
        {
            return this.ObjectContext.Malta;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MarshallIslands' query.
        public IQueryable<MarshallIslands> GetMarshallIslands()
        {
            return this.ObjectContext.MarshallIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Martinique' query.
        public IQueryable<Martinique> GetMartinique()
        {
            return this.ObjectContext.Martinique;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Mayotte' query.
        public IQueryable<Mayotte> GetMayotte()
        {
            return this.ObjectContext.Mayotte;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Mexico' query.
        public IQueryable<Mexico> GetMexico()
        {
            return this.ObjectContext.Mexico;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Moldova' query.
        public IQueryable<Moldova> GetMoldova()
        {
            return this.ObjectContext.Moldova;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Monaco' query.
        public IQueryable<Monaco> GetMonaco()
        {
            return this.ObjectContext.Monaco;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Morocco' query.
        public IQueryable<Morocco> GetMorocco()
        {
            return this.ObjectContext.Morocco;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Nepal' query.
        public IQueryable<Nepal> GetNepal()
        {
            return this.ObjectContext.Nepal;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Netherlands' query.
        public IQueryable<Netherlands> GetNetherlands()
        {
            return this.ObjectContext.Netherlands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NetherlandsAntilles' query.
        public IQueryable<NetherlandsAntilles> GetNetherlandsAntilles()
        {
            return this.ObjectContext.NetherlandsAntilles;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NewZealand' query.
        public IQueryable<NewZealand> GetNewZealand()
        {
            return this.ObjectContext.NewZealand;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Nigeria' query.
        public IQueryable<Nigeria> GetNigeria()
        {
            return this.ObjectContext.Nigeria;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NorthernMarianaIslands' query.
        public IQueryable<NorthernMarianaIslands> GetNorthernMarianaIslands()
        {
            return this.ObjectContext.NorthernMarianaIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Norway' query.
        public IQueryable<Norway> GetNorway()
        {
            return this.ObjectContext.Norway;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Pakistan' query.
        public IQueryable<Pakistan> GetPakistan()
        {
            return this.ObjectContext.Pakistan;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PapuaNewGuinea' query.
        public IQueryable<PapuaNewGuinea> GetPapuaNewGuinea()
        {
            return this.ObjectContext.PapuaNewGuinea;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Peru' query.
        public IQueryable<Peru> GetPeru()
        {
            return this.ObjectContext.Peru;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Philippines' query.
        public IQueryable<Philippines> GetPhilippines()
        {
            return this.ObjectContext.Philippines;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Poland' query.
        public IQueryable<Poland> GetPoland()
        {
            return this.ObjectContext.Poland;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Portugal' query.
        public IQueryable<Portugal> GetPortugal()
        {
            return this.ObjectContext.Portugal;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PuertoRico' query.
        public IQueryable<PuertoRico> GetPuertoRico()
        {
            return this.ObjectContext.PuertoRico;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Qatar' query.
        public IQueryable<Qatar> GetQatar()
        {
            return this.ObjectContext.Qatar;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Reunion' query.
        public IQueryable<Reunion> GetReunion()
        {
            return this.ObjectContext.Reunion;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Russia' query.
        public IQueryable<Russia> GetRussia()
        {
            return this.ObjectContext.Russia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SaintPierreandMiquelon' query.
        public IQueryable<SaintPierreandMiquelon> GetSaintPierreandMiquelon()
        {
            return this.ObjectContext.SaintPierreandMiquelon;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SaintVincentandtheGrenadines' query.
        public IQueryable<SaintVincentandtheGrenadines> GetSaintVincentandtheGrenadines()
        {
            return this.ObjectContext.SaintVincentandtheGrenadines;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Samoa' query.
        public IQueryable<Samoa> GetSamoa()
        {
            return this.ObjectContext.Samoa;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SaudiArabia' query.
        public IQueryable<SaudiArabia> GetSaudiArabia()
        {
            return this.ObjectContext.SaudiArabia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Senegal' query.
        public IQueryable<Senegal> GetSenegal()
        {
            return this.ObjectContext.Senegal;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Slovakia' query.
        public IQueryable<Slovakia> GetSlovakia()
        {
            return this.ObjectContext.Slovakia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Slovenia' query.
        public IQueryable<Slovenia> GetSlovenia()
        {
            return this.ObjectContext.Slovenia;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SolomonIslands' query.
        public IQueryable<SolomonIslands> GetSolomonIslands()
        {
            return this.ObjectContext.SolomonIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SouthAfrica' query.
        public IQueryable<SouthAfrica> GetSouthAfrica()
        {
            return this.ObjectContext.SouthAfrica;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Spain' query.
        public IQueryable<Spain> GetSpain()
        {
            return this.ObjectContext.Spain;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SriLanka' query.
        public IQueryable<SriLanka> GetSriLanka()
        {
            return this.ObjectContext.SriLanka;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Sweden' query.
        public IQueryable<Sweden> GetSweden()
        {
            return this.ObjectContext.Sweden;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Switzerland' query.
        public IQueryable<Switzerland> GetSwitzerland()
        {
            return this.ObjectContext.Switzerland;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Syria' query.
        public IQueryable<Syria> GetSyria()
        {
            return this.ObjectContext.Syria;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'sysdiagrams' query.
        public IQueryable<sysdiagrams> GetSysdiagrams()
        {
            return this.ObjectContext.sysdiagrams;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Taiwan' query.
        public IQueryable<Taiwan> GetTaiwan()
        {
            return this.ObjectContext.Taiwan;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Tanzania' query.
        public IQueryable<Tanzania> GetTanzania()
        {
            return this.ObjectContext.Tanzania;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Thailand' query.
        public IQueryable<Thailand> GetThailand()
        {
            return this.ObjectContext.Thailand;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'TrinidadandTobago' query.
        public IQueryable<TrinidadandTobago> GetTrinidadandTobago()
        {
            return this.ObjectContext.TrinidadandTobago;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Turkey' query.
        public IQueryable<Turkey> GetTurkey()
        {
            return this.ObjectContext.Turkey;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Uganda' query.
        public IQueryable<Uganda> GetUganda()
        {
            return this.ObjectContext.Uganda;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ukraine' query.
        public IQueryable<Ukraine> GetUkraine()
        {
            return this.ObjectContext.Ukraine;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UnitedArabEmirates' query.
        public IQueryable<UnitedArabEmirates> GetUnitedArabEmirates()
        {
            return this.ObjectContext.UnitedArabEmirates;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UnitedKingdom' query.
        public IQueryable<UnitedKingdom> GetUnitedKingdom()
        {
            return this.ObjectContext.UnitedKingdom;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UnitedStates' query.
        public IQueryable<UnitedStates> GetUnitedStates()
        {
            return this.ObjectContext.UnitedStates;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'USVirginIslands' query.
        public IQueryable<USVirginIslands> GetUSVirginIslands()
        {
            return this.ObjectContext.USVirginIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Venezuela' query.
        public IQueryable<Venezuela> GetVenezuela()
        {
            return this.ObjectContext.Venezuela;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Vietnam' query.
        public IQueryable<Vietnam> GetVietnam()
        {
            return this.ObjectContext.Vietnam;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Yemen' query.
        public IQueryable<Yemen> GetYemen()
        {
            return this.ObjectContext.Yemen;
        }
    }
}


