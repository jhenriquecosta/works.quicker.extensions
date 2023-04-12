var quicker = quicker || {};
(function ($) {
    if (!sweetAlert || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    quicker.libs = quicker.libs || {};
    quicker.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                icon: 'info'
            },
            success: {
                icon: 'success'
            },
            warn: {
                icon: 'warning'
            },
            error: {
                icon: 'error'
            },
            confirm: {
                icon: 'warning',
                title: 'Are you sure?',
                buttons: ['Cancel', 'Yes']
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }

        var opts = $.extend(
            {},
            quicker.libs.sweetAlert.config['default'],
            quicker.libs.sweetAlert.config[type],
            {
                title: title,
                text: message
            }
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts).then(function () {
                $dfd.resolve();
            });
        });
    };

    quicker.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    quicker.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    quicker.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    quicker.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    quicker.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var opts = $.extend(
            {},
            quicker.libs.sweetAlert.config['default'],
            quicker.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts).then(function (isConfirmed) {
                if (isConfirmed) {
                    callback && callback(isConfirmed);
                }
                $dfd.resolve(isConfirmed);
            });
        });
    };

    quicker.event.on('quicker.dynamicScriptsInitialized', function () {
        quicker.libs.sweetAlert.config.confirm.title = quicker.localization.quickerWeb('AreYouSure');
        quicker.libs.sweetAlert.config.confirm.buttons = [quicker.localization.quickerWeb('Cancel'), quicker.localization.quickerWeb('Yes')];
    });

})(jQuery);