
require.config({
    paths: {
        jquery: 'jquery-1.8.3'
    }
});

require(['jquery'], function ($) {
    //alert($().jquery);
});

require(["mymath"], function (mymath) {
    console.log(mymath.add(1, 2));
});


require(['../Models/user', 'router'], function (user, router) {
    var users = [new user('Barney'),
        new user('Cartman'),
        new user('Sheldon')];

    localStorage.users = JSON.stringify(users);
    router.startRouting();
});




