$(function () {

    $("#addlocation").click(function () {
        $(".location-modal-input").val("");
        $("#modallocation").modal();
    });

  


    $(".button-cancel").click(function () {
        $(".location-modal-input").val("");
        $.modal.close();
    });

    $("#locationform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data === true) {
                    $(".location-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="location-success">Площадка успешно сохранена!</span>');
                    $("#statusbar").show();
                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="location-fail">Ошибка! Неудалось сохранить площадку!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})