
define(['../Javascript/AddView.js', '../Models/user'], function (AddView, user) {
    function start() {
        AddView.render();
        bindEvents();
    }

    function bindEvents() {
        document.getElementById('add').addEventListener('click', function () {
            var users = JSON.parse(localStorage.users);
            var userName = document.getElementById('user-name').value;

            users.push(new user(userName));
            localStorage.users = JSON.stringify(users);
            //require(['../Controllers/ListController'], function (ListController) {
            //    ListController.start();
            //});
            window.location.hash = '#list';

        }, false);
    }

    return {
        start: start
    };
});
