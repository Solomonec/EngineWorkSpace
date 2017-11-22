$(function () {

    $("#adddepartment").click(function () {
        $(".department-modal-input").val("");
        $("#modaldepartment").modal();
    });

    $("#viewdepartment").click(function () {
        $.ajax({
            url: "",
            type: "Post",
            data: { id: this.data('id') },
            success: function (data) {
                if (data === true) {
                    $(".department-modal-input").val("");
                    $("#statusbar").html('<span class="department-success">Исполнитель успешно сохранен!</span>');

                } else {

                    $("#statusbar").html('<span class="department-fail">Ошибка! Неудалось сохранить исполнителя!</span>');
                }


            }

        });

    });



    $(".button-cancel").click(function () {
        $(".department-modal-input").val("");
        $.modal.close();
    });

    $("#departmentform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data === true) {
                    $(".department-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="department-success">Отдел успешно сохранен!</span>');
                    $("#statusbar").show();

                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="department-fail">Ошибка! Неудалось сохранить отдел!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})