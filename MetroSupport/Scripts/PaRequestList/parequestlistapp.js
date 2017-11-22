
$(document).ready(function () {
    initializeControlButtons();
    initializeGridCheckboxes('PaCallRequestsGrid');
    initializeDatepickers();
    initializeAutocompliters();

});


function initializeControlButtons() {

    $("#newrequest").click(function() {

        window.open("/PaCallRequest/Index", "_blank");
        
    });


    $("#advsearch").click(function () {


        $("#searchthemelist").empty();
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/PaCallRequestList/GetPaThemes',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выберите тему...</option>";
                    for (i = 0; i < data.length; i++) {

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
            url: '/PaCallRequestList/GetPaThemes',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выберите тему...</option>";
                    for (i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].SubjectName + "\">" + data[i].SubjectName + "</option>";

                    }
                    $("#reportthemelist").append(results);
                }
            }
        });

        $("#modalreport").modal();

    });

    $("#deleterequest").click(function () {


        var checkGridSelection = "";
        var selected = $("#PaCallRequestsGrid [name=id]:checked").map(function () {
            checkGridSelection = "Selected";
            return $(this).val();
        }).get();

        if (checkGridSelection === "Selected") {
            var r = confirm("Выбранные вами заявки будут удалены. Удалить заявки?");
            if (r === true) {
                var selectedIds = selected.join(';');
                var ref = document.location;
                $.ajax({
                    type: 'Post',
                    cache: false,
                    url: '/PaCallRequestList/DeleteCallRequests/',
                    data: { selectedIds: selectedIds },
                    dataType: 'json',
                    success: function (data) {
                        if (data === true)
                            document.location = ref;
                        else
                            alert("Произошла ошибка во время удаления заявок");
                    }
                });
            }
        }

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
    for (var i = 0; i < people.length; i++) {
        $(people[i]).autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "/PaCallRequestList/GetPeople",
                    type: "POST",
                    dataType: "json",
                    data: { prefix: request.term },
                    success: function(data) {
                        response($.map(data, function(item) {
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