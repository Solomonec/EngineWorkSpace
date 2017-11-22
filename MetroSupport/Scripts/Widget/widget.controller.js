var requestapp = angular.module("MetroWidgetApp", []);


requestapp.controller("RequestsCountCtrl", function ($scope, $http, $timeout) {

    $scope.timeoutflag = 0;
    $scope.inwork = 0;
    $scope.holdon = 0;
    $scope.close = 0;


    $timeout(function() {
        $http({
            url: '/Widget/RequestsCounts',
            method: 'POST'
        }).success(function(data) {

            if (data != null) {

                $scope.inwork = data.InWork;
                $scope.holdon = data.HoldOn;
                $scope.close = data.Close;
            }
        });
    }, 5000);

});

requestapp.controller("TopRequestsCtrl", function ($scope, $http, $timeout) {

    $scope.timeoutflag = 0;

    $timeout(function() {
        $http({
            url: '/Widget/TopCallRequests',
            method: 'POST'
        }).success(function(data) {
            var result = "";
            angular.element("#requestscontainer").empty();
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<a href=\"" + data[i].Department + "CallRequest/Index?id=" + data[i].RequestId + "\" class=\"toprequest-link\"><span class=\"toprequest-new\"></span><span class=\"toprequest-number\">" + data[i].RequestNumber +
                        "</span><span class=\"toprequest-theme\">" + data[i].Theme + "</span><span class=\"toprequest-arrow\"></span></a>";
                }

                angular.element("#requestscontainer").append(result);

            }
        });
    }, 10000);

});

