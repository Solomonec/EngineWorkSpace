var requestapp = angular.module("PaRequestApp", []);

requestapp.run(function($rootScope) {
    $rootScope.device = devicemodel;
    $rootScope.category = categorymodel;
    $rootScope.requestor = requestormodel;
    $rootScope.theme = thememodel;
    $rootScope.assigner = assignermodel;
   
});

requestapp.controller("ButtonsControlCtrl", function ($scope) {

    $scope.saverequest = function() {
        angular.element("#pacallrequestform").submit();
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
        angular.element('div.pa-themes-box').empty();
        angular.element('#modalthemes').modal();
        $http({
            url: '/PaCallRequest/GetThemes',
            method: 'POST',
            data: {department: "Pa"}
        }).success(function (data) {
            var result = "";
            themesresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"pa-themes-item\" data-index=\"" + i + "\" ng-click=\"themeitem($event)\">" + data[i].SubjectName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.pa-themes-box').append(result);

            }
        });
    }

    
    $scope.themeitem = function (e) {

        angular.forEach(angular.element('div.pa-themes-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('pa-themes-item-selected');
            div.addClass('pa-themes-item');

        });
        $(e.target).removeClass('pa-themes-item');
        $(e.target).addClass('pa-themes-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.themeacception = function () {

        $scope.theme.themename = themesresult[currentdataindex].SubjectName;
        
        $.modal.close();

    }

});

requestapp.controller("DeviceInfoCtrl", function ($scope, $http, $compile) {

   
    var currentdataindex = "";
    var devicesresult = ""; 
    
   
    $scope.searchdevice = "";

    $scope.opendialog = function () {
        angular.element('div.pa-devices-box').empty();
        angular.element('#modaldevices').modal();
    }


    $scope.search = function () {
        $http({
            url: '/PaCallRequest/GetDevices',
            method: 'POST',
            data: { invnumber: $scope.searchdevice }
        }).success(function (data) {
            var result = "";
            devicesresult = data;
            if (data != null)
            {
                for (i = 0; i < data.length; i++) {
                    result += "<div class=\"pa-devices-item\" data-index=\"" + i + "\" ng-click=\"item($event)\">" + data[i].DevInvNum + " - " + data[i].DeviceType + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.pa-devices-box').append(result);

            }
        });
    }

    $scope.item = function (e) {

        angular.forEach(angular.element('div.pa-devices-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('pa-devices-item-selected');
            div.addClass('pa-devices-item');
            
        });
        $(e.target).removeClass('pa-devices-item');
        $(e.target).addClass('pa-devices-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.deviceacception = function () {

        $scope.device.invnumber = devicesresult[currentdataindex].DevInvNum;

        $scope.device.devicename = devicesresult[currentdataindex].DeviceType;

        $scope.device.deviceclass = devicesresult[currentdataindex].DeviceClass;

        $scope.device.devicetype = devicesresult[currentdataindex].DeviceType;

        $scope.device.devicemodel = devicesresult[currentdataindex].DeviceModel;

        $scope.requestor.requestorname = devicesresult[currentdataindex].DeviceOwner.FullName;

        $scope.requestor.department = devicesresult[currentdataindex].DeviceOwner.Slugba;

        $scope.requestor.subdepartment = devicesresult[currentdataindex].DeviceOwner.Department;

        $scope.requestor.tel = devicesresult[currentdataindex].DeviceOwner.Tel;

        $scope.requestor.address = devicesresult[currentdataindex].DeviceOwner.Address;

        $scope.requestor.room = devicesresult[currentdataindex].DeviceOwner.Room;

        $scope.requestor.floor = devicesresult[currentdataindex].DeviceOwner.Floor;

       $.modal.close();
       
    }
});


requestapp.controller("AssignerCtrl", function ($scope, $http, $compile) {
    
    var currentdataindex = "";
    var assignersresult = "";
    var departmentsresult = "";

   
    $scope.searchassigner = "";

    $scope.openassignerdialog = function () {
        angular.element('div.pa-assigner-department-box').empty();
        angular.element('div.pa-assigner-box').empty();
        $http({
            url: '/PaCallRequest/GetDepartments',
            method: 'POST'
         }).success(function (data) {
            var result = "";
            departmentsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"pa-assigner-department-item\" data-index=\""+ i + "\" ng-click=\"departmentitem($event)\">" + data[i].SubDepartmentName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.pa-assigner-department-box').append(result);

            }
        });
        angular.element('#modalassigners').modal();
    }

    
    $scope.departmentitem = function (e) {
        angular.element('div.pa-assigner-box').empty();
        var departmentname = departmentsresult[$(e.target).data("index")].SubDepartmentName;
        $http({
            url: '/PaCallRequest/GetAssigners',
            method: 'POST',
            data: { department: departmentname }
        }).success(function (data) {
            var result = "";
            assignersresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"pa-assigner-item\" data-index=\"" + i + "\" ng-click=\"assigneritem($event)\"> <div class=\"pa-assigner-tip\"></div>"
                                 + data[i].AssignerName +"</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.pa-assigner-box').append(result);

            }
        });
    }

    $scope.assigneritem = function (e) {

        angular.forEach(angular.element('div.pa-assigner-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('pa-assigner-item-selected');
            div.addClass('pa-assigner-item');

        });
        $(e.target).removeClass('pa-assigner-item');
        $(e.target).addClass('pa-assigner-item-selected');
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
        angular.element('div.pa-requestors-box').empty();
        angular.element('#modalrequestors').modal();
    }
    
    $scope.searchallrequestors = function () {
        angular.element('div.pa-requestors-box').empty();
        $http({
            url: '/PaCallRequest/GetRequestors',
            method: 'POST',
            data: { requestor: $scope.searchrequestor }
        }).success(function (data) {
            var result = "";
            requestorsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"pa-requestors-item\" data-index=\"" + i + "\" ng-click=\"requestoritem($event)\">" + data[i].RequestorName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.pa-requestors-box').append(result);

            }
        });
    }

    $scope.requestoritem = function(e) {

        angular.forEach(angular.element('div.pa-requestors-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('pa-requestors-item-selected');
            div.addClass('pa-requestors-item');

        });
        $(e.target).removeClass('pa-requestors-item');
        $(e.target).addClass('pa-requestors-item-selected');
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
    var modelsresult = "";
    var numberindex = "";

    $scope.searchrequestor = "";

    function cleanFilds(startnumber) {
        var position = (startnumber + 1);
            for (var i = position; i <= 5; i++) {
                $scope.category["subcategory" + (i + 1)] = "";
                $scope.category["nextsubcategory" + (i + 1)] = "";
                $scope.category["btncategory" + (i + 2)] = false;
            }
            if ($scope.category["modelcontainer"] === true && startnumber === -1) {
                $scope.category["modelid"] = "";
                $scope.category["model"] = "";
                $scope.category["modelcontainer"] = false;
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
        angular.element('div.pa-categories-box').empty();
        var categorykey = "";
        numberindex = ($(e.target).data('number') - 1);
        if (numberindex !== -1)
            categorykey = angular.element("#nextsubcategory" + ($(e.target).data("number") - 1)).val();
         
        $http({
            url: '/PaCallRequest/GetCategories',
            method: 'POST',
            data: { category: categorykey }
        }).success(function (data) {
            var result = "";
            categoryresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"pa-categories-item\" data-index=\"" + i + "\" ng-click=\"categoriesitem($event)\">" + data[i].CategoryName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.pa-categories-box').append(result);

            }
        });

        angular.element('#modalcategories').modal();
    };

   
    $scope.categoriesitem = function(e) {

        angular.forEach(angular.element('div.pa-categories-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('pa-categories-item-selected');
            div.addClass('pa-categories-item');

        });
        $(e.target).removeClass('pa-categories-item');
        $(e.target).addClass('pa-categories-item-selected');
        currentdataindex = $(e.target).data('index');

    };

    $scope.categoryacception = function () {
        if (numberindex === -1) {
            $scope.category["categoryname"] = categoryresult[currentdataindex].CategoryName;
            $scope.category["nextsubcategory0"] = categoryresult[currentdataindex].NextSubCategory;
        } else {
            $scope.category["subcategory" + (numberindex+1)] = categoryresult[currentdataindex].CategoryName;
            $scope.category["nextsubcategory" + (numberindex+1)] = categoryresult[currentdataindex].NextSubCategory;
            if (categoryresult[currentdataindex].ModelIndexator !== "")
                $scope.category["modelid"] = categoryresult[currentdataindex].ModelIndexator;
        }
        checkButtonsStatement();
        cleanFilds(numberindex);

        $.modal.close();
    };


    $scope.opendevicemodelsdialog = function () {
        angular.element('div.pa-devicemodels-box').empty();
        angular.element('#modaldevicemodels').modal();
        $http({
            url: '/PaCallRequest/GetDeviceModels',
            method: 'POST',
            data: { index: angular.element('#modelid').val() }
        }).success(function (data) {
            var result = "";
            modelsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"pa-devicemodels-item\" data-index=\"" + i + "\" ng-click=\"devicemodelitem($event)\">" + data[i].ModelName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.pa-devicemodels-box').append(result);

            }
        });
    }


    $scope.devicemodelitem = function (e) {

        angular.forEach(angular.element('div.pa-devicemodels-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('pa-devicemodels-item-selected');
            div.addClass('pa-devicemodels-item');

        });
        $(e.target).removeClass('pa-devicemodels-item');
        $(e.target).addClass('pa-devicemodels-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.devicemodelacception = function () {

        $scope.category.model = modelsresult[currentdataindex].ModelName;

        $.modal.close();

    }
});