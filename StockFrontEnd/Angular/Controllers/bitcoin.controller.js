(function () {
    'use strict';
    angular
        .module('bitcoinModule')
        .controller('bitcoinController', bitcoinController);

    bitcoinController.$inject = ['bitcoinService'];

    function bitcoinController(bitcoinService) {
        var bitcoinVm = this;
        bitcoinVm.title = "Bitcoin Terminal";
        bitcoinService.getBitcoin().then(function (response) {
            bitcoinVm.data = response;
        }, function (error) {
            console.log(error);
        });
    }
})();