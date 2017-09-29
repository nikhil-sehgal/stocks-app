(function () {
    'use strict';
    angular
        .module('ethereumModule')
        .service('ethereumService', ethereumService);

    ethereumService.$inject = ['$q', '$http', '$interval'];

    function ethereumService($q, $http, $interval) {
        var self = this;

        self.getEthereum = function () {
            return $http({
                method: 'GET',
                url: 'use the hosted localhost service of stocks web api'
            }).then(function (response) {
                return response.data;
            }, function (error) {
                return $q.reject(error);
            });
        };

        var theInterval = $interval(function () {
            self.getEthereum();
        }.bind(self), 10000);
    }
})();