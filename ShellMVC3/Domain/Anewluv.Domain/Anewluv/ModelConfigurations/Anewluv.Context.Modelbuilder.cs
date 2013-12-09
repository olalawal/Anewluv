using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Anewluv.Domain.Data.Mapping;

namespace  Anewluv.Domain
{
     public partial class AnewluvContext
    {
        //4-26-2013 olawal - added model builders for the review detail and promotion object detail
        //TO DO - determine weather we should be doing man detail ojects per rview since we have history as well
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             modelBuilder.Configurations.Add(new abusereportnoteMap());
            modelBuilder.Configurations.Add(new abusereportMap());
            modelBuilder.Configurations.Add(new applicationiconconversionMap());
            modelBuilder.Configurations.Add(new applicationitemMap());
            modelBuilder.Configurations.Add(new applicationroleMap());
            modelBuilder.Configurations.Add(new applicationMap());
            modelBuilder.Configurations.Add(new blocknoteMap());
            modelBuilder.Configurations.Add(new blockMap());
            modelBuilder.Configurations.Add(new communicationquotaMap());
            modelBuilder.Configurations.Add(new favoriteMap());
            modelBuilder.Configurations.Add(new friendMap());
            modelBuilder.Configurations.Add(new hotlistMap());
            modelBuilder.Configurations.Add(new interestMap());
            modelBuilder.Configurations.Add(new likeMap());
            modelBuilder.Configurations.Add(new lu_abusetypeMap());
            modelBuilder.Configurations.Add(new lu_activitytypeMap());
            modelBuilder.Configurations.Add(new lu_applicationitempaymenttypeMap());
            modelBuilder.Configurations.Add(new lu_applicationitemtransfertypeMap());
            modelBuilder.Configurations.Add(new lu_applicationtypeMap());
            modelBuilder.Configurations.Add(new lu_bodytypeMap());
            modelBuilder.Configurations.Add(new lu_defaultmailboxfolderMap());
            modelBuilder.Configurations.Add(new lu_dietMap());
            modelBuilder.Configurations.Add(new lu_drinksMap());
            modelBuilder.Configurations.Add(new lu_educationlevelMap());
            modelBuilder.Configurations.Add(new lu_employmentstatusMap());
            modelBuilder.Configurations.Add(new lu_ethnicityMap());
            modelBuilder.Configurations.Add(new lu_exerciseMap());
            modelBuilder.Configurations.Add(new lu_eyecolorMap());
            modelBuilder.Configurations.Add(new lu_flagyesnoMap());
            modelBuilder.Configurations.Add(new lu_genderMap());
            modelBuilder.Configurations.Add(new lu_haircolorMap());
            modelBuilder.Configurations.Add(new lu_havekidsMap());
            modelBuilder.Configurations.Add(new lu_heightMap());
            modelBuilder.Configurations.Add(new lu_hobbyMap());
            modelBuilder.Configurations.Add(new lu_hotfeatureMap());
            modelBuilder.Configurations.Add(new lu_humorMap());
            modelBuilder.Configurations.Add(new lu_iconformatMap());
            modelBuilder.Configurations.Add(new lu_iconImagersizerformatMap());
            modelBuilder.Configurations.Add(new lu_incomelevelMap());
            modelBuilder.Configurations.Add(new lu_livingsituationMap());
            modelBuilder.Configurations.Add(new lu_lookingforMap());
            modelBuilder.Configurations.Add(new lu_maritalstatusMap());
            modelBuilder.Configurations.Add(new lu_notetypeMap());
            modelBuilder.Configurations.Add(new lu_openidproviderMap());
            modelBuilder.Configurations.Add(new lu_photoapprovalstatusMap());
            modelBuilder.Configurations.Add(new lu_photoformatMap());
            modelBuilder.Configurations.Add(new lu_photoImagersizerformatMap());
            modelBuilder.Configurations.Add(new lu_photoimagetypeMap());
            modelBuilder.Configurations.Add(new lu_photorejectionreasonMap());
            modelBuilder.Configurations.Add(new lu_photostatusMap());
            modelBuilder.Configurations.Add(new lu_photostatusdescriptionMap());
            modelBuilder.Configurations.Add(new lu_politicalviewMap());
            modelBuilder.Configurations.Add(new lu_professionMap());
            modelBuilder.Configurations.Add(new lu_profilefiltertypeMap());
            modelBuilder.Configurations.Add(new lu_profilestatusMap());
            modelBuilder.Configurations.Add(new lu_religionMap());
            modelBuilder.Configurations.Add(new lu_religiousattendanceMap());
            modelBuilder.Configurations.Add(new lu_roleMap());
            modelBuilder.Configurations.Add(new lu_securityleveltypeMap());
            modelBuilder.Configurations.Add(new lu_securityquestionMap());
            modelBuilder.Configurations.Add(new lu_showmeMap());
            modelBuilder.Configurations.Add(new lu_signMap());
            modelBuilder.Configurations.Add(new lu_smokesMap());
            modelBuilder.Configurations.Add(new lu_sortbytypeMap());
            modelBuilder.Configurations.Add(new lu_wantskidsMap());
            modelBuilder.Configurations.Add(new mailboxfolderMap());
            modelBuilder.Configurations.Add(new mailboxfoldertypeMap());
            modelBuilder.Configurations.Add(new mailboxmessagefolderMap());
            modelBuilder.Configurations.Add(new mailboxmessageMap());
            modelBuilder.Configurations.Add(new mailupdatefreqencyMap());
            modelBuilder.Configurations.Add(new membersinroleMap());
            modelBuilder.Configurations.Add(new openidMap());
            modelBuilder.Configurations.Add(new peekMap());
            modelBuilder.Configurations.Add(new photo_securitylevelMap());
            modelBuilder.Configurations.Add(new photoalbum_securitylevelMap());
            modelBuilder.Configurations.Add(new photoalbumMap());
            modelBuilder.Configurations.Add(new photoconversionMap());
            modelBuilder.Configurations.Add(new photoreviewMap());
            modelBuilder.Configurations.Add(new photoMap());
            modelBuilder.Configurations.Add(new profileactivityMap());
            modelBuilder.Configurations.Add(new profileactivitygeodataMap());
            modelBuilder.Configurations.Add(new profiledata_ethnicityMap());
            modelBuilder.Configurations.Add(new profiledata_hobbyMap());
            modelBuilder.Configurations.Add(new profiledata_hotfeatureMap());
            modelBuilder.Configurations.Add(new profiledata_lookingforMap());
            modelBuilder.Configurations.Add(new profiledataMap());
            modelBuilder.Configurations.Add(new profilemetadataMap());
            modelBuilder.Configurations.Add(new profileMap());
            modelBuilder.Configurations.Add(new ratingMap());
            modelBuilder.Configurations.Add(new ratingvalueMap());
            modelBuilder.Configurations.Add(new searchsetting_bodytypeMap());
            modelBuilder.Configurations.Add(new searchsetting_dietMap());
            modelBuilder.Configurations.Add(new searchsetting_drinkMap());
            modelBuilder.Configurations.Add(new searchsetting_educationlevelMap());
            modelBuilder.Configurations.Add(new searchsetting_employmentstatusMap());
            modelBuilder.Configurations.Add(new searchsetting_ethnicityMap());
            modelBuilder.Configurations.Add(new searchsetting_exerciseMap());
            modelBuilder.Configurations.Add(new searchsetting_eyecolorMap());
            modelBuilder.Configurations.Add(new searchsetting_genderMap());
            modelBuilder.Configurations.Add(new searchsetting_haircolorMap());
            modelBuilder.Configurations.Add(new searchsetting_havekidsMap());
            modelBuilder.Configurations.Add(new searchsetting_hobbyMap());
            modelBuilder.Configurations.Add(new searchsetting_hotfeatureMap());
            modelBuilder.Configurations.Add(new searchsetting_humorMap());
            modelBuilder.Configurations.Add(new searchsetting_incomelevelMap());
            modelBuilder.Configurations.Add(new searchsetting_livingstituationMap());
            modelBuilder.Configurations.Add(new searchsetting_locationMap());
            modelBuilder.Configurations.Add(new searchsetting_lookingforMap());
            modelBuilder.Configurations.Add(new searchsetting_maritalstatusMap());
            modelBuilder.Configurations.Add(new searchsetting_politicalviewMap());
            modelBuilder.Configurations.Add(new searchsetting_professionMap());
            modelBuilder.Configurations.Add(new searchsetting_religionMap());
            modelBuilder.Configurations.Add(new searchsetting_religiousattendanceMap());
            modelBuilder.Configurations.Add(new searchsetting_showmeMap());
            modelBuilder.Configurations.Add(new searchsetting_signMap());
            modelBuilder.Configurations.Add(new searchsetting_smokesMap());
            modelBuilder.Configurations.Add(new searchsetting_sortbytypeMap());
            modelBuilder.Configurations.Add(new searchsetting_wantkidsMap());
            modelBuilder.Configurations.Add(new searchsettingMap());
            modelBuilder.Configurations.Add(new systempagesettingMap());
            modelBuilder.Configurations.Add(new userlogtimeMap());
            modelBuilder.Configurations.Add(new visiblitysettingMap());
            modelBuilder.Configurations.Add(new visiblitysettings_countryMap());
            modelBuilder.Configurations.Add(new visiblitysettings_genderMap());
             

        }
    }

    
}
