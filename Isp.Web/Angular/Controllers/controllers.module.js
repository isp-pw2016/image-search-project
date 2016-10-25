(function () {
    'use strict';

    angular
        .module('controllers')
        .contrant('root', 'http://localhost:52487/')
        .constant('paths',
        {
            'home': {
                'getGoogleImages': 'Home/GoogleImages'
            }
        });
})();