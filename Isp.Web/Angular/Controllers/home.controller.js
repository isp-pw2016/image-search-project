(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$q', 'clientService', 'serverService', 'commonFactory'];

    function HomeController($q, clientService, serverService, commonFactory) {
        var vm = this;

        vm.model = {};
        vm.isBusy = false;
        vm.isInitialised = false;
        vm.startProcedure = startProcedure;

        vm.google = {};
        vm.bing = {};
        vm.instagram = {};
        vm.flickr = {};
        vm.shutterstock = {};

        vm.benchmarkServer = {};
        vm.benchmarkClient = {};
        vm.benchmarkCount = 0;

        vm.chartIsInitialised = false;
        vm.chart = chart;
        vm.chartData = [];
        vm.chartLabels = [];
        vm.chartSeries = [];
        vm.chartOptions = {};

        init();

        ////////////////////

        function init() {
            vm.model.skip = 0;
            vm.model.take = 10;

            vm.benchmarkServer = benchmarkModel();
            vm.benchmarkClient = benchmarkModel();
        }

        function benchmarkModel() {
            return {
                google: [],
                bing: [],
                instagram: [],
                flickr: [],
                shutterstock: []
            };
        }

        function startProcedure() {
            if (!commonFactory.isStringNotNull(vm.model.query)) {
                commonFactory.showInfo(
                    'Please provide the keywords which describe best the sought images',
                    'Empty query'
                );

                return;
            }

            vm.isBusy = true;

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

                    vm.benchmarkServer.google.push(vm.google.server.time);
                    vm.benchmarkClient.google.push(vm.google.client.time);

                    vm.benchmarkServer.bing.push(vm.bing.server.time);
                    vm.benchmarkClient.bing.push(vm.bing.client.time);

                    vm.benchmarkServer.instagram.push(vm.instagram.server.time);
                    vm.benchmarkClient.instagram.push(vm.instagram.client.time);

                    vm.benchmarkServer.flickr.push(vm.flickr.server.time);
                    vm.benchmarkClient.flickr.push(vm.flickr.client.time);

                    vm.benchmarkServer.shutterstock.push(vm.shutterstock.server.time);
                    vm.benchmarkClient.shutterstock.push(vm.shutterstock.client.time);

                    vm.benchmarkCount++;
                    vm.chartIsInitialised = false;
                })
                .finally(function() {
                    vm.isInitialised = true;
                    vm.isBusy = false;
                });
        }

        function chart() {
            vm.chartLabels = ['Google', 'Bing', 'Instagram', 'Flickr', 'Shutterstock'];
            vm.chartSeries = ['Server', 'Client'];

            vm.chartOptions = {
                scales: {
                    yAxes: [
                        {
                            scaleLabel: {
                                display: true,
                                labelString: 'Median [ms]'
                            }
                        }
                    ]
                },
                legend: {
                    display: true
                }
            };

            vm.isBusy = true;

            var googleServerMedianPromise = serverService.getMedian(vm.benchmarkServer.google);
            var googleClientMedianPromise = serverService.getMedian(vm.benchmarkClient.google);
            var bingServerMedianPromise = serverService.getMedian(vm.benchmarkServer.bing);
            var bingClientMedianPromise = serverService.getMedian(vm.benchmarkClient.bing);
            var instagramServerMedianPromise = serverService.getMedian(vm.benchmarkServer.instagram);
            var instagramClientMedianPromise = serverService.getMedian(vm.benchmarkClient.instagram);
            var flickrServerMedianPromise = serverService.getMedian(vm.benchmarkServer.flickr);
            var flickrClientMedianPromise = serverService.getMedian(vm.benchmarkClient.flickr);
            var shutterstockServerMedianPromise = serverService.getMedian(vm.benchmarkServer.shutterstock);
            var shutterstockClientMedianPromise = serverService.getMedian(vm.benchmarkClient.shutterstock);

            $q.all([
                    googleServerMedianPromise, googleClientMedianPromise, bingServerMedianPromise,
                    bingClientMedianPromise, instagramServerMedianPromise, instagramClientMedianPromise,
                    flickrServerMedianPromise, flickrClientMedianPromise, shutterstockServerMedianPromise,
                    shutterstockClientMedianPromise
                ])
                .then(function(responses) {
                    vm.chartData = [
                        [
                            responses[0].median, responses[2].median, responses[4].median, responses[6].median,
                            responses[8].median
                        ],
                        [
                            responses[1].median, responses[3].median, responses[5].median, responses[7].median,
                            responses[9].median
                        ]
                    ];

                    vm.chartIsInitialised = true;
                })
                .finally(function() {
                    vm.isBusy = false;
                });
        }
    }
})();