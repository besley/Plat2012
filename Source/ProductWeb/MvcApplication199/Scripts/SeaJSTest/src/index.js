
define('index', function (require, exports) {
    var msgconsole = require('simple');

    exports.log = function () {
        msgconsole.log('how are you!');
    };
});
