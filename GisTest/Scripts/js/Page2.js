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
        //sự kiện click vào địa điểm trên danh sách bản đổ
        function GetThongTin(e) {
            var codeHtml = "";
            $.ajax({
                url: "/Home/GetFullThongTinDoiTuongByValue?value=" + e,
                type: 'get',
                async: false,
                success: function (data) {
                    dulieuve = ' {"type": "FeatureCollection","features": [' + data[0].DuLieuVe + ']}';
                    pLat = data[0].Lat;
                    pLng = data[0].Lng;
                    zoom = data[0].Zoom;
                    data.forEach(function (item) {
                        codeHtml = '<div class="row item-info"><label class="col-6 ' + item.Code.replace("/", "") + '" ></label ><label class="col-6">' + item.Ten + '</label></div >' + codeHtml;
                    });
                    codeHtml = codeHtml + '<div class="row item-info"><label class="col-6" >Tên:</label ><label class="col-6">' + data[0].Ten + '</label></div >';
                }
            });
            return codeHtml;
        };
        //Click vao Map thi Load len thong tin cua dia diem do
        map.leaflet.on('click', function (e) {
            $.ajax({
                url: "/Home/GetThongTinByLatLng?lat=" + e.latlng.lat + "&lng=" + e.latlng.lng,
                type: 'get',
                success: function (obj) {
                    var htmlCode = GetThongTin(obj);
                    $(".load-info").html(htmlCode);
                    $("#show-info").css("display", "flex");
                }
            });
        });
    });
});