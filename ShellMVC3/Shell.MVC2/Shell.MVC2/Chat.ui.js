/// <reference path="Scripts/jquery-1.7.js" />
/// <reference path="Scripts/jQuery.tmpl.js" />
/// <reference path="Scripts/jquery.cookie.js" />
/// <reference path="Chat.toast.js" />
//jquery.autotabcomplete.js
///Scripts/jquery.livesearch.js")"

//Variablesw added from kelly chat anantgard.com



(function ($, window, document, utility) {
    "use strict";
    var $chatArea = null,
        $tabs = null,
        $submitButton = null,
        $newMessage = null,
        $toast = null,
        $disconnectDialog = null,
        $downloadIcon = null,
        $downloadDialog = null,
        $downloadDialogButton = null,
        $downloadRange = null,
        $ui = null,
        $sound = null,
        templates = null,
        focus = true,
        commands = [],
        Keys = { Up: 38, Down: 40, Esc: 27, Enter: 13 },
        scrollTopThreshold = 75,
        toast = window.chat.toast,
        preferences = null,
        $login = null,
        name,
        lastCycledMessage = null,
        $updatePopup = null,
        $window = $(window),
        $document = $(document),
        $roomFilterInput = null,
        updateTimeout = 15000,
        chatBoxes = new Array(),
        windowFocus = true,
username,
 chatHeartbeatCount = 0,
 minChatHeartbeat = 1000,
 maxChatHeartbeat = 33000,
 chatHeartbeatTime = minChatHeartbeat,
 originalTitle,
 blinkOrder = 0,
 chatboxFocus = new Array(),
newMessages = new Array(),
 newMessagesWin = new Array(),
    $chatboxes = null;





    function getRoomId(roomName) {
        return escape(roomName.toLowerCase()).replace(/[^a-z0-9]/, '_');
    }

    function getUserClassName(userName) {
        return '[data-name="' + userName + '"]';
    }

    function getRoomPreferenceKey(roomName) {
        return '_room_' + roomName;
    }

    //new functions for parsing out dta names and class names
    function getChatBoxCreatorUserName(userName) {
        return '[data-name="' + userName + '"]';
    }

    function getChatBoxInvitedUserName(userName) {
        return '[data-invited="' + userName + '"]';
    }

    function getChatBoxName(creator, invited) {
        return '[id="' + creator + '_' + invited + '"]';
    }

    function getRoomPreferenceKey(roomName) {
        return '_room_' + roomName;
    }


    function Room($tab, $usersContainer, $usersOwners, $usersActive, $usersIdle, $messages, $roomTopic) {
        this.tab = $tab;
        this.users = $usersContainer;
        this.owners = $usersOwners;
        this.activeUsers = $usersActive;
        this.idleUsers = $usersIdle;
        this.messages = $messages;
        this.roomTopic = $roomTopic;

        function glowTab() {
            // Stop if we're not unread anymore
            if (!$tab.hasClass('unread')) {
                return;
            }

            // Go light
            $tab.animate({ backgroundColor: '#e5e5e5', color: '#000000' }, 800, function () {
                // Stop if we're not unread anymore
                if (!$tab.hasClass('unread')) {
                    return;
                }

                // Go dark
                $tab.animate({ backgroundColor: '#164C85', color: '#ffffff' }, 800, function () {
                    // Glow the tab again
                    glowTab();
                });
            });
        }

        this.isLocked = function () {
            return this.tab.hasClass('locked');
        };

        this.isLobby = function () {
            return this.tab.hasClass('lobby');
        };

        this.hasUnread = function () {
            return this.tab.hasClass('unread');
        };

        this.getUnread = function () {
            return $tab.data('unread') || 0;
        };

        this.hasSeparator = function () {
            return this.messages.find('.message-separator').length > 0;
        };

        this.needsSeparator = function () {
            if (this.isActive()) {
                return false;
            }
            return this.isInitialized() && this.getUnread() === 5;
        };

        this.addSeparator = function () {
            if (this.isLobby()) {
                return;
            }

            // find first correct unread message
            var n = this.getUnread(),
                $unread = this.messages.find('.message').eq(-(n + 1));

            $unread.after(templates.separator.tmpl())
                .data('unread', n); // store unread count

            this.scrollToBottom();
        };

        this.removeSeparator = function () {
            this.messages.find('.message-separator').fadeOut(2000, function () {
                $(this).remove();
            });
        };

        this.updateUnread = function (isMentioned) {
            var $tab = this.tab.addClass('unread'),
                $content = $tab.find('.content'),
                unread = ($tab.data('unread') || 0) + 1,
                hasMentions = $tab.data('hasMentions') || isMentioned; // Whether or not the user already has unread messages to him/her

            $content.text((hasMentions ? '*' : '') + '(' + unread + ') ' + this.getName());

            $tab.data('unread', unread);
            $tab.data('hasMentions', hasMentions);

            if (!this.isActive() && unread === 1) {
                // If this room isn't active then we're going to glow the tab
                // to get the user's attention
                glowTab();
            }
        };

        this.scrollToBottom = function () {
            //  this.messages.scrollTop(this.messages[0].scrollHeight);
        };

        this.isNearTheEnd = function () {
            return this.messages.isNearTheEnd();
        };

        this.getName = function () {
            return this.tab.data('name');
        };

        this.isActive = function () {
            return this.tab.hasClass('current');
        };

        this.exists = function () {
            return this.tab.length > 0;
        };

        this.clear = function () {
            this.messages.empty();
            this.owners.empty();
            this.activeUsers.empty();
            this.idleUsers.empty();
        };

        this.makeInactive = function () {
            this.tab.removeClass('current');

            this.messages.removeClass('current')
                         .hide();

            this.users.removeClass('current')
                      .hide();

            this.roomTopic.removeClass('current')
                      .hide();

            if (this.isLobby()) {
                $roomFilterInput.hide();
            }
        };

        this.makeActive = function () {
            var currUnread = this.getUnread(),
                lastUnread = this.messages.find('.message-separator').data('unread') || 0;

            if (!utility.isMobile) {
                $newMessage.focus();
            }

            this.tab.addClass('current')
                    .removeClass('unread')
                    .stop(true, true)
                    .css('backgroundColor', '')
                    .css('color', '')
                    .data('unread', 0)
                    .data('hasMentions', false)
                    .find('.content')
                    .text(this.getName());

            this.messages.addClass('current')
                         .show();

            this.users.addClass('current')
                      .show();

            this.roomTopic.addClass('current')
                      .show();

            if (this.isLobby()) {
                $roomFilterInput.show();
            }
            // if no unread since last separator
            // remove previous separator
            if (currUnread <= lastUnread) {
                this.removeSeparator();
            }
        };

        this.setInitialized = function () {
            this.tab.data('initialized', true);
        };

        this.isInitialized = function () {
            return this.tab.data('initialized') === true;
        };

        // Users
        this.getUser = function (userName) {
            return this.users.find(getUserClassName(userName));
        };

        this.getUserReferences = function (userName) {
            return $.merge(this.getUser(userName),
                           this.messages.find(getUserClassName(userName)));
        };

        this.setLocked = function () {
            this.tab.addClass('locked');
        };

        this.setListState = function (list) {
            if (list.children('li').length > 0) {
                var roomEmptyStatus = list.children('li.empty');
                if (roomEmptyStatus.length == 0) {
                    return;
                } else {
                    roomEmptyStatus.remove();
                    return;
                }
            }
            list.append($('<li class="empty">No users</li>'));
        };

        this.addUser = function (userViewModel, $user) {
            if (userViewModel.active) {
                this.addUserToList($user, this.activeUsers);
            } else if (userViewModel.owner) {
                this.addUserToList($user, this.owners);
            }
            else {
                this.addUserToList($user, this.idleUsers);
            }
        };

        this.addUserToList = function ($user, list) {
            var oldParentList = $user.parent('ul');
            $user.appendTo(list);
            this.setListState(list);
            if (typeof oldParentList != undefined) {
                this.setListState(oldParentList);
            }
        };

        this.appearsInList = function ($user, list) {
            return $user.parent('ul').attr('id') == list.attr('id');
        };

        this.updateUserStatus = function ($user) {
            var owner = $user.data('owner') || false;

            if (owner === true) {
                if (!this.appearsInList($user, this.owners)) {
                    this.addUserToList($user, this.owners);
                }
                return;
            }

            var status = $user.data('active');
            if (typeof status === "undefined") {
                return;
            }

            if (status === true) {
                if (!this.appearsInList($user, this.activeUsers)) {
                    this.addUserToList($user, this.activeUsers);
                }
            } else {
                if (!this.appearsInList($user, this.idleUsers)) {
                    this.addUserToList($user, this.idleUsers);
                }
            }
        };

        this.sortLists = function () {
            this.sortList(this.activeUsers);
            this.sortList(this.idleUsers);
        };

        this.sortList = function (listToSort) {
            var listItems = listToSort.children('li').get();
            listItems.sort(function (a, b) {
                var compA = $(a).data('name').toString().toUpperCase();
                var compB = $(b).data('name').toString().toUpperCase();
                return (compA < compB) ? -1 : (compA > compB) ? 1 : 0;
            })
            $.each(listItems, function (index, item) { listToSort.append(item); });
        };
    }

    function getRoomElements(roomName) {
        var roomId = getRoomId(roomName);
        var room = new Room($('#tabs-' + roomId),
                        $('#userlist-' + roomId),
                        $('#userlist-' + roomId + '-owners'),
                        $('#userlist-' + roomId + '-active'),
                        $('#userlist-' + roomId + '-idle'),
                        $('#messages-' + roomId),
                        $('#roomTopic-' + roomId));
        return room;
    }

    function getCurrentRoomElements() {
        var room = new Room($tabs.find('li.current'),
                        $('.users.current'),
                        $('.userlist.current .owners'),
                        $('.userlist.current .active'),
                        $('.userlist.current .idle'),
                        $('.messages.current'),
                        $('.roomTopic.current'));
        return room;
    }

    ///for chatbox  select
    function Chatbox($chatbox, $usersContainer, $usersOwners, $usersActive, $usersIdle, $messages, $roomTopic) {
        this.chatbox = $chatbox;
        this.users = $usersContainer;
        this.owners = $usersOwners;
        this.activeUsers = $usersActive;
        this.idleUsers = $usersIdle;
        this.messages = $messages;
        this.roomTopic = $roomTopic;

        function glowchatbox() {
            // Stop if we're not unread anymore
            if (!$chatbox.hasClass('unread')) {
                return;
            }

            // Go light
            $chatbox.animate({ backgroundColor: '#e5e5e5', color: '#000000' }, 800, function () {
                // Stop if we're not unread anymore
                if (!$chatbox.hasClass('unread')) {
                    return;
                }

                // Go dark
                $chatbox.animate({ backgroundColor: '#164C85', color: '#ffffff' }, 800, function () {
                    // Glow the chatbox again
                    glowchatbox();
                });
            });
        }

        this.isLocked = function () {
            return this.chatbox.hasClass('locked');
        };

        this.isLobby = function () {
            return this.chatbox.hasClass('lobby');
        };

        this.hasUnread = function () {
            return this.chatbox.hasClass('unread');
        };

        this.getUnread = function () {
            return $chatbox.data('unread') || 0;
        };

        this.hasSeparator = function () {
            return this.messages.find('.message-separator').length > 0;
        };

        this.needsSeparator = function () {
            if (this.isActive()) {
                return false;
            }
            return this.isInitialized() && this.getUnread() === 5;
        };

        this.addSeparator = function () {
            if (this.isLobby()) {
                return;
            }

            // find first correct unread message
            var n = this.getUnread(),
                $unread = this.messages.find('.message').eq(-(n + 1));

            $unread.after(templates.separator.tmpl())
                .data('unread', n); // store unread count

            this.scrollToBottom();
        };

        this.removeSeparator = function () {
            this.messages.find('.message-separator').fadeOut(2000, function () {
                $(this).remove();
            });
        };

        this.updateUnread = function (isMentioned) {
            var $chatbox = this.chatbox.addClass('unread'),
                $content = $chatbox.find('.content'),
                unread = ($chatbox.data('unread') || 0) + 1,
                hasMentions = $chatbox.data('hasMentions') || isMentioned; // Whether or not the user already has unread messages to him/her

            $content.text((hasMentions ? '*' : '') + '(' + unread + ') ' + this.getName());

            $chatbox.data('unread', unread);
            $chatbox.data('hasMentions', hasMentions);

            if (!this.isActive() && unread === 1) {
                // If this room isn't active then we're going to glow the chatbox
                // to get the user's attention
                glowchatbox();
            }
        };

        this.scrollToBottom = function () {
            //  this.messages.scrollTop(this.messages[0].scrollHeight);
        };

        this.isNearTheEnd = function () {
            return this.messages.isNearTheEnd();
        };

        this.getName = function () {
            return this.chatbox.data('name');
        };

        this.isActive = function () {
            return this.chatbox.hasClass('current');
        };

        this.exists = function () {
            return this.chatbox.length > 0;
        };

        this.clear = function () {
            this.messages.empty();
            this.owners.empty();
            this.activeUsers.empty();
            this.idleUsers.empty();
        };

        this.makeInactive = function () {
            this.chatbox.removeClass('current');

            this.messages.removeClass('current')
                         .hide();

            this.users.removeClass('current')
                      .hide();

            this.roomTopic.removeClass('current')
                      .hide();

            if (this.isLobby()) {
                $roomFilterInput.hide();
            }
        };

        this.makeActive = function () {
            var currUnread = this.getUnread(),
                lastUnread = this.messages.find('.message-separator').data('unread') || 0;

            if (!utility.isMobile) {
                $newMessage.focus();
            }

            this.chatbox.addClass('current')
                    .removeClass('unread')
                    .stop(true, true)
                    .css('backgroundColor', '')
                    .css('color', '')
                    .data('unread', 0)
                    .data('hasMentions', false)
                    .find('.content')
                    .text(this.getName());

            this.messages.addClass('current')
                         .show();

            this.users.addClass('current')
                      .show();

            this.roomTopic.addClass('current')
                      .show();

            if (this.isLobby()) {
                $roomFilterInput.show();
            }
            // if no unread since last separator
            // remove previous separator
            if (currUnread <= lastUnread) {
                this.removeSeparator();
            }
        };

        this.setInitialized = function () {
            this.chatbox.data('initialized', true);
        };

        this.isInitialized = function () {
            return this.chatbox.data('initialized') === true;
        };

        // Users
        this.getUser = function (userName) {
            return this.users.find(getUserClassName(userName));
        };

        this.getUserReferences = function (userName) {
            return $.merge(this.getUser(userName),
                           this.messages.find(getUserClassName(userName)));
        };

        this.setLocked = function () {
            this.chatbox.addClass('locked');
        };

        this.setListState = function (list) {
            if (list.children('li').length > 0) {
                var roomEmptyStatus = list.children('li.empty');
                if (roomEmptyStatus.length == 0) {
                    return;
                } else {
                    roomEmptyStatus.remove();
                    return;
                }
            }
            list.append($('<li class="empty">No users</li>'));
        };

        this.addUser = function (userViewModel, $user) {
            if (userViewModel.active) {
                this.addUserToList($user, this.activeUsers);
            } else if (userViewModel.owner) {
                this.addUserToList($user, this.owners);
            }
            else {
                this.addUserToList($user, this.idleUsers);
            }
        };

        this.addUserToList = function ($user, list) {
            var oldParentList = $user.parent('ul');
            $user.appendTo(list);
            this.setListState(list);
            if (typeof oldParentList != undefined) {
                this.setListState(oldParentList);
            }
        };

        this.appearsInList = function ($user, list) {
            return $user.parent('ul').attr('id') == list.attr('id');
        };

        this.updateUserStatus = function ($user) {
            var owner = $user.data('owner') || false;

            if (owner === true) {
                if (!this.appearsInList($user, this.owners)) {
                    this.addUserToList($user, this.owners);
                }
                return;
            }

            var status = $user.data('active');
            if (typeof status === "undefined") {
                return;
            }

            if (status === true) {
                if (!this.appearsInList($user, this.activeUsers)) {
                    this.addUserToList($user, this.activeUsers);
                }
            } else {
                if (!this.appearsInList($user, this.idleUsers)) {
                    this.addUserToList($user, this.idleUsers);
                }
            }
        };

        this.sortLists = function () {
            this.sortList(this.activeUsers);
            this.sortList(this.idleUsers);
        };

        this.sortList = function (listToSort) {
            var listItems = listToSort.children('li').get();
            listItems.sort(function (a, b) {
                var compA = $(a).data('name').toString().toUpperCase();
                var compB = $(b).data('name').toString().toUpperCase();
                return (compA < compB) ? -1 : (compA > compB) ? 1 : 0;
            })
            $.each(listItems, function (index, item) { listToSort.append(item); });
        };
    }



    function getChatboxElements(chatboxName) {
        var roomId = getRoomId(chatboxName);
        var room = new Room($('#chatbox-' + roomId),
                        $('#userlist-' + roomId),
                        $('#userlist-' + roomId + '-owners'),
                        $('#userlist-' + roomId + '-active'),
                        $('#userlist-' + roomId + '-idle'),
                        $('#messages-' + roomId),
                        $('#roomTopic-' + roomId));
        return room;
    }



    function getAllRoomElements() {
        var rooms = [];
        $("ul#tabs > li.room").each(function () {
            rooms[rooms.length] = getRoomElements($(this).data("name"));
        });
        return rooms;
    }

    function getLobby() {
        return getRoomElements('Lobby');
    }

    function updateLobbyRoomCount(room, count) {
        var lobby = getLobby(),
            $room = lobby.users.find('[data-room="' + room.Name + '"]'),
            $count = $room.find('.count');

        $room.css('background-color', '#f5f5f5');
        $count.text(' (' + count + ')');

        if (room.Private === true) {
            $room.addClass('locked');
        }

        // Do a little animation
        $room.animate({ backgroundColor: '#e5e5e5' }, 800);
    }


    function addRoom(roomName) {
        // Do nothing if the room exists
        var room = getRoomElements(roomName),
            roomId = null,
            viewModel = null,
            $messages = null,
            $roomTopic = null,
            scrollHandler = null,
            userContainer = null;

        if (room.exists()) {
            return false;
        }

        roomId = getRoomId(roomName);

        // Add the tab
        viewModel = {
            id: roomId,
            name: roomName
        };

        templates.tab.tmpl(viewModel).appendTo($tabs);

        $messages = $('<ul/>').attr('id', 'messages-' + roomId)
                              .addClass('messages')
                              .appendTo($chatArea)
                              .hide();

        $roomTopic = $('<div/>').attr('id', 'roomTopic-' + roomId)
                              .addClass('roomTopic')
                              .appendTo($chatArea)
                              .hide();

        if (roomName !== "lobby") {
            userContainer = $('<div/>').attr('id', 'userlist-' + roomId)
                .addClass('users')
                .appendTo($chatArea).hide();
            templates.userlist.tmpl({ listname: 'Room Owners', id: 'userlist-' + roomId + '-owners' })
                .addClass('owners')
                .appendTo(userContainer);
            templates.userlist.tmpl({ listname: 'Online', id: 'userlist-' + roomId + '-active' })
                .addClass('active')
                .appendTo(userContainer);
            templates.userlist.tmpl({ listname: 'Away', id: 'userlist-' + roomId + '-idle' })
                .addClass('idle')
                .appendTo(userContainer);
            userContainer.find('h3').click(function () {
                $(this).next().toggle(0);
                return false;
            });
        } else {
            $('<ul/>').attr('id', 'userlist-' + roomId)
                .addClass('users')
                .appendTo($chatArea).hide();
        }

        $tabs.find('li')
            .not('.lobby')
            .sortElements(function (a, b) {
                return $(a).data('name').toLowerCase() > $(b).data('name').toLowerCase() ? 1 : -1;
            });

        scrollHandler = function (ev) {
            var messageId = null;

            // Do nothing if there's nothing else
            if ($(this).data('full') === true) {
                return;
            }

            // If you're we're near the top, raise the event
            if ($(this).scrollTop() <= scrollTopThreshold) {
                var $child = $messages.children('.message:first');
                if ($child.length > 0) {
                    messageId = $child.attr('id')
                                      .substr(2); // Remove the "m-"
                    $ui.trigger(ui.events.scrollRoomTop, [{ name: roomName, messageId: messageId}]);
                }
            }
        };

        // Hookup the scroll handler since event delegation doesn't work with scroll events
        $messages.bind('scroll', scrollHandler);

        // Store the scroll handler so we can remove it later
        $messages.data('scrollHandler', scrollHandler);

        setAccessKeys();
        return true;
    }

    //creates chat room using roominfo
    function createchatroom(roominfo) {

        $ui.trigger(ui.events.acceptChatRequest, roominfo);
    }

    //sends message to chat request initator statting that request  was denied
    function rejectchatrequest(roominfo) {
        $ui.trigger(ui.events.rejectChatRequest, roominfo);
    }

    function removeRoom(roomName) {
        var room = getRoomElements(roomName),
            scrollHandler = null;

        if (room.exists()) {
            // Remove the scroll handler from this room
            scrollHandler = room.messages.data('scrollHandler');
            room.messages.unbind('scrollHandler', scrollHandler);

            room.tab.remove();
            room.messages.remove();
            room.users.remove();
            setAccessKeys();
        }
    }

    function setAccessKeys() {
        $.each($tabs.find('li.room'), function (index, item) {
            $(item).children('button').attr('accesskey', getRoomAccessKey(index));
        });
    }

    function getRoomAccessKey(index) {
        if (index < 10) {
            return index + 1;
        }
        return 0;
    }

    function navigateToRoom(roomName) {
        $.history.load('/rooms/' + roomName);
    }

    function processMessage(message) {
        //TO DO re-instate this if we want to integrate with content providers
        // var isFromCollapibleContentProvider = message.message.indexOf('class="collapsible_box"') > -1;
        // message.message = isFromCollapibleContentProvider ? message.message : utility.parseEmojis(message.message);
        message.message = utility.parseEmojis(message.message);
        message.trimmedName = utility.trim(message.name, 21);
        message.when = message.date.formatTime(true);
        message.fulldate = message.date.toLocaleString();
    }

    function processChatBoxMessage(message) {
        //TO DO re-instate this if we want to integrate with content providers
        // var isFromCollapibleContentProvider = message.message.indexOf('class="collapsible_box"') > -1;
        // message.message = isFromCollapibleContentProvider ? message.message : utility.parseEmojis(message.message);
        message.message = utility.parseEmojis(message.message);
        message.trimmedName = utility.trim(message.name, 21);
        message.when = message.date.formatTime(true);
        message.fulldate = message.date.toLocaleString();
        return message;
    }

    function triggerFocus() {
        ui.focus = true;
        $ui.trigger(ui.events.focusit);
    }

    function loadPreferences() {
        // Restore the global preferences

    }

    function toggleElement($element, preferenceName, roomName) {
        var value = roomName ? getRoomPreference(roomName, preferenceName) : preferences[preferenceName];
        if (value === true) {
            $element.removeClass('off');
        }
        else {
            $element.addClass('off');
        }
    }

    function loadRoomPreferences(roomName) {
        var roomPreferences = getRoomPreference(roomName);

        // Placeholder for room level preferences
        toggleElement($sound, 'hasSound', roomName);
        toggleElement($toast, 'canToast', roomName);
    }

    function setPreference(name, value) {
        preferences[name] = value;

        $(ui).trigger(ui.events.preferencesChanged);
    }

    function setRoomPreference(roomName, name, value) {
        var roomPreferences = preferences[getRoomPreferenceKey(roomName)];

        if (!roomPreferences) {
            roomPreferences = {};
            preferences[getRoomPreferenceKey(roomName)] = roomPreferences;
        }

        roomPreferences[name] = value;

        $ui.trigger(ui.events.preferencesChanged);
    }

    function getRoomPreference(roomName, name) {
        return (preferences[getRoomPreferenceKey(roomName)] || {})[name];
    }

    function getActiveRoomPreference(name) {
        var room = getCurrentRoomElements();
        return getRoomPreference(room.getName(), name);
    }

    function anyRoomPreference(name, value) {
        for (var key in preferences) {
            if (preferences[key][name] === value) {
                return true;
            }
        }
        return false;
    }

    function triggerSend() {
        var msg = $.trim($newMessage.val());


        if (msg) {
            if (msg.toLowerCase() == '/login') {
                ui.showLogin();
            }
            else {
                $ui.trigger(ui.events.sendMessage, [msg]);
            }
        }

        $newMessage.val('');
        $newMessage.focus();

        triggerFocus();

        // always scroll to bottom after new message sent
        var room = getCurrentRoomElements();
        room.scrollToBottom();
        room.removeSeparator();
    }
    //overload of triggger send for chat boxes
    function triggerSend(event, chatboxtextarea, chatboxtitle) {
        var msg = $.trim($newMessage.val());

        //var test = 'wego';

        var senderscreenname = ui.getUserName();

        msg = $(chatboxtextarea).val();


        //we allow slash commands
        //msg = msg.replace(/^\s+|\s+$/g, "");



        $(chatboxtextarea).val('');
        $(chatboxtextarea).focus();
        $(chatboxtextarea).css('height', '44px');

        if (msg) {
            if (msg.toLowerCase() == '/login') {
                ui.showLogin();
            }
            else {
                $ui.trigger(ui.events.sendMessage, [{ msg: msg, chatboxtitle: chatboxtitle, chatboxtextarea: chatboxtextarea}]);
            }
        }

        // $newMessage.val('');
        // $newMessage.focus();
        $(chatboxtextarea).focus();
        triggerFocus();

        // always scroll to bottom after new message sent
        // var room = getCurrentRoomElements();
        // room.scrollToBottom();
        // room.removeSeparator();
    }



    function updateNote(userViewModel, $user) {
        var $note = $user.find('.note'),
            noteText = userViewModel.note,
            noteTextEncoded = null,
            requireRoomUpdate = false;

        if (userViewModel.noteClass === 'afk') {
            noteText = userViewModel.note + ' (' + userViewModel.timeAgo + ')';
            requireRoomUpdate = ui.setUserInActive($user);
        }
        else if (userViewModel.active) {
            requireRoomUpdate = ui.setUserActive($user);
        }
        else {
            requireRoomUpdate = ui.setUserInActive($user);
        }

        noteTextEncoded = $('<div/>').html(noteText).text();

        // Remove all classes and the text
        $note.removeClass('afk message');
        $note.removeAttr('title');

        $note.addClass(userViewModel.noteClass);
        if (userViewModel.note) {
            $note.attr('title', noteTextEncoded);
        }

        if (requireRoomUpdate) {
            $user.each(function () {
                var room = getRoomElements($(this).data('inroom'));
                room.updateUserStatus($(this));
                room.sortLists();
            });
        }
    }

    function updateFlag(userViewModel, $user) {
        var $flag = $user.find('.flag');

        $flag.removeClass();
        $flag.addClass('flag');
        $flag.removeAttr('title');

        $flag.addClass(userViewModel.flagClass);
        if (userViewModel.country) {
            $flag.attr('title', userViewModel.country);
        }
    }

    function updateRoomTopic(roomViewModel) {
        var room = getRoomElements(roomViewModel.Name);
        var topic = roomViewModel.Topic;
        var topicHtml = topic === '' ? 'You\'re chatting in ' + roomViewModel.Name : '<strong>Topic: </strong>' + topic;
        room.roomTopic.html(topicHtml);
    }


    function toggleChatBoxGrowth(chatboxtitle) {
        if ($('#chatbox_' + chatboxtitle + ' .chatboxcontent').css('display') == 'none') {

            var minimizedChatBoxes = new Array();

            if ($.cookie('chatbox_minimized')) {
                minimizedChatBoxes = $.cookie('chatbox_minimized').split(/\|/);
            }

            var newCookie = '';

            for (var i = 0; i < minimizedChatBoxes.length; i++) {
                if (minimizedChatBoxes[i] != chatboxtitle) {
                    newCookie += chatboxtitle + '|';
                }
            }

            newCookie = newCookie.slice(0, -1)


            $.cookie('chatbox_minimized', newCookie);
            $('#chatbox_' + chatboxtitle + ' .chatboxcontent').css('display', 'block');
            $('#chatbox_' + chatboxtitle + ' .chatboxinput').css('display', 'block');
            $("#chatbox_" + chatboxtitle + " .chatboxcontent").scrollTop($("#chatbox_" + chatboxtitle + " .chatboxcontent")[0].scrollHeight);
        } else {

            var newCookie = chatboxtitle;

            if ($.cookie('chatbox_minimized')) {
                newCookie += '|' + $.cookie('chatbox_minimized');
            }


            $.cookie('chatbox_minimized', newCookie);
            $('#chatbox_' + chatboxtitle + ' .chatboxcontent').css('display', 'none');
            $('#chatbox_' + chatboxtitle + ' .chatboxinput').css('display', 'none');
        }

    }

    function closeChatBox(chatboxtitle) {
        $('#chatbox_' + chatboxtitle).css('display', 'none');
        ui.restructureChatBoxes();

        //$.post("chat.php?action=closechat", { chatbox: chatboxtitle }, function (data) {
        // });

    }

    function checkChatBoxInputKey(event, chatboxtextarea, chatboxtitle) {

        if (event.keyCode == 13 && event.shiftKey == 0) {
            message = $(chatboxtextarea).val();
            message = message.replace(/^\s+|\s+$/g, "");

            $(chatboxtextarea).val('');
            $(chatboxtextarea).focus();
            $(chatboxtextarea).css('height', '44px');
            if (message != '') {
                $.post("chat.php?action=sendchat", { to: chatboxtitle, message: message }, function (data) {
                    message = message.replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\"/g, "&quot;");
                    $("#chatbox_" + chatboxtitle + " .chatboxcontent").append('<div class="chatboxmessage"><span class="chatboxmessagefrom">' + username + ':&nbsp;&nbsp;</span><span class="chatboxmessagecontent">' + message + '</span></div>');
                    $("#chatbox_" + chatboxtitle + " .chatboxcontent").scrollTop($("#chatbox_" + chatboxtitle + " .chatboxcontent")[0].scrollHeight);
                });
            }
            chatHeartbeatTime = minChatHeartbeat;
            chatHeartbeatCount = 1;

            return false;
        }

        var adjustedHeight = chatboxtextarea.clientHeight;
        var maxHeight = 94;

        if (maxHeight > adjustedHeight) {
            adjustedHeight = Math.max(chatboxtextarea.scrollHeight, adjustedHeight);
            if (maxHeight)
                adjustedHeight = Math.min(maxHeight, adjustedHeight);
            if (adjustedHeight > chatboxtextarea.clientHeight)
                $(chatboxtextarea).css('height', adjustedHeight + 8 + 'px');
        } else {
            $(chatboxtextarea).css('overflow', 'auto');
        }

    }




    var ui = {

        //lets store any events to be triggered as constants here to aid intellisense and avoid
        //string duplication everywhere
        events: {

            acceptChatRequest: 'acceptChatRequest',
            rejectChatRequest: 'rejectChatRequest',
            createRoom: 'createRoom',
            leaveRoom: 'leaveRoom',
            changeRoomTopic: 'changeRoomTopic',
            closeRoom: 'closeRoom',
            prevMessage: 'prevMessage',
            openRoom: 'openRoom',
            nextMessage: 'nextMessage',
            createChatRequest: 'createChatRequest',
            activeRoomChanged: 'activeRoomChanged',
            scrollRoomTop: 'scrollRoomTop',
            typing: 'typing',
            sendMessage: 'sendMessage',
            focusit: 'focusit',
            blurit: 'blurit',
            preferencesChanged: 'preferencesChanged'
        },

        initialize: function (state) {
            $ui = $(this);
            preferences = state || {};
            $chatArea = $('#chat-area');
            $tabs = $('#tabs');
            //added global to hold all chatboxes
            $chatboxes = $('.chatbox');
            $submitButton = $('#send');
            $newMessage = $('#new-message');
            // $newMessage = $('.chatboxtextarea .chatboxtextareaselected'); //changed it to trigger off our chatbox text area instead of jabra chats
            $toast = $('#preferences .toast');
            $sound = $('#preferences .sound');
            $downloadIcon = $('#preferences .download');
            $downloadDialog = $('#download-dialog');
            $downloadDialogButton = $('#download-dialog-button');
            $downloadRange = $('#download-range');
            $disconnectDialog = $('#disconnect-dialog');
            $login = $('.janrainEngage');
            $updatePopup = $('#jabbr-update');
            focus = true;
            $roomFilterInput = $('#users-filter');
            templates = {
                userlist: $('#new-userlist-template'),
                user: $('#new-user-template'),
                message: $('#new-message-template'),
                notification: $('#new-notification-template'),
                separator: $('#message-separator-template'),
                tab: $('#new-tab-template')
            };

            if (toast.canToast()) {
                $toast.show();
            }
            else {
                $downloadIcon.css({ left: '26px' });
                // We need to set the toast setting to false
                preferences.canToast = false;
            }

            // DOM events
            $document.on('click', 'h3.collapsible_title', function () {
                var $message = $(this).closest('.message'),
                    nearEnd = ui.isNearTheEnd();

                $(this).next().toggle(0, function () {
                    if (nearEnd) {
                        ui.scrollToBottom();
                    }
                });
            });


            //#region "New added UI commands for front end clicks added for AnewluvChat "




            //events for kelly chat
            //events that initiate private chat request
            //click event to open chat window direcrly ,i.e for freinds and such
            $document.on('click', '#InviteToChat', function (ev) {
                var Topic = $(this).data('name');
                var TopicStarter = ui.getUserName();

                if (typeof TopicStarter == 'undefined') {
                    $(window.location).attr('href', '/Account/Logon');
                    window.location = '/EditProfile/EditProfileBasicSettings';
                }
                else {
                    var chatrequestboxname = TopicStarter + '_' + Topic;
                    //create the room if it does not exist
                    $ui.trigger(ui.events.createChatRequest, [{ Topic: Topic, TopicStarter: TopicStarter, chatrequestboxname: chatrequestboxname}]);
                }

            });

            //temp method for closing invite to chat 
            $document.on('click', '#CloseInviteToChat', function (ev) {
                var Topic = $(this).data('name');
                var TopicStarter = ui.getUserName();

                if (typeof TopicStarter == 'undefined') {
                    $(window.location).attr('href', '/Account/Logon');
                    window.location = '/EditProfile/EditProfileBasicSettings';
                }
                else {
                    var Roomname = TopicStarter + '_' + Topic;
                    //create the room if it does not exist
                    $ui.trigger(ui.events.closeRoom, [{ Topic: Topic, TopicStarter: TopicStarter, name: Roomname}]);
                }

            });


            //click event to open chat window direcrly ,i.e for freinds and such
            $document.on('click', '#ChatWithUser', function (ev) {
                var Topic = $(this).data('name');
                var TopicStarter = ui.getUserName();

                if (typeof TopicStarter == 'undefined') {
                    $(window.location).attr('href', '/EditProfile/EditProfileBasicSettings');
                    window.location = '/EditProfile/EditProfileBasicSettings';
                }
                else {
                    var chatboxname = TopicStarter + '_' + Topic;
                    //create the room if it does not exist
                    $ui.trigger(ui.events.createRoom, [{ Topic: Topic, TopicStarter: TopicStarter, chatboxname: chatboxname}]);

                }

            });
            //click event to close chat window
            $document.on('click', '.closechat', function (ev) {
                var Topic = $(this).data('name');
                //get the roomname from UI since in this case it already exists               
                var fsfs = $(this).parent(2).attr("id");
                var chatboxname = $(this).parent().parent().parent().attr("id").replace('chatbox_', '');
                var TopicStarter = ui.getUserName();

                if (typeof TopicStarter == 'undefined') {
                    $(window.location).attr('href', '/EditProfile/EditProfileBasicSettings');
                    window.location = '/EditProfile/EditProfileBasicSettings';
                }
                else {
                    //create the room if it does not exist
                    $ui.trigger(ui.events.leaveRoom, [chatboxname]);
                    $('#chatbox_' + chatboxname).css('display', 'none');
                    closeChatBox(chatboxname);
                }
            });

            //click event to minimize chat window
            $document.on('click', '.minimizechat', function (ev) {
                var Topic = $(this).data('name');
                //get the roomname from UI since in this case it already exists               
                var fsfs = $(this).parent(2).attr("id");
                var chatboxname = $(this).parent().parent().parent().attr("id").replace('chatbox_', '');
                var senderScreenName = ui.getUserName();
                if (typeof senderScreenName == 'undefined') {
                    $(window.location).attr('href', '/EditProfile/EditProfileBasicSettings');
                    window.location = '/EditProfile/EditProfileBasicSettings';
                }
                else {


                    toggleChatBoxGrowth(chatboxname)
                }
            });

            //  handles typing in chat boxes
            $document.on('keydown', '.chatboxtextarea', function (ev) {
                //chatbox title withoute the chatbox part
                if (ev.keyCode == 9) {  //tab pressed
                    ev.preventDefault(); // stops its action
                    return false;
                }

                var key = ev.keyCode || ev.which;
                switch (key) {
                    case Keys.Up:
                        cycleMessage(ui.events.prevMessage);
                        break;
                    case Keys.tab:

                        break;
                    case Keys.Down:
                        cycleMessage(ui.events.nextMessage);
                        break;
                    case Keys.Esc:
                        $(this).val('');
                        break;
                    case Keys.Enter:
                        //var fsfs = $(this).parent().parent();
                        var chatboxtitle = $(this).parent().parent().attr("id").replace('chatbox_', '');
                        //modifed for use with chat boxes
                        //TO make a custom trigger for chat boxes so we dont break the group chat
                        triggerSend(ev, this, chatboxtitle);
                        ev.preventDefault();
                        return false;
                }
            });







            // Auto-complete for user names
            $(".chatboxtextarea").live('focus', function () {

                $(".chatboxtextarea").autoTabComplete({

                    prefixMatch: '[@#/\:]',
                    get: function (prefix) {
                        switch (prefix) {
                            case '@':
                                var room = getCurrentRoomElements();
                                // exclude current username from autocomplete
                                return room.users.find('li[data-name != "' + ui.getUserName() + '"]')
                                         .not('.room')
                                         .map(function () { return ($(this).data('name') || "").toString(); });
                            case '#':
                                var lobby = getLobby();
                                return lobby.users.find('li')
                                         .map(function () { return $(this).data('name'); });

                            case '/':
                                var commands = ui.getCommands();
                                return ui.getCommands()
                                         .map(function (cmd) { return cmd.Name; });

                            case ':':
                                return Emoji.getIcons();
                            default:
                                return [];
                        }
                    }
                });

            });





            $document.on('keypress', '.chatboxtextarea', function (ev) {
                var key = ev.keyCode || ev.which;
                switch (key) {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Esc:
                    case Keys.Enter:
                        break;
                    default:
                        $ui.trigger(ui.events.typing);
                        break;
                }
            });

            //#endregion


            $document.on('click', '#tabs li', function () {
                ui.setActiveRoom($(this).data('name'))
            });

            $document.on('click', 'li.room', function () {
                var roomName = $(this).data('name');

                navigateToRoom(roomName);

                return false;
            });

            $document.on('click', '#tabs li .close', function (ev) {
                var roomName = $(this).closest('li').data('name');

                $ui.trigger(ui.events.closeRoom, [roomName]);

                ev.preventDefault();
                return false;
            });

            // handle click on notifications
            $document.on('click', '.notification a.info', function (ev) {
                var $notification = $(this).closest('.notification');

                if ($(this).hasClass('collapse')) {
                    ui.collapseNotifications($notification);
                }
                else {
                    ui.expandNotifications($notification);
                }
            });

            $submitButton.click(function (ev) {
                triggerSend();

                ev.preventDefault();
                return false;
            });

            $sound.click(function () {
                var room = getCurrentRoomElements();

                if (room.isLobby()) {
                    return;
                }

                $(this).toggleClass('off');

                var enabled = !$(this).hasClass('off');

                // Store the preference
                setRoomPreference(room.getName(), 'hasSound', enabled);
            });

            $toast.click(function () {
                var $this = $(this),
                    enabled = !$this.hasClass('off'),
                    room = getCurrentRoomElements();

                if (room.isLobby()) {
                    return;
                }

                if (enabled) {
                    // If it's enabled toggle the preference
                    setRoomPreference(room.getName(), 'canToast', !enabled);
                    $this.toggleClass('off');
                }
                else {
                    toast.enableToast()
                    .done(function () {
                        setRoomPreference(room.getName(), 'canToast', true);
                        $this.removeClass('off');
                    })
                    .fail(function () {
                        setRoomPreference(room.getName(), 'canToast', false);
                        $this.addClass('off');
                    });
                }
            });

            $(toast).bind('toast.focus', function (ev, room) {
                window.focus();

                if (room) {
                    ui.setActiveRoom(room);
                }
            });

            $downloadIcon.click(function () {
                var room = getCurrentRoomElements();

                if (room.isLobby()) {
                    return; //Show a message?
                }

                if (room.isLocked()) {
                    return; //Show a message?
                }

                $downloadDialog.modal({ backdrop: true, keyboard: true });
            });

            $downloadDialogButton.click(function () {
                var room = getCurrentRoomElements();

                var url = document.location.href;
                var nav = url.indexOf('#');
                url = nav > 0 ? url.substring(0, nav) : url;
                url = url.replace('index.htm', '');
                url += 'api/v1/messages/' +
                       encodeURI(room.getName()) +
                       '?download=true&range=' +
                       encodeURIComponent($downloadRange.val());

                $('<iframe style="display:none">').attr('src', url).appendTo(document.body);

                $downloadDialog.modal('hide');
            });

            $window.blur(function () {
                ui.focus = false;
                $ui.trigger(ui.events.blurit);
            });

            $window.focus(function () {
                // clear unread count in active room
                var room = getCurrentRoomElements();
                room.makeActive();
                triggerFocus();
            });

            $window.resize(function () {
                var room = getCurrentRoomElements();
                room.scrollToBottom();
            });

            $newMessage.keydown(function (ev) {
                var key = ev.keyCode || ev.which;
                switch (key) {
                    case Keys.Up:
                        cycleMessage(ui.events.prevMessage);
                        break;
                    case Keys.Down:
                        cycleMessage(ui.events.nextMessage);
                        break;
                    case Keys.Esc:
                        $(this).val('');
                        break;
                    case Keys.Enter:
                        //  triggerSend();
                        //   ev.preventDefault();
                        //   return false;
                        //var fsfs = $(this).parent().parent();
                        var chatboxtitle = $(this).parent().parent().attr("id").replace('chatbox_', '');
                        //modifed for use with chat boxes
                        //TO make a custom trigger for chat boxes so we dont break the group chat
                        triggerSend(ev, this, chatboxtitle);
                        ev.preventDefault();
                        return false;
                }
            });

            function cycleMessage(messageHistoryDirection) {
                var currentMessage = $newMessage[0].value;
                if (currentMessage.length === 0 || lastCycledMessage === currentMessage) {
                    $ui.trigger(messageHistoryDirection);
                }
            }

            // Auto-complete for user names
            $newMessage.autoTabComplete({

                prefixMatch: '[@#/\:]',
                get: function (prefix) {
                    switch (prefix) {
                        case '@':
                            var room = getCurrentRoomElements();
                            // exclude current username from autocomplete
                            return room.users.find('li[data-name != "' + ui.getUserName() + '"]')
                                         .not('.room')
                                         .map(function () { return ($(this).data('name') || "").toString(); });
                        case '#':
                            var lobby = getLobby();
                            return lobby.users.find('li')
                                         .map(function () { return $(this).data('name'); });

                        case '/':
                            var commands = ui.getCommands();
                            return ui.getCommands()
                                         .map(function (cmd) { return cmd.Name; });

                        case ':':
                            return Emoji.getIcons();
                        default:
                            return [];
                    }
                }
            });

            $newMessage.keypress(function (ev) {
                var key = ev.keyCode || ev.which;
                switch (key) {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Esc:
                    case Keys.Enter:
                        break;
                    default:
                        $ui.trigger(ui.events.typing);
                        break;
                }
            });

            $newMessage.focus();

            // Make sure we can toast at all
            toast.ensureToast(preferences);

            // Load preferences
            loadPreferences();

            // Initilize liveUpdate plugin for room search
            ui.$roomFilter = $roomFilterInput.liveUpdate('#userlist-lobby', true);
        },
        showDynamicNotificationDialog: function (message, title, ismodal, dialogid) {

            //falback method in case we have problems
            //var NewDialog = $('<div id="dialog-test">\ <p>This is your dialog content, which can be multiline and dynamic.</p> </div>');


            //another cleaner way of building your div
            var NewDialog = $(document.createElement('div'));
            NewDialog.html(message);
            //add an ID if it it was passed
            if (dialogid) {
                NewDialog.attr("id", dialogid);
            }
            if (!ismodal) {
                ismodal = false;
            }

            //newDiv.dialog();
            // NewDialog.dialog('open');

            //  var NewDialog = $(document.createElement('div'));
            //  NewDialog.html('hello there');
            //  NewDialog.attr("id", "placeholder");
            //           $("body").css("background", (n < 2) ? "green" : "orange");


            NewDialog.dialog({
                modal: ismodal,
                title: (title) ? title : 'Notification',
                autoOpen: true,
                width: 300,
                show: 'clip',
                hide: 'clip',
                buttons: [
                { text: "Ok", click: function () { $(this).dialog("close") } }
            ]
            });

            // NewDialog.appendTo($("body"));
            //TO DO another way to add hml to dialog 
            //$('#dialog-test').html('<div> weak </div>');
            NewDialog.dialog('open');

        },
        //4-1-2012  - find a way to merg these , ie use this one for all chat based messages from and to users
        showDynamicNotificationDialogWithImage: function (message, title, room, senderScreenName, ismodal, dialogid) {


            //falback method in case we have problems
            //var NewDialog = $('<div id="dialog-test">\ <p>This is your dialog content, which can be multiline and dynamic.</p> </div>');



            //another cleaner way of building your div
            var NewDialog = $(document.createElement('div'));
            var imageDiv = $(document.createElement('div'));
            var imageTag = $(document.createElement('img'));
            //no need for a n id for image div but give it some css
            imageDiv.attr('class', 'left');
            //set the source for the image
            imageTag.attr('src', "../Images/GetGalleryImageByScreenName/" + senderScreenName);

            imageTag.attr('width', "30");
            imageTag.attr('height', "30");
            //addend the image tag and maybe more to this
            imageDiv.append(imageTag);


            NewDialog.html(message);
            //add the image and div tag to the Dialog
            NewDialog.append(imageDiv);
            //add an ID if it it was passed
            if (dialogid) {
                NewDialog.attr("id", dialogid);

            }
            if (!ismodal) {
                ismodal = false;
            }


            NewDialog.dialog({
                modal: ismodal,
                title: (title) ? title : 'Notification',
                autoOpen: true,
                width: 300,
                show: 'clip',
                hide: 'clip',
                buttons: [
               { text: "Ok", click: function () { $(this).dialog("close") } }
                //TO DO allow them to close the chat window from here 
                // , { text: "CloseChat", click: function () { rejectchatrequest(room); $(this).dialog("close") } }
            ]
            });

            // NewDialog.appendTo($("body"));
            //TO DO another way to add hml to dialog 
            //$('#dialog-test').html('<div> weak </div>');
            NewDialog.dialog('open');

        },
        //3-29-2012 for some reason this dialog thing cannot parse the room object se we send the sender screeenName separately
        //TO DO look at this
        showDynamicChatRequestDialog: function (message, title, room, ScreenName, ismodal, dialogid) {


            //falback method in case we have problems
            //var NewDialog = $('<div id="dialog-test">\ <p>This is your dialog content, which can be multiline and dynamic.</p> </div>');



            //another cleaner way of building your div
            var NewDialog = $(document.createElement('div'));
            var imageDiv = $(document.createElement('div'));
            var imageTag = $(document.createElement('img'));
            //no need for a n id for image div but give it some css
            imageDiv.attr('class', 'left');
            //set the source for the image
            imageTag.attr('src', "../Images/GetGalleryImageByScreenName/" + ScreenName);

            imageTag.attr('width', "30");
            imageTag.attr('height', "30");
            //addend the image tag and maybe more to this
            imageDiv.append(imageTag);


            NewDialog.html(message);
            //add the image and div tag to the Dialog
            NewDialog.append(imageDiv);
            //add an ID if it it was passed
            if (dialogid) {
                NewDialog.attr("id", dialogid);

            }
            if (!ismodal) {
                ismodal = false;
            }

            //newDiv.dialog();
            // NewDialog.dialog('open');

            //  var NewDialog = $(document.createElement('div'));
            //  NewDialog.html('hello there');
            //  NewDialog.attr("id", "placeholder");
            //           $("body").css("background", (n < 2) ? "green" : "orange");


            NewDialog.dialog({
                modal: ismodal,
                title: (title) ? title : 'Notification',
                autoOpen: true,
                width: 300,
                show: 'clip',
                hide: 'clip',
                buttons: [
                { text: "Accept", click: function () { createchatroom(room); $(this).dialog('close'); } },
                { text: "Reject", click: function () { rejectchatrequest(room); $(this).dialog("close") } }
            ]
            });

            // NewDialog.appendTo($("body"));
            //TO DO another way to add hml to dialog 
            //$('#dialog-test').html('<div> weak </div>');
            NewDialog.dialog('open');

        },
        //3-27-2012 added code to handle switching of topic so you do not see yourself, same with data name
        //whoch should be the screen nam of who yo uare chatting with
        createchatbox: function (room, minimizeChatBox) {

            //topic is bacially the header split the roomname since all chatboxes are 1 to 1 chat right now
            //5-6-2012 set default name of thechatbox
            var chatboxScreenName = room.Name;
            //*** this is where we figure out who is chating and what title to use ****
            //TO do find a better way
            //make sure to show the oposit of what they are i.e if the name matches topic, then set them up as topic starter 
            //otherewise we are fine leave it be
            if (ui.getUserName() == room.Topic) {
                chatboxScreenName = room.TopicStarterScreenName;

            }
            else {
                chatboxScreenName = room.TopicScreenName;
            }


            if ($("#chatbox_" + room.Name).length > 0) {
                if ($("#chatbox_" + room.Name).css('display') == 'none') {
                    $("#chatbox_" + room.Name).css('display', 'block');
                    ui.restructureChatBoxes();
                }


                $("#chatbox_" + room.Name + " .chatboxtextarea").focus();
                return;
            }
            //updated the data name value to the to the topic
            $(" <div />").attr("id", "chatbox_" + room.Name)
	.addClass("chatbox")
	.html('<div class="chatboxhead"><div class="chatboxtitle">' + chatboxScreenName + '</div><div class="chatboxoptions"><a class="minimizechat" href="javascript:void(0)" data-name=\'' + room.Topic + '\'>_</a><a class="closechat" href="javascript:void(0)" data-name=\'' + room.Topic + '\'">X</a></div><br clear="all"/></div><div class="chatboxcontent"></div><div class="chatboxinput"><textarea class="chatboxtextarea" data-name=\'' + room.Topic + '\'></textarea></div>')
	.appendTo($("body"));

            $("#chatbox_" + room.Name).css('bottom', '0px');

            //saves the number for chatboxes that are live for use in rebuilding the UI after referesh or whatever
            //was unasigned broke jquery in firefox
            var chatBoxeslength = 0;

            for (var x in chatBoxes) {
                //added code to make sure the indexed item is not undifinded as it breaks the selector
                //var validchatbox = jQuery.isEmptyObject(chatBoxes[x]);
                if ((jQuery.isEmptyObject(chatBoxes[x]) == false) && ($("#chatbox_" + chatBoxes[x]).css('display') != 'none')) {
                    chatBoxeslength++;
                }
            }

            if (chatBoxeslength == 0) {
                $("#chatbox_" + room.Name).css('right', '20px');
            } else {
                width = (chatBoxeslength) * (225 + 7) + 20;
                $("#chatbox_" + room.Name).css('right', width + 'px');
            }


            //important , empty chatboxe array causes sizzle error
            chatBoxes.push(room.Name);

            if (minimizeChatBox == true) {
                var minimizedChatBoxes = new Array();

                if ($.cookie('chatbox_minimized')) {
                    minimizedChatBoxes = $.cookie('chatbox_minimized').split(/\|/);
                }
                //debugger;

                var minimize = 0;
                //added code to test for empty array
                if (jQuery.isEmptyObject(minimizedChatBoxes) == false) {
                    for (var j = 0; j < minimizedChatBoxes.length; j++) {
                        if (minimizedChatBoxes[j] == room.Name) {
                            minimize = 1;
                        }
                    }
                }
                if (minimize == 1) {
                    $('#chatbox_' + room.Name + ' .chatboxcontent').css('display', 'none');
                    $('#chatbox_' + room.Name + ' .chatboxinput').css('display', 'none');
                }
            }

            chatboxFocus[room.Name] = false;

            $("#chatbox_" + room.Name + " .chatboxtextarea").blur(function () {
                chatboxFocus[room.Name] = false;
                $("#chatbox_" + room.Name + " .chatboxtextarea").removeClass('chatboxtextareaselected');
            }).focus(function () {
                chatboxFocus[room.Name] = true;
                newMessages[room.Name] = false;
                $('#chatbox_' + room.Name + ' .chatboxhead').removeClass('chatboxblink');
                $("#chatbox_" + room.Name + " .chatboxtextarea").addClass('chatboxtextareaselected');
            });

            $("#chatbox_" + room.Name).click(function () {
                if ($('#chatbox_' + room.Name + ' .chatboxcontent').css('display') != 'none') {
                    $("#chatbox_" + room.Name + " .chatboxtextarea").focus();
                }
            });

            $("#chatbox_" + room.Name).show();
        },
        restructureChatBoxes: function () {
            var align = 0;
            var test = $chatboxes;

            for (var x in chatBoxes) {
                chatboxtitle = chatBoxes[x];

                // var validchatbox = jQuery.isEmptyObject(chatboxtitle);

                if ((jQuery.isEmptyObject(chatboxtitle) == false) && ($("#chatbox_" + chatboxtitle).css('display') != 'none')) {
                    if (align == 0) {
                        $("#chatbox_" + chatboxtitle).css('right', '20px');
                    } else {
                        width = (align) * (225 + 7) + 20;
                        $("#chatbox_" + chatboxtitle).css('right', width + 'px');
                    }
                    align++;
                }
            }

        },
        run: function () {
            $.history.init(function (hash) {
                if (hash.length && hash[0] == '/') {
                    hash = hash.substr(1);
                }

                var parts = hash.split('/');
                if (parts[0] === 'rooms') {
                    var roomName = parts[1];

                    if (ui.setActiveRoom(roomName) === false) {
                        $ui.trigger(ui.events.openRoom, [roomName]);
                    }
                }
            },
            { unescape: ',/' });
        },
        setMessage: function (value) {
            $newMessage.val(value);
            lastCycledMessage = value;
            if (value) {
                $newMessage.selectionEnd = value.length;
            }
        },
        addRoom: addRoom,
        removeRoom: removeRoom,
        setRoomOwner: function (ownerName, roomName) {
            var room = getRoomElements(roomName),
                $user = room.getUser(ownerName);
            $user
                .attr('data-owner', true)
                .data('owner', true)
                .find('.owner')
                .text('(owner)');
            room.updateUserStatus($user);
        },
        clearRoomOwner: function (ownerName, roomName) {
            var room = getRoomElements(roomName),
                $user = room.getUser(ownerName);
            $user
                 .removeAttr('data-owner')
                 .data('owner', false)
                 .find('.owner')
                 .text('');
            room.updateUserStatus($user);
        },
        setActiveRoom: function (roomName) {
            var room = getRoomElements(roomName);

            loadRoomPreferences(roomName);

            if (room.isActive()) {
                // Still trigger the event (just do less overall work)
                $ui.trigger(ui.events.activeRoomChanged, [roomName]);
                return true;
            }

            var currentRoom = getCurrentRoomElements();

            if (room.exists() && currentRoom.exists()) {
                currentRoom.makeInactive();
                triggerFocus();
                room.makeActive();

                document.location.hash = '#/rooms/' + roomName;
                $ui.trigger(ui.events.activeRoomChanged, [roomName]);
                return true;
            }

            return false;
        },
        setRoomLocked: function (roomName) {
            var room = getRoomElements(roomName);

            room.setLocked();
        },
        updateLobbyRoomCount: updateLobbyRoomCount,
        updateUnread: function (roomName, isMentioned) {
            var room = roomName ? getRoomElements(roomName) : getCurrentRoomElements();

            if (ui.hasFocus() && room.isActive()) {
                return;
            }

            room.updateUnread(isMentioned);
        },
        scrollToBottom: function (roomName) {
            var room = roomName ? getRoomElements(roomName) : getCurrentRoomElements();

            if (room.isActive()) {
                room.scrollToBottom();
            }
        },
        isNearTheEnd: function (roomName) {
            var room = roomName ? getRoomElements(roomName) : getCurrentRoomElements();

            return room.isNearTheEnd();
        },
        populateLobbyRooms: function (rooms) {
            var lobby = getLobby(),
            // sort lobby by room count descending
            sorted = rooms.sort(function (a, b) {
                return a.Count > b.Count ? -1 : 1;
            });

            lobby.users.empty();

            $.each(sorted, function () {
                var $name = $('<span/>').addClass('name')
                                        .html(this.Name),
                    $count = $('<span/>').addClass('count')
                                         .html(' (' + this.Count + ')')
                                         .data('count', this.Count),
                    $locked = $('<span/>').addClass('lock'),
                    $li = $('<li/>').addClass('room')
                          .attr('data-room', this.Name)
                          .data('name', this.Name)
                          .append($locked)
                          .append($name)
                          .append($count)
                          .appendTo(lobby.users);

                if (this.Private) {
                    $li.addClass('locked');
                }
            });

            if (lobby.isActive()) {
                // update cache of room names
                $roomFilterInput.show();
            }

            ui.$roomFilter.update();
            $roomFilterInput.val('');
        },
        addUser: function (userViewModel, roomName) {
            var room = getRoomElements(roomName),
                $user = null;

            // Remove all users that are being removed
            room.users.find('.removing').remove();

            // Get the user element
            $user = room.getUser(userViewModel.name);

            if ($user.length) {
                return false;
            }

            $user = templates.user.tmpl(userViewModel);
            $user.data('inroom', roomName);
            $user.data('owner', userViewModel.owner);

            room.addUser(userViewModel, $user);
            updateNote(userViewModel, $user);
            updateFlag(userViewModel, $user);

            return true;
        },
        setUserActivity: function (userViewModel) {
            var $user = $('.users').find(getUserClassName(userViewModel.name)),
                active = $user.data('active');

            if (userViewModel.active !== active) {
                if (userViewModel.active === true) {
                    $user.fadeTo('slow', 1, function () {
                        $user.removeClass('idle');
                    });
                } else {
                    $user.fadeTo('slow', 0.5, function () {
                        $user.addClass('idle');
                    });
                }
            }

            updateNote(userViewModel, $user);
        },
        setUserActive: function ($user) {
            if ($user.data('active') === true) {
                return false;
            }
            $user.attr('data-active', true);
            $user.data('active', true);
            return true;
        },
        setUserInActive: function ($user) {
            if ($user.data('active') === false) {
                return false;
            }
            $user.attr('data-active', false);
            $user.data('active', false);
            return true;
        },
        changeUserName: function (oldName, user, roomName) {
            var room = getRoomElements(roomName),
                $user = room.getUserReferences(oldName);

            // Update the user's name
            $user.find('.name').fadeOut('normal', function () {
                $(this).html(user.Name);
                $(this).fadeIn('normal');
            });
            $user.data('name', user.Name);
            $user.attr('data-name', user.Name);
            room.sortLists();
        },
        changeGravatar: function (user, roomName) {
            var room = getRoomElements(roomName),
                $user = room.getUserReferences(user.Name),
                src = 'http://www.gravatar.com/avatar/' + user.Hash + '?s=16&d=mm';

            $user.find('.gravatar')
                 .attr('src', src);
        },
        removeUser: function (user, roomName) {
            var room = getRoomElements(roomName),
                $user = room.getUser(user.Name);

            $user.addClass('removing')
                .fadeOut('slow', function () {
                    $(this).remove();
                });
        },
        //TO DO set up way to show somone is typing down the line
        setUserTyping: function (userViewModel, roomName) {
            //  var room = getRoomElements(roomName),
            //  $user = room.getUser(userViewModel.name),
            //   timeout = null;

            // Do not show typing indicator for current user
            if (userViewModel.name === ui.getUserName()) {
                return;
            }

            // Mark the user as typing
            //$user.addClass('typing');
            // var oldTimeout = $user.data('typing');

            //   if (oldTimeout) {
            //       clearTimeout(oldTimeout);
            //   }

            //   timeout = window.setTimeout(function () {
            //      $user.removeClass('typing');
            //   },
            //   3000);

            //  $user.data('typing', timeout);
        },
        prependChatMessages: function (messages, roomName) {
            var room = getRoomElements(roomName),
                $messages = room.messages,
                $target = $messages.children().first(),
                $previousMessage = null,
                $current = null,
                previousUser = null;

            if (messages.length === 0) {
                // Mark this list as full
                $messages.data('full', true);
                return;
            }

            // Populate the old messages
            $.each(messages, function (index) {
                processMessage(this);

                if ($previousMessage) {
                    previousUser = $previousMessage.data('name');
                }

                // Determine if we need to show the user
                this.showUser = !previousUser || previousUser !== this.name;

                // Render the new message
                $target.before(templates.message.tmpl(this));

                if (this.showUser === false) {
                    $previousMessage.addClass('continue');
                }

                $previousMessage = $('#m-' + this.id);
            });

            // Scroll to the bottom element so the user sees there's more messages
            $target[0].scrollIntoView();
        },
        addChatMessage: function (message, roomName, blchatbox) {
            //update to the code to add to chat box instead of room so roomname is chatbox basically

            //sa,mple for building image with gravatar
            //            <div class="left">
            //                {{if showUser}}
            //                <img src="http://www.gravatar.com/avatar/${hash}?s=16&d=mm" class="gravatar" />
            //                <span class="name">${trimmedName}</span>
            //                {{/if}}
            //                <span class="state"></span>
            //            </div>


            // return false;
            //end here for now
            //TO DO hanlde the other parsing as well as the gravater with a template as well
            //FInd a way to populate the values of the room we are chatting in like they did above
            //var room = getRoomElements(roomName),
            var $previousMessage = $('#' + roomName).last(),
                            previousUser = null,
                            previousTimestamp = new Date(),
                            showUserName = true,
                            $message = null,
                            isMention = message.highlight;

            if ($previousMessage.length > 0) {
                previousUser = $previousMessage.data('name');
                previousTimestamp = new Date($previousMessage.data('timestamp') || new Date());
            }

            //TO DO fix this to work
            // Determine if we need to show the user name next to the message
            //  showUserName = previousUser !== message.name;
            //  message.showUser = showUserName;

            message = processChatBoxMessage(message);

            //  if (showUserName === false) {
            //      $previousMessage.addClass('continue');
            //   }

            //TO DO spearator
            //hanlde separateor stuff at later date
            // check to see if room needs a separator
            //            if (room.needsSeparator()) {
            //                // if there's an existing separator, remove it
            //                if (room.hasSeparator()) {
            //                    room.removeSeparator();
            //                }
            //                room.addSeparator();
            //            }

            //TO DO do somethin gto show the date of the last message if the times a kinda far apart
            //if (message.date.toDate().diffDays(previousTimestamp.toDate())) {
            //      ui.addMessage(message.date.toLocaleDateString(), 'list-header', roomName)
            //         .find('.right').remove(); // remove timestamp on date indicator
            //   }

            //  templates.message.tmpl(message).appendTo(room.messages);
            //Do the append here
            if (message != '' && blchatbox == true) {

                //updated 
                //4-1-2012 updated this code to include the time stamp of last message , if its over a certain period show the time stamp so they know
                //TO do make sure this works 
                $("#chatbox_" + roomName + " .chatboxcontent").append('<div class="chatboxmessage" data-name=\'' + message.name + '\'" data-timestamp=\'' + message.date + '\'" ><span class="chatboxmessagefrom">' + message.screenName + ':&nbsp;&nbsp;</span><span class="chatboxmessagecontent">' + message.message + '</span></div>');
                $("#chatbox_" + roomName + " .chatboxcontent").scrollTop($("#chatbox_" + roomName + " .chatboxcontent")[0].scrollHeight);


            }
            chatHeartbeatTime = minChatHeartbeat;
            chatHeartbeatCount = 1;
            return;
            //TO do sound preferences
            //handle sound prfencess at later date
            //            if (room.isInitialized()) {
            //                if (isMention) {
            //                    // Always do sound notification for mentions if any room as sound enabled
            //                    if (anyRoomPreference('hasSound') === true) {
            //                        ui.notify(true);
            //                    }

            //                    if (ui.focus === false && anyRoomPreference('canToast') === true) {
            //                        // Only toast if there's no focus (even on mentions)
            //                        ui.toast(message, true);
            //                    }
            //                }
            //                else {
            //                    // Only toast if chat isn't focused
            //                    if (ui.focus === false) {
            //                        ui.notifyRoom(roomName);
            //                        ui.toastRoom(roomName, message);
            //                    }
            //                }
            //            }
        },

        replaceMessage: function (message) {
            processMessage(message);

            $('#m-' + message.id).find('.middle')
                                 .html(message.message);
        },
        messageExists: function (id) {
            return $('#m-' + id).length > 0;
        },
        addChatMessageContent: function (id, content, roomName) {
            var $message = $('#m-' + id);

            $message.find('.middle')
                    .append(content);
        },
        addPrivateMessage: function (content, type) {
            var rooms = getAllRoomElements();
            for (var r in rooms) {
                if (rooms[r].getName() != undefined) {
                    this.addMessage(content, type, rooms[r].getName());
                }
            }
        }, //send private message creating a popup
        addPrivateMessagepopup: function (content, type) {
            //  var rooms = getAllRoomElements();
            //to the roo
            ui.createChataRequestBox(content.roomname, content.from);
            //after the box is created then we want to show the message in there
            //TO do create a box with ok or cancel and have two separate click events so we can hanndle them via css 
            triggerSend(this, null, content.roomname);


            for (var r in rooms) {
                if (rooms[r].getName() != undefined) {
                    this.addMessage(content, type, rooms[r].getName());
                }
            }
        },

        //TO DO , this breaks when we take out those templates, fix it
        addMessage: function (content, type, roomName) {
            var room = roomName ? getRoomElements(roomName) : getCurrentRoomElements(),
                nearEnd = room.isNearTheEnd(),
                $element = null,
                now = new Date(),
                message = {
                    message: content,
                    type: type,
                    date: now,
                    when: now.formatTime(true),
                    fulldate: now.toLocaleString()
                };

            $element = templates.notification.tmpl(message).appendTo(room.messages);

            if (type === 'notification' && room.isLobby() === false) {
                ui.collapseNotifications($element);
            }

            if (nearEnd) {
                ui.scrollToBottom(roomName);
            }

            return $element;
        },
        hasFocus: function () {
            return ui.focus;
        },
        getCommands: function () {
            return ui.commands;
        },
        setCommands: function (commands) {
            ui.commands = commands;
        },
        setInitialized: function (roomName) {
            var room = roomName ? getRoomElements(roomName) : getCurrentRoomElements();
            room.setInitialized();
        },
        collapseNotifications: function ($notification) {
            // collapse multiple notifications
            var $notifications = $notification.prevUntil(':not(.notification)');
            if ($notifications.length > 3) {
                $notifications
                    .hide()
                    .find('.info').text('');    // clear any prior text
                $notification.find('.info')
                    .text(' (plus ' + $notifications.length + ' hidden... click to expand)')
                    .removeClass('collapse');
            }
        },
        expandNotifications: function ($notification) {
            // expand collapsed notifications
            var $notifications = $notification.prevUntil(':not(.notification)'),
                topBefore = $notification.position().top;

            $notification.find('.info')
                .text(' (click to collapse)')
                .addClass('collapse');
            $notifications.show();

            var room = getCurrentRoomElements(),
                topAfter = $notification.position().top,
                scrollTop = room.messages.scrollTop();

            // make sure last notification is visible
            room.messages.scrollTop(scrollTop + topAfter - topBefore + $notification.height());
        },
        getState: function () {
            return preferences;
        },
        notifyRoom: function (roomName) {
            if (getRoomPreference(roomName, 'hasSound') === true) {
                $('#noftificationSound')[0].play();
            }
        },
        toastRoom: function (roomName, message) {
            if (getRoomPreference(roomName, 'canToast') === true) {
                toast.toastMessage(message, roomName);
            }
        },
        notify: function (force) {
            if (getActiveRoomPreference('hasSound') === true || force) {
                $('#noftificationSound')[0].play();
            }
        },
        toast: function (message, force) {
            if (getActiveRoomPreference('canToast') === true || force) {
                toast.toastMessage(message);
            }
        },
        setUserName: function (name) {
            ui.name = name;
        },
        getUserName: function () {
            //debugger;
            return ui.name;
        },
        showLogin: function () {
            $login.click();
        },
        showDisconnectUI: function () {
            $disconnectDialog.modal();
        },
        showUpdateUI: function () {
            $updatePopup.modal();

            window.setTimeout(function () {
                // Reload the page
                document.location = document.location.pathname;
            },
            updateTimeout);
        },
        changeNote: function (userViewModel, roomName) {
            var room = getRoomElements(roomName),
                $user = room.getUser(userViewModel.name);

            updateNote(userViewModel, $user);
        },
        changeFlag: function (userViewModel, roomName) {
            var room = getRoomElements(roomName),
                $user = room.getUser(userViewModel.name);

            updateFlag(userViewModel, $user);
        },
        changeRoomTopic: function (roomViewModel) {
            updateRoomTopic(roomViewModel);
        },
        confirmMessage: function (id) {
            $('#m-' + id).removeClass('failed')
                         .removeClass('loading');
        },
        failMessage: function (id) {
            $('#m-' + id).removeClass('loading')
                         .addClass('failed');
        },
        markMessagePending: function (id) {
            var $message = $('#m-' + id);

            if ($message.hasClass('failed') === false) {
                $message.addClass('loading');
            }
        }
    };

    if (!window.chat) {
        window.chat = {};
    }
    window.chat.ui = ui;
})(jQuery, window, window.document, window.chat.utility);
