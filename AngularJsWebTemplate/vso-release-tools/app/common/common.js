angular.module('myApp.common', [])

    .service('commonService', [function () {
        var self = this;
        self.webServiceUrlAllVsoReleaseDefinitions = 'https://microsoftit.vsrm.visualstudio.com/DefaultCollection/OneITVSO/_apis/release/definitions?api-version=3.0-preview.1';
        self.webServiceUrlVsoReleaseDefinition = 'https://microsoftit.vsrm.visualstudio.com/DefaultCollection/OneITVSO/_apis/release/definitions/2239?api-version=3.0-preview.1';
        self.webServiceUrlAllVsoReleasesByDefinitionId = 'https://microsoftit.vsrm.visualstudio.com/defaultcollection/OneITVSO/_apis/release/releases?api-version=3.0-preview.1&definitionId=2239';
    }]);