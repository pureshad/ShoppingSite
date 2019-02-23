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