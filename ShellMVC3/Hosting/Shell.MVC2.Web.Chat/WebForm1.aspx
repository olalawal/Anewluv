<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Shell.MVC2.Web.Chat.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
  
        <script src="Scripts/jquery-1.6.4.js" ></script>
          <script src="Scripts/json2.js"></script>
        <script src="Scripts/jquery.signalR-1.0.0.js"></script>
</head>
<body>

    <button id="internal">Internal</button>
    <button id="external">External</button>

    <script type="text/javascript">
        $(function () {

            var cn = $.hubConnection();

           // var internal = cn.createProxy('chatHub');
            var chat = $.hubConnection.chatHub


            internal.on('Join', function (msg) {
                alert(msg);
            });

            chat.join()
                .fail(function (e) {
                    ui.addMessage(e, 'error');
                })
                .done(function (success) {
                    if (success === false) {
                        //ui.showLogin();
                        //ui.addMessage('Type /login to show the login screen', 'notification');
                    }
                    //if we  are logged in fine send chat user name to UI
                    else {
                        //get the userName and make it avialable to UI
                        chat.getUserName()
                    .done(function (username) {
                        //ui.setUserName(username);
                    });
                    }
                    //show the chat info on screen on how to use chat 



                    // get list of available commands
                    //                    chat.getCommands()
                    //                        .done(function (commands) {
                    //                            ui.setCommands(commands);
                    //                        });
                });


            internal.on('onSomeInternalMethod', function (msg) {
                alert(msg);
            });

            $('#internal').click(function () {
                internal.invoke('someInternalMethod');
            });

            var external = cn.createProxy('externalHub');

            external.on('onSomeExternalMethod', function (msg) {
                alert(msg);
            });

            $('#external').click(function () {
                external.invoke('someExternalMethod');
            });

            cn.start(function () {
                alert('started');
            });
        });
    </script>

</body>
</html>
