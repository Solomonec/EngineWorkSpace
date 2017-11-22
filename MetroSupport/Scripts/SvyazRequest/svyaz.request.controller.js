var requestapp = angular.module("SvyazRequestApp", []);

requestapp.run(function($rootScope) {
  
    $rootScope.category = categorymodel;
    $rootScope.requestor = requestormodel;
    $rootScope.theme = thememodel;
    $rootScope.assigner = assignermodel;
   
});

requestapp.controller("ButtonsControlCtrl", function ($scope) {

    $scope.saverequest = function() {
        angular.element("#svyazcallrequestform").submit();
    }

    $scope.openreturntoassigner = function () {
        angular.element("#modalreturnto").modal();
    }

    $scope.openholdon = function () {
        angular.element("#modalholdon").modal();
    }

    $scope.opendenyrequest = function () {
        angular.element("#modaldeny").modal();
    }

});

requestapp.controller("TroubleSubjectCtrl", function ($scope, $http, $compile) {

    var currentdataindex = "";
    var themesresult = "";


    $scope.searchdevice = "";

    $scope.openthemedialog = function () {
        angular.element('div.svyaz-themes-box').empty();
        angular.element('#modalthemes').modal();
        $http({
            url: '/SvyazCallRequest/GetThemes',
            method: 'POST',
            data: {department: "Svyaz"}
        }).success(function (data) {
            var result = "";
            themesresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"svyaz-themes-item\" data-index=\"" + i + "\" ng-click=\"themeitem($event)\">" + data[i].SubjectName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.svyaz-themes-box').append(result);

            }
        });
    }

    
    $scope.themeitem = function (e) {

        angular.forEach(angular.element('div.svyaz-themes-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('svyaz-themes-item-selected');
            div.addClass('svyaz-themes-item');

        });
        $(e.target).removeClass('svyaz-themes-item');
        $(e.target).addClass('svyaz-themes-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.themeacception = function () {

        $scope.theme.themename = themesresult[currentdataindex].SubjectName;
        
        $.modal.close();

    }

});

requestapp.controller("AssignerCtrl", function ($scope, $http, $compile) {
    
    var currentdataindex = "";
    var assignersresult = "";
    var departmentsresult = "";

   
    $scope.searchassigner = "";

    $scope.openassignerdialog = function () {
        angular.element('div.svyaz-assigner-department-box').empty();
        angular.element('div.svyaz-assigner-box').empty();
        $http({
            url: '/SvyazCallRequest/GetDepartments',
            method: 'POST'
         }).success(function (data) {
            var result = "";
            departmentsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"svyaz-assigner-department-item\" data-index=\""+ i + "\" ng-click=\"departmentitem($event)\">" + data[i].SubDepartmentName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.svyaz-assigner-department-box').append(result);

            }
        });
        angular.element('#modalassigners').modal();
    }

    
    $scope.departmentitem = function (e) {
        angular.element('div.svyaz-assigner-box').empty();
        var departmentname = departmentsresult[$(e.target).data("index")].SubDepartmentName;
        $http({
            url: '/SvyazCallRequest/GetAssigners',
            method: 'POST',
            data: { department: departmentname }
        }).success(function (data) {
            var result = "";
            assignersresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"svyaz-assigner-item\" data-index=\"" + i + "\" ng-click=\"assigneritem($event)\"> <div class=\"svyaz-assigner-tip\"></div>"
                                 + data[i].AssignerName +"</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.svyaz-assigner-box').append(result);

            }
        });
    }

    $scope.assigneritem = function (e) {

        angular.forEach(angular.element('div.svyaz-assigner-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('svyaz-assigner-item-selected');
            div.addClass('svyaz-assigner-item');

        });
        $(e.target).removeClass('svyaz-assigner-item');
        $(e.target).addClass('svyaz-assigner-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.assigneracception = function () {

        $scope.assigner.assignername = assignersresult[currentdataindex].AssignerName;

        $scope.assigner.bossname = assignersresult[currentdataindex].BossName;

        $scope.assigner.department = assignersresult[currentdataindex].Department;

        $scope.assigner.subdepartment = assignersresult[currentdataindex].SubDepartment;

        $.modal.close();
        

    }
});

requestapp.controller("RequestorCtrl", function ($scope, $http, $compile) {
    

    var currentdataindex = "";
    var requestorsresult = "";
   
    

    $scope.searchrequestor = "";

    $scope.openrequestordialog = function () {
        angular.element('div.svyaz-requestors-box').empty();
        angular.element('#modalrequestors').modal();
    }
    
    $scope.searchallrequestors = function () {
        angular.element('div.svyaz-requestors-box').empty();
        $http({
            url: '/SvyazCallRequest/GetRequestors',
            method: 'POST',
            data: { requestor: $scope.searchrequestor }
        }).success(function (data) {
            var result = "";
            requestorsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"svyaz-requestors-item\" data-index=\"" + i + "\" ng-click=\"requestoritem($event)\">" + data[i].RequestorName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.svyaz-requestors-box').append(result);

            }
        });
    }

    $scope.requestoritem = function(e) {

        angular.forEach(angular.element('div.svyaz-requestors-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('svyaz-requestors-item-selected');
            div.addClass('svyaz-requestors-item');

        });
        $(e.target).removeClass('svyaz-requestors-item');
        $(e.target).addClass('svyaz-requestors-item-selected');
        currentdataindex = $(e.target).data("index");
    }

    $scope.requestoracception = function () {

            $scope.requestor.requestorname = requestorsresult[currentdataindex].RequestorName;

            $scope.requestor.department = requestorsresult[currentdataindex].Organization;

            $scope.requestor.subdepartment = requestorsresult[currentdataindex].Department;

            $scope.requestor.tel = requestorsresult[currentdataindex].Tel;

            $scope.requestor.address = requestorsresult[currentdataindex].Address;

            $scope.requestor.room = requestorsresult[currentdataindex].Room;
            
            $scope.requestor.floor = requestorsresult[currentdataindex].Floor;

            $.modal.close();
        }

   
});

requestapp.controller("CategoriesCtrl", function ($scope, $http, $compile) {


    var currentdataindex = "";
    var categoryresult = "";
    var numberindex = "";

    $scope.searchrequestor = "";

    function cleanFilds(startnumber) {
        var position = (startnumber + 1);
            for (var i = position; i <= 5; i++) {
                $scope.category["subcategory" + (i + 1)] = "";
                $scope.category["nextsubcategory" + (i + 1)] = "";
                $scope.category["btncategory" + (i + 2)] = false;
            }
            if ($scope.category["modelcontainer"] === false) {
                $scope.category["modelid"] = "";
                $scope.category["model"] = "";
            }
        

    };

    function checkButtonsStatement() {
        for (var i = 0; i <= 5; i++) {

            var res = $scope.category["nextsubcategory" + i];
            if (res === "") res = angular.element("#nextsubcategory" + i).val();

            if (res !== "")
                $scope.category["btncategory" + (i + 1)] = true;
            else {
                $scope.category["btncategory" + (i + 1)] = false;
               
            }
        }
        if ($scope.category["modelid"] !== "" || angular.element("#modelid").val() !== "")
            $scope.category["modelcontainer"] = true;
        else {
            $scope.category["modelcontainer"] = false;
        }
       
    };

    checkButtonsStatement();
     
   

    $scope.opencategoriesdialog = function(e) {
        angular.element('div.svyaz-categories-box').empty();
        var categorykey = "";
        numberindex = ($(e.target).data('number') - 1);
        if (numberindex !== -1)
            categorykey = angular.element("#nextsubcategory" + ($(e.target).data("number") - 1)).val();
         
        $http({
            url: '/SvyazCallRequest/GetCategories',
            method: 'POST',
            data: { category: categorykey }
        }).success(function (data) {
            var result = "";
            categoryresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"svyaz-categories-item\" data-index=\"" + i + "\" ng-click=\"categoriesitem($event)\">" + data[i].CategoryName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.svyaz-categories-box').append(result);

            }
        });

        angular.element('#modalcategories').modal();
    };

   
    $scope.categoriesitem = function(e) {

        angular.forEach(angular.element('div.svyaz-categories-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('svyaz-categories-item-selected');
            div.addClass('svyaz-categories-item');

        });
        $(e.target).removeClass('svyaz-categories-item');
        $(e.target).addClass('svyaz-categories-item-selected');
        currentdataindex = $(e.target).data('index');

    };

    $scope.categoryacception = function () {
        if (numberindex === -1) {
            $scope.category["categoryname"] = categoryresult[currentdataindex].CategoryName;
            $scope.category["nextsubcategory0"] = categoryresult[currentdataindex].NextSubCategory;
        } else {
            $scope.category["subcategory" + (numberindex+1)] = categoryresult[currentdataindex].CategoryName;
            $scope.category["nextsubcategory" + (numberindex+1)] = categoryresult[currentdataindex].NextSubCategory;
            if (categoryresult[currentdataindex].ModelId !== "")
            $scope.category["modelid"] = categoryresult[currentdataindex].ModelId;
        }
        checkButtonsStatement();
        cleanFilds(numberindex);

        $.modal.close();
    };
});