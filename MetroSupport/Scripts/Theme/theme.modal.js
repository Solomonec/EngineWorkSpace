$(function () {

    $("#addtheme").click(function () {
        $(".theme-modal-input").val("");
        $("#modaltheme").modal();
    });

  
    $(".button-cancel").click(function () {
        $(".theme-modal-input").val("");
        $.modal.close();
    });

    $("#themeform").on("submit", function (e) {
        e.preventDefault();
        $("#statusbar").empty();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data === true) {
                    $(".theme-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="theme-success">Тема успешно сохранена!</span>');
                    $("#statusbar").show();

                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="theme-fail">Ошибка! Неудалось сохранить тему!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})