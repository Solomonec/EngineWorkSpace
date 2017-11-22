$(function () {

    $("#addboss").click(function () {
        $(".boss-modal-input").val("");
        $("#modalboss").modal();
    });

    $("span.editico").click(function () {
        var id = $(this).data('bossid');
        $.ajax({
            url: "/Bosses/BossInfo",
            type: "POST",
            data: { bossid: id },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    $(".boss-modal-input").val("");
                    $("#bossid").val(data.BossId);
                    $("#bossname").val(data.BossName);
                    $("#job").val(data.Organization);
                    $("#departmentlist").val(data.Department);
                    $("#modalboss").modal();

                } else {

                    $("#statusbar").html('<span class="boss-fail">Ошибка! Неудалось получить данные ответственного!</span>');
                    $("#modalboss").modal();
                }


            }

        });

    });



    $(".button-cancel").click(function () {
        $(".boss-modal-input").val("");
        $.modal.close();
    });

    $("#bossform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data === true) {
                    $(".boss-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="boss-success">Ответственный успешно сохранен</span>');


                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="boss-fail">Ошибка! Неудалось сохранить ответственного</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})