(function() {
    'use strict';

    angular
        .module('app')
        .factory('commonFactory', commonFactory);

    commonFactory.inject = ['$q', '$timeout', 'toaster'];

    function commonFactory($q, $timeout, toaster) {
        var factory = {
            showError: showError,
            showInfo: showInfo,
            isUndefined: isUndefined,
            isObject: isObject,
            isStringNotNull: isStringNotNull,
            isBool: isBool,
            isNumber: isNumber,
            isArrayNotNull: isArrayNotNull,
            requestSuccess: requestSuccess,
            requestFailure: requestFailure
        };

        return factory;

        ////////////////////

        function showError(message, title, type) {
            $timeout(function() {
                toaster.pop({
                    type: type || 'error',
                    title: title || 'Error',
                    body: message || 'Was getting caught part of your plan?',
                    timeout: 8000,
                    showCloseButton: false
                });
            }, 0);
        }

        function showInfo(message, caption) {
            showError(message, caption, 'info');
        }

        function isUndefined(arg) {
            return typeof arg === 'undefined';
        }

        function isObject(arg) {
            return typeof arg === 'object' && arg !== null;
        }

        function isStringNotNull(arg) {
            return typeof arg === 'string' && arg.length > 0;
        }

        function isBool(arg) {
            return typeof arg === 'boolean';
        }

        function isNumber(arg) {
            return typeof arg === 'number';
        }

        function isArrayNotNull(arg) {
            return !!arg && arg.constructor === Array && arg.length > 0;
        }

        function requestSuccess(response) {
            return response.data;
        }

        function requestFailure(error) {
            var message = '';
            var title = '';

            if (isObject(error) && isObject(error.data)) {
                if (error.data.message.length) {
                    message = error.data.message;
                }
                if (error.data.title.length) {
                    title = error.data.title;
                }
            }

            showError(message, title);
            console.log(error);

            return $q.reject(error);
        }
    }
})();