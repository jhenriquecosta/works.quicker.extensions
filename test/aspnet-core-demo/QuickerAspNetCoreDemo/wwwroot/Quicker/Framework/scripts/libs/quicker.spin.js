var quicker = quicker || {};
(function () {

    if (!$.fn.spin) {
        return;
    }

    quicker.libs = quicker.libs || {};

    quicker.libs.spinjs = {

        spinner_config: {
            lines: 11,
            length: 0,
            width: 10,
            radius: 20,
            corners: 1.0,
            trail: 60,
            speed: 1.2
        },

        //Config for busy indicator in element's inner element that has '.quicker-busy-indicator' class.
        spinner_config_inner_busy_indicator: {
            lines: 11,
            length: 0,
            width: 4,
            radius: 7,
            corners: 1.0,
            trail: 60,
            speed: 1.2
        }

    };

    quicker.ui.setBusy = function (elm, optionsOrPromise) {
        optionsOrPromise = optionsOrPromise || {};
        if (optionsOrPromise.always || optionsOrPromise['finally']) { //Check if it's promise
            optionsOrPromise = {
                promise: optionsOrPromise
            };
        }

        var options = $.extend({}, optionsOrPromise);

        if (!elm) {
            if (options.blockUI != false) {
                quicker.ui.block();
            }

            $('body').spin(quicker.libs.spinjs.spinner_config);
        } else {
            var $elm = $(elm);
            var $busyIndicator = $elm.find('.quicker-busy-indicator'); //TODO@Halil: What if  more than one element. What if there are nested elements?
            if ($busyIndicator.length) {
                $busyIndicator.spin(quicker.libs.spinjs.spinner_config_inner_busy_indicator);
            } else {
                if (options.blockUI != false) {
                    quicker.ui.block(elm);
                }

                $elm.spin(quicker.libs.spinjs.spinner_config);
            }
        }

        if (options.promise) { //Supports Q and jQuery.Deferred
            if (options.promise.always) {
                options.promise.always(function () {
                    quicker.ui.clearBusy(elm);
                });
            } else if (options.promise['finally']) {
                options.promise['finally'](function () {
                    quicker.ui.clearBusy(elm);
                });
            }
        }
    };

    quicker.ui.clearBusy = function (elm) {
        //TODO@Halil: Maybe better to do not call unblock if it's not blocked by setBusy
        if (!elm) {
            quicker.ui.unblock();
            $('body').spin(false);
        } else {
            var $elm = $(elm);
            var $busyIndicator = $elm.find('.quicker-busy-indicator');
            if ($busyIndicator.length) {
                $busyIndicator.spin(false);
            } else {
                quicker.ui.unblock(elm);
                $elm.spin(false);
            }
        }
    };

})();