$(function () {

    $("#addindexator").click(function () {
        $(".indexator-modal-input").val("");
        $("#modalindexator").modal();
    });

    $("span.editico").click(function () {
        var id = $(this).data('indexatorid');
        $.ajax({
            url: "/Indexator/IndexatorInfo",
            type: "POST",
            data: { indexatorid: id },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    $(".indexator-modal-input").val("");
                    $("#indexatorid").val(data.IndexatorId);
                    $("#indexator").val(data.CategoryIndexatorName);
                    $("#categorytype").val(data.CategoryType);
                    $("#departmentlist").val(data.Department);
                    $("#modalindexator").modal();

                } else {

                    $("#statusbar").html('<span class="boss-fail">Ошибка! Неудалось получить данные ответственного!</span>');
                    $("#modalindexator").modal();
                }


            }

        });

    });



    $(".button-cancel").click(function () {
        $(".indexator-modal-input").val("");
        $.modal.close();
    });

    $("#indexatorform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (data) {
                if (data === true) {
                    $(".indexator-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="indexator-success">Индекс успешно сохранен!</span>');
                    $("#statusbar").show();

                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="indexator-fail">Ошибка! Неудалось сохранить индекс!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})