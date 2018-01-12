
$(document).ready(function () {
    initializeControlButtons();
    initializeGridCheckboxes('ItCallRequestsGrid');
    initializeDatepickers();
    initializeAutocompliters();

});


function initializeControlButtons() {

    $("#newrequest").click(function() {

        window.open("/ItCallRequest/Index", "_blank");
        
    });


    $("#advsearch").click(function () {

        $("#searchthemelist").empty();
        
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/ItCallRequestList/GetItThemes',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выберите тему...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].SubjectName + "\">" + data[i].SubjectName + "</option>";

                    }
                    $("#searchthemelist").append(results);
                }
            }
        });
        $("#modalsearch").modal();
        
    });

    $("#report").click(function () {

        $("#reportthemelist").empty();
       
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/ItCallRequestList/GetItThemes',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выберите тему...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].SubjectName + "\">" + data[i].SubjectName + "</option>";

                    }
                    $("#reportthemelist").append(results);
                }
            }
        });

        $("#modalreport").modal();

    });

    $(".button-cancel").click(function (e) {
        $.modal.close();
    });

    $("#deleterequest").click(function () {


        var deleteRequests = new DeleteRequests();
        deleteRequests.DeleteConfirmation();
        deleteRequests.InitYes();
        deleteRequests.InitNo();
      

    });

 

}

function initializeGridCheckboxes(gridId) {
    var $grid = $('#' + gridId);
    $grid.on('click', '[name=chkAll]', function () {
        var isChecked = $(this).prop('checked');
        $grid.find('[name=id]').prop('checked', isChecked);
    });
}

function initializeDatepickers() {

    $.datetimepicker.setLocale('ru');
    
    $('#startdatepickersearch').datetimepicker({
        format: 'd.m.Y',
        timepicker: false,
        lang: 'ru'
    });

    $('#enddatepickersearch').datetimepicker({
        format: 'd.m.Y',
        timepicker: false,
        lang: 'ru'
    });

    $('#startdatepickerreport').datetimepicker({
        format: 'd.m.Y',
        timepicker: false,
        lang: 'ru'
    });

    $('#enddatepickerreport').datetimepicker({
        format: 'd.m.Y',
        timepicker: false,
        lang: 'ru'
    });




}

function initializeAutocompliters() {

    var people = ["#SearchAssigner", "#SearchBoss", "#SearchRequestor", "#ReportAssigner", "#ReportBoss", "#ReportRequestor"];
    for (var i = 0 ; i < people.length; i++) {
        $(people[i]).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/ItCallRequestList/GetPeople",
                    type: "POST",
                    dataType: "json",
                    data: { prefix: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.RequestorAltName + " (" + item.RequestorName + ")", value: item.RequestorName };
                        }));

                    }
                });
            },
            messages: {
                noResults: "",
                results: ""
            }

        });
    }
}

function DeleteRequests() {

    var checkGridSelection = "";

    function getCheckRequests() {

        return $("input[type=checkbox]:checked").map(function () {
            checkGridSelection = "Selected";
            return $(this).val();
        }).get();

    }

    this.DeleteConfirmation = function() {
        getCheckRequests();
        if (checkGridSelection === "Selected") $("#modaldeleteconfirm").modal();
    };

    this.InitYes = function() {
        $(".button-yes").click(function() {

                var selected = getCheckRequests();

                if (checkGridSelection === "Selected") {

                    var selectedIds = selected.join(';');
                    var ref = document.location;
                    $.ajax({
                        type: 'Post',
                        cache: false,
                        url: '/ItCallRequestList/DeleteCallRequests/',
                        data: { selectedIds: selectedIds },
                        dataType: 'json',
                        success: function(data) {
                            if (data === true)
                                document.location = ref;
                            else
                                $.modal.close();
                            alert("Произошла ошибка во время удаления заявок");
                        }
                    });
                }
            });
    }

    this.InitNo = function() {
        $(".button-no").click(
            function() {
                $.modal.close();
            });
    }

}

