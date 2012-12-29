//define(['Views/ListView'], function (ListView) {

//    function start() {
//        var users = JSON.parse(localStorage.users);
//        ListView.render({ users: users });
//    }

//    return {
//        start: start
//    };
//});

define(['Controllers/Main/ListView'], function (ListView) {
    function start() {
        var users = JSON.parse(localStorage.users);
        ListView.render({ users: users });
    }

    return {
        start: start
    };
});