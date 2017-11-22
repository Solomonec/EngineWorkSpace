var requestapp = angular.module("AsppRequestApp", []);

requestapp.run(function($rootScope) {
    $rootScope.device = devicemodel;
    $rootScope.category = categorymodel;
    $rootScope.requestor = requestormodel;
    $rootScope.theme = thememodel;
   
});

requestapp.controller("ButtonsControlCtrl", function ($scope) {

    $scope.saverequest = function () {
        angular.element("#asppcallrequestform").submit();
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
        angular.element('div.aspp-themes-box').empty();
        angular.element('#modalthemes').modal();
        $http({
            url: '/AsppCallRequest/GetThemes',
            method: 'POST'
        }).success(function (data) {
            var result = "";
            themesresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"aspp-themes-item\" data-index=\"" + i + "\" ng-click=\"themeitem($event)\">" + data[i].SubjectName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.aspp-themes-box').append(result);

            }
        });
    }

    
    $scope.themeitem = function (e) {

        angular.forEach(angular.element('div.aspp-themes-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('aspp-themes-item-selected');
            div.addClass('aspp-themes-item');

        });
        $(e.target).removeClass('aspp-themes-item');
        $(e.target).addClass('aspp-themes-item-selected');
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
        angular.element('div.aspp-devices-box').empty();
        angular.element('#modaldevices').modal();
    }


    $scope.search = function () {
        angular.element('div.aspp-devices-box').empty();
        $http({
            url: '/AsppCallRequest/GetDevices',
            method: 'POST',
            data: { invnumber: $scope.searchdevice }
        }).success(function (data) {
            var result = "";
            devicesresult = data;
            if (data != null)
            {
                for (i = 0; i < data.length; i++) {
                    result += '<div class=\"aspp-devices-item\" data-index=\"' + i + '\" ng-click=\"item($event)\">' + data[i].DevInvNum + " - " + data[i].DeviceType + '</div>';
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.aspp-devices-box').append(result);

            }
        });
    }

    $scope.item = function (e) {

        angular.forEach(angular.element('div.aspp-devices-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('aspp-devices-item-selected');
            div.addClass('aspp-devices-item');
            
        });
        $(e.target).removeClass('aspp-devices-item');
        $(e.target).addClass('aspp-devices-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.deviceacception = function () {

        $scope.device.invnumber = devicesresult[currentdataindex].DevInvNum;

        $scope.device.devicename = devicesresult[currentdataindex].DeviceType;

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
});


requestapp.controller("AssignerCtrl", function ($scope, $http, $compile) {
    
    var currentdataindex = "";
    var assignersresult = "";
    var departmentsresult = "";

   
    $scope.searchassigner = "";

    $scope.openassignerdialog = function () {
        angular.element('div.aspp-assigner-department-box').empty();
        angular.element('div.aspp-assigner-box').empty();
        $http({
            url: '/AsppCallRequest/GetDepartments',
            method: 'POST',
         }).success(function (data) {
            var result = "";
            departmentsresult = data;
            if (data != null) {
                for (i = 0; i < data.length; i++) {
                    result += "<div class=\"aspp-assigner-department-item\" data-index=\""+ i + "\" ng-click=\"departmentitem($event)\">" + data[i].SubDepartment + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('aspp-assigner-department-box').append(result);

            }
        });
        angular.element('#modalassigners').modal();
    }

    
    $scope.departmentitem = function (e) {
        angular.element('div.aspp-assigner-box').empty();
        var departmentname = departmentsresult[$(e.target).data("index")].SubDepartment;
        $http({
            url: '/AsppCallRequest/GetAssigners',
            method: 'POST',
            data: { subdepartment: departmentname }
        }).success(function (data) {
            var result = "";
            assignersresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"aspp-assigner-item\" data-index=\"" + i + "\" ng-click=\"assigneritem($event)\"> <div class=\"aspp-assigner-tip\"></div>"
                                 + "<div class=\"aspp-assigner-item-text\">" + data[i].AssignerName + "</div></div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('aspp-assigner-box').append(result);

            }
        });
    }

    $scope.assigneritem = function (e) {

        angular.forEach(angular.element('div.aspp-assigner-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('aspp-assigner-item-selected');
            div.addClass('aspp-assigner-item');

        });
        $(e.target).removeClass('aspp-assigner-item');
        $(e.target).addClass('aspp-assigner-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.assigneracception = function () {

        $scope.assigner.ass = assignersresult[currentdataindex].AssignerName;

        $scope.assigner.devicename = assignersresult[currentdataindex].Organization;

        $scope.assigner.subdepartment = assignersresult[currentdataindex].Department;

        $scope.assigner.tel = assignersresult[currentdataindex].Tel;

    }
});

requestapp.controller("RequestorCtrl", function ($scope, $http, $compile) {
    

    var currentdataindex = "";
    var requestorsresult = "";
   
    

    $scope.searchrequestor = "";

    $scope.openrequestordialog = function () {
        angular.element('div.aspp-requestors-box').empty();
        angular.element('#modalrequestors').modal();
    }
    
    $scope.searchallrequestors = function () {
        angular.element('div.aspp-requestors-box').empty();
        $http({
            url: '/AsppCallRequest/GetRequestors',
            method: 'POST',
            data: { requestor: $scope.searchrequestor }
        }).success(function (data) {
            var result = "";
            requestorsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"aspp-requestors-item\" data-index=\"" + i + "\" ng-click=\"requestoritem($event)\">" + data[i].RequestorName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.aspp-requestors-box').append(result);

            }
        });
    }

    $scope.requestoritem = function(e) {

        angular.forEach(angular.element('div.aspp-requestors-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('aspp-requestors-item-selected');
            div.addClass('aspp-requestors-item');

        });
        $(e.target).removeClass('aspp-requestors-item');
        $(e.target).addClass('aspp-requestors-item-selected');
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

    function CheckButtonsStatement() {
        for (var i = 0; i <= 5; i++) {

            var res = $scope.category["nextsubcategory" + i];

            if (res !== "")
                $scope.category["btncategory" + (i + 1)] = true;
            else
                $scope.category["btncategory" + (i + 1)] = false;
        }
        if ($scope.category["modelid"] !== "") $scope.category["modelcontainer"] = true;
    }


    $scope.searchrequestor = "";


    CheckButtonsStatement();
     
   

    $scope.opencategoriesdialog = function(e) {
        angular.element('div.aspp-categories-box').empty();
        var categorykey = "";
        numberindex = ($(e.target).data('number') - 1);
        if (numberindex !== -1)
            categorykey = angular.element("#nextsubcategory" + ($(e.target).data("number") - 1)).val();
         
        $http({
            url: '/AsppCallRequest/GetCategories',
            method: 'POST',
            data: { category: categorykey }
        }).success(function (data) {
            var result = "";
            categoryresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"aspp-categories-item\" data-index=\"" + i + "\" ng-click=\"categoriesitem($event)\">" + data[i].CategoryName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.aspp-categories-box').append(result);

            }
        });

        angular.element('#modalcategories').modal();
    };

   
    $scope.categoriesitem = function(e) {

        angular.forEach(angular.element('div.aspp-categories-item-selected'), function(value, key) {
            var div = angular.element(value);
            div.removeClass('aspp-categories-item-selected');
            div.addClass('aspp-categories-item');

        });
        $(e.target).removeClass('aspp-categories-item');
        $(e.target).addClass('aspp-categories-item-selected');
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
        CheckButtonsStatement();
        cleanFilds(numberindex);

        $.modal.close();
    };


    $scope.opendevicemodelsdialog = function () {
        angular.element('div.aspp-devicemodels-box').empty();
        angular.element('#modaldevicemodels').modal();
        $http({
            url: '/AsppCallRequest/GetDeviceModels',
            method: 'POST',
            data: { index: angular.element('#modelid').val() }
        }).success(function (data) {
            var result = "";
            modelsresult = data;
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    result += "<div class=\"aspp-devicemodels-item\" data-index=\"" + i + "\" ng-click=\"devicemodelitem($event)\">" + data[i].ModelName + "</div>";
                }

                var linkFunc = $compile(result);
                result = linkFunc($scope);
                angular.element('div.aspp-devicemodels-box').append(result);

            }
        });
    }


    $scope.devicemodelitem = function (e) {

        angular.forEach(angular.element('div.aspp-devicemodels-item-selected'), function (value, key) {
            var div = angular.element(value);
            div.removeClass('aspp-devicemodels-item-selected');
            div.addClass('aspp-devicemodels-item');

        });
        $(e.target).removeClass('aspp-devicemodels-item');
        $(e.target).addClass('aspp-devicemodels-item-selected');
        currentdataindex = $(e.target).data("index");

    }

    $scope.devicemodelacception = function () {

        $scope.category.model = modelsresult[currentdataindex].ModelName;

        $.modal.close();

    }

   

});