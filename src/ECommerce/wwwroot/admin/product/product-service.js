/*global angular*/
(function () {
    angular
        .module('shopAdmin.product')
        .factory('productService', productService);

    /* @ngInject */
    function productService($http, Upload) {
        var service = {
            getProducts: getProducts,
            createProduct: createProduct,
            editProduct: editProduct,
            getProduct: getProduct
        };
        return service;

        function getProduct(id) {
            return $http.get('Product/Get/' + id);
        }

        function getProducts(params) {
            return $http.post('Product/List', params);
        }

        function createProduct(product, thumbnailImage, productImages) {
            return Upload.upload({
                url: 'Product/Create',
                data: {
                    product: product,
                    thumbnailImage: thumbnailImage,
                    productImages: productImages
                }
            });
        }

        function editProduct(product, thumbnailImage, productImages) {
            return Upload.upload({
                url: 'Product/Edit/' + product.Id,
                data: {
                    product: product,
                    thumbnailImage: thumbnailImage,
                    productImages: productImages
                }
            });
        }
    }
})();