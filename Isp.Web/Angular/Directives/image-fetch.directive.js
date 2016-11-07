(function() {
    'use strict';

    angular
        .module('app')
        .directive('imageFetch', imageFetch)
        .controller('ImageFetchController', ImageFetchController);

    function imageFetch() {
        var directive = {
            controller: 'ImageFetchController as vm',
            restrict: 'EA',
            templateUrl: 'image-fetch.html',
            scope: {},
            bindToController: {
                name: '@',
                server: '=',
                client: '=',
                marginAfter: '=?'
            }
        };

        return directive;
    }

    ImageFetchController.$inject = ['$scope', 'commonFactory'];

    function ImageFetchController($scope, commonFactory) {
        var vm = this;

        vm.marginAfter = commonFactory.isBool(vm.marginAfter) ? vm.marginAfter : true;

        $scope.$watch('vm.server', function() {
            parse(vm.server);
        });

        $scope.$watch('vm.client', function () {
            parse(vm.client);
        });

        ////////////////////

        function parse(obj) {
            if (commonFactory.isObject(obj)) {
                timeToString(obj);

                if (commonFactory.isObject(obj.imageFetch)) {
                    timeToString(obj.imageFetch);
                }
            }
        }

        function timeToString(obj) {
            if (commonFactory.isNumber(obj.time) && obj.time > 0 && commonFactory.isStringNotNull(obj.timeString)) {
                obj.time = '(' + obj.time + ')';
            } else {
                obj.time = '';
                obj.timeString = 'X';
            }
        }
    }
})();