function hp07goto(t, e) {
    try {
        var i = $("div.hp07v0").first();
        if ("auto" == e && i.hasClass("hp07gonemanual")) return;
        if ("auto" == e && i.hasClass("hp07pause")) return void setTimeout(function () {
            hp07goto("+1", "auto")
        }, 1e3 * i.attr("data-hp07rotate"));
        i.addClass("hp07busy"), "auto" != e && i.addClass("hp07gonemanual");
        var n = i[0].current,
            s = i.find("div.hp07").length,
            o = n - 1 == 0 ? s : n - 1,
            r = n + 1 > s ? 1 : n + 1,
            u = null;
        /[+]/.test(t) ? (t = r, u = "n") : /[-]/.test(t) && (t = o, u = "p"), $(".hp07dnav").animate({
            opacity: "0"
        }, 300, "easeInOutSine"), $(".hp07selected").removeClass("hp07selected"), $("#fnav-" + t).addClass("hp07selected");
        var h = "-100%",
            l = "-80%",
            c = "100%",
            d = "-20%",
            f = "20%";
        (t > n && n != s && "p" != u || "n" == u) && (h = "100%", l = "80%", c = "-100%", d = "20%", f = "-20%"), i[0].autocount ? i.hasClass("hp07gonemanual") ? i[0].autocount = 0 : i[0].autocount = i[0].autocount + 1 : i[0].autocount = 2, "function" == typeof navTrack && "function" == typeof s_setAccount && i[0].autocount <= i.find(".hp07v1").length && "auto" != e && navTrack(s_setAccount()[1], s_setAccount()[2], "hp07", "show-hpf" + t + ":" + a + ":" + e);
        var p = i.width() < 620 ? 600 : 1e3;
        document.addEventListener ? ($("#feature-" + t).addClass("cfeature"), $("#feature-" + t).css("top", h).css("left", h), $("#hp07img-" + t).css("top", "0%").css("left", "0%").css("z-index", "0"), $("#hp07img-" + t).addClass("cslide"), $("#hp07img-" + t + " div").css("top", d).css("left", d), $("#hp07img-" + n).css("z-index", "2"), $("#feature-" + n).animate({
            top: c,
            left: c
        }, p, "easeInOutSine"), $("#hp07img-" + n + " div").animate({
            top: l,
            left: l
        }, p, "easeInOutSine"), $("#hp07img-" + n).animate({
            top: c,
            left: c
        }, p, "easeInOutSine", function () {
            $(this).css("top", c).css("left", c), $("#feature-" + n).removeClass("cfeature"), $("#hp07img-" + n).removeClass("cslide"), "auto" == e && setTimeout(function () {
                hp07goto("+1", "auto")
            }, 1e3 * i.attr("data-hp07rotate"))
        }), $("#hp07img-" + t + " div").animate({
            top: "0",
            left: "0"
        }, p, "easeInOutSine"), $("#hp07img-" + t).animate({
            top: "0",
            left: "0"
        }, p, "easeInOutSine"), $("#feature-" + t).animate({
            top: "0%",
            left: "0%"
        }, p, "easeInOutSine", function () {
            $(".hp07dnav").animate({
                opacity: "1"
            }, 150, "easeInOutSine"), $(".hp07busy").removeClass("hp07busy")
        })) : ($("#feature-" + t).addClass("cfeature"), $("#feature-" + t).css("top", h), $("#hp07img-" + t).css("top", "0").css("left", "0").css("z-index", "1"), $("#hp07img-" + t + " div").css("top", d), $("#hp07img-" + t).addClass("cslide"), $("#hp07img-" + n).css("z-index", "2"), $("#feature-" + n).animate({
            top: c
        }, p, "easeInOutSine"), $("#hp07img-" + n + " div").animate({
            top: l
        }, p, "easeInOutSine"), $("#hp07img-" + n).animate({
            top: c
        }, p, "easeInOutSine", function () {
            $(this).css("top", c), $("#feature-" + n).removeClass("cfeature"), $("#hp07img-" + n).removeClass("cslide"), "auto" == e && setTimeout(function () {
                hp07goto("+1", "auto")
            }, 1e3 * i.attr("data-hp07rotate"))
        }), $("#hp07img-" + t + " div").animate({
            top: "0"
        }, p, "easeInOutSine"), $("#hp07img-" + t).animate({
            top: "0"
        }, p, "easeInOutSine"), $("#feature-" + t).animate({
            top: "0%"
        }, p, "easeInOutSine", function () {
            $(".hp07dnav").animate({
                opacity: "1"
            }, 150, "easeInOutSine"), $(".hp07busy").removeClass("hp07busy")
        })), i[0].current = t, "auto" != e && i.addClass("hp07gonemanual")
    } catch (m) { }
}

function hp08resize() {
    jQuery("div.hp08promo").each(function () {
        var t = jQuery(this).find(".hp08teaser").height(),
            e = jQuery(this).height(),
            i = jQuery(this).find(".cmps-bttns span").width();
        jQuery(this).find(".hp08cta").css("height", e + "px").css("margin-top", -1 * t + "px").css("padding-top", 1 * t + "px"), jQuery(this).find(".hp08w3").css("top", e - t + "px").css("padding-right", i + 40 + "px"), jQuery(this).find(".hp08w4").css("padding-right", i + 40 + "px")
    })
}

function hp08showpromo(t) {
    var e = $("#" + t);
    e.parent().removeClass("height200ForIpad"), e.parent().removeClass("height400"), e.parent().find(".hp08cta").hide();
    var i = $("#" + $(".hp08v0").attr("data-cfilter"));
    i[0] && ($(".hp08prevf").removeClass("hp08prevf"), i.removeClass("hp08currentfilter").addClass("hp08prevf"), i.find(".hp08cta").hide()), e.addClass("hp08currentfilter"), e.find(".hp08promo a").length > 3 ? e.parent().addClass("height400") : e.parent().addClass("height200ForIpad"), e.find(".hp08promo a").css("top", "-150%"), $(".hp08selected").removeClass("hp08selected").removeClass("hp08default"), $('.hp08tablist a[href$="#' + t + '"]').parent().addClass("hp08selected"), $("#hp08selector a").html($('.hp08tablist a[href="#' + t + '"]').parent().text()), $(".hp08v0").attr("data-cfilter", t), e.find(".hp08promo a").each(function (t) {
        if (!$(this).find(".hp08img")[0] && $(this).is("[data-bgimg]")) {
            var e = $(this).is("[data-bgimg2x]") && $("html").hasClass("retina") ? $(this).attr("data-bgimg2x") : $(this).attr("data-bgimg");
            $(this).removeAttr("data-bgimg").removeAttr("data-bgimg2x"), imgpreload([e], function (t, i) {
                i.css("top", "-150%");
                var n = hp08imgpos(e);
                i.prepend('<div class="hp08img ' + n + '" style="background-image:url(\'' + e + "')\"></div>"), hp08loadp(i)
            }, $(this))
        } else {
            var i = $(this);
            if (i.closest(".hp08imgitems")[0]) {
                setTimeout(function () {
                    hp08loadp(i)
                }, t * (100 - 4 * t))
            } else hp08loadp(i)
        }
    })
}

function hp08loadp(t) {
    t.find(".hp08teaser").show(), hp08resize();
    var e = "hp08promo" + t.parent()[0].className.split("8promo")[1].split(" ")[0];
    t.closest(".hp08imgitems")[0] ? t.animate({
        top: "0%"
    }, 700, "easeInOutSine", function () {
        $(this).find(".hp08cta").css("display", "table"), $("div.hp08prevf ." + e + " a").css("top", "-150%")
    }) : (t.css("top", 0).show(), t.find(".hp08cta").css("display", "table"))
}

function hp08imgpos(t) {
    return /hp08c[13]p1/.test(t) ? " hp08bg-tl" : /hp08c[13]p2/.test(t) ? " hp08bg-tc" : /hp08c[13]p3/.test(t) ? " hp08bg-tr" : /hp08c2p1/.test(t) ? " hp08bg-tc" : /hp08c2p2/.test(t) ? " hp08bg-mc" : /hp08c2p3/.test(t) ? " hp08bg-bc" : ""
}

function imgpreload(t, e, i) {
    var n = 0,
        s = [];
    t = "[object Array]" === Object.prototype.toString.apply(t) ? t : [t];
    for (var o = function () {
            n += 1, n === t.length && e && e(s, i)
    }, a = 0; a < t.length; a++) s[a] = new Image, s[a].onabort = o, s[a].onerror = o, s[a].onload = o, s[a].src = t[a]
} ! function (t, e, i) {
    function n(t, e) {
        return typeof t === e
    }

    function s() {
        var t, e, i, s, o, a, r;
        for (var u in w)
            if (w.hasOwnProperty(u)) {
                if (t = [], e = w[u], e.name && (t.push(e.name.toLowerCase()), e.options && e.options.aliases && e.options.aliases.length))
                    for (i = 0; i < e.options.aliases.length; i++) t.push(e.options.aliases[i].toLowerCase());
                for (s = n(e.fn, "function") ? e.fn() : e.fn, o = 0; o < t.length; o++) a = t[o], r = a.split("."), 1 === r.length ? x[r[0]] = s : (!x[r[0]] || x[r[0]] instanceof Boolean || (x[r[0]] = new Boolean(x[r[0]])), x[r[0]][r[1]] = s), y.push((s ? "" : "no-") + r.join("-"))
            }
    }

    function o(t) {
        var e = C.className,
            i = x._config.classPrefix || "";
        if (k && (e = e.baseVal), x._config.enableJSClass) {
            var n = new RegExp("(^|\\s)" + i + "no-js(\\s|$)");
            e = e.replace(n, "$1" + i + "js$2")
        }
        x._config.enableClasses && (e += " " + i + t.join(" " + i), k ? C.className.baseVal = e : C.className = e)
    }

    function a() {
        return "function" != typeof e.createElement ? e.createElement(arguments[0]) : k ? e.createElementNS.call(e, "http://www.w3.org/2000/svg", arguments[0]) : e.createElement.apply(e, arguments)
    }

    function r() {
        var t = e.body;
        return t || (t = a(k ? "svg" : "body"), t.fake = !0), t
    }

    function u(t, i, n, s) {
        var o, u, h, l, c = "modernizr",
            d = a("div"),
            f = r();
        if (parseInt(n, 10))
            for (; n--;) h = a("div"), h.id = s ? s[n] : c + (n + 1), d.appendChild(h);
        return o = a("style"), o.type = "text/css", o.id = "s" + c, (f.fake ? f : d).appendChild(o), f.appendChild(d), o.styleSheet ? o.styleSheet.cssText = t : o.appendChild(e.createTextNode(t)), d.id = c, f.fake && (f.style.background = "", f.style.overflow = "hidden", l = C.style.overflow, C.style.overflow = "hidden", C.appendChild(f)), u = i(d, t), f.fake ? (f.parentNode.removeChild(f), C.style.overflow = l, C.offsetHeight) : d.parentNode.removeChild(d), !!u
    }

    function h(t, e) {
        return !!~("" + t).indexOf(e)
    }

    function l(t) {
        return t.replace(/([a-z])-([a-z])/g, function (t, e, i) {
            return e + i.toUpperCase()
        }).replace(/^-/, "")
    }

    function c(t, e) {
        return function () {
            return t.apply(e, arguments)
        }
    }

    function d(t, e, i) {
        var s;
        for (var o in t)
            if (t[o] in e) return i === !1 ? t[o] : (s = e[t[o]], n(s, "function") ? c(s, i || e) : s);
        return !1
    }

    function f(t) {
        return t.replace(/([A-Z])/g, function (t, e) {
            return "-" + e.toLowerCase()
        }).replace(/^ms-/, "-ms-")
    }

    function p(e, n) {
        var s = e.length;
        if ("CSS" in t && "supports" in t.CSS) {
            for (; s--;)
                if (t.CSS.supports(f(e[s]), n)) return !0;
            return !1
        }
        if ("CSSSupportsRule" in t) {
            for (var o = []; s--;) o.push("(" + f(e[s]) + ":" + n + ")");
            return o = o.join(" or "), u("@supports (" + o + ") { #modernizr { position: absolute; } }", function (t) {
                return "absolute" == getComputedStyle(t, null).position
            })
        }
        return i
    }

    function m(t, e, s, o) {
        function r() {
            c && (delete D.style, delete D.modElem)
        }
        if (o = n(o, "undefined") ? !1 : o, !n(s, "undefined")) {
            var u = p(t, s);
            if (!n(u, "undefined")) return u
        }
        for (var c, d, f, m, g, v = ["modernizr", "tspan"]; !D.style;) c = !0, D.modElem = a(v.shift()), D.style = D.modElem.style;
        for (f = t.length, d = 0; f > d; d++)
            if (m = t[d], g = D.style[m], h(m, "-") && (m = l(m)), D.style[m] !== i) {
                if (o || n(s, "undefined")) return r(), "pfx" == e ? m : !0;
                try {
                    D.style[m] = s
                } catch (y) { }
                if (D.style[m] != g) return r(), "pfx" == e ? m : !0
            } return r(), !1
    }

    function g(t, e, i, s, o) {
        var a = t.charAt(0).toUpperCase() + t.slice(1),
            r = (t + " " + A.join(a + " ") + a).split(" ");
        return n(e, "string") || n(e, "undefined") ? m(r, e, s, o) : (r = (t + " " + T.join(a + " ") + a).split(" "), d(r, e, i))
    }

    function v(t, e, n) {
        return g(t, i, i, e, n)
    }
    var y = [],
        w = [],
        b = {
            _version: "3.3.1",
            _config: {
                classPrefix: "",
                enableClasses: !0,
                enableJSClass: !0,
                usePrefixes: !0
            },
            _q: [],
            on: function (t, e) {
                var i = this;
                setTimeout(function () {
                    e(i[t])
                }, 0)
            },
            addTest: function (t, e, i) {
                w.push({
                    name: t,
                    fn: e,
                    options: i
                })
            },
            addAsyncTest: function (t) {
                w.push({
                    name: null,
                    fn: t
                })
            }
        },
        x = function () { };
    x.prototype = b, x = new x, x.addTest("history", function () {
        var e = navigator.userAgent;
        return -1 === e.indexOf("Android 2.") && -1 === e.indexOf("Android 4.0") || -1 === e.indexOf("Mobile Safari") || -1 !== e.indexOf("Chrome") || -1 !== e.indexOf("Windows Phone") ? t.history && "pushState" in t.history : !1
    });
    var _ = b._config.usePrefixes ? " -webkit- -moz- -o- -ms- ".split(" ") : [];
    b._prefixes = _;
    var C = e.documentElement,
        k = "svg" === C.nodeName.toLowerCase();
    k || ! function (t, e) {
        function i(t, e) {
            var i = t.createElement("p"),
                n = t.getElementsByTagName("head")[0] || t.documentElement;
            return i.innerHTML = "x<style>" + e + "</style>", n.insertBefore(i.lastChild, n.firstChild)
        }

        function n() {
            var t = w.elements;
            return "string" == typeof t ? t.split(" ") : t
        }

        function s(t, e) {
            var i = w.elements;
            "string" != typeof i && (i = i.join(" ")), "string" != typeof t && (t = t.join(" ")), w.elements = i + " " + t, h(e)
        }

        function o(t) {
            var e = y[t[g]];
            return e || (e = {}, v++, t[g] = v, y[v] = e), e
        }

        function a(t, i, n) {
            if (i || (i = e), c) return i.createElement(t);
            n || (n = o(i));
            var s;
            return s = n.cache[t] ? n.cache[t].cloneNode() : m.test(t) ? (n.cache[t] = n.createElem(t)).cloneNode() : n.createElem(t), !s.canHaveChildren || p.test(t) || s.tagUrn ? s : n.frag.appendChild(s)
        }

        function r(t, i) {
            if (t || (t = e), c) return t.createDocumentFragment();
            i = i || o(t);
            for (var s = i.frag.cloneNode(), a = 0, r = n(), u = r.length; u > a; a++) s.createElement(r[a]);
            return s
        }

        function u(t, e) {
            e.cache || (e.cache = {}, e.createElem = t.createElement, e.createFrag = t.createDocumentFragment, e.frag = e.createFrag()), t.createElement = function (i) {
                return w.shivMethods ? a(i, t, e) : e.createElem(i)
            }, t.createDocumentFragment = Function("h,f", "return function(){var n=f.cloneNode(),c=n.createElement;h.shivMethods&&(" + n().join().replace(/[\w\-:]+/g, function (t) {
                return e.createElem(t), e.frag.createElement(t), 'c("' + t + '")'
            }) + ");return n}")(w, e.frag)
        }

        function h(t) {
            t || (t = e);
            var n = o(t);
            return !w.shivCSS || l || n.hasCSS || (n.hasCSS = !!i(t, "article,aside,dialog,figcaption,figure,footer,header,hgroup,main,nav,section{display:block}mark{background:#FF0;color:#000}template{display:none}")), c || u(t, n), t
        }
        var l, c, d = "3.7.3",
            f = t.html5 || {},
            p = /^<|^(?:button|map|select|textarea|object|iframe|option|optgroup)$/i,
            m = /^(?:a|b|code|div|fieldset|h1|h2|h3|h4|h5|h6|i|label|li|ol|p|q|span|strong|style|table|tbody|td|th|tr|ul)$/i,
            g = "_html5shiv",
            v = 0,
            y = {};
        ! function () {
            try {
                var t = e.createElement("a");
                t.innerHTML = "<xyz></xyz>", l = "hidden" in t, c = 1 == t.childNodes.length || function () {
                    e.createElement("a");
                    var t = e.createDocumentFragment();
                    return "undefined" == typeof t.cloneNode || "undefined" == typeof t.createDocumentFragment || "undefined" == typeof t.createElement
                }()
            } catch (i) {
                l = !0, c = !0
            }
        }();
        var w = {
            elements: f.elements || "abbr article aside audio bdi canvas data datalist details dialog figcaption figure footer header hgroup main mark meter nav output picture progress section summary template time video",
            version: d,
            shivCSS: f.shivCSS !== !1,
            supportsUnknownElements: c,
            shivMethods: f.shivMethods !== !1,
            type: "default",
            shivDocument: h,
            createElement: a,
            createDocumentFragment: r,
            addElements: s
        };
        t.html5 = w, h(e), "object" == typeof module && module.exports && (module.exports = w)
    }("undefined" != typeof t ? t : this, e);
    var S = "Moz O ms Webkit",
        T = b._config.usePrefixes ? S.toLowerCase().split(" ") : [];
    b._domPrefixes = T;
    var W = function () {
        function t(t, e) {
            var s;
            return t ? (e && "string" != typeof e || (e = a(e || "div")), t = "on" + t, s = t in e, !s && n && (e.setAttribute || (e = a("div")), e.setAttribute(t, ""), s = "function" == typeof e[t], e[t] !== i && (e[t] = i), e.removeAttribute(t)), s) : !1
        }
        var n = !("onblur" in e.documentElement);
        return t
    }();
    b.hasEvent = W, x.addTest("hashchange", function () {
        return W("hashchange", t) === !1 ? !1 : e.documentMode === i || e.documentMode > 7
    }), x.addTest("cssgradients", function () {
        for (var t, e = "background-image:", i = "gradient(linear,left top,right bottom,from(#9f9),to(white));", n = "", s = 0, o = _.length - 1; o > s; s++) t = 0 === s ? "to " : "", n += e + _[s] + "linear-gradient(" + t + "left top, #9f9, white);";
        x._config.usePrefixes && (n += e + "-webkit-" + i);
        var r = a("a"),
            u = r.style;
        return u.cssText = n, ("" + u.backgroundImage).indexOf("gradient") > -1
    }), x.addTest("rgba", function () {
        var t = a("a").style;
        return t.cssText = "background-color:rgba(150,255,150,.5)", ("" + t.backgroundColor).indexOf("rgba") > -1
    });
    var E = function () {
        var e = t.matchMedia || t.msMatchMedia;
        return e ? function (t) {
            var i = e(t);
            return i && i.matches || !1
        } : function (e) {
            var i = !1;
            return u("@media " + e + " { #modernizr { position: absolute; } }", function (e) {
                i = "absolute" == (t.getComputedStyle ? t.getComputedStyle(e, null) : e.currentStyle).position
            }), i
        }
    }();
    b.mq = E;
    var M = b.testStyles = u;
    x.addTest("touchevents", function () {
        var i;
        if ("ontouchstart" in t || t.DocumentTouch && e instanceof DocumentTouch) i = !0;
        else {
            var n = ["@media (", _.join("touch-enabled),("), "heartz", ")", "{#modernizr{top:9px;position:absolute}}"].join("");
            M(n, function (t) {
                i = 9 === t.offsetTop
            })
        }
        return i
    });
    var A = b._config.usePrefixes ? S.split(" ") : [];
    b._cssomPrefixes = A;
    var $ = {
        elem: a("modernizr")
    };
    x._q.push(function () {
        delete $.elem
    });
    var D = {
        style: $.elem.style
    };
    x._q.unshift(function () {
        delete D.style
    }), b.testProp = function (t, e, n) {
        return m([t], i, e, n)
    }, b.testAllProps = g, b.testAllProps = v, x.addTest("boxshadow", v("boxShadow", "1px 1px", !0)), s(), o(y), delete b.addTest, delete b.addAsyncTest;
    for (var z = 0; z < x._q.length; z++) x._q[z]();
    t.Modernizr = x
}(window, document), jQuery("html").attr("class", jQuery("html").attr("class").replace(/touchevents/gi, "touch")), jQuery(document).ready(function (t) {
    t("input.autoclear").bind("focus", function (e) {
        var i = t(this).get(0);
        i.value == i.defaultValue && (i.value = "")
    }), t("input.autoclear").bind("blur", function (e) {
        var i = t(this).get(0);
        "" == i.value && (i.value = i.defaultValue)
    })
}),
    function (t) {
        "function" == typeof define && define.amd ? define(["jquery"], t) : t(jQuery)
    }(function (t) {
        function e(e, n) {
            var s, o, a, r = e.nodeName.toLowerCase();
            return "area" === r ? (s = e.parentNode, o = s.name, e.href && o && "map" === s.nodeName.toLowerCase() ? (a = t("img[usemap='#" + o + "']")[0], !!a && i(a)) : !1) : (/input|select|textarea|button|object/.test(r) ? !e.disabled : "a" === r ? e.href || n : n) && i(e)
        }
        t.ui = t.ui || {}, t.extend(t.ui, {
            version: "1.11.2",
            keyCode: {
                BACKSPACE: 8,
                COMMA: 188,
                DELETE: 46,
                DOWN: 40,
                END: 35,
                ENTER: 13,
                ESCAPE: 27,
                HOME: 36,
                LEFT: 37,
                PAGE_DOWN: 34,
                PAGE_UP: 33,
                PERIOD: 190,
                RIGHT: 39,
                SPACE: 32,
                TAB: 9,
                UP: 38
            }
        }), t.fn.extend({
            scrollParent: function (e) {
                var i = this.css("position"),
                    n = "absolute" === i,
                    s = e ? /(auto|scroll|hidden)/ : /(auto|scroll)/,
                    o = this.parents().filter(function () {
                        var e = t(this);
                        return n && "static" === e.css("position") ? !1 : s.test(e.css("overflow") + e.css("overflow-y") + e.css("overflow-x"))
                    }).eq(0);
                return "fixed" !== i && o.length ? o : t(this[0].ownerDocument || document)
            },
            uniqueId: function () {
                var t = 0;
                return function () {
                    return this.each(function () {
                        this.id || (this.id = "ui-id-" + ++t)
                    })
                }
            }(),
            removeUniqueId: function () {
                return this.each(function () {
                    /^ui-id-\d+$/.test(this.id) && t(this).removeAttr("id")
                })
            }
        }), t.extend(t.expr[":"], {
            data: t.expr.createPseudo ? t.expr.createPseudo(function (e) {
                return function (i) {
                    return !!t.data(i, e)
                }
            }) : function (e, i, n) {
                return !!t.data(e, n[3])
            },
            focusable: function (i) {
                return e(i, !isNaN(t.attr(i, "tabindex")))
            },
            tabbable: function (i) {
                var n = t.attr(i, "tabindex"),
                    s = isNaN(n);
                return (s || n >= 0) && e(i, !s)
            }
        }), t("<a>").outerWidth(1).jquery || t.each(["Width", "Height"], function (e, i) {
            function n(e, i, n, o) {
                return t.each(s, function () {
                    i -= parseFloat(t.css(e, "padding" + this)) || 0, n && (i -= parseFloat(t.css(e, "border" + this + "Width")) || 0), o && (i -= parseFloat(t.css(e, "margin" + this)) || 0)
                }), i
            }
            var s = "Width" === i ? ["Left", "Right"] : ["Top", "Bottom"],
                o = i.toLowerCase(),
                a = {
                    innerWidth: t.fn.innerWidth,
                    innerHeight: t.fn.innerHeight,
                    outerWidth: t.fn.outerWidth,
                    outerHeight: t.fn.outerHeight
                };
            t.fn["inner" + i] = function (e) {
                return void 0 === e ? a["inner" + i].call(this) : this.each(function () {
                    t(this).css(o, n(this, e) + "px")
                })
            }, t.fn["outer" + i] = function (e, s) {
                return "number" != typeof e ? a["outer" + i].call(this, e) : this.each(function () {
                    t(this).css(o, n(this, e, !0, s) + "px")
                })
            }
        }), t.fn.addBack || (t.fn.addBack = function (t) {
            return this.add(null == t ? this.prevObject : this.prevObject.filter(t))
        }), t("<a>").data("a-b", "a").removeData("a-b").data("a-b") && (t.fn.removeData = function (e) {
            return function (i) {
                return arguments.length ? e.call(this, t.camelCase(i)) : e.call(this)
            }
        }(t.fn.removeData)), t.ui.ie = !!/msie [\w.]+/.exec(navigator.userAgent.toLowerCase()), t.fn.extend({
            focus: function (e) {
                return function (i, n) {
                    return "number" == typeof i ? this.each(function () {
                        var e = this;
                        setTimeout(function () {
                            t(e).focus(), n && n.call(e)
                        }, i)
                    }) : e.apply(this, arguments)
                }
            }(t.fn.focus),
            disableSelection: function () {
                var t = "onselectstart" in document.createElement("div") ? "selectstart" : "mousedown";
                return function () {
                    return this.bind(t + ".ui-disableSelection", function (t) {
                        t.preventDefault()
                    })
                }
            }(),
            enableSelection: function () {
                return this.unbind(".ui-disableSelection")
            },
            zIndex: function (e) {
                if (void 0 !== e) return this.css("zIndex", e);
                if (this.length)
                    for (var i, n, s = t(this[0]) ; s.length && s[0] !== document;) {
                        if (i = s.css("position"), ("absolute" === i || "relative" === i || "fixed" === i) && (n = parseInt(s.css("zIndex"), 10), !isNaN(n) && 0 !== n)) return n;
                        s = s.parent()
                    }
                return 0
            }
        }), t.ui.plugin = {
            add: function (e, i, n) {
                var s, o = t.ui[e].prototype;
                for (s in n) o.plugins[s] = o.plugins[s] || [], o.plugins[s].push([i, n[s]])
            },
            call: function (t, e, i, n) {
                var s, o = t.plugins[e];
                if (o && (n || t.element[0].parentNode && 11 !== t.element[0].parentNode.nodeType))
                    for (s = 0; o.length > s; s++) t.options[o[s][0]] && o[s][1].apply(t.element, i)
            }
        };
        var n = 0,
            s = Array.prototype.slice;
        t.cleanData = function (e) {
            return function (i) {
                var n, s, o;
                for (o = 0; null != (s = i[o]) ; o++) try {
                    n = t._data(s, "events"), n && n.remove && t(s).triggerHandler("remove")
                } catch (a) { }
                e(i)
            }
        }(t.cleanData), t.widget = function (e, i, n) {
            var s, o, a, r, u = {},
                h = e.split(".")[0];
            return e = e.split(".")[1], s = h + "-" + e, n || (n = i, i = t.Widget), t.expr[":"][s.toLowerCase()] = function (e) {
                return !!t.data(e, s)
            }, t[h] = t[h] || {}, o = t[h][e], a = t[h][e] = function (t, e) {
                return this._createWidget ? void (arguments.length && this._createWidget(t, e)) : new a(t, e)
            }, t.extend(a, o, {
                version: n.version,
                _proto: t.extend({}, n),
                _childConstructors: []
            }), r = new i, r.options = t.widget.extend({}, r.options), t.each(n, function (e, n) {
                return t.isFunction(n) ? void (u[e] = function () {
                    var t = function () {
                        return i.prototype[e].apply(this, arguments)
                    },
                        s = function (t) {
                            return i.prototype[e].apply(this, t)
                        };
                    return function () {
                        var e, i = this._super,
                            o = this._superApply;
                        return this._super = t, this._superApply = s, e = n.apply(this, arguments), this._super = i, this._superApply = o, e
                    }
                }()) : void (u[e] = n)
            }), a.prototype = t.widget.extend(r, {
                widgetEventPrefix: o ? r.widgetEventPrefix || e : e
            }, u, {
                constructor: a,
                namespace: h,
                widgetName: e,
                widgetFullName: s
            }), o ? (t.each(o._childConstructors, function (e, i) {
                var n = i.prototype;
                t.widget(n.namespace + "." + n.widgetName, a, i._proto)
            }), delete o._childConstructors) : i._childConstructors.push(a), t.widget.bridge(e, a), a
        }, t.widget.extend = function (e) {
            for (var i, n, o = s.call(arguments, 1), a = 0, r = o.length; r > a; a++)
                for (i in o[a]) n = o[a][i], o[a].hasOwnProperty(i) && void 0 !== n && (e[i] = t.isPlainObject(n) ? t.isPlainObject(e[i]) ? t.widget.extend({}, e[i], n) : t.widget.extend({}, n) : n);
            return e
        }, t.widget.bridge = function (e, i) {
            var n = i.prototype.widgetFullName || e;
            t.fn[e] = function (o) {
                var a = "string" == typeof o,
                    r = s.call(arguments, 1),
                    u = this;
                return o = !a && r.length ? t.widget.extend.apply(null, [o].concat(r)) : o, a ? this.each(function () {
                    var i, s = t.data(this, n);
                    return "instance" === o ? (u = s, !1) : s ? t.isFunction(s[o]) && "_" !== o.charAt(0) ? (i = s[o].apply(s, r), i !== s && void 0 !== i ? (u = i && i.jquery ? u.pushStack(i.get()) : i, !1) : void 0) : t.error("no such method '" + o + "' for " + e + " widget instance") : t.error("cannot call methods on " + e + " prior to initialization; attempted to call method '" + o + "'")
                }) : this.each(function () {
                    var e = t.data(this, n);
                    e ? (e.option(o || {}), e._init && e._init()) : t.data(this, n, new i(o, this))
                }), u
            }
        }, t.Widget = function () { }, t.Widget._childConstructors = [], t.Widget.prototype = {
            widgetName: "widget",
            widgetEventPrefix: "",
            defaultElement: "<div>",
            options: {
                disabled: !1,
                create: null
            },
            _createWidget: function (e, i) {
                i = t(i || this.defaultElement || this)[0], this.element = t(i), this.uuid = n++, this.eventNamespace = "." + this.widgetName + this.uuid, this.bindings = t(), this.hoverable = t(), this.focusable = t(), i !== this && (t.data(i, this.widgetFullName, this), this._on(!0, this.element, {
                    remove: function (t) {
                        t.target === i && this.destroy()
                    }
                }), this.document = t(i.style ? i.ownerDocument : i.document || i), this.window = t(this.document[0].defaultView || this.document[0].parentWindow)), this.options = t.widget.extend({}, this.options, this._getCreateOptions(), e), this._create(), this._trigger("create", null, this._getCreateEventData()), this._init()
            },
            _getCreateOptions: t.noop,
            _getCreateEventData: t.noop,
            _create: t.noop,
            _init: t.noop,
            destroy: function () {
                this._destroy(), this.element.unbind(this.eventNamespace).removeData(this.widgetFullName).removeData(t.camelCase(this.widgetFullName)), this.widget().unbind(this.eventNamespace).removeAttr("aria-disabled").removeClass(this.widgetFullName + "-disabled ui-state-disabled"), this.bindings.unbind(this.eventNamespace), this.hoverable.removeClass("ui-state-hover"), this.focusable.removeClass("ui-state-focus")
            },
            _destroy: t.noop,
            widget: function () {
                return this.element
            },
            option: function (e, i) {
                var n, s, o, a = e;
                if (0 === arguments.length) return t.widget.extend({}, this.options);
                if ("string" == typeof e)
                    if (a = {}, n = e.split("."), e = n.shift(), n.length) {
                        for (s = a[e] = t.widget.extend({}, this.options[e]), o = 0; n.length - 1 > o; o++) s[n[o]] = s[n[o]] || {}, s = s[n[o]];
                        if (e = n.pop(), 1 === arguments.length) return void 0 === s[e] ? null : s[e];
                        s[e] = i
                    } else {
                        if (1 === arguments.length) return void 0 === this.options[e] ? null : this.options[e];
                        a[e] = i
                    } return this._setOptions(a), this
            },
            _setOptions: function (t) {
                var e;
                for (e in t) this._setOption(e, t[e]);
                return this
            },
            _setOption: function (t, e) {
                return this.options[t] = e, "disabled" === t && (this.widget().toggleClass(this.widgetFullName + "-disabled", !!e), e && (this.hoverable.removeClass("ui-state-hover"), this.focusable.removeClass("ui-state-focus"))), this
            },
            enable: function () {
                return this._setOptions({
                    disabled: !1
                })
            },
            disable: function () {
                return this._setOptions({
                    disabled: !0
                })
            },
            _on: function (e, i, n) {
                var s, o = this;
                "boolean" != typeof e && (n = i, i = e, e = !1), n ? (i = s = t(i), this.bindings = this.bindings.add(i)) : (n = i, i = this.element, s = this.widget()), t.each(n, function (n, a) {
                    function r() {
                        return e || o.options.disabled !== !0 && !t(this).hasClass("ui-state-disabled") ? ("string" == typeof a ? o[a] : a).apply(o, arguments) : void 0
                    }
                    "string" != typeof a && (r.guid = a.guid = a.guid || r.guid || t.guid++);
                    var u = n.match(/^([\w:-]*)\s*(.*)$/),
                        h = u[1] + o.eventNamespace,
                        l = u[2];
                    l ? s.delegate(l, h, r) : i.bind(h, r)
                })
            },
            _off: function (e, i) {
                i = (i || "").split(" ").join(this.eventNamespace + " ") + this.eventNamespace, e.unbind(i).undelegate(i), this.bindings = t(this.bindings.not(e).get()), this.focusable = t(this.focusable.not(e).get()), this.hoverable = t(this.hoverable.not(e).get())
            },
            _delay: function (t, e) {
                function i() {
                    return ("string" == typeof t ? n[t] : t).apply(n, arguments)
                }
                var n = this;
                return setTimeout(i, e || 0)
            },
            _hoverable: function (e) {
                this.hoverable = this.hoverable.add(e), this._on(e, {
                    mouseenter: function (e) {
                        t(e.currentTarget).addClass("ui-state-hover")
                    },
                    mouseleave: function (e) {
                        t(e.currentTarget).removeClass("ui-state-hover")
                    }
                })
            },
            _focusable: function (e) {
                this.focusable = this.focusable.add(e), this._on(e, {
                    focusin: function (e) {
                        t(e.currentTarget).addClass("ui-state-focus")
                    },
                    focusout: function (e) {
                        t(e.currentTarget).removeClass("ui-state-focus")
                    }
                })
            },
            _trigger: function (e, i, n) {
                var s, o, a = this.options[e];
                if (n = n || {}, i = t.Event(i), i.type = (e === this.widgetEventPrefix ? e : this.widgetEventPrefix + e).toLowerCase(), i.target = this.element[0], o = i.originalEvent)
                    for (s in o) s in i || (i[s] = o[s]);
                return this.element.trigger(i, n), !(t.isFunction(a) && a.apply(this.element[0], [i].concat(n)) === !1 || i.isDefaultPrevented())
            }
        }, t.each({
            show: "fadeIn",
            hide: "fadeOut"
        }, function (e, i) {
            t.Widget.prototype["_" + e] = function (n, s, o) {
                "string" == typeof s && (s = {
                    effect: s
                });
                var a, r = s ? s === !0 || "number" == typeof s ? i : s.effect || i : e;
                s = s || {}, "number" == typeof s && (s = {
                    duration: s
                }), a = !t.isEmptyObject(s), s.complete = o, s.delay && n.delay(s.delay), a && t.effects && t.effects.effect[r] ? n[e](s) : r !== e && n[r] ? n[r](s.duration, s.easing, o) : n.queue(function (i) {
                    t(this)[e](), o && o.call(n[0]), i()
                })
            }
        }), t.widget;
        var o = !1;
        t(document).mouseup(function () {
            o = !1
        }), t.widget("ui.mouse", {
            version: "1.11.2",
            options: {
                cancel: "input,textarea,button,select,option",
                distance: 1,
                delay: 0
            },
            _mouseInit: function () {
                var e = this;
                this.element.bind("mousedown." + this.widgetName, function (t) {
                    return e._mouseDown(t)
                }).bind("click." + this.widgetName, function (i) {
                    return !0 === t.data(i.target, e.widgetName + ".preventClickEvent") ? (t.removeData(i.target, e.widgetName + ".preventClickEvent"), i.stopImmediatePropagation(), !1) : void 0
                }), this.started = !1
            },
            _mouseDestroy: function () {
                this.element.unbind("." + this.widgetName), this._mouseMoveDelegate && this.document.unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate)
            },
            _mouseDown: function (e) {
                if (!o) {
                    this._mouseMoved = !1, this._mouseStarted && this._mouseUp(e), this._mouseDownEvent = e;
                    var i = this,
                        n = 1 === e.which,
                        s = "string" == typeof this.options.cancel && e.target.nodeName ? t(e.target).closest(this.options.cancel).length : !1;
                    return n && !s && this._mouseCapture(e) ? (this.mouseDelayMet = !this.options.delay, this.mouseDelayMet || (this._mouseDelayTimer = setTimeout(function () {
                        i.mouseDelayMet = !0
                    }, this.options.delay)), this._mouseDistanceMet(e) && this._mouseDelayMet(e) && (this._mouseStarted = this._mouseStart(e) !== !1, !this._mouseStarted) ? (e.preventDefault(), !0) : (!0 === t.data(e.target, this.widgetName + ".preventClickEvent") && t.removeData(e.target, this.widgetName + ".preventClickEvent"), this._mouseMoveDelegate = function (t) {
                        return i._mouseMove(t)
                    }, this._mouseUpDelegate = function (t) {
                        return i._mouseUp(t)
                    }, this.document.bind("mousemove." + this.widgetName, this._mouseMoveDelegate).bind("mouseup." + this.widgetName, this._mouseUpDelegate), e.preventDefault(), o = !0, !0)) : !0
                }
            },
            _mouseMove: function (e) {
                if (this._mouseMoved) {
                    if (t.ui.ie && (!document.documentMode || 9 > document.documentMode) && !e.button) return this._mouseUp(e);
                    if (!e.which) return this._mouseUp(e)
                }
                return (e.which || e.button) && (this._mouseMoved = !0), this._mouseStarted ? (this._mouseDrag(e), e.preventDefault()) : (this._mouseDistanceMet(e) && this._mouseDelayMet(e) && (this._mouseStarted = this._mouseStart(this._mouseDownEvent, e) !== !1, this._mouseStarted ? this._mouseDrag(e) : this._mouseUp(e)), !this._mouseStarted)
            },
            _mouseUp: function (e) {
                return this.document.unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate), this._mouseStarted && (this._mouseStarted = !1, e.target === this._mouseDownEvent.target && t.data(e.target, this.widgetName + ".preventClickEvent", !0), this._mouseStop(e)), o = !1, !1
            },
            _mouseDistanceMet: function (t) {
                return Math.max(Math.abs(this._mouseDownEvent.pageX - t.pageX), Math.abs(this._mouseDownEvent.pageY - t.pageY)) >= this.options.distance
            },
            _mouseDelayMet: function () {
                return this.mouseDelayMet
            },
            _mouseStart: function () { },
            _mouseDrag: function () { },
            _mouseStop: function () { },
            _mouseCapture: function () {
                return !0
            }
        }),
            function () {
                function e(t, e, i) {
                    return [parseFloat(t[0]) * (f.test(t[0]) ? e / 100 : 1), parseFloat(t[1]) * (f.test(t[1]) ? i / 100 : 1)]
                }

                function i(e, i) {
                    return parseInt(t.css(e, i), 10) || 0
                }

                function n(e) {
                    var i = e[0];
                    return 9 === i.nodeType ? {
                        width: e.width(),
                        height: e.height(),
                        offset: {
                            top: 0,
                            left: 0
                        }
                    } : t.isWindow(i) ? {
                        width: e.width(),
                        height: e.height(),
                        offset: {
                            top: e.scrollTop(),
                            left: e.scrollLeft()
                        }
                    } : i.preventDefault ? {
                        width: 0,
                        height: 0,
                        offset: {
                            top: i.pageY,
                            left: i.pageX
                        }
                    } : {
                        width: e.outerWidth(),
                        height: e.outerHeight(),
                        offset: e.offset()
                    }
                }
                t.ui = t.ui || {};
                var s, o, a = Math.max,
                    r = Math.abs,
                    u = Math.round,
                    h = /left|center|right/,
                    l = /top|center|bottom/,
                    c = /[\+\-]\d+(\.[\d]+)?%?/,
                    d = /^\w+/,
                    f = /%$/,
                    p = t.fn.position;
                t.position = {
                    scrollbarWidth: function () {
                        if (void 0 !== s) return s;
                        var e, i, n = t("<div style='display:block;position:absolute;width:50px;height:50px;overflow:hidden;'><div style='height:100px;width:auto;'></div></div>"),
                            o = n.children()[0];
                        return t("body").append(n), e = o.offsetWidth, n.css("overflow", "scroll"), i = o.offsetWidth, e === i && (i = n[0].clientWidth), n.remove(), s = e - i
                    },
                    getScrollInfo: function (e) {
                        var i = e.isWindow || e.isDocument ? "" : e.element.css("overflow-x"),
                            n = e.isWindow || e.isDocument ? "" : e.element.css("overflow-y"),
                            s = "scroll" === i || "auto" === i && e.width < e.element[0].scrollWidth,
                            o = "scroll" === n || "auto" === n && e.height < e.element[0].scrollHeight;
                        return {
                            width: o ? t.position.scrollbarWidth() : 0,
                            height: s ? t.position.scrollbarWidth() : 0
                        }
                    },
                    getWithinInfo: function (e) {
                        var i = t(e || window),
                            n = t.isWindow(i[0]),
                            s = !!i[0] && 9 === i[0].nodeType;
                        return {
                            element: i,
                            isWindow: n,
                            isDocument: s,
                            offset: i.offset() || {
                                left: 0,
                                top: 0
                            },
                            scrollLeft: i.scrollLeft(),
                            scrollTop: i.scrollTop(),
                            width: n || s ? i.width() : i.outerWidth(),
                            height: n || s ? i.height() : i.outerHeight()
                        }
                    }
                }, t.fn.position = function (s) {
                    if (!s || !s.of) return p.apply(this, arguments);
                    s = t.extend({}, s);
                    var f, m, g, v, y, w, b = t(s.of),
                        x = t.position.getWithinInfo(s.within),
                        _ = t.position.getScrollInfo(x),
                        C = (s.collision || "flip").split(" "),
                        k = {};
                    return w = n(b), b[0].preventDefault && (s.at = "left top"), m = w.width, g = w.height, v = w.offset, y = t.extend({}, v), t.each(["my", "at"], function () {
                        var t, e, i = (s[this] || "").split(" ");
                        1 === i.length && (i = h.test(i[0]) ? i.concat(["center"]) : l.test(i[0]) ? ["center"].concat(i) : ["center", "center"]), i[0] = h.test(i[0]) ? i[0] : "center", i[1] = l.test(i[1]) ? i[1] : "center", t = c.exec(i[0]), e = c.exec(i[1]), k[this] = [t ? t[0] : 0, e ? e[0] : 0], s[this] = [d.exec(i[0])[0], d.exec(i[1])[0]]
                    }), 1 === C.length && (C[1] = C[0]), "right" === s.at[0] ? y.left += m : "center" === s.at[0] && (y.left += m / 2), "bottom" === s.at[1] ? y.top += g : "center" === s.at[1] && (y.top += g / 2), f = e(k.at, m, g), y.left += f[0], y.top += f[1], this.each(function () {
                        var n, h, l = t(this),
                            c = l.outerWidth(),
                            d = l.outerHeight(),
                            p = i(this, "marginLeft"),
                            w = i(this, "marginTop"),
                            S = c + p + i(this, "marginRight") + _.width,
                            T = d + w + i(this, "marginBottom") + _.height,
                            W = t.extend({}, y),
                            E = e(k.my, l.outerWidth(), l.outerHeight());
                        "right" === s.my[0] ? W.left -= c : "center" === s.my[0] && (W.left -= c / 2), "bottom" === s.my[1] ? W.top -= d : "center" === s.my[1] && (W.top -= d / 2), W.left += E[0], W.top += E[1], o || (W.left = u(W.left), W.top = u(W.top)), n = {
                            marginLeft: p,
                            marginTop: w
                        }, t.each(["left", "top"], function (e, i) {
                            t.ui.position[C[e]] && t.ui.position[C[e]][i](W, {
                                targetWidth: m,
                                targetHeight: g,
                                elemWidth: c,
                                elemHeight: d,
                                collisionPosition: n,
                                collisionWidth: S,
                                collisionHeight: T,
                                offset: [f[0] + E[0], f[1] + E[1]],
                                my: s.my,
                                at: s.at,
                                within: x,
                                elem: l
                            })
                        }), s.using && (h = function (t) {
                            var e = v.left - W.left,
                                i = e + m - c,
                                n = v.top - W.top,
                                o = n + g - d,
                                u = {
                                    target: {
                                        element: b,
                                        left: v.left,
                                        top: v.top,
                                        width: m,
                                        height: g
                                    },
                                    element: {
                                        element: l,
                                        left: W.left,
                                        top: W.top,
                                        width: c,
                                        height: d
                                    },
                                    horizontal: 0 > i ? "left" : e > 0 ? "right" : "center",
                                    vertical: 0 > o ? "top" : n > 0 ? "bottom" : "middle"
                                };
                            c > m && m > r(e + i) && (u.horizontal = "center"), d > g && g > r(n + o) && (u.vertical = "middle"), u.important = a(r(e), r(i)) > a(r(n), r(o)) ? "horizontal" : "vertical", s.using.call(this, t, u)
                        }), l.offset(t.extend(W, {
                            using: h
                        }))
                    })
                }, t.ui.position = {
                    fit: {
                        left: function (t, e) {
                            var i, n = e.within,
                                s = n.isWindow ? n.scrollLeft : n.offset.left,
                                o = n.width,
                                r = t.left - e.collisionPosition.marginLeft,
                                u = s - r,
                                h = r + e.collisionWidth - o - s;
                            e.collisionWidth > o ? u > 0 && 0 >= h ? (i = t.left + u + e.collisionWidth - o - s, t.left += u - i) : t.left = h > 0 && 0 >= u ? s : u > h ? s + o - e.collisionWidth : s : u > 0 ? t.left += u : h > 0 ? t.left -= h : t.left = a(t.left - r, t.left)
                        },
                        top: function (t, e) {
                            var i, n = e.within,
                                s = n.isWindow ? n.scrollTop : n.offset.top,
                                o = e.within.height,
                                r = t.top - e.collisionPosition.marginTop,
                                u = s - r,
                                h = r + e.collisionHeight - o - s;
                            e.collisionHeight > o ? u > 0 && 0 >= h ? (i = t.top + u + e.collisionHeight - o - s, t.top += u - i) : t.top = h > 0 && 0 >= u ? s : u > h ? s + o - e.collisionHeight : s : u > 0 ? t.top += u : h > 0 ? t.top -= h : t.top = a(t.top - r, t.top)
                        }
                    },
                    flip: {
                        left: function (t, e) {
                            var i, n, s = e.within,
                                o = s.offset.left + s.scrollLeft,
                                a = s.width,
                                u = s.isWindow ? s.scrollLeft : s.offset.left,
                                h = t.left - e.collisionPosition.marginLeft,
                                l = h - u,
                                c = h + e.collisionWidth - a - u,
                                d = "left" === e.my[0] ? -e.elemWidth : "right" === e.my[0] ? e.elemWidth : 0,
                                f = "left" === e.at[0] ? e.targetWidth : "right" === e.at[0] ? -e.targetWidth : 0,
                                p = -2 * e.offset[0];
                            0 > l ? (i = t.left + d + f + p + e.collisionWidth - a - o, (0 > i || r(l) > i) && (t.left += d + f + p)) : c > 0 && (n = t.left - e.collisionPosition.marginLeft + d + f + p - u, (n > 0 || c > r(n)) && (t.left += d + f + p))
                        },
                        top: function (t, e) {
                            var i, n, s = e.within,
                                o = s.offset.top + s.scrollTop,
                                a = s.height,
                                u = s.isWindow ? s.scrollTop : s.offset.top,
                                h = t.top - e.collisionPosition.marginTop,
                                l = h - u,
                                c = h + e.collisionHeight - a - u,
                                d = "top" === e.my[1],
                                f = d ? -e.elemHeight : "bottom" === e.my[1] ? e.elemHeight : 0,
                                p = "top" === e.at[1] ? e.targetHeight : "bottom" === e.at[1] ? -e.targetHeight : 0,
                                m = -2 * e.offset[1];
                            0 > l ? (n = t.top + f + p + m + e.collisionHeight - a - o, t.top + f + p + m > l && (0 > n || r(l) > n) && (t.top += f + p + m)) : c > 0 && (i = t.top - e.collisionPosition.marginTop + f + p + m - u, t.top + f + p + m > c && (i > 0 || c > r(i)) && (t.top += f + p + m))
                        }
                    },
                    flipfit: {
                        left: function () {
                            t.ui.position.flip.left.apply(this, arguments), t.ui.position.fit.left.apply(this, arguments)
                        },
                        top: function () {
                            t.ui.position.flip.top.apply(this, arguments), t.ui.position.fit.top.apply(this, arguments)
                        }
                    }
                },
                    function () {
                        var e, i, n, s, a, r = document.getElementsByTagName("body")[0],
                            u = document.createElement("div");
                        e = document.createElement(r ? "div" : "body"), n = {
                            visibility: "hidden",
                            width: 0,
                            height: 0,
                            border: 0,
                            margin: 0,
                            background: "none"
                        }, r && t.extend(n, {
                            position: "absolute",
                            left: "-1000px",
                            top: "-1000px"
                        });
                        for (a in n) e.style[a] = n[a];
                        e.appendChild(u), i = r || document.documentElement, i.insertBefore(e, i.firstChild), u.style.cssText = "position: absolute; left: 10.7432222px;", s = t(u).offset().left, o = s > 10 && 11 > s, e.innerHTML = "", i.removeChild(e)
                    }()
            }(), t.ui.position, t.widget("ui.menu", {
                version: "1.11.2",
                defaultElement: "<ul>",
                delay: 300,
                options: {
                    icons: {
                        submenu: "ui-icon-carat-1-e"
                    },
                    items: "> *",
                    menus: "ul",
                    position: {
                        my: "left-1 top",
                        at: "right top"
                    },
                    role: "menu",
                    blur: null,
                    focus: null,
                    select: null
                },
                _create: function () {
                    this.activeMenu = this.element, this.mouseHandled = !1, this.element.uniqueId().addClass("ui-menu ui-widget ui-widget-content").toggleClass("ui-menu-icons", !!this.element.find(".ui-icon").length).attr({
                        role: this.options.role,
                        tabIndex: 0
                    }), this.options.disabled && this.element.addClass("ui-state-disabled").attr("aria-disabled", "true"), this._on({
                        "mousedown .ui-menu-item": function (t) {
                            t.preventDefault()
                        },
                        "click .ui-menu-item": function (e) {
                            var i = t(e.target);
                            !this.mouseHandled && i.not(".ui-state-disabled").length && (this.select(e), e.isPropagationStopped() || (this.mouseHandled = !0), i.has(".ui-menu").length ? this.expand(e) : !this.element.is(":focus") && t(this.document[0].activeElement).closest(".ui-menu").length && (this.element.trigger("focus", [!0]), this.active && 1 === this.active.parents(".ui-menu").length && clearTimeout(this.timer)))
                        },
                        "mouseenter .ui-menu-item": function (e) {
                            if (!this.previousFilter) {
                                var i = t(e.currentTarget);
                                i.siblings(".ui-state-active").removeClass("ui-state-active"), this.focus(e, i)
                            }
                        },
                        mouseleave: "collapseAll",
                        "mouseleave .ui-menu": "collapseAll",
                        focus: function (t, e) {
                            var i = this.active || this.element.find(this.options.items).eq(0);
                            e || this.focus(t, i)
                        },
                        blur: function (e) {
                            this._delay(function () {
                                t.contains(this.element[0], this.document[0].activeElement) || this.collapseAll(e)
                            })
                        },
                        keydown: "_keydown"
                    }), this.refresh(), this._on(this.document, {
                        click: function (t) {
                            this._closeOnDocumentClick(t) && this.collapseAll(t), this.mouseHandled = !1
                        }
                    })
                },
                _destroy: function () {
                    this.element.removeAttr("aria-activedescendant").find(".ui-menu").addBack().removeClass("ui-menu ui-widget ui-widget-content ui-menu-icons ui-front").removeAttr("role").removeAttr("tabIndex").removeAttr("aria-labelledby").removeAttr("aria-expanded").removeAttr("aria-hidden").removeAttr("aria-disabled").removeUniqueId().show(), this.element.find(".ui-menu-item").removeClass("ui-menu-item").removeAttr("role").removeAttr("aria-disabled").removeUniqueId().removeClass("ui-state-hover").removeAttr("tabIndex").removeAttr("role").removeAttr("aria-haspopup").children().each(function () {
                        var e = t(this);
                        e.data("ui-menu-submenu-carat") && e.remove()
                    }), this.element.find(".ui-menu-divider").removeClass("ui-menu-divider ui-widget-content")
                },
                _keydown: function (e) {
                    var i, n, s, o, a = !0;
                    switch (e.keyCode) {
                        case t.ui.keyCode.PAGE_UP:
                            this.previousPage(e);
                            break;
                        case t.ui.keyCode.PAGE_DOWN:
                            this.nextPage(e);
                            break;
                        case t.ui.keyCode.HOME:
                            this._move("first", "first", e);
                            break;
                        case t.ui.keyCode.END:
                            this._move("last", "last", e);
                            break;
                        case t.ui.keyCode.UP:
                            this.previous(e);
                            break;
                        case t.ui.keyCode.DOWN:
                            this.next(e);
                            break;
                        case t.ui.keyCode.LEFT:
                            this.collapse(e);
                            break;
                        case t.ui.keyCode.RIGHT:
                            this.active && !this.active.is(".ui-state-disabled") && this.expand(e);
                            break;
                        case t.ui.keyCode.ENTER:
                        case t.ui.keyCode.SPACE:
                            this._activate(e);
                            break;
                        case t.ui.keyCode.ESCAPE:
                            this.collapse(e);
                            break;
                        default:
                            a = !1, n = this.previousFilter || "", s = String.fromCharCode(e.keyCode), o = !1, clearTimeout(this.filterTimer), s === n ? o = !0 : s = n + s, i = this._filterMenuItems(s), i = o && -1 !== i.index(this.active.next()) ? this.active.nextAll(".ui-menu-item") : i, i.length || (s = String.fromCharCode(e.keyCode), i = this._filterMenuItems(s)), i.length ? (this.focus(e, i), this.previousFilter = s, this.filterTimer = this._delay(function () {
                                delete this.previousFilter
                            }, 1e3)) : delete this.previousFilter
                    }
                    a && e.preventDefault()
                },
                _activate: function (t) {
                    this.active.is(".ui-state-disabled") || (this.active.is("[aria-haspopup='true']") ? this.expand(t) : this.select(t))
                },
                refresh: function () {
                    var e, i, n = this,
                        s = this.options.icons.submenu,
                        o = this.element.find(this.options.menus);
                    this.element.toggleClass("ui-menu-icons", !!this.element.find(".ui-icon").length), o.filter(":not(.ui-menu)").addClass("ui-menu ui-widget ui-widget-content ui-front").hide().attr({
                        role: this.options.role,
                        "aria-hidden": "true",
                        "aria-expanded": "false"
                    }).each(function () {
                        var e = t(this),
                            i = e.parent(),
                            n = t("<span>").addClass("ui-menu-icon ui-icon " + s).data("ui-menu-submenu-carat", !0);
                        i.attr("aria-haspopup", "true").prepend(n), e.attr("aria-labelledby", i.attr("id"))
                    }), e = o.add(this.element), i = e.find(this.options.items), i.not(".ui-menu-item").each(function () {
                        var e = t(this);
                        n._isDivider(e) && e.addClass("ui-widget-content ui-menu-divider")
                    }), i.not(".ui-menu-item, .ui-menu-divider").addClass("ui-menu-item").uniqueId().attr({
                        tabIndex: -1,
                        role: this._itemRole()
                    }), i.filter(".ui-state-disabled").attr("aria-disabled", "true"), this.active && !t.contains(this.element[0], this.active[0]) && this.blur()
                },
                _itemRole: function () {
                    return {
                        menu: "menuitem",
                        listbox: "option"
                    }[this.options.role]
                },
                _setOption: function (t, e) {
                    "icons" === t && this.element.find(".ui-menu-icon").removeClass(this.options.icons.submenu).addClass(e.submenu), "disabled" === t && this.element.toggleClass("ui-state-disabled", !!e).attr("aria-disabled", e), this._super(t, e)
                },
                focus: function (t, e) {
                    var i, n;
                    this.blur(t, t && "focus" === t.type), this._scrollIntoView(e), this.active = e.first(), n = this.active.addClass("ui-state-focus").removeClass("ui-state-active"), this.options.role && this.element.attr("aria-activedescendant", n.attr("id")), this.active.parent().closest(".ui-menu-item").addClass("ui-state-active"), t && "keydown" === t.type ? this._close() : this.timer = this._delay(function () {
                        this._close()
                    }, this.delay), i = e.children(".ui-menu"), i.length && t && /^mouse/.test(t.type) && this._startOpening(i), this.activeMenu = e.parent(), this._trigger("focus", t, {
                        item: e
                    })
                },
                _scrollIntoView: function (e) {
                    var i, n, s, o, a, r;
                    this._hasScroll() && (i = parseFloat(t.css(this.activeMenu[0], "borderTopWidth")) || 0, n = parseFloat(t.css(this.activeMenu[0], "paddingTop")) || 0, s = e.offset().top - this.activeMenu.offset().top - i - n, o = this.activeMenu.scrollTop(), a = this.activeMenu.height(), r = e.outerHeight(), 0 > s ? this.activeMenu.scrollTop(o + s) : s + r > a && this.activeMenu.scrollTop(o + s - a + r))
                },
                blur: function (t, e) {
                    e || clearTimeout(this.timer), this.active && (this.active.removeClass("ui-state-focus"), this.active = null, this._trigger("blur", t, {
                        item: this.active
                    }))
                },
                _startOpening: function (t) {
                    clearTimeout(this.timer), "true" === t.attr("aria-hidden") && (this.timer = this._delay(function () {
                        this._close(), this._open(t)
                    }, this.delay))
                },
                _open: function (e) {
                    var i = t.extend({
                        of: this.active
                    }, this.options.position);
                    clearTimeout(this.timer), this.element.find(".ui-menu").not(e.parents(".ui-menu")).hide().attr("aria-hidden", "true"), e.show().removeAttr("aria-hidden").attr("aria-expanded", "true").position(i)
                },
                collapseAll: function (e, i) {
                    clearTimeout(this.timer), this.timer = this._delay(function () {
                        var n = i ? this.element : t(e && e.target).closest(this.element.find(".ui-menu"));
                        n.length || (n = this.element), this._close(n), this.blur(e), this.activeMenu = n
                    }, this.delay)
                },
                _close: function (t) {
                    t || (t = this.active ? this.active.parent() : this.element), t.find(".ui-menu").hide().attr("aria-hidden", "true").attr("aria-expanded", "false").end().find(".ui-state-active").not(".ui-state-focus").removeClass("ui-state-active")
                },
                _closeOnDocumentClick: function (e) {
                    return !t(e.target).closest(".ui-menu").length
                },
                _isDivider: function (t) {
                    return !/[^\-\u2014\u2013\s]/.test(t.text())
                },
                collapse: function (t) {
                    var e = this.active && this.active.parent().closest(".ui-menu-item", this.element);
                    e && e.length && (this._close(), this.focus(t, e))
                },
                expand: function (t) {
                    var e = this.active && this.active.children(".ui-menu ").find(this.options.items).first();
                    e && e.length && (this._open(e.parent()), this._delay(function () {
                        this.focus(t, e)
                    }))
                },
                next: function (t) {
                    this._move("next", "first", t)
                },
                previous: function (t) {
                    this._move("prev", "last", t)
                },
                isFirstItem: function () {
                    return this.active && !this.active.prevAll(".ui-menu-item").length
                },
                isLastItem: function () {
                    return this.active && !this.active.nextAll(".ui-menu-item").length
                },
                _move: function (t, e, i) {
                    var n;
                    this.active && (n = "first" === t || "last" === t ? this.active["first" === t ? "prevAll" : "nextAll"](".ui-menu-item").eq(-1) : this.active[t + "All"](".ui-menu-item").eq(0)), n && n.length && this.active || (n = this.activeMenu.find(this.options.items)[e]()), this.focus(i, n)
                },
                nextPage: function (e) {
                    var i, n, s;
                    return this.active ? void (this.isLastItem() || (this._hasScroll() ? (n = this.active.offset().top, s = this.element.height(), this.active.nextAll(".ui-menu-item").each(function () {
                        return i = t(this), 0 > i.offset().top - n - s
                    }), this.focus(e, i)) : this.focus(e, this.activeMenu.find(this.options.items)[this.active ? "last" : "first"]()))) : void this.next(e)
                },
                previousPage: function (e) {
                    var i, n, s;
                    return this.active ? void (this.isFirstItem() || (this._hasScroll() ? (n = this.active.offset().top, s = this.element.height(), this.active.prevAll(".ui-menu-item").each(function () {
                        return i = t(this), i.offset().top - n + s > 0
                    }), this.focus(e, i)) : this.focus(e, this.activeMenu.find(this.options.items).first()))) : void this.next(e)
                },
                _hasScroll: function () {
                    return this.element.outerHeight() < this.element.prop("scrollHeight")
                },
                select: function (e) {
                    this.active = this.active || t(e.target).closest(".ui-menu-item");
                    var i = {
                        item: this.active
                    };
                    this.active.has(".ui-menu").length || this.collapseAll(e, !0), this._trigger("select", e, i)
                },
                _filterMenuItems: function (e) {
                    var i = e.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, "\\$&"),
                        n = RegExp("^" + i, "i");
                    return this.activeMenu.find(this.options.items).filter(".ui-menu-item").filter(function () {
                        return n.test(t.trim(t(this).text()))
                    })
                }
            }), t.widget("ui.autocomplete", {
                version: "1.11.2",
                defaultElement: "<input>",
                options: {
                    appendTo: null,
                    autoFocus: !1,
                    delay: 300,
                    minLength: 1,
                    position: {
                        my: "left top",
                        at: "left bottom",
                        collision: "none"
                    },
                    source: null,
                    change: null,
                    close: null,
                    focus: null,
                    open: null,
                    response: null,
                    search: null,
                    select: null
                },
                requestIndex: 0,
                pending: 0,
                _create: function () {
                    var e, i, n, s = this.element[0].nodeName.toLowerCase(),
                        o = "textarea" === s,
                        a = "input" === s;
                    this.isMultiLine = o ? !0 : a ? !1 : this.element.prop("isContentEditable"), this.valueMethod = this.element[o || a ? "val" : "text"], this.isNewMenu = !0, this.element.addClass("ui-autocomplete-input").attr("autocomplete", "off"), this._on(this.element, {
                        keydown: function (s) {
                            if (this.element.prop("readOnly")) return e = !0, n = !0, void (i = !0);
                            e = !1, n = !1, i = !1;
                            var o = t.ui.keyCode;
                            switch (s.keyCode) {
                                case o.PAGE_UP:
                                    e = !0, this._move("previousPage", s);
                                    break;
                                case o.PAGE_DOWN:
                                    e = !0, this._move("nextPage", s);
                                    break;
                                case o.UP:
                                    e = !0, this._keyEvent("previous", s);
                                    break;
                                case o.DOWN:
                                    e = !0, this._keyEvent("next", s);
                                    break;
                                case o.ENTER:
                                    this.menu.active && (e = !0, s.preventDefault(), this.menu.select(s));
                                    break;
                                case o.TAB:
                                    this.menu.active && this.menu.select(s);
                                    break;
                                case o.ESCAPE:
                                    this.menu.element.is(":visible") && (this.isMultiLine || this._value(this.term), this.close(s), s.preventDefault());
                                    break;
                                default:
                                    i = !0, this._searchTimeout(s)
                            }
                        },
                        keypress: function (n) {
                            if (e) return e = !1, void ((!this.isMultiLine || this.menu.element.is(":visible")) && n.preventDefault());
                            if (!i) {
                                var s = t.ui.keyCode;
                                switch (n.keyCode) {
                                    case s.PAGE_UP:
                                        this._move("previousPage", n);
                                        break;
                                    case s.PAGE_DOWN:
                                        this._move("nextPage", n);
                                        break;
                                    case s.UP:
                                        this._keyEvent("previous", n);
                                        break;
                                    case s.DOWN:
                                        this._keyEvent("next", n)
                                }
                            }
                        },
                        input: function (t) {
                            return n ? (n = !1, void t.preventDefault()) : void this._searchTimeout(t)
                        },
                        focus: function () {
                            this.selectedItem = null, this.previous = this._value()
                        },
                        blur: function (t) {
                            return this.cancelBlur ? void delete this.cancelBlur : (clearTimeout(this.searching), this.close(t), void this._change(t))
                        }
                    }), this._initSource(), this.menu = t("<ul>").addClass("ui-autocomplete ui-front").appendTo(this._appendTo()).menu({
                        role: null
                    }).hide().menu("instance"), this._on(this.menu.element, {
                        mousedown: function (e) {
                            e.preventDefault(), this.cancelBlur = !0, this._delay(function () {
                                delete this.cancelBlur
                            });
                            var i = this.menu.element[0];
                            t(e.target).closest(".ui-menu-item").length || this._delay(function () {
                                var e = this;
                                this.document.one("mousedown", function (n) {
                                    n.target === e.element[0] || n.target === i || t.contains(i, n.target) || e.close()
                                })
                            })
                        },
                        menufocus: function (e, i) {
                            var n, s;
                            return this.isNewMenu && (this.isNewMenu = !1, e.originalEvent && /^mouse/.test(e.originalEvent.type)) ? (this.menu.blur(), void this.document.one("mousemove", function () {
                                t(e.target).trigger(e.originalEvent)
                            })) : (s = i.item.data("ui-autocomplete-item"), !1 !== this._trigger("focus", e, {
                                item: s
                            }) && e.originalEvent && /^key/.test(e.originalEvent.type) && this._value(s.value), n = i.item.attr("aria-label") || s.value, void (n && t.trim(n).length && (this.liveRegion.children().hide(), t("<div>").text(n).appendTo(this.liveRegion))))
                        },
                        menuselect: function (t, e) {
                            var i = e.item.data("ui-autocomplete-item"),
                                n = this.previous;
                            this.element[0] !== this.document[0].activeElement && (this.element.focus(), this.previous = n, this._delay(function () {
                                this.previous = n, this.selectedItem = i
                            })), !1 !== this._trigger("select", t, {
                                item: i
                            }) && this._value(i.value), this.term = this._value(), this.close(t), this.selectedItem = i
                        }
                    }), this.liveRegion = t("<span>", {
                        role: "status",
                        "aria-live": "assertive",
                        "aria-relevant": "additions"
                    }).addClass("ui-helper-hidden-accessible").appendTo(this.document[0].body), this._on(this.window, {
                        beforeunload: function () {
                            this.element.removeAttr("autocomplete")
                        }
                    })
                },
                _destroy: function () {
                    clearTimeout(this.searching), this.element.removeClass("ui-autocomplete-input").removeAttr("autocomplete"), this.menu.element.remove(), this.liveRegion.remove()
                },
                _setOption: function (t, e) {
                    this._super(t, e), "source" === t && this._initSource(), "appendTo" === t && this.menu.element.appendTo(this._appendTo()), "disabled" === t && e && this.xhr && this.xhr.abort()
                },
                _appendTo: function () {
                    var e = this.options.appendTo;
                    return e && (e = e.jquery || e.nodeType ? t(e) : this.document.find(e).eq(0)), e && e[0] || (e = this.element.closest(".ui-front")), e.length || (e = this.document[0].body), e
                },
                _initSource: function () {
                    var e, i, n = this;
                    t.isArray(this.options.source) ? (e = this.options.source, this.source = function (i, n) {
                        n(t.ui.autocomplete.filter(e, i.term))
                    }) : "string" == typeof this.options.source ? (i = this.options.source, this.source = function (e, s) {
                        n.xhr && n.xhr.abort(), n.xhr = t.ajax({
                            url: i,
                            data: e,
                            dataType: "json",
                            success: function (t) {
                                s(t)
                            },
                            error: function () {
                                s([])
                            }
                        })
                    }) : this.source = this.options.source
                },
                _searchTimeout: function (t) {
                    clearTimeout(this.searching), this.searching = this._delay(function () {
                        var e = this.term === this._value(),
                            i = this.menu.element.is(":visible"),
                            n = t.altKey || t.ctrlKey || t.metaKey || t.shiftKey;
                        (!e || e && !i && !n) && (this.selectedItem = null, this.search(null, t))
                    }, this.options.delay)
                },
                search: function (t, e) {
                    return t = null != t ? t : this._value(), this.term = this._value(), t.length < this.options.minLength ? this.close(e) : this._trigger("search", e) !== !1 ? this._search(t) : void 0
                },
                _search: function (t) {
                    this.pending++, this.element.addClass("ui-autocomplete-loading"), this.cancelSearch = !1, this.source({
                        term: t
                    }, this._response())
                },
                _response: function () {
                    var e = ++this.requestIndex;
                    return t.proxy(function (t) {
                        e === this.requestIndex && this.__response(t), this.pending--, this.pending || this.element.removeClass("ui-autocomplete-loading")
                    }, this)
                },
                __response: function (t) {
                    t && (t = this._normalize(t)), this._trigger("response", null, {
                        content: t
                    }), !this.options.disabled && t && t.length && !this.cancelSearch ? (this._suggest(t), this._trigger("open")) : this._close()
                },
                close: function (t) {
                    this.cancelSearch = !0, this._close(t)
                },
                _close: function (t) {
                    this.menu.element.is(":visible") && (this.menu.element.hide(), this.menu.blur(), this.isNewMenu = !0, this._trigger("close", t))
                },
                _change: function (t) {
                    this.previous !== this._value() && this._trigger("change", t, {
                        item: this.selectedItem
                    })
                },
                _normalize: function (e) {
                    return e.length && e[0].label && e[0].value ? e : t.map(e, function (e) {
                        return "string" == typeof e ? {
                            label: e,
                            value: e
                        } : t.extend({}, e, {
                            label: e.label || e.value,
                            value: e.value || e.label
                        })
                    })
                },
                _suggest: function (e) {
                    var i = this.menu.element.empty();
                    this._renderMenu(i, e), this.isNewMenu = !0, this.menu.refresh(), i.show(), this._resizeMenu(), i.position(t.extend({
                        of: this.element
                    }, this.options.position)), this.options.autoFocus && this.menu.next()
                },
                _resizeMenu: function () {
                    var t = this.menu.element;
                    t.outerWidth(Math.max(t.width("").outerWidth() + 1, this.element.outerWidth()))
                },
                _renderMenu: function (e, i) {
                    var n = this;
                    t.each(i, function (t, i) {
                        n._renderItemData(e, i)
                    })
                },
                _renderItemData: function (t, e) {
                    return this._renderItem(t, e).data("ui-autocomplete-item", e)
                },
                _renderItem: function (e, i) {
                    return t("<li>").text(i.label).appendTo(e)
                },
                _move: function (t, e) {
                    return this.menu.element.is(":visible") ? this.menu.isFirstItem() && /^previous/.test(t) || this.menu.isLastItem() && /^next/.test(t) ? (this.isMultiLine || this._value(this.term), void this.menu.blur()) : void this.menu[t](e) : void this.search(null, e)
                },
                widget: function () {
                    return this.menu.element
                },
                _value: function () {
                    return this.valueMethod.apply(this.element, arguments)
                },
                _keyEvent: function (t, e) {
                    (!this.isMultiLine || this.menu.element.is(":visible")) && (this._move(t, e), e.preventDefault())
                }
            }), t.extend(t.ui.autocomplete, {
                escapeRegex: function (t) {
                    return t.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, "\\$&")
                },
                filter: function (e, i) {
                    var n = RegExp(t.ui.autocomplete.escapeRegex(i), "i");
                    return t.grep(e, function (t) {
                        return n.test(t.label || t.value || t)
                    })
                }
            }), t.widget("ui.autocomplete", t.ui.autocomplete, {
                options: {
                    messages: {
                        noResults: "No search results.",
                        results: function (t) {
                            return t + (t > 1 ? " results are" : " result is") + " available, use up and down arrow keys to navigate."
                        }
                    }
                },
                __response: function (e) {
                    var i;
                    this._superApply(arguments), this.options.disabled || this.cancelSearch || (i = e && e.length ? this.options.messages.results(e.length) : this.options.messages.noResults, this.liveRegion.children().hide(), t("<div>").text(i).appendTo(this.liveRegion))
                }
            }), t.ui.autocomplete, t.extend(t.ui, {
                datepicker: {
                    version: "1.11.2"
                }
            });
        var a = "ui-effects-",
            r = t;
        t.effects = {
            effect: {}
        },
            function (t, e) {
                function i(t, e, i) {
                    var n = c[e.type] || {};
                    return null == t ? i || !e.def ? null : e.def : (t = n.floor ? ~~t : parseFloat(t), isNaN(t) ? e.def : n.mod ? (t + n.mod) % n.mod : 0 > t ? 0 : t > n.max ? n.max : t)
                }

                function n(i) {
                    var n = h(),
                        s = n._rgba = [];
                    return i = i.toLowerCase(), p(u, function (t, o) {
                        var a, r = o.re.exec(i),
                            u = r && o.parse(r),
                            h = o.space || "rgba";
                        return u ? (a = n[h](u), n[l[h].cache] = a[l[h].cache], s = n._rgba = a._rgba, !1) : e
                    }), s.length ? ("0,0,0,0" === s.join() && t.extend(s, o.transparent), n) : o[i]
                }

                function s(t, e, i) {
                    return i = (i + 1) % 1, 1 > 6 * i ? t + 6 * (e - t) * i : 1 > 2 * i ? e : 2 > 3 * i ? t + 6 * (e - t) * (2 / 3 - i) : t
                }
                var o, a = "backgroundColor borderBottomColor borderLeftColor borderRightColor borderTopColor color columnRuleColor outlineColor textDecorationColor textEmphasisColor",
                    r = /^([\-+])=\s*(\d+\.?\d*)/,
                    u = [{
                        re: /rgba?\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
                        parse: function (t) {
                            return [t[1], t[2], t[3], t[4]]
                        }
                    }, {
                        re: /rgba?\(\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
                        parse: function (t) {
                            return [2.55 * t[1], 2.55 * t[2], 2.55 * t[3], t[4]]
                        }
                    }, {
                        re: /#([a-f0-9]{2})([a-f0-9]{2})([a-f0-9]{2})/,
                        parse: function (t) {
                            return [parseInt(t[1], 16), parseInt(t[2], 16), parseInt(t[3], 16)]
                        }
                    }, {
                        re: /#([a-f0-9])([a-f0-9])([a-f0-9])/,
                        parse: function (t) {
                            return [parseInt(t[1] + t[1], 16), parseInt(t[2] + t[2], 16), parseInt(t[3] + t[3], 16)]
                        }
                    }, {
                        re: /hsla?\(\s*(\d+(?:\.\d+)?)\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
                        space: "hsla",
                        parse: function (t) {
                            return [t[1], t[2] / 100, t[3] / 100, t[4]]
                        }
                    }],
                    h = t.Color = function (e, i, n, s) {
                        return new t.Color.fn.parse(e, i, n, s)
                    },
                    l = {
                        rgba: {
                            props: {
                                red: {
                                    idx: 0,
                                    type: "byte"
                                },
                                green: {
                                    idx: 1,
                                    type: "byte"
                                },
                                blue: {
                                    idx: 2,
                                    type: "byte"
                                }
                            }
                        },
                        hsla: {
                            props: {
                                hue: {
                                    idx: 0,
                                    type: "degrees"
                                },
                                saturation: {
                                    idx: 1,
                                    type: "percent"
                                },
                                lightness: {
                                    idx: 2,
                                    type: "percent"
                                }
                            }
                        }
                    },
                    c = {
                        "byte": {
                            floor: !0,
                            max: 255
                        },
                        percent: {
                            max: 1
                        },
                        degrees: {
                            mod: 360,
                            floor: !0
                        }
                    },
                    d = h.support = {},
                    f = t("<p>")[0],
                    p = t.each;
                f.style.cssText = "background-color:rgba(1,1,1,.5)", d.rgba = f.style.backgroundColor.indexOf("rgba") > -1, p(l, function (t, e) {
                    e.cache = "_" + t, e.props.alpha = {
                        idx: 3,
                        type: "percent",
                        def: 1
                    }
                }), h.fn = t.extend(h.prototype, {
                    parse: function (s, a, r, u) {
                        if (s === e) return this._rgba = [null, null, null, null], this;
                        (s.jquery || s.nodeType) && (s = t(s).css(a), a = e);
                        var c = this,
                            d = t.type(s),
                            f = this._rgba = [];
                        return a !== e && (s = [s, a, r, u], d = "array"), "string" === d ? this.parse(n(s) || o._default) : "array" === d ? (p(l.rgba.props, function (t, e) {
                            f[e.idx] = i(s[e.idx], e)
                        }), this) : "object" === d ? (s instanceof h ? p(l, function (t, e) {
                            s[e.cache] && (c[e.cache] = s[e.cache].slice())
                        }) : p(l, function (e, n) {
                            var o = n.cache;
                            p(n.props, function (t, e) {
                                if (!c[o] && n.to) {
                                    if ("alpha" === t || null == s[t]) return;
                                    c[o] = n.to(c._rgba)
                                }
                                c[o][e.idx] = i(s[t], e, !0)
                            }), c[o] && 0 > t.inArray(null, c[o].slice(0, 3)) && (c[o][3] = 1, n.from && (c._rgba = n.from(c[o])))
                        }), this) : e
                    },
                    is: function (t) {
                        var i = h(t),
                            n = !0,
                            s = this;
                        return p(l, function (t, o) {
                            var a, r = i[o.cache];
                            return r && (a = s[o.cache] || o.to && o.to(s._rgba) || [], p(o.props, function (t, i) {
                                return null != r[i.idx] ? n = r[i.idx] === a[i.idx] : e
                            })), n
                        }), n
                    },
                    _space: function () {
                        var t = [],
                            e = this;
                        return p(l, function (i, n) {
                            e[n.cache] && t.push(i)
                        }), t.pop()
                    },
                    transition: function (t, e) {
                        var n = h(t),
                            s = n._space(),
                            o = l[s],
                            a = 0 === this.alpha() ? h("transparent") : this,
                            r = a[o.cache] || o.to(a._rgba),
                            u = r.slice();
                        return n = n[o.cache], p(o.props, function (t, s) {
                            var o = s.idx,
                                a = r[o],
                                h = n[o],
                                l = c[s.type] || {};
                            null !== h && (null === a ? u[o] = h : (l.mod && (h - a > l.mod / 2 ? a += l.mod : a - h > l.mod / 2 && (a -= l.mod)), u[o] = i((h - a) * e + a, s)))
                        }), this[s](u)
                    },
                    blend: function (e) {
                        if (1 === this._rgba[3]) return this;
                        var i = this._rgba.slice(),
                            n = i.pop(),
                            s = h(e)._rgba;
                        return h(t.map(i, function (t, e) {
                            return (1 - n) * s[e] + n * t
                        }))
                    },
                    toRgbaString: function () {
                        var e = "rgba(",
                            i = t.map(this._rgba, function (t, e) {
                                return null == t ? e > 2 ? 1 : 0 : t
                            });
                        return 1 === i[3] && (i.pop(), e = "rgb("), e + i.join() + ")"
                    },
                    toHslaString: function () {
                        var e = "hsla(",
                            i = t.map(this.hsla(), function (t, e) {
                                return null == t && (t = e > 2 ? 1 : 0), e && 3 > e && (t = Math.round(100 * t) + "%"), t
                            });
                        return 1 === i[3] && (i.pop(), e = "hsl("), e + i.join() + ")"
                    },
                    toHexString: function (e) {
                        var i = this._rgba.slice(),
                            n = i.pop();
                        return e && i.push(~~(255 * n)), "#" + t.map(i, function (t) {
                            return t = (t || 0).toString(16), 1 === t.length ? "0" + t : t
                        }).join("")
                    },
                    toString: function () {
                        return 0 === this._rgba[3] ? "transparent" : this.toRgbaString()
                    }
                }), h.fn.parse.prototype = h.fn, l.hsla.to = function (t) {
                    if (null == t[0] || null == t[1] || null == t[2]) return [null, null, null, t[3]];
                    var e, i, n = t[0] / 255,
                        s = t[1] / 255,
                        o = t[2] / 255,
                        a = t[3],
                        r = Math.max(n, s, o),
                        u = Math.min(n, s, o),
                        h = r - u,
                        l = r + u,
                        c = .5 * l;
                    return e = u === r ? 0 : n === r ? 60 * (s - o) / h + 360 : s === r ? 60 * (o - n) / h + 120 : 60 * (n - s) / h + 240, i = 0 === h ? 0 : .5 >= c ? h / l : h / (2 - l), [Math.round(e) % 360, i, c, null == a ? 1 : a]
                }, l.hsla.from = function (t) {
                    if (null == t[0] || null == t[1] || null == t[2]) return [null, null, null, t[3]];
                    var e = t[0] / 360,
                        i = t[1],
                        n = t[2],
                        o = t[3],
                        a = .5 >= n ? n * (1 + i) : n + i - n * i,
                        r = 2 * n - a;
                    return [Math.round(255 * s(r, a, e + 1 / 3)), Math.round(255 * s(r, a, e)), Math.round(255 * s(r, a, e - 1 / 3)), o]
                }, p(l, function (n, s) {
                    var o = s.props,
                        a = s.cache,
                        u = s.to,
                        l = s.from;
                    h.fn[n] = function (n) {
                        if (u && !this[a] && (this[a] = u(this._rgba)), n === e) return this[a].slice();
                        var s, r = t.type(n),
                            c = "array" === r || "object" === r ? n : arguments,
                            d = this[a].slice();
                        return p(o, function (t, e) {
                            var n = c["object" === r ? t : e.idx];
                            null == n && (n = d[e.idx]), d[e.idx] = i(n, e)
                        }), l ? (s = h(l(d)), s[a] = d, s) : h(d)
                    }, p(o, function (e, i) {
                        h.fn[e] || (h.fn[e] = function (s) {
                            var o, a = t.type(s),
                                u = "alpha" === e ? this._hsla ? "hsla" : "rgba" : n,
                                h = this[u](),
                                l = h[i.idx];
                            return "undefined" === a ? l : ("function" === a && (s = s.call(this, l), a = t.type(s)), null == s && i.empty ? this : ("string" === a && (o = r.exec(s), o && (s = l + parseFloat(o[2]) * ("+" === o[1] ? 1 : -1))), h[i.idx] = s, this[u](h)))
                        })
                    })
                }), h.hook = function (e) {
                    var i = e.split(" ");
                    p(i, function (e, i) {
                        t.cssHooks[i] = {
                            set: function (e, s) {
                                var o, a, r = "";
                                if ("transparent" !== s && ("string" !== t.type(s) || (o = n(s)))) {
                                    if (s = h(o || s), !d.rgba && 1 !== s._rgba[3]) {
                                        for (a = "backgroundColor" === i ? e.parentNode : e;
                                            ("" === r || "transparent" === r) && a && a.style;) try {
                                                r = t.css(a, "backgroundColor"), a = a.parentNode
                                            } catch (u) { }
                                        s = s.blend(r && "transparent" !== r ? r : "_default")
                                    }
                                    s = s.toRgbaString()
                                }
                                try {
                                    e.style[i] = s
                                } catch (u) { }
                            }
                        }, t.fx.step[i] = function (e) {
                            e.colorInit || (e.start = h(e.elem, i), e.end = h(e.end), e.colorInit = !0), t.cssHooks[i].set(e.elem, e.start.transition(e.end, e.pos))
                        }
                    })
                }, h.hook(a), t.cssHooks.borderColor = {
                    expand: function (t) {
                        var e = {};
                        return p(["Top", "Right", "Bottom", "Left"], function (i, n) {
                            e["border" + n + "Color"] = t
                        }), e
                    }
                }, o = t.Color.names = {
                    aqua: "#00ffff",
                    black: "#000000",
                    blue: "#0000ff",
                    fuchsia: "#ff00ff",
                    gray: "#808080",
                    green: "#008000",
                    lime: "#00ff00",
                    maroon: "#800000",
                    navy: "#000080",
                    olive: "#808000",
                    purple: "#800080",
                    red: "#ff0000",
                    silver: "#c0c0c0",
                    teal: "#008080",
                    white: "#ffffff",
                    yellow: "#ffff00",
                    transparent: [null, null, null, 0],
                    _default: "#ffffff"
                }
            }(r),
            function () {
                function e(e) {
                    var i, n, s = e.ownerDocument.defaultView ? e.ownerDocument.defaultView.getComputedStyle(e, null) : e.currentStyle,
                        o = {};
                    if (s && s.length && s[0] && s[s[0]])
                        for (n = s.length; n--;) i = s[n], "string" == typeof s[i] && (o[t.camelCase(i)] = s[i]);
                    else
                        for (i in s) "string" == typeof s[i] && (o[i] = s[i]);
                    return o
                }

                function i(e, i) {
                    var n, o, a = {};
                    for (n in i) o = i[n], e[n] !== o && (s[n] || (t.fx.step[n] || !isNaN(parseFloat(o))) && (a[n] = o));
                    return a
                }
                var n = ["add", "remove", "toggle"],
                    s = {
                        border: 1,
                        borderBottom: 1,
                        borderColor: 1,
                        borderLeft: 1,
                        borderRight: 1,
                        borderTop: 1,
                        borderWidth: 1,
                        margin: 1,
                        padding: 1
                    };
                t.each(["borderLeftStyle", "borderRightStyle", "borderBottomStyle", "borderTopStyle"], function (e, i) {
                    t.fx.step[i] = function (t) {
                        ("none" !== t.end && !t.setAttr || 1 === t.pos && !t.setAttr) && (r.style(t.elem, i, t.end), t.setAttr = !0)
                    }
                }), t.fn.addBack || (t.fn.addBack = function (t) {
                    return this.add(null == t ? this.prevObject : this.prevObject.filter(t))
                }), t.effects.animateClass = function (s, o, a, r) {
                    var u = t.speed(o, a, r);
                    return this.queue(function () {
                        var o, a = t(this),
                            r = a.attr("class") || "",
                            h = u.children ? a.find("*").addBack() : a;
                        h = h.map(function () {
                            var i = t(this);
                            return {
                                el: i,
                                start: e(this)
                            }
                        }), o = function () {
                            t.each(n, function (t, e) {
                                s[e] && a[e + "Class"](s[e])
                            })
                        }, o(), h = h.map(function () {
                            return this.end = e(this.el[0]), this.diff = i(this.start, this.end), this
                        }), a.attr("class", r), h = h.map(function () {
                            var e = this,
                                i = t.Deferred(),
                                n = t.extend({}, u, {
                                    queue: !1,
                                    complete: function () {
                                        i.resolve(e)
                                    }
                                });
                            return this.el.animate(this.diff, n), i.promise()
                        }), t.when.apply(t, h.get()).done(function () {
                            o(), t.each(arguments, function () {
                                var e = this.el;
                                t.each(this.diff, function (t) {
                                    e.css(t, "")
                                })
                            }), u.complete.call(a[0])
                        })
                    })
                }, t.fn.extend({
                    addClass: function (e) {
                        return function (i, n, s, o) {
                            return n ? t.effects.animateClass.call(this, {
                                add: i
                            }, n, s, o) : e.apply(this, arguments)
                        }
                    }(t.fn.addClass),
                    removeClass: function (e) {
                        return function (i, n, s, o) {
                            return arguments.length > 1 ? t.effects.animateClass.call(this, {
                                remove: i
                            }, n, s, o) : e.apply(this, arguments)
                        }
                    }(t.fn.removeClass),
                    toggleClass: function (e) {
                        return function (i, n, s, o, a) {
                            return "boolean" == typeof n || void 0 === n ? s ? t.effects.animateClass.call(this, n ? {
                                add: i
                            } : {
                                remove: i
                            }, s, o, a) : e.apply(this, arguments) : t.effects.animateClass.call(this, {
                                toggle: i
                            }, n, s, o)
                        }
                    }(t.fn.toggleClass),
                    switchClass: function (e, i, n, s, o) {
                        return t.effects.animateClass.call(this, {
                            add: i,
                            remove: e
                        }, n, s, o)
                    }
                })
            }(),
            function () {
                function e(e, i, n, s) {
                    return t.isPlainObject(e) && (i = e, e = e.effect), e = {
                        effect: e
                    }, null == i && (i = {}), t.isFunction(i) && (s = i, n = null, i = {}), ("number" == typeof i || t.fx.speeds[i]) && (s = n, n = i, i = {}), t.isFunction(n) && (s = n, n = null), i && t.extend(e, i), n = n || i.duration, e.duration = t.fx.off ? 0 : "number" == typeof n ? n : n in t.fx.speeds ? t.fx.speeds[n] : t.fx.speeds._default, e.complete = s || i.complete, e
                }

                function i(e) {
                    return !e || "number" == typeof e || t.fx.speeds[e] ? !0 : "string" != typeof e || t.effects.effect[e] ? t.isFunction(e) ? !0 : "object" != typeof e || e.effect ? !1 : !0 : !0
                }
                t.extend(t.effects, {
                    version: "1.11.2",
                    save: function (t, e) {
                        for (var i = 0; e.length > i; i++) null !== e[i] && t.data(a + e[i], t[0].style[e[i]])
                    },
                    restore: function (t, e) {
                        var i, n;
                        for (n = 0; e.length > n; n++) null !== e[n] && (i = t.data(a + e[n]), void 0 === i && (i = ""), t.css(e[n], i))
                    },
                    setMode: function (t, e) {
                        return "toggle" === e && (e = t.is(":hidden") ? "show" : "hide"), e
                    },
                    getBaseline: function (t, e) {
                        var i, n;
                        switch (t[0]) {
                            case "top":
                                i = 0;
                                break;
                            case "middle":
                                i = .5;
                                break;
                            case "bottom":
                                i = 1;
                                break;
                            default:
                                i = t[0] / e.height
                        }
                        switch (t[1]) {
                            case "left":
                                n = 0;
                                break;
                            case "center":
                                n = .5;
                                break;
                            case "right":
                                n = 1;
                                break;
                            default:
                                n = t[1] / e.width
                        }
                        return {
                            x: n,
                            y: i
                        }
                    },
                    createWrapper: function (e) {
                        if (e.parent().is(".ui-effects-wrapper")) return e.parent();
                        var i = {
                            width: e.outerWidth(!0),
                            height: e.outerHeight(!0),
                            "float": e.css("float")
                        },
                            n = t("<div></div>").addClass("ui-effects-wrapper").css({
                                fontSize: "100%",
                                background: "transparent",
                                border: "none",
                                margin: 0,
                                padding: 0
                            }),
                            s = {
                                width: e.width(),
                                height: e.height()
                            },
                            o = document.activeElement;
                        try {
                            o.id
                        } catch (a) {
                            o = document.body
                        }
                        return e.wrap(n), (e[0] === o || t.contains(e[0], o)) && t(o).focus(), n = e.parent(), "static" === e.css("position") ? (n.css({
                            position: "relative"
                        }), e.css({
                            position: "relative"
                        })) : (t.extend(i, {
                            position: e.css("position"),
                            zIndex: e.css("z-index")
                        }), t.each(["top", "left", "bottom", "right"], function (t, n) {
                            i[n] = e.css(n), isNaN(parseInt(i[n], 10)) && (i[n] = "auto")
                        }), e.css({
                            position: "relative",
                            top: 0,
                            left: 0,
                            right: "auto",
                            bottom: "auto"
                        })), e.css(s), n.css(i).show()
                    },
                    removeWrapper: function (e) {
                        var i = document.activeElement;
                        return e.parent().is(".ui-effects-wrapper") && (e.parent().replaceWith(e), (e[0] === i || t.contains(e[0], i)) && t(i).focus()), e
                    },
                    setTransition: function (e, i, n, s) {
                        return s = s || {}, t.each(i, function (t, i) {
                            var o = e.cssUnit(i);
                            o[0] > 0 && (s[i] = o[0] * n + o[1])
                        }), s
                    }
                }), t.fn.extend({
                    effect: function () {
                        function i(e) {
                            function i() {
                                t.isFunction(o) && o.call(s[0]), t.isFunction(e) && e()
                            }
                            var s = t(this),
                                o = n.complete,
                                r = n.mode;
                            (s.is(":hidden") ? "hide" === r : "show" === r) ? (s[r](), i()) : a.call(s[0], n, i)
                        }
                        var n = e.apply(this, arguments),
                            s = n.mode,
                            o = n.queue,
                            a = t.effects.effect[n.effect];
                        return t.fx.off || !a ? s ? this[s](n.duration, n.complete) : this.each(function () {
                            n.complete && n.complete.call(this)
                        }) : o === !1 ? this.each(i) : this.queue(o || "fx", i)
                    },
                    show: function (t) {
                        return function (n) {
                            if (i(n)) return t.apply(this, arguments);
                            var s = e.apply(this, arguments);
                            return s.mode = "show", this.effect.call(this, s)
                        }
                    }(t.fn.show),
                    hide: function (t) {
                        return function (n) {
                            if (i(n)) return t.apply(this, arguments);
                            var s = e.apply(this, arguments);
                            return s.mode = "hide", this.effect.call(this, s)
                        }
                    }(t.fn.hide),
                    toggle: function (t) {
                        return function (n) {
                            if (i(n) || "boolean" == typeof n) return t.apply(this, arguments);
                            var s = e.apply(this, arguments);
                            return s.mode = "toggle", this.effect.call(this, s)
                        }
                    }(t.fn.toggle),
                    cssUnit: function (e) {
                        var i = this.css(e),
                            n = [];
                        return t.each(["em", "px", "%", "pt"], function (t, e) {
                            i.indexOf(e) > 0 && (n = [parseFloat(i), e])
                        }), n
                    }
                })
            }(),
            function () {
                var e = {};
                t.each(["Quad", "Cubic", "Quart", "Quint", "Expo"], function (t, i) {
                    e[i] = function (e) {
                        return Math.pow(e, t + 2)
                    }
                }), t.extend(e, {
                    Sine: function (t) {
                        return 1 - Math.cos(t * Math.PI / 2)
                    },
                    Circ: function (t) {
                        return 1 - Math.sqrt(1 - t * t)
                    },
                    Elastic: function (t) {
                        return 0 === t || 1 === t ? t : -Math.pow(2, 8 * (t - 1)) * Math.sin((80 * (t - 1) - 7.5) * Math.PI / 15)
                    },
                    Back: function (t) {
                        return t * t * (3 * t - 2)
                    },
                    Bounce: function (t) {
                        for (var e, i = 4;
                            ((e = Math.pow(2, --i)) - 1) / 11 > t;);
                        return 1 / Math.pow(4, 3 - i) - 7.5625 * Math.pow((3 * e - 2) / 22 - t, 2)
                    }
                }), t.each(e, function (e, i) {
                    t.easing["easeIn" + e] = i, t.easing["easeOut" + e] = function (t) {
                        return 1 - i(1 - t)
                    }, t.easing["easeInOut" + e] = function (t) {
                        return .5 > t ? i(2 * t) / 2 : 1 - i(-2 * t + 2) / 2
                    }
                })
            }(), t.effects, t.effects.effect.blind = function (e, i) {
                var n, s, o, a = t(this),
                    r = /up|down|vertical/,
                    u = /up|left|vertical|horizontal/,
                    h = ["position", "top", "bottom", "left", "right", "height", "width"],
                    l = t.effects.setMode(a, e.mode || "hide"),
                    c = e.direction || "up",
                    d = r.test(c),
                    f = d ? "height" : "width",
                    p = d ? "top" : "left",
                    m = u.test(c),
                    g = {},
                    v = "show" === l;
                a.parent().is(".ui-effects-wrapper") ? t.effects.save(a.parent(), h) : t.effects.save(a, h), a.show(), n = t.effects.createWrapper(a).css({
                    overflow: "hidden"
                }), s = n[f](), o = parseFloat(n.css(p)) || 0, g[f] = v ? s : 0, m || (a.css(d ? "bottom" : "right", 0).css(d ? "top" : "left", "auto").css({
                    position: "absolute"
                }), g[p] = v ? o : s + o), v && (n.css(f, 0), m || n.css(p, o + s)), n.animate(g, {
                    duration: e.duration,
                    easing: e.easing,
                    queue: !1,
                    complete: function () {
                        "hide" === l && a.hide(), t.effects.restore(a, h), t.effects.removeWrapper(a), i()
                    }
                })
            }, t.effects.effect.bounce = function (e, i) {
                var n, s, o, a = t(this),
                    r = ["position", "top", "bottom", "left", "right", "height", "width"],
                    u = t.effects.setMode(a, e.mode || "effect"),
                    h = "hide" === u,
                    l = "show" === u,
                    c = e.direction || "up",
                    d = e.distance,
                    f = e.times || 5,
                    p = 2 * f + (l || h ? 1 : 0),
                    m = e.duration / p,
                    g = e.easing,
                    v = "up" === c || "down" === c ? "top" : "left",
                    y = "up" === c || "left" === c,
                    w = a.queue(),
                    b = w.length;
                for ((l || h) && r.push("opacity"), t.effects.save(a, r), a.show(), t.effects.createWrapper(a), d || (d = a["top" === v ? "outerHeight" : "outerWidth"]() / 3), l && (o = {
                    opacity: 1
                }, o[v] = 0, a.css("opacity", 0).css(v, y ? 2 * -d : 2 * d).animate(o, m, g)), h && (d /= Math.pow(2, f - 1)), o = {}, o[v] = 0, n = 0; f > n; n++) s = {}, s[v] = (y ? "-=" : "+=") + d, a.animate(s, m, g).animate(o, m, g), d = h ? 2 * d : d / 2;
                h && (s = {
                    opacity: 0
                }, s[v] = (y ? "-=" : "+=") + d, a.animate(s, m, g)), a.queue(function () {
                    h && a.hide(), t.effects.restore(a, r), t.effects.removeWrapper(a), i()
                }), b > 1 && w.splice.apply(w, [1, 0].concat(w.splice(b, p + 1))), a.dequeue()
            }, t.effects.effect.clip = function (e, i) {
                var n, s, o, a = t(this),
                    r = ["position", "top", "bottom", "left", "right", "height", "width"],
                    u = t.effects.setMode(a, e.mode || "hide"),
                    h = "show" === u,
                    l = e.direction || "vertical",
                    c = "vertical" === l,
                    d = c ? "height" : "width",
                    f = c ? "top" : "left",
                    p = {};
                t.effects.save(a, r), a.show(), n = t.effects.createWrapper(a).css({
                    overflow: "hidden"
                }), s = "IMG" === a[0].tagName ? n : a, o = s[d](), h && (s.css(d, 0), s.css(f, o / 2)), p[d] = h ? o : 0, p[f] = h ? 0 : o / 2, s.animate(p, {
                    queue: !1,
                    duration: e.duration,
                    easing: e.easing,
                    complete: function () {
                        h || a.hide(), t.effects.restore(a, r), t.effects.removeWrapper(a), i()
                    }
                })
            }, t.effects.effect.drop = function (e, i) {
                var n, s = t(this),
                    o = ["position", "top", "bottom", "left", "right", "opacity", "height", "width"],
                    a = t.effects.setMode(s, e.mode || "hide"),
                    r = "show" === a,
                    u = e.direction || "left",
                    h = "up" === u || "down" === u ? "top" : "left",
                    l = "up" === u || "left" === u ? "pos" : "neg",
                    c = {
                        opacity: r ? 1 : 0
                    };
                t.effects.save(s, o), s.show(), t.effects.createWrapper(s), n = e.distance || s["top" === h ? "outerHeight" : "outerWidth"](!0) / 2, r && s.css("opacity", 0).css(h, "pos" === l ? -n : n), c[h] = (r ? "pos" === l ? "+=" : "-=" : "pos" === l ? "-=" : "+=") + n, s.animate(c, {
                    queue: !1,
                    duration: e.duration,
                    easing: e.easing,
                    complete: function () {
                        "hide" === a && s.hide(), t.effects.restore(s, o), t.effects.removeWrapper(s), i()
                    }
                })
            }, t.effects.effect.explode = function (e, i) {
                function n() {
                    w.push(this), w.length === c * d && s()
                }

                function s() {
                    f.css({
                        visibility: "visible"
                    }), t(w).remove(), m || f.hide(), i()
                }
                var o, a, r, u, h, l, c = e.pieces ? Math.round(Math.sqrt(e.pieces)) : 3,
                    d = c,
                    f = t(this),
                    p = t.effects.setMode(f, e.mode || "hide"),
                    m = "show" === p,
                    g = f.show().css("visibility", "hidden").offset(),
                    v = Math.ceil(f.outerWidth() / d),
                    y = Math.ceil(f.outerHeight() / c),
                    w = [];
                for (o = 0; c > o; o++)
                    for (u = g.top + o * y, l = o - (c - 1) / 2, a = 0; d > a; a++) r = g.left + a * v, h = a - (d - 1) / 2, f.clone().appendTo("body").wrap("<div></div>").css({
                        position: "absolute",
                        visibility: "visible",
                        left: -a * v,
                        top: -o * y
                    }).parent().addClass("ui-effects-explode").css({
                        position: "absolute",
                        overflow: "hidden",
                        width: v,
                        height: y,
                        left: r + (m ? h * v : 0),
                        top: u + (m ? l * y : 0),
                        opacity: m ? 0 : 1
                    }).animate({
                        left: r + (m ? 0 : h * v),
                        top: u + (m ? 0 : l * y),
                        opacity: m ? 1 : 0
                    }, e.duration || 500, e.easing, n)
            }, t.effects.effect.fade = function (e, i) {
                var n = t(this),
                    s = t.effects.setMode(n, e.mode || "toggle");
                n.animate({
                    opacity: s
                }, {
                    queue: !1,
                    duration: e.duration,
                    easing: e.easing,
                    complete: i
                })
            }, t.effects.effect.fold = function (e, i) {
                var n, s, o = t(this),
                    a = ["position", "top", "bottom", "left", "right", "height", "width"],
                    r = t.effects.setMode(o, e.mode || "hide"),
                    u = "show" === r,
                    h = "hide" === r,
                    l = e.size || 15,
                    c = /([0-9]+)%/.exec(l),
                    d = !!e.horizFirst,
                    f = u !== d,
                    p = f ? ["width", "height"] : ["height", "width"],
                    m = e.duration / 2,
                    g = {},
                    v = {};
                t.effects.save(o, a), o.show(), n = t.effects.createWrapper(o).css({
                    overflow: "hidden"
                }), s = f ? [n.width(), n.height()] : [n.height(), n.width()], c && (l = parseInt(c[1], 10) / 100 * s[h ? 0 : 1]), u && n.css(d ? {
                    height: 0,
                    width: l
                } : {
                    height: l,
                    width: 0
                }), g[p[0]] = u ? s[0] : l, v[p[1]] = u ? s[1] : 0, n.animate(g, m, e.easing).animate(v, m, e.easing, function () {
                    h && o.hide(), t.effects.restore(o, a), t.effects.removeWrapper(o), i()
                })
            }, t.effects.effect.highlight = function (e, i) {
                var n = t(this),
                    s = ["backgroundImage", "backgroundColor", "opacity"],
                    o = t.effects.setMode(n, e.mode || "show"),
                    a = {
                        backgroundColor: n.css("backgroundColor")
                    };
                "hide" === o && (a.opacity = 0), t.effects.save(n, s), n.show().css({
                    backgroundImage: "none",
                    backgroundColor: e.color || "#ffff99"
                }).animate(a, {
                    queue: !1,
                    duration: e.duration,
                    easing: e.easing,
                    complete: function () {
                        "hide" === o && n.hide(), t.effects.restore(n, s), i()
                    }
                })
            }, t.effects.effect.size = function (e, i) {
                var n, s, o, a = t(this),
                    r = ["position", "top", "bottom", "left", "right", "width", "height", "overflow", "opacity"],
                    u = ["position", "top", "bottom", "left", "right", "overflow", "opacity"],
                    h = ["width", "height", "overflow"],
                    l = ["fontSize"],
                    c = ["borderTopWidth", "borderBottomWidth", "paddingTop", "paddingBottom"],
                    d = ["borderLeftWidth", "borderRightWidth", "paddingLeft", "paddingRight"],
                    f = t.effects.setMode(a, e.mode || "effect"),
                    p = e.restore || "effect" !== f,
                    m = e.scale || "both",
                    g = e.origin || ["middle", "center"],
                    v = a.css("position"),
                    y = p ? r : u,
                    w = {
                        height: 0,
                        width: 0,
                        outerHeight: 0,
                        outerWidth: 0
                    };
                "show" === f && a.show(), n = {
                    height: a.height(),
                    width: a.width(),
                    outerHeight: a.outerHeight(),
                    outerWidth: a.outerWidth()
                }, "toggle" === e.mode && "show" === f ? (a.from = e.to || w, a.to = e.from || n) : (a.from = e.from || ("show" === f ? w : n), a.to = e.to || ("hide" === f ? w : n)), o = {
                    from: {
                        y: a.from.height / n.height,
                        x: a.from.width / n.width
                    },
                    to: {
                        y: a.to.height / n.height,
                        x: a.to.width / n.width
                    }
                }, ("box" === m || "both" === m) && (o.from.y !== o.to.y && (y = y.concat(c), a.from = t.effects.setTransition(a, c, o.from.y, a.from), a.to = t.effects.setTransition(a, c, o.to.y, a.to)), o.from.x !== o.to.x && (y = y.concat(d), a.from = t.effects.setTransition(a, d, o.from.x, a.from), a.to = t.effects.setTransition(a, d, o.to.x, a.to))), ("content" === m || "both" === m) && o.from.y !== o.to.y && (y = y.concat(l).concat(h), a.from = t.effects.setTransition(a, l, o.from.y, a.from), a.to = t.effects.setTransition(a, l, o.to.y, a.to)), t.effects.save(a, y), a.show(), t.effects.createWrapper(a), a.css("overflow", "hidden").css(a.from), g && (s = t.effects.getBaseline(g, n), a.from.top = (n.outerHeight - a.outerHeight()) * s.y, a.from.left = (n.outerWidth - a.outerWidth()) * s.x, a.to.top = (n.outerHeight - a.to.outerHeight) * s.y, a.to.left = (n.outerWidth - a.to.outerWidth) * s.x), a.css(a.from), ("content" === m || "both" === m) && (c = c.concat(["marginTop", "marginBottom"]).concat(l), d = d.concat(["marginLeft", "marginRight"]), h = r.concat(c).concat(d), a.find("*[width]").each(function () {
                    var i = t(this),
                        n = {
                            height: i.height(),
                            width: i.width(),
                            outerHeight: i.outerHeight(),
                            outerWidth: i.outerWidth()
                        };
                    p && t.effects.save(i, h), i.from = {
                        height: n.height * o.from.y,
                        width: n.width * o.from.x,
                        outerHeight: n.outerHeight * o.from.y,
                        outerWidth: n.outerWidth * o.from.x
                    }, i.to = {
                        height: n.height * o.to.y,
                        width: n.width * o.to.x,
                        outerHeight: n.height * o.to.y,
                        outerWidth: n.width * o.to.x
                    }, o.from.y !== o.to.y && (i.from = t.effects.setTransition(i, c, o.from.y, i.from), i.to = t.effects.setTransition(i, c, o.to.y, i.to)), o.from.x !== o.to.x && (i.from = t.effects.setTransition(i, d, o.from.x, i.from), i.to = t.effects.setTransition(i, d, o.to.x, i.to)), i.css(i.from), i.animate(i.to, e.duration, e.easing, function () {
                        p && t.effects.restore(i, h)
                    })
                })), a.animate(a.to, {
                    queue: !1,
                    duration: e.duration,
                    easing: e.easing,
                    complete: function () {
                        0 === a.to.opacity && a.css("opacity", a.from.opacity), "hide" === f && a.hide(), t.effects.restore(a, y), p || ("static" === v ? a.css({
                            position: "relative",
                            top: a.to.top,
                            left: a.to.left
                        }) : t.each(["top", "left"], function (t, e) {
                            a.css(e, function (e, i) {
                                var n = parseInt(i, 10),
                                    s = t ? a.to.left : a.to.top;
                                return "auto" === i ? s + "px" : n + s + "px"
                            })
                        })), t.effects.removeWrapper(a), i()
                    }
                })
            }, t.effects.effect.scale = function (e, i) {
                var n = t(this),
                    s = t.extend(!0, {}, e),
                    o = t.effects.setMode(n, e.mode || "effect"),
                    a = parseInt(e.percent, 10) || (0 === parseInt(e.percent, 10) ? 0 : "hide" === o ? 0 : 100),
                    r = e.direction || "both",
                    u = e.origin,
                    h = {
                        height: n.height(),
                        width: n.width(),
                        outerHeight: n.outerHeight(),
                        outerWidth: n.outerWidth()
                    },
                    l = {
                        y: "horizontal" !== r ? a / 100 : 1,
                        x: "vertical" !== r ? a / 100 : 1
                    };
                s.effect = "size", s.queue = !1, s.complete = i, "effect" !== o && (s.origin = u || ["middle", "center"], s.restore = !0), s.from = e.from || ("show" === o ? {
                    height: 0,
                    width: 0,
                    outerHeight: 0,
                    outerWidth: 0
                } : h), s.to = {
                    height: h.height * l.y,
                    width: h.width * l.x,
                    outerHeight: h.outerHeight * l.y,
                    outerWidth: h.outerWidth * l.x
                }, s.fade && ("show" === o && (s.from.opacity = 0, s.to.opacity = 1), "hide" === o && (s.from.opacity = 1, s.to.opacity = 0)), n.effect(s)
            }, t.effects.effect.puff = function (e, i) {
                var n = t(this),
                    s = t.effects.setMode(n, e.mode || "hide"),
                    o = "hide" === s,
                    a = parseInt(e.percent, 10) || 150,
                    r = a / 100,
                    u = {
                        height: n.height(),
                        width: n.width(),
                        outerHeight: n.outerHeight(),
                        outerWidth: n.outerWidth()
                    };
                t.extend(e, {
                    effect: "scale",
                    queue: !1,
                    fade: !0,
                    mode: s,
                    complete: i,
                    percent: o ? a : 100,
                    from: o ? u : {
                        height: u.height * r,
                        width: u.width * r,
                        outerHeight: u.outerHeight * r,
                        outerWidth: u.outerWidth * r
                    }
                }), n.effect(e)
            }, t.effects.effect.pulsate = function (e, i) {
                var n, s = t(this),
                    o = t.effects.setMode(s, e.mode || "show"),
                    a = "show" === o,
                    r = "hide" === o,
                    u = a || "hide" === o,
                    h = 2 * (e.times || 5) + (u ? 1 : 0),
                    l = e.duration / h,
                    c = 0,
                    d = s.queue(),
                    f = d.length;
                for ((a || !s.is(":visible")) && (s.css("opacity", 0).show(), c = 1), n = 1; h > n; n++) s.animate({
                    opacity: c
                }, l, e.easing), c = 1 - c;
                s.animate({
                    opacity: c
                }, l, e.easing), s.queue(function () {
                    r && s.hide(), i()
                }), f > 1 && d.splice.apply(d, [1, 0].concat(d.splice(f, h + 1))), s.dequeue()
            }, t.effects.effect.shake = function (e, i) {
                var n, s = t(this),
                    o = ["position", "top", "bottom", "left", "right", "height", "width"],
                    a = t.effects.setMode(s, e.mode || "effect"),
                    r = e.direction || "left",
                    u = e.distance || 20,
                    h = e.times || 3,
                    l = 2 * h + 1,
                    c = Math.round(e.duration / l),
                    d = "up" === r || "down" === r ? "top" : "left",
                    f = "up" === r || "left" === r,
                    p = {},
                    m = {},
                    g = {},
                    v = s.queue(),
                    y = v.length;
                for (t.effects.save(s, o), s.show(), t.effects.createWrapper(s), p[d] = (f ? "-=" : "+=") + u, m[d] = (f ? "+=" : "-=") + 2 * u, g[d] = (f ? "-=" : "+=") + 2 * u, s.animate(p, c, e.easing), n = 1; h > n; n++) s.animate(m, c, e.easing).animate(g, c, e.easing);
                s.animate(m, c, e.easing).animate(p, c / 2, e.easing).queue(function () {
                    "hide" === a && s.hide(), t.effects.restore(s, o), t.effects.removeWrapper(s), i()
                }), y > 1 && v.splice.apply(v, [1, 0].concat(v.splice(y, l + 1))), s.dequeue()
            }, t.effects.effect.slide = function (e, i) {
                var n, s = t(this),
                    o = ["position", "top", "bottom", "left", "right", "width", "height"],
                    a = t.effects.setMode(s, e.mode || "show"),
                    r = "show" === a,
                    u = e.direction || "left",
                    h = "up" === u || "down" === u ? "top" : "left",
                    l = "up" === u || "left" === u,
                    c = {};
                t.effects.save(s, o), s.show(), n = e.distance || s["top" === h ? "outerHeight" : "outerWidth"](!0), t.effects.createWrapper(s).css({
                    overflow: "hidden"
                }), r && s.css(h, l ? isNaN(n) ? "-" + n : -n : n), c[h] = (r ? l ? "+=" : "-=" : l ? "-=" : "+=") + n, s.animate(c, {
                    queue: !1,
                    duration: e.duration,
                    easing: e.easing,
                    complete: function () {
                        "hide" === a && s.hide(), t.effects.restore(s, o), t.effects.removeWrapper(s), i()
                    }
                })
            }, t.effects.effect.transfer = function (e, i) {
                var n = t(this),
                    s = t(e.to),
                    o = "fixed" === s.css("position"),
                    a = t("body"),
                    r = o ? a.scrollTop() : 0,
                    u = o ? a.scrollLeft() : 0,
                    h = s.offset(),
                    l = {
                        top: h.top - r,
                        left: h.left - u,
                        height: s.innerHeight(),
                        width: s.innerWidth()
                    },
                    c = n.offset(),
                    d = t("<div class='ui-effects-transfer'></div>").appendTo(document.body).addClass(e.className).css({
                        top: c.top - r,
                        left: c.left - u,
                        height: n.innerHeight(),
                        width: n.innerWidth(),
                        position: o ? "fixed" : "absolute"
                    }).animate(l, e.duration, e.easing, function () {
                        d.remove(), i()
                    })
            }
    }), ! function () {
        function t(n) {
            if (!n) throw new Error("No options passed to Waypoint constructor");
            if (!n.element) throw new Error("No element option passed to Waypoint constructor");
            if (!n.handler) throw new Error("No handler option passed to Waypoint constructor");
            this.key = "waypoint-" + e, this.options = t.Adapter.extend({}, t.defaults, n), this.element = this.options.element, this.adapter = new t.Adapter(this.element), this.callback = n.handler, this.axis = this.options.horizontal ? "horizontal" : "vertical", this.enabled = this.options.enabled, this.triggerPoint = null, this.group = t.Group.findOrCreate({
                name: this.options.group,
                axis: this.axis
            }), this.context = t.Context.findOrCreateByElement(this.options.context), t.offsetAliases[this.options.offset] && (this.options.offset = t.offsetAliases[this.options.offset]), this.group.add(this), this.context.add(this), i[this.key] = this, e += 1
        }
        var e = 0,
            i = {};
        t.prototype.queueTrigger = function (t) {
            this.group.queueTrigger(this, t)
        }, t.prototype.trigger = function (t) {
            this.enabled && this.callback && this.callback.apply(this, t)
        }, t.prototype.destroy = function () {
            this.context.remove(this), this.group.remove(this), delete i[this.key]
        }, t.prototype.disable = function () {
            return this.enabled = !1, this
        }, t.prototype.enable = function () {
            return this.context.refresh(), this.enabled = !0, this
        }, t.prototype.next = function () {
            return this.group.next(this)
        }, t.prototype.previous = function () {
            return this.group.previous(this)
        }, t.invokeAll = function (t) {
            var e = [];
            for (var n in i) e.push(i[n]);
            for (var s = 0, o = e.length; o > s; s++) e[s][t]()
        }, t.destroyAll = function () {
            t.invokeAll("destroy")
        }, t.disableAll = function () {
            t.invokeAll("disable")
        }, t.enableAll = function () {
            t.invokeAll("enable")
        }, t.refreshAll = function () {
            t.Context.refreshAll()
        }, t.viewportHeight = function () {
            return window.innerHeight || document.documentElement.clientHeight
        }, t.viewportWidth = function () {
            return document.documentElement.clientWidth
        }, t.adapters = [], t.defaults = {
            context: window,
            continuous: !0,
            enabled: !0,
            group: "default",
            horizontal: !1,
            offset: 0
        }, t.offsetAliases = {
            "bottom-in-view": function () {
                return this.context.innerHeight() - this.adapter.outerHeight()
            },
            "right-in-view": function () {
                return this.context.innerWidth() - this.adapter.outerWidth()
            }
        }, window.Waypoint = t
    }(),
    function () {
        function t(t) {
            window.setTimeout(t, 1e3 / 60)
        }

        function e(t) {
            this.element = t, this.Adapter = s.Adapter, this.adapter = new this.Adapter(t), this.key = "waypoint-context-" + i, this.didScroll = !1, this.didResize = !1, this.oldScroll = {
                x: this.adapter.scrollLeft(),
                y: this.adapter.scrollTop()
            }, this.waypoints = {
                vertical: {},
                horizontal: {}
            }, t.waypointContextKey = this.key, n[t.waypointContextKey] = this, i += 1, this.createThrottledScrollHandler(), this.createThrottledResizeHandler()
        }
        var i = 0,
            n = {},
            s = window.Waypoint,
            o = window.onload;
        e.prototype.add = function (t) {
            var e = t.options.horizontal ? "horizontal" : "vertical";
            this.waypoints[e][t.key] = t, this.refresh()
        }, e.prototype.checkEmpty = function () {
            var t = this.Adapter.isEmptyObject(this.waypoints.horizontal),
                e = this.Adapter.isEmptyObject(this.waypoints.vertical);
            t && e && (this.adapter.off(".waypoints"), delete n[this.key])
        }, e.prototype.createThrottledResizeHandler = function () {
            function t() {
                e.handleResize(), e.didResize = !1
            }
            var e = this;
            this.adapter.on("resize.waypoints", function () {
                e.didResize || (e.didResize = !0, s.requestAnimationFrame(t))
            })
        }, e.prototype.createThrottledScrollHandler = function () {
            function t() {
                e.handleScroll(), e.didScroll = !1
            }
            var e = this;
            this.adapter.on("scroll.waypoints", function () {
                (!e.didScroll || s.isTouch) && (e.didScroll = !0, s.requestAnimationFrame(t))
            })
        }, e.prototype.handleResize = function () {
            s.Context.refreshAll()
        }, e.prototype.handleScroll = function () {
            var t = {},
                e = {
                    horizontal: {
                        newScroll: this.adapter.scrollLeft(),
                        oldScroll: this.oldScroll.x,
                        forward: "right",
                        backward: "left"
                    },
                    vertical: {
                        newScroll: this.adapter.scrollTop(),
                        oldScroll: this.oldScroll.y,
                        forward: "down",
                        backward: "up"
                    }
                };
            for (var i in e) {
                var n = e[i],
                    s = n.newScroll > n.oldScroll,
                    o = s ? n.forward : n.backward;
                for (var a in this.waypoints[i]) {
                    var r = this.waypoints[i][a],
                        u = n.oldScroll < r.triggerPoint,
                        h = n.newScroll >= r.triggerPoint,
                        l = u && h,
                        c = !u && !h;
                    (l || c) && (r.queueTrigger(o), t[r.group.id] = r.group)
                }
            }
            for (var d in t) t[d].flushTriggers();
            this.oldScroll = {
                x: e.horizontal.newScroll,
                y: e.vertical.newScroll
            }
        }, e.prototype.innerHeight = function () {
            return this.element == this.element.window ? s.viewportHeight() : this.adapter.innerHeight()
        }, e.prototype.remove = function (t) {
            delete this.waypoints[t.axis][t.key], this.checkEmpty()
        }, e.prototype.innerWidth = function () {
            return this.element == this.element.window ? s.viewportWidth() : this.adapter.innerWidth()
        }, e.prototype.destroy = function () {
            var t = [];
            for (var e in this.waypoints)
                for (var i in this.waypoints[e]) t.push(this.waypoints[e][i]);
            for (var n = 0, s = t.length; s > n; n++) t[n].destroy()
        }, e.prototype.refresh = function () {
            var t, e = this.element == this.element.window,
                i = this.adapter.offset(),
                n = {};
            this.handleScroll(), t = {
                horizontal: {
                    contextOffset: e ? 0 : i.left,
                    contextScroll: e ? 0 : this.oldScroll.x,
                    contextDimension: this.innerWidth(),
                    oldScroll: this.oldScroll.x,
                    forward: "right",
                    backward: "left",
                    offsetProp: "left"
                },
                vertical: {
                    contextOffset: e ? 0 : i.top,
                    contextScroll: e ? 0 : this.oldScroll.y,
                    contextDimension: this.innerHeight(),
                    oldScroll: this.oldScroll.y,
                    forward: "down",
                    backward: "up",
                    offsetProp: "top"
                }
            };
            for (var s in t) {
                var o = t[s];
                for (var a in this.waypoints[s]) {
                    var r, u, h, l, c, d = this.waypoints[s][a],
                        f = d.options.offset,
                        p = d.triggerPoint,
                        m = 0,
                        g = null == p;
                    d.element !== d.element.window && (m = d.adapter.offset()[o.offsetProp]), "function" == typeof f ? f = f.apply(d) : "string" == typeof f && (f = parseFloat(f), d.options.offset.indexOf("%") > -1 && (f = Math.ceil(o.contextDimension * f / 100))), r = o.contextScroll - o.contextOffset, d.triggerPoint = m + r - f, u = p < o.oldScroll, h = d.triggerPoint >= o.oldScroll, l = u && h, c = !u && !h, !g && l ? (d.queueTrigger(o.backward), n[d.group.id] = d.group) : !g && c ? (d.queueTrigger(o.forward), n[d.group.id] = d.group) : g && o.oldScroll >= d.triggerPoint && (d.queueTrigger(o.forward), n[d.group.id] = d.group)
                }
            }
            for (var v in n) n[v].flushTriggers();
            return this
        }, e.findOrCreateByElement = function (t) {
            return e.findByElement(t) || new e(t)
        }, e.refreshAll = function () {
            for (var t in n) n[t].refresh()
        }, e.findByElement = function (t) {
            return n[t.waypointContextKey]
        }, window.onload = function () {
            o && o(), e.refreshAll()
        }, s.requestAnimationFrame = function (e) {
            var i = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || t;
            i.call(window, e)
        }, s.Context = e
    }(),
    function () {
        function t(t, e) {
            return t.triggerPoint - e.triggerPoint
        }

        function e(t, e) {
            return e.triggerPoint - t.triggerPoint
        }

        function i(t) {
            this.name = t.name, this.axis = t.axis, this.id = this.name + "-" + this.axis, this.waypoints = [], this.clearTriggerQueues(), n[this.axis][this.name] = this
        }
        var n = {
            vertical: {},
            horizontal: {}
        },
            s = window.Waypoint;
        i.prototype.add = function (t) {
            this.waypoints.push(t)
        }, i.prototype.clearTriggerQueues = function () {
            this.triggerQueues = {
                up: [],
                down: [],
                left: [],
                right: []
            }
        }, i.prototype.flushTriggers = function () {
            for (var i in this.triggerQueues) {
                var n = this.triggerQueues[i],
                    s = "up" === i || "left" === i;
                n.sort(s ? e : t);
                for (var o = 0, a = n.length; a > o; o += 1) {
                    var r = n[o];
                    (r.options.continuous || o === n.length - 1) && r.trigger([i])
                }
            }
            this.clearTriggerQueues()
        }, i.prototype.next = function (e) {
            this.waypoints.sort(t);
            var i = s.Adapter.inArray(e, this.waypoints),
                n = i === this.waypoints.length - 1;
            return n ? null : this.waypoints[i + 1]
        }, i.prototype.previous = function (e) {
            this.waypoints.sort(t);
            var i = s.Adapter.inArray(e, this.waypoints);
            return i ? this.waypoints[i - 1] : null
        }, i.prototype.queueTrigger = function (t, e) {
            this.triggerQueues[e].push(t)
        }, i.prototype.remove = function (t) {
            var e = s.Adapter.inArray(t, this.waypoints);
            e > -1 && this.waypoints.splice(e, 1)
        }, i.prototype.first = function () {
            return this.waypoints[0]
        }, i.prototype.last = function () {
            return this.waypoints[this.waypoints.length - 1]
        }, i.findOrCreate = function (t) {
            return n[t.axis][t.name] || new i(t)
        }, s.Group = i
    }(),
    function () {
        function t(t) {
            this.$element = e(t)
        }
        var e = window.jQuery,
            i = window.Waypoint;
        e.each(["innerHeight", "innerWidth", "off", "offset", "on", "outerHeight", "outerWidth", "scrollLeft", "scrollTop"], function (e, i) {
            t.prototype[i] = function () {
                var t = Array.prototype.slice.call(arguments);
                return this.$element[i].apply(this.$element, t)
            }
        }), e.each(["extend", "inArray", "isEmptyObject"], function (i, n) {
            t[n] = e[n]
        }), i.adapters.push({
            name: "jquery",
            Adapter: t
        }), i.Adapter = t
    }(),
    function () {
        function t(t) {
            return function () {
                var i = [],
                    n = arguments[0];
                return t.isFunction(arguments[0]) && (n = t.extend({}, arguments[1]), n.handler = arguments[0]), this.each(function () {
                    var s = t.extend({}, n, {
                        element: this
                    });
                    "string" == typeof s.context && (s.context = t(this).closest(s.context)[0]), i.push(new e(s))
                }), i
            }
        }
        var e = window.Waypoint;
        window.jQuery && (window.jQuery.fn.waypoint = t(window.jQuery)), window.Zepto && (window.Zepto.fn.waypoint = t(window.Zepto))
    }(), jQuery(document).ready(function (t) {
        function e() {
            "block" == t(".u01mmenu").css("display") && t("body").hasClass("f11") ? t(".u01navtools + .u01pfile #pfile-acct, .u01navtools + .u01pfile #pfile-sout, .u01navtools + .u01pfile #pfile-regs, .u01navtools + .u01pfile #pfile-language").detach().insertBefore(".u01navtools").wrapAll('<ul class="u01pfile" style="list-style: none;"></ul>') : t(".u01logo + .u01pfile #pfile-acct, .u01logo + .u01pfile #pfile-sout, .u01logo + .u01pfile #pfile-regs, .u01logo + .u01pfile #pfile-language").unwrap('<ul class="u01pfile" style="list-style: none;"></ul>').detach().insertAfter("#pfile-wlcm")
        }

        function i() {
            try {
                t("div.u01tools").each(function () {
                    t(this).find("ul li > a").css("font-size", "");
                    for (var e = 1 * t(this).find("ul.u01navtools li > a").css("font-size").split("px")[0] - 1; t(this).height() > 66 && e >= 11;) t(this).find("ul li > a").css("font-size", e + "px"), e--
                })
            } catch (e) { }
        }

        function n() {
            t("ul.u01nav").each(function () {
                t(this).find("li h3").css("font-size", "");
                for (var e = 1 * t(this).find("li h3").css("font-size").split("px")[0] - 1; t(this).height() > 66 && e >= 14;) t(this).find("li h3").css("font-size", e + "px"), 14 == e && t(this).height() > 66 && t("a.u01nav h3").css("padding-left", "8px").css("padding-right", "8px"), e--
            })
        }
        document.getElementById("u01") && (t(".f01r1 .f01v0w1").addClass("f01bg4u01"), t("div.u01search").attr("data-lbl", "search"), t("ul.u01nav").after('<div class="u01z3"></div>'), t(".u01logo a span")[0] || t(".u01logoinline .u01logo")[0] || t(".u01logo a").wrapInner("<span></span>"), t("#u01").prepend('<div class="u01mmenu"><a href="#menu">MENU</a></div><div class="u01mlogo"></div><div class="u01msearch"><div class="rightFloat socialMediaMarginMobile"><a href="https://www.starmicronics.com/blog/" title="blog"><div class="rightFloat mSocialMediaIcon"></div></a></div>'), t(".u01logoinline .u01logo img")[0] && (t(".u01mlogo").empty(), t(".u01logoinline .u01logo img").clone().appendTo(".u01mlogo")), t("div.u01tools").wrapAll('<div class="u01toolsw1"></div>'), t("body.f11v1").addClass("u01loaded"), e(), t(window).resize(function () {
            e()
        }), i(), t(window).resize(function () {
            i()
        }), "#727272" == t("#pfile-wlcm").css("color") && t("div.u01tools").css("width", t(".u01navtools").outerWidth() + t(".u01pfile").outerWidth() + 1 * t(".u01navtools").css("margin-right").split("px")[0]), n(), t(window).resize(function () {
            n()
        }), t("ul.u01nav > li").each(function () {
            function e() {
                "none" == t(".u01mmenu").css("display") ? t(this).find("a.u01nav h3").first().height() && t(this).find("a.u01nav h3").first().height() < 20 ? t(this).find("a.u01nav h3").css("height", "32px").css("padding-top", "14px") : t(this).find("a.u01nav h3").first().height() && t(this).find("a.u01nav h3").first().height() < 37 && t(this).find("a.u01nav h3").css("height", "33px").css("padding-top", "8px") : t(this).find("a.u01nav h3").css("height", "").css("padding-top", "")
            }
            t(this).find("a.u01nav,div.u01w7").append('<div class="u01z1"><div class="u01z2"></div></div>'), e(), t(window).resize(function () {
                e()
            }), t(this).find("a.u01nav").bind("mouseover", function (e) {
                if ("none" == t(".u01mmenu").css("display")) {
                    try {
                        var i = window.getComputedStyle(t(this)[0], null).width
                    } catch (n) {
                        var i = t(this).width() + "px"
                    }
                   // t(this).css("width", i), t(this).find("div.u01z1").css("width", i)
                } else t(this).css("width", ""), t(this).find("div.u01z1").css("width", "")
            }), t(window).resize(function () {
                t("a.u01nav").css("width", ""), t("div.u01z1").css("width", "")
            }), t(this).find("div.u01w7").bind("mouseover", function (e) {
                try {
                    var i = window.getComputedStyle(t(this)[0], null).width
                } catch (n) {
                    var i = t(this).width() + "px"
                }
                i = i.replace(/px/, ""), i = 1 * i + 1 + "px", t(this).find("div.u01z1").css("width", i)
            }), t(this).each(function () {
                t(this).parent("ul.u01nav").hasClass("u01disabled") && t(this).hover(function () {
                    t(this).addClass("u01hover")
                }, function () {
                    t(this).removeClass("u01hover")
                }), t(this).bind("mouseenter", function (e) {
                    if (t(this).bind("click", function (e) {
                            t(window).width() > 974 && !t(this).closest("li").hasClass("u01hover") && !t(this).closest("li").hasClass("u01sansmenu") && e.preventDefault()
                    }), "none" == t(".u01mmenu").css("display")) {
                        "hidden" == t(this).find(".u01w5bannerimg img[data-imgpath]").first().css("visibility") && t(this).find(".u01w5bannerimg img[data-imgpath]").each(function () {
                            t(this).attr("src", t(this).attr("data-imgpath")).css("visibility", "visible"), t(this).on("load", function () {
                                t(this).removeAttr("data-imgpath"), t(this).closest(".u01").find(".u01w5bannerimg img[data-imgpath]").each(function () {
                                    t(this).attr("src", t(this).attr("data-imgpath")).css("visibility", "visible"), t(this).removeAttr("data-imgpath")
                                })
                            })
                        });
                        var i = t(this);
                        t.data(this, "u01timer", setTimeout(function () {
                            i.hasClass("u01sansmenu") || t("#u01 .u01z3").addClass("u01z3opened"), i.find("div.u01menu, .u01z1").show(0, function () {
                                t(this).parent("li").addClass("u01hover");
                                var e = 0;
                                t(this).find("div.u01w1").children("div").each(function () {
                                    e = t(this).outerHeight() > e ? t(this).outerHeight() : e
                                }), t(this).find("div.u01w6").each(function () {
                                    t(this).css("height", e - (1 * t(this).css("padding-top").split("px")[0] + 1 * t(this).css("padding-bottom").split("px")[0]))
                                })
                            })
                        }, 300))
                    }
                })
            }), t(this).bind("mouseleave", function (e) {
                clearTimeout(t.data(this, "u01timer")), t(this).find("div.u01menu, .u01z1").hide(), t(".u01nav > li").removeClass("u01hover"), t("#u01 .u01z3").removeClass("u01z3opened")
            })
        }), t(".u01mmenu a").on("click", function (e) {
            e.preventDefault();
            var i = t("#u01 .u01active"),
                n = t(".u01navtools"),
                s = t(this).parent("div").siblings("ul.u01nav"),
                o = t(this).parent("div"),
                a = t(".u01logo + .u01pfile");
            t(n).toggleClass("u01active"), t(s).toggleClass("u01active"), t(o).toggleClass("u01active"), t(a).toggleClass("u01active"), t(i).not(n).not(s).not(o).not(a).removeClass("u01active")
        }), t(window).resize(function () {
            "none" == t(".u01mmenu").css("display") && t("body").hasClass("f11") && (t(".u01navtools").removeClass("u01active"), t("ul.u01nav").removeClass("u01active"), t(".u01mmenu a").parent("div").removeClass("u01active"), t(".u01logo + .u01pfile").removeClass("u01active"), t(".u01search").removeClass("u01active"), t(".u01msearch a").parent("div").removeClass("u01active"))
        }), t(".u01msearch a").on("click", function (e) {
            e.preventDefault();
            var i = t("#u01 .u01active"),
                n = t(".u01search"),
                s = t(this).parent("div");
            t(n).toggleClass("u01active"), t(s).toggleClass("u01active"), t(i).not(n).not(s).removeClass("u01active")
        }))
    }), jQuery(document).ready(function (t) {
        t(".u10w3").append('<div class="u10btn"></div>'), t("ul.u10-links li").last().addClass("u10last"), t(".u10w3 h5, .u10btn").each(function () {
            t(this).click(function (e) {
                var i = t(this).parents("div.u10w3"),
                    n = t("div.u10active");
                t(i).toggleClass("u10active"), t(n).not(i).removeClass("u10active")
            })
        })
    }), jQuery(document).ready(function (t) {
        t("div.hp07v0").each(function (e) {
            function i(t) {
                if (/-bg(......)-/.test(t)) {
                    var e = t.replace(/.*-bg(......).*/gi, "$1");
                    return e
                }
                return "ffffff"
            }
            var n = t(this),
                s = n.hasClass("hp07random") ? Math.floor(Math.random() * (n.find("div.hp07").length - 1 + 1)) + 1 : 1;
            if (n[0].current = s, n.append('<div class="hp07z1"></div>').append('<div class="hp07z2"></div>'), n.find("div.hp07").length > 1) {
                var o = '<div class="hp07nav"><ul style="margin: 0;padding: 0;list-style: none; -webkit-font-smoothing: antialiased; -moz-osx-font-smoothing: grayscale; margin-top:-' + (1.05 * n.find("div.hp07").length / 2 + .15) + 'em">';
                n.find("div.hp07").each(function (e) {
                    t(this).attr("id", "feature-" + (e + 1));
                    var i = e == n[0].current - 1 ? ' class="hp07selected"' : "";
                    o += '<li><a href="#feature-' + (e + 1) + '"' + i + ' id="fnav-' + (e + 1) + '" data-goto="' + (e + 1) + '"> </a></li>'
                }), o += "</ul></div>", n.find(".hp07w2").append(o + '\n')
            } else n.find("div.hp07").first().attr("id", "feature-" + n[0].current), n.addClass("hp07single");
            n.find("div.hp07").each(function (e) {
                if (e == n[0].current - 1) {
                    t(this).addClass("cfeature");
                    var s = t(this).is("[data-bgimg]") ? i(t(this).attr("data-bgimg")) : "ffffff",
                        o = t(this).is("[data-bgimg2x]") && t("html").hasClass("retina") && t(window).width() > 600 ? t(this).attr("data-bgimg2x") : t(this).attr("data-bgimg");

                    var imglnk = t(this).attr("data-bgimglnk");
                    n.append('<div class="hp07w4"><div class="hp07imgslide cslide" id="hp07img-' + (e + 1) + '"><div style="background-color:#' + s + '"><a href="' + imglnk + '"><img class="hp07img" style="background-size: 100% auto!important;" src="' + o + '"></a></div></div></div>');
                    var o = t(this).attr("data-bgimg");
                    
                    imgpreload([o], function (e, s) {
                        var o = s.find("div.hp07w4");
                        s.find("div.hp07").each(function (e) {
                            if (e != n[0].current - 1) {
                                var s = t(this).is("[data-bgimg]") ? i(t(this).attr("data-bgimg")) : "ffffff",
                                    
                                    a = t(this).is("[data-bgimg2x]") && t("html").hasClass("retina") && t(window).width() > 600 ? t(this).attr("data-bgimg2x") : t(this).attr("data-bgimg");
                                var imglnk = t(this).attr("data-bgimglnk");
                                e < n[0].current - 1 ? o.find("#hp07img-" + n[0].current).before('<div class="hp07imgslide" id="hp07img-' + (e + 1) + '"><div style="background-color:#' + s + '"><a href="' + imglnk + '"><img class="hp07img" style="background-size: 100% auto!important;" src="' + a + '"></a></div></div>') : o.append('<div class="hp07imgslide" id="hp07img-' + (e + 1) + '"><div style="background-color:#' + s + '"><a href="' + imglnk + '"><img class="hp07img" style="background-size: 100% auto!important;" src="' + a + '"></a></div></div>')
                            }
                        })
                    }, t(this).closest("div.hp07v0"))
                }
                t(this).attr("data-lbl", "hpf" + (e + 1)), t(this).find(".hp07w3").first().is("[data-lbl]") || t(this).find(".hp07w3").attr("data-lbl", t(this).find(".hp07ttl").text())
            }), t("#feature-" + n[0].current).css("top", 0).css("left", 0), t("#hp07img-" + n[0].current).css("top", 0).css("left", 0)
        }), t("body").on("mouseenter", "#hp07v0", function () {
            t(this).addClass("hp07pause")
        }).on("mouseleave", "#hp07v0", function () {
            t(this).removeClass("hp07pause")
        })
    }), $(window).load(function () {
        var t = $("#hp07v0").is("[data-hp07rotate]") ? $("#hp07v0").attr("data-hp07rotate") : 6;
        $("#hp07v0").attr("data-hp07rotate", t), 0 == t || $("#hp07v0").hasClass("hp07single") || setTimeout(function () {
            hp07goto("+1", "auto")
        }, 1e3 * t)
    }), $(document).on("click", ".hp08promo a[target]", function () {
        $(this).blur()
    }), $(document).on("click", "a.hp07dnav,.hp07nav a", function () {
        if (!$(".hp07busy")[0] && !$(this).hasClass("hp07selected"))
            if (/[-+]/.test($(this).attr("data-goto"))) {
                var t = $(this).attr("data-goto").indexOf("+") > -1 ? "next" : "prev";
                hp07goto($(this).attr("data-goto"), t)
            } else hp07goto(1 * $(this).attr("data-goto"), "nav");
        return !1
    }), jQuery(document).ready(function (t) {
        t("div.hp08v0").each(function () {
            var e = "",
                i = t(this).is("[data-showlist]") ? t(this).attr("data-showlist") : "Show List View",
                n = t(this).is("[data-showimgs]") ? t(this).attr("data-showimgs") : "Show Panel View";
            t(this).addClass("hp08notloaded").addClass("hp08imgitems").attr("data-lbl", "panelview"), t(this).find(".hp08v1").each(function (i) {
                var n = t(this)[0].id,
                    s = t(this).find(".hp08label").text(),
                    o = t(this).is("[data-lbl]") ? t(this).attr("data-lbl") : n;
                e += '<li><a href="#' + n + '" data-lbl="' + o + '">' + s + "<i></i></a></li>", t(this).find(".hp08promo").each(function (e) {
                    t(this).attr("data-lbl", "location" + (e + 1))
                })
            }), e = '<div class="hp08tablist" id="hp08tablist"><ul style="margin: 0; padding: 0; list-style: none; -webkit-font-smoothing: antialiased; -moz-osx-font-smoothing: grayscale; " data-lbl="tab">' + e + '</ul></div>', t(this).prepend(e);
            var s = 1;
            if (t(this).hasClass("randomtab")) {
                var o = 2 * t(this).find(".hp08tablist li").length + 2;
                o = Math.floor(Math.random() * (o - 1 + 1)) + 1, o > 4 && (s = Math.round((o - 2) / 2))
            }
            t(this).find(".hp08tablist li:first").addClass("hp08default"), t(this).prepend('<div id="hp08selector"><a href="#choosefilter" data-lbl="notrack"></a></div>'), t(this).find(".hp08teaser").wrapInner("<span><em></em></span>").append('<b class="hp08arrw"></b>').hide(), t(this).find(".hp08cta").wrapInner("<span></span>").hide();
            var a = '	<div class="hp08v1 hp08bgtiles"><div class="hp08promo hp08promo1 hp08c1 hp08bgtile"></div><div class="hp08promo hp08promo2 hp08c1 hp08bgtile"></div><div class="hp08promo hp08promo3 hp08c1 hp08bgtile"></div></div>';
            t(this).find(".hp08w2").append(a), hp08resize()
        }), t(".hp08v0").waypoint(function (e) {
            try {
                t(this.element).find(".hp08img")[0] || hp08showpromo(t(".hp08v0 .hp08default a").attr("href").split("#")[1])
            } catch (i) { }
        }, {
            offset: "100%"
        })
    }), jQuery(window).resize(function (t) {
        hp08resize()
    }), $(document).on("click", ".hp08tablist ul a", function () {
        return $(this)[0].href.split("#")[1] != $(".hp08v0").attr("data-cfilter") && hp08showpromo($(this)[0].href.split("#")[1]), $(".hp08viewmenu").removeClass("hp08viewmenu"), !1
    }), $(document).on("click", ".hp08tglview a", function () {
        return $(this).closest(".hp08").toggleClass("hp08lvw").toggleClass("hp08imgitems"),
            "panelview" == $(this).closest(".hp08").attr("data-lbl") ? $(this).closest(".hp08").attr("data-lbl", "listview") : $(this).closest(".hp08").attr("data-lbl", "panelview"), hp08resize(), $(this).parent().find(".hp08tgl-icn").css("opacity", .2).animate({
                opacity: 1
            }, 800, "easeInOutSine"), !1
    }), $(document).on("click", "#hp08selector a", function () {
        return $(this).closest(".hp08").toggleClass("hp08viewmenu"), !1
    }), $(document).on("click", ".hp08promo a[target]", function () {
        $(this).blur()
    }), $(document).on("mouseenter", ".hp08notloaded", function () {
        $(this).find(".hp08promo a[data-bgimg]").each(function (t) {
            var e = hp08imgpos($(this).attr("data-bgimg")),
                i = $(this).is("[data-bgimg2x]") && $("html").hasClass("retina") ? $(this).attr("data-bgimg2x") : $(this).attr("data-bgimg");
            $(this).prepend('<div class="hp08img ' + e + '" style="background-image:url(\'' + i + "')\"></div>"), $(this).removeAttr("data-bgimg").removeAttr("data-bgimg2x")
        }), $(this).removeClass("hp08notloaded")
    });