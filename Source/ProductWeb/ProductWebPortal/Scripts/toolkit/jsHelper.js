


//构造html的通用方法
buildHTML = function (tag, html, attrs) {
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
};
