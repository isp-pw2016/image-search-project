(function() {
    'use strict';

    angular
        .module('app')
        .service('clientService', clientService);

    clientService.inject = ['$http', '$q', '$window', 'commonFactory'];

    function clientService($http, $q, $window, commonFactory) {
        var configs = $window.constants.configs;

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
            return getImages(configs.googleApiUrl, model, paramsGoogleImages, successGoogleImages);
        }

        function paramsGoogleImages(model) {
            return {
                key: configs.googleApiKey,
                cx: configs.googleEngineId,
                searchType: 'image',
                fields: 'items(link,title),searchInformation',
                start: _.max([model.skip, 1]),
                num: _.min([model.take, 10]),
                q: model.query
            };
        }

        function successGoogleImages(response) {
            var i;
            var model = {};
            var resp = response.data;
            var itemsCount = 0;

            model.imageItems = [];
            if (commonFactory.isArrayNotNull(resp.items)) {
                itemsCount = resp.items.length;
                for (i = 0; i < itemsCount; i++) {
                    var iter = resp.items[i];

                    model.imageItems.push({
                        link: iter.link,
                        title: iter.title
                    });
                }
            }

            if (commonFactory.isObject(resp.searchInformation)) {
                model.totalCount = commonFactory.isStringNotNull(resp.searchInformation.totalResults)
                    ? resp.searchInformation.totalResults
                    : itemsCount.toString();

                model.time = commonFactory.isNumber(resp.searchInformation.searchTime)
                    ? resp.searchInformation.searchTime
                    : undefined;
            }

            return model;
        }

        ////////////////////

        function getBingImages(model) {
            var headers = {
                'Ocp-Apim-Subscription-Key': configs.bingApiKey
            };

            return getImages(configs.bingApiUrl, model, paramsBingImages, successBingImages, headers);
        }

        function paramsBingImages(model) {
            return {
                q: model.query,
                count: _.min([model.take, 150]),
                offset: _.max([model.skip, 0])
            };
        }

        function successBingImages(response) {
            var i;
            var model = {};
            var resp = response.data;
            var itemsCount = 0;

            model.imageItems = [];
            if (commonFactory.isArrayNotNull(resp.value)) {
                itemsCount = resp.value.length;
                for (i = 0; i < itemsCount; i++) {
                    var iter = resp.value[i];

                    model.imageItems.push({
                        link: iter.contentUrl,
                        title: iter.name
                    });
                }
            }

            model.totalCount = commonFactory.isNumber(resp.totalEstimatedMatches)
                ? resp.totalEstimatedMatches.toString()
                : itemsCount.toString();

            return model;
        }

        ////////////////////

        function getInstagramImages(model) {
            var trimmed = _.trim(model.query);
            var hashtag = _.split(trimmed, ' ', 1);

            var url = _.replace(configs.instagramApiUrl, '{0}', hashtag);

            return getImages(url, model, paramsInstagramImages, successInstagramImages, undefined, 'JSONP');
        }

        function paramsInstagramImages(model) {
            return {
                access_token: configs.instagramAccessToken,
                count: model.take,
                callback: 'JSON_CALLBACK'
            };
        }

        function successInstagramImages(response) {
            var i;
            var model = {};
            var resp = response.data;

            model.imageItems = [];
            if (commonFactory.isArrayNotNull(resp.data)) {
                for (i = 0; i < resp.data.length; i++) {
                    var iter = resp.data[i];

                    var link = commonFactory.isObject(iter.images)
                        && commonFactory.isObject(iter.images.standard_resolution)
                        ? iter.images.standard_resolution.url
                        : null;
                    var title = commonFactory.isObject(iter.caption) ? iter.caption.text : null;

                    model.imageItems.push({
                        link: link,
                        title: title
                    });
                }
            }

            model.totalCount = '?';

            return model;
        }

        ////////////////////

        function getFlickrImages(model) {
            var headers = {
                'X-Requested-With': undefined
            };

            return getImages(configs.flickrApiUrl, model, paramsFlickrImages, successFlickrImages, headers);
        }

        function paramsFlickrImages(model) {
            var trimmed = _.trim(model.query);
            var tagsArr = _.split(trimmed, ' ', 20);
            var tags = _.join(tagsArr, ',');

            return {
                method: 'flickr.photos.search',
                api_key: configs.flickrApiKey,
                format: 'json',
                nojsoncallback: '1',
                tags: tags,
                sort: 'relevance',
                per_page: _.min([model.take, 500]),
                page: _.max([model.skip, 1])
            };
        }

        function successFlickrImages(response) {
            var i;
            var model = {};
            var resp = response.data;
            var itemsCount = 0;

            model.imageItems = [];

            if (!commonFactory.isObject(resp.photos)) {
                model.totalCount = '0';
                return model;
            }

            if (commonFactory.isArrayNotNull(resp.photos.photo)) {
                itemsCount = resp.photos.photo.length;
                for (i = 0; i < itemsCount; i++) {
                    var iter = resp.photos.photo[i];

                    var link = configs.flickrPhotoUrl;
                    link = _.replace(link, '{0}', iter.farm);
                    link = _.replace(link, '{1}', iter.server);
                    link = _.replace(link, '{2}', iter.id);
                    link = _.replace(link, '{3}', iter.secret);

                    model.imageItems.push({
                        link: link,
                        title: iter.title
                    });
                }
            }

            model.totalCount = commonFactory.isStringNotNull(resp.photos.total)
                ? resp.photos.total
                : itemsCount.toString();

            return model;
        }

        ////////////////////

        function getShutterstockImages(model) {
            var headers = {
                'Authorization': 'Basic ' + configs.shutterstockCredentials,
                'X-Requested-With': undefined
            };

            return getImages(configs.shutterstockApiUrl, model, paramsShutterstockImages, successShutterstockImages,
                headers);
        }

        function paramsShutterstockImages(model) {
            return {
                query: model.query,
                sort: 'relevance',
                license: ['commercial', 'editorial', 'enhanced', 'sensitive', 'NOT enhanced', 'NOT sensitive'],
                page: _.max([model.skip, 1]),
                per_page: _.min([model.take, 500])
            };
        }

        function successShutterstockImages(response) {
            var i;
            var model = {};
            var resp = response.data;
            var itemsCount = 0;

            model.imageItems = [];
            if (commonFactory.isArrayNotNull(resp.data)) {
                itemsCount = resp.data.length;
                for (i = 0; i < itemsCount; i++) {
                    var iter = resp.data[i];
                    var link = commonFactory.isObject(iter.assets) && commonFactory.isObject(iter.assets.preview)
                        ? iter.assets.preview.url
                        : null;

                    model.imageItems.push({
                        link: link,
                        title: iter.description
                    });
                }
            }

            model.totalCount = commonFactory.isNumber(resp.totalCount)
                ? resp.totalCount.toString()
                : itemsCount.toString();

            return model;
        }

        ////////////////////

        function getImages(apiUrl, model, requestParams, requestSuccess, headers, method) {
            var start = performance.now();

            if (!commonFactory.isStringNotNull(apiUrl)
                || !commonFactory.isObject(model)
                || !_.isFunction(requestParams)
                || !_.isFunction(requestSuccess)) {
                $q.reject();
            }

            var methodParsed = commonFactory.isStringNotNull(method) ? method : 'GET';
            var params = requestParams(model);

            return $http({
                    method: methodParsed,
                    url: apiUrl,
                    params: params,
                    headers: headers
                })
                .then(function(resp) {
                    var respParsed = requestSuccess(resp);
                    var stop = performance.now();

                    return requestResponse(respParsed, start, stop);
                }, requestFailure);
        }

        function requestResponse(obj, start, stop) {
            var time = stop - start;

            var objTimeString = '';
            if (commonFactory.isNumber(obj.time)) {
                var millis = _.round(obj.time * 1000, 6);
                objTimeString = millis.toString() + 'ms';
            }

            return {
                imageFetch: {
                    imageItems: obj.imageItems,
                    totalCount: obj.totalCount,
                    time: obj.time,
                    timeString: objTimeString
                },
                time: _.round(time / 1000, 6),
                timeString: _.round(time, 6).toString() + ' ms'
            };
        }

        function requestFailure(error) {
            return commonFactory.requestFailure(error);
        }
    }
})();