/*global angular, jQuery*/
(function ($) {
    angular
        .module('shopAdmin.product')
        .controller('ProductFormCtrl', ProductFormCtrl);

    /* @ngInject */
    function ProductFormCtrl($state, $stateParams, $http, categoryService, productService, summerNoteService, brandService) {
        var vm = this;
        // declare shoreDescription and description for summernote
        vm.product = { shortDescription: '', description: '', specification: '' };
        vm.product.CategoryIds = [];
        vm.categories = [];
        vm.thumbnailImage = null;
        vm.productImages = [];
        vm.productId = $stateParams.id;
        vm.isEditMode = vm.productId > 0;
        vm.brands = [];

        vm.shortDescUpload = function (files) {
            summerNoteService.upload(files[0])
                .success(function (url) {
                    $(vm.shortDescEditor).summernote('insertImage', url);
                });
        };

        vm.descUpload = function (files) {
            summerNoteService.upload(files[0])
                .success(function (url) {
                    $(vm.descEditor).summernote('insertImage', url);
                });
        };

        vm.specUpload = function (files) {
            summerNoteService.upload(files[0])
                .success(function (url) {
                    $(vm.specEditor).summernote('insertImage', url);
                });
        };

        vm.removeMedia = function removeMedia(media) {
            var index = vm.product.ProductMedias.indexOf(media);
            vm.product.ProductMedias.splice(index, 1);
            vm.product.DeletedMediaIds.push(media.Id);
        };

        vm.toggleCategories = function toggleCategories(categoryId) {
            var index = vm.product.CategoryIds.indexOf(categoryId);
            if (index > -1) {
                vm.product.CategoryIds.splice(index, 1);
            } else {
                vm.product.CategoryIds.push(categoryId);
            }
        };

        vm.save = function save() {
            var promise;
            if (vm.isEditMode) {
                promise = productService.editProduct(vm.product, vm.thumbnailImage, vm.productImages);
            } else {
                promise = productService.createProduct(vm.product, vm.thumbnailImage, vm.productImages);
            }

            promise.success(function (result) {
                    $state.go('product');
                })
                .error(function (error) {
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push('Could not add product.');
                    }
                });
        };

        function getProduct() {
            productService.getProduct($stateParams.id).then(function (result) {
                var i, index;
                vm.product = result.data;
            });
        }

        function getCategories() {
            categoryService.getCategories().then(function (result) {
                vm.categories = result.data;
            });
        }

        function getBrands() {
            brandService.getBrands().then(function (result) {
                vm.brands = result.data;
            });
        }

        function init() {
            if (vm.isEditMode) {
                getProduct();
            }
            getCategories();
            getBrands();
        }

        init();
    }
})(jQuery);