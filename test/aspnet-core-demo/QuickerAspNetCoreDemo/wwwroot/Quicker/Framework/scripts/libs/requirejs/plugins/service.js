define(function () {
    return {
        load: function (name, req, onload, config) {
            var url = quicker.appPath + 'api/QuickerServiceProxies/Get?name=' + name;
            req([url], function (value) {
                onload(value);
            });
        }
    };
});