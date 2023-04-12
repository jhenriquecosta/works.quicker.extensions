var quicker = quicker || {};
(function ($) {

    //Check if SignalR is defined
    if (!$ || !$.connection) {
        return;
    }

    //Create namespaces
    quicker.signalr = quicker.signalr || {};
    quicker.signalr.hubs = quicker.signalr.hubs || {};

    //Get the common hub
    quicker.signalr.hubs.common = $.connection.quickerCommonHub;

    var commonHub = quicker.signalr.hubs.common;
    if (!commonHub) {
        return;
    }

    //Register to get notifications
    commonHub.client.getNotification = function (notification) {
        quicker.event.trigger('quicker.notifications.received', notification);
    };

    //Connect to the server
    quicker.signalr.connect = function() {
        $.connection.hub.start().done(function () {
            quicker.log.debug('Connected to SignalR server!'); //TODO: Remove log
            quicker.event.trigger('quicker.signalr.connected');
            commonHub.server.register().done(function () {
                quicker.log.debug('Registered to the SignalR server!'); //TODO: Remove log
            });
        });
    };

    if (quicker.signalr.autoConnect === undefined) {
        quicker.signalr.autoConnect = true;
    }

    if (quicker.signalr.autoConnect) {
        quicker.signalr.connect();
    }

})(jQuery);