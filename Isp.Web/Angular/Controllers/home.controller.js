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

            var googlePromise = homeService.getGoogleImages(vm.model);
            var bingPromise = homeService.getBingImages(vm.model);
            var instagramPromise = homeService.getInstagramImages(vm.model);
            var flickrPromise = homeService.getFlickrImages(vm.model);
            var shutterstockPromise = homeService.getShutterstockImages(vm.model);

            $q.all([googlePromise, bingPromise, instagramPromise, flickrPromise, shutterstockPromise])
                .then(function(responses) {
                    vm.results = {
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