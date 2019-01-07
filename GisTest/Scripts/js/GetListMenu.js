$(document).ready(function () {
    $("#listCity").on('click', '.node-item', function () {
        var par = $(this).parent();
        var kd = par.parent();
        if (par.hasClass("open")) {
            par.removeClass("open");
            par.children("ul").hide();
        }
        else {
            kd.children("li").removeClass("open");
            kd.children("li").children("ul").hide();
            par.addClass("open");

            var value = $(this).attr("data-id");
            var result = CallAjax(value);
            if (result != "<ul></ul>") {
                par.append(result);
            }

        }
    });

    function CallAjax(e) {
        var htmlCode = "<ul>";
        $.ajax({
            url: "/Home/GetDiaDiem?value=" + e,
            type: 'get',
            async: false,
            success: function (data) {
                data.forEach(function (item) {
                    htmlCode = htmlCode + '<li><a href="javascript:;" data-id="' + item.Value + '" class="node-item"><span>' + item.Ten + '</span><span class="node-icon"><i class="fa fa-angle-left"></i></span></a ></li>';
                });
            }
        });
        return htmlCode + '</ul>';
    };

    $("a#hidden-listCity").on("click", function () {
        $("#listCity").toggleClass("hidden");
        $("#xinkciti-map").toggleClass("big");
        $(this).toggleClass("clicked");
    });
    $("a#hidden-info").on("click", function () {
        $("#show-info").toggleClass("hidden");
        $(this).toggleClass("clicked");
    });
});