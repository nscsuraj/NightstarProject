function centreDiv(t) {
    return $(t).css({
        top: "50%",
        left: "50%",
        margin: "-" + $(t).height() / 2 + "px 0 0 -" + $(t).width() / 2 + "px"
    }), $(t)
}

function cms_toggleBoxClicked(ctrl) {
    //1 open 0 close
    var isOpen = $(ctrl).hasClass("active");
    if (isOpen) {
        $(ctrl).attr("class", "collapsed");
        $(ctrl).find(".fusion-toggle-heading").html($(ctrl).find(".fusion-toggle-heading").attr("data-Text"));
    } else {
        $(ctrl).attr("class", "active");
        $(ctrl).find(".fusion-toggle-heading").attr("data-Text", $(ctrl).find(".fusion-toggle-heading").html());
        $(ctrl).find(".fusion-toggle-heading").html("Read Less...");
    }
}

function cms_closeAlertBox(t) {
    $(t).parent().hide("slow")
}

function cms_sharingBoxIconClicked(t, e) {
    t.preventDefault();
    var o = $(e).attr("data-boxType"),
        a = $(e).parent().attr("data-title"),
        n = $(e).parent().attr("data-description"),
        s = $(e).parent().attr("data-link"),
        i = $(e).parent().attr("data-image"),
        r = "";
    switch (o) {
        case "linkd":
            r = "https://www.linkedin.com/shareArticle?mini=true&url=" + s + "&title=" + a + "&summary=" + n;
            break;
        case "fb":
            r = "http://www.facebook.com/sharer.php?s=100&p[url]=" + s + "&p[images][0]=" + i + "&p[title]=" + a + "&p[summary]=" + n;
            break;
        case "tw":
            r = "https://twitter.com/share?text=" + ("" != n ? n : a) + "&url=" + s;
            break;
        case "ytb":
            r = "https://www.youtube.com/user/StarMarCom?text=" + ("" != n ? n : a) + "&url=" + s;
            break;
        case "intgm":
            r = "https://www.instagram.com/starmicronics/?text=" + ("" != n ? n : a) + "&url=" + s;
            break;
        case "blog":
            r = "https://www.starmicronics.com/blog/?text=" + ("" != n ? n : a) + "&url=" + s;
            break;
        case "pint":
            r = "http://pinterest.com/pin/create/button/?url=" + s + "&description=" + n + "&media=" + i;
            break;
        case "tumb":
            r = "http://www.tumblr.com/share/link?url=" + s + "&name=" + a + "&description=" + n;
            break;
        case "gp":
            r = "https://plus.google.com/share?url=" + s;
            break;       
        case "redd":
            r = "http://reddit.com/submit?url=" + s + "&title=" + a;
            break;
        case "vk":
            r = "http://vkontakte.ru/share.php?url=" + s + "&title=" + a + "&description=" + n;
            break;
        case "mail":
            r = "mailto:?subject=" + a + "&body=" + s
    }
    window.open(r, "", "menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=600,width=600")
}

function cms_openModalFor(t, e) {
    t.preventDefault(), $("." + e).show("slow"), $("." + e).position({
        my: "left top",
        at: "left top",
        of: $(window)
    }), $(window).bind("resize", function () {
        $("." + e).position({
            my: "left top",
            at: "left top",
            of: $(window)
        })
    }), $(window).scroll(function () {
        $("." + e).position({
            my: "left top",
            at: "left top",
            of: $(window)
        })
    }), document.onkeydown = function (t) {
        t = t || window.event, 27 == t.keyCode && $("." + e).hide("slow")
    }
}

function cms_closeModal(t, e) {
    t.preventDefault(), $("." + e).hide("slow")
}

function cms_googleMap(t, e, o, a, n, s, i, r, l, p, c, m, g, d, u) {
    for (var w = "yes" === i ? google.maps.Animation.BOUNCE : "", h = new google.maps.Geocoder, f = {
        zoom: parseInt(o),
        scrollwheel: "yes" === a ? !0 : !1,
        scaleControl: "yes" === n ? !0 : !1,
        panControl: "yes" === s ? !0 : !1,
        center: new google.maps.LatLng(37.09, -95.71),
        mapTypeId: e,
        mapTypeControl: !0,
        mapTypeControlOptions: {
        style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
        poistion: google.maps.ControlPosition.TOP_RIGHT,
        mapTypeIds: [google.maps.MapTypeId.ROADMAP, google.maps.MapTypeId.TERRAIN, google.maps.MapTypeId.HYBRID, google.maps.MapTypeId.SATELLITE]
    },
        navigationControl: !0,
        navigationControlOptions: {
        style: google.maps.NavigationControlStyle.ZOOM_PAN
    },
        disableDoubleClickZoom: !0,
        streetViewControl: !0,
        draggableCursor: "move"
    }, y = new google.maps.Map(document.getElementById("divGoogleMapShow"), f), b = t.split("|"), $ = 0; $ < b.length; $++) ! function (t) {
        h.geocode({
            address: b[t]
        }, function (e, o) {
            if (o == google.maps.GeocoderStatus.OK) {
                y.setCenter(e[0].geometry.location);
                var a = new google.maps.Marker({
                    map: y,
                    position: e[0].geometry.location,
                    animation: w,
                    icon: u,
                    title: "Click me"
                }),
                    n = new google.maps.InfoWindow({
                        content: '<span id="spnInfowindow" style= color:' + g + "><b>Location info:<br/>" + b[t] + "<br/>Latitude:" + e[0].geometry.location.lat() + "<br/>Longitude:" + e[0].geometry.location.lng() + "</b></span>"
                    });
                google.maps.event.addListener(a, "click", function () {
                    n.open(y, a)
                })
            }
        })
    }($)
}

function cms_setCounterBoxEventDate(t, e) {
    var o, a;
    "string" == typeof t && t.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}[amp ]{0,3}$/i) ? (t = t.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}/), t = t.toString().split(":"), o = new Date, o.setHours(t[0]), o.setMinutes(t[1]), o.setSeconds(t[2])) : "string" == typeof t && t.match(/^now$/i) ? o = new Date : "string" == typeof t && t.match(/^tomorrow$/i) ? (o = new Date, o.setHours(24), o.setMinutes(0), o.setSeconds(1)) : o = new Date(t), "string" == typeof e && e.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}[amp ]{0,3}$/i) ? (e = e.match(/^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}/), e = e.toString().split(":"), a = new Date, a.setHours(e[0]), a.setMinutes(e[1]), a.setSeconds(e[2])) : "string" == typeof e && e.match(/^now$/i) ? a = new Date : "string" == typeof e && e.match(/^tomorrow$/i) ? (a = new Date, a.setHours(24), a.setMinutes(0), a.setSeconds(1)) : a = new Date(e);
    var n = a.getTime() - o.getTime() > 0 ? (a.getTime() - o.getTime()) / 1e3 : (864e5 + a.getTime() - o.getTime()) / 1e3;
    n = Math.abs(Math.floor(n));
    var s = {};
    return s.days = Math.floor(n / 86400), s.totalhours = Math.floor(n / 3600), s.totalmin = Math.floor(n / 60), s.totalsec = n, n -= 86400 * s.days, s.hours = Math.floor(n / 3600), n -= 3600 * s.hours, s.minutes = Math.floor(n / 60), n -= 60 * s.minutes, s.seconds = Math.floor(n), s
}