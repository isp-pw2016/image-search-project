﻿(function() {
    'use strict';

    angular
        .module('app')
        .service('homeService', homeService);

    homeService.inject = ['$http', '$q', '$window', 'commonFactory'];

    function homeService($http, $q, $window, commonFactory) {
        var paths = $window.constants.paths;

        var service = {
            getGoogleImages: getGoogleImages
        };

        return service;

        ////////////////////

        function getGoogleImages(query) {
            return $http.get(paths.getGoogleImages, {
                    params: { query: query }
                })
                .then(requestSuccess)
                .catch(requestFailure);
        }

        ////////////////////

        function requestSuccess(response) {
            return commonFactory.requestSuccess(response);
        }

        function requestFailure(error) {
            return commonFactory.requestFailure(error);
        }
    }
})();