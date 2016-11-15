(function() {
    'use strict';

    angular
        .module('app')
        .service('serverService', serverService);

    serverService.inject = ['$http', '$q', '$window', 'commonFactory', 'Upload'];

    function serverService($http, $q, $window, commonFactory, Upload) {
        var paths = $window.constants.paths;
        var enums = $window.constants.enums;

        var service = {
            getGoogleImages: getGoogleImages,
            getBingImages: getBingImages,
            getInstagramImages: getInstagramImages,
            getFlickrImages: getFlickrImages,
            getShutterstockImages: getShutterstockImages,
            getMedian: getMedian,
            postImage: postImage
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

        function getMedian(time) {
            if (!commonFactory.isArrayNotNull(time)) {
                $q.reject();
            }

            var params = paths.getMedian + '?';
            for (var i = 0; i < time.length; i++) {
                var iter = time[i];

                if (isNaN(iter)) {
                    $q.reject();
                }

                if (i > 0) {
                    params += '&';
                }

                params = params.concat('time=', iter.toString());
            }

            return $http.get(params).then(requestSuccess, requestFailure);
        }

        function postImage(file) {
            return Upload.upload({
                    url: paths.postImage,
                    data: { file: file }
                })
                .then(requestSuccess, requestFailure);
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