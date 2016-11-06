(function() {
    'use strict';

    angular
        .module('app')
        .service('serverService', serverService);

    serverService.inject = ['$http', '$q', '$window', 'commonFactory'];

    function serverService($http, $q, $window, commonFactory) {
        var paths = $window.constants.paths;
        var enums = $window.constants.enums;

        var service = {
            getGoogleImages: getGoogleImages,
            getBingImages: getBingImages,
            getInstagramImages: getInstagramImages,
            getFlickrImages: getFlickrImages,
            getShutterstockImages: getShutterstockImages
        };

        return service;

        ////////////////////

        function getGoogleImages(model) {
            return getImages(enums.google, model);
        }

        function getBingImages(model) {
            return getImages(enums.bing, model);
        }

        function getInstagramImages(model) {
            return getImages(enums.instagram, model);
        }

        function getFlickrImages(model) {
            return getImages(enums.flickr, model);
        }

        function getShutterstockImages(model) {
            return getImages(enums.shutterstock, model);
        }

        ////////////////////

        function getImages(handler, model) {
            if (isNaN(handler) || !commonFactory.isObject(model)) {
                $q.reject();
            }

            return $http.get(paths.getImages, {
                    params: {
                        handler: handler,
                        query: model.query,
                        skip: model.skip,
                        take: model.take
                    }
                })
                .then(requestSuccess, requestFailure);
        }

        function requestSuccess(response) {
            return commonFactory.requestSuccess(response);
        }

        function requestFailure(error) {
            return commonFactory.requestFailure(error);
        }
    }
})();