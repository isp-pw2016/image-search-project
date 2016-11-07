(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$q', 'clientService', 'serverService', 'commonFactory'];

    function HomeController($q, clientService, serverService, commonFactory) {
        var vm = this;

        vm.model = {};
        vm.google = {};
        vm.bing = {};
        vm.instagram = {};
        vm.flickr = {};
        vm.shutterstock = {};
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
            var googleClientPromise = clientService.getGoogleImages(vm.model);
            var bingServerPromise = serverService.getBingImages(vm.model);
            var bingClientPromise = clientService.getBingImages(vm.model);
            var instagramServerPromise = serverService.getInstagramImages(vm.model);
            var instagramClientPromise = clientService.getInstagramImages(vm.model);
            var flickrServerPromise = serverService.getFlickrImages(vm.model);
            var flickrClientPromise = clientService.getFlickrImages(vm.model);
            var shutterstockServerPromise = serverService.getShutterstockImages(vm.model);
            var shutterstockClientPromise = clientService.getShutterstockImages(vm.model);

            $q.all([
                    googleServerPromise, googleClientPromise, bingServerPromise, bingClientPromise,
                    instagramServerPromise, instagramClientPromise, flickrServerPromise, flickrClientPromise,
                    shutterstockServerPromise, shutterstockClientPromise
                ])
                .then(function(responses) {
                    vm.google = {
                        server: responses[0],
                        client: responses[1]
                    };

                    vm.bing = {
                        server: responses[2],
                        client: responses[3]
                    };

                    vm.instagram = {
                        server: responses[4],
                        client: responses[5]
                    };

                    vm.flickr = {
                        server: responses[6],
                        client: responses[7]
                    };

                    vm.shutterstock = {
                        server: responses[8],
                        client: responses[9]
                    };
                })
                .finally(function() {
                    vm.isInitialised = true;
                    vm.isBusy = false;
                });
        }
    }
})();