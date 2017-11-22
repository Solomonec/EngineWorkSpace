$(function () {

    $("#changepassword").click(function () {
        $(".changepassword-modal-input").val("");
        $("#modalchangepassword").modal();
    });

   $(".button-cancel").click(function () {
        $(".changepassword-modal-input").val("");
        $.modal.close();
    });

    $("#changepasswordform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                var result = data;
                if (result === true) {
                    $(".changepassword-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="changepassword-success">Пароль успешно изменен!</span>');
                    $("#statusbar").show();
                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="changepassword-fail">Ошибка! Неудалось сменить пароль!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})