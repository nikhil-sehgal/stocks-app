(function () {
    'use strict';
    angular
        .module('bitcoinModule')
        .service('bitcoinService', bitcoinService);

    bitcoinService.$inject = ['$q', '$http', '$interval'];

    function bitcoinService($q, $http, $interval) {
        var self = this;

        self.getBitcoin = function () {
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
            self.getBitcoin();
        }.bind(self), 10000);
    }
})();