'use strict';
(function (a) {
    function b(k, l) {
        if (null != l && l != void 0 && 'undefined' != l)
            for (var m in l) k[m] = l[m];
        return k
    }

    function c(k) {
        for (var l = void 0, m = void 0, n = 0; n < k.length; n++) k[n].className.match('active') && (m = k[n].querySelector('.' + h.aClass), m.style.height = 'auto', l = m.offsetHeight, m.style.height = l + 'px')
    }

    function d(k) {
        var l = k.querySelector('.' + h.aClass);
        l.style.height = 0
    }

    function e(k) {
        var l = k.querySelector('.' + h.aClass);
        l.style.WebkitTransitionDuration = h.duration + 'ms', l.style.transitionDuration = h.duration + 'ms'
    }

    function f(k) {
        var l, m = k.querySelector('.' + h.aClass);
        if (k.classList) k.classList.toggle('active');
        else {
            var n = k.className.split(' '),
                o = n.indexOf('active');
            0 <= o ? n.splice(o, 1) : n.push('active'), k.className = n.join(' ')
        }
        0 < parseInt(m.style.height) ? m.style.height = 0 : (m.style.height = 'auto', l = m.offsetHeight, m.style.height = 0, setTimeout(function () {
            m.style.height = l + 'px'
        }, 10))
    }

    function g(k, l) {
        for (var m = 0; m < k.length; m++)
            if (m != l) {
                for (var n = '', o = k[m].className.split(' '), p = 0; p < o.length; p++) 'active' !== o[p] && (n += o[p]);
                k[m].className = n, d(k[m])
            }
    }
    var h;
    a.Accordion = function (l, m) {
        h = b({
            duration: 600,
            closeOthers: !0,
            showFirst: !1,
            elClass: 'ac',
            qClass: 'ac-q',
            aClass: 'ac-a'
        }, m);
        for (var o = document.querySelectorAll(l), p = function (s) {
                for (var t = o[s].querySelectorAll('.' + h.elClass), u = function (x) {
                        d(t[x]), e(t[x]), t[x].addEventListener('click', function (y) {
                            var z = this,
                                A = y.target || y.srcElement;
                            A.className.match(h.qClass) && (y.preventDefault ? y.preventDefault() : y.returnValue = !1, !0 === h.closeOthers && g(t, x), f(z))
        })
        }, v = 0; v < t.length; v++) u(v);
                !0 === h.showFirst && f(t[0])
        }, q = 0; q < o.length; q++) p(q);
        a.addEventListener('resize', function () {
            clearTimeout(a.resizeTimer), a.resizeTimer = setTimeout(function () {
                for (var s, r = 0; r < o.length; r++) s = o[r].querySelectorAll('.' + h.elClass), c(s)
            }, 100)
        })
    }
})(window);