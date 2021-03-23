$(document).ready(function () {
    var adminActive = $.connection.userTrackHub;
    adminActive.client.updateAdminOnlineStatus = function (isOnline) {
        if (isOnline) {
            $("#ChatLink").show();
            $("#chatOffline").hide();
        }
        else {
            $("#ChatLink").hide();
            $("#chatOffline").show();
        }
    };
    //$('#myModal').load('/Home/_PartialChat');

    $.connection.hub.start();
});

function openNav() {
    document.getElementById("mySidepanel").style.width = "250px";
}

/* Set the width of the sidebar to 0 (hide it) */
function closeNav() {
    document.getElementById("mySidepanel").style.width = "0";
}
function openChat() {
    //document.getElementById("chatBox").style.width = "450px";
    document.getElementById("chatBox").style.width = "450px";
    document.getElementById("chatBox").style.height = "50%";
}

function closeChat() {
    document.getElementById("chatBox").style.height = "0";
    document.getElementById("chatBox").style.width = "0";
}

function focusOnMessage() {
    $('#messageInput').focus();
    alert("FOCUS");
}