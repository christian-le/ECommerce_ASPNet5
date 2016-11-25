/*global angular*/
(function () {
    angular
        .module('shopAdmin.brand')
        .factory('brandService', brandService);

    /* @ngInject */
    function brandService($http) {
        var service = {
            getBrand: getBrand,
            createBrand: createBrand,
            editBrand: editBrand,
            deleteBrand: deleteBrand,
            getBrands: getBrands
        };
        return service;

        function getBrand(id) {
            return $http.get('Brand/Get/' + id);
        }

        function getBrands() {
            return $http.get('Brand/List');
        }

        function createBrand(brand) {
            return $http.post('Brand/Create', brand);
        }

        function editBrand(brand) {
            return $http.post('Brand/Edit/' + brand.Id, brand);
        }

        function deleteBrand(brand) {
            return $http.post('Brand/Delete/' + brand.Id, null);
        }
    }
})();