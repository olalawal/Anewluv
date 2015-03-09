namespace Anewluv.Domain.Migrations.AnewluvMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.actions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        creator_profile_id = c.Int(nullable: false),
                        target_profile_id = c.Int(nullable: false),
                        actiontype_id = c.Int(nullable: false),
                        creationdate = c.DateTime(),
                        viewdate = c.DateTime(),
                        modificationdate = c.DateTime(),
                        deletedbycreatordate = c.DateTime(),
                        deletedbytargetdate = c.DateTime(),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profilemetadatas", t => t.creator_profile_id)
                .ForeignKey("dbo.lu_actiontype", t => t.actiontype_id, cascadeDelete: true)
                .ForeignKey("dbo.profilemetadatas", t => t.target_profile_id)
                .Index(t => t.creator_profile_id)
                .Index(t => t.target_profile_id)
                .Index(t => t.actiontype_id);
            
            CreateTable(
                "dbo.profilemetadatas",
                c => new
                    {
                        profile_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.profile_id)
                .ForeignKey("dbo.profiles", t => t.profile_id)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.applications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        applicationtype_id = c.Int(nullable: false),
                        transfertype_id = c.Int(),
                        paymenttype_id = c.Int(nullable: false),
                        profile_id = c.Int(nullable: false),
                        purchaserprofile_id = c.Int(nullable: false),
                        creationdate = c.DateTime(nullable: false),
                        active = c.Boolean(nullable: false),
                        deactivationdate = c.DateTime(),
                        purchasedate = c.DateTime(nullable: false),
                        lasttransferdate = c.DateTime(),
                        activateddate = c.DateTime(),
                        expirationdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_applicationpaymenttype", t => t.paymenttype_id, cascadeDelete: true)
                .ForeignKey("dbo.lu_applicationtransfertype", t => t.applicationtype_id, cascadeDelete: true)
                .ForeignKey("dbo.lu_applicationtype", t => t.applicationtype_id, cascadeDelete: true)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id)
                .Index(t => t.applicationtype_id)
                .Index(t => t.paymenttype_id)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.applicationiconconversions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        application_id = c.Int(nullable: false),
                        creationdate = c.DateTime(),
                        image = c.Binary(),
                        size = c.Long(nullable: false),
                        iconformat_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.applications", t => t.application_id, cascadeDelete: true)
                .ForeignKey("dbo.lu_iconformat", t => t.iconformat_id)
                .Index(t => t.application_id)
                .Index(t => t.iconformat_id);
            
            CreateTable(
                "dbo.lu_iconformat",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        iconImagersizerformat_id = c.Int(nullable: false),
                        iconImageresizerformat_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_iconImagersizerformat", t => t.iconImageresizerformat_id)
                .Index(t => t.iconImageresizerformat_id);
            
            CreateTable(
                "dbo.lu_iconImagersizerformat",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.applicationroles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        active = c.Boolean(),
                        application_id = c.Int(nullable: false),
                        roleexpiredate = c.DateTime(),
                        rolestartdate = c.DateTime(),
                        deactivationdate = c.DateTime(),
                        creationdate = c.DateTime(),
                        role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.applications", t => t.application_id, cascadeDelete: true)
                .ForeignKey("dbo.lu_role", t => t.role_id, cascadeDelete: true)
                .Index(t => t.application_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.lu_role",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.membersinroles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        active = c.Boolean(),
                        profile_id = c.Int(nullable: false),
                        roleexpiredate = c.DateTime(),
                        rolestartdate = c.DateTime(),
                        role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_role", t => t.role_id, cascadeDelete: true)
                .ForeignKey("dbo.profiles", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.profiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        emailaddress = c.String(),
                        screenname = c.String(),
                        activationcode = c.String(),
                        dailsentmessagequota = c.Int(),
                        dailysentemailquota = c.Int(),
                        forwardmessages = c.Byte(),
                        logindate = c.DateTime(),
                        modificationdate = c.DateTime(),
                        creationdate = c.DateTime(),
                        readprivacystatement = c.Boolean(),
                        readtemsofuse = c.Boolean(),
                        password = c.String(),
                        apikey = c.Guid(nullable: false),
                        passwordChangeddate = c.DateTime(),
                        passwordchangecount = c.Int(),
                        failedpasswordchangedate = c.DateTime(),
                        failedpasswordchangeattemptcount = c.Int(),
                        salt = c.String(),
                        securityanswer = c.String(),
                        sentemailquotahitcount = c.Int(),
                        sentmessagequotahitcount = c.Int(),
                        status_id = c.Int(),
                        securityquestion_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_profilestatus", t => t.status_id)
                .ForeignKey("dbo.lu_securityquestion", t => t.securityquestion_id)
                .Index(t => t.status_id)
                .Index(t => t.securityquestion_id);
            
            CreateTable(
                "dbo.lu_profilestatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_securityquestion",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.openids",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        openididentifier = c.String(),
                        profile_id = c.Int(nullable: false),
                        openidprovider_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_openidprovider", t => t.openidprovider_id)
                .ForeignKey("dbo.profiles", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.openidprovider_id);
            
            CreateTable(
                "dbo.lu_openidprovider",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.profileactivities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        creationdate = c.DateTime(),
                        ipaddress = c.String(),
                        profile_id = c.Int(nullable: false),
                        sessionid = c.String(),
                        apikey = c.Guid(nullable: false),
                        useragent = c.String(),
                        routeurl = c.String(),
                        actionname = c.String(),
                        profileactivitygeodata_id = c.Int(),
                        activitytype_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_activitytype", t => t.activitytype_id)
                .ForeignKey("dbo.profiles", t => t.profile_id, cascadeDelete: true)
                .ForeignKey("dbo.profileactivitygeodatas", t => t.profileactivitygeodata_id)
                .Index(t => t.profile_id)
                .Index(t => t.profileactivitygeodata_id)
                .Index(t => t.activitytype_id);
            
            CreateTable(
                "dbo.lu_activitytype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.profileactivitygeodatas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        city = c.String(),
                        regionname = c.String(),
                        continent = c.String(),
                        countryId = c.Int(),
                        countrycode = c.String(),
                        countryname = c.String(),
                        creationdate = c.DateTime(),
                        lattitude = c.Double(),
                        longitude = c.Double(),
                        activity_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profileactivities", t => t.activity_id, cascadeDelete: true)
                .Index(t => t.activity_id);
            
            CreateTable(
                "dbo.profiledatas",
                c => new
                    {
                        profile_id = c.Int(nullable: false),
                        age = c.Int(),
                        birthdate = c.DateTime(),
                        city = c.String(),
                        countryregion = c.String(),
                        stateprovince = c.String(),
                        countryid = c.Int(),
                        longitude = c.Double(),
                        latitude = c.Double(),
                        aboutme = c.String(),
                        height = c.Long(),
                        mycatchyintroLine = c.String(),
                        phone = c.String(),
                        postalcode = c.String(),
                        profilemetadata_profile_id = c.Int(),
                        visibilitysettings_id = c.Int(),
                        gender_id = c.Int(),
                        bodytype_id = c.Int(),
                        eyecolor_id = c.Int(),
                        haircolor_id = c.Int(),
                        diet_id = c.Int(),
                        drinking_id = c.Int(),
                        exercise_id = c.Int(),
                        humor_id = c.Int(),
                        politicalview_id = c.Int(),
                        religion_id = c.Int(),
                        religiousattendance_id = c.Int(),
                        sign_id = c.Int(),
                        smoking_id = c.Int(),
                        educationlevel_id = c.Int(),
                        employmentstatus_id = c.Int(),
                        kidstatus_id = c.Int(),
                        incomelevel_id = c.Int(),
                        livingsituation_id = c.Int(),
                        maritalstatus_id = c.Int(),
                        profession_id = c.Int(),
                        wantsKidstatus_id = c.Int(),
                        visiblitysetting_id = c.Int(),
                    })
                .PrimaryKey(t => t.profile_id)
                .ForeignKey("dbo.lu_bodytype", t => t.bodytype_id)
                .ForeignKey("dbo.lu_diet", t => t.diet_id)
                .ForeignKey("dbo.lu_drinks", t => t.drinking_id)
                .ForeignKey("dbo.lu_educationlevel", t => t.educationlevel_id)
                .ForeignKey("dbo.lu_employmentstatus", t => t.employmentstatus_id)
                .ForeignKey("dbo.lu_exercise", t => t.exercise_id)
                .ForeignKey("dbo.lu_eyecolor", t => t.eyecolor_id)
                .ForeignKey("dbo.visiblitysettings", t => t.visiblitysetting_id)
                .ForeignKey("dbo.lu_gender", t => t.gender_id)
                .ForeignKey("dbo.lu_haircolor", t => t.haircolor_id)
                .ForeignKey("dbo.lu_havekids", t => t.kidstatus_id)
                .ForeignKey("dbo.lu_humor", t => t.humor_id)
                .ForeignKey("dbo.lu_incomelevel", t => t.incomelevel_id)
                .ForeignKey("dbo.lu_livingsituation", t => t.livingsituation_id)
                .ForeignKey("dbo.lu_maritalstatus", t => t.maritalstatus_id)
                .ForeignKey("dbo.lu_politicalview", t => t.politicalview_id)
                .ForeignKey("dbo.lu_profession", t => t.profession_id)
                .ForeignKey("dbo.lu_religion", t => t.religion_id)
                .ForeignKey("dbo.lu_religiousattendance", t => t.religiousattendance_id)
                .ForeignKey("dbo.lu_sign", t => t.sign_id)
                .ForeignKey("dbo.lu_smokes", t => t.smoking_id)
                .ForeignKey("dbo.lu_wantskids", t => t.wantsKidstatus_id)
                .ForeignKey("dbo.profiles", t => t.profile_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id)
                .Index(t => t.profile_id)
                .Index(t => t.gender_id)
                .Index(t => t.bodytype_id)
                .Index(t => t.eyecolor_id)
                .Index(t => t.haircolor_id)
                .Index(t => t.diet_id)
                .Index(t => t.drinking_id)
                .Index(t => t.exercise_id)
                .Index(t => t.humor_id)
                .Index(t => t.politicalview_id)
                .Index(t => t.religion_id)
                .Index(t => t.religiousattendance_id)
                .Index(t => t.sign_id)
                .Index(t => t.smoking_id)
                .Index(t => t.educationlevel_id)
                .Index(t => t.employmentstatus_id)
                .Index(t => t.kidstatus_id)
                .Index(t => t.incomelevel_id)
                .Index(t => t.livingsituation_id)
                .Index(t => t.maritalstatus_id)
                .Index(t => t.profession_id)
                .Index(t => t.wantsKidstatus_id)
                .Index(t => t.visiblitysetting_id);
            
            CreateTable(
                "dbo.communicationquotas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        active = c.Boolean(),
                        quotadescription = c.String(),
                        quotaname = c.String(),
                        quotaroleid = c.Int(),
                        quotavalue = c.Int(),
                        updaterprofile_id = c.String(),
                        updatedate = c.DateTime(),
                        updaterprofiledata_profile_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profiledatas", t => t.updaterprofiledata_profile_id, cascadeDelete: true)
                .Index(t => t.updaterprofiledata_profile_id);
            
            CreateTable(
                "dbo.lu_bodytype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_diet",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_drinks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_educationlevel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_employmentstatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_exercise",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_eyecolor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_gender",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.visiblitysettings_gender",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        visiblitysetting_id = c.Int(nullable: false),
                        gender_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_gender", t => t.gender_id)
                .ForeignKey("dbo.visiblitysettings", t => t.visiblitysetting_id, cascadeDelete: true)
                .Index(t => t.visiblitysetting_id)
                .Index(t => t.gender_id);
            
            CreateTable(
                "dbo.visiblitysettings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile_id = c.Int(nullable: false),
                        agemaxvisibility = c.Int(),
                        ageminvisibility = c.Int(),
                        chatvisiblitytointerests = c.Boolean(),
                        chatvisiblitytolikes = c.Boolean(),
                        chatvisiblitytomatches = c.Boolean(),
                        chatvisiblitytopeeks = c.Boolean(),
                        chatvisiblitytosearch = c.Boolean(),
                        lastupdatedate = c.DateTime(),
                        mailchatrequest = c.Boolean(),
                        mailintrests = c.Boolean(),
                        maillikes = c.Boolean(),
                        mailmatches = c.Boolean(),
                        mailnews = c.Boolean(),
                        mailpeeks = c.Boolean(),
                        profilevisiblity = c.Boolean(),
                        saveofflinechat = c.Boolean(),
                        steathpeeks = c.Boolean(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profiledatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.visiblitysettings_country",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        countryId = c.String(),
                        countryname = c.String(),
                        visiblitysetting_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.visiblitysettings", t => t.visiblitysetting_id, cascadeDelete: true)
                .Index(t => t.visiblitysetting_id);
            
            CreateTable(
                "dbo.lu_haircolor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_havekids",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_humor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_incomelevel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_livingsituation",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_maritalstatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_politicalview",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_profession",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_religion",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_religiousattendance",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_sign",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        month = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_smokes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_wantskids",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.userlogtimes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        logintime = c.DateTime(),
                        logouttime = c.DateTime(),
                        offline = c.Boolean(),
                        profile_id = c.Int(nullable: false),
                        sessionid = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profiles", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.lu_applicationpaymenttype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_applicationtransfertype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_applicationtype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.mailboxfolders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile_id = c.Int(nullable: false),
                        active = c.Int(),
                        foldertype_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.mailboxfoldertypes", t => t.foldertype_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.foldertype_id);
            
            CreateTable(
                "dbo.mailboxfoldertypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        active = c.Boolean(),
                        creationdate = c.DateTime(),
                        deleteddate = c.DateTime(),
                        maxsize = c.Int(),
                        defaultfolder_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_defaultmailboxfolder", t => t.defaultfolder_id)
                .Index(t => t.defaultfolder_id);
            
            CreateTable(
                "dbo.lu_defaultmailboxfolder",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.mailboxmessagefolders",
                c => new
                    {
                        mailboxfolder_id = c.Int(nullable: false),
                        mailboxmessage_id = c.Int(nullable: false),
                        deleteddate = c.DateTime(),
                        draftdate = c.DateTime(),
                        flaggeddate = c.DateTime(),
                        readdate = c.DateTime(),
                        recent = c.Boolean(),
                        replieddate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.mailboxfolder_id, t.mailboxmessage_id })
                .ForeignKey("dbo.mailboxfolders", t => t.mailboxfolder_id, cascadeDelete: true)
                .ForeignKey("dbo.mailboxmessages", t => t.mailboxmessage_id, cascadeDelete: true)
                .Index(t => t.mailboxfolder_id)
                .Index(t => t.mailboxmessage_id);
            
            CreateTable(
                "dbo.mailboxmessages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        creationdate = c.DateTime(),
                        recipient_id = c.Int(nullable: false),
                        sender_id = c.Int(nullable: false),
                        body = c.String(),
                        subject = c.String(),
                        uniqueid = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profilemetadatas", t => t.recipient_id)
                .ForeignKey("dbo.profilemetadatas", t => t.sender_id)
                .Index(t => t.recipient_id)
                .Index(t => t.sender_id);
            
            CreateTable(
                "dbo.mailupdatefreqencies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        updatefreqency = c.Int(),
                        profile_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.photoalbums",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        profile_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.photoalbum_securitylevel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photoalbum_id = c.Int(nullable: false),
                        securityleveltype_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_securityleveltype", t => t.securityleveltype_id)
                .ForeignKey("dbo.photoalbums", t => t.photoalbum_id, cascadeDelete: true)
                .Index(t => t.photoalbum_id)
                .Index(t => t.securityleveltype_id);
            
            CreateTable(
                "dbo.lu_securityleveltype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.photo_securitylevel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo_id = c.Guid(nullable: false),
                        securityleveltype_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_securityleveltype", t => t.securityleveltype_id)
                .ForeignKey("dbo.photos", t => t.photo_id, cascadeDelete: true)
                .Index(t => t.photo_id)
                .Index(t => t.securityleveltype_id);
            
            CreateTable(
                "dbo.photos",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        size = c.Long(nullable: false),
                        profile_id = c.Int(nullable: false),
                        creationdate = c.DateTime(),
                        imagecaption = c.String(),
                        imagename = c.String(),
                        providername = c.String(),
                        rejectionreason_id = c.Int(),
                        photostatus_id = c.Int(),
                        approvalstatus_id = c.Int(),
                        imagetype_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_photoapprovalstatus", t => t.approvalstatus_id)
                .ForeignKey("dbo.lu_photoimagetype", t => t.imagetype_id)
                .ForeignKey("dbo.lu_photorejectionreason", t => t.rejectionreason_id)
                .ForeignKey("dbo.lu_photostatus", t => t.photostatus_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.rejectionreason_id)
                .Index(t => t.photostatus_id)
                .Index(t => t.approvalstatus_id)
                .Index(t => t.imagetype_id);
            
            CreateTable(
                "dbo.lu_photoapprovalstatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_photoimagetype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_photorejectionreason",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        userMessage = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_photostatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_photostatusdescription",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        photostatus_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_photostatus", t => t.photostatus_id)
                .Index(t => t.photostatus_id);
            
            CreateTable(
                "dbo.photoconversions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo_id = c.Guid(nullable: false),
                        creationdate = c.DateTime(),
                        description = c.String(),
                        image = c.Binary(),
                        size = c.Long(nullable: false),
                        formattype_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_photoformat", t => t.formattype_id, cascadeDelete: true)
                .ForeignKey("dbo.photos", t => t.photo_id, cascadeDelete: true)
                .Index(t => t.photo_id)
                .Index(t => t.formattype_id);
            
            CreateTable(
                "dbo.lu_photoformat",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        photoImagersizerformat_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_photoImagersizerformat", t => t.photoImagersizerformat_id, cascadeDelete: true)
                .Index(t => t.photoImagersizerformat_id);
            
            CreateTable(
                "dbo.lu_photoImagersizerformat",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.photophotoalbum",
                c => new
                    {
                        photophotoalbumid = c.Int(nullable: false, identity: true),
                        photo_id = c.Guid(nullable: false),
                        photoalbum_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.photophotoalbumid)
                .ForeignKey("dbo.photos", t => t.photo_id)
                .ForeignKey("dbo.photoalbums", t => t.photoalbum_id)
                .Index(t => t.photo_id)
                .Index(t => t.photoalbum_id);
            
            CreateTable(
                "dbo.photoreviews",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        notes = c.String(),
                        creationdate = c.DateTime(),
                        reviewerprofile_id = c.Int(nullable: false),
                        photo_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.photos", t => t.photo_id, cascadeDelete: true)
                .ForeignKey("dbo.profilemetadatas", t => t.reviewerprofile_id)
                .Index(t => t.reviewerprofile_id)
                .Index(t => t.photo_id);
            
            CreateTable(
                "dbo.profiledata_ethnicity",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile_id = c.Int(nullable: false),
                        ethnicty_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_ethnicity", t => t.ethnicty_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.ethnicty_id);
            
            CreateTable(
                "dbo.lu_ethnicity",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.profiledata_hobby",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile_id = c.Int(nullable: false),
                        hobby_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_hobby", t => t.hobby_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.hobby_id);
            
            CreateTable(
                "dbo.lu_hobby",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.profiledata_hotfeature",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile_id = c.Int(nullable: false),
                        hotfeature_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_hotfeature", t => t.hotfeature_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.hotfeature_id);
            
            CreateTable(
                "dbo.lu_hotfeature",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.profiledata_lookingfor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile_id = c.Int(nullable: false),
                        lookingfor_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_lookingfor", t => t.lookingfor_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.lookingfor_id);
            
            CreateTable(
                "dbo.lu_lookingfor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ratingvalues",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rating_id = c.Int(nullable: false),
                        profile_id = c.Int(nullable: false),
                        rateeprofile_id = c.Int(nullable: false),
                        date = c.DateTime(),
                        value = c.Double(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ratings", t => t.rating_id, cascadeDelete: true)
                .ForeignKey("dbo.profilemetadatas", t => t.rateeprofile_id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id)
                .Index(t => t.rating_id)
                .Index(t => t.profile_id)
                .Index(t => t.rateeprofile_id);
            
            CreateTable(
                "dbo.ratings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        ratingmaxvalue = c.Int(),
                        ratingweight = c.Int(),
                        increment = c.Long(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.searchsettings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile_id = c.Int(nullable: false),
                        agemax = c.Int(),
                        agemin = c.Int(),
                        creationdate = c.DateTime(),
                        distancefromme = c.Int(),
                        heightmax = c.Int(),
                        heightmin = c.Int(),
                        lastupdatedate = c.DateTime(),
                        myperfectmatch = c.Boolean(),
                        savedsearch = c.Boolean(),
                        searchname = c.String(),
                        searchrank = c.Int(),
                        systemmatch = c.Boolean(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profilemetadatas", t => t.profile_id, cascadeDelete: true)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.searchsettingdetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        searchsetting_id = c.Int(nullable: false),
                        searchsettingdetailtype_id = c.Int(nullable: false),
                        value = c.Int(nullable: false),
                        creationdate = c.DateTime(),
                        modificationdate = c.DateTime(),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_searchsettingdetailtype", t => t.searchsettingdetailtype_id, cascadeDelete: true)
                .ForeignKey("dbo.searchsettings", t => t.searchsetting_id, cascadeDelete: true)
                .Index(t => t.searchsetting_id)
                .Index(t => t.searchsettingdetailtype_id);
            
            CreateTable(
                "dbo.lu_searchsettingdetailtype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.searchsetting_location",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        city = c.String(),
                        countryid = c.Int(),
                        postalcode = c.String(),
                        searchsetting_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.searchsettings", t => t.searchsetting_id, cascadeDelete: true)
                .Index(t => t.searchsetting_id);
            
            CreateTable(
                "dbo.lu_actiontype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.notes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        notetype_id = c.Int(nullable: false),
                        action_id = c.Int(nullable: false),
                        abusetype_id = c.Int(),
                        notedetail = c.String(),
                        creationdate = c.DateTime(),
                        reviewdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.lu_abusetype", t => t.abusetype_id)
                .ForeignKey("dbo.lu_notetype", t => t.notetype_id, cascadeDelete: true)
                .ForeignKey("dbo.actions", t => t.action_id, cascadeDelete: true)
                .Index(t => t.notetype_id)
                .Index(t => t.action_id)
                .Index(t => t.abusetype_id);
            
            CreateTable(
                "dbo.lu_abusetype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_notetype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_flagyesno",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_height",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_profilefiltertype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_showme",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lu_sortbytype",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.systempagesettings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        bodycssstylename = c.String(),
                        description = c.String(),
                        hitCount = c.Int(),
                        ismasterpage = c.Boolean(),
                        path = c.String(),
                        title = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.actions", "target_profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.notes", "action_id", "dbo.actions");
            DropForeignKey("dbo.notes", "notetype_id", "dbo.lu_notetype");
            DropForeignKey("dbo.notes", "abusetype_id", "dbo.lu_abusetype");
            DropForeignKey("dbo.actions", "actiontype_id", "dbo.lu_actiontype");
            DropForeignKey("dbo.actions", "creator_profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.searchsettings", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.searchsetting_location", "searchsetting_id", "dbo.searchsettings");
            DropForeignKey("dbo.searchsettingdetails", "searchsetting_id", "dbo.searchsettings");
            DropForeignKey("dbo.searchsettingdetails", "searchsettingdetailtype_id", "dbo.lu_searchsettingdetailtype");
            DropForeignKey("dbo.ratingvalues", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.ratingvalues", "rateeprofile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.ratingvalues", "rating_id", "dbo.ratings");
            DropForeignKey("dbo.profiledata_lookingfor", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.profiledata_lookingfor", "lookingfor_id", "dbo.lu_lookingfor");
            DropForeignKey("dbo.profiledata_hotfeature", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.profiledata_hotfeature", "hotfeature_id", "dbo.lu_hotfeature");
            DropForeignKey("dbo.profiledata_hobby", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.profiledata_hobby", "hobby_id", "dbo.lu_hobby");
            DropForeignKey("dbo.profiledata_ethnicity", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.profiledata_ethnicity", "ethnicty_id", "dbo.lu_ethnicity");
            DropForeignKey("dbo.profilemetadatas", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.photoalbums", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.photoalbum_securitylevel", "photoalbum_id", "dbo.photoalbums");
            DropForeignKey("dbo.photoalbum_securitylevel", "securityleveltype_id", "dbo.lu_securityleveltype");
            DropForeignKey("dbo.photo_securitylevel", "photo_id", "dbo.photos");
            DropForeignKey("dbo.photos", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.photoreviews", "reviewerprofile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.photoreviews", "photo_id", "dbo.photos");
            DropForeignKey("dbo.photophotoalbum", "photoalbum_id", "dbo.photoalbums");
            DropForeignKey("dbo.photophotoalbum", "photo_id", "dbo.photos");
            DropForeignKey("dbo.photoconversions", "photo_id", "dbo.photos");
            DropForeignKey("dbo.photoconversions", "formattype_id", "dbo.lu_photoformat");
            DropForeignKey("dbo.lu_photoformat", "photoImagersizerformat_id", "dbo.lu_photoImagersizerformat");
            DropForeignKey("dbo.photos", "photostatus_id", "dbo.lu_photostatus");
            DropForeignKey("dbo.lu_photostatusdescription", "photostatus_id", "dbo.lu_photostatus");
            DropForeignKey("dbo.photos", "rejectionreason_id", "dbo.lu_photorejectionreason");
            DropForeignKey("dbo.photos", "imagetype_id", "dbo.lu_photoimagetype");
            DropForeignKey("dbo.photos", "approvalstatus_id", "dbo.lu_photoapprovalstatus");
            DropForeignKey("dbo.photo_securitylevel", "securityleveltype_id", "dbo.lu_securityleveltype");
            DropForeignKey("dbo.mailupdatefreqencies", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.mailboxfolders", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.mailboxmessagefolders", "mailboxmessage_id", "dbo.mailboxmessages");
            DropForeignKey("dbo.mailboxmessages", "sender_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.mailboxmessages", "recipient_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.mailboxmessagefolders", "mailboxfolder_id", "dbo.mailboxfolders");
            DropForeignKey("dbo.mailboxfolders", "foldertype_id", "dbo.mailboxfoldertypes");
            DropForeignKey("dbo.mailboxfoldertypes", "defaultfolder_id", "dbo.lu_defaultmailboxfolder");
            DropForeignKey("dbo.applications", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.applications", "applicationtype_id", "dbo.lu_applicationtype");
            DropForeignKey("dbo.applications", "applicationtype_id", "dbo.lu_applicationtransfertype");
            DropForeignKey("dbo.applications", "paymenttype_id", "dbo.lu_applicationpaymenttype");
            DropForeignKey("dbo.applicationroles", "role_id", "dbo.lu_role");
            DropForeignKey("dbo.membersinroles", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.userlogtimes", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.visiblitysettings", "profile_id", "dbo.profiledatas");
            DropForeignKey("dbo.profiledatas", "profile_id", "dbo.profilemetadatas");
            DropForeignKey("dbo.profiledatas", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.profiledatas", "wantsKidstatus_id", "dbo.lu_wantskids");
            DropForeignKey("dbo.profiledatas", "smoking_id", "dbo.lu_smokes");
            DropForeignKey("dbo.profiledatas", "sign_id", "dbo.lu_sign");
            DropForeignKey("dbo.profiledatas", "religiousattendance_id", "dbo.lu_religiousattendance");
            DropForeignKey("dbo.profiledatas", "religion_id", "dbo.lu_religion");
            DropForeignKey("dbo.profiledatas", "profession_id", "dbo.lu_profession");
            DropForeignKey("dbo.profiledatas", "politicalview_id", "dbo.lu_politicalview");
            DropForeignKey("dbo.profiledatas", "maritalstatus_id", "dbo.lu_maritalstatus");
            DropForeignKey("dbo.profiledatas", "livingsituation_id", "dbo.lu_livingsituation");
            DropForeignKey("dbo.profiledatas", "incomelevel_id", "dbo.lu_incomelevel");
            DropForeignKey("dbo.profiledatas", "humor_id", "dbo.lu_humor");
            DropForeignKey("dbo.profiledatas", "kidstatus_id", "dbo.lu_havekids");
            DropForeignKey("dbo.profiledatas", "haircolor_id", "dbo.lu_haircolor");
            DropForeignKey("dbo.profiledatas", "gender_id", "dbo.lu_gender");
            DropForeignKey("dbo.visiblitysettings_gender", "visiblitysetting_id", "dbo.visiblitysettings");
            DropForeignKey("dbo.visiblitysettings_country", "visiblitysetting_id", "dbo.visiblitysettings");
            DropForeignKey("dbo.profiledatas", "visiblitysetting_id", "dbo.visiblitysettings");
            DropForeignKey("dbo.visiblitysettings_gender", "gender_id", "dbo.lu_gender");
            DropForeignKey("dbo.profiledatas", "eyecolor_id", "dbo.lu_eyecolor");
            DropForeignKey("dbo.profiledatas", "exercise_id", "dbo.lu_exercise");
            DropForeignKey("dbo.profiledatas", "employmentstatus_id", "dbo.lu_employmentstatus");
            DropForeignKey("dbo.profiledatas", "educationlevel_id", "dbo.lu_educationlevel");
            DropForeignKey("dbo.profiledatas", "drinking_id", "dbo.lu_drinks");
            DropForeignKey("dbo.profiledatas", "diet_id", "dbo.lu_diet");
            DropForeignKey("dbo.profiledatas", "bodytype_id", "dbo.lu_bodytype");
            DropForeignKey("dbo.communicationquotas", "updaterprofiledata_profile_id", "dbo.profiledatas");
            DropForeignKey("dbo.profileactivities", "profileactivitygeodata_id", "dbo.profileactivitygeodatas");
            DropForeignKey("dbo.profileactivitygeodatas", "activity_id", "dbo.profileactivities");
            DropForeignKey("dbo.profileactivities", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.profileactivities", "activitytype_id", "dbo.lu_activitytype");
            DropForeignKey("dbo.openids", "profile_id", "dbo.profiles");
            DropForeignKey("dbo.openids", "openidprovider_id", "dbo.lu_openidprovider");
            DropForeignKey("dbo.profiles", "securityquestion_id", "dbo.lu_securityquestion");
            DropForeignKey("dbo.profiles", "status_id", "dbo.lu_profilestatus");
            DropForeignKey("dbo.membersinroles", "role_id", "dbo.lu_role");
            DropForeignKey("dbo.applicationroles", "application_id", "dbo.applications");
            DropForeignKey("dbo.applicationiconconversions", "iconformat_id", "dbo.lu_iconformat");
            DropForeignKey("dbo.lu_iconformat", "iconImageresizerformat_id", "dbo.lu_iconImagersizerformat");
            DropForeignKey("dbo.applicationiconconversions", "application_id", "dbo.applications");
            DropIndex("dbo.notes", new[] { "abusetype_id" });
            DropIndex("dbo.notes", new[] { "action_id" });
            DropIndex("dbo.notes", new[] { "notetype_id" });
            DropIndex("dbo.searchsetting_location", new[] { "searchsetting_id" });
            DropIndex("dbo.searchsettingdetails", new[] { "searchsettingdetailtype_id" });
            DropIndex("dbo.searchsettingdetails", new[] { "searchsetting_id" });
            DropIndex("dbo.searchsettings", new[] { "profile_id" });
            DropIndex("dbo.ratingvalues", new[] { "rateeprofile_id" });
            DropIndex("dbo.ratingvalues", new[] { "profile_id" });
            DropIndex("dbo.ratingvalues", new[] { "rating_id" });
            DropIndex("dbo.profiledata_lookingfor", new[] { "lookingfor_id" });
            DropIndex("dbo.profiledata_lookingfor", new[] { "profile_id" });
            DropIndex("dbo.profiledata_hotfeature", new[] { "hotfeature_id" });
            DropIndex("dbo.profiledata_hotfeature", new[] { "profile_id" });
            DropIndex("dbo.profiledata_hobby", new[] { "hobby_id" });
            DropIndex("dbo.profiledata_hobby", new[] { "profile_id" });
            DropIndex("dbo.profiledata_ethnicity", new[] { "ethnicty_id" });
            DropIndex("dbo.profiledata_ethnicity", new[] { "profile_id" });
            DropIndex("dbo.photoreviews", new[] { "photo_id" });
            DropIndex("dbo.photoreviews", new[] { "reviewerprofile_id" });
            DropIndex("dbo.photophotoalbum", new[] { "photoalbum_id" });
            DropIndex("dbo.photophotoalbum", new[] { "photo_id" });
            DropIndex("dbo.lu_photoformat", new[] { "photoImagersizerformat_id" });
            DropIndex("dbo.photoconversions", new[] { "formattype_id" });
            DropIndex("dbo.photoconversions", new[] { "photo_id" });
            DropIndex("dbo.lu_photostatusdescription", new[] { "photostatus_id" });
            DropIndex("dbo.photos", new[] { "imagetype_id" });
            DropIndex("dbo.photos", new[] { "approvalstatus_id" });
            DropIndex("dbo.photos", new[] { "photostatus_id" });
            DropIndex("dbo.photos", new[] { "rejectionreason_id" });
            DropIndex("dbo.photos", new[] { "profile_id" });
            DropIndex("dbo.photo_securitylevel", new[] { "securityleveltype_id" });
            DropIndex("dbo.photo_securitylevel", new[] { "photo_id" });
            DropIndex("dbo.photoalbum_securitylevel", new[] { "securityleveltype_id" });
            DropIndex("dbo.photoalbum_securitylevel", new[] { "photoalbum_id" });
            DropIndex("dbo.photoalbums", new[] { "profile_id" });
            DropIndex("dbo.mailupdatefreqencies", new[] { "profile_id" });
            DropIndex("dbo.mailboxmessages", new[] { "sender_id" });
            DropIndex("dbo.mailboxmessages", new[] { "recipient_id" });
            DropIndex("dbo.mailboxmessagefolders", new[] { "mailboxmessage_id" });
            DropIndex("dbo.mailboxmessagefolders", new[] { "mailboxfolder_id" });
            DropIndex("dbo.mailboxfoldertypes", new[] { "defaultfolder_id" });
            DropIndex("dbo.mailboxfolders", new[] { "foldertype_id" });
            DropIndex("dbo.mailboxfolders", new[] { "profile_id" });
            DropIndex("dbo.userlogtimes", new[] { "profile_id" });
            DropIndex("dbo.visiblitysettings_country", new[] { "visiblitysetting_id" });
            DropIndex("dbo.visiblitysettings", new[] { "profile_id" });
            DropIndex("dbo.visiblitysettings_gender", new[] { "gender_id" });
            DropIndex("dbo.visiblitysettings_gender", new[] { "visiblitysetting_id" });
            DropIndex("dbo.communicationquotas", new[] { "updaterprofiledata_profile_id" });
            DropIndex("dbo.profiledatas", new[] { "visiblitysetting_id" });
            DropIndex("dbo.profiledatas", new[] { "wantsKidstatus_id" });
            DropIndex("dbo.profiledatas", new[] { "profession_id" });
            DropIndex("dbo.profiledatas", new[] { "maritalstatus_id" });
            DropIndex("dbo.profiledatas", new[] { "livingsituation_id" });
            DropIndex("dbo.profiledatas", new[] { "incomelevel_id" });
            DropIndex("dbo.profiledatas", new[] { "kidstatus_id" });
            DropIndex("dbo.profiledatas", new[] { "employmentstatus_id" });
            DropIndex("dbo.profiledatas", new[] { "educationlevel_id" });
            DropIndex("dbo.profiledatas", new[] { "smoking_id" });
            DropIndex("dbo.profiledatas", new[] { "sign_id" });
            DropIndex("dbo.profiledatas", new[] { "religiousattendance_id" });
            DropIndex("dbo.profiledatas", new[] { "religion_id" });
            DropIndex("dbo.profiledatas", new[] { "politicalview_id" });
            DropIndex("dbo.profiledatas", new[] { "humor_id" });
            DropIndex("dbo.profiledatas", new[] { "exercise_id" });
            DropIndex("dbo.profiledatas", new[] { "drinking_id" });
            DropIndex("dbo.profiledatas", new[] { "diet_id" });
            DropIndex("dbo.profiledatas", new[] { "haircolor_id" });
            DropIndex("dbo.profiledatas", new[] { "eyecolor_id" });
            DropIndex("dbo.profiledatas", new[] { "bodytype_id" });
            DropIndex("dbo.profiledatas", new[] { "gender_id" });
            DropIndex("dbo.profiledatas", new[] { "profile_id" });
            DropIndex("dbo.profileactivitygeodatas", new[] { "activity_id" });
            DropIndex("dbo.profileactivities", new[] { "activitytype_id" });
            DropIndex("dbo.profileactivities", new[] { "profileactivitygeodata_id" });
            DropIndex("dbo.profileactivities", new[] { "profile_id" });
            DropIndex("dbo.openids", new[] { "openidprovider_id" });
            DropIndex("dbo.openids", new[] { "profile_id" });
            DropIndex("dbo.profiles", new[] { "securityquestion_id" });
            DropIndex("dbo.profiles", new[] { "status_id" });
            DropIndex("dbo.membersinroles", new[] { "role_id" });
            DropIndex("dbo.membersinroles", new[] { "profile_id" });
            DropIndex("dbo.applicationroles", new[] { "role_id" });
            DropIndex("dbo.applicationroles", new[] { "application_id" });
            DropIndex("dbo.lu_iconformat", new[] { "iconImageresizerformat_id" });
            DropIndex("dbo.applicationiconconversions", new[] { "iconformat_id" });
            DropIndex("dbo.applicationiconconversions", new[] { "application_id" });
            DropIndex("dbo.applications", new[] { "profile_id" });
            DropIndex("dbo.applications", new[] { "paymenttype_id" });
            DropIndex("dbo.applications", new[] { "applicationtype_id" });
            DropIndex("dbo.profilemetadatas", new[] { "profile_id" });
            DropIndex("dbo.actions", new[] { "actiontype_id" });
            DropIndex("dbo.actions", new[] { "target_profile_id" });
            DropIndex("dbo.actions", new[] { "creator_profile_id" });
            DropTable("dbo.systempagesettings");
            DropTable("dbo.lu_sortbytype");
            DropTable("dbo.lu_showme");
            DropTable("dbo.lu_profilefiltertype");
            DropTable("dbo.lu_height");
            DropTable("dbo.lu_flagyesno");
            DropTable("dbo.lu_notetype");
            DropTable("dbo.lu_abusetype");
            DropTable("dbo.notes");
            DropTable("dbo.lu_actiontype");
            DropTable("dbo.searchsetting_location");
            DropTable("dbo.lu_searchsettingdetailtype");
            DropTable("dbo.searchsettingdetails");
            DropTable("dbo.searchsettings");
            DropTable("dbo.ratings");
            DropTable("dbo.ratingvalues");
            DropTable("dbo.lu_lookingfor");
            DropTable("dbo.profiledata_lookingfor");
            DropTable("dbo.lu_hotfeature");
            DropTable("dbo.profiledata_hotfeature");
            DropTable("dbo.lu_hobby");
            DropTable("dbo.profiledata_hobby");
            DropTable("dbo.lu_ethnicity");
            DropTable("dbo.profiledata_ethnicity");
            DropTable("dbo.photoreviews");
            DropTable("dbo.photophotoalbum");
            DropTable("dbo.lu_photoImagersizerformat");
            DropTable("dbo.lu_photoformat");
            DropTable("dbo.photoconversions");
            DropTable("dbo.lu_photostatusdescription");
            DropTable("dbo.lu_photostatus");
            DropTable("dbo.lu_photorejectionreason");
            DropTable("dbo.lu_photoimagetype");
            DropTable("dbo.lu_photoapprovalstatus");
            DropTable("dbo.photos");
            DropTable("dbo.photo_securitylevel");
            DropTable("dbo.lu_securityleveltype");
            DropTable("dbo.photoalbum_securitylevel");
            DropTable("dbo.photoalbums");
            DropTable("dbo.mailupdatefreqencies");
            DropTable("dbo.mailboxmessages");
            DropTable("dbo.mailboxmessagefolders");
            DropTable("dbo.lu_defaultmailboxfolder");
            DropTable("dbo.mailboxfoldertypes");
            DropTable("dbo.mailboxfolders");
            DropTable("dbo.lu_applicationtype");
            DropTable("dbo.lu_applicationtransfertype");
            DropTable("dbo.lu_applicationpaymenttype");
            DropTable("dbo.userlogtimes");
            DropTable("dbo.lu_wantskids");
            DropTable("dbo.lu_smokes");
            DropTable("dbo.lu_sign");
            DropTable("dbo.lu_religiousattendance");
            DropTable("dbo.lu_religion");
            DropTable("dbo.lu_profession");
            DropTable("dbo.lu_politicalview");
            DropTable("dbo.lu_maritalstatus");
            DropTable("dbo.lu_livingsituation");
            DropTable("dbo.lu_incomelevel");
            DropTable("dbo.lu_humor");
            DropTable("dbo.lu_havekids");
            DropTable("dbo.lu_haircolor");
            DropTable("dbo.visiblitysettings_country");
            DropTable("dbo.visiblitysettings");
            DropTable("dbo.visiblitysettings_gender");
            DropTable("dbo.lu_gender");
            DropTable("dbo.lu_eyecolor");
            DropTable("dbo.lu_exercise");
            DropTable("dbo.lu_employmentstatus");
            DropTable("dbo.lu_educationlevel");
            DropTable("dbo.lu_drinks");
            DropTable("dbo.lu_diet");
            DropTable("dbo.lu_bodytype");
            DropTable("dbo.communicationquotas");
            DropTable("dbo.profiledatas");
            DropTable("dbo.profileactivitygeodatas");
            DropTable("dbo.lu_activitytype");
            DropTable("dbo.profileactivities");
            DropTable("dbo.lu_openidprovider");
            DropTable("dbo.openids");
            DropTable("dbo.lu_securityquestion");
            DropTable("dbo.lu_profilestatus");
            DropTable("dbo.profiles");
            DropTable("dbo.membersinroles");
            DropTable("dbo.lu_role");
            DropTable("dbo.applicationroles");
            DropTable("dbo.lu_iconImagersizerformat");
            DropTable("dbo.lu_iconformat");
            DropTable("dbo.applicationiconconversions");
            DropTable("dbo.applications");
            DropTable("dbo.profilemetadatas");
            DropTable("dbo.actions");
        }
    }
}
