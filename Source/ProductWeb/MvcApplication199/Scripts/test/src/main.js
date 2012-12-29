//define(function (require) {

//    // 得到 Spinning 函数类
//    var Spinning = require('./spinning')

//    // 初始化
//    var s = new Spinning('#container')
//    s.render()

//})

define(function (require) {
    var $ = require('jquery');
    var Spinning = require('./simple');
    //var s = Spinning.sum(1, 2);
    //$('#container').text(s);

    var s = Spinning.mode1.HelloMessage();
    $('#container').text(s);
});