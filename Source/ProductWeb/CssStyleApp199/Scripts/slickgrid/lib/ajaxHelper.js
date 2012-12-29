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
//            "doJsonGet": doJsonGet,
//            "doJsonUpdate": doJsonUpdate,
//            "doJsonSave": doJsonSave,
//            "doJsonDelete": doJsonDelete
//        }
//    });

    

    /***
     * HttpGet获取服务端数据
     * @url 业务数据
     * @data
     */
    function doJsonGet(url, fn) {
        $.getJSON(url, fn);
    }

    /***
     * HttpPut更新数据到服务端
     * @url 业务数据
     * @data
     */
    function doJsonUpdate(url, data, fn) {
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
    function doJsonDelete(url, data, fn) {
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
    function doJsonSave(url, data, fn) {
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