
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
    public partial class PostalDataService : LinqToEntitiesDomainService<PostalData2Entities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Afghanistans' query.
        public IQueryable<Afghanistan> GetAfghanistans()
        {
            return this.ObjectContext.Afghanistans;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'AmericanSamoas' query.
        public IQueryable<AmericanSamoa> GetAmericanSamoas()
        {
            return this.ObjectContext.AmericanSamoas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Andorras' query.
        public IQueryable<Andorra> GetAndorras()
        {
            return this.ObjectContext.Andorras;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Angolas' query.
        public IQueryable<Angola> GetAngolas()
        {
            return this.ObjectContext.Angolas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'AntiguaandBarbudas' query.
        public IQueryable<AntiguaandBarbuda> GetAntiguaandBarbudas()
        {
            return this.ObjectContext.AntiguaandBarbudas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Argentinas' query.
        public IQueryable<Argentina> GetArgentinas()
        {
            return this.ObjectContext.Argentinas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Australias' query.
        public IQueryable<Australia> GetAustralias()
        {
            return this.ObjectContext.Australias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Austrias' query.
        public IQueryable<Austria> GetAustrias()
        {
            return this.ObjectContext.Austrias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Azerbaijans' query.
        public IQueryable<Azerbaijan> GetAzerbaijans()
        {
            return this.ObjectContext.Azerbaijans;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bahamas' query.
        public IQueryable<Bahama> GetBahamas()
        {
            return this.ObjectContext.Bahamas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bahrains' query.
        public IQueryable<Bahrain> GetBahrains()
        {
            return this.ObjectContext.Bahrains;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bangladeshes' query.
        public IQueryable<Bangladesh> GetBangladeshes()
        {
            return this.ObjectContext.Bangladeshes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Barbados' query.
        public IQueryable<Barbado> GetBarbados()
        {
            return this.ObjectContext.Barbados;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Belgiums' query.
        public IQueryable<Belgium> GetBelgiums()
        {
            return this.ObjectContext.Belgiums;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bermudas' query.
        public IQueryable<Bermuda> GetBermudas()
        {
            return this.ObjectContext.Bermudas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Brazils' query.
        public IQueryable<Brazil> GetBrazils()
        {
            return this.ObjectContext.Brazils;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'BritishVirginIslands' query.
        public IQueryable<BritishVirginIsland> GetBritishVirginIslands()
        {
            return this.ObjectContext.BritishVirginIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bulgarias' query.
        public IQueryable<Bulgaria> GetBulgarias()
        {
            return this.ObjectContext.Bulgarias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Canadas' query.
        public IQueryable<Canada> GetCanadas()
        {
            return this.ObjectContext.Canadas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CapeVerdes' query.
        public IQueryable<CapeVerde> GetCapeVerdes()
        {
            return this.ObjectContext.CapeVerdes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CaymanIslands' query.
        public IQueryable<CaymanIsland> GetCaymanIslands()
        {
            return this.ObjectContext.CaymanIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Chinas' query.
        public IQueryable<China> GetChinas()
        {
            return this.ObjectContext.Chinas;
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
        public IQueryable<CountryCode> GetCountryCodes()
        {
            return this.ObjectContext.CountryCodes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Croatias' query.
        public IQueryable<Croatia> GetCroatias()
        {
            return this.ObjectContext.Croatias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CustomRegions' query.
        public IQueryable<CustomRegion> GetCustomRegions()
        {
            return this.ObjectContext.CustomRegions;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Cyprus' query.
        public IQueryable<Cypru> GetCyprus()
        {
            return this.ObjectContext.Cyprus;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CzechRepublics' query.
        public IQueryable<CzechRepublic> GetCzechRepublics()
        {
            return this.ObjectContext.CzechRepublics;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Denmarks' query.
        public IQueryable<Denmark> GetDenmarks()
        {
            return this.ObjectContext.Denmarks;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'DominicanRepublics' query.
        public IQueryable<DominicanRepublic> GetDominicanRepublics()
        {
            return this.ObjectContext.DominicanRepublics;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Egypts' query.
        public IQueryable<Egypt> GetEgypts()
        {
            return this.ObjectContext.Egypts;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Eritreas' query.
        public IQueryable<Eritrea> GetEritreas()
        {
            return this.ObjectContext.Eritreas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FalklandIslands' query.
        public IQueryable<FalklandIsland> GetFalklandIslands()
        {
            return this.ObjectContext.FalklandIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Fijis' query.
        public IQueryable<Fiji> GetFijis()
        {
            return this.ObjectContext.Fijis;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Finlands' query.
        public IQueryable<Finland> GetFinlands()
        {
            return this.ObjectContext.Finlands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Frances' query.
        public IQueryable<France> GetFrances()
        {
            return this.ObjectContext.Frances;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FrenchGuianas' query.
        public IQueryable<FrenchGuiana> GetFrenchGuianas()
        {
            return this.ObjectContext.FrenchGuianas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FrenchPolynesias' query.
        public IQueryable<FrenchPolynesia> GetFrenchPolynesias()
        {
            return this.ObjectContext.FrenchPolynesias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Germanies' query.
        public IQueryable<Germany> GetGermanies()
        {
            return this.ObjectContext.Germanies;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ghanas' query.
        public IQueryable<Ghana> GetGhanas()
        {
            return this.ObjectContext.Ghanas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Gibraltars' query.
        public IQueryable<Gibraltar> GetGibraltars()
        {
            return this.ObjectContext.Gibraltars;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'GreatBritains' query.
        public IQueryable<GreatBritain> GetGreatBritains()
        {
            return this.ObjectContext.GreatBritains;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Greenlands' query.
        public IQueryable<Greenland> GetGreenlands()
        {
            return this.ObjectContext.Greenlands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guams' query.
        public IQueryable<Guam> GetGuams()
        {
            return this.ObjectContext.Guams;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guatemalas' query.
        public IQueryable<Guatemala> GetGuatemalas()
        {
            return this.ObjectContext.Guatemalas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guernseys' query.
        public IQueryable<Guernsey> GetGuernseys()
        {
            return this.ObjectContext.Guernseys;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Guyanas' query.
        public IQueryable<Guyana> GetGuyanas()
        {
            return this.ObjectContext.Guyanas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Haitis' query.
        public IQueryable<Haiti> GetHaitis()
        {
            return this.ObjectContext.Haitis;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Honduras' query.
        public IQueryable<Hondura> GetHonduras()
        {
            return this.ObjectContext.Honduras;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'HongKongs' query.
        public IQueryable<HongKong> GetHongKongs()
        {
            return this.ObjectContext.HongKongs;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Hungaries' query.
        public IQueryable<Hungary> GetHungaries()
        {
            return this.ObjectContext.Hungaries;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Icelands' query.
        public IQueryable<Iceland> GetIcelands()
        {
            return this.ObjectContext.Icelands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Indias' query.
        public IQueryable<India> GetIndias()
        {
            return this.ObjectContext.Indias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Indonesias' query.
        public IQueryable<Indonesia> GetIndonesias()
        {
            return this.ObjectContext.Indonesias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Iraqs' query.
        public IQueryable<Iraq> GetIraqs()
        {
            return this.ObjectContext.Iraqs;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'IsleOfMen' query.
        public IQueryable<IsleOfMan> GetIsleOfMen()
        {
            return this.ObjectContext.IsleOfMen;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Israels' query.
        public IQueryable<Israel> GetIsraels()
        {
            return this.ObjectContext.Israels;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Italies' query.
        public IQueryable<Italy> GetItalies()
        {
            return this.ObjectContext.Italies;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Jamaicas' query.
        public IQueryable<Jamaica> GetJamaicas()
        {
            return this.ObjectContext.Jamaicas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Japans' query.
        public IQueryable<Japan> GetJapans()
        {
            return this.ObjectContext.Japans;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Jerseys' query.
        public IQueryable<Jersey> GetJerseys()
        {
            return this.ObjectContext.Jerseys;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Jordans' query.
        public IQueryable<Jordan> GetJordans()
        {
            return this.ObjectContext.Jordans;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Kenyas' query.
        public IQueryable<Kenya> GetKenyas()
        {
            return this.ObjectContext.Kenyas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Lebanons' query.
        public IQueryable<Lebanon> GetLebanons()
        {
            return this.ObjectContext.Lebanons;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Liberias' query.
        public IQueryable<Liberia> GetLiberias()
        {
            return this.ObjectContext.Liberias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Liechtensteins' query.
        public IQueryable<Liechtenstein> GetLiechtensteins()
        {
            return this.ObjectContext.Liechtensteins;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Luxembourgs' query.
        public IQueryable<Luxembourg> GetLuxembourgs()
        {
            return this.ObjectContext.Luxembourgs;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Macedonias' query.
        public IQueryable<Macedonia> GetMacedonias()
        {
            return this.ObjectContext.Macedonias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Madagascars' query.
        public IQueryable<Madagascar> GetMadagascars()
        {
            return this.ObjectContext.Madagascars;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MarshallIslands' query.
        public IQueryable<MarshallIsland> GetMarshallIslands()
        {
            return this.ObjectContext.MarshallIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Martiniques' query.
        public IQueryable<Martinique> GetMartiniques()
        {
            return this.ObjectContext.Martiniques;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Mayottes' query.
        public IQueryable<Mayotte> GetMayottes()
        {
            return this.ObjectContext.Mayottes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Mexicoes' query.
        public IQueryable<Mexico> GetMexicoes()
        {
            return this.ObjectContext.Mexicoes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Moldovas' query.
        public IQueryable<Moldova> GetMoldovas()
        {
            return this.ObjectContext.Moldovas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Monacoes' query.
        public IQueryable<Monaco> GetMonacoes()
        {
            return this.ObjectContext.Monacoes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Moroccoes' query.
        public IQueryable<Morocco> GetMoroccoes()
        {
            return this.ObjectContext.Moroccoes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Netherlands' query.
        public IQueryable<Netherland> GetNetherlands()
        {
            return this.ObjectContext.Netherlands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NetherlandsAntilles' query.
        public IQueryable<NetherlandsAntille> GetNetherlandsAntilles()
        {
            return this.ObjectContext.NetherlandsAntilles;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NewZealands' query.
        public IQueryable<NewZealand> GetNewZealands()
        {
            return this.ObjectContext.NewZealands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Nigerias' query.
        public IQueryable<Nigeria> GetNigerias()
        {
            return this.ObjectContext.Nigerias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NorthernMarianaIslands' query.
        public IQueryable<NorthernMarianaIsland> GetNorthernMarianaIslands()
        {
            return this.ObjectContext.NorthernMarianaIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Norways' query.
        public IQueryable<Norway> GetNorways()
        {
            return this.ObjectContext.Norways;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Pakistans' query.
        public IQueryable<Pakistan> GetPakistans()
        {
            return this.ObjectContext.Pakistans;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PapuaNewGuineas' query.
        public IQueryable<PapuaNewGuinea> GetPapuaNewGuineas()
        {
            return this.ObjectContext.PapuaNewGuineas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Philippines' query.
        public IQueryable<Philippine> GetPhilippines()
        {
            return this.ObjectContext.Philippines;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Polands' query.
        public IQueryable<Poland> GetPolands()
        {
            return this.ObjectContext.Polands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Portugals' query.
        public IQueryable<Portugal> GetPortugals()
        {
            return this.ObjectContext.Portugals;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PuertoRicoes' query.
        public IQueryable<PuertoRico> GetPuertoRicoes()
        {
            return this.ObjectContext.PuertoRicoes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Qatars' query.
        public IQueryable<Qatar> GetQatars()
        {
            return this.ObjectContext.Qatars;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Reunions' query.
        public IQueryable<Reunion> GetReunions()
        {
            return this.ObjectContext.Reunions;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Russias' query.
        public IQueryable<Russia> GetRussias()
        {
            return this.ObjectContext.Russias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SaintPierreandMiquelons' query.
        public IQueryable<SaintPierreandMiquelon> GetSaintPierreandMiquelons()
        {
            return this.ObjectContext.SaintPierreandMiquelons;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Samoas' query.
        public IQueryable<Samoa> GetSamoas()
        {
            return this.ObjectContext.Samoas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SaudiArabias' query.
        public IQueryable<SaudiArabia> GetSaudiArabias()
        {
            return this.ObjectContext.SaudiArabias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SiriLankas' query.
        public IQueryable<SiriLanka> GetSiriLankas()
        {
            return this.ObjectContext.SiriLankas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Slovakias' query.
        public IQueryable<Slovakia> GetSlovakias()
        {
            return this.ObjectContext.Slovakias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Slovenias' query.
        public IQueryable<Slovenia> GetSlovenias()
        {
            return this.ObjectContext.Slovenias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SolomonIslands' query.
        public IQueryable<SolomonIsland> GetSolomonIslands()
        {
            return this.ObjectContext.SolomonIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SouthAfricas' query.
        public IQueryable<SouthAfrica> GetSouthAfricas()
        {
            return this.ObjectContext.SouthAfricas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Spains' query.
        public IQueryable<Spain> GetSpains()
        {
            return this.ObjectContext.Spains;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Swedens' query.
        public IQueryable<Sweden> GetSwedens()
        {
            return this.ObjectContext.Swedens;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Switzerlands' query.
        public IQueryable<Switzerland> GetSwitzerlands()
        {
            return this.ObjectContext.Switzerlands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Syrias' query.
        public IQueryable<Syria> GetSyrias()
        {
            return this.ObjectContext.Syrias;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'sysdiagrams' query.
        public IQueryable<sysdiagram> GetSysdiagrams()
        {
            return this.ObjectContext.sysdiagrams;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Taiwans' query.
        public IQueryable<Taiwan> GetTaiwans()
        {
            return this.ObjectContext.Taiwans;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Thailands' query.
        public IQueryable<Thailand> GetThailands()
        {
            return this.ObjectContext.Thailands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Turkeys' query.
        public IQueryable<Turkey> GetTurkeys()
        {
            return this.ObjectContext.Turkeys;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ugandas' query.
        public IQueryable<Uganda> GetUgandas()
        {
            return this.ObjectContext.Ugandas;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ukraines' query.
        public IQueryable<Ukraine> GetUkraines()
        {
            return this.ObjectContext.Ukraines;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UnitedArabEmirates' query.
        public IQueryable<UnitedArabEmirate> GetUnitedArabEmirates()
        {
            return this.ObjectContext.UnitedArabEmirates;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UnitedStates' query.
        public IQueryable<UnitedState> GetUnitedStates()
        {
            return this.ObjectContext.UnitedStates;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'USVirginIslands' query.
        public IQueryable<USVirginIsland> GetUSVirginIslands()
        {
            return this.ObjectContext.USVirginIslands;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Vietnams' query.
        public IQueryable<Vietnam> GetVietnams()
        {
            return this.ObjectContext.Vietnams;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Yemen' query.
        public IQueryable<Yeman> GetYemen()
        {
            return this.ObjectContext.Yemen;
        }
    }
}


