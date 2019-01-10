$(document).ready(function () {
    $("a#hidden-info").on("click", function () {
        $("#show-info").toggleClass("hidden");
        $(this).toggleClass("clicked");
    });
    var map;
    $(function () {

        var paramMapDefault = {
            lat: 10.167615,
            lng: 106.411514,
            zoom: 4,
            mode: "2d"
        };
        paramMap = {
            mode: paramMapDefault.mode,
            center: { lat: paramMapDefault.lat, lon: paramMapDefault.lng },
            tilt: 60,
            rotation: 0,
            zoom: paramMapDefault.zoom,
            plugins: [
                //MapGL.SearchComponent
            ]
        }
        map = MapGL.initMap("xinkciti-map", paramMap);
        //setview cho map khi load
        map.leaflet.setView([10.167615, 106.411514], 5)

        //Click vao Map thi Load len thong tin cua dia diem do
        map.leaflet.on('click', function (e) {
            $.ajax({
                url: "/Home/GetThongTinByLatLng?lat=" + e.latlng.lat + "&lng=" + e.latlng.lng,
                type: 'get',
                success: function (data) {
                    var htmlCode = '';
                    if (data.length > 0) {
                        data.forEach(function (item) {
                            htmlCode = '<div class="row item-info"><label class="col-6 ' + item.Code.replace("/", "") + '" ></label ><label class="col-6">' + item.Ten + '</label></div >' + htmlCode;
                        });
                        htmlCode = '<div class="row item-info"><label class="col-6">Quốc gia:</label><label class="col-6">Việt Nam</label></div>' + htmlCode + '<div class="row item-info"><label class="col-6" >Tên:</label ><label class="col-6">' + data[0].Ten + '</label></div >';
                    }

                    $("#show-info").removeClass("hidden");
                    $(".load-info").html(htmlCode);
                    $("#show-info").css("display", "flex");
                }
            });
        });
    });
});