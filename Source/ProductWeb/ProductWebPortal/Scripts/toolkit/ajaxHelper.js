/*
 * AjaxHelper - A JavaScript Helper
 * 
 *
 * Copyright (c) 2012 TaoPlat
 * Dual licensed under the MIT (MIT-LICENSE.txt)
 * and GPL (GPL-LICENSE.txt) licenses.
 */

//(function ($) {
//    $.extend(true, window, {
//        'ajaxHelper': {
//            "doAjaxGet": doAjaxGet,
//            "doHttpClientUpdate": doHttpClientUpdate,
//            "doHttpClientSave": doHttpClientSave,
//            "doHttpClientDelete": doHttpClientDelete
//        }
//    });

    

    /***
     * HttpGet获取服务端数据
     * @url 业务数据
     * @data
     */
    function doHttpClientGet(url, fn) {
        $.getJSON(url, fn);
    }

    /***
     * HttpPut更新数据到服务端
     * @url 业务数据
     * @data
     */
    function doHttpClientUpdate(url, data, fn) {
        $.ajax({
            url: url,
            type: 'PUT',
            data: data,
            dataType: 'json',
            contentType: 'application/json',
            success: fn
        });
    }

    /***
     * HttpDelete删除数据
     * @url 业务数据
     * @data
     */
    function doHttpClientDelete(url, data, fn) {
        $.ajax({
            url: url,
            type: 'DELETE',
            data: data,
            dataType: 'json',
            contentType: 'application/json',
            success: fn
        });
    }

    /***
     * HttpPost保存数据
     * @url 业务数据
     * @data
     */
    function doHttpClientSave(url, data, fn) {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            dataType: 'json',
            contentType: 'application/json',
            success: fn
        });
    }

    /***
     * ajax获取服务端数据
     * @url 业务数据
     * @data
     */
    function doAjaxGet(url, fn) {
        //$.getJSON(url, fn);
        $.ajax({
            url: url,
            type: "GET",
            dataType: 'json',
            //data: data,
            contentType: 'application/json',
            success: fn
        });
    }

    function doAjaxPost(url, data, fn) {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            dataType: 'json',
            contentType: 'application/json',
            success: fn
        });
    }

//})(jQuery);