$(function () {

    $("#adddevicemodel").click(function () {
        $("#modelindexatorlist").empty();

        $.ajax({
            type: 'Post',
            cache: false,
            url: '/DeviceModels/GetModelIndexators',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].ModelIndexatorName + "\">" + data[i].ModelIndexatorName + "</option>";

                    }
                    $("#modelindexatorlist").append("<option value=\"\">Выберите индекс...</option>");
                    $("#modelindexatorlist").append(results);
                   
                }
            }
        });

        $(".devicemodel-modal-input").val("");
        $("#modaldevice").modal();
    });

    $("span.editico").click(function () {

        $("#modelindexatorlist").empty();

        var id = $(this).data('modelid');
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/DeviceModels/GetModelIndexators',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].ModelIndexatorName + "\">" + data[i].ModelIndexatorName + "</option>";

                    }
                    $("#modelindexatorlist").append("<option value=\"\">Выберите индекс...</option>");
                    $("#modelindexatorlist").append(results);
                    $("#modaldevice").modal();
                }
                          
            },complete: function() {
                

               
                $.ajax({
                    url: "/DeviceModels/DeviceModelInfo",
                    type: "POST",
                    data: { modelid: id },
                    dataType: 'json',
                    success: function (data) {
                        if (data != null) {

                            $(".devicemodel-modal-input").val("");
                            $("#modelid").val(data.ModelId);
                            $("#modelindexatorlist").val(data.ModelIndexator);
                            $("#modelname").val(data.ModelName);
                            $("#departmentlist").val(data.Department);
                            $("#modaldevice").modal();

                        } else {

                            $("#statusbar").html('<span class="devicemodel-fail">Ошибка! Неудалось получить данные модели!</span>');
                            $("#modaldevice").modal();
                        }


                    }

                });


            }
        });
     

    });



    $(".button-cancel").click(function () {
        $(".device-modal-input").val("");
        $.modal.close();
    });

    $("#devicemodelform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data === true) {
                    $(".device-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="devicemodel-success">Устройство успешно сохранено!</span>');
                    $("#statusbar").show();

                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="devicemodel-fail">Ошибка! Неудалось сохранить устройство!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})