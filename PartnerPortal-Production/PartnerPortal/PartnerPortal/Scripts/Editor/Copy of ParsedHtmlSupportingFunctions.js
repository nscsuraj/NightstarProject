function centreDiv(d) {
    $(d).css({ top: '50%', left: '50%', margin: '-' + ($(d).height() / 2) + 'px 0 0 -' + ($(d).width() / 2) + 'px' });
//    $(d).css("position", "absolute");
 //   $(d).css("top", Math.max(0, (($(window).height() - $(d).outerHeight()) / 2) +
                                         //       $(window).scrollTop()) + "px");
   // $(d).css("left", Math.max(0, (($(window).width() - $(d).outerWidth()) / 2) +
                                           //     $(window).scrollLeft()) + "px");
    return $(d);

    //d.style.position="absolute";
    //d.style.top = (h/2)-(divH/2)+"px";
    //d.style.left = (w/2)-(divW/2)+"px";
}

//for toggle box
function cms_toggleBoxClicked(ctrl) {
    //1 open 0 close
    var isOpen = $(ctrl).hasClass("active");if (isOpen) {$(ctrl).attr("class", "collapsed");$(ctrl).find(".fusion-toggle-heading").html($(ctrl).find(".fusion-toggle-heading").attr("data-Text"));} else {$(ctrl).attr("class", "active");$(ctrl).find(".fusion-toggle-heading").attr("data-Text", $(ctrl).find(".fusion-toggle-heading").html());
        $(ctrl).find(".fusion-toggle-heading").html("Read Less...");
    }
}

function cms_closeAlertBox(ctrl) {
    $(ctrl).parent().hide("slow");
}

function cms_sharingBoxIconClicked(ev, ctrl) {
    ev.preventDefault();
    var type = $(ctrl).attr("data-boxType");
    var title = $(ctrl).parent().attr("data-title");
    var descr = $(ctrl).parent().attr("data-description");
    var link = $(ctrl).parent().attr("data-link");
    var image = $(ctrl).parent().attr("data-image");

    var url = '';

    switch(type) {

        case 'fb':
        {
            url = 'http://www.facebook.com/sharer.php?s=100&p[url]=' + link + '&p[images][0]=' + image + '&p[title]=' + title + '&p[summary]=' + descr;
            break;
        }
        case 'tw':{
            url = 'https://twitter.com/share?text=' + (descr != "" ? descr : title) + '&url=' + link;
            break;
        }
        case 'pint': {
            url = 'http://pinterest.com/pin/create/button/?url=' + link + '&description=' + descr + '&media=' + image;
            break;
        }
        case 'tumb': {
            url = 'http://www.tumblr.com/share/link?url=' + link + '&name=' + title + '&description=' + descr;
            break;
        }
        case 'gp': {
            url = 'https://plus.google.com/share?url=' + link;
            break;
        }
        case 'linkd':
        {
            url = 'https://www.linkedin.com/shareArticle?mini=true&url=' + link + '&title=' + title + '&summary=' + descr;
            break;
        }
        case 'redd': {
            url = 'http://reddit.com/submit?url=' + link + '&title=' + title;
            break;
        }
        case 'vk': {
            url = 'http://vkontakte.ru/share.php?url=' + link + '&title=' + title + '&description=' + descr;
            break;
        }
        case 'mail': {
            url = 'mailto:?subject=' + title + '&body=' + link;
            break;
        }
    }


    window.open(url, '', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=600,width=600');
}

function cms_openModalFor(ev,modalSelector) {
    ev.preventDefault();
    $("." + modalSelector).show("slow");
    //centreDiv($("." + modalSelector));
    $("." + modalSelector).position({
        my: "left top",
        at: "left top",
        of: $(window)
    });
    $(window).bind('resize', function () {
        $("." + modalSelector).position({
            my: "left top",
            at: "left top",
            of: $(window)
        });
    });

    $(window).scroll(function () {
        $("." + modalSelector).position({
            my: "left top",
            at: "left top",
            of: $(window)
        });
    });

    document.onkeydown = function (evt) {
        evt = evt || window.event;
        if (evt.keyCode == 27) {
            $("." + modalSelector).hide("slow");
        }
    };
}

function cms_closeModal(ev, modalSelector) {
    ev.preventDefault();
    $("." + modalSelector).hide("slow");
}

function cms_googleMap(address, mapType, zoomLevel, scrollwheelonMap, showScaleControlonMap, showPanControlonMap,
    addressPinAnimation, showtooltipbydefault, selecttheMapStylingSwitch, mapOverlayColor, infoboxStyling, infoboxContent,
    infoBoxTextColor, infoBoxBackgroundColor, customMarkerIcon) {
    //showtooltipbydefault
    //selecttheMapStylingSwitch
    //mapOverlayColor
    //infoboxStyling
    //infoboxContent &&  address may be same
    //infoBoxBackgroundColor
    var infoAnimation = addressPinAnimation === 'yes' ? google.maps.Animation.BOUNCE : '';
    var geocoder = new google.maps.Geocoder();
    var myOptions =
    {
        zoom: parseInt(zoomLevel),
        scrollwheel: scrollwheelonMap === 'yes' ? true : false,
        scaleControl: showScaleControlonMap === 'yes' ? true : false,
        panControl: showPanControlonMap === 'yes' ? true : false,
        center: new google.maps.LatLng(37.09, -95.71),
        mapTypeId: mapType,
        mapTypeControl: true,
        mapTypeControlOptions:
        {
            style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
            poistion: google.maps.ControlPosition.TOP_RIGHT,
            mapTypeIds: [
                google.maps.MapTypeId.ROADMAP, google.maps.MapTypeId.TERRAIN, google.maps.MapTypeId.HYBRID,
                google.maps.MapTypeId.SATELLITE
            ]
        },
        navigationControl: true,
        navigationControlOptions:
        {
            style: google.maps.NavigationControlStyle.ZOOM_PAN
        },
        disableDoubleClickZoom: true,
        streetViewControl: true,
        draggableCursor: 'move'
    };
    var map = new google.maps.Map(document.getElementById("divGoogleMapShow"), myOptions);

    var singleAddress = address.split("|");
    for (var i = 0; i < singleAddress.length; i++) {
        (function(i) {
            geocoder.geocode({ 'address': singleAddress[i] },
                function(results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        map.setCenter(results[0].geometry.location);
                        var marker = new google.maps.Marker({
                            map: map,
                            position: results[0].geometry.location,
                            animation: infoAnimation,
                            icon: customMarkerIcon,
                            //https://maps.google.com/mapfiles/kml/shapes/parking_lot_maps.png
                            //https://maps.google.com/mapfiles/kml/shapes/library_maps.png
                            //https://maps.google.com/mapfiles/kml/shapes/info-i_maps.png
                            title: 'Click me'
                        });
                        var infowindow = new google.maps.InfoWindow({
                            content: '<span id="spnInfowindow" style= color:' +
                                infoBoxTextColor +
                                '><b>Location info:<br/>' +
                                singleAddress[i] +
                                '<br/>Latitude:' +
                                results[0].geometry.location.lat() +
                                '<br/>Longitude:' +
                                results[0].geometry.location.lng() +
                                '</b></span>'
                        });
                        google.maps.event.addListener(marker, 'click', function() { infowindow.open(map, marker); });
                    } else {
                        //alert("Geocode was not successful for the following reason: " + status);
                    }
                });
        })(i);
    }
}

function cms_setCounterBoxEventDate(startDt, endDt) {
    var obstartDt;
    var obendDt;
    if (typeof startDt == 'string' && startDt.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}[amp ]{0,3}$/i)) {
        startDt = startDt.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}/);
        startDt = startDt.toString().split(':');
        obstartDt = new Date();
        obstartDt.setHours(startDt[0]);
        obstartDt.setMinutes(startDt[1]);
        obstartDt.setSeconds(startDt[2]);
    } else if (typeof startDt == 'string' && startDt.match(/^now$/i)) obstartDt = new Date();
    else if (typeof startDt == 'string' && startDt.match(/^tomorrow$/i)) {
        obstartDt = new Date();
        obstartDt.setHours(24);
        obstartDt.setMinutes(0);
        obstartDt.setSeconds(1);
    } else obstartDt = new Date(startDt);

    if (typeof endDt == 'string' && endDt.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}[amp ]{0,3}$/i)) {
        endDt = endDt.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}/);
        endDt = endDt.toString().split(':');
        obendDt = new Date();
        obendDt.setHours(endDt[0]);
        obendDt.setMinutes(endDt[1]);
        obendDt.setSeconds(endDt[2]);
    } else if (typeof endDt == 'string' && endDt.match(/^now$/i)) obendDt = new Date();
    else if (typeof endDt == 'string' && endDt.match(/^tomorrow$/i)) {
        obendDt = new Date();
        obendDt.setHours(24);
        obendDt.setMinutes(0);
        obendDt.setSeconds(1);
    } else obendDt = new Date(endDt);
    var secondsDiff = (obendDt.getTime() - obstartDt.getTime()) > 0
        ? (obendDt.getTime() - obstartDt.getTime()) / 1000
        : (86400000 + obendDt.getTime() - obstartDt.getTime()) / 1000;
    secondsDiff = Math.abs(Math.floor(secondsDiff));

    var oDiff = {}; // object that will store data returned by this function

    oDiff.days = Math.floor(secondsDiff / 86400);
    oDiff.totalhours = Math.floor(secondsDiff / 3600); // total number of hours in difference
    oDiff.totalmin = Math.floor(secondsDiff / 60); // total number of minutes in difference
    oDiff.totalsec = secondsDiff; // total number of seconds in difference

    secondsDiff -= oDiff.days * 86400;
    oDiff.hours = Math.floor(secondsDiff / 3600); // number of hours after days

    secondsDiff -= oDiff.hours * 3600;
    oDiff.minutes = Math.floor(secondsDiff / 60); // number of minutes after hours

    secondsDiff -= oDiff.minutes * 60;
    oDiff.seconds = Math.floor(secondsDiff); // number of seconds after minutes

    return oDiff;
}