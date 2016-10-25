(function() {
    'use strict';

    angular
        .module('app')
        .factory('commonFactory', commonFactory);

    commonFactory.inject = ['$timeout', 'toaster'];

    function commonFactory($timeout, toaster) {
        var factory = {
            showError: showError
        };

        return factory;

        function showError(message, caption) {
            $timeout(function() {
                toaster.pop({
                    type: 'error',
                    title: caption || 'Error',
                    body: message || 'Was getting caught part of your plan?',
                    showCloseButton: false
                });
            }, 0);
        }
    }
})();