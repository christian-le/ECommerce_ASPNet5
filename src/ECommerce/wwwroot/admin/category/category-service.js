/*global angular*/
(function () {
    angular
        .module('shopAdmin.category')
        .factory('categoryService', categoryService);

    /* @ngInject */
    function categoryService($http) {
        var service = {
            getCategory: getCategory,
            createCategory: createCategory,
            editCategory: editCategory,
            deleteCategory: deleteCategory,
            getCategories: getCategories
        };
        return service;

        function getCategory(id) {
            return $http.get('Category/Get/' + id);
        }

        function getCategories() {
            return $http.get('Category/List');
        }

        function createCategory(category) {
            return $http.post('Category/Create', category);
        }

        function editCategory(category) {
            return $http.post('Category/Edit/' + category.Id, category);
        }

        function deleteCategory(category) {
            return $http.post('Category/Delete/' + category.Id, null);
        }
    }
})();