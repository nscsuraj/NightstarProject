function full_width_container_dropped(containerId,parentId,objId,obj) {
    if (containerId === 'editor') {
        //alert(containerId);
        //alert(parentId);
        //alert(objId);

        var tmpl = document.getElementById('full_width_container_template').content.cloneNode(true);
        tmpl.querySelector('.fusion_full_width').id = parentId;
        $(tmpl.querySelector('.fusion_full_width')).attr("data-objId", objId);
        $(tmpl.querySelector('.fusion_full_width')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.fusion_full_width')).attr("data-objType", obj.Type);
        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;
        
        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Full Width Container";
        AssignObjectProperties(editorContent, obj);
//        add_object(objId, containerId, ElementTypeEnum.FullWidthContainer);
        //return objId;
    }
    //return null;
}

function grid_one_dropped(containerId, parentId, objId,obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_one_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_one_holder').id + Math.random();
        tmpl.querySelector('.grid_one_holder').id = parentId;

        $(tmpl.querySelector('.grid_one_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_one_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_one_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);

        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid One Container";
        AssignObjectProperties(editorContent, obj);

     //   add_object(objId, containerId, ElementTypeEnum.GridOne);
        //return objId;
    }
 ///   return null;
}

function grid_two_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_two_template').content.cloneNode(true);
       // var parentId = tmpl.querySelector('.grid_two_holder').id + Math.random();
        tmpl.querySelector('.grid_two_holder').id = parentId;
        $(tmpl.querySelector('.grid_two_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_two_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_two_holder')).attr("data-objType", obj.Type);
        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Two Container";
        AssignObjectProperties(editorContent, obj);

       // add_object(objId, containerId, ElementTypeEnum.GridTwo);
       // return objId;
    }
  //  return null;
}

function grid_three_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_three_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_three_holder').id + Math.random();
        tmpl.querySelector('.grid_three_holder').id = parentId;
        $(tmpl.querySelector('.grid_three_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_three_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_three_holder')).attr("data-objType", obj.Type);
        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Three Container";
        AssignObjectProperties(editorContent, obj);

      //  add_object(objId, containerId, ElementTypeEnum.GridThree);
      //  return objId;
    }
  //  return null;
}

function grid_two_third_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_two_third_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_two_third_holder').id + Math.random();
        tmpl.querySelector('.grid_two_third_holder').id = parentId;
        $(tmpl.querySelector('.grid_two_third_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_two_third_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_two_third_holder')).attr("data-objType", obj.Type);
        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Two Third Container";
        AssignObjectProperties(editorContent, obj);

      //  add_object(objId, containerId, ElementTypeEnum.GridTwoThird);
    //    return objId;
    }
  //  return null;
}

function grid_four_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_four_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_four_holder').id + Math.random();
        tmpl.querySelector('.grid_four_holder').id = parentId;

        $(tmpl.querySelector('.grid_four_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_four_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_four_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Four Container";
        AssignObjectProperties(editorContent, obj);

     //   add_object(objId, containerId, ElementTypeEnum.GridFour);
   //     return objId;
    }
 //   return null;
}

function grid_three_four_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_three_fourth_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_three_fourth_holder').id + Math.random();
        tmpl.querySelector('.grid_three_fourth_holder').id = parentId;

        $(tmpl.querySelector('.grid_three_fourth_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_three_fourth_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_three_fourth_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Three Fourth Container";
        AssignObjectProperties(editorContent, obj);

       // add_object(objId, containerId, ElementTypeEnum.GridThreeFourth);
     //   return objId;
    }
 //   return null;
}

function grid_five_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_five_template').content.cloneNode(true);
       // var parentId = tmpl.querySelector('.grid_five_holder').id + Math.random();
        tmpl.querySelector('.grid_five_holder').id = parentId;

        $(tmpl.querySelector('.grid_five_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_five_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_five_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Five Container";
        AssignObjectProperties(editorContent, obj);

     //   add_object(objId, containerId, ElementTypeEnum.GridFive);
    //    return objId;
    }
  //  return null;
}

function grid_two_fifth_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_two_fifth_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_two_fifth_holder').id + Math.random();
        tmpl.querySelector('.grid_two_fifth_holder').id = parentId;

        $(tmpl.querySelector('.grid_two_fifth_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_two_fifth_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_two_fifth_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Two Fifth Container";
        AssignObjectProperties(editorContent, obj);

    //    add_object(objId, containerId, ElementTypeEnum.GridTwoFifth);
      //  return objId;
    }
   // return null;
}

function grid_three_fifth_dropped(containerId, parentId, objId,obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_three_fifth_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_three_fifth_holder').id + Math.random();
        tmpl.querySelector('.grid_three_fifth_holder').id = parentId;

        $(tmpl.querySelector('.grid_three_fifth_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_three_fifth_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_three_fifth_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Three Fifth Container";
        AssignObjectProperties(editorContent, obj);

     //   add_object(objId, containerId, ElementTypeEnum.GridThreeFifth);
     //   return objId;
    }
  //  return null;
}

function grid_four_fifth_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_four_fifth_template').content.cloneNode(true);
       // var parentId = tmpl.querySelector('.grid_four_fifth_holder').id + Math.random();
        tmpl.querySelector('.grid_four_fifth_holder').id = parentId;

        $(tmpl.querySelector('.grid_four_fifth_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_four_fifth_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_four_fifth_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Four Fifth Container";
        AssignObjectProperties(editorContent, obj);

     //   add_object(objId, containerId, ElementTypeEnum.GridFourFifth);
      //  return objId;
    }
 //   return null;
}

function grid_one_six_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_six_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.grid_six_holder').id + Math.random();
        tmpl.querySelector('.grid_six_holder').id = parentId;

        $(tmpl.querySelector('.grid_six_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_six_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_six_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
      //  alert($(t).offset());
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid One Sixth Container";
        AssignObjectProperties(editorContent, obj);

     //   add_object(objId, containerId, ElementTypeEnum.GridSix);
     //   return objId;
    }
 //   return null;
} 

function grid_five_sixth_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('grid_') === -1) {
        var tmpl = document.getElementById('grid_five_sixth_template').content.cloneNode(true);
       // var parentId = tmpl.querySelector('.grid_five_sixth_holder').id + Math.random();
        tmpl.querySelector('.grid_five_sixth_holder').id = parentId;

        $(tmpl.querySelector('.grid_five_sixth_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.grid_five_sixth_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.grid_five_sixth_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        document.getElementById(containerId).appendChild(tmpl);

        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Grid Five Sixth Container";
        AssignObjectProperties(editorContent, obj);

       // add_object(objId, containerId, ElementTypeEnum.GridFiveSixth);
       // return objId;
    }
   // return null;
}