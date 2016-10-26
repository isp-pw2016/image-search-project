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
            isObject: isObject,
            requestSuccess: requestSuccess,
            requestFailure: requestFailure
        };

        return factory;

        ////////////////////

        function showError(message, caption, type) {
            $timeout(function() {
                toaster.pop({
                    type: type || 'error',
                    title: caption || 'Error',
                    body: message || 'Was getting caught part of your plan?',
                    timeout: 10000,
                    showCloseButton: false
                });
            }, 0);
        }

        function showInfo(message, caption) {
            showError(message, caption, 'info');
        }

        function isObject(obj) {
            return typeof obj === 'object' && obj !== null;
        }

        function requestSuccess(response) {
            return response.data;
        }

        function requestFailure(error) {
            var message = '';
            if (isObject(error) && isObject(error.data) && error.data.message.length) {
                message = error.data.message;
            }

            showError(message);
            console.log(error);

            return $q.reject(error);
        }
    }
})();