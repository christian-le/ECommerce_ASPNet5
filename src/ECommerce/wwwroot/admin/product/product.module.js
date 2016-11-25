/*global angular*/
(function () {
    'use strict';

    angular
        .module('shopAdmin.product', [])
        .config(['$stateProvider',
            function ($stateProvider) {
                $stateProvider
                    .state('product', {
                        url: '/product',
                        templateUrl: 'admin/product/product-list.html',
                        controller: 'ProductListCtrl as vm'
                    })
                    .state('product-create', {
                        url: '/product-create',
                        templateUrl: 'admin/product/product-form.html',
                        controller: 'ProductFormCtrl as vm'
                    })
                    .state('product-edit', {
                        url: '/product/edit/:id',
                        templateUrl: 'admin/product/product-form.html',
                        controller: 'ProductFormCtrl as vm'
                    })
                ;
            }
        ]);
})();