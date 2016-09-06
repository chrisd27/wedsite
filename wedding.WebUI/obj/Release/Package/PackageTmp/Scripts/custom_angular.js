var app = angular.module('Wedsite', ['ngRoute', 'ngAnimate', 'ngTouch']);

app.controller('WedsiteController',["$scope", "WeddingService", function ($scope, WeddingService) {
    $scope.Wedding = null;
    WeddingService.GetData().then(function (d) {
        $scope.Wedding = d.data; // Success
    }, function () {
        alert('Failed'); // Failed
    });

}])

app.controller('navigationController',[ "$scope", "$timeout", function ($scope, $timeout) {

    var hasSwiped = false;

    var stopActions = function ($event) {
        if ($event.stopPropagation) {
            $event.stopPropagation();
        }
        if ($event.preventDefault) {
            $event.preventDefault();
        }
        $event.cancelBubble = true;
        $event.returnValue = false;
    };
    // Increment carousel thing
    $scope.slideNext = function ($event) {
        hasSwiped = true;
        stopActions($event);
        var currentEl = $('.menu_link.show')[0];
        var index = $(currentEl).attr('ng-slide-index');
        var nextIndex = Number(index) + 1;
        $scope.navigation[index].show = 'hide';
        if (nextIndex == $scope.navigation.length) {
            $scope.navigation[0].show = 'show';
        } else {
            $scope.navigation[nextIndex].show = 'show';
        }
        $timeout(function () { hasSwiped = false; }, 1000);
    };
    // Decrement carousel thing
    $scope.slidePrev = function ($event) {
        hasSwiped = true;
        stopActions($event);
        var currentEl = $('.menu_link.show')[0];
        var index = $(currentEl).attr('ng-slide-index');
        var nextIndex = Number(index) - 1;
        $scope.navigation[index].show = 'hide';
        if (nextIndex == -1) {
            $scope.navigation[$scope.navigation.length-1].show = 'show';
        } else {
            $scope.navigation[nextIndex].show = 'show';
        }
        $timeout(function () { hasSwiped = false; }, 1000);
    };

    $scope.slideToElAng = function (obj) {
        if (!hasSwiped) {
            console.log('in');
            var slideToId = obj.currentTarget.childNodes[1].dataset.slideto;
            var navHeight = $('#navBar').height();
            var top = 0;
            if (slideToId == 'mainSection') {
                var top = 0;
            } else if (!$('#mobile-navBar').hasClass('sticky')) {
                top = $('#' + slideToId).offset().top - (navHeight * 2);
            } else {
                top = $('#' + slideToId).offset().top - navHeight;
            }
            $('html, body').animate({ scrollTop: top }, 1000);
        }
    }
    /*    $scope.slideNext = function () {
        console.log($scope.navigation[nextIndex]);
        setTimeout(function () {
            $scope.$apply(function () {
                $scope.navigation[nextIndex].show = true;
                $scope.navigation[index].show = false;
            });
        }, 500);
    }; 

    $scope.slidePrev = function () {
        console.log('prev');
    };*/
    $scope.index = 0;
    // Links
    $scope.navigation = [{
        destination: "HOME",
        slideTo: "mainSection",
        slideIndex: 0,
        show: 'show'
    }, {
        destination: "SAVE THE DATE & RSVP",
        slideTo: "firstSection",
        slideIndex: 1,
        show: 'hide'
    }, {
        destination: "WEDDING",
        slideTo: "secondSection",
        slideIndex: 2,
        show: 'hide'
    }, {
        destination: "DIRECTIONS & SLEEP",
        slideTo: "thirdSection",
        slideIndex: 3,
        show: 'hide'
    }];
}])
.factory('WeddingService',["$http", function ($http) { // here I have created a factory which is a populer way to create and configure services
    var weddingId = '1003';
    var fac = {};                               //using factory method i am configuring service.
    fac.GetData = function () {
        return $http.get('/Wedsite/GetData/', { params: { weddingId: 1003 } });      //gets data from GetData Action in HomeController
    }
    return fac;
}]);