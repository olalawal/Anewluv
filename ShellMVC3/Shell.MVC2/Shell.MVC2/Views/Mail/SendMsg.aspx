<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Members/Shared/MembersWhite.Master" Inherits="System.Web.Mvc.ViewPage<Shell.MVC2.Models.MailMessageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SendMsg
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="postBoardDiv">
    <script type="text/javascript"><!--
        google_ad_client = "ca-pub-9814096529112578";
        /* 728x90, created 3/10/10 */
        google_ad_slot = "2378578289";
        google_ad_width = 728;
        google_ad_height = 90;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
		</div>
        <div class="clearFloat"></div>
    <div id="photoBoxStrip">
			<div id="pic9">
			</div>
			<div id="pic8">
			</div>
			<div id="pic7">
			</div>
			<div id="pic6">
			</div>
			<div id="pic5">
			</div>
			<div id="pic4">
			</div>
			<div id="pic3">
			</div>
			<div id="pic2">
			</div>
			<div id="pic1">
			</div>
			<div class="clearFloat"></div>
		</div>
	<div class="clearFloat"></div>
	
  <div id="leftDivsendmsg">
  <script type="text/javascript"><!--
      google_ad_client = "ca-pub-9814096529112578";
      /* 160x600image add */
      google_ad_slot = "0602151849";
      google_ad_width = 160;
      google_ad_height = 600;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
	</div>
	<div id="bodyDivsendmsg">
		<div class="Txt_SENDsendmsg">
			
				<p class="lastNode">SEND MESSAGE
			</p>
		</div>
		<div class="clearFloat"></div>
          <%:ViewData["message"] %>
        <% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

		<div id="Box3SliceContainersendmsg">
			<div id="Box3SliceContainer_top_1sendmsg">
			</div>
			<div class="Txt_Sendingsendmsg">
				
					<p class="lastNode">Sending a message is strictly confidential. So your name and email address will never be revealed. Your Contact info is private and is never displayed.
				</p>
			</div>
			<div class="clearFloat"></div>
			<div id="colwrap4sendmsg">
				<div id="leftBoxSlicesendmsg">
					<div class="Txt_Subjectsendmsg">
						
							<p class="lastNode">Subject:
						</p>
					</div>
					<div class="clearFloat"></div>
				  <div id = "TextField_winsendmsg"><%= Html.TextBoxFor(model => model.Subject, new { style = "width:190px;margin-left: 48px;" })%>
                    <%: Html.ValidationMessageFor(model => model.Subject)%></div>
										<div class="clearFloat"></div>
					<div class="Txt_Messagesendmsg">
						
							<p class="lastNode">Message:
						</p>
					</div>
					<div class="clearFloat"></div>
				</div>
			</div>
			<div id="colwrap5sendmsg">
				<div id="rightboxSlicesendmsg">
					<div class="Txt_Tosendmsg">
						
							<p class="lastNode">To:
						</p>
					</div>
					<div class="Txt_Namesendmsg">
						
							<p class="lastNode"><%: Model.RecipientName %>
						</p>
					</div>
					<div class="clearFloat"></div>
					<div id="photoDivsendmsg">
                        <img  src="<%: Url.Action("GetGalleryImageByScreenName", "Images", new { id = Model.RecipientName},null) %>" alt="" width ="94" height ="94" />
					
					</div>
					<div class="clearFloat"></div>
				</div>
				<div class="clearFloat"></div>
			</div>
			<div id="textAreaDivsendmsg">
                 <!--%: Html.TextAreaFor(model => model.Body,
                new
                {
                    cols = "95%",
                    rows = "12%",
                    Style = "color: #000;font-family: Arial, Helvetica, sans-serif;	font-size: 120%;"
                })%>-->
          <%: Html.TextAreaFor(model => model.Body, 14, 68, new { Width = "450px;", Height = "150px;" }) %>
                      <div class ="TextFeildRightValidation">
                         <%: Html.ValidationMessageFor(model => model.Body)%>
             </div> 
			</div>
            <%= Html.HiddenFor(model => model.RecipientID)%>
              <%= Html.HiddenFor(model => model.RecipientName )%>
            <%= Html.HiddenFor(model => model.SenderID)%>
            <%= Html.HiddenFor(model => model.SenderName)%>
            <%= Html.HiddenFor(model => model.Subject)%> 
            <%= Html.HiddenFor(model => model.uniqueID)%>
			
			<div class="clearFloat"></div>
			<div id="buttonDivsendmsg">
             <input type="submit" value="Send" name="Save" id="Submit1"   />
			</div>

			<div class="clearFloat"></div>
		<div id="Box3SliceContainer_bottom_1sendmsg">
		</div>
		</div>
             <% } /* End of BeginForm() */%>
		<div class="clearFloat"></div>
	</div>
	<div id="rightDivsendmsg">
    <script type="text/javascript"><!--
        google_ad_client = "ca-pub-9814096529112578";
        /* 160x600image add */
        google_ad_slot = "0602151849";
        google_ad_width = 160;
        google_ad_height = 600;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
	</div>

	<div class="clearFloat"></div>
    <div id="footerPhotoEditsDiv">
<center>
    <script type="text/javascript"><!--
        google_ad_client = "ca-pub-9814096529112578";
        /* 728x90, created 3/10/10 */
        google_ad_slot = "2378578289";
        google_ad_width = 728;
        google_ad_height = 90;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
</center> 
</div>

</asp:Content>
