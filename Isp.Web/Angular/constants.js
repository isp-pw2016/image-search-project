(function () {
    'use strict';

    angular
        .module('app')
        .constant('constants', {
            'root': 'http://localhost:52487/',
            'paths': {
                'getGoogleImages': 'Home/GetGoogleImages'
            }
        });
})();