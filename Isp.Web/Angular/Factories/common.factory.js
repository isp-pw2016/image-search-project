(function() {
    'use strict';

    angular
        .module('app')
        .factory('commonFactory', commonFactory);

    commonFactory.inject = ['$timeout', 'toaster'];

    function commonFactory($timeout, toaster) {
        var factory = {
            showError: showError,
            showInfo: showInfo
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
    }
})();