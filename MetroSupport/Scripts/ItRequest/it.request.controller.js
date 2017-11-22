var requestapp = angular.module("ItRequestApp", []);

requestapp.run(function($rootScope) {
    $rootScope.device = devicemodel;
    $rootScope.category = categorymodel;
    $rootScope.requestor = requestormodel;
    $rootScope.theme = thememodel;
    $rootScope.assigner = assignermodel;
   
});

requestapp.controller("ButtonsControlCtrl", function ($scope) {

    $scope.saverequest = function() {
        angular.element("#itcallrequestform").submit();
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
        angular.element('div.it-themes-box').empty();
        angular.element('#modalthemes').modal();
        $http({
            url: '/ItCallRequest/GetThemes',
            method: 'POST',
            data: {department: "IT"}
        }).success(function (data) {
            var result = "";
            themesresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"it-themes-item\" data-index=\"" + i + "\" ng-click=\"themeitem($event)\">" + data[i].SubjectName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.it-themes-box').append(result);

            }
        });
    }

    
    $scope.themeitem = function (e) {

        angular.forEach(angular.element('div.it-themes-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('it-themes-item-selected');
            div.addClass('it-themes-item');

        });
        $(e.target).removeClass('it-themes-item');
        $(e.target).addClass('it-themes-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.themeacception = function () {

        $scope.theme.themename = themesresult[currentdataindex].SubjectName;
        
        $.modal.close();

    }

    $scope.themeclose = function () {
        $.modal.close();
    }

});

requestapp.controller("DeviceInfoCtrl", function ($scope, $http, $compile) {

   
    var currentdataindex = "";
    var devicesresult = ""; 
    
   
    $scope.searchdevice = "";

    $scope.opendialog = function () {
        angular.element('div.it-devices-box').empty();
        angular.element('#modaldevices').modal();
    }


    $scope.search = function () {
        $http({
            url: '/ItCallRequest/GetDevices',
            method: 'POST',
            data: { invnumber: $scope.searchdevice }
        }).success(function (data) {
            var result = "";
            devicesresult = data;
            if (data != null)
            {
                for (i = 0; i < data.length; i++) {
                    result += "<div class=\"it-devices-item\" data-index=\"" + i + "\" ng-click=\"item($event)\">" + data[i].DevInvNum + " - " + data[i].DeviceType + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.it-devices-box').append(result);

            }
        });
    }

    $scope.item = function (e) {

        angular.forEach(angular.element('div.it-devices-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('it-devices-item-selected');
            div.addClass('it-devices-item');
            
        });
        $(e.target).removeClass('it-devices-item');
        $(e.target).addClass('it-devices-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.deviceacception = function () {

        $scope.device.invnumber = devicesresult[currentdataindex].DevInvNum;

        $scope.device.devicename = devicesresult[currentdataindex].DeviceType;

        $scope.device.serialnumber = devicesresult[currentdataindex].SerialNumber;

        $scope.device.deviceclass = devicesresult[currentdataindex].DeviceClass;

        $scope.device.devicetype = devicesresult[currentdataindex].DeviceType;

        $scope.device.devicemodel = devicesresult[currentdataindex].DeviceModel;

        $scope.device.dateinwork = devicesresult[currentdataindex].DateInWork;

        $scope.requestor.requestorname = devicesresult[currentdataindex].DeviceOwner.FullName;

        $scope.requestor.department = devicesresult[currentdataindex].DeviceOwner.Slugba;

        $scope.requestor.subdepartment = devicesresult[currentdataindex].DeviceOwner.Department;

        $scope.requestor.tel = devicesresult[currentdataindex].DeviceOwner.Tel;

        $scope.requestor.address = devicesresult[currentdataindex].DeviceOwner.Address;

        $scope.requestor.room = devicesresult[currentdataindex].DeviceOwner.Room;

        $scope.requestor.floor = devicesresult[currentdataindex].DeviceOwner.Floor;

       $.modal.close();
       
    }

    $scope.deviceclose = function() {
        $.modal.close();
    }
});


requestapp.controller("AssignerCtrl", function ($scope, $http, $compile) {
    
    var currentdataindex = "";
    var departmentsresult = "";
    var assignersresult = "";

   
    $scope.searchassigner = "";

    $scope.openassignerdialog = function () {
        angular.element('div.it-assigner-department-box').empty();
        angular.element('div.it-assigner-box').empty();
        $http({
            url: '/ItCallRequest/GetDepartments',
            method: 'POST'
         }).success(function (data) {
            var result = "";
            departmentsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"it-assigner-department-item\" data-index=\""+ i + "\" ng-click=\"departmentitem($event)\">" + data[i].SubDepartmentName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.it-assigner-department-box').append(result);

            }
        });
        angular.element('#modalassigners').modal();
    }

    
    $scope.departmentitem = function (e) {
        angular.element('div.it-assigner-box').empty();
        var departmentname = departmentsresult[$(e.target).data("index")].SubDepartmentName;
        $http({
            url: '/ItCallRequest/GetAssigners',
            method: 'POST',
            data: { department: departmentname }
        }).success(function (data) {
            var result = "";
            assignersresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"it-assigner-item\" data-index=\"" + i + "\" ng-click=\"assigneritem($event)\"> <div class=\"it-assigner-tip\"></div>"
                                 + data[i].AssignerName +"</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.it-assigner-box').append(result);

            }
        });
    }

    $scope.assigneritem = function (e) {

        angular.forEach(angular.element('div.it-assigner-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('it-assigner-item-selected');
            div.addClass('it-assigner-item');

        });
        $(e.target).removeClass('it-assigner-item');
        $(e.target).addClass('it-assigner-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.assigneracception = function () {

        $scope.assigner.assignername = assignersresult[currentdataindex].AssignerName;

        $scope.assigner.bossname = assignersresult[currentdataindex].BossName;

        $scope.assigner.department = assignersresult[currentdataindex].Department;

        $scope.assigner.subdepartment = assignersresult[currentdataindex].Organization;

        $.modal.close();
        

    }

    $scope.assignerclose = function () {
        $.modal.close();
    }
});

requestapp.controller("RequestorCtrl", function ($scope, $http, $compile) {
    

    var currentdataindex = "";
    var requestorsresult = "";
   
    

    $scope.searchrequestor = "";

    $scope.openrequestordialog = function () {
        angular.element('div.it-requestors-box').empty();
        angular.element('#modalrequestors').modal();
    }
    
    $scope.searchallrequestors = function () {
        angular.element('div.it-requestors-box').empty();
        $http({
            url: '/ItCallRequest/GetRequestors',
            method: 'POST',
            data: { requestor: $scope.searchrequestor }
        }).success(function (data) {
            var result = "";
            requestorsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"it-requestors-item\" data-index=\"" + i + "\" ng-click=\"requestoritem($event)\">" + data[i].RequestorName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.it-requestors-box').append(result);

            }
        });
    }

    $scope.requestoritem = function(e) {

        angular.forEach(angular.element('div.it-requestors-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('it-requestors-item-selected');
            div.addClass('it-requestors-item');

        });
        $(e.target).removeClass('it-requestors-item');
        $(e.target).addClass('it-requestors-item-selected');
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

    $scope.requestorclose = function () {
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

            if ($scope.category.modelid !== "" || angular.element("#modelid").val() !== "")
                $scope.category["modelcontainer"] = true;
            else {
                $scope.category["modelcontainer"] = false;
            }
        }
       
       
    };

    checkButtonsStatement();
     
   

    $scope.opencategoriesdialog = function(e) {
        angular.element('div.it-categories-box').empty();
        var categorykey = "";
        numberindex = ($(e.target).data('number') - 1);
        if (numberindex !== -1)
            categorykey = angular.element("#nextsubcategory" + ($(e.target).data("number") - 1)).val();
         
        $http({
            url: '/ItCallRequest/GetCategories',
            method: 'POST',
            data: { category: categorykey }
        }).success(function (data) {
            var result = "";
            categoryresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"it-categories-item\" data-index=\"" + i + "\" ng-click=\"categoriesitem($event)\">" + data[i].CategoryName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.it-categories-box').append(result);

            }
        });

        angular.element('#modalcategories').modal();
    };

   
    $scope.categoriesitem = function(e) {

        angular.forEach(angular.element('div.it-categories-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('it-categories-item-selected');
            div.addClass('it-categories-item');

        });
        $(e.target).removeClass('it-categories-item');
        $(e.target).addClass('it-categories-item-selected');
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
        angular.element('div.it-devicemodels-box').empty();
        angular.element('#modaldevicemodels').modal();
        $http({
            url: '/ItCallRequest/GetDeviceModels',
            method: 'POST',
            data: { index: angular.element('#modelid').val() }
        }).success(function (data) {
            var result = "";
            modelsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"it-devicemodels-item\" data-index=\"" + i + "\" ng-click=\"devicemodelitem($event)\">" + data[i].ModelName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.it-devicemodels-box').append(result);

            }
        });
    }


    $scope.devicemodelitem = function (e) {

        angular.forEach(angular.element('div.it-devicemodels-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('it-devicemodels-item-selected');
            div.addClass('it-devicemodels-item');

        });
        $(e.target).removeClass('it-devicemodels-item');
        $(e.target).addClass('it-devicemodels-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.devicemodelacception = function () {

        $scope.category.model = modelsresult[currentdataindex].ModelName;

        $.modal.close();

    }

    $scope.categoryclose = function () {
        $.modal.close();
    }

});