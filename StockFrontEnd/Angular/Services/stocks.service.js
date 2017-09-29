(function () {
    'use strict';
    angular
        .module('stocksModule')
        .service('stocksService', stocksService);

    stocksService.$inject = ['$q', '$http', '$interval'];

    function stocksService($q, $http, $interval) {
        var self = this;

        self.getStocks = function () {
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
            self.getStocks();
        }.bind(self), 10000);
    }
})();