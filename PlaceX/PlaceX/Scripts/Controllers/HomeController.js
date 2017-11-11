    var map;

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

        var clickHandler = new ClickEventHandler(map, centerPoint);
    }

    google.maps.event.addDomListener(window, 'load', initialize);

    var ClickEventHandler = function (map, origin) {
        this.origin = origin;
        this.map = map;
        this.infowindow = new google.maps.InfoWindow;
        this.placesService = new google.maps.places.PlacesService(map);
        // Listen for clicks on the map.
        this.map.addListener('click', this.handleClick.bind(this));
    };

    ClickEventHandler.prototype.handleClick = function (event) {
        if (event.placeId) {
            event.stop();
            this.getPlaceInformation(event.placeId);
        }
    };

    ClickEventHandler.prototype.getPlaceInformation = function (placeId) {
        var me = this;
        this.placesService.getDetails({ placeId: placeId }, function (place, status) {
            if (status === 'OK') {
                me.infowindow.close();
                me.infowindow.setPosition(place.geometry.location);
                me.infowindow.setContent(
                    '<div><img src="' + place.icon + '" height="50" width="50"> '
                    + '<big><strong>' + place.name + '</strong></big><br>' + place.formatted_address + '</div>' +
                    '<a href="/Home/PlaceInfo?placeId=' + place.place_id + '" target="_blank">Подробнее...</a>');
                me.infowindow.open(me.map);
            }
        });
    };








    function showRegisterForm() {
        $('.loginBox').fadeOut('fast', function () {
            $('.registerBox').fadeIn('fast');
            $('.login-footer').fadeOut('fast', function () {
                $('.register-footer').fadeIn('fast');
            });
            $('.modal-title').html('Register with');
        });
        $('.error').removeClass('alert alert-danger').html('');

    }
    function showLoginForm() {
        $('#loginModal .registerBox').fadeOut('fast', function () {
            $('.loginBox').fadeIn('fast');
            $('.register-footer').fadeOut('fast', function () {
                $('.login-footer').fadeIn('fast');
            });

            $('.modal-title').html('Login with');
        });
        $('.error').removeClass('alert alert-danger').html('');
    }

    function openLoginModal() {
        showLoginForm();
        setTimeout(function () {
            $('#loginModal').modal('show');
        }, 230);

    }
    function openRegisterModal() {
        showRegisterForm();
        setTimeout(function () {
            $('#loginModal').modal('show');
        }, 230);

    }

    function loginAjax() {
        /*   Remove this comments when moving to server
        $.post( "/login", function( data ) {
                if(data == 1){
                    window.location.replace("/home");            
                } else {
                     shakeModal(); 
                }
            });
        */

        /*   Simulate error message from the server   */
        shakeModal();
    }

    function shakeModal() {
        $('#loginModal .modal-dialog').addClass('shake');
        $('.error').addClass('alert alert-danger').html("Invalid email/password combination");
        $('input[type="password"]').val('');
        setTimeout(function () {
            $('#loginModal .modal-dialog').removeClass('shake');
        }, 1000);
    }
    // тут лютый костыль из-за особенностей гугл api
    setTimeout(mapPosition, 1000);
    function mapPosition() {
        $("#map").css({
            "position": "static",
            "height": "100%",
            "width": "100%"
        });
    }
    
