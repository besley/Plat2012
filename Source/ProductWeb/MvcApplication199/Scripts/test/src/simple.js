
define(function (require, exports, module) {
    //var $ = require('jquery');

    function Spinning() {}

    Spinning.mode1 = (function (){
        return {
            HelloMessage: function(){
                return 'hello world';
            }
        }
    })()

    Spinning.mode2 = (function () {
        return {
            HelloMessage: function () {
                return 'hello wonderful';
            }
        }
    })()

    module.exports = Spinning;
    //exports.sum = function (x, y) {
    //    return x + y;
    //}
})