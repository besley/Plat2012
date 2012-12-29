/*
 * AjaxHelper - A JavaScript Helper
 * 
 *
 * Copyright (c) 2012 TaoPlat
 * Dual licensed under the MIT (MIT-LICENSE.txt)
 * and GPL (GPL-LICENSE.txt) licenses.
 */

define(['jquery'], function () {
    /***
     * HttpGet获取服务端数据
     * @url 业务数据
     * @data
     */
    $.doHttpClientGet = function(url, fn) {
        $.getJSON(url, fn);
    }

    /***
     * HttpPut更新数据到服务端
     * @url 业务数据
     * @data
     */
    $.doHttpClientUpdate = function(url, data, fn) {
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
    $.doHttpClientDelete = function(url, data, fn) {
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
    $.doHttpClientSave = function(url, data, fn) {
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
    $.doAjaxGet = function(url, fn) {
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

    $.doAjaxPost = function(url, data, fn) {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            dataType: 'json',
            contentType: 'application/json',
            success: fn
        });
    }

    //构造html的通用方法
    $.buildHTML = function(tag, html, attrs) {
        // you can skip html param
        if (typeof (html) != 'string') {
            attrs = html;
            html = null;
        }
        var h = '<' + tag;
        for (attr in attrs) {
            if (attrs[attr] === false) continue;
            h += ' ' + attr + '="' + attrs[attr] + '"';
        }
        return h += html ? ">" + html + "</" + tag + ">" : "/>";
    }

    //构造JsTree的通用方法
    $.fn.buildJsTree = function (url, fn) {
        var object = require(['jstree'], function(){
            $.jstree._themes = "/PlatJS/Scripts/jstree/themes/";
            var myTree = $(this).jstree({
                "json_data": {
                    "ajax": {
                        "url": url,
                        "type": "GET",
                        "dataType": "json",
                        "contentType": "application/json charset=utf-8",
                        "success": fn
                    }
                },
                "plugins": ["themes", "json_data", "ui"]
            });
        })
    }
})

