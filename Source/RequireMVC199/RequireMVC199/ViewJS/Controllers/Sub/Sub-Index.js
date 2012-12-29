define(['../../../Scripts/common'], function (common) {
    //require(['app/main1']);
    alert('happy sub index js code');

    function test() {
        alert('test hello in sub');
    }

    test();

    return {
        test: test
    }
});

