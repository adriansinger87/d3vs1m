/*
 * jQuery addons
 */

$.fn.safeBind = function (type, func) {
    this.off(type).on(type, func);
};

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

if (!Array.prototype.last) {
    Array.prototype.last = function () {
        return this[this.length - 1];
    };
}