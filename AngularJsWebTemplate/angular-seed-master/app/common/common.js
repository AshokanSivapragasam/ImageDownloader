angular.module('myApp.common', [])

.service('commonService', [function(){
        var self = this;
        self.currentMovieId = 0;
        self.currentVideoMirrorId = 0;
    }]);