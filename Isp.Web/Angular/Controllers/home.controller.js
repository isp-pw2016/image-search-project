(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['homeService', 'commonFactory'];

    function HomeController(homeService, commonFactory) {
        var vm = this;

        vm.model = {};
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
            homeService.getGoogleImages('test')
                .then(function(resp) {
                    vm.isBusy = false;
                    console.log('OK');
                });
        }
    }
})();