(function() {
    'use strict';

    angular
        .module('app', [
            'toaster',
            'chart.js'
        ])
        .config([
            '$httpProvider', function($httpProvider) {
                $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
            }
        ])
        .constant('_', window._);
})();