<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Shell.MVC2.Web.Chat.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="http://code.jquery.com/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="Scripts/json2.js" type="text/javascript" ></script>
    <script src="Scripts/jquery.signalR-1.0.0.js"></script>
  <script type="text/javascript" src="~/signalr/hubs"></script>
</head>
<body>

  <button id="internal">Internal</button>
    <button id="external">External</button>

<script type="text/javascript">
    $(function () {
        // Proxy created on the fly          
       // var chat = $.connection.chat;

        // Declare a function on the chat hub so the server can invoke it          
       // chat.client.addMessage = function (message) {
       //     $('#messages').append('<li>' + message + '</li>');
      //  };

        // Start the connection
       // $.connection.hub.start().done(function () {
      //      $("#broadcast").click(function () {
       //         // Call the chat method on the server
      //          chat.server.send($('#msg').val());
        //    });
        //   });

        var cn = $.hubConnection("http://localhost/Shell.MVC2.Web.Chat/signalR");

        //test internal proxy first
        var internal = cn.createHubProxy('Chat');

        internal.on('onSomeInternalMethod', function (msg) {
            alert(msg);
        });
                       
        $('#internal').click(function () {
            internal.invoke('someInternalMethod');
        });

        //external tests 

        //external tests 

        var external = cn.createHubProxy('chatHub');

        external.on('Sendtest', function (msg) {
            alert(msg);
        });

        $('#external').click(function () {
            //  internal.invoke('someInternalMethod');
            external.invoke('Sendtest', "1", "2")
     .done(function (result) {
         alert('The result is ' + result);
     });
        });




        //var external = cn.createHubProxy('externalHub');

       // external.on('onSomeExternalMethod', function (msg) {
      //      alert(msg);
      //  });

        //$('#external').click(function () {
      //      external.invoke('someExternalMethod');
      //  });

        cn.start(function () {
            alert('started');
        });

    });
</script>
  
  <div>
    <input type="text" id="msg" />
<input type="button" id="broadcast" value="broadcast" />

<ul id="messages">
</ul>
  </div>


</body>
</html>
