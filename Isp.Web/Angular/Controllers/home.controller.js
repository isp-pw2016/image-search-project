(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['homeService'];

    function HomeController(homeService) {
        var vm = this;

        test();

        function test() {
            var xd;
            homeService.getGoogleImages()
                .then(function(resp) {
                    xd = resp;
                    console.log('OK');
                });
        }
    }
})();