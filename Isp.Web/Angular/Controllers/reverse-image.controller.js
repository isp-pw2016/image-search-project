(function () {
    'use strict';

    angular
        .module('app')
        .controller('ReverseImageController', ReverseImageController);

    ReverseImageController.$inject = ['serverService', 'commonFactory'];

    function ReverseImageController(serverService, commonFactory) {
        var vm = this;

        vm.isBusy = false;
        vm.uploadImage = uploadImage;

        ////////////////////

        function uploadImage(file) {
            if (commonFactory.isUndefined(file) || file === null) {
                return;
            }

            if (!commonFactory.isNumber(file.size) || file.size > (1024 * 1024 * 10)) {
                commonFactory.showInfo('Image size up to 10MB', 'Size validation');
                return;
            }
            
            vm.isBusy = true;

            serverService.postImage(file)
                .then(function(response) {
                    console.log(response);
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();