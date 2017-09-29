(function () {
    'use strict';
    angular
        .module('mainModule', ['stocksModule', 'bitcoinModule', 'ethereumModule'])
        .run(function () {
            //console.log('starting Main Module.');
        });
})();
