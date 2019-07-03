/*
 * jQuery addons
 */

$.fn.safeBind = function (type, func) {
    this.off(type).on(type, func);
};

if (!Array.prototype.last) {
    Array.prototype.last = function () {
        return this[this.length - 1];
    };
}