﻿/*global angular*/
(function () {
    'use strict';

    angular.module('shopAdmin.category', [])
        .config(['$stateProvider', function ($stateProvider) {
            $stateProvider
                .state('category', {
                    url: '/category',
                    templateUrl: 'admin/category/category-list.html',
                    controller: 'CategoryListCtrl as vm'
                })
                .state('category-create', {
                    url: '/category/create',
                    templateUrl: 'admin/category/category-form.html',
                    controller: 'CategoryCreateCtrl as vm'
                })
                .state('category-edit', {
                    url: '/category/edit/:id',
                    templateUrl: 'admin/category/category-form.html',
                    controller: 'CategoryEditCtrl as vm'
                });
        }]);
})();