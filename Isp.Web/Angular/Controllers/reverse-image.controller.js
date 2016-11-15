(function () {
    'use strict';

    angular
        .module('app')
        .controller('ReverseImageController', ReverseImageController);

    ReverseImageController.$inject = ['$q', 'serverService', 'commonFactory'];

    function ReverseImageController($q, serverService, commonFactory) {
        var vm = this;

        vm.model = {};
        vm.isBusy = false;
        vm.isFileUploaded = false;
        vm.isInitialised = false;
        vm.uploadImage = uploadImage;
        vm.reverseImages = reverseImages;

        vm.bing = {};

        ////////////////////

        function uploadImage(file) {
            if (commonFactory.isUndefined(file) || file === null) {
                return;
            }

            if (!commonFactory.isNumber(file.size) || file.size > (1024 * 1024)) {
                commonFactory.showInfo('Image size up to 1MB', 'Size validation');
                return;
            }

            vm.model = {};
            vm.isBusy = true;
            vm.isFileUploaded = false;
            vm.isInitialised = false;

            serverService.postImage(file)
                .then(function(response) {
                    vm.model = response;
                    vm.isFileUploaded = true;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        function reverseImages(model) {
            if (!commonFactory.isStringNotNull(model.name) || !vm.isFileUploaded) {
                vm.isInitialised = false;
                return;
            }

            if (!commonFactory.isStringNotNull(model.query)) {
                commonFactory.showInfo(
                    'Please provide the keywords which describe best the sought images',
                    'Empty query');
                return;
            }

            var query = {
                query: model.query,
                fileName: model.name,
                skip: 0,
                take: 10
            }
            vm.isBusy = true;

            var bingPromise = serverService.getBingReverseImages(query);

            $q.all([bingPromise])
                .then(function (responses) {
                    vm.bing = responses[0];

                    vm.isInitialised = true;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();