
//require(['../Models/User', '../Javascript/Controllers/Main/ListController',
//        '../Javascript/Controllers/Main/AddController'],
//        function (User, ListController, AddController) {
//    var users = [new User('Barney'),
//                new User('Cartman'),
//                new User('Sheldon')];

//    for (var i = 0, len = users.length; i < len; i++) {
//        console.log(users[i].name);
//    }

//    localStorage.users = JSON.stringify(users);

//    ListController.start();

//    AddController.start();
//});


require(['../Models/User', 'Router'], function (User, Router) {
    var users = [new User('Barney'),
             new User('Cartman'),
             new User('Sheldon')];

    localStorage.users = JSON.stringify(users);

    Router.startRouting();
});