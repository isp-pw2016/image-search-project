(function() {
    'use strict';

    angular
        .module('app')
        .service('homeService', homeService);

    homeService.inject = ['$http', '$q', 'constants', 'commonFactory'];

    function homeService($http, $q, constants, commonFactory) {
        var root = constants.root;
        var paths = constants.paths;

        var service = {
            getGoogleImages: getGoogleImages
        };

        return service;

        ////////////////////

        function getGoogleImages(query) {
            return $http.get(root + paths.getGoogleImages, {
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