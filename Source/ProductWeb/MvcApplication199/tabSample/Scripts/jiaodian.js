
function SubShowClass(ID, eventType, defaultID, openClassName, closeClassName) {
    this.version = "1.21";
    this.author = "mengjia";
    this.parentObj = SubShowClass.$(ID);
    if (this.parentObj == null && ID != "none") {
        throw new Error("SubShowClass(ID) Parameter Error:ID Object Exist!(value:" + ID + ")")
    };

    if (!SubShowClass.childs) {
        SubShowClass.childs = []
    };

    this.ID = SubShowClass.childs.length;
    SubShowClass.childs.push(this);

    this.lock = false;
    this.label = [];
    this.defaultID = defaultID == null ? 0 : defaultID;
    this.selectedIndex = this.defaultID;
    this.openClassName = openClassName == null ? "selected" : openClassName;
    this.closeClassName = closeClassName == null ? "" : closeClassName;
    this.mouseIn = false;
    var mouseInFunc = Function("SubShowClass.childs[" + this.ID + "].mouseIn = true"),
        mouseOutFunc = Function("SubShowClass.childs[" + this.ID + "].mouseIn = false");

    if (ID != "none") {
        if (this.parentObj.attachEvent) {
            this.parentObj.attachEvent("onClick", mouseInFunc)
        } else {
            this.parentObj.addEventListener("mouseover", mouseInFunc, false)
        }
    };

    if (ID != "none") {
        if (this.parentObj.attachEvent) {
            this.parentObj.attachEvent("onmouseout", mouseOutFunc)
        } else {
            this.parentObj.addEventListener("mouseout", mouseOutFunc, false)
        }
    };

    if (typeof (eventType) != "string") {
        eventType = "onmousedown"
    };

    eventType = eventType.toLowerCase();
    switch (eventType) {
        case "onClick":
            this.eventType = "mouseover";
            break;
        case "onmouseout":
            this.eventType = "mouseout";
            break;
        case "onclick":
            this.eventType = "click";
            break;
        case "onmouseup":
            this.eventType = "mouseup";
            break;
        default:
            this.eventType = "mousedown"
    };

    this.addLabel = function (labelID, contID, parentBg, springEvent, blurEvent) {
        if (SubShowClass.$(labelID) == null && labelID != "none") {
            throw new Error("addLabel(labelID) Paramert Error:labelID Object Exist!(value:" + labelID + ")")
        };

        var TempID = this.label.length;
        if (parentBg == "") {
            parentBg = null
        };

        this.label.push([labelID, contID, parentBg, springEvent, blurEvent]);
        var tempFunc = Function('SubShowClass.childs[' + this.ID + '].select(' + TempID + ')');

        if (labelID != "none") {
            if (SubShowClass.$(labelID).attachEvent) {
                SubShowClass.$(labelID).attachEvent("on" + this.eventType, tempFunc)
            } else {
                SubShowClass.$(labelID).addEventListener(this.eventType, tempFunc, false)
            }
        };

        if (TempID == this.defaultID) {
            if (labelID != "none") {
                SubShowClass.$(labelID).className = this.openClassName
            };

            if (SubShowClass.$(contID)) {
                SubShowClass.$(contID).style.display = ""
            };

            if (ID != "none") {
                if (parentBg != null) {
                    this.parentObj.style.background = parentBg
                }
            };

            if (springEvent != null) {
                eval(springEvent)
            }
        } else {
            if (labelID != "none") {
                SubShowClass.$(labelID).className = this.closeClassName
            };

            if (SubShowClass.$(contID)) {
                SubShowClass.$(contID).style.display = "none"
            }
        };
        if (SubShowClass.$(contID)) {
            if (SubShowClass.$(contID).attachEvent) {
                SubShowClass.$(contID).attachEvent("onClick", mouseInFunc)
            } else {
                SubShowClass.$(contID).addEventListener("mouseover", mouseInFunc, false)
            };

            if (SubShowClass.$(contID).attachEvent) {
                SubShowClass.$(contID).attachEvent("onmouseout", mouseOutFunc)
            } else {
                SubShowClass.$(contID).addEventListener("mouseout", mouseOutFunc, false)
            }
        }
    };

    this.select = function (num, force) {
        if (typeof (num) != "number") {
            throw new Error("select(num) Parameter Error :num Not number Type!(value:" + num + ")")
        };

        if (force != true && this.selectedIndex == num) { return };

        var i;
        for (i = 0; i < this.label.length; i++) {
            if (i == num) {
                if (this.label[i][0] != "none") {
                    SubShowClass.$(this.label[i][0]).className = this.openClassName
                };

                if (SubShowClass.$(this.label[i][1])) {
                    SubShowClass.$(this.label[i][1]).style.display = ""
                };

                if (ID != "none") {
                    if (this.label[i][2] != null) {
                        this.parentObj.style.background = this.label[i][2]
                    }
                };

                if (this.label[i][3] != null) {
                    eval(this.label[i][3])
                }
            } else if (this.selectedIndex == i || force == true) {
                if (this.label[i][0] != "none") {
                    SubShowClass.$(this.label[i][0]).className = this.closeClassName
                };

                if (SubShowClass.$(this.label[i][1])) {
                    SubShowClass.$(this.label[i][1]).style.display = "none"
                };

                if (this.label[i][4] != null) { eval(this.label[i][4]) }
            }
        };

        this.selectedIndex = num
    };

    this.random = function () {
        if (arguments.length != this.label.length) {
            throw new Error("random() Parameter Error: Argument amout not matached label amout!(length:" + arguments.length + ")")
        };

        var sum = 0, i; for (i = 0; i < arguments.length; i++) {
            sum += arguments[i]
        };
        var randomNum = Math.random(), percent = 0;

        for (i = 0; i < arguments.length; i++) {
            percent += arguments[i] / sum;
            if (randomNum < percent) {
                this.select(i); break
            }
        }
    };

    this.autoPlay = false;
    var autoPlayTimeObj = null;
    this.spaceTime = 5000;

    this.play = function (spTime) {
        if (typeof (spTime) == "number") {
            this.spaceTime = spTime
        };

        clearInterval(autoPlayTimeObj);
        autoPlayTimeObj = setInterval("SubShowClass.childs[" + this.ID + "].autoPlayFunc()", this.spaceTime);
        this.autoPlay = true
    };

    this.autoPlayFunc = function () {
        if (this.autoPlay == false || this.mouseIn == true) { return };
        this.nextLabel()
    };

    this.nextLabel = function () {
        var index = this.selectedIndex; index++;

        if (index >= this.label.length) { index = 0 };
        this.select(index);

        if (this.autoPlay == true) {
            clearInterval(autoPlayTimeObj);
            autoPlayTimeObj = setInterval("SubShowClass.childs[" + this.ID + "].autoPlayFunc()", this.spaceTime)
        }
    };

    this.previousLabel = function () {
        var index = this.selectedIndex; index--;
        if (index < 0) {
            index = this.label.length - 1
        };

        this.select(index);

        if (this.autoPlay == true) {
            clearInterval(autoPlayTimeObj);
            autoPlayTimeObj = setInterval("SubShowClass.childs[" + this.ID + "].autoPlayFunc()", this.spaceTime)
        }
    };

    this.stop = function () {
        clearInterval(autoPlayTimeObj);
        this.autoPlay = false
    }
};

SubShowClass.$ = function (objName) {
    var div = $("#SubShow_01");
    //if (document.getElementById) {
    //    return eval('document.getElementById("' + objName + '")')
    //} else {
    //    return eval('document.all.' + objName)
    //}
    return div;
}

function PLabel(SubObjID, SubName) {
    var SubObj = SubShowClass.childs[SubObjID];

    SubObj.previousLabel();
    ChkArr(SubObjID, SubName);
}

function NLabel(SubObjID, SubName) {
    var SubObj = SubShowClass.childs[SubObjID];
    SubObj.nextLabel();
    ChkArr(SubObjID, SubName);
}

function ChkArr(SubObjID, SubName) {
    var SubObj = SubShowClass.childs[SubObjID];

    if (SubObj.selectedIndex == 0) {
        SubShowClass.$("SSArr_" + SubName + "_L").className = "arrLeft_d";
        SubShowClass.$("SSArr_" + SubName + "_L").onclick = null;
    } else {
        SubShowClass.$("SSArr_" + SubName + "_L").className = "arrLeft";
        SubShowClass.$("SSArr_" + SubName + "_L").onclick = Function("PLabel(" + SubObjID + ",'" + SubName + "')");
    };

    if (SubObj.selectedIndex >= SubObj.label.length - 1) {
        SubShowClass.$("SSArr_" + SubName + "_R").className = "arrRight_d";
        SubShowClass.$("SSArr_" + SubName + "_R").onclick = null;
    } else {
        SubShowClass.$("SSArr_" + SubName + "_R").className = "arrRight";
        SubShowClass.$("SSArr_" + SubName + "_R").onclick = Function("NLabel(" + SubObjID + ",'" + SubName + "')");
    };
}

try {
    document.execCommand('BackgroundImageCache', false, true);
} catch (e) {

}
//function SubShowClass(ID,eventType,defaultID,openClassName,closeClassName){this.version="1.21";this.author="mengjia";this.parentObj=SubShowClass.$(ID);if(this.parentObj==null&&ID!="none"){throw new Error("SubShowClass(ID)参数错误:ID 对像存在!(value:"+ID+")")};if(!SubShowClass.childs){SubShowClass.childs=[]};this.ID=SubShowClass.childs.length;SubShowClass.childs.push(this);this.lock=false;this.label=[];this.defaultID=defaultID==null?0:defaultID;this.selectedIndex=this.defaultID;this.openClassName=openClassName==null?"selected":openClassName;this.closeClassName=closeClassName==null?"":closeClassName;this.mouseIn=false;var mouseInFunc=Function("SubShowClass.childs["+this.ID+"].mouseIn = true"),mouseOutFunc=Function("SubShowClass.childs["+this.ID+"].mouseIn = false");if(ID!="none"){if(this.parentObj.attachEvent){this.parentObj.attachEvent("onClick",mouseInFunc)}else{this.parentObj.addEventListener("mouseover",mouseInFunc,false)}};if(ID!="none"){if(this.parentObj.attachEvent){this.parentObj.attachEvent("onmouseout",mouseOutFunc)}else{this.parentObj.addEventListener("mouseout",mouseOutFunc,false)}};if(typeof(eventType)!="string"){eventType="onmousedown"};eventType=eventType.toLowerCase();switch(eventType){case "onClick":this.eventType="mouseover";break;case "onmouseout":this.eventType="mouseout";break;case "onclick":this.eventType="click";break;case "onmouseup":this.eventType="mouseup";break;default:this.eventType="mousedown"};this.addLabel=function(labelID,contID,parentBg,springEvent,blurEvent){if(SubShowClass.$(labelID)==null&&labelID!="none"){throw new Error("addLabel(labelID)参数错误:labelID 对像存在!(value:"+labelID+")")};var TempID=this.label.length;if(parentBg==""){parentBg=null};this.label.push([labelID,contID,parentBg,springEvent,blurEvent]);var tempFunc=Function('SubShowClass.childs['+this.ID+'].select('+TempID+')');if(labelID!="none"){if(SubShowClass.$(labelID).attachEvent){SubShowClass.$(labelID).attachEvent("on"+this.eventType,tempFunc)}else{SubShowClass.$(labelID).addEventListener(this.eventType,tempFunc,false)}};if(TempID==this.defaultID){if(labelID!="none"){SubShowClass.$(labelID).className=this.openClassName};if(SubShowClass.$(contID)){SubShowClass.$(contID).style.display=""};if(ID!="none"){if(parentBg!=null){this.parentObj.style.background=parentBg}};if(springEvent!=null){eval(springEvent)}}else{if(labelID!="none"){SubShowClass.$(labelID).className=this.closeClassName};if(SubShowClass.$(contID)){SubShowClass.$(contID).style.display="none"}};if(SubShowClass.$(contID)){if(SubShowClass.$(contID).attachEvent){SubShowClass.$(contID).attachEvent("onClick",mouseInFunc)}else{SubShowClass.$(contID).addEventListener("mouseover",mouseInFunc,false)};if(SubShowClass.$(contID).attachEvent){SubShowClass.$(contID).attachEvent("onmouseout",mouseOutFunc)}else{SubShowClass.$(contID).addEventListener("mouseout",mouseOutFunc,false)}}};this.select=function(num,force){if(typeof(num)!="number"){throw new Error("select(num)参数错误:num 不是 number 类型!(value:"+num+")")};if(force!=true&&this.selectedIndex==num){return};var i;for(i=0;i<this.label.length;i++){if(i==num){if(this.label[i][0]!="none"){SubShowClass.$(this.label[i][0]).className=this.openClassName};if(SubShowClass.$(this.label[i][1])){SubShowClass.$(this.label[i][1]).style.display=""};if(ID!="none"){if(this.label[i][2]!=null){this.parentObj.style.background=this.label[i][2]}};if(this.label[i][3]!=null){eval(this.label[i][3])}}else if(this.selectedIndex==i||force==true){if(this.label[i][0]!="none"){SubShowClass.$(this.label[i][0]).className=this.closeClassName};if(SubShowClass.$(this.label[i][1])){SubShowClass.$(this.label[i][1]).style.display="none"};if(this.label[i][4]!=null){eval(this.label[i][4])}}};this.selectedIndex=num};this.random=function(){if(arguments.length!=this.label.length){throw new Error("random()参数错误:参数数量与标签数量不符!(length:"+arguments.length+")")};var sum=0,i;for(i=0;i<arguments.length;i++){sum+=arguments[i]};var randomNum=Math.random(),percent=0;for(i=0;i<arguments.length;i++){percent+=arguments[i]/sum;if(randomNum<percent){this.select(i);break}}};this.autoPlay=false;var autoPlayTimeObj=null;this.spaceTime=5000;this.play=function(spTime){if(typeof(spTime)=="number"){this.spaceTime=spTime};clearInterval(autoPlayTimeObj);autoPlayTimeObj=setInterval("SubShowClass.childs["+this.ID+"].autoPlayFunc()",this.spaceTime);this.autoPlay=true};this.autoPlayFunc=function(){if(this.autoPlay==false||this.mouseIn==true){return};this.nextLabel()};this.nextLabel=function(){var index=this.selectedIndex;index++;if(index>=this.label.length){index=0};this.select(index);if(this.autoPlay==true){clearInterval(autoPlayTimeObj);autoPlayTimeObj=setInterval("SubShowClass.childs["+this.ID+"].autoPlayFunc()",this.spaceTime)}};this.previousLabel=function(){var index=this.selectedIndex;index--;if(index<0){index=this.label.length-1};this.select(index);if(this.autoPlay==true){clearInterval(autoPlayTimeObj);autoPlayTimeObj=setInterval("SubShowClass.childs["+this.ID+"].autoPlayFunc()",this.spaceTime)}};this.stop=function(){clearInterval(autoPlayTimeObj);this.autoPlay=false}};SubShowClass.$=function(objName){if(document.getElementById){return eval('document.getElementById("'+objName+'")')}else{return eval('document.all.'+objName)}}
//function PLabel(SubObjID,SubName){
//	var SubObj = SubShowClass.childs[SubObjID];
//	SubObj.previousLabel();
//	ChkArr(SubObjID,SubName);
//}
//function NLabel(SubObjID,SubName){
//	var SubObj = SubShowClass.childs[SubObjID];
//	SubObj.nextLabel();
//	ChkArr(SubObjID,SubName);
//}
//function ChkArr(SubObjID,SubName){
//	var SubObj = SubShowClass.childs[SubObjID];
//	if(SubObj.selectedIndex == 0){
//		SubShowClass.$("SSArr_" + SubName + "_L").className = "arrLeft_d";
//		SubShowClass.$("SSArr_" + SubName + "_L").onclick = null;
//	}else{
//		SubShowClass.$("SSArr_" + SubName + "_L").className = "arrLeft";
//		SubShowClass.$("SSArr_" + SubName + "_L").onclick = Function("PLabel(" + SubObjID + ",'" + SubName + "')");
//	};
//	if(SubObj.selectedIndex >= SubObj.label.length - 1){
//		SubShowClass.$("SSArr_" + SubName + "_R").className = "arrRight_d";
//		SubShowClass.$("SSArr_" + SubName + "_R").onclick = null;
//	}else{
//		SubShowClass.$("SSArr_" + SubName + "_R").className = "arrRight";
//		SubShowClass.$("SSArr_" + SubName + "_R").onclick = Function("NLabel(" + SubObjID + ",'" + SubName + "')");
//	};
//}

//try{document.execCommand('BackgroundImageCache', false, true);}catch(e){}