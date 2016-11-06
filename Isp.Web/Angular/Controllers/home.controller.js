(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$q', 'clientService', 'serverService', 'commonFactory'];

    function HomeController($q, clientService, serverService, commonFactory) {
        var vm = this;

        vm.model = {};
        vm.clientResults = {};
        vm.serverResults = {};
        vm.isBusy = false;
        vm.isInitialised = false;
        vm.startProcedure = startProcedure;

        ////////////////////

        function startProcedure() {
            if (!commonFactory.isStringNotNull(vm.model.query)) {
                commonFactory.showInfo(
                    'Please provide the keywords which describe best the sought images',
                    'Empty query'
                );

                return;
            }

            vm.isBusy = true;

            vm.model.skip = 0;
            vm.model.take = 10;

            var googleServerPromise = serverService.getGoogleImages(vm.model);
            var bingServerPromise = serverService.getBingImages(vm.model);
            var instagramServerPromise = serverService.getInstagramImages(vm.model);
            var flickrServerPromise = serverService.getFlickrImages(vm.model);
            var shutterstockServerPromise = serverService.getShutterstockImages(vm.model);

            $q.all([
                    googleServerPromise, bingServerPromise, instagramServerPromise, flickrServerPromise,
                    shutterstockServerPromise
                ])
                .then(function(responses) {
                    vm.serverResults = {
                        google: responses[0],
                        bing: responses[1],
                        instagram: responses[2],
                        flickr: responses[3],
                        shutterstock: responses[4]
                    };
                })
                .finally(function() {
                    vm.isInitialised = true;
                    vm.isBusy = false;
                });
        }
    }
})();