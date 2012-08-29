
//var accountNumbers = new Array(); //list of account numbers passed when retreiving assignments
//var accountCount = 0;
//var groups = { System: new Array(), User: new Array()} //list of testcode groups split into system and user buckets
//var lastSel = -1;
//var loadedAssignments = new Array(); //list of the assignments brought back from the handler
//var unavailableTestCodes = new Array(); //list of user/testcodes for determining which test codes are available
//var groupAddTestCodes = new Array(); //list of test codes to be added to a group
//var isAddingGroup = false;

//Use this to possible get the current DOM item
(function($){

    var imageGetUrl = "../Images/GetGalleryImageByScreenName/"
    var currentdocument = null;
//regular way
//(function($){ 2   tile = function() { 3     $("body").append('See? It works.'); 4   }; 5 })(jQuery); 


 deletelistitemsconfirm = function (message, title, okbuttontext, ismodal, showimage, items, imageUrl) {

debugger;
    var buttonsOpts = {}

    //if we have a confirm delete put on both buttons 
    if (okbuttontext == "Confirm") {
        buttonsOpts[okbuttontext] = function () { updateinterests(items); $(this).dialog('close'); };
        buttonsOpts["Cancel"] = function () { $(this).dialog("close"); };
    }
    //we have just an alert type dilago so just use the ok button with no closer
    else {
        buttonsOpts[okbuttontext] = function () { $(this).dialog("close"); };
    }

    //set up inner text
    //4-3-2012 added a way to also add image div based on the flag
    var NewDialog = $(document.createElement('div'));
    NewDialog.html(message);


    //add the image and div tag to the Dialog
    if (showimage == true) {
        //get image URL
        imageURL =  (ImageUrl)? imageURL : imageGetUrl + item[0]     
        NewDialog.append(createImageBox(35, 35,imageUrl));
    }

    //add an ID if it it was passed
    //        if (dialogid) {
    //            NewDialog.attr("id", dialogid);

    //        }
    if (!ismodal) ismodal = false;

    NewDialog.dialog({
        modal: ismodal,
        title: (title) ? title : 'Notification',
        autoOpen: true,
        width: 300,
        show: 'clip',
        hide: 'clip',
        buttons: buttonsOpts
    });

    // NewDialog.appendTo($("body"));
    //TO DO another way to add hml to dialog 
    //$('#dialog-test').html('<div> weak </div>');
    NewDialog.dialog('open');
     

};

/// creates an image box and varaying hight and width and returns the tag
 function createImageBox(height,width,imageURL)
    {
      var imageDiv = $(document.createElement('div'));
        var imageTag = $(document.createElement('img'));
        //no need for a n id for image div but give it some css
        imageDiv.attr('class', 'left');
        //set the source for the image
        imageTag.attr('src',imageURL);
        imageTag.attr('width', width);
        imageTag.attr('height', height);
        //addend the image tag and maybe more to this
        imageDiv.append(imageTag);

        return imageDiv;
    
    }

})(jQuery); 