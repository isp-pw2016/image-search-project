(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['homeService', 'commonFactory'];

    function HomeController(homeService, commonFactory) {
        var vm = this;

        vm.model = {};
        vm.results = {};
        vm.isPresent = {};
        vm.isBusy = false;
        vm.startProcedure = startProcedure;

        ////////////////////

        function startProcedure() {
            if (typeof vm.model.query !== 'string' || !vm.model.query) {
                commonFactory.showInfo(
                    'Please provide the keywords which describe best the sought images',
                    'Empty query');

                return;
            }

            vm.isBusy = true;

            homeService.getGoogleImages(vm.model.query)
                .then(function(resp) {
                    vm.isPresent.google = true;
                    vm.results.google = resp;
                })
                .finally(function() {
                    vm.isBusy = false;
                });
        }
    }
})();