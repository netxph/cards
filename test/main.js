//bootstraps requirejs and angularjs app

'use strict'

var cardsApp = angular.module('cardsApp',['ngRoute', 'ngResource']);

//configurations
cardsApp.constant('AppSettings', { 'serviceBaseUrl':'http://localhost/' })

var dependencies = [];

//patterns
var tests = /base\/test\/spec.*js$/;
var mocks = /base\/test\/mock.*js$/;
var depends = /base\/app\/scripts.*js$/;

for (var file in window.__karma__.files) {
  if (window.__karma__.files.hasOwnProperty(file)) {
    if (tests.test(file) || depends.test(file) || mocks.test(file)) {
      dependencies.push(file);
    }
  }
}

requirejs.config({
    // ask Require.js to load these files (all our tests)
    deps: dependencies,

    // start test run, once Require.js is done
    callback: function() {
        window.__karma__.start();
    }
});
