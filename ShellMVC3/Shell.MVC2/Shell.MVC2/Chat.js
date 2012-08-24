/// <reference path="Scripts/jquery-1.7.js" />
/// <reference path="Scripts/jQuery.tmpl.js" />
/// <reference path="Scripts/jquery.cookie.js" />
/// <reference path="Chat.ui.js" />




(function ($, connection, window, ui, utility) {
    "use strict";
    var chat = $.connection.chatHub,
        messageHistory = [],
        historyLocation = 0,
        originalTitle = document.title,
        unread = 0,
        isUnreadMessageForUser = false,
        focus = true,
        loadingHistory = false,
        checkingStatus = false,
        typing = false,
        typingTimeoutId = null,
        $ui = $(ui),
        messageSendingDelay = 1500,
        pendingMessages = {};



    function isSelf(user) {
        return chat.name === user.Name;
    }

    function getNoteCssClass(user) {
        if (user.IsAfk === true) {
            return 'afk';
        }
        else if (user.Note) {
            return 'message';
        }
        return '';
    }

    function getNote(user) {
        if (user.IsAfk === true) {
            if (user.AfkNote) {
                return 'AFK - ' + user.AfkNote;
            }
            return 'AFK';
        }

        return user.Note;
    }

    function getFlagCssClass(user) {
        return (user.Flag) ? 'flag flag-' + user.Flag : '';
    }

    function populateRoom(room) {
        var d = $.Deferred();
        // Populate the list of users rooms and messages 
        chat.getRoomInfo(room)
                .done(function (roomInfo) {
                    $.each(roomInfo.Users, function () {
                        var userViewModel = getUserViewModel(this);
                        ui.addUser(userViewModel, room);
                        ui.setUserActivity(userViewModel);
                        //TO do show chatboxes as well
                        //TO DO add some logic to show show correct person
                        //Added minimize option, right now we are saving this info as cookies
                        //but in the future we will store chat proferences and old messages in the database

                        ui.createchatbox(roomInfo, true);
                    });

                    $.each(roomInfo.Owners, function () {
                        ui.setRoomOwner(this, room);
                    });

                    $.each(roomInfo.RecentMessages, function () {
                        var viewModel = getMessageViewModel(this);

                        // ui.addChatMessage(viewModel, room);
                        ui.addChatMessage(viewModel, room, true);
                    });

                    //TO DO topic should be the opposite of you again figure this out
                    //ui.changeRoomTopic(roomInfo);
                    // mark room as initialized to differentiate messages
                    // that are added after initial population
                    ui.setInitialized(room);
                    ui.scrollToBottom(room);

                    d.resolveWith(chat);
                })
                .fail(function () {
                    d.rejectWith(chat);
                });

        return d;
    }

    function sendChatRequestMessage(room) {
        //now create the chatrequest message and send it

        var id = utility.newId(),
            clientMessage = {
                id: id,
                content: "/chatreq @" + room.Topic + " " +
                room.chatrequestboxname + " Wants to chat with you, press ok to accept",
                room: room.roomname
            },
            messageCompleteTimeout = null;


        // If there's a significant delay in getting the message sent
        // mark it as pending
        messageCompleteTimeout = window.setTimeout(function () {
            // If after a second
            ui.markMessagePending(id);
        },
            messageSendingDelay);

        pendingMessages[id] = messageCompleteTimeout;



        //send the message that creates the chat request after the room is created and both users joined to it
        chat.send(clientMessage)
            .done(function (requiresUpdate) {
                if (requiresUpdate === true) {
                    //TO DO find a way to update user 
                    //  ui.showUpdateUI();
                    //  var bk = requiresUpdate;
                }

                if (messageCompleteTimeout) {
                    clearTimeout(messageCompleteTimeout);
                    delete pendingMessages[id];
                }

                ui.confirmMessage(id);
            })
            .fail(function (e) {
                chat.sentChatRequestNotification(chat.name, e, chat.activeRoom);

                ui.addMessage(e, 'error');
            });

        // Store message history
        messageHistory.push(clientMessage.content);

        // REVIEW: should this pop items off the top after a certain length?
        historyLocation = messageHistory.length;
        return true;
    }
    //TO DO can probbaly use the same function
    function AcceptChatRequest(room) {

        //now Join both members to the chat room
        //Spaces are important Yo
        //TO do you really dont need to pass chat room since it can be found now
        var id = utility.newId(),
            clientMessage = {
                id: id,
                content: "/acceptchatreq " + room.TopicStarter + " " +
                room.Name + " Your chat request has been Accepted",
                room: room.roomname
            },
            messageCompleteTimeout = null;


        // If there's a significant delay in getting the message sent
        // mark it as pending
        messageCompleteTimeout = window.setTimeout(function () {
            // If after a second
            ui.markMessagePending(id);
        },
            messageSendingDelay);

        pendingMessages[id] = messageCompleteTimeout;

        //send the message that creates the chat request after the room is created and both users joined to it
        chat.send(clientMessage)
            .done(function (requiresUpdate) {
                if (requiresUpdate === true) {
                    //TO DO find a way to update user 
                    //  ui.showUpdateUI();
                    //  var bk = requiresUpdate;
                }

                if (messageCompleteTimeout) {
                    clearTimeout(messageCompleteTimeout);
                    delete pendingMessages[id];
                }

                ui.confirmMessage(id);
            })
            .fail(function (e) {
                chat.sentChatRequestNotification(chat.name, e, chat.activeRoom);

                ui.addMessage(e, 'error');
            });

        // Store message history
        messageHistory.push(clientMessage.content);

        // REVIEW: should this pop items off the top after a certain length?
        historyLocation = messageHistory.length;
        return true;

    }

    //Function to complete chat request
    function RejectChatRequest(room) {

        //now Join both members to the chat room
        //Spaces are important Yo
        //TO do you really dont need to pass chat room since it can be found now
        //TO do add the name of the rejector ?
        var id = utility.newId(),
            clientMessage = {
                id: id,
                content: "/rejectchatreq " + room.TopicStarter + " " +
                room.Name + " Your chat request was not accepted by: ",
                room: room.Name
            },
            messageCompleteTimeout = null;


        // If there's a significant delay in getting the message sent
        // mark it as pending
        messageCompleteTimeout = window.setTimeout(function () {
            // If after a second
            ui.markMessagePending(id);
        },
            messageSendingDelay);

        pendingMessages[id] = messageCompleteTimeout;

        //send the message that creates the chat request after the room is created and both users joined to it
        chat.send(clientMessage)
            .done(function (requiresUpdate) {
                if (requiresUpdate === true) {
                    //TO DO find a way to update user 
                    //  ui.showUpdateUI();
                    //  var bk = requiresUpdate;
                }

                if (messageCompleteTimeout) {
                    clearTimeout(messageCompleteTimeout);
                    delete pendingMessages[id];
                }

                ui.confirmMessage(id);
            })
            .fail(function (e) {
                chat.sentChatRequestNotification(chat.name, e, chat.activeRoom);

                ui.addMessage(e, 'error');
            });

        // Store message history
        messageHistory.push(clientMessage.content);

        // REVIEW: should this pop items off the top after a certain length?
        historyLocation = messageHistory.length;
        return true;

    }

    function populateLobbyRooms() {
        // Populate the user list with room names
        chat.getRooms()
            .done(function (rooms) {
                ui.populateLobbyRooms(rooms);
            });
    }

    function scrollIfNecessary(callback, room) {
        var nearEnd = ui.isNearTheEnd(room);

        callback();

        if (nearEnd) {
            ui.scrollToBottom(room);
        }
    }

    function getUserViewModel(user, isOwner) {
        var lastActive = user.LastActivity.fromJsonDate();
        return {
            name: user.Name,
            screenName: user.ScreenName,
            hash: user.Hash,
            owner: isOwner,
            active: user.Active,
            noteClass: getNoteCssClass(user),
            note: getNote(user),
            flagClass: getFlagCssClass(user),
            flag: user.Flag,
            country: user.Country,
            lastActive: lastActive,
            timeAgo: $.timeago(lastActive)
        };
    }

    function getMessageViewModel(message) {
        var re = new RegExp("\\b@?" + chat.name.replace(/\./, '\\.') + "\\b", "i");
        return {
            name: message.User.Name,
            screenName : message.User.ScreenName,
            hash: message.User.Hash,
            message: message.Content,
            id: message.Id,
            date: message.When.fromJsonDate(),
            highlight: re.test(message.Content) ? 'highlight' : '',
            isOwn: re.test(message.User.name)
        };
    }

    // Save some state in a cookie
    function updateCookie() {
        var state = {
            userId: chat.id,
            activeRoom: chat.activeRoom,
            preferences: ui.getState()
        },
        jsonState = window.JSON.stringify(state);

        $.cookie('jabbr.state', jsonState, { path: '/', expires: 30 });
    }

    function updateTitle() {
        if (unread === 0) {
            document.title = originalTitle;
        }
        else {
            document.title =
                (isUnreadMessageForUser ? '*' : '')
                + '(' + unread + ') ' + originalTitle;
        }
    }

    function updateUnread(room, isMentioned) {
        if (focus === false) {
            isUnreadMessageForUser = (isUnreadMessageForUser || isMentioned);

            unread = unread + 1;
        } else {
            //we're currently focused so remove
            //the * notification
            isUnreadMessageForUser = false;
        }

        ui.updateUnread(room, isMentioned);

        updateTitle();
    }

    // Room commands

    // When the /join command gets raised this is called
    chat.joinRoom = function (room) {
        //TO DO change the added to create the chatbox here and return true
        //var added = ui.addRoom(room.Name);

        //only put regular chat rooms into active room status, request are not active rooms
        //and should be gone until the Chatrrequest flag is removed
        if (room.ChatRequestAccepted) {
            ui.setActiveRoom(room.Name);
        }

        if (room.Private) {
            ui.setRoomLocked(room.Name);
        }
        //Updated to only populate generic rooms when it is not a chatrequest
        //  if (added && room.ChatRequestAccepted) {
        //removed added for now
        //TO DO show message of joining
        if (room.ChatRequestAccepted) {
            populateRoom(room.Name).done(function () {
                ui.addMessage('You just entered ' + room.Name, 'notification', room.Name);
                // ui.createchatbox(roomInfo.Name, roomInfo.Topic, 1);
                //  $("#chatbox_" + roomInfo.Name + " .chatboxtextarea").focus();
            });
        }


    };

    // Called when a returning users join chat
    chat.logOn = function (rooms) {



        var activeRoom = this.activeRoom,
            loadRooms = function () {
                $.each(rooms, function (index, room) {
                    if (chat.activeRoom !== room.Name && room.ChatRequestAccepted == true) {
                        populateRoom(room.Name);
                    }
                });
                populateLobbyRooms();
            };

        $.each(rooms, function (index, room) {
            ui.addRoom(room.Name);
            if (room.Private) {
                ui.setRoomLocked(room.Name);
            }
        });
        ui.setUserName(chat.name);
        ui.addMessage('Welcome back ' + chat.name, 'notification', 'lobby');
        ui.addMessage('You can join any of the rooms on the right', 'notification', 'lobby');
        ui.addMessage('Type /rooms to list all available rooms', 'notification', 'lobby');
        ui.addMessage('Type /logout to log out of chat', 'notification', 'lobby');

        // Process any urls that may contain room names
        ui.run();

        // If the active room didn't change then set the active room (since no navigation happened)
        if (activeRoom === this.activeRoom) {
            ui.setActiveRoom(this.activeRoom || 'Lobby');
        }
        //Updated to only populate generic rooms when it is not a chatrequest
        if (this.activeRoom) {
            // Always populate the active room first then load the other rooms so it looks fast :)
            populateRoom(this.activeRoom).done(loadRooms);
        }
        else {
            // There's no active room so we don't care
            loadRooms();
        }
    };

    chat.lockRoom = function (user, room) {
        if (!isSelf(user) && this.activeRoom === room) {
            ui.addMessage(user.Name + ' has locked ' + room + '.', 'notification', this.activeRoom);
        }

        ui.setRoomLocked(room);
    };

    // Called when this user locked a room
    chat.roomLocked = function (room) {
        ui.addMessage(room + ' is now locked.', 'notification', this.activeRoom);
    };

    chat.roomClosed = function (room) {
        populateLobbyRooms();
        ui.addMessage('Room \'' + room + '\' is now closed', 'notification', this.activeRoom);
    };

    chat.addOwner = function (user, room) {
        ui.setRoomOwner(user.Name, room);
    };

    chat.removeOwner = function (user, room) {
        ui.clearRoomOwner(user.Name, room);
    };

    chat.updateRoomCount = function (room, count) {
        ui.updateLobbyRoomCount(room, count);
    };

    chat.markInactive = function (users) {
        $.each(users, function () {
            var viewModel = getUserViewModel(this);
            ui.setUserActivity(viewModel);
        });
    };

    chat.updateActivity = function (user) {
        var viewModel = getUserViewModel(user);
        ui.setUserActivity(viewModel);
    };

    //Message and chat request methods

    //called from server when chat request was sent
    chat.sendChatRequest = function (from, to, message, room) {


        //TO DO put back when live
        //        if (isSelf({ Name: to })) {
        //            // Force notification for direct messages
        //            ui.notify(true);
        //        }

        ui.showDynamicNotificationDialog(message);
        //it if the current joiner is the owner and we have a chat request populate the different notifications for each end
        //        if (jQuery.inArray(chat.name, room.Owners) && room.ChatRequest && chat.name == from) {
        //            message.message = "Your Chat Request has been sent to " + to;
        //            ui.addprivatemessagepopup([{ roomname: from + "_" + to, to: to, from: from, message: message}]);
        //        }
        //        if (chat.name == to) {
        //            ui.addprivatemessagepopup([{ roomname: room.Name, to: to, from: from, message: message}]);
        //        }


    };

    chat.sentChatRequestNotification = function (user, message, room) {

        if (!isSelf(user)) {
            ui.showDynamicNotificationDialog(message);
        }
    };

    chat.addMessageContent = function (id, content, room) {
        var nearTheEndBefore = ui.isNearTheEnd(room);

        scrollIfNecessary(function () {
            ui.addChatMessageContent(id, content, room);
        }, room);

        updateUnread(room, false /* isMentioned: this is outside normal messages and user shouldn't be mentioned */);

        // Adding external content can sometimes take a while to load
        // Since we don't know when it'll become full size in the DOM
        // we're just going to wait a little bit and hope for the best :) (still a HACK tho)
        window.setTimeout(function () {
            var nearTheEndAfter = ui.isNearTheEnd(room);
            if (nearTheEndBefore && nearTheEndAfter) {
                ui.scrollToBottom();
            }
        }, 850);
    };

    chat.addMessage = function (message, room) {
        var viewModel = getMessageViewModel(message);

        // Update your message when it comes from the server
        if (ui.messageExists(viewModel.id)) {
            ui.replaceMessage(viewModel);
            return;
        }

        scrollIfNecessary(function () {
            //used overload with add flag for chatbox message set to true
            ui.addChatMessage(viewModel, room, true);
        }, room);

        var isMentioned = viewModel.highlight === 'highlight';

        updateUnread(room, isMentioned);
    };

    //TO DO figure out if we still need this , it does not work
    chat.sendDialogNotification = function (user, message, room) {

        if (!isSelf(user)) {
            ui.showDynamicNotificationDialog(message);
        }
    };

    chat.addUser = function (user, room, isOwner) {
        var viewModel = getUserViewModel(user, isOwner);

        var added = ui.addUser(viewModel, room);

        if (added) {
            if (!isSelf(user)) {
                ui.addMessage(user.Name + ' just entered ' + room, 'notification', room);
            }
        }
    };

    chat.changeUserName = function (oldName, user, room) {
        ui.changeUserName(oldName, user, room);

        if (!isSelf(user)) {
            ui.addMessage(oldName + '\'s nick has changed to ' + user.Name, 'notification', room);
        }
    };

    chat.changeGravatar = function (user, room) {
        ui.changeGravatar(user, room);

        if (!isSelf(user)) {
            ui.addMessage(user.Name + "'s gravatar changed.", 'notification', room);
        }
    };

    // User single client commands

    chat.allowUser = function (room) {
        ui.addMessage('You were granted access to ' + room, 'notification', this.activeRoom);
    };

    chat.userAllowed = function (user, room) {
        ui.addMessage(user + ' now has access to ' + room, 'notification', this.activeRoom);
    };

    chat.unallowUser = function (user) {
        ui.addMessage('You access to ' + room + ' was revoked.', 'notification', this.activeRoom);
    };

    chat.userUnallowed = function (user, room) {
        ui.addMessage('You have revoked ' + user + '"s access to ' + room, 'notification', this.activeRoom);
    };

    // Called when you make someone an owner
    chat.ownerMade = function (user, room) {
        ui.addMessage(user + ' is now an owner of ' + room, 'notification', this.activeRoom);
    };

    chat.ownerRemoved = function (user, room) {
        ui.addMessage(user + ' is no longer an owner of ' + room, 'notification', this.activeRoom);
    };

    // Called when you've been made an owner
    chat.makeOwner = function (room) {
        ui.addMessage('You are now an owner of ' + room, 'notification', this.activeRoom);
    };

    // Called when you've been removed as an owner
    chat.demoteOwner = function (room) {
        ui.addMessage('You are no longer an owner of ' + room, 'notification', this.activeRoom);
    };

    // Called when your gravatar has been changed
    chat.gravatarChanged = function () {
        ui.addMessage('Your gravatar has been set', 'notification', this.activeRoom);
    };

    // Called when the server sends a notification message
    chat.postNotification = function (msg, room) {
        ui.addMessage(msg, 'notification', room);
    };

    // Called when you created a new user
    chat.userCreated = function () {
        ui.setUserName(this.name);
        ui.addMessage('Your nick is ' + this.name, 'notification');

        // Process any urls that may contain room names
        ui.run();

        if (!this.activeRoom) {
            // Set the active room to the lobby so the rooms on the right load
            ui.setActiveRoom('Lobby');
        }

        // Update the cookie
        updateCookie();
    };

    chat.logOut = function (rooms) {
        ui.setActiveRoom('Lobby');

        // Close all rooms
        $.each(rooms, function () {
            ui.removeRoom(this);
        });

        ui.addMessage("You've been logged out.", 'notification', this.activeRoom);

        chat.activeRoom = undefined;
        chat.name = undefined;
        chat.id = undefined;

        updateCookie();

        // Reload the page
        document.location = document.location.pathname;
    };

    chat.showUserInfo = function (userInfo) {
        var lastActivityDate = userInfo.LastActivity.fromJsonDate();
        var status = "Currently " + userInfo.Status;
        if (userInfo.IsAfk) {
            status += userInfo.Status == 'Active' ? ' but ' : ' and ';
            status += ' is Afk';
        }
        ui.addMessage('User information for ' + userInfo.Name +
            " (" + status + " - last seen " + $.timeago(lastActivityDate) + ")", 'list-header');

        if (userInfo.AfkNote) {
            ui.addMessage('Afk: ' + userInfo.AfkNote, 'list-item');
        }
        else if (userInfo.Note) {
            ui.addMessage('Note: ' + userInfo.Note, 'list-item');
        }

        chat.showUsersOwnedRoomList(userInfo.Name, userInfo.OwnedRooms);
    };

    chat.setPassword = function () {
        ui.addMessage('Your password has been set', 'notification', this.activeRoom);
    };

    chat.changePassword = function () {
        ui.addMessage('Your password has been changed', 'notification', this.activeRoom);
    };

    // Called when you have added or cleared a note
    chat.noteChanged = function (isAfk, isCleared) {
        var afkMessage = 'You have gone AFK';
        var noteMessage = 'Your note has been ' + (isCleared ? 'cleared' : 'set');
        ui.addMessage(isAfk ? afkMessage : noteMessage, 'notification', this.activeRoom);
    };

    // Make sure all the people in all the rooms know that a user has changed their note.
    chat.changeNote = function (user, room) {
        var viewModel = getUserViewModel(user);

        ui.changeNote(viewModel, room);

        if (!isSelf(user)) {
            var message;
            if (user.IsAfk === true) {
                message = user.Name + ' has gone AFK';
            }
            else {
                message = user.Name + ' has ' + (user.Note ? 'set' : 'cleared') + ' their note';
            }

            ui.addMessage(message, 'notification', room);
        }
    };

    chat.changeTopic = function (room) {
        ui.changeRoomTopic(room);
    };

    chat.topicChanged = function (isCleared, topic) {
        var action = isCleared ? 'cleared' : 'set';
        var to = topic ? ' to ' + '"' + topic + '"' : '';
        var message = 'You have ' + action + ' the room topic' + to;
        ui.addMessage(message, 'notification', this.activeRoom);
    };

    // Called when you have added or cleared a flag
    chat.flagChanged = function (isCleared, country) {
        var action = isCleared ? 'cleared' : 'set';
        var place = country ? ' to ' + country : '';
        var message = 'You have ' + action + ' your flag' + place;
        ui.addMessage(message, 'notification', this.activeRoom);
    };

    // Make sure all the people in the all the rooms know that a user has changed their flag
    chat.changeFlag = function (user, room) {
        var viewModel = getUserViewModel(user);

        ui.changeFlag(viewModel, room);

        if (!isSelf(user)) {
            var action = user.Flag ? 'set' : 'cleared';
            var country = viewModel.country ? ' to ' + viewModel.country : '';
            var message = user.Name + ' has ' + action + ' their flag' + country;
            ui.addMessage(message, 'notification', room);
        }
    };

    chat.userNameChanged = function (user) {
        // Update the client state
        chat.name = user.Name;
        ui.setUserName(chat.name);
        ui.addMessage('Your name is now ' + user.Name, 'notification', this.activeRoom);
    };

    chat.setTyping = function (user, room) {
        var viewModel = getUserViewModel(user);
        ui.setUserTyping(viewModel, room);
    };

    chat.sendMeMessage = function (name, message, room) {
        ui.addMessage('*' + name + ' ' + message, 'notification', room);
    };

    chat.sendPrivateMessage = function (from, to, message, roomviewmodel) {
        //debugger;
        //handle the case of succsful send request  and a rejected request by the TopicStarter i.e the creator of the request  /
        //do nothing for accepted you would just open the chat room for the topic starter,   initial request since both are false
        if (isSelf({ Name: roomviewmodel.TopicStarter }) && roomviewmodel.ChatRequestRejected == false && roomviewmodel.ChatRequestAccepted == false) {
            // Force notification for direct messages
            ui.showDynamicNotificationDialog("Your chat request has been sent to " + roomviewmodel.TopicScreenName);


        }
        //handle the case where the topic sataerter was rejected
        else if (isSelf({ Name: roomviewmodel.TopicStarter }) && roomviewmodel.ChatRequestRejected == true) {
            ui.showDynamicNotificationDialog(message);
        }

        //handle the cases of the perseon who is getting requested to chat , initial request since both are false
        if (isSelf({ Name: roomviewmodel.Topic }) && roomviewmodel.ChatRequestRejected == false && roomviewmodel.ChatRequestAccepted == false) {
            // Force notification for direct messages
            //var roominfo = [{ screenName: to, senderScreenName: from, chatboxname: roomviewmodel.Name}];
            ui.showDynamicChatRequestDialog(message, "Notification", roomviewmodel, roomviewmodel.TopicStarterScreenName);
            // ui.notify(true);

        }
        //handle the case where the topic sataerter was rejected
        //do stuff for like allowing them to block the user or add user to a certain list
        else if (isSelf({ Name: roomviewmodel.Topic }) && roomviewmodel.ChatRequestRejected == true) {
            ui.showDynamicNotificationDialog("You have rejected the chat request from: " + roomviewmodel.TopicStarterScreenName);
        }


        //send a message on UI instead test this since the recipient should get this 
        // ui.showUpdateUI();

        //this will create the PM basically a static box both can see, add both users to the room and allow for ok an cancel.
        // ui.addprivatemessagepopup(temproomname, from, to, message);

        //   ui.addPrivateMessage('<emp>*' + from + '* &raquo; *' + to + '*</emp> ' + message, 'pm');
    };

    chat.nudge = function (from, to) {
        function shake(n) {
            var move = function (x, y) {
                parent.moveBy(x, y);
            };
            for (var i = n; i > 0; i--) {
                for (var j = 1; j > 0; j--) {
                    move(i, 0);
                    move(0, -i);
                    move(-i, 0);
                    move(0, i);
                    move(i, 0);
                    move(0, -i);
                    move(-i, 0);
                    move(0, i);
                    move(i, 0);
                    move(0, -i);
                    move(-i, 0);
                    move(0, i);
                }
            }
            return this;
        };
        $("body").effect("pulsate", { times: 3 }, 300);
        window.setTimeout(function () {
            shake(20);
        }, 300);

        ui.addMessage('*' + from + ' nudged ' + (to ? 'you' : 'the room'), to ? 'pm' : 'notification');
    };
    chat.leave = function (user, room) {


        if (isSelf(user)) {
            //no need to tell a user is they have left a room
            //TO DO add code to ask if they want to add the user to contacts at some point in the left cchat
            //ADD an IScontact flag or something from the DB
            //room already closed via jquery for this user
            // ui.setActiveRoom('Lobby');
            // ui.removeRoom(room);
            // ui.addMessage('You have left ' + room, 'notification');
            //  ui.showDynamicNotificationDialogWithImage(' you ended the chat session with ', "Notification", room, user.Name);

        }
        else {
            // ui.removeUser(user, room);
            // ui.addMessage(user.Name + ' left ' + room, 'notification', room);
            //send popup notifciation 
            ui.showDynamicNotificationDialogWithImage(user.SceenName + ' has ended the chat session ', "Notification", room, user.Name);
        }
    };
    chat.kick = function (room) {
        ui.setActiveRoom('Lobby');
        ui.removeRoom(room);
        ui.addMessage('You were kicked from ' + room, 'notification');
    };
    // Helpish commands
    chat.showRooms = function (rooms) {
        ui.addMessage('Rooms', 'list-header');
        if (!rooms.length) {
            ui.addMessage('No rooms available', 'list-item');
        }
        else {
            // sort rooms by count descending
            var sorted = rooms.sort(function (a, b) {
                return a.Count > b.Count ? -1 : 1;
            });

            $.each(sorted, function () {
                ui.addMessage(this.Name + ' (' + this.Count + ')', 'list-item');
            });
        }
    };
    chat.showCommands = function () {
        ui.addMessage('Help', 'list-header');
        $.each(ui.getCommands(), function () {
            ui.addMessage(this.Name + ' - ' + this.Description, 'list-item');
        });
    };
    chat.showUsersInRoom = function (room, names) {
        ui.addMessage('Users in ' + room, 'list-header');
        if (names.length === 0) {
            ui.addMessage('Room is empty', 'list-item');
        }
        else {
            $.each(names, function () {
                ui.addMessage('- ' + this, 'list-item');
            });
        }
    };
    chat.listUsers = function (users) {
        if (users.length === 0) {
            ui.addMessage('No users matched your search', 'list-header');
        }
        else {
            ui.addMessage('The following users match your search', 'list-header');
            ui.addMessage(users.join(', '), 'list-item');
        }
    };
    chat.showUsersRoomList = function (user, rooms) {
        var status = "Currently " + user.Status;
        if (rooms.length === 0) {
            ui.addMessage(user.Name + ' (' + status + ') is not in any rooms', 'list-header');
        }
        else {
            ui.addMessage(user.Name + ' (' + status + ') is in the following rooms', 'list-header');
            ui.addMessage(rooms.join(', '), 'list-item');
        }
    };
    chat.showUsersOwnedRoomList = function (user, rooms) {
        if (rooms.length === 0) {
            ui.addMessage(user + ' does not own any rooms', 'list-header');
        }
        else {
            ui.addMessage(user + ' owns the following rooms', 'list-header');
            ui.addMessage(rooms.join(', '), 'list-item');
        }
    };
    $ui.bind(ui.events.typing, function () {
        // If not in a room, don't try to send typing notifications
        if (!chat.activeRoom) {
            return;
        }

        if (checkingStatus === false && typing === false) {
            typing = true;

            chat.typing(chat.activeRoom);

            window.setTimeout(function () {
                typing = false;
            },
            3000);
        }
    });

    //added chatbox title too function
    $ui.bind(ui.events.sendMessage, function (ev, msg) {
        var id = utility.newId(),
            clientMessage = {
                id: id,
                content: msg.msg,
                room: chat.activeRoom
            },
            messageCompleteTimeout = null;

        //code to handle chatboxes modify the client message model
        if (msg.chatboxtitle && msg[0] !== '/') {
            clientMessage.room = msg.chatboxtitle;
            var viewModel = {
                name: chat.name,
                hash: chat.hash,
                message: $('<div/>').text(clientMessage.content).html(),
                id: clientMessage.id,
                date: new Date(),
                highlight: ''
            };

            //3-25-2012 added code to make sure we do not send the message to ourselfs
            //since it is already put on our UI
            //TO do preempt this on the UI side so we do not even get here i think
            //this is done via callback 
            // ui.addChatMessage(viewModel, clientMessage.room, true);



            // If there's a significant delay in getting the message sent
            // mark it as pending
            messageCompleteTimeout = window.setTimeout(function () {
                // If after a second
                ui.markMessagePending(id);
            },
            messageSendingDelay);

            pendingMessages[id] = messageCompleteTimeout;
        }



        //        if (msg[0] !== '/') {
        //            // Added the message to the ui first
        //            var viewModel = {
        //                name: chat.name,
        //                hash: chat.hash,
        //                message: $('<div/>').text(clientMessage.content).html(),
        //                id: clientMessage.id,
        //                date: new Date(),
        //                highlight: ''
        //            };

        //            ui.addChatMessage(viewModel, clientMessage.room);

        //            // If there's a significant delay in getting the message sent
        //            // mark it as pending
        //            messageCompleteTimeout = window.setTimeout(function () {
        //                // If after a second
        //                ui.markMessagePending(id);
        //            },
        //            messageSendingDelay);

        //            pendingMessages[id] = messageCompleteTimeout;
        //        }

        chat.send(clientMessage)
            .done(function (requiresUpdate) {
                if (requiresUpdate === true) {
                    //TO do we dont want rereshes on each message
                    //TO do have the chatbox flash i thinl
                    //  ui.showUpdateUI();
                }

                if (messageCompleteTimeout) {
                    clearTimeout(messageCompleteTimeout);
                    delete pendingMessages[id];
                }

                ui.confirmMessage(id);
            })
            .fail(function (e) {
                ui.addMessage(e, 'error');
            });

        // Store message history
        messageHistory.push(msg);

        // REVIEW: should this pop items off the top after a certain length?
        historyLocation = messageHistory.length;
    });

    //added added ability to send pop up PMs in order to initiate chat request
    $ui.bind(ui.events.createChatRequest, function (ev, room) {


        //first create the room between both users if it does not already exist, the chatrequest box is created via the notification service
        //thats how the other user on the other end gets the poppup, they are notified and UI pops up, for now do nothing on sender end 
        //TO DO have a popup stating your message has been sent
        chat.getRoomInfo(room.chatrequestboxname)
          .done(function (roomInfo) {

              //do the room populatin and display from here then since its alreayd created
              //if we have a valid room just show the UI and connect the users similar to populaetue 
              //user 
              if (roomInfo) {
                  //update activity

                  //then update owener activity
                  //TO DO change to use chatbox UI
//                  $.each(roomInfo.Owners, function () {
//                      ui.setRoomOwner(this, room.senderScreenName);
//                  });
                  //if room exists we probbaly want to just add both users and send the chat message

                  //changed coded to not join the room until chat request is created since in that area
                  //we flag the room a chatrequest so it does not call populate room on ui referesh
                  sendChatRequestMessage(roomInfo)

              }
              //no room exists then create the room and set the topic to the screen name of the user you are talking to
              //TO DO add a new field since both users see opostite topics 
              else {
                  //only change the topic after room is created and then join as well
                  //var dd = data; //temp var for testing 
                  //changed coded to not join the room until chat request is created since in that area
                  //we flag the room a chatrequest so it does not call populate room on ui referesh
            
                  sendChatRequestMessage(room)


              }
          })
          .fail(function (e) {
              alert("There was an error opening chat , please try again later")
          });


    });


    //TO do figure out whats up if room does not exist !!
    $ui.bind(ui.events.acceptChatRequest, function (ev, room) {

        // var roomtest = room;
        //TO DO do we really need to do this still ?
        chat.getRoomInfo(room.Name)
          .done(function (roomInfo) {

              //do the room populatin and display from here then since its alreayd created
              //if we have a valid room just show the UI and connect the users similar to populaetue 
              //user 
              if (roomInfo) {
                  //update activity

                  //changed coded to not join the room until chat request is created since in that area
                  //we flag the room a chatrequest so it does not call populate room on ui referesh
                  AcceptChatRequest(roomInfo)

              }

          })
          .fail(function (e) {
              alert("There was an error opening chat , please try again later")
          });


    });

    //TO do figure out whats up if room does not exist !!
    $ui.bind(ui.events.rejectChatRequest, function (ev, room) {

        // var roomtest = room; 
        //TO DO do we really need to do this still ? we should always have a room by this point ?
        chat.getRoomInfo(room.Name)
          .done(function (roomInfo) {

              //do the room populatin and display from here then since its alreayd created
              //if we have a valid room just show the UI and connect the users similar to populaetue 
              //user 
              if (roomInfo) {
                  //update activity

                  //changed coded to not join the room until chat request is created since in that area
                  //we flag the room a chatrequest so it does not call populate room on ui referesh
                  RejectChatRequest(roomInfo)

              }

          })
          .fail(function (e) {
              alert("please try again later")
          });


    });

    //new method aded by me to automatically creaye rooms and setup UI
    //we dont create rooms though this anymore
    //we user the Acceptchatrequest method, maybe this method can be used for chatting with online friends
    $ui.bind(ui.events.createRoom, function (ev, room) {

        chat.getRoomInfo(room.chatboxname)
          .done(function (roomInfo) {

              //do the room populatin and display from here then since its alreayd created
              //if we have a valid room just show the UI and connect the users similar to populaetue 
              //user 
              if (roomInfo) {
                  //update activity

                  //then update owener activity
                  //TO DO change to use chatbox UI
//                  $.each(roomInfo.Owners, function () {
//                      ui.setRoomOwner(this, room.senderScreenName);
//                  });

                  //join the room if you are not in it already!!
                  chat.send('/join ' + roomInfo.Name, chat.activeRoom)
                            .done(function (data) {
                                //inivialize the room (i.e show etc)
                                //TO do determine what topic to be shown based i.e the opposite of your
                                //screen name                   
                                ui.createchatbox(roomInfo, true);
                                $("#chatbox_" + roomInfo.Name + " .chatboxtextarea").focus();
                            })
                             .fail(function (e) {
                                 var error = e;

                             });


                  //scroll to bottm etc and 
              }
              //no room exists then create the room and set the topic to the screen name of the user you are talking to
              //TO DO add a new field since both users see opostite topics 
              else {
                  //only change the topic after room is created and then join as well
                  chat.send('/create ' + room.chatboxname, chat.activeRoom)
                    .done(function (data) {
                        //set the room topic here
                        chat.send('/topic ' + room.Topic, room.chatboxname)  //dont minizmise new boxes
                        .done(function (data) {
                            var dd = data; //temp var for testing
                            //join the room yay!! only after the topic is updated since we need it in viewmodel later
                            chat.send('/join ' + room.chatboxname, chat.activeRoom)
                            .done(function (data) {
                                //once we are joined via signalIR and server show show or create the chatbox
                                ui.createchatbox(room.chatboxname, room.Topic, room.TopicStarter);
                                $("#chatbox_" + room.chatboxname + " .chatboxtextarea").focus();
                            });
                        });
                    })
                     .fail(function (e) {
                         ui.setActiveRoom('Lobby');
                         ui.addMessage(e, 'error');
                     });
              }
          })
          .fail(function (e) {
              alert("There was an error opening chat , please try again later")
          });
    });

    //new method aded by me to automatically creaye rooms and setup UI
    $ui.bind(ui.events.closeRoom, function (ev, room) {

        chat.getRoomInfo(room.name)
          .done(function (roomInfo) {
            if (roomInfo) {             

                  //leave the room !!
                  chat.send('/leave ' + room.name, chat.activeRoom)
                            .done(function (data) {

                                //now close room!!
                                 chat.send('/close ' + room.name, chat.activeRoom)
                                .done(function (data) {

                                })
                             .fail(function (e) {
                                // var error = e;
                                 ui.showDynamicNotificationDialog('No chat room specified to close !');
                             });

                            })
                             .fail(function (e) {
                                 var error = e;
                                 ui.showDynamicNotificationDialog('No chat room specified to leave !');
                             });

                  //scroll to bottm etc and 
              }
              //no room exists then create the room and set the topic to the screen name of the user you are talking to
              //TO DO add a new field since both users see opostite topics 
              else {
                  //send a message 
                  ui.showDynamicNotificationDialog('No chat room specified !');
              }
          })
          
    });


    $ui.bind(ui.events.focusit, function () {
        focus = true;
        unread = 0;
        updateTitle();
    });

    $ui.bind(ui.events.blurit, function () {
        focus = false;

        updateTitle();
    });

    $ui.bind(ui.events.acceptinvite, function (ev, topic) {
        chat.send('/topic ' + topic.name, topic.activeroom)
            .fail(function (e) {
                ui.setActiveRoom('Lobby');
                ui.addMessage(e, 'error');
            });
    })

    $ui.bind(ui.events.openRoom, function (ev, room) {
        chat.send('/join ' + room, chat.activeRoom)
            .fail(function (e) {
                ui.setActiveRoom('Lobby');
                ui.addMessage(e, 'error');
            });
    });

    //anyone can attempt to close the rooms in this since its two to two
    //if there are more than two users in the room check if the closer is the owner.
    //first leave the room then attempt close
    $ui.bind(ui.events.closeRoom, function (ev, room)
     {
        chat.send('/leave ' + room, chat.activeRoom)
            .done(function (data) {
                chat.send('/close' + room, chat.activeRoom)
            })
            .fail(function () {
                loadingHistory = false;
            });
    });

    //when user hits x on thier end we want to end the chat room on only one end
    //i.e dont automatically close the room on the other end unless certian conditions are met
    //TDD what that is 
    $ui.bind(ui.events.leaveRoom, function (ev, room) {
        chat.send('/leave ' + room, chat.activeRoom)
                       .fail(function () {
                           loadingHistory = false;
                       });
    });


    $ui.bind(ui.events.prevMessage, function () {
        historyLocation -= 1;
        if (historyLocation < 0) {
            historyLocation = messageHistory.length - 1;
        }
        ui.setMessage(messageHistory[historyLocation]);
    });

    $ui.bind(ui.events.nextMessage, function () {
        historyLocation = (historyLocation + 1) % messageHistory.length;
        ui.setMessage(messageHistory[historyLocation]);
    });

    $ui.bind(ui.events.activeRoomChanged, function (ev, room) {
        if (room === 'Lobby') {
            populateLobbyRooms();

            // Remove the active room
            chat.activeRoom = undefined;
        }
        else {
            // When the active room changes update the client state and the cookie
            chat.activeRoom = room;
        }

        ui.scrollToBottom(room);
        updateCookie();
    });

    $ui.bind(ui.events.scrollRoomTop, function (ev, roomInfo) {
        // Do nothing if we're loading history already
        if (loadingHistory === true) {
            return;
        }

        loadingHistory = true;

        // TODO: Show a little animation so the user experience looks fancy
        chat.getPreviousMessages(roomInfo.messageId)
            .done(function (messages) {
                ui.prependChatMessages($.map(messages, getMessageViewModel), roomInfo.name);
                loadingHistory = false;
            })
            .fail(function () {
                loadingHistory = false;
            });
    });

    $(ui).bind(ui.events.preferencesChanged, function (ev) {
        updateCookie();
    });

    $(function () {
        var stateCookie = $.cookie('jabbr.state'),
            state = stateCookie ? JSON.parse(stateCookie) : {};

        // Initialize the ui, passing the user preferences
        ui.initialize(state.preferences);

        ui.addMessage('Welcome to ' + originalTitle, 'notification');
        ui.addMessage('Type /help to see the list of for anewluv chat test', 'notification');

        connection.hub.start(function () {
            chat.join()
                .fail(function (e) {
                    ui.addMessage(e, 'error');
                })
                .done(function (success) {
                    if (success === false) {
                        ui.showLogin();
                        ui.addMessage('Type /login to show the login screen', 'notification');
                    }
                    //if we  are logged in fine send chat user name to UI
                    else {
                        //get the userName and make it avialable to UI
                        chat.getUserName()
                    .done(function (username) {
                        ui.setUserName(username);
                    });
                    }
                    //show the chat info on screen on how to use chat 



                    // get list of available commands
//                    chat.getCommands()
//                        .done(function (commands) {
//                            ui.setCommands(commands);
//                        });
                });
        });

        connection.hub.reconnected(function () {
            if (checkingStatus === true) {
                return;
            }

            checkingStatus = true;

            chat.checkStatus()
                .done(function (requiresUpdate) {
                    if (requiresUpdate === true) {
                        //TO DO not sure we need to update the UI this way anymore since we do ours with modals
                        //
                        //    ui.showUpdateUI();
                    }
                })
                .always(function () {
                    checkingStatus = false;
                });
        });

        connection.hub.disconnected(function () {
            ui.showDisconnectUI();
        });

        connection.hub.error(function (err) {
            // Make all pening messages failed if there's an error
            for (var id in pendingMessages) {
                clearTimeout(pendingMessages[id]);
                ui.failMessage(id);
                delete pendingMessages[id];
            }
        });

    });

})(jQuery, $.connection, window, window.chat.ui, window.chat.utility);