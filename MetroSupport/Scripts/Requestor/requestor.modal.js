$(function () {

    $("#addrequestor").click(function () {
        $(".requestor-modal-input").val("");
        $("#requestorform").modal();
    });

    $("span.editico").click(function () {
        var id = $(this).data('requestorid');
        $.ajax({
            url: "/RequestOwners/RequestOwnerInfo",
            type: "POST",
            data: { requestorid: id },
            dataType: 'json',
            success: function (data) {
                if (data != null) {

                    $(".requestor-modal-input").val("");
                    $("#requestorid").val(data.RequestorId);
                    $("#requestorname").val(data.RequestorName);
                    $("#requestoraltname").val(data.RequestorAltName);
                    $("#job").val(data.Job);
                    $("#organization").val(data.Organization);
                    $("#department").val(data.Department);
                    $("#address").val(data.Address);
                    $("#floor").val(data.Floor);
                    $("#room").val(data.Room);
                    $("#tel").val(data.Tel);
                    $("#requestorform").modal();
                } else {

                    $("#statusbar").html('<span class="requestor-fail">Ошибка! Неудалось получить данные заявителя!</span>');
                    $("#requestorform").modal();
                }


            }

        });

    });



    $(".button-cancel").click(function () {
        $(".requestor-modal-input").val("");
        $.modal.close();
    });

    $("#requestorform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                var result = data;
                if (result === true) {
                    $(".requestor-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="requestor-success">Заявитель успешно сохранен!</span>');
                    $("#statusbar").show();
                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="requestor-fail">Ошибка! Неудалось сохранить заявителя!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})