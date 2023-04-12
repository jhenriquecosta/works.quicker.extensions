var quicker = quicker || {};
(function ($) {

    if (!$) {
        return;
    }

    /* JQUERY ENHANCEMENTS ***************************************************/

    // quicker.ajax -> uses $.ajax ------------------------------------------------

    quicker.ajax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend({}, quicker.ajax.defaultOpts, userOptions);
        options.success = undefined;
        options.error = undefined;

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    if (data.__quicker) {
                        quicker.ajax.handleResponse(data, userOptions, $dfd, jqXHR);
                    } else {
                        $dfd.resolve(data);
                        userOptions.success && userOptions.success(data);
                    }
                }).fail(function (jqXHR) {
                    if (jqXHR.responseJSON && jqXHR.responseJSON.__quicker) {
                        quicker.ajax.handleResponse(jqXHR.responseJSON, userOptions, $dfd, jqXHR);
                    } else {
                        quicker.ajax.handleNonQuickerErrorResponse(jqXHR, userOptions, $dfd);
                    }
                });
        });
    };

    $.extend(quicker.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json'
        },

        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        defaultError401: {
            message: 'You are not authenticated!',
            details: 'You should be authenticated (sign in) in order to perform this operation.'
        },

        defaultError403: {
            message: 'You are not authorized!',
            details: 'You are not allowed to perform this operation.'
        },

        defaultError404: {
            message: 'Resource not found!',
            details: 'The resource requested could not be found on the server.'
        },

        logError: function (error) {
            quicker.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return quicker.message.error(error.details, error.message);
            } else {
                return quicker.message.error(error.message || quicker.ajax.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = quicker.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleNonQuickerErrorResponse: function (jqXHR, userOptions, $dfd) {
            switch (jqXHR.status) {
                case 401:
                    quicker.ajax.handleUnAuthorizedRequest(
                        quicker.ajax.showError(quicker.ajax.defaultError401),
                        quicker.appPath
                    );
                    break;
                case 403:
                    quicker.ajax.showError(quicker.ajax.defaultError403);
                    break;
                case 404:
                    quicker.ajax.showError(quicker.ajax.defaultError404);
                    break;
                default:
                    quicker.ajax.showError(quicker.ajax.defaultError);
                    break;
            }

            $dfd.reject.apply(this, arguments);
            userOptions.error && userOptions.error.apply(this, arguments);
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    quicker.ajax.handleTargetUrl(targetUrl);
                });
            } else {
                quicker.ajax.handleTargetUrl(targetUrl);
            }
        },

        handleResponse: function (data, userOptions, $dfd, jqXHR) {
            if (data) {
                if (data.success === true) {
                    $dfd && $dfd.resolve(data.result, data, jqXHR);
                    userOptions.success && userOptions.success(data.result, data, jqXHR);

                    if (data.targetUrl) {
                        quicker.ajax.handleTargetUrl(data.targetUrl);
                    }
                } else if (data.success === false) {
                    var messagePromise = null;

                    if (data.error) {
                        messagePromise = quicker.ajax.showError(data.error);
                    } else {
                        data.error = quicker.ajax.defaultError;
                    }

                    quicker.ajax.logError(data.error);

                    $dfd && $dfd.reject(data.error, jqXHR);
                    userOptions.error && userOptions.error(data.error, jqXHR);

                    if (jqXHR.status === 401) {
                        quicker.ajax.handleUnAuthorizedRequest(messagePromise, data.targetUrl);
                    }
                } else { //not wrapped result
                    $dfd && $dfd.resolve(data, null, jqXHR);
                    userOptions.success && userOptions.success(data, null, jqXHR);
                }
            } else { //no data sent to back
                $dfd && $dfd.resolve(jqXHR);
                userOptions.success && userOptions.success(jqXHR);
            }
        },

        blockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //block whole page
                    quicker.ui.setBusy();
                } else { //block an element
                    quicker.ui.setBusy(options.blockUI);
                }
            }
        },

        unblockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //unblock whole page
                    quicker.ui.clearBusy();
                } else { //unblock an element
                    quicker.ui.clearBusy(options.blockUI);
                }
            }
        },

        ajaxSendHandler: function (event, request, settings) {
            if (!settings.headers || settings.headers[quicker.security.antiForgery.tokenHeaderName] === undefined) {
                request.setRequestHeader(quicker.security.antiForgery.tokenHeaderName, quicker.security.antiForgery.getToken());
            }
        }
    });

    $(document).ajaxSend(function (event, request, settings) {
        return quicker.ajax.ajaxSendHandler(event, request, settings);
    });

    /* JQUERY PLUGIN ENHANCEMENTS ********************************************/

    /* jQuery Form Plugin 
     * http://www.malsup.com/jquery/form/
     */

    // quickerAjaxForm -> uses ajaxForm ------------------------------------------

    if ($.fn.ajaxForm) {
        $.fn.quickerAjaxForm = function (userOptions) {
            userOptions = userOptions || {};

            var options = $.extend({}, $.fn.quickerAjaxForm.defaults, userOptions);

            options.beforeSubmit = function () {
                quicker.ajax.blockUI(options);
                userOptions.beforeSubmit && userOptions.beforeSubmit.apply(this, arguments);
            };

            options.success = function (data) {
                quicker.ajax.handleResponse(data, userOptions);
            };

            //TODO: Error?

            options.complete = function () {
                quicker.ajax.unblockUI(options);
                userOptions.complete && userOptions.complete.apply(this, arguments);
            };

            return this.ajaxForm(options);
        };

        $.fn.quickerAjaxForm.defaults = {
            method: 'POST'
        };
    }

    quicker.event.on('quicker.dynamicScriptsInitialized', function () {
        quicker.ajax.defaultError.message = quicker.localization.quickerWeb('DefaultError');
        quicker.ajax.defaultError.details = quicker.localization.quickerWeb('DefaultErrorDetail');
        quicker.ajax.defaultError401.message = quicker.localization.quickerWeb('DefaultError401');
        quicker.ajax.defaultError401.details = quicker.localization.quickerWeb('DefaultErrorDetail401');
        quicker.ajax.defaultError403.message = quicker.localization.quickerWeb('DefaultError403');
        quicker.ajax.defaultError403.details = quicker.localization.quickerWeb('DefaultErrorDetail403');
        quicker.ajax.defaultError404.message = quicker.localization.quickerWeb('DefaultError404');
        quicker.ajax.defaultError404.details = quicker.localization.quickerWeb('DefaultErrorDetail404');
    });

})(jQuery);
