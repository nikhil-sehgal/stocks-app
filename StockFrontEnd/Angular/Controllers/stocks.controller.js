(function () {
    'use strict';
    angular
        .module('stocksModule')
        .controller('stocksController', stocksController);

    stocksController.$inject = ['stocksService'];   
    
    function stocksController(stocksService) {
        var stocksVm = this;
        stocksVm.title = "Stocks Terminal";
        stocksService.getStocks().then(function (response) {            
            stocksVm.data = response;
        }, function (error) {
            console.log(error);
        });
    }
})();
