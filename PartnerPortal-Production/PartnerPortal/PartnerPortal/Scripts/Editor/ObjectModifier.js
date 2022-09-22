
var editorContent = {};
editorContent.Id = "editor";
editorContent.Type = "Editor";
editorContent.Children = [];

function initEditor() {
    editorContent = {};
    editorContent.Id = "editor";
    editorContent.Type = "Editor";
    editorContent.Children = [];
    $("#editor").html('');
}

function AddObjectbyId(rootObj, objToAdd) {
    if (rootObj.Id === objToAdd.ContainerId) {
        rootObj.Children.push(objToAdd);
    } else {
        {
            if (rootObj.Children != undefined) {
                for (var i = 0; i < rootObj.Children.length; i++) {
                    if (rootObj.Children[i].Id === parentId) {
                        if (rootObj.Children[i].Children != undefined) {
                            rootObj.Children[i].Children.push(objToAdd);
                        }
                    } else {
                        AddObjectbyId(parentId, rootObj.Children[i], objToAdd);
                    }
                }
            }
        }
    }
}

function AssignObjectProperties(rootObj, obj) {
    if (rootObj.Id === obj.Id) {
        rootObj = obj;
    } else {
        {
            if (rootObj.Children != undefined) {
                for (var i = 0; i < rootObj.Children.length; i++) {
                    if (rootObj.Children[i].Id === obj.Id) {
                        
                        rootObj.Children[i] = obj;
                        
                    } else {
                        AssignObjectProperties(rootObj.Children[i], obj);
                    }
                }
            }
        }
    }
}

function DeleteObjectbyId(objId, rootObj) {
    if (rootObj.Id === objId) {
        rootObj= {};
    } else {
        {
            if (rootObj.Children != undefined) {
                for (var i = 0; i < rootObj.Children.length; i++) {
                    if (rootObj.Children[i].Id === objId) {
                        var objs = $.grep(rootObj.Children, function (x) {
                            return !(x.Id === objId);
                        });
                        rootObj.Children = objs;
                    } else {
                        DeleteObjectbyId(objId, rootObj.Children[i]);
                    }
                }
            }
        }
    }
}

//function GetObjectbyId(objId, rootObj) {
//    if (rootObj.Id === objId) {
//        return rootObj;
//    } else {
//        {
//            var tempObj = null;
//            if (rootObj.Children != undefined) {
//                for (var i = 0; i < rootObj.Children.length; i++) {
//                    if (rootObj.Children[i].Id === objId) {
//                        tempObj = rootObj.Children[i];
//                        return tempObj;
//                    } else {
//                        tempObj = GetObjectbyId(objId, rootObj.Children[i]);
//                    }
//                }
//            }
//            return tempObj;
//        }
//    }
//}

function GetObjectbyId(matchingTitle, element) {
   // alert(element.Id);
    if (element.Id === matchingTitle) {
        
        return element;
    } else if (element.Children != null) {
        var result = null;
        for (var i = 0; result == null && i < element.Children.length; i++) {
            result = GetObjectbyId(matchingTitle, element.Children[i]);
        }
        return result;
    }
    return null;
}

function PrepareObjectForCloning(obj, containerId) {
    obj.ParentId = obj.ParentId.replace(/[0-9]/g, "") + "_" + Math.random();
    obj.Id = obj.Id.replace(/[0-9]/g, "") + "_" + Math.random();
    obj.ContainerId = containerId;
    obj.DropX = null;
    obj.DropY = null;
    for (var i = 0; i < obj.Children.length; i++) {
        PrepareObjectForCloning(obj.Children[i], obj.Id);
    }
    return obj;
}


function parseFullObject() {
    $("#editor").empty();
    for (var i = 0; i < editorContent.Children.length; i++) {
        parseObject(editorContent.Children[i]);
    }
}

function parseObject(obj) {
   objectToElementParser(obj);
//    alert(obj.Id);
  //  alert(obj.Type);
    if (obj.Children != undefined) {
        for (var i = 0; i < obj.Children.length; i++) {
           // obj.Children[i].ContainerId = conId;
            parseObject(obj.Children[i]);
        }
    }
}

function validate_custom_object(ContainerId, customObj) {
    var isValid = false;
    for (var i = 0; i < customObj.length; i++) {
        if (ContainerId.indexOf("editor") >= 0) {
           // alert(1);
            isValid = true;
        } else if (ContainerId.indexOf("full_width_container") >= 0) {
            //alert(2);
            //alert(customObj[i].Type);
            //alert(customObj[i].Type.indexOf("fullwidth"));
            if (customObj[i].Type.indexOf("fullwidth") >= 0) {
              //  alert(2.1);
                return false;
            } else {
             //   alert(2.2);
                isValid = true;
            }
        } else if (ContainerId.indexOf("grid") >= 0) {
           // alert(3);
            if (customObj[i].Type.indexOf("fullwidth") >= 0) {
                //isValid = false;
                return false;
            } else if (customObj[i].Type.indexOf("grid") >= 0) {
                return false;
                //isValid = false;
            } else {
                isValid = true;
            }
        }
    }
    return isValid;
}

function process_custom_object(ContainerId, dropX, dropY,customObj) {
    //get the position where to insert
    //-1 is the last position
    if (ContainerId == "") {
        return false;
    }
    if (!validate_custom_object(ContainerId, customObj)) {
        alert("Template can not be inserted at that position");
        return false;
    }
    
    //get insertion point in container
    var index = get_object_insertion_position(ContainerId, dropX, dropY);
    if (index != -1) {
        var container = GetObjectbyId(ContainerId, editorContent);
        for (var i = 0; i < customObj.length; i++) {
           
            var obj = JSON.parse(JSON.stringify(customObj[i]));

            //obj.ParentId = customObj[i].ParentId;
            //obj.Id = customObj[i].Id;
            //obj.Type = customObj[i].Type;
            obj.ContainerId = ContainerId;
            obj.Children = [];
            obj.DropX = null;
            obj.DropY = null;
            container.Children.splice(index, 0, obj);
            AssignObject(container, editorContent);
            if (customObj[i].Children.length > 0) {
                add_custom_object(obj.Id, customObj[i].Children);
            }
            index = index + 1;
        }

        parseFullObject();
        return true;

    }
    // alert(result);
    return false;
}

function add_custom_object(ContainerId, customObj) {
    var container = GetObjectbyId(ContainerId, editorContent);
    var index = 0;
    for (var i = 0; i < customObj.length; i++) {
        var obj = JSON.parse(JSON.stringify(customObj[i]));

        //obj.ParentId = customObj[i].ParentId;
        //obj.Id = customObj[i].Id;
        //obj.Type = customObj[i].Type;
        obj.ContainerId = ContainerId;
        obj.Children = [];
        obj.DropX = null;
        obj.DropY = null;
        container.Children.splice(index, 0, obj);
        AssignObject(container, editorContent);
        if (customObj[i].Children.length > 0) {
            add_custom_object(obj.Id, customObj[i].Children);
        }
        index = index + 1;
        
    }
}

function get_object_insertion_position(ContainerId, dropX, dropY) {
    var container = GetObjectbyId(ContainerId, editorContent);
    var result = 0;
    if (container.Children != undefined && container.Children.length === 0) //first object
    {
        //container.Children.push(obj);
        result = 0;
    }
    else if (dropX === undefined || dropX === null) {
//        container.Children.push(obj);
        result = container.Children.length;
    }
    else {
        var prevObjs = [];
        for (var i = 0; i < container.Children.length; i++) {
            if (document.getElementById(container.Children[i].ParentId) == undefined) {
                return -1;
            }
            var eleHeight = document.getElementById(container.Children[i].ParentId).offsetHeight;
            if (container.Children[i].OffsetX < dropX) {
                //X is lower
                if ((container.Children[i].OffsetY) < dropY) {
                    prevObjs.push(container.Children[i]);
                }
            } else {
                if ((container.Children[i].OffsetY + eleHeight) < dropY) {
                    prevObjs.push(container.Children[i]);
                } else {
                    break;
                }
            }
            //alert(prevObjs.length);
        }

        //container.Children.splice(prevObjs.length, 0, obj);
        result = prevObjs.length;
    }
    return result;
}

function add_object(elementId, obj) {
   // alert(obj.ContainerId === "editor");
    if (obj.ContainerId == "") {
        return false;
    }
    if (obj.Type.indexOf("container") > 0) {
        if (obj.ContainerId != "editor" && obj.ContainerId.indexOf("full_width_container") === -1) {
            return false;
        }
    }
    var container = GetObjectbyId(obj.ContainerId, editorContent);
    //alert(container.Children[0].ParentId);
    var result = false;
    //alert(obj.DropX);
    //alert(obj.DropY);
    if (container.Children != undefined && container.Children.length === 0) //first object
    {
        container.Children.push(obj);
        result = true;
    }
    else if (obj.DropX === undefined || obj.DropX === null) {
        container.Children.push(obj);
        result = true;
    }
    else {
        var prevObjs = [];
        for (var i = 0; i < container.Children.length; i++) {
            if (document.getElementById(container.Children[i].ParentId) != undefined) {

                var eleHeight = document.getElementById(container.Children[i].ParentId).offsetHeight;
                if (container.Children[i].OffsetX < obj.DropX) {
                    //X is lower
                    //   alert("X" + i);
                    if ((container.Children[i].OffsetY) < obj.DropY) {
                        //   alert("XX" + i);
                        prevObjs.push(container.Children[i]);
                    }
                    //prevObjs.push(container.Children[i]);
                } else {
                    //  alert("Y" + i);
                    // alert((container.Children[i].OffsetY + eleHeight));
                    if ((container.Children[i].OffsetY + eleHeight) < obj.DropY) {
                        //  alert("YY" + i);
                        prevObjs.push(container.Children[i]);
                    } else {
                        break;
                    }
                }
            }
            //alert(prevObjs.length);
        }

        container.Children.splice(prevObjs.length, 0, obj);
        result = true;
    }

    AssignObject(container, editorContent);
    parseFullObject();
    // alert(result);
    return result;
}

function AssignObject(objToAdd, rootObj) {
    if (rootObj.Id === objToAdd.Id) {
        rootObj = objToAdd;
    } else {
        {
            if (rootObj.Children != undefined) {
                for (var i = 0; i < rootObj.Children.length; i++) {
                    //if (rootObj.Children[i].Id === parentId) {
                    //    if (rootObj.Children[i].Children != undefined) {
                    //        rootObj.Children[i].Children.push(objToAdd);
                    //    }
                    //} else {
                    AssignObject(objToAdd, rootObj.Children[i]);
                    //  }
                }
            }
        }
    }
}

//elementId for method to call while rendering
//function add_object(elementId, obj) {
//    //  alert(parentId);
// //   alert("Start Calling ---->" + obj.ContainerId);
//    var container = GetObjectbyId(obj.ContainerId, editorContent);
//    var result = false;
//    //alert(container);
//    //alert(obj.DropX);
//    //alert(obj.DropY);
//    if (container.Children != undefined && container.Children.length === 0) //first object
//    {
//        alert(1);
//        container.Children.push(obj);
//        result = true;
//    }
//    else if (obj.DropX === undefined || obj.DropX === null) {
//        alert(10);
//        container.Children.push(obj);
//        result = true;
//    }
//    else {
//        var index = 0;
//        //var objsWithLowerY = GetObjectsWithLowerY(obj.DropY + (obj.OuterHeight == undefined ? 0 : obj.OuterHeight), container);
//       // alert(obj.DropY);
//        var objsWithLowerY = GetObjectsWithLowerY(obj.DropY, container);
//     //   alert(objsWithLowerY.length);
//      //  alert(JSON.stringify(objsWithLowerY));
//        if (objsWithLowerY.length === 0) {//no element with lower Y. Should be inserted at first position.
//            var objsWithLowerX = GetObjectsWithLowerX(obj.DropX, container);
//            alert(objsWithLowerX.length);
//            //alert(1);
//            alert(obj.DropX);
//            if (objsWithLowerX.length === 0) {
//                alert(2);
//                container.Children.splice(index, 0, obj);
//                result = true;
//            } else {
//                //objsWithLowerX = objsWithLowerX.sort(function(a,b) 
//                //{
//                //    return a.OffsetX < b.OffsetX;
//                //});
//                index = objsWithLowerX.length;
//                container.Children.splice(index, 0, obj);
//                result = true;
//            }

//        } else {
//            //alert(JSON.stringify(objsWithLowerY));
//            //alert(obj.DropY);
//            alert(objsWithLowerY.length);
//            //objsWithLowerY.sort(function(a,b)
//            //{
//            //    return a.OffsetY > b.OffsetY ? 1 : (a.OffsetY < b.OffsetY ? -1 : 0);
//            //});

//            //Identify whether dropped in horizontal line gap or in between two objects


//            var y = objsWithLowerY[objsWithLowerY.length - 1].OffsetY + objsWithLowerY[objsWithLowerY.length - 1].OuterHeight;
            
//            if (obj.DropY > y) {
//                alert(0);
//                //dropped in the horizontal line gap
//                alert(obj.DropX);
//                alert(obj.DropY);
//                //alert("going");
//                objsWithLowerX = GetObjectsWithLowerX(obj.DropX, container,obj.DropY);
                
//                alert("X : " + objsWithLowerX.length);
//                //alert(1);
//                //alert(obj.DropX);
//                if (objsWithLowerX.length === 0) {
//                    alert(2);
//                    index = objsWithLowerY.length;
//                    container.Children.splice(index, 0, obj);
//                    result = true;
//                } else {
//                    alert(container.Children.length);
//                    var prevObj = objsWithLowerX[objsWithLowerX.length - 1];
//                    alert("P ->" + prevObj.Id);
//                    index = 1;
//                    for (var i = 0; i < container.Children.length; i++) {
//                        alert(container.Children[i].Id);
//                        if (prevObj.Id != container.Children[i].Id) {
//                            index = index + 1;
//                        } else {
//                            break;
//                        }
//                    }

//                    //index = objsWithLowerX.length;
//                    alert("Index: " + index);
//                    container.Children.splice(index, 0, obj);
//                    result = true;
//                }
//                //index = objsWithLowerY.length;
//                //container.Children.splice(index, 0, obj);
//                //result = true;
//            } else {
//                //dropped in between two objects
//                //Find last row
//                var tempObjs = [];
//                var prevY = objsWithLowerY[objsWithLowerY.length - 1].OffsetY;
//                for (var i = objsWithLowerY.length - 1; i >= 0; i--) {
//                    if (objsWithLowerY[i].OffsetX > obj.DropX && objsWithLowerY[i].OffsetY === prevY) {
//                        tempObjs.push(objsWithLowerY[i]);
//                    }
//                }
//                index = objsWithLowerY.length - tempObjs.length;
//                container.Children.splice(index, 0, obj);
//                result = true;
//            }
            
//            //for (var i = 0; i < objsWithLowerY.length; i++) {
//            //    alert(objsWithLowerY[i].OffsetY);
//            //    alert(objsWithLowerY[i].OffsetY + objsWithLowerY[i].OuterHeight);
//            //}

//          //  container.Children.push(obj);
//        }
//    }

//    AssignObject(container, editorContent);
//    parseFullObject();
//   // alert(result);
//    return result;
//}

//function GetObjectsWithLowerY(objY, container) {
//    var objs = [];
//    if (container.Children != undefined) {
//        for (var i = 0; i < container.Children.length; i++) {
//            if (container.Children[i].OffsetY + container.Children[i].OuterHeight <= objY) {
//                objs.push(container.Children[i]);
//            }
//        }
//    }
//    return objs;
//}

//function GetObjectsWithLowerX(objX, container,YtoDetermine) {
//    var objs = [];
//    if (container.Children != undefined) {
//        for (var i = 0; i < container.Children.length; i++) {
//            if (container.Children[i].OffsetX <= objX) {
//                if ((YtoDetermine != undefined)) {
//                    //alert("K =" + i);
//                    //alert((container.Children[i].OffsetY + container.Children[i].OuterHeight));
//                    //alert(YtoDetermine);
//                    if ((container.Children[i].OffsetY + container.Children[i].OuterHeight) <= YtoDetermine) {
//                      //  alert("K =" + container.Children[i].Id);
//                        objs.push(container.Children[i]);
//                    }
//                } else {
//                    objs.push(container.Children[i]);
//                }
//            }
//        }
//    }
//    return objs;
//}