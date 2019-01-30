angular.module('myApp.common', [])

    .service('commonService', [function () {
        var self = this;
        self.currentMovieId = 0;
        self.currentVideoMirrorId = 0;
        self.webServiceBaseUrl = 'http://localhost/TamilYogiWebApi';
        self.vaultBaseUrl = 'http://localhost/vault';
        self.siteuser = 'guest';
        self.isidentifieduser = false;
    }]);