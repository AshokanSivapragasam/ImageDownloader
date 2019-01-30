angular.module('myApp.common', [])

    .service('commonService', [function () {
        var self = this;
        self.webServiceBaseUrl = 'http://localhost:3000';
        self.vaultBaseUrl = 'http://localhost/vault';
    }]);