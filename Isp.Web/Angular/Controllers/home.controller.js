(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$q', 'homeService', 'commonFactory'];

    function HomeController($q, homeService, commonFactory) {
        var vm = this;

        vm.model = {};
        vm.results = {};
        vm.isInitialised = false;
        vm.isBusy = false;
        vm.startProcedure = startProcedure;

        ////////////////////

        function startProcedure() {
            if (typeof vm.model.query !== 'string' || vm.model.query.length === 0) {
                commonFactory.showInfo(
                    'Please provide the keywords which describe best the sought images',
                    'Empty query');

                return;
            }

            vm.isBusy = true;

            vm.model.skip = 0;
            vm.model.take = 10;

            var googlePromise = homeService.getGoogleImages(vm.model);
            var bingPromise = homeService.getBingImages(vm.model);

            $q.all([googlePromise, bingPromise])
                .then(function(responses) {
                    vm.results = {
                        google: responses[0],
                        bing: responses[1]
                    };
                })
                .finally(function () {
                    vm.isInitialised = true;
                    vm.isBusy = false;
                });
        }
    }
})();