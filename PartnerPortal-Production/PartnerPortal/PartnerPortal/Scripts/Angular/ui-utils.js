"use strict"; angular.module("ui.alias", []).config(["$compileProvider", "uiAliasConfig", function (e, n) { n = n || {}, angular.forEach(n, function (n, t) { angular.isString(n) && (n = { replace: !0, template: n }), e.directive(t, function () { return n }) }) }]), angular.module("ui.event", []).directive("uiEvent", ["$parse", function (e) { return function (n, t, r) { var i = n.$eval(r.uiEvent); angular.forEach(i, function (r, i) { var u = e(r); t.bind(i, function (e) { var t = Array.prototype.slice.call(arguments); t = t.splice(1), u(n, { $event: e, $params: t }), n.$$phase || n.$apply() }) }) } }]), angular.module("ui.format", []).filter("format", function () { return function (e, n) { var t = e; if (angular.isString(t) && void 0 !== n) if (angular.isArray(n) || angular.isObject(n) || (n = [n]), angular.isArray(n)) { var r = n.length, i = function (e, t) { return t = parseInt(t, 10), t >= 0 && r > t ? n[t] : e }; t = t.replace(/\$([0-9]+)/g, i) } else angular.forEach(n, function (e, n) { t = t.split(":" + n).join(e) }); return t } }), angular.module("ui.highlight", []).filter("highlight", function () { return function (e, n, t) { return n || angular.isNumber(n) ? (e = e.toString(), n = n.toString(), t ? e.split(n).join('<span class="ui-match">' + n + "</span>") : e.replace(new RegExp(n, "gi"), '<span class="ui-match">$&</span>')) : e } }), angular.module("ui.include", []).directive("uiInclude", ["$http", "$templateCache", "$anchorScroll", "$compile", function (e, n, t, r) { return { restrict: "ECA", terminal: !0, compile: function (i, u) { var o = u.uiInclude || u.src, a = u.fragment || "", l = u.onload || "", c = u.autoscroll; return function (i, u) { function s() { var s = ++d, p = i.$eval(o), h = i.$eval(a); p ? e.get(p, { cache: n }).success(function (e) { if (s === d) { f && f.$destroy(), f = i.$new(); var n; n = h ? angular.element("<div/>").html(e).find(h) : angular.element("<div/>").html(e).contents(), u.html(n), r(n)(f), !angular.isDefined(c) || c && !i.$eval(c) || t(), f.$emit("$includeContentLoaded"), i.$eval(l) } }).error(function () { s === d && g() }) : g() } var f, d = 0, g = function () { f && (f.$destroy(), f = null), u.html("") }; i.$watch(a, s), i.$watch(o, s) } } } }]), angular.module("ui.indeterminate", []).directive("uiIndeterminate", [function () { return { compile: function (e, n) { return n.type && "checkbox" === n.type.toLowerCase() ? function (e, n, t) { e.$watch(t.uiIndeterminate, function (e) { n[0].indeterminate = !!e }) } : angular.noop } } }]), angular.module("ui.inflector", []).filter("inflector", function () { function e(e) { return e.replace(/^([a-z])|\s+([a-z])/g, function (e) { return e.toUpperCase() }) } function n(e, n) { return e.replace(/[A-Z]/g, function (e) { return n + e }) } var t = { humanize: function (t) { return e(n(t, " ").split("_").join(" ")) }, underscore: function (e) { return e.substr(0, 1).toLowerCase() + n(e.substr(1), "_").toLowerCase().split(" ").join("_") }, variable: function (n) { return n = n.substr(0, 1).toLowerCase() + e(n.split("_").join(" ")).substr(1).split(" ").join("") } }; return function (e, n) { return n !== !1 && angular.isString(e) ? (n = n || "humanize", t[n](e)) : e } }), angular.module("ui.jq", []).value("uiJqConfig", {}).directive("uiJq", ["uiJqConfig", "$timeout", function (e, n) { return { restrict: "A", compile: function (t, r) { if (!angular.isFunction(t[r.uiJq])) throw new Error('ui-jq: The "' + r.uiJq + '" function does not exist'); var i = e && e[r.uiJq]; return function (e, t, r) { function u() { n(function () { t[r.uiJq].apply(t, o) }, 0, !1) } var o = []; r.uiOptions ? (o = e.$eval("[" + r.uiOptions + "]"), angular.isObject(i) && angular.isObject(o[0]) && (o[0] = angular.extend({}, i, o[0]))) : i && (o = [i]), r.ngModel && t.is("select,input,textarea") && t.bind("change", function () { t.trigger("input") }), r.uiRefresh && e.$watch(r.uiRefresh, function () { u() }), u() } } } }]), angular.module("ui.keypress", []).factory("keypressHelper", ["$parse", function (e) { var n = { 8: "backspace", 9: "tab", 13: "enter", 27: "esc", 32: "space", 33: "pageup", 34: "pagedown", 35: "end", 36: "home", 37: "left", 38: "up", 39: "right", 40: "down", 45: "insert", 46: "delete" }, t = function (e) { return e.charAt(0).toUpperCase() + e.slice(1) }; return function (r, i, u, o) { var a, l = []; a = i.$eval(o["ui" + t(r)]), angular.forEach(a, function (n, t) { var r, i; i = e(n), angular.forEach(t.split(" "), function (e) { r = { expression: i, keys: {} }, angular.forEach(e.split("-"), function (e) { r.keys[e] = !0 }), l.push(r) }) }), u.bind(r, function (e) { var t = !(!e.metaKey || e.ctrlKey), u = !!e.altKey, o = !!e.ctrlKey, a = !!e.shiftKey, c = e.keyCode; "keypress" === r && !a && c >= 97 && 122 >= c && (c -= 32), angular.forEach(l, function (r) { var l = r.keys[n[c]] || r.keys[c.toString()], s = !!r.keys.meta, f = !!r.keys.alt, d = !!r.keys.ctrl, g = !!r.keys.shift; l && s === t && f === u && d === o && g === a && i.$apply(function () { r.expression(i, { $event: e }) }) }) }) } }]), angular.module("ui.keypress").directive("uiKeydown", ["keypressHelper", function (e) { return { link: function (n, t, r) { e("keydown", n, t, r) } } }]), angular.module("ui.keypress").directive("uiKeypress", ["keypressHelper", function (e) { return { link: function (n, t, r) { e("keypress", n, t, r) } } }]), angular.module("ui.keypress").directive("uiKeyup", ["keypressHelper", function (e) { return { link: function (n, t, r) { e("keyup", n, t, r) } } }]), angular.module("ui.mask", []).value("uiMaskConfig", { maskDefinitions: { 9: /\d/, A: /[a-zA-Z]/, "*": /[a-zA-Z0-9]/ } }).directive("uiMask", ["uiMaskConfig", function (e) { return { priority: 100, require: "ngModel", restrict: "A", compile: function () { var n = e; return function (e, t, r, i) { function u(e) { return angular.isDefined(e) ? ($(e), _ ? (s(), f(), !0) : c()) : c() } function o(e) { angular.isDefined(e) && (O = e, _ && x()) } function a(e) { return _ ? (A = p(e || ""), j = g(A), i.$setValidity("mask", j), j && A.length ? h(A) : void 0) : e } function l(e) { return _ ? (A = p(e || ""), j = g(A), i.$viewValue = A.length ? h(A) : "", i.$setValidity("mask", j), "" === A && void 0 !== i.$error.required && i.$setValidity("required", !1), j ? A : void 0) : e } function c() { return _ = !1, d(), angular.isDefined(F) ? t.attr("placeholder", F) : t.removeAttr("placeholder"), angular.isDefined(K) ? t.attr("maxlength", K) : t.removeAttr("maxlength"), t.val(i.$modelValue), i.$viewValue = i.$modelValue, !1 } function s() { A = D = p(i.$modelValue || ""), P = T = h(A), j = g(A); var e = j && A.length ? P : ""; r.maxlength && t.attr("maxlength", 2 * H[H.length - 1]), t.attr("placeholder", O), t.val(e), i.$viewValue = e } function f() { z || (t.bind("blur", b), t.bind("mousedown mouseup", y), t.bind("input keyup click focus", x), z = !0) } function d() { z && (t.unbind("blur", b), t.unbind("mousedown", y), t.unbind("mouseup", y), t.unbind("input", x), t.unbind("keyup", x), t.unbind("click", x), t.unbind("focus", x), z = !1) } function g(e) { return e.length ? e.length >= R : !0 } function p(e) { var n = "", t = V.slice(); return e = e.toString(), angular.forEach(q, function (n) { e = e.replace(n, "") }), angular.forEach(e.split(""), function (e) { t.length && t[0].test(e) && (n += e, t.shift()) }), n } function h(e) { var n = "", t = H.slice(); return angular.forEach(O.split(""), function (r, i) { e.length && i === t[0] ? (n += e.charAt(0) || "_", e = e.substr(1), t.shift()) : n += r }), n } function m(e) { var n = r.placeholder; return "undefined" != typeof n && n[e] ? n[e] : "_" } function v() { return O.replace(/[_]+/g, "_").replace(/([^_]+)([a-zA-Z0-9])([^_])/g, "$1$2_$3").split("_") } function $(e) { var n = 0; if (H = [], V = [], O = "", "string" == typeof e) { R = 0; var t = !1, r = e.split(""); angular.forEach(r, function (e, r) { W.maskDefinitions[e] ? (H.push(n), O += m(r), V.push(W.maskDefinitions[e]), n++, t || R++) : "?" === e ? t = !0 : (O += e, n++) }) } H.push(H.slice().pop() + 1), q = v(), _ = H.length > 1 ? !0 : !1 } function b() { M = 0, L = 0, j && 0 !== A.length || (P = "", t.val(""), e.$apply(function () { i.$setViewValue("") })) } function y(e) { "mousedown" === e.type ? t.bind("mouseout", w) : t.unbind("mouseout", w) } function w() { L = C(this), t.unbind("mouseout", w) } function x(n) { n = n || {}; var r = n.which, u = n.type; if (16 !== r && 91 !== r) { var o, a = t.val(), l = T, c = p(a), s = D, f = !1, d = S(this) || 0, g = M || 0, m = d - g, v = H[0], $ = H[c.length] || H.slice().shift(), b = L || 0, y = C(this) > 0, w = b > 0, x = a.length > l.length || b && a.length > l.length - b, V = a.length < l.length || b && a.length === l.length - b, O = r >= 37 && 40 >= r && n.shiftKey, q = 37 === r, R = 8 === r || "keyup" !== u && V && -1 === m, A = 46 === r || "keyup" !== u && V && 0 === m && !w, P = (q || R || "click" === u) && d > v; if (L = C(this), !O && (!y || "click" !== u && "keyup" !== u)) { if ("input" === u && V && !w && c === s) { for (; R && d > v && !k(d) ;) d--; for (; A && $ > d && -1 === H.indexOf(d) ;) d++; var j = H.indexOf(d); c = c.substring(0, j) + c.substring(j + 1), f = !0 } for (o = h(c), T = o, D = c, t.val(o), f && e.$apply(function () { i.$setViewValue(c) }), x && v >= d && (d = v + 1), P && d--, d = d > $ ? $ : v > d ? v : d; !k(d) && d > v && $ > d;) d += P ? -1 : 1; (P && $ > d || x && !k(g)) && d++, M = d, E(this, d) } } } function k(e) { return H.indexOf(e) > -1 } function S(e) { if (!e) return 0; if (void 0 !== e.selectionStart) return e.selectionStart; if (document.selection) { e.focus(); var n = document.selection.createRange(); return n.moveStart("character", -e.value.length), n.text.length } return 0 } function E(e, n) { if (!e) return 0; if (0 !== e.offsetWidth && 0 !== e.offsetHeight) if (e.setSelectionRange) e.focus(), e.setSelectionRange(n, n); else if (e.createTextRange) { var t = e.createTextRange(); t.collapse(!0), t.moveEnd("character", n), t.moveStart("character", n), t.select() } } function C(e) { return e ? void 0 !== e.selectionStart ? e.selectionEnd - e.selectionStart : document.selection ? document.selection.createRange().text.length : 0 : 0 } var H, V, O, q, R, A, P, j, T, D, M, L, _ = !1, z = !1, F = r.placeholder, K = r.maxlength, W = {}; r.uiOptions ? (W = e.$eval("[" + r.uiOptions + "]"), angular.isObject(W[0]) && (W = function (e, n) { for (var t in e) Object.prototype.hasOwnProperty.call(e, t) && (n[t] ? angular.extend(n[t], e[t]) : n[t] = angular.copy(e[t])); return n }(n, W[0]))) : W = n, r.$observe("uiMask", u), r.$observe("placeholder", o), i.$formatters.push(a), i.$parsers.push(l), t.bind("mousedown mouseup", y), Array.prototype.indexOf || (Array.prototype.indexOf = function (e) { if (null === this) throw new TypeError; var n = Object(this), t = n.length >>> 0; if (0 === t) return -1; var r = 0; if (arguments.length > 1 && (r = Number(arguments[1]), r !== r ? r = 0 : 0 !== r && r !== 1 / 0 && r !== -(1 / 0) && (r = (r > 0 || -1) * Math.floor(Math.abs(r)))), r >= t) return -1; for (var i = r >= 0 ? r : Math.max(t - Math.abs(r), 0) ; t > i; i++) if (i in n && n[i] === e) return i; return -1 }) } } } }]), angular.module("ui.reset", []).value("uiResetConfig", null).directive("uiReset", ["uiResetConfig", function (e) { var n = null; return void 0 !== e && (n = e), { require: "ngModel", link: function (e, t, r, i) { var u; u = angular.element('<a class="ui-reset" />'), t.wrap('<span class="ui-resetwrap" />').after(u), u.bind("click", function (t) { t.preventDefault(), e.$apply(function () { r.uiReset ? i.$setViewValue(e.$eval(r.uiReset)) : i.$setViewValue(n), i.$render() }) }) } } }]), angular.module("ui.route", []).directive("uiRoute", ["$location", "$parse", function (e, n) { return { restrict: "AC", scope: !0, compile: function (t, r) { var i; if (r.uiRoute) i = "uiRoute"; else if (r.ngHref) i = "ngHref"; else { if (!r.href) throw new Error("uiRoute missing a route or href property on " + t[0]); i = "href" } return function (t, r, u) { function o(n) { var r = n.indexOf("#"); r > -1 && (n = n.substr(r + 1)), (c = function () { l(t, e.path().indexOf(n) > -1) })() } function a(n) { var r = n.indexOf("#"); r > -1 && (n = n.substr(r + 1)), (c = function () { var r = new RegExp("^" + n + "$", ["i"]); l(t, r.test(e.path())) })() } var l = n(u.ngModel || u.routeModel || "$uiRoute").assign, c = angular.noop; switch (i) { case "uiRoute": u.uiRoute ? a(u.uiRoute) : u.$observe("uiRoute", a); break; case "ngHref": u.ngHref ? o(u.ngHref) : u.$observe("ngHref", o); break; case "href": o(u.href) } t.$on("$routeChangeSuccess", function () { c() }), t.$on("$stateChangeSuccess", function () { c() }) } } } }]), angular.module("ui.scroll.jqlite", ["ui.scroll"]).service("jqLiteExtras", ["$log", "$window", function (e, n) { return { registerFor: function (e) { var t, r, i, u, o, a, l; return r = angular.element.prototype.css, e.prototype.css = function (e, n) { var t, i; return i = this, t = i[0], t && 3 !== t.nodeType && 8 !== t.nodeType && t.style ? r.call(i, e, n) : void 0 }, a = function (e) { return e && e.document && e.location && e.alert && e.setInterval }, l = function (e, n, t) { var r, i, u, o, l; return r = e[0], l = { top: ["scrollTop", "pageYOffset", "scrollLeft"], left: ["scrollLeft", "pageXOffset", "scrollTop"] }[n], i = l[0], o = l[1], u = l[2], a(r) ? angular.isDefined(t) ? r.scrollTo(e[u].call(e), t) : o in r ? r[o] : r.document.documentElement[i] : angular.isDefined(t) ? r[i] = t : r[i] }, n.getComputedStyle ? (u = function (e) { return n.getComputedStyle(e, null) }, t = function (e, n) { return parseFloat(n) }) : (u = function (e) { return e.currentStyle }, t = function (e, n) { var t, r, i, u, o, a, l; return t = /[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source, u = new RegExp("^(" + t + ")(?!px)[a-z%]+$", "i"), u.test(n) ? (l = e.style, r = l.left, o = e.runtimeStyle, a = o && o.left, o && (o.left = l.left), l.left = n, i = l.pixelLeft, l.left = r, a && (o.left = a), i) : parseFloat(n) }), i = function (e, n) { var r, i, o, l, c, s, f, d, g, p, h, m, v; return a(e) ? (r = document.documentElement[{ height: "clientHeight", width: "clientWidth" }[n]], { base: r, padding: 0, border: 0, margin: 0 }) : (v = { width: [e.offsetWidth, "Left", "Right"], height: [e.offsetHeight, "Top", "Bottom"] }[n], r = v[0], f = v[1], d = v[2], s = u(e), h = t(e, s["padding" + f]) || 0, m = t(e, s["padding" + d]) || 0, i = t(e, s["border" + f + "Width"]) || 0, o = t(e, s["border" + d + "Width"]) || 0, l = s["margin" + f], c = s["margin" + d], g = t(e, l) || 0, p = t(e, c) || 0, { base: r, padding: h + m, border: i + o, margin: g + p }) }, o = function (e, n, t) { var r, o, a; return o = i(e, n), o.base > 0 ? { base: o.base - o.padding - o.border, outer: o.base, outerfull: o.base + o.margin }[t] : (r = u(e), a = r[n], (0 > a || null === a) && (a = e.style[n] || 0), a = parseFloat(a) || 0, { base: a - o.padding - o.border, outer: a, outerfull: a + o.padding + o.border + o.margin }[t]) }, angular.forEach({ before: function (e) { var n, t, r, i, u, o, a; if (u = this, t = u[0], i = u.parent(), n = i.contents(), n[0] === t) return i.prepend(e); for (r = o = 1, a = n.length - 1; a >= 1 ? a >= o : o >= a; r = a >= 1 ? ++o : --o) if (n[r] === t) return void angular.element(n[r - 1]).after(e); throw new Error("invalid DOM structure " + t.outerHTML) }, height: function (e) { var n; return n = this, angular.isDefined(e) ? (angular.isNumber(e) && (e += "px"), r.call(n, "height", e)) : o(this[0], "height", "base") }, outerHeight: function (e) { return o(this[0], "height", e ? "outerfull" : "outer") }, offset: function (e) { var n, t, r, i, u, o; return u = this, arguments.length ? void 0 === e ? u : e : (n = { top: 0, left: 0 }, i = u[0], (t = i && i.ownerDocument) ? (r = t.documentElement, i.getBoundingClientRect && (n = i.getBoundingClientRect()), o = t.defaultView || t.parentWindow, { top: n.top + (o.pageYOffset || r.scrollTop) - (r.clientTop || 0), left: n.left + (o.pageXOffset || r.scrollLeft) - (r.clientLeft || 0) }) : void 0) }, scrollTop: function (e) { return l(this, "top", e) }, scrollLeft: function (e) { return l(this, "left", e) } }, function (n, t) { return e.prototype[t] ? void 0 : e.prototype[t] = n }) } } }]).run(["$log", "$window", "jqLiteExtras", function (e, n, t) { return n.jQuery ? void 0 : t.registerFor(angular.element) }]), angular.module("ui.scroll", []).directive("ngScrollViewport", ["$log", function () { return { controller: ["$scope", "$element", function (e, n) { return n }] } }]).directive("ngScroll", ["$log", "$injector", "$rootScope", "$timeout", function (e, n, t, r) { return { require: ["?^ngScrollViewport"], transclude: "element", priority: 1e3, terminal: !0, compile: function (i, u, o) { return function (u, a, l, c) { var s, f, d, g, p, h, m, v, $, b, y, w, x, k, S, E, C, H, V, O, q, R, A, P, j, T, D, M, L, _, z, F, K, W, J, I; if (P = l.ngScroll.match(/^\s*(\w+)\s+in\s+(\w+)\s*$/), !P) throw new Error('Expected ngScroll in form of "item_ in _datasource_" but got "' + l.ngScroll + '"'); if (R = P[1], w = P[2], O = function (e) { return angular.isObject(e) && e.get && angular.isFunction(e.get) }, y = u[w], !O(y) && (y = n.get(w), !O(y))) throw new Error(w + " is not a valid datasource"); return v = Math.max(3, +l.bufferSize || 10), m = function () { return I.height() * Math.max(.1, +l.padding || .1) }, z = function (e) { return e[0].scrollHeight || e[0].document.documentElement.scrollHeight }, s = null, o(W = u.$new(), function (e) { var n, t, r, u, o, a; if (u = e[0].localName, "dl" === u) throw new Error("ng-scroll directive does not support <" + e[0].localName + "> as a repeating tag: " + e[0].outerHTML); return "li" !== u && "tr" !== u && (u = "div"), a = c[0] || angular.element(window), a.css({ "overflow-y": "auto", display: "block" }), r = function (e) { var n, t, r; switch (e) { case "tr": return r = angular.element("<table><tr><td><div></div></td></tr></table>"), n = r.find("div"), t = r.find("tr"), t.paddingHeight = function () { return n.height.apply(n, arguments) }, t; default: return t = angular.element("<" + e + "></" + e + ">"), t.paddingHeight = t.height, t } }, t = function (e, n, t) { return n[{ top: "before", bottom: "after" }[t]](e), { paddingHeight: function () { return e.paddingHeight.apply(e, arguments) }, insert: function (n) { return e[{ top: "after", bottom: "before" }[t]](n) } } }, o = t(r(u), i, "top"), n = t(r(u), i, "bottom"), W.$destroy(), s = { viewport: a, topPadding: o.paddingHeight, bottomPadding: n.paddingHeight, append: n.insert, prepend: o.insert, bottomDataPos: function () { return z(a) - n.paddingHeight() }, topDataPos: function () { return o.paddingHeight() } } }), I = s.viewport, H = 1, j = 1, h = [], T = [], k = !1, g = !1, A = y.loading || function () { }, q = !1, M = function (e, n) { var t, r; for (t = r = e; n >= e ? n > r : r > n; t = n >= e ? ++r : --r) h[t].scope.$destroy(), h[t].element.remove(); return h.splice(e, n - e) }, D = function () { return H = 1, j = 1, M(0, h.length), s.topPadding(0), s.bottomPadding(0), T = [], k = !1, g = !1, f(!1) }, p = function () { return I.scrollTop() + I.height() }, J = function () { return I.scrollTop() }, F = function () { return !k && s.bottomDataPos() < p() + m() }, $ = function () { var n, t, r, i, u, o; for (n = 0, i = 0, t = u = o = h.length - 1; (0 >= o ? 0 >= u : u >= 0) && (r = h[t].element.outerHeight(!0), s.bottomDataPos() - n - r > p() + m()) ; t = 0 >= o ? ++u : --u) n += r, i++, k = !1; return i > 0 ? (s.bottomPadding(s.bottomPadding() + n), M(h.length - i, h.length), j -= i, e.log("clipped off bottom " + i + " bottom padding " + s.bottomPadding())) : void 0 }, K = function () { return !g && s.topDataPos() > J() - m() }, b = function () { var n, t, r, i, u, o; for (i = 0, r = 0, u = 0, o = h.length; o > u && (n = h[u], t = n.element.outerHeight(!0), s.topDataPos() + i + t < J() - m()) ; u++) i += t, r++, g = !1; return r > 0 ? (s.topPadding(s.topPadding() + i), M(0, r), H += r, e.log("clipped off top " + r + " top padding " + s.topPadding())) : void 0 }, x = function (e, n) { return q || (q = !0, A(!0)), 1 === T.push(e) ? E(n) : void 0 }, V = function (e, n) { var t, r, i; return t = u.$new(), t[R] = n, r = e > H, t.$index = e, r && t.$index--, i = { scope: t }, o(t, function (n) { return i.element = n, r ? e === j ? (s.append(n), h.push(i)) : (h[e - H].element.after(n), h.splice(e - H + 1, 0, i)) : (s.prepend(n), h.unshift(i)) }), { appended: r, wrapper: i } }, d = function (e, n) { var t; return e ? s.bottomPadding(Math.max(0, s.bottomPadding() - n.element.outerHeight(!0))) : (t = s.topPadding() - n.element.outerHeight(!0), t >= 0 ? s.topPadding(t) : I.scrollTop(I.scrollTop() + n.element.outerHeight(!0))) }, f = function (n, t, i) { var u; return u = function () { return e.log("top {actual=" + s.topDataPos() + " visible from=" + J() + " bottom {visible through=" + p() + " actual=" + s.bottomDataPos() + "}"), F() ? x(!0, n) : K() && x(!1, n), i ? i() : void 0 }, t ? r(function () { var e, n, r; for (n = 0, r = t.length; r > n; n++) e = t[n], d(e.appended, e.wrapper); return u() }) : u() }, C = function (e, n) { return f(e, n, function () { return T.shift(), 0 === T.length ? (q = !1, A(!1)) : E(e) }) }, E = function (n) { var t; return t = T[0], t ? h.length && !F() ? C(n) : y.get(j, v, function (t) { var r, i, u, o; if (i = [], 0 === t.length) k = !0, s.bottomPadding(0), e.log("appended: requested " + v + " records starting from " + j + " recieved: eof"); else { for (b(), u = 0, o = t.length; o > u; u++) r = t[u], i.push(V(++j, r)); e.log("appended: requested " + v + " received " + t.length + " buffer size " + h.length + " first " + H + " next " + j) } return C(n, i) }) : h.length && !K() ? C(n) : y.get(H - v, v, function (t) { var r, i, u, o; if (i = [], 0 === t.length) g = !0, s.topPadding(0), e.log("prepended: requested " + v + " records starting from " + (H - v) + " recieved: bof"); else { for ($(), r = u = o = t.length - 1; 0 >= o ? 0 >= u : u >= 0; r = 0 >= o ? ++u : --u) i.unshift(V(--H, t[r])); e.log("prepended: requested " + v + " received " + t.length + " buffer size " + h.length + " first " + H + " next " + j) } return C(n, i) }) }, L = function () { return t.$$phase || q ? void 0 : (f(!1), u.$apply()) }, I.bind("resize", L), _ = function () { return t.$$phase || q ? void 0 : (f(!0), u.$apply()) }, I.bind("scroll", _), u.$watch(y.revision, function () { return D() }), S = y.scope ? y.scope.$new() : u.$new(), u.$on("$destroy", function () { return S.$destroy(), I.unbind("resize", L), I.unbind("scroll", _) }), S.$on("update.items", function (e, n, t) { var r, i, u, o, a; if (angular.isFunction(n)) for (i = function (e) { return n(e.scope) }, u = 0, o = h.length; o > u; u++) r = h[u], i(r); else 0 <= (a = n - H - 1) && a < h.length && (h[n - H - 1].scope[R] = t); return null }), S.$on("delete.items", function (e, n) { var t, r, i, u, o, a, l, c, s, d, g, p; if (angular.isFunction(n)) { for (i = [], a = 0, s = h.length; s > a; a++) r = h[a], i.unshift(r); for (o = function (e) { return n(e.scope) ? (M(i.length - 1 - t, i.length - t), j--) : void 0 }, t = l = 0, d = i.length; d > l; t = ++l) u = i[t], o(u) } else 0 <= (p = n - H - 1) && p < h.length && (M(n - H - 1, n - H), j--); for (t = c = 0, g = h.length; g > c; t = ++c) r = h[t], r.scope.$index = H + t; return f(!1) }), S.$on("insert.item", function (e, n, t) { var r, i, u, o, a, l, c, s, d, g, p, m; if (i = [], angular.isFunction(n)) { for (u = [], l = 0, d = h.length; d > l; l++) t = h[l], u.unshift(t); for (a = function (e) { var u, o, a, l, c; if (o = n(e.scope)) { if (V = function (e, n) { return V(e, n), j++ }, angular.isArray(o)) { for (c = [], u = a = 0, l = o.length; l > a; u = ++a) t = o[u], c.push(i.push(V(r + u, t))); return c } return i.push(V(r, o)) } }, r = c = 0, g = u.length; g > c; r = ++c) o = u[r], a(o) } else 0 <= (m = n - H - 1) && m < h.length && (i.push(V(n, t)), j++); for (r = s = 0, p = h.length; p > s; r = ++s) t = h[r], t.scope.$index = H + r; return f(!1, i) }) } } } }]), angular.module("ui.scrollfix", []).directive("uiScrollfix", ["$window", function (e) { return { require: "^?uiScrollfixTarget", link: function (n, t, r, i) { function u() { var n; if (angular.isDefined(e.pageYOffset)) n = e.pageYOffset; else { var i = document.compatMode && "BackCompat" !== document.compatMode ? document.documentElement : document.body; n = i.scrollTop } !t.hasClass("ui-scrollfix") && n > r.uiScrollfix ? t.addClass("ui-scrollfix") : t.hasClass("ui-scrollfix") && n < r.uiScrollfix && t.removeClass("ui-scrollfix") } var o = t[0].offsetTop, a = i && i.$element || angular.element(e); r.uiScrollfix ? "string" == typeof r.uiScrollfix && ("-" === r.uiScrollfix.charAt(0) ? r.uiScrollfix = o - parseFloat(r.uiScrollfix.substr(1)) : "+" === r.uiScrollfix.charAt(0) && (r.uiScrollfix = o + parseFloat(r.uiScrollfix.substr(1)))) : r.uiScrollfix = o, a.on("scroll", u), n.$on("$destroy", function () { a.off("scroll", u) }) } } }]).directive("uiScrollfixTarget", [function () { return { controller: ["$element", function (e) { this.$element = e }] } }]), angular.module("ui.showhide", []).directive("uiShow", [function () { return function (e, n, t) { e.$watch(t.uiShow, function (e) { e ? n.addClass("ui-show") : n.removeClass("ui-show") }) } }]).directive("uiHide", [function () { return function (e, n, t) { e.$watch(t.uiHide, function (e) { e ? n.addClass("ui-hide") : n.removeClass("ui-hide") }) } }]).directive("uiToggle", [function () { return function (e, n, t) { e.$watch(t.uiToggle, function (e) { e ? n.removeClass("ui-hide").addClass("ui-show") : n.removeClass("ui-show").addClass("ui-hide") }) } }]), angular.module("ui.unique", []).filter("unique", ["$parse", function (e) { return function (n, t) { if (t === !1) return n; if ((t || angular.isUndefined(t)) && angular.isArray(n)) { var r = [], i = angular.isString(t) ? e(t) : function (e) { return e }, u = function (e) { return angular.isObject(e) ? i(e) : e }; angular.forEach(n, function (e) { for (var n = !1, t = 0; t < r.length; t++) if (angular.equals(u(r[t]), u(e))) { n = !0; break } n || r.push(e) }), n = r } return n } }]), angular.module("ui.validate", []).directive("uiValidate", function () { return { restrict: "A", require: "ngModel", link: function (e, n, t, r) { function i(n) { return angular.isString(n) ? void e.$watch(n, function () { angular.forEach(o, function (e) { e(r.$modelValue) }) }) : angular.isArray(n) ? void angular.forEach(n, function (n) { e.$watch(n, function () { angular.forEach(o, function (e) { e(r.$modelValue) }) }) }) : void (angular.isObject(n) && angular.forEach(n, function (n, t) { angular.isString(n) && e.$watch(n, function () { o[t](r.$modelValue) }), angular.isArray(n) && angular.forEach(n, function (n) { e.$watch(n, function () { o[t](r.$modelValue) }) }) })) } var u, o = {}, a = e.$eval(t.uiValidate); a && (angular.isString(a) && (a = { validator: a }), angular.forEach(a, function (n, t) { u = function (i) { var u = e.$eval(n, { $value: i }); return angular.isObject(u) && angular.isFunction(u.then) ? (u.then(function () { r.$setValidity(t, !0) }, function () { r.$setValidity(t, !1) }), i) : u ? (r.$setValidity(t, !0), i) : (r.$setValidity(t, !1), i) }, o[t] = u, r.$formatters.push(u), r.$parsers.push(u) }), t.uiValidateWatch && i(e.$eval(t.uiValidateWatch))) } } }), angular.module("ui.utils", ["ui.event", "ui.format", "ui.highlight", "ui.include", "ui.indeterminate", "ui.inflector", "ui.jq", "ui.keypress", "ui.mask", "ui.reset", "ui.route", "ui.scrollfix", "ui.scroll", "ui.scroll.jqlite", "ui.showhide", "ui.unique", "ui.validate"]);