"use strict";
app.directive('reservationWidget', ['$rootScope', '$routeParams', '$timeout', 'ReservationService', function ($rootScope, $routeParams, $timeout, ReservationService) {

        return {
            restrict: 'E',
            scope: {
                selectedTables: '=',
                callbackChangeParameters: '&',
                callbackBooking: '&',
                isKassa: '='
            },            
            templateUrl: 'app/views/reservationWidget.html',
            link: function (scope, element, attrs, controller) {

                var s, l = 300;
                scope.loadingState = true;
                scope.times = [];
                scope.selected = {};
                scope.isAuthorized = $rootScope.isAuthorized;
                scope.userRoles = $rootScope.userRoles;

                scope.isAuthorizedSubmit = function () {                                        
                    if (scope.isKassa) {
                        return true;
                    }
                    return !scope.isAuthorized([scope.userRoles.Administrator, scope.userRoles.BarOwner]);
                };

                scope.init = function (roomId) {
                    ReservationService.getMerchantSettings(roomId).then(function (t) {
                        scope.settings = t.merchantSettings;
                        scope.settings.id = roomId;
                        scope.request = t.request;
                        scope.request.capacity = t.merchantSettings.getClosestCapacity(t.request.capacity);
                        scope.minDate = t.merchantSettings.getMinDate();
                        scope.maxDate = t.merchantSettings.getMaxDate()
                    });
                };
                scope.closeAll = function () {
                    scope.closeCalendar();
                    scope.toggleTimes(!1);
                };
                scope.closeCalendar = function () {
                    scope.loadingState || (scope.expandCalendar = !1)
                };
                scope.toggleCalendar = function () {
                    scope.expandCalendar = !scope.expandCalendar
                };
                scope.toggleTimes = function (e) {
                    scope.expandTimes = "boolean" != typeof e ? !scope.expandTimes : e
                };
                scope.selectTime = function (t) {
                    scope.request.time = t;
                    scope.selected.time = t;
                    scope.closeAll()
                };

                scope.booking = function () {
                    scope.callbackBooking({
                        time: scope.request.time,
                        date: scope.request.date,
                        capacity: scope.request.capacity
                    });
                };

                scope.$watch("[request.capacity, request.date]", function refreshSelectedTime(r, i) {
                    if (r !== i) {                        
                        angular.isDefined(i[1]) && scope.closeAll();
                        $timeout.cancel(s);
                        scope.loadingState = true;
                        s = $timeout(function () {
                            ReservationService.getTimes(scope.settings.id, scope.request.capacity, scope.request.date).then(function (t) {
                                scope.times = t.times;
                                scope.request.time ? scope.request.time = scope.request.time.fromCollection(scope.times) : t.selectedTime ? scope.request.time = t.selectedTime : t.times.length && (scope.request.time = t.times[0]);
                                scope.loadingState = false;
                            })
                        }, l)
                    }
                });

                scope.$watch("request.time", function refreshSelectedTime(r, i) {
                    if (r !== i) {                                                
                        scope.callbackChangeParameters({
                            time: scope.request.time,
                            date: scope.request.date,
                            capacity: scope.request.capacity
                        });
                    }
                });

                function init() {
                    if (!$routeParams.id) {
                        return;
                    }
                    scope.init($routeParams.id);
                };

                init();               
            }
        }
    }
]);

app.constant("TIME", {
    DATETIME_API_FORMAT: "yyyy-MM-ddTHH:mm:ss",
    DATETIME_FORMAT_PATTERN: /([0-9]{4})\-([0-9]{1,2})-([0-9]{1,2})T([0-9]{1,2}):([0-9]{1,2}):([0-9]{1,2})/,
    DATETIME_FORMAT_PATTERN_ARRAY: [/([0-9]{4})\-([0-9]{1,2})-([0-9]{1,2})T([0-9]{1,2}):([0-9]{1,2}):([0-9]{1,2})/, /([0-9]{4})\-([0-9]{1,2})-([0-9]{1,2})/],
    MORNING_SHIFT: "MORNING",
    MIDDAY_SHIFT: "MIDDAY",
    EVENING_SHIFT: "EVENING"
}).factory("Time", ["TIME", "$filter", function (e, t) {
    "use strict";

    function n(e, t, r, i) {
        this.date = e;
        this.timestamp = t;
        //this.discountId = n;
        //this.discount = null;
        this.available = r;
        this.shift = i;        
    }
    n.build = function (e) {
        return new n(e.date, e.timestamp, e.available, e.shift)
    };
    n.apiResponseTransformer = function (e) {
        var t = {
            date: n.parseApiDateTime(e.date),
            timestamp: e.timestamp ? 1e3 * e.timestamp : null,
            //discount: {
            //    id: null
            //},
            available: e.available            
        };
        if (!angular.isDate(t.date)) return null;
        //angular.isDefined(e.discount) && null !== e.discount && (t.discount.id = e.discount.id);
        return n.build(t)
    };
    n.parseApiDateTime = function (t) {
        var n, r, i = 0;
        n = new Date;
        do {
            r = t.match(e.DATETIME_FORMAT_PATTERN_ARRAY[i]);
            i++
        } while (!angular.isArray(r) && e.DATETIME_FORMAT_PATTERN_ARRAY.length > i);
        if (angular.isArray(r)) {
            n = new Date(parseInt(r[1], 10), parseInt(r[2], 10) - 1, parseInt(r[3], 10), parseInt(r[4] ? r[4] : 0, 10), parseInt(r[5] ? r[5] : 0, 10), parseInt(r[6] ? r[6] : 0, 10));
            return n
        }
        return null
    };
    n.dateToString = function (n) {
        return t("date")(n, e.DATETIME_API_FORMAT)
    };
    n.prototype.toString = function () {
        return n.dateToString(this.date)
    };
    n.prototype.getDate = function () {
        return angular.copy(this.date).setHours(0, 0, 0, 0)
    };
    n.prototype.fromCollection = function (e) {
        if (!angular.isArray(e) || 0 === e.length) return null;
        for (var t = 0, n = e.length; n > t; t++)
            if (e[t].hasOwnProperty("date") && e[t].date.getHours() === this.date.getHours() && e[t].date.getMinutes() === this.date.getMinutes()) return e[t];
        return e[0]
    };
    return n
}]);

app.factory("MerchantSetting", ["Time", function (t) {
    "use strict";

    function n(e, t, n, r, i, a) {
        this.id = e;
        this.capacities = t;
        this.restrictedDays = n;
        this.allowedDays = r;
        this.restrictedWeekDays = i;
        this.maxDayInAdvance = a;        
    }
    n.build = function (e) {
        return new n(e.id, e.capacities, e.restrictedDays, e.allowedDays, e.restrictedWeekDays, e.maxDayInAdvance)
    };
    n.apiResponseTransformer = function (r) {
        var i, a = [],
            o = [],
            s = [],
            l = [];        
        angular.isArray(r.reservation_restricted_days) && (s = r.reservation_restricted_days.map(t.parseApiDateTime).filter(Boolean));
        angular.isArray(r.reservation_allowed_days) && (l = r.reservation_allowed_days.map(t.parseApiDateTime).filter(Boolean));
        angular.forEach(r.reservation_restricted_weekdays, function (e) {
            o.push(e)
        });
        i = {
            id: r.id,
            capacities: r.capacities,
            restrictedDays: s,
            allowedDays: l,
            restrictedWeekDays: o,
            maxDayInAdvance: r.reservation_allowed_advance            
        };
        return n.build(i)
    };      
    n.prototype.isClosed = function (e, t) {
        var n, r;
        e.setHours(0, 0, 0, 0);
        if (angular.isDefined(t) && "day" !== t) return !1;
        for (n = 0, r = this.restrictedDays.length; r > n; n++)
            if (e.getTime() === this.restrictedDays[n].getTime()) return !0;
        for (n = 0, r = this.allowedDays.length; r > n; n++)
            if (e.getTime() === this.allowedDays[n].getTime()) return !1;
        for (n = 0, r = this.restrictedWeekDays.length; r > n; n++)
            if (e.getDay() === this.restrictedWeekDays[n]) return !0;
        return !1
    };    
    n.prototype.getMaxDate = function () {
        var e = new Date;
        e.setTime(e.getTime() + 1e3 * 3600 * 24 * (this.maxDayInAdvance ? this.maxDayInAdvance : 360));
        e.setHours(0, 0, 0, 0);
        return e
    };
    n.prototype.getMinDate = function () {
        var e = new Date;
        e.setHours(0, 0, 0, 0);
        return e
    };
    n.prototype.getClosestCapacity = function (e) {
        var t, n, r;
        if (angular.isArray(this.capacities)) {
            t = this.capacities[0];
            for (var i = 0, a = this.capacities.length; a > i; i++) {
                n = Math.abs(e - t);
                r = Math.abs(e - this.capacities[i]);
                n > r && (t = this.capacities[i]);
                n = null;
                r = null
            }
            return t
        }
        return null
    };
    return n
}]);

app.factory("Request", ["Time", function (e) {
    "use strict";

    function t(e, t, n) {
        this.time = e;
        this.capacity = t;
        this.date = n;
        //this.discount = r
    }
    t.build = function (e) {
        return new t(e.time, parseInt(e.capacity), e.date)
    };
    t.apiResponseTransformer = function (n) {
        var r;
        r = {
            time: null,
            date: null,
            capacity: null
            //discount: null            
        };
        //n.hasOwnProperty("selected_time") && n.selected_time && (r.time = e.apiResponseTransformer(n.selected_time));
        if (n.hasOwnProperty("selected_date") && n.selected_date) {
            r.date = new Date(e.parseApiDateTime(n.selected_date).setHours(0, 0, 0, 0));
            r.time = e.apiResponseTransformer({
                date: n.selected_date
            })
        }
        r.capacity = n.selected_capacity;
        return t.build(r)
    };
    return t
}]);

app.factory("ReservationService", ["$http", "$q", "MerchantSetting", "Request", "Time", "ngAuthSettings", function (e, t, n, r, i, ngAuthSettings) {
    "use strict";

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    function findData(e) {
        for (; e.hasOwnProperty("data") ;) e = e.data;
        return e
    }

    function getMerchantSettingsThen(e) {
        e = findData(e);
        return {
            merchantSettings: n.apiResponseTransformer(e),
            request: r.apiResponseTransformer(e)
        }
    }

    function getTimesThen(e) {
        var t, n = [],
            s = [],
            l = null;
        e = findData(e);
        if (angular.isArray(e.times))
            for (var c = 0, u = e.times.length; u > c; c++)
                for (var d = 0, f = e.times[c].times.length; f > d; d++) {
                    t = i.apiResponseTransformer(e.times[c].times[d]);
                    t.shift = e.times[c].name;
                    angular.isDate(t.date) && n.push(t)
                }        
        return {
            times: n,
            selectedTime: l,            
            request: r.apiResponseTransformer(e)
        }
    }

    function toLocalIsoString(date) {
        function pad(n) { return n < 10 ? '0' + n : n }
        var localIsoString = date.getFullYear() + '-'
            + pad(date.getMonth() + 1) + '-'
            + pad(date.getDate()) + 'T'
            + pad(date.getHours()) + ':'
            + pad(date.getMinutes()) + ':'
            + pad(date.getSeconds());
        if (date.getTimezoneOffset() == 0) localIsoString += 'Z';
        return localIsoString;
    };

    return {
        getMerchantSettings: function (id) {
            var reservation = {
                roomId: id
            };
            return e.post(serviceBase + "api/Booking/GetReservation", reservation).then(getMerchantSettingsThen)
        },
        getTimes: function (id, count, date) {
            var reservation = {
                roomId: id,
                peopleCount: count,
                time: toLocalIsoString(date)
            };            
            return e.post(serviceBase + "api/Booking/GetReservation", reservation).then(getTimesThen)
        }
    }
}]);

app.directive("ecIncrementation", ["$compile", function (e) {
    "use strict";
    return {
        restrict: "A",
        scope: {
            asName: "@?ecIncrementation",
            collection: "=ecCollection",
            current: "=ngModel",
            valueField: "@?ecValueField"
        },
        require: "ngModel",
        link: function (t, n, r, i) {
            function a(e) {
                for (var n = 0, r = t.collection.length; r > n; n++) {
                    if ("object" != typeof e && e === t.collection[n]) return n;
                    if ("object" == typeof e && e.hasOwnProperty(c) && t.collection[n].hasOwnProperty(c) && e[c] === t.collection[n][c]) return n
                }
                return l
            }

            function o(e, t) {
                return "object" != typeof e && "object" != typeof t ? e === t : e && t && e.hasOwnProperty(c) && t.hasOwnProperty(c) && e[c] === t[c]
            }

            function s() {
                var e, n = t.asName || "obj";
                t[n] = {};
                i.$viewValue || (i.$viewValue = t.collection[l]);
                if ("object" == typeof i.$viewValue) {
                    e = i.$viewValue;
                    for (var r in e) e.hasOwnProperty(r) && (t[n][r] = e[r])
                } else t[n].value = i.$viewValue
            }
            var l = 0,
                c = t.valueField || "value";
            angular.isArray(t.collection) || (t.collection = []);
            t.$watch("current", function (e, t) {
                if (e) {
                    i.$setViewValue(e);
                    l = a(e);
                    o(e, t) || s()
                }
            });
            i.$render = s;
            t.canIncrement = function () {
                return t.collection.length - 1 > l
            };
            t.increment = function () {
                if (t.canIncrement()) {
                    l++;
                    i.$setViewValue(t.collection[l])
                }
            };
            t.canDecrement = function () {
                return l > 0
            };
            t.decrement = function () {
                if (t.canDecrement()) {
                    l--;
                    i.$setViewValue(t.collection[l])
                }
            };
            e(n.contents())(t)
        }
    }
}]);

app.constant("datepickerConfig", {
    formatDay: "dd",
    formatMonth: "MMMM",
    formatYear: "yyyy",
    formatDayHeader: "EEE",
    formatDayTitle: "MMMM yyyy",
    formatMonthTitle: "yyyy",
    datepickerMode: "day",
    minMode: "day",
    maxMode: "year",
    showWeeks: !0,
    startingDay: 0,
    yearRange: 20,
    minDate: null,
    maxDate: null
}).controller("DatepickerController", ["$scope", "$attrs", "$parse", "$interpolate", "$timeout", "$log", "dateFilter", "datepickerConfig", function (e, t, n, r, i, a, o, s) {
    var l = this,
        c = {
            $setViewValue: angular.noop
        };
    this.modes = ["day", "month", "year"];
    angular.forEach(["formatDay", "formatMonth", "formatYear", "formatDayHeader", "formatDayTitle", "formatMonthTitle", "minMode", "maxMode", "showWeeks", "startingDay", "yearRange"], function (n, i) {
        l[n] = angular.isDefined(t[n]) ? 8 > i ? r(t[n])(e.$parent) : e.$parent.$eval(t[n]) : s[n]
    });
    angular.forEach(["minDate", "maxDate"], function (r) {
        t[r] ? e.$parent.$watch(n(t[r]), function (e) {
            l[r] = e ? new Date(e) : null;
            l.refreshView()
        }) : l[r] = s[r] ? new Date(s[r]) : null
    });
    e.datepickerMode = e.datepickerMode || s.datepickerMode;
    e.uniqueId = "custom-datepicker-" + e.$id + "-" + Math.floor(1e4 * Math.random());
    this.activeDate = angular.isDefined(t.initDate) ? e.$parent.$eval(t.initDate) : new Date;
    e.isActive = function (t) {
        if (0 === l.compare(t.date, l.activeDate)) {
            e.activeDateId = t.uid;
            return !0
        }
        return !1
    };
    this.init = function (e) {
        c = e;
        c.$render = function () {
            l.render()
        }
    };
    this.render = function () {
        if (c.$modelValue) {
            var e = new Date(c.$modelValue),
                t = !isNaN(e);
            t ? this.activeDate = e : a.error('Datepicker directive: "ng-model" value must be a Date object, a number of milliseconds since 01.01.1970 or a string representing an RFC2822 or ISO 8601 date.');
            c.$setValidity("date", t)
        }
        this.refreshView()
    };
    this.refreshView = function () {
        if (this.element) {
            this._refreshView();
            var e = c.$modelValue ? new Date(c.$modelValue) : null;
            c.$setValidity("date-disabled", !e || this.element && !this.isDisabled(e))
        }
    };
    this.createDateObject = function (e, t) {
        var n = c.$modelValue ? new Date(c.$modelValue) : null;
        return {
            date: e,
            label: o(e, t),
            selected: n && 0 === this.compare(e, n),
            disabled: this.isDisabled(e),
            current: 0 === this.compare(e, new Date),
            customClass: this.getCustomClass(e)
        }
    };
    this.getCustomClass = function (n) {
        return t.dateCustomClass && e.dateCustomClass({
            date: n
        })
    };
    this.isDisabled = function (n) {
        return this.minDate && 0 > this.compare(n, this.minDate) || this.maxDate && this.compare(n, this.maxDate) > 0 || t.dateDisabled && e.dateDisabled({
            date: n,
            mode: e.datepickerMode
        })
    };
    this.split = function (e, t) {
        for (var n = []; e.length > 0;) n.push(e.splice(0, t));
        return n
    };
    e.select = function (t) {
        if (e.datepickerMode === l.minMode) {
            var n = c.$modelValue ? new Date(c.$modelValue) : new Date(0, 0, 0, 0, 0, 0, 0);
            n.setFullYear(t.getFullYear(), t.getMonth(), t.getDate());
            c.$setViewValue(n);
            c.$render()
        } else {
            l.activeDate = t;
            e.datepickerMode = l.modes[l.modes.indexOf(e.datepickerMode) - 1]
        }
    };
    e.move = function (e) {
        var t = l.activeDate.getFullYear() + e * (l.step.years || 0),
            n = l.activeDate.getMonth() + e * (l.step.months || 0);
        l.activeDate.setFullYear(t, n, 1);
        l.refreshView()
    };
    e.toggleMode = function (t) {
        t = t || 1;
        e.datepickerMode === l.maxMode && 1 === t || e.datepickerMode === l.minMode && -1 === t || (e.datepickerMode = l.modes[l.modes.indexOf(e.datepickerMode) + t])
    };
    e.keys = {
        13: "enter",
        32: "space",
        33: "pageup",
        34: "pagedown",
        35: "end",
        36: "home",
        37: "left",
        38: "up",
        39: "right",
        40: "down"
    };
    var u = function () {
        i(function () {
            l.element[0].focus()
        }, 0, !1)
    };
    e.$on("custom-datepicker.focus", u);
    e.keydown = function (t) {
        var n = e.keys[t.which];
        if (n && !t.shiftKey && !t.altKey) {
            t.preventDefault();
            t.stopPropagation();
            if ("enter" === n || "space" === n) {
                if (l.isDisabled(l.activeDate)) return;
                e.select(l.activeDate);
                u()
            } else if (!t.ctrlKey || "up" !== n && "down" !== n) {
                l.handleKeyDown(n, t);
                l.refreshView()
            } else {
                e.toggleMode("up" === n ? 1 : -1);
                u()
            }
        }
    }
}]).directive("customDatepicker", [function () {
    return {
        restrict: "EA",
        replace: !0,
        templateUrl: "app/views/datepicker.html",
        scope: {
            datepickerMode: "=?",
            dateDisabled: "&",
            dateCustomClass: "&"
        },
        require: ["customDatepicker", "?^ngModel"],
        controller: "DatepickerController",
        link: function (e, t, n, r) {
            var i = r[0],
                a = r[1];
            a && i.init(a)
        }
    }
}]).directive("customDaypicker", ["dateFilter", "$filter", function (e, t) {
    return {
        restrict: "EA",
        replace: !0,
        templateUrl: "app/views/day.html",
        require: "^customDatepicker",
        link: function (n, r, i, a) {
            function o(e, t) {
                return 1 !== t || 0 !== e % 4 || 0 === e % 100 && 0 !== e % 400 ? c[t] : 29
            }

            function s(e, t) {
                var n = Array(t),
                    r = new Date(e),
                    i = 0;
                r.setHours(12);
                for (; t > i;) {
                    n[i++] = new Date(r);
                    r.setDate(r.getDate() + 1)
                }
                return n
            }

            function l(e) {
                var t = new Date(e);
                t.setDate(t.getDate() + 4 - (t.getDay() || 7));
                var n = t.getTime();
                t.setMonth(0);
                t.setDate(1);
                return Math.floor(Math.round((n - t) / 864e5) / 7) + 1
            }
            n.showWeeks = a.showWeeks;
            a.step = {
                months: 1
            };
            a.element = r;
            var c = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            a._refreshView = function () {
                var r = a.activeDate.getFullYear(),
                    i = a.activeDate.getMonth(),
                    o = new Date(r, i, 1),
                    c = a.startingDay - o.getDay(),
                    u = c > 0 ? 7 - c : -c,
                    d = new Date(o);
                u > 0 && d.setDate(-u + 1);
                for (var p = s(d, 42), h = 0; 42 > h; h++) {
                    p[h] = angular.extend(a.createDateObject(p[h], a.formatDay), {
                        secondary: p[h].getMonth() !== i,
                        uid: n.uniqueId + "-" + h
                    });
                    p[h].day = p[h].date.getDay();
                    p[h].weeknumber = l(p[h].date)
                }
                n.daypickerNavPrevDisabled = a.minDate && a.minDate > o;
                n.daypickerNavNextDisabled = a.maxDate && p[41].date > a.maxDate;
                var g = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
                n.labels = Array(7);
                for (var m = 0; 7 > m; m++) n.labels[m] = {
                    abbr: g[p[m].date.getDay()],
                    full: e(p[m].date, "EEEE")
                };
                n.title = t("date")(a.activeDate, "MM") + " " + t("date")(a.activeDate, "yyyy");
                n.rows = a.split(p, 7);
                if (n.showWeeks) {
                    n.weekNumbers = [];
                    for (var v = l(n.rows[0][0].date), $ = n.rows.length; $ > n.weekNumbers.push(v++) ;);
                }
            };
            a.compare = function (e, t) {
                return new Date(e.getFullYear(), e.getMonth(), e.getDate()) - new Date(t.getFullYear(), t.getMonth(), t.getDate())
            };
            a.handleKeyDown = function (e) {
                var t = a.activeDate.getDate();
                if ("left" === e) t -= 1;
                else if ("up" === e) t -= 7;
                else if ("right" === e) t += 1;
                else if ("down" === e) t += 7;
                else if ("pageup" === e || "pagedown" === e) {
                    var n = a.activeDate.getMonth() + ("pageup" === e ? -1 : 1);
                    a.activeDate.setMonth(n, 1);
                    t = Math.min(o(a.activeDate.getFullYear(), a.activeDate.getMonth()), t)
                } else "home" === e ? t = 1 : "end" === e && (t = o(a.activeDate.getFullYear(), a.activeDate.getMonth()));
                a.activeDate.setDate(t)
            };
            a.refreshView()
        }
    }
}]).directive("customMonthpicker", ["dateFilter", function (e) {
    return {
        restrict: "EA",
        replace: !0,
        templateUrl: "app/views/month.html",
        require: "^customDatepicker",
        link: function (e, t, n, r) {
            r.step = {
                years: 1
            };
            r.element = t;
            r._refreshView = function () {
                for (var t = Array(12), n = r.activeDate.getFullYear(), i = 0; 12 > i; i++) t[i] = angular.extend(r.createDateObject(new Date(n, i, 1), r.formatMonth), {
                    uid: e.uniqueId + "-" + i
                });
                e.title = data.months[$filter("date")(r.activeDate, "MM")] + " " + $filter("date")(r.activeDate, "yyyy");
                e.rows = r.split(t, 3)
            };
            r.compare = function (e, t) {
                return new Date(e.getFullYear(), e.getMonth()) - new Date(t.getFullYear(), t.getMonth())
            };
            r.handleKeyDown = function (e) {
                var t = r.activeDate.getMonth();
                if ("left" === e) t -= 1;
                else if ("up" === e) t -= 3;
                else if ("right" === e) t += 1;
                else if ("down" === e) t += 3;
                else if ("pageup" === e || "pagedown" === e) {
                    var n = r.activeDate.getFullYear() + ("pageup" === e ? -1 : 1);
                    r.activeDate.setFullYear(n)
                } else "home" === e ? t = 0 : "end" === e && (t = 11);
                r.activeDate.setMonth(t)
            };
            r.refreshView()
        }
    }
}]).directive("customYearpicker", ["dateFilter", function (e) {
    return {
        restrict: "EA",
        replace: !0,
        templateUrl: "app/views/year.html",
        require: "^customDatepicker",
        link: function (e, t, n, r) {
            function i(e) {
                return parseInt((e - 1) / a, 10) * a + 1
            }
            var a = r.yearRange;
            r.step = {
                years: a
            };
            r.element = t;
            r._refreshView = function () {
                for (var t = Array(a), n = 0, o = i(r.activeDate.getFullYear()) ; a > n; n++) t[n] = angular.extend(r.createDateObject(new Date(o + n, 0, 1), r.formatYear), {
                    uid: e.uniqueId + "-" + n
                });
                e.title = [t[0].label, t[a - 1].label].join(" - ");
                e.rows = r.split(t, 5)
            };
            r.compare = function (e, t) {
                return e.getFullYear() - t.getFullYear()
            };
            r.handleKeyDown = function (e) {
                var t = r.activeDate.getFullYear();
                "left" === e ? t -= 1 : "up" === e ? t -= 5 : "right" === e ? t += 1 : "down" === e ? t += 5 : "pageup" === e || "pagedown" === e ? t += ("pageup" === e ? -1 : 1) * r.step.years : "home" === e ? t = i(r.activeDate.getFullYear()) : "end" === e && (t = i(r.activeDate.getFullYear()) + a - 1);
                r.activeDate.setFullYear(t)
            };
            r.refreshView()
        }
    }
}])