(function (quicker, angular) {

    if (!angular) {
        return;
    }

    quicker.ng = quicker.ng || {};

    quicker.ng.http = {
        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        logError: function (error) {
            quicker.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return quicker.message.error(error.details, error.message || quicker.ng.http.defaultError.message);
            } else {
                return quicker.message.error(error.message || quicker.ng.http.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            location.href = targetUrl;
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    if (!targetUrl) {
                        location.reload();
                    } else {
                        quicker.ng.http.handleTargetUrl(targetUrl);
                    }
                });
            } else {
                if (!targetUrl) {
                    location.reload();
                } else {
                    quicker.ng.http.handleTargetUrl(targetUrl);
                }
            }
        },

        handleResponse: function (response, defer) {
            var originalData = response.data;

            if (originalData.success === true) {
                response.data = originalData.result;
                defer.resolve(response);

                if (originalData.targetUrl) {
                    quicker.ng.http.handleTargetUrl(originalData.targetUrl);
                }
            } else if (originalData.success === false) {
                var messagePromise = null;

                if (originalData.error) {
                    messagePromise = quicker.ng.http.showError(originalData.error);
                } else {
                    originalData.error = defaultError;
                }

                quicker.ng.http.logError(originalData.error);

                response.data = originalData.error;
                defer.reject(response);

                if (originalData.unAuthorizedRequest) {
                    quicker.ng.http.handleUnAuthorizedRequest(messagePromise, originalData.targetUrl);
                }
            } else { //not wrapped result
                defer.resolve(response);
            }
        }
    }

    var quickerModule = angular.module('quicker', []);

    quickerModule.config([
        '$httpProvider', function ($httpProvider) {
            $httpProvider.interceptors.push(['$q', function ($q) {

                return {

                    'request': function (config) {
                        if (config.url.indexOf('.cshtml') !== -1) {
                            config.url = quicker.appPath + 'QuickerAppView/Load?viewUrl=' + config.url + '&_t=' + quicker.pageLoadTime.getTime();
                        }

                        return config;
                    },

                    'response': function (response) {
                        if (!response.data || !response.data.__quicker) {
                            return response;
                        }

                        var defer = $q.defer();
                        quicker.ng.http.handleResponse(response, defer);
                        return defer.promise;
                    },

                    'responseError': function (ngError) {
                        if (!ngError.data || !ngError.data.__quicker) {
                            quicker.ng.http.showError(quicker.ng.http.defaultError);
                            return ngError;
                        }

                        var defer = $q.defer();
                        quicker.ng.http.handleResponse(ngError, defer);
                        return defer.promise;
                    }

                };
            }]);
        }
    ]);

    quicker.event.on('quicker.dynamicScriptsInitialized', function () {
        quicker.ng.http.defaultError.message = quicker.localization.quickerWeb('DefaultError');
        quicker.ng.http.defaultError.details = quicker.localization.quickerWeb('DefaultErrorDetail');
    });

})((quicker || (quicker = {})), (angular || undefined));