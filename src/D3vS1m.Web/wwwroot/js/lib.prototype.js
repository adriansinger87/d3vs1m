/*
 * jQuery addons
 */

$.fn.safeBind = function (type, func) {
    this.off(type).on(type, func);
};