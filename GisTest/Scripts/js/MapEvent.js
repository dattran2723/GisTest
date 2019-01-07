﻿var map, dulieuve, layer, pLat, pLng, zoom;
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
    $("#listCity").on('click', '.node-item', function () {
        if (map.leaflet.hasLayer(layer)) {
            map.leaflet.removeLayer(layer);
        }
        var value = $(this).attr("data-id");
        var htmlCode = GetThongTin(value);
        //vẽ layer lên bản đồ
        //console.log(JSON.stringify(dulieuve));
        layer = new L.GeoJSON(JSON.parse(dulieuve));
        map.leaflet.addLayer(layer);

        //zoom đến địa điểm
        map.leaflet.setView([pLat, pLng], zoom);
        //add html để hiển thị thông tin
        $(".load-info").html(htmlCode);
        $("#show-info").css("display", "flex");
    });

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
});