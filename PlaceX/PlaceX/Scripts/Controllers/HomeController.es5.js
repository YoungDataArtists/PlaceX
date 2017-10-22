'use strict';

var map;
var marker = new google.maps.Marker();
var coordinatesForServer = "";
var markers = [];

function initialize() {
    var centerPoint = new google.maps.LatLng(46.483232, 30.738147);

    var mapOptions = {
        center: centerPoint,
        zoom: 16,
        mapTypeId: 'roadmap',
        mapTypeControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false
    };

    map = new google.maps.Map(document.getElementById('map'), mapOptions);
    map.data.loadGeoJson('/Home/GetGeoJson');

    map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);
    });
}

google.maps.event.addDomListener(window, 'load', initialize);

function placeMarkerAndPanTo(latLng, map) {
    document.getElementById("input-position").value = latLng;
    marker.setMap(null);
    marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
    showMarkers();
}

function saveMarker() {
    markers.push(marker);
    coordinatesForServer += document.getElementById("input-position").value;
    document.getElementById("UpdateDiv").value = coordinatesForServer;
}

function showMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

