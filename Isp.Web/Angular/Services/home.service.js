(function() {
    'use strict';

    angular
        .module('app')
        .service('homeService', homeService);

    homeService.inject = ['$http', '$q', '$window', 'commonFactory'];

    function homeService($http, $q, $window, commonFactory) {
        var paths = $window.constants.paths;

        var service = {
            getGoogleImages: getGoogleImages,
            getBingImages: getBingImages,
            getInstagramImages: getInstagramImages
        };

        return service;

        ////////////////////

        function getGoogleImages(model) {
            return getImages(paths.getGoogleImages, model);
        }

        function getBingImages(model) {
            return getImages(paths.getBingImages, model);
        }

        function getInstagramImages(model) {
            return getImages(paths.getInstagramImages, model);
        }

        ////////////////////

        function getImages(path, model) {
            return $http.get(path, { params: model })
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