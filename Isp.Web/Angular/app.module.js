(function() {
    'use strict';

    angular
        .module('app', [
            'toaster'
        ])
        .config([
            '$httpProvider', function($httpProvider) {
                $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
            }
        ]);
})();