﻿/*global angular*/
(function () {
    'use strict';

    angular.module('shopAdmin.brand', [])
        .config(['$stateProvider', function ($stateProvider) {
            $stateProvider
                .state('brand', {
                    url: '/brand',
                    templateUrl: 'admin/brand/brand-list.html',
                    controller: 'BrandListCtrl as vm'
                })
                .state('brand-create', {
                    url: '/brand/create',
                    templateUrl: 'admin/brand/brand-form.html',
                    controller: 'BrandCreateCtrl as vm'
                })
                .state('brand-edit', {
                    url: '/brand/edit/:id',
                    templateUrl: 'admin/brand/brand-form.html',
                    controller: 'BrandEditCtrl as vm'
                });
        }]);
})();