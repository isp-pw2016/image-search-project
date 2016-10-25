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

        function getGoogleImages() {
            return $http.get(root + paths.getGoogleImages)
                .then(function(resp) {
                    return resp.data;
                })
                .catch(function(err) {
                    commonFactory.showError();
                    console.log(err);
                    return $q.reject();
                });
        }
    }
})();