(function() {
    'use strict';

    angular
        .module('app')
        .directive('reverseImageFetch', reverseImageFetch)
        .controller('ReverseImageFetchController', ReverseImageFetchController);

    function reverseImageFetch() {
        var directive = {
            controller: 'ReverseImageFetchController as vm',
            restrict: 'EA',
            templateUrl: 'reverse-image-fetch.html',
            scope: {},
            bindToController: {
                name: '@',
                model: '=',
                marginAfter: '=?'
            }
        };

        return directive;
    }

    ReverseImageFetchController.$inject = ['commonFactory'];

    function ReverseImageFetchController(commonFactory) {
        var vm = this;

        vm.marginAfter = commonFactory.isBool(vm.marginAfter) ? vm.marginAfter : true;
    }
})();