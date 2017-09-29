(function () {
    'use strict';
    angular
        .module('ethereumModule')
        .controller('ethereumController', ethereumController);

    ethereumController.$inject = ['ethereumService'];

    function ethereumController(ethereumService) {
        var ethereumVm = this;
        ethereumVm.title = "Ethereum Terminal";
        ethereumService.getEthereum().then(function (response) {
            ethereumVm.data = response;
        }, function (error) {
            console.log(error);
        });
    }
})();