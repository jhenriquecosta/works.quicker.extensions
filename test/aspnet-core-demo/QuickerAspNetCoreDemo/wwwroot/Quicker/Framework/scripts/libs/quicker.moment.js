var quicker = quicker || {};
(function () {
    if (!moment || !moment.tz) {
        return;
    }

    /* DEFAULTS *************************************************/

    quicker.timing = quicker.timing || {};

    /* FUNCTIONS **************************************************/

    quicker.timing.convertToUserTimezone = function (date) {
        var momentDate = moment(date);
        var targetDate = momentDate.clone().tz(quicker.timing.timeZoneInfo.iana.timeZoneId);
        return targetDate;
    };

})();