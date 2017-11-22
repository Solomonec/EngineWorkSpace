$(function() {

    $("#addassigner").click(function() {
        $(".assigner-modal-input").val("");
        $("#assignerform").modal();
    });

    $("span.editico").click(function() {
        var id = $(this).data('assignerid');
        var boss = "";
        $.ajax({
            url: "/Assigners/AssignerInfo",
            type: "POST",
            data: { assignerid:  id},
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    $(".assigner-modal-input").val("");
                    $("#assignerid").val(data.AssignerId);
                    $("#assignername").val(data.AssignerName);
                    boss = data.BossName;
                    $("#organization").val(data.Organization);
                    $("#departmentlist").val(data.Department);
                    $("#assignerform").modal();
                    
                } else {

                    $("#statusbar").html('<span class="assigner-fail">Ошибка! Неудалось получить данные исполнителя!</span>');
                    $("#assignerform").modal();
                }


            },
            complete: function () {

                var departmentvalue = $("#departmentlist option:selected").val();
                $("#bosslist").empty();
                $.ajax({
                    type: 'Post',
                    cache: false,
                    url: '/Assigners/GetDepartmentBosses',
                    data: { department: departmentvalue },
                    dataType: 'json',
                    success: function (data) {

                        if (data !== null) {
                            var results = "<option value=\"\">Выберите ответственного...</option>";
                            for (var i = 0; i < data.length; i++) {

                                results += "<option value=\"" + data[i].BossName + "\">" + data[i].BossName + "</option>";

                            }
                            $("#bosslist").append(results);
                            $("#bosslist").val(boss);
                        }
                    }
                });

            }

        });

    });



    $(".button-cancel").click(function() {
        $(".assigner-modal-input").val("");
        $.modal.close();
    });

    $("#assignerform").on("submit", function(e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function(data){
                if (data === true) {
                    $(".assigner-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="assigner-success">Исполнитель успешно сохранен!</span>');
                    $("#statusbar").show();

                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="assigner-fail">Ошибка! Неудалось сохранить исполнителя!</span>');
                    $("#statusbar").show();
                }


            }

      });

    });

    $("#departmentlist").change(function() {

      
        var departmentvalue = $("#departmentlist option:selected").val();
        $("#bosslist").empty();
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/Assigners/GetDepartmentBosses',
            data: { department: departmentvalue },
            dataType: 'json',
            success: function(data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выберите ответственного...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].BossName + "\">" + data[i].BossName + "</option>";

                    }
                    $("#bosslist").append(results);
                }
            }
        });
    });


});

