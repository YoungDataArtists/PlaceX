    var map;
    var myloc;

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
            fullscreenControl: false,
            gestureHandling: 'greedy'

        };

        map = new google.maps.Map(document.getElementById('map'), mapOptions);

        var clickHandler = new ClickEventHandler(map, centerPoint);

        //***Begin AutocompleteSearch***
        var input = /** @type {!HTMLInputElement} */(document.getElementById('search-input'));
        var autocomplete = new google.maps.places.Autocomplete(input);
        autocomplete.bindTo('bounds', map);
        autocomplete.addListener('place_changed', function () {
            var place = autocomplete.getPlace();

            if (!place.geometry) {
                // User entered the name of a Place that was not suggested and
                // pressed the Enter key, or the Place Details request failed.
                window.alert("No details available for input: '" + place.name + "'");
                return;
            }

            if (place.geometry.viewport) {
                map.fitBounds(place.geometry.viewport);
                clickHandler.getPlaceInformation(place.place_id);
            } else {
                map.setCenter(place.geometry.location);
                map.setZoom(17);  // Why 17? Because it looks good.
            }

        });
        autocomplete.setTypes(['establishment']);
        //***End AutocompleteSearch***      

        //Ask user if it is allowed to use his position
        watchMyPosition();
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
                    '<div><img src="' + place.icon + '" height="15" width="15"> '
                    + '<big><strong>' + place.name + '</strong></big><br>' + place.formatted_address + '</div>' +
                    '<div><img src="' + place.photos[0].getUrl({ 'maxWidth': 150, 'maxHeight': 150 }) +'"></div>'+
                    '<a href="/Home/PlaceInfo?placeId=' + place.place_id + '" target="_blank">Подробнее...</a>');
                map.setZoom(17); 
                me.infowindow.open(me.map);
            }
        });
    };

    //
    function watchMyPosition()
    {
        myloc = new google.maps.Marker({
            clickable: false,
            icon: new google.maps.MarkerImage('//maps.gstatic.com/mapfiles/mobile/mobileimgs2.png',
                new google.maps.Size(22, 22),
                new google.maps.Point(0, 18),
                new google.maps.Point(11, 11)),
            shadow: null,
            zIndex: 999,
            map: map
        });

        if (navigator.geolocation) 
            navigator.geolocation.watchPosition(function (pos) {
                var me = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
                myloc.setPosition(me);
                map.setCenter(me);
            }, function (error) {
                var btnMyLocation = document.getElementById('btn-my-location');
                btnMyLocation.classList.add("disabled");
                });
    }

    function showMyPosition()
    {
            map.setCenter(myloc.position);
    }






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
        var form = $('#__AjaxAntiForgeryForm');
        var token = $('input[name="__RequestVerificationToken"]', form).val();
        $.ajax({
            type: 'POST',
            url: "Account/LoginWithAjax",
            data: {
                __RequestVerificationToken: token,
                email: $('#email').val(),
                password: $('#password').val()
            },
            success: function (data, status, xhr) {
                if (data.Success)
                    window.location.replace("");
                else {
                    shakeModal();
                    return false;
                }

            },
            async: false
        });
        return false;
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
    //slider for placeinfo 
    $('.carousel[data-type="multi"] .item').each(function () {
        var next = $(this).next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }
        next.children(':first-child').clone().appendTo($(this));

        for (var i = 0; i < 2; i++) {
            next = next.next();
            if (!next.length) {
                next = $(this).siblings(':first');
            }

            next.children(':first-child').clone().appendTo($(this));
        }
    });


    
