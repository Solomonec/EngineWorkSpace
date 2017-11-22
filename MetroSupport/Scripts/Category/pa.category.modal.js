$(function () {

    $("#addcategory").click(function () {
        $(".category-modal-input").val("");
        $("#categoryindexlist").empty();
        $("#subcategoryindexlist").empty();
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/Categories/GetPaCategories',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выберите категорию...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].CategoryIndexatorName + "\">" + data[i].CategoryIndexatorName + "</option>";

                    }
                    $("#categoryindexlist").append(results);
                }
            }
        });
        $("#modalcategory").modal();
    });

    $("span.editico").click(function () {
        $("#categoryindexlist").empty();
        $("#subcategoryindexlist").empty();

        var id = $(this).data('categoryid');

        $.ajax({
            type: 'Post',
            cache: false,
            url: '/Categories/GetAllPaCategories',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].CategoryIndexatorName + "\">" + data[i].CategoryIndexatorName + "</option>";

                    }
                    $("#categoryindexlist").append("<option value=\"\">Выберите категорию...</option>");
                    $("#categoryindexlist").append(results);
                    $("#subcategoryindexlist").append("<option value=\"\">Выберите подкатегорию...</option>");
                    $("#subcategoryindexlist").append(results);

                }
            },
            complete: function () {

                $.ajax({
                    url: "/Categories/PaCategoryInfo",
                    type: "POST",
                    data: { categoryid: id },
                    dataType: 'json',
                    success: function (data) {
                        if (data != null) {
                            $(".category-modal-input").val("");
                            $("#categoryid").val(data.CategoryId);
                            $("#categoryindexlist").val(data.CategoryIndexator);
                            $("#categoryname").val(data.CategoryName);
                            $("#subcategoryindexlist").val(data.NextSubCategory);
                            $("#typelist").val(data.CategoryType);
                            $("#modalcategory").modal();

                        } else {
                            $(".category-modal-input").val("");
                            $("#statusbar").html('<span class="category-fail">Ошибка! Неудалось получить данные категории!</span>');
                            $("#modalcategory").modal();
                        }


                    }

                });
            }
        });

    });

    $("#categoryindexlist").change(function () {

        $("#subcategoryindexlist").empty();
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/Categories/GetPaSubCategories',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выберите подкатегорию...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i].CategoryIndexatorName + "\">" + data[i].CategoryIndexatorName + "</option>";

                    }
                    $("#subcategoryindexlist").append(results);
                }
            }
        });
    });

    $(".button-cancel").click(function () {
        $(".category-modal-input").val("");
        $.modal.close();
    });

    $("#categoryform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data === true) {
                    $(".category-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="category-success">Категория сохранена в иерархии</span>');
                    $("#statusbar").show();

                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="category-fail">Ошибка! Возникли проблемы с сохранением категории в иерархию</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})