function element_image_frame_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('image_frame_template').content.cloneNode(true);
       // var parentId = tmpl.querySelector('.image_holder').id + Math.random();
        tmpl.querySelector('.image_holder').id = parentId;

        $(tmpl.querySelector('.image_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.image_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.image_holder')).attr("data-objType", obj.Type);

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
        obj.EditModalTitle = "Image Frame";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.ImageFrame);
        //return objId;
    }
    //return null;
}

function element_button_block_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('button_block_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.button_holder').id + Math.random();
        tmpl.querySelector('.button_holder').id = parentId;
        $(tmpl.querySelector('.button_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.button_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.button_holder')).attr("data-objType", obj.Type);
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

        if (obj[385] != undefined) {
            $(tmpl.querySelector('.textboxContent')).html(obj[385]);
            var st = $(tmpl.querySelector('.textboxContent')).text();
            if (st.length > 36) {
                $(tmpl.querySelector('.textboxContent')).text(st.substring(0, 36));
            } else {
                $(tmpl.querySelector('.textboxContent')).text(st);
            }
        }
        if (obj[379] != undefined) {
            $(tmpl.querySelector('.button')).addClass(obj[379]);
            }
        
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Button Block";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.ButtonBlock);
        //return objId;
    }
    //return null;
}

function element_button_with_icon_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('button_with_icon_template').content.cloneNode(true);
        tmpl.querySelector('.button_holder').id = parentId;
        $(tmpl.querySelector('.button_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.button_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.button_holder')).attr("data-objType", obj.Type);
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

        if (obj[672] != undefined) {
            $(tmpl.querySelector('.textboxContent')).html(obj[672]);
            var st = $(tmpl.querySelector('.textboxContent')).text();
            if (st.length > 36) {
                $(tmpl.querySelector('.textboxContent')).text(st.substring(0, 36));
            } else {
                $(tmpl.querySelector('.textboxContent')).text(st);
            }
        }
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Button With Icon";
        AssignObjectProperties(editorContent, obj);
    }
}

function element_google_map_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('google_map_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.google_map_holder').id + Math.random();
        tmpl.querySelector('.google_map_holder').id = parentId;
        $(tmpl.querySelector('.google_map_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.google_map_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.google_map_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "Google Map";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.GoogleMap);
        //return objId;
    }
    //return null;
}
function element_back_to_top_block_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('back_to_top_block_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.menu_anchor_holder').id + Math.random();
        tmpl.querySelector('.back_to_top_block_holder').id = parentId;
        $(tmpl.querySelector('.back_to_top_block_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.back_to_top_block_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.back_to_top_block_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "Back To Top";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.MenuAnchor);
        //return objId;
    }
    //return null;
}
function element_menu_anchor_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('menu_anchor_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.menu_anchor_holder').id + Math.random();
        tmpl.querySelector('.menu_anchor_holder').id = parentId;
        $(tmpl.querySelector('.menu_anchor_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.menu_anchor_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.menu_anchor_holder')).attr("data-objType", obj.Type);
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

        if (obj[446] != undefined) {
            $(tmpl.querySelector(".anchor_name")).html(obj[446]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Menu Anchor";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.MenuAnchor);
        //return objId;
    }
    //return null;
}

function element_check_list_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('check_list_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.check_list_holder').id + Math.random();
        tmpl.querySelector('.check_list_holder').id = parentId;
        $(tmpl.querySelector('.check_list_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.check_list_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.check_list_holder')).attr("data-objType", obj.Type);
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

        var icons = '';
        var display = '';
        if (obj[622] != undefined) {
            for (var i = 0 ; i < obj[622].length ; i++) {
                if (obj[622][i][86] != undefined) {
                    if (obj[622][i][86] != '') {
                        icons = "<i class='fa " + obj[622][i][86] + "'></i>";
                    }
                    else {
                        icons = "<i class='fa " + obj[404] + "'></i>";
                    }
                }
                if (obj[622][i][87] != undefined) {
                    display += "<li>" + icons + obj[622][i][87].replace(/(<([^>]+)>)/ig,"") + "</li>";
                }
            }
            $(tmpl.querySelector(".checklist_elements")).html(display);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Check List";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.CheckList);
        //return objId;
    }
    //return null;
}

function element_modal_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('modal_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.modal_holder').id + Math.random();
        tmpl.querySelector('.modal_holder').id = parentId;
        $(tmpl.querySelector('.modal_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.modal_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.modal_holder')).attr("data-objType", obj.Type);
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

        if (obj[623] != undefined) {
            $(tmpl.querySelector('.modal_name')).html(obj[623]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Modal";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.Modal);
        //return objId;
    }
    //return null;
}

function element_content_boxes_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('content_boxes_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.content_boxes_holder').id + Math.random();
        tmpl.querySelector('.content_boxes_holder').id = parentId;
        $(tmpl.querySelector('.content_boxes_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.content_boxes_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.content_boxes_holder')).attr("data-objType", obj.Type);
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

        if (obj[412] != undefined) {
            $(tmpl.querySelector('.content_boxes_layout')).html(obj[412]);
        }
        if (obj[417] != undefined) {
            $(tmpl.querySelector('.content_boxes_columns')).html(obj[417]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Content Box";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.ContentBoxes);
        //return objId;
    }
    //return null;
}

function element_table_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('table_box_template').content.cloneNode(true);
      //  var parentId = tmpl.querySelector('.table_box_holder').id + Math.random();
        tmpl.querySelector('.table_box_holder').id = parentId;

        $(tmpl.querySelector('.table_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.table_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.table_box_holder')).attr("data-objType", obj.Type);

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

        if (obj[554] != undefined) {
            $(tmpl.querySelector('.table_style')).html(obj[554]);
        }

        if (obj[555] != undefined) {
            $(tmpl.querySelector('.table_columns')).html(obj[555]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Table Box";
        AssignObjectProperties(editorContent, obj);
     //   add_object(objId, containerId, ElementTypeEnum.TableBox);
    }
    
}

function element_page_scroller_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('page_scroller_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.tabs_box_holder').id + Math.random();
        tmpl.querySelector('.page_scroller_holder').id = parentId;
        $(tmpl.querySelector('.page_scroller_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.page_scroller_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.page_scroller_holder')).attr("data-objType", obj.Type);
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

        //if (obj[72] != undefined) {
        //    $(tmpl.querySelector('.tabs_elements')).html(obj[72]);
        //}
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Page Scroller";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.TabsBox);
        //return objId;
    }
    //return null;
}

function element_tabs_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('tabs_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.tabs_box_holder').id + Math.random();
        tmpl.querySelector('.tabs_box_holder').id = parentId;
        $(tmpl.querySelector('.tabs_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.tabs_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.tabs_box_holder')).attr("data-objType", obj.Type);
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

        if (obj[72] != undefined) {
            $(tmpl.querySelector('.tabs_elements')).html(obj[72]);
        }
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Tab Box";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.TabsBox);
        //return objId;
    }
    //return null;
}

function element_separator_element_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('separator_element_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.separator_element_holder').id + Math.random();
        tmpl.querySelector('.separator_element_holder').id = parentId;
        $(tmpl.querySelector('.separator_element_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.separator_element_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.separator_element_holder')).attr("data-objType", obj.Type);
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

        if (obj[478] != undefined) {
            $(tmpl.querySelector('.separator')).addClass(obj[478]);
        }
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Separator";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.Separator);
        //return objId;
    }
    //return null;
}

function element_progress_bar_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('progress_bar_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.progress_bar_holder').id + Math.random();
        tmpl.querySelector('.progress_bar_holder').id = parentId;
        $(tmpl.querySelector('.progress_bar_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.progress_bar_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.progress_bar_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "Progress Bar";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.ProgressBar);
        //return objId;
    }
    //return null;
}

function element_text_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('text_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.text_box_holder').id + Math.random();
        tmpl.querySelector('.text_box_holder').id = parentId;
        $(tmpl.querySelector('.text_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.text_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.text_box_holder')).attr("data-objType", obj.Type);
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

        if (obj[353] != undefined) {
            $(tmpl.querySelector('.textboxContent')).html(obj[353]);
            var st = $(tmpl.querySelector('.textboxContent')).text();
            if (st.length > 36) {
                $(tmpl.querySelector('.textboxContent')).text(st.substring(0, 36));
            } else {
                $(tmpl.querySelector('.textboxContent')).text(st);
            }
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Text Box";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.TextBox);
        //return objId;
    }
    //return null;
}

function element_toggles_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('toggles_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.toggles_box_holder').id + Math.random();
        tmpl.querySelector('.toggles_box_holder').id = parentId;
        $(tmpl.querySelector('.toggles_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.toggles_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.toggles_box_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "Toggle Box";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.Toggles);
        //return objId;
    }
    //return null;
}

function element_title_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('title_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.title_box_holder').id + Math.random();
        tmpl.querySelector('.title_box_holder').id = parentId;
        $(tmpl.querySelector('.title_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.title_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.title_box_holder')).attr("data-objType", obj.Type);
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

        if (obj[600] != undefined) {
            $(tmpl.querySelector('.title_text')).html(obj[600]);
            var st = $(tmpl.querySelector('.title_text')).text();
            if (st.length > 36) {
                $(tmpl.querySelector('.title_text')).text(st.substring(0, 36));
            } else {
                $(tmpl.querySelector('.title_text')).text(st);
            }
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Title Box";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.Title);
        //return objId;
    }
    //return null;
}

function element_video_youtube_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('video_youtube_template').content.cloneNode(true);
        //alert(containerId);
        //var parentId = tmpl.querySelector('.video_youtube_holder').id + Math.random();
        tmpl.querySelector('.video_youtube_holder').id = parentId;
        $(tmpl.querySelector('.video_youtube_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.video_youtube_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.video_youtube_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "Youtube Video";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.VideoYoutube);
        //return objId;
    }
    //return null;
}

function element_video_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('video_template').content.cloneNode(true);
        //alert(containerId);
        //var parentId = tmpl.querySelector('.video_holder').id + Math.random();
        tmpl.querySelector('.video_holder').id = parentId;
        $(tmpl.querySelector('.video_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.video_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.video_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "Video";
        AssignObjectProperties(editorContent, obj);
    }
}

function element_alert_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('alert_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.alert_box_holder').id + Math.random();
        tmpl.querySelector('.alert_box_holder').id = parentId;

        $(tmpl.querySelector('.alert_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.alert_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.alert_box_holder')).attr("data-objType", obj.Type);

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

        if (obj[347] != undefined) {
            $(tmpl.querySelector('.textboxContent')).html(obj[347]);
            var st = $(tmpl.querySelector('.textboxContent')).text();
            if (st.length > 36) {
                $(tmpl.querySelector('.textboxContent')).text(st.substring(0, 36));
            } else {
                $(tmpl.querySelector('.textboxContent')).text(st);
            }
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Alert Box";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.Alert);
        //return objId;
    }
    //return null;
}

function element_wp_blog_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('wp_blog_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.wp_blog_holder').id + Math.random();
        tmpl.querySelector('.wp_blog_holder').id = parentId;
        $(tmpl.querySelector('.wp_blog_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.wp_blog_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.wp_blog_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "WP Blog";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.Blog);
        //return objId;
    }
    //return null;
}

function element_woo_shortcodes_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('woo_shortcodes_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_shortcodes_holder').id + Math.random();
        tmpl.querySelector('.woo_shortcodes_holder').id = parentId;

        $(tmpl.querySelector('.woo_shortcodes_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.woo_shortcodes_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.woo_shortcodes_holder')).attr("data-objType", obj.Type);

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
        obj.EditModalTitle = "Woo Shortcodes";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.WooShortcodes);
        return objId;
    }
    return null;
}

function element_woo_featured_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('woo_featured_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_featured_holder').id + Math.random();
        tmpl.querySelector('.woo_featured_holder').id = parentId;

        $(tmpl.querySelector('.woo_featured_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.woo_featured_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.woo_featured_holder')).attr("data-objType", obj.Type);

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
        obj.EditModalTitle = "Woo Featured";
        AssignObjectProperties(editorContent, obj);
        //   add_object(objId, containerId, ElementTypeEnum.WooFeatured);
       
    }
}

function element_fusion_code_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('fusion_code_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.fusion_code_holder').id + Math.random();
        tmpl.querySelector('.fusion_code_holder').id = parentId;
        $(tmpl.querySelector('.fusion_code_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.fusion_code_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.fusion_code_holder')).attr("data-objType", obj.Type);
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
        obj.EditModalTitle = "Fusion Code";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.CodeBlock);
        //return objId;
    }
    //return null;
}

function element_countdown_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('countdown_box_template').content.cloneNode(true);
        tmpl.querySelector('.countdown_box_holder').id = parentId;
        $(tmpl.querySelector('.countdown_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.countdown_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.countdown_box_holder')).attr("data-objType", obj.Type);
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

        if (obj[651] != undefined) {
            $(tmpl.querySelector(".countdown_box_columns")).html(obj[651]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Countdown Box";
        AssignObjectProperties(editorContent, obj);
        return objId;
    }
    return null;
}

//function element_counter_box_dropped(containerId, parentId, objId, obj) {
//    if (containerId.indexOf('element_') === -1) {
//        var tmpl = document.getElementById('counter_box_template').content.cloneNode(true);
//        //var parentId = tmpl.querySelector('.counter_box_holder').id + Math.random();
//        tmpl.querySelector('.counter_box_holder').id = parentId;
//        $(tmpl.querySelector('.counter_box_holder')).attr("data-objId", objId);
//        $(tmpl.querySelector('.counter_box_holder')).attr("data-parentObjId", containerId);
//        $(tmpl.querySelector('.counter_box_holder')).attr("data-objType", obj.Type);
//        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
//        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
//        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
//        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
//        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
//        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
//        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
//        //var objId = tmpl.querySelector('.editable-element').id;

//        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
//        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
//        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

//        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
//        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
//        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

//        if (obj[331] != undefined) {
//            $(tmpl.querySelector(".counter_box_columns")).html(obj[331]);
//        }

//        document.getElementById(containerId).appendChild(tmpl);
//        var t = document.getElementById(parentId);
//        obj.OffsetX = $(t).offset().left;
//        obj.OffsetY = $(t).offset().top;
//        obj.OuterHeight = $(t).outerHeight();
//        obj.EditModalTitle = "Counter Box";
//        AssignObjectProperties(editorContent, obj);
//        //add_object(objId, containerId, ElementTypeEnum.CounterBox);
//        return objId;
//    }
//    return null;
//}

//function element_counter_circle_dropped(containerId, parentId, objId, obj) {
//    if (containerId.indexOf('element_') === -1) {
//        var tmpl = document.getElementById('counter_circle_template').content.cloneNode(true);
//        //var parentId = tmpl.querySelector('.counter_circle_holder').id + Math.random();
//        tmpl.querySelector('.counter_circle_holder').id = parentId;
//        $(tmpl.querySelector('.counter_circle_holder')).attr("data-objId", objId);
//        $(tmpl.querySelector('.counter_circle_holder')).attr("data-parentObjId", containerId);
//        $(tmpl.querySelector('.counter_circle_holder')).attr("data-objType", obj.Type);
//        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
//        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
//        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
//        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
//        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
//        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
//        tmpl.querySelector('.editable-element').id = objId;//tmpl.querySelector('.editable-element').id + Math.random();
//        //var objId = tmpl.querySelector('.editable-element').id;

//        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
//        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
//        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

//        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
//        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
//        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

//        document.getElementById(containerId).appendChild(tmpl);
//        var t = document.getElementById(parentId);
//        obj.OffsetX = $(t).offset().left;
//        obj.OffsetY = $(t).offset().top;
//        obj.OuterHeight = $(t).outerHeight();
//        obj.EditModalTitle = "Counter Circle";
//        AssignObjectProperties(editorContent, obj);
//        //add_object(objId, containerId, ElementTypeEnum.CounterCircle);
//        return objId;
//    }
//    return null;
//}

function element_woo_carousel_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('woo_carousel_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.woo_carousel_holder').id = parentId;
        $(tmpl.querySelector('.woo_carousel_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.woo_carousel_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.woo_carousel_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
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
        obj.EditModalTitle = "Woo Carousel";
        AssignObjectProperties(editorContent, obj);
       // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_testimonial_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('testimonial_box_template').content.cloneNode(true);
        // var parentId = tmpl.querySelector('.testimonial_holder').id + Math.random();
        tmpl.querySelector('.testimonial_box_holder').id = parentId;
        $(tmpl.querySelector('.testimonial_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.testimonial_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.testimonial_box_holder')).attr("data-objType", obj.Type);
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

        if (obj[75] != undefined) {
            $(tmpl.querySelector(".testimonial_content")).html(obj[75]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Testimonial Box";
        AssignObjectProperties(editorContent, obj);
       // add_object(objId, containerId, ElementTypeEnum.Testimonial);
        return objId;
    }
    return null;
}

function element_video_vimeo_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('video_vimeo_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.video_vimeo_holder').id = parentId;
        $(tmpl.querySelector('.video_vimeo_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.video_vimeo_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.video_vimeo_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        if (obj[325] != undefined) {
            $(tmpl.querySelector('.viemo_url')).html(obj[325]);
        }

        if (obj[605] != undefined) {
            $(tmpl.querySelector('.youtube_url')).html(obj[605]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Vimeo Video";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_tagline_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('tagline_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.tagline_box_holder').id = parentId;
        $(tmpl.querySelector('.tagline_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.tagline_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.tagline_box_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
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
        obj.EditModalTitle = "Tagline Box";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_sound_cloud_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('sound_cloud_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.sound_cloud_holder').id = parentId;
        $(tmpl.querySelector('.sound_cloud_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.sound_cloud_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.sound_cloud_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        if (obj[543] != undefined) {
            $(tmpl.querySelector('.soundcloud_url')).html(obj[543]);
        }
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Sound Cloud";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_social_link_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('social_link_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.social_link_holder').id = parentId;
        $(tmpl.querySelector('.social_link_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.social_link_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.social_link_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
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
        obj.EditModalTitle = "Social Links";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_slider_element_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('slider_element_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.slider_element_holder').id = parentId;
        $(tmpl.querySelector('.slider_element_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.slider_element_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.slider_element_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        var images = '';
        if (obj[614] != undefined) {
            for (var i = 0 ; i < obj[614].length ; i++) {
                if (obj[614][i][36] != undefined) {
                    images += "<li> <img src='" + obj[614][i][36] + "'> </li>";
                }
            }
            $(tmpl.querySelector(".slider_elements")).html(images);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Slider";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_flip_boxes_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('flip_boxes_template').content.cloneNode(true);
        tmpl.querySelector('.flip_boxes_holder').id = parentId;
        $(tmpl.querySelector('.flip_boxes_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.flip_boxes_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.flip_boxes_holder')).attr("data-objType", obj.Type);

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

        if (obj[424] != undefined) {
            $(tmpl.querySelector(".flip_boxes_columns")).html(obj[424]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Flip Box";
        AssignObjectProperties(editorContent, obj);
    }
}

function element_sharing_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('sharing_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.sharing_box_holder').id = parentId;
        $(tmpl.querySelector('.sharing_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.sharing_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.sharing_box_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        if (obj[490] != undefined) {
            $(tmpl.querySelector(".sharing_tagline")).html(obj[490]);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Sharing Box";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_font_awesome_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('font_awesome_template').content.cloneNode(true);
        tmpl.querySelector('.font_awesome_holder').id = parentId;
        $(tmpl.querySelector('.font_awesome_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.font_awesome_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.font_awesome_holder')).attr("data-objType", obj.Type);

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
        obj.EditModalTitle = "Font Awesome";
        AssignObjectProperties(editorContent, obj);
    }
}

function element_fusionslider_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('fusionslider_template').content.cloneNode(true);
        tmpl.querySelector('.fusionslider_holder').id = parentId;
        $(tmpl.querySelector('.fusionslider_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.fusionslider_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.fusionslider_holder')).attr("data-objType", obj.Type);

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
        obj.EditModalTitle = "Fusion Slider";
        AssignObjectProperties(editorContent, obj);
    }
}

function element_section_separator_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('section_separator_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.section_separator_holder').id = parentId;
        $(tmpl.querySelector('.section_separator_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.section_separator_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.section_separator_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        if (obj[478] != undefined) {
            $(tmpl.querySelector('.separator')).addClass(obj[478]);
        }
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Section Separator";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_revolution_slider_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('revolution_slider_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.revolution_slider_holder').id = parentId;
        $(tmpl.querySelector('.revolution_slider_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.revolution_slider_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.revolution_slider_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        if (obj[469] != undefined) {
            $(tmpl.querySelector(".rev_slider_name")).html(obj[469]);
        }
        
        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Revolution Slider";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_recent_works_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('recent_works_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.recent_works_holder').id = parentId;
        $(tmpl.querySelector('.recent_works_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.recent_works_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.recent_works_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
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
        obj.EditModalTitle = "Recent Works";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_recent_posts_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('recent_posts_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.recent_posts_holder').id = parentId;
        $(tmpl.querySelector('.recent_posts_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.recent_posts_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.recent_posts_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
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
        obj.EditModalTitle = "Recent Posts";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_pricing_table_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('pricing_table_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.pricing_table_holder').id = parentId;
        $(tmpl.querySelector('.pricing_table_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.pricing_table_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.pricing_table_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        if (obj[460] != undefined) {
            $(tmpl.querySelector(".pricing_table_style")).html(obj[460]);
        }

        if (obj[465] != undefined) {
            if (obj[465].length == 218) {
                $(tmpl.querySelector(".pricing_table_columns")).html(1);
            } else if (obj[465].length == 422) {
                $(tmpl.querySelector(".pricing_table_columns")).html(2);
            } else if (obj[465].length == 626) {
                $(tmpl.querySelector(".pricing_table_columns")).html(3);
            } else if (obj[465].length == 830) {
                $(tmpl.querySelector(".pricing_table_columns")).html(4);
            } else if (obj[465].length == 1034) {
                $(tmpl.querySelector(".pricing_table_columns")).html(5);
            } else if (obj[465].length == 1238) {
                $(tmpl.querySelector(".pricing_table_columns")).html(6);
            }
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Pricing Table";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_post_slider_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('post_slider_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.post_slider_holder').id = parentId;
        $(tmpl.querySelector('.post_slider_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.post_slider_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.post_slider_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
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
        obj.EditModalTitle = "Post Slider";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_person_box_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('person_box_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.person_box_holder').id = parentId;
        $(tmpl.querySelector('.person_box_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.person_box_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.person_box_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
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
        obj.EditModalTitle = "Person Box";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_layer_sldier_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('layer_sldier_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.layer_sldier_holder').id = parentId;
        $(tmpl.querySelector('.layer_sldier_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.layer_sldier_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.layer_sldier_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);
        var images = '';
        if (obj[635] != undefined) {
            for (var i = 0 ; i < obj[635].length ; i++) {
                if (obj[635][i][90]  != undefined) {
                    images += "<li> <img src='" + obj[635][i][90] + "'> </li>";
                }
                if (obj[635][i][93] != undefined) {
                    images += "<li> <img src='" + obj[635][i][93] + "'> </li>";
                }
            }
            $(tmpl.querySelector(".layer_slider_id")).html(images);
        }


        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Layer Slider";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        //return objId;
    }
    //return null;
}

function element_image_carousel_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('image_carousel_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.woo_carousel_holder').id + Math.random();
        tmpl.querySelector('.image_carousel_holder').id = parentId;
        $(tmpl.querySelector('.image_carousel_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.image_carousel_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.image_carousel_holder')).attr("data-objType", obj.Type);

        tmpl.querySelector('.edit-element').id = tmpl.querySelector('.edit-element').id + Math.random();
        $(tmpl.querySelector('.edit-element')).attr("data-parent", parentId);
        tmpl.querySelector('.clone-element').id = tmpl.querySelector('.clone-element').id + Math.random();
        $(tmpl.querySelector('.clone-element')).attr("data-parent", parentId);
        tmpl.querySelector('.delete-element').id = tmpl.querySelector('.delete-element').id + Math.random();
        $(tmpl.querySelector('.delete-element')).attr("data-parent", parentId);
        tmpl.querySelector('.editable-element').id = objId;// tmpl.querySelector('.editable-element').id + Math.random();
        //var objId = tmpl.querySelector('.editable-element').id;

        $(tmpl.querySelector('.edit-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.clone-element')).attr("data-objId", objId);
        $(tmpl.querySelector('.delete-element')).attr("data-objId", objId);

        $(tmpl.querySelector('.edit-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.clone-element')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.delete-element')).attr("data-parentObjId", containerId);

        var images = '';
        if (obj[617] != undefined) {
            for (var i = 0 ; i < obj[617].length ; i++) {
                if (obj[617][i][70] != undefined) {
                    images += "<li> <img src='" + obj[617][i][70] + "'> </li>";
                }
            }
            $(tmpl.querySelector(".image_carousel_elements")).html(images);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Image Carousel";
        AssignObjectProperties(editorContent, obj);
        // add_object(objId, containerId, ElementTypeEnum.WooCarousel);
        return objId;
    }
    return null;
}

function element_slider_with_thumb_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('slider_Thumb_element_template').content.cloneNode(true);
        tmpl.querySelector('.slider_element_Thumb_holder').id = parentId;
        $(tmpl.querySelector('.slider_element_Thumb_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.slider_element_Thumb_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.slider_element_Thumb_holder')).attr("data-objType", obj.Type);

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
        var images = '';
        if (obj[668] != undefined) {
            for (var i = 0 ; i < obj[668].length ; i++) {
                if (obj[668][i][99] != undefined) {
                    images += "<li> <img src='" + obj[668][i][99] + "'> </li>";
                }
                if (obj[668][i][100] != undefined) {
                    images += "<li> <img src='" + obj[668][i][100] + "'> </li>";
                }
            }
            $(tmpl.querySelector(".slider_Thumb_id")).html(images);
        }


        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Slider With Thumb";
        AssignObjectProperties(editorContent, obj);
    }
}

function element_apn_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('apn_element_template').content.cloneNode(true);
        //var parentId = tmpl.querySelector('.text_box_holder').id + Math.random();
        tmpl.querySelector('.apn_element_holder').id = parentId;
        $(tmpl.querySelector('.apn_element_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.apn_element_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.apn_element_holder')).attr("data-objType", obj.Type);
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

        if (obj[700] != undefined) {
            var st = obj[700].toString();
            if (st.length > 36) {
                $(tmpl.querySelector('.productNumberContent')).text(st.substring(0, 36));
            } else {
                $(tmpl.querySelector('.productNumberContent')).text(st);
            }
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Automated Product Number";
        AssignObjectProperties(editorContent, obj);
        //add_object(objId, containerId, ElementTypeEnum.TextBox);
        //return objId;
    }
    //return null;
}

function element_imagelibrary_dropped(containerId, parentId, objId, obj) {
    if (containerId.indexOf('element_') === -1) {
        var tmpl = document.getElementById('imagelibrary_element_template').content.cloneNode(true);
        tmpl.querySelector('.imagelibrary_element_holder').id = parentId;
        $(tmpl.querySelector('.imagelibrary_element_holder')).attr("data-objId", objId);
        $(tmpl.querySelector('.imagelibrary_element_holder')).attr("data-parentObjId", containerId);
        $(tmpl.querySelector('.imagelibrary_element_holder')).attr("data-objType", obj.Type);
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

        var images = '';
        if (obj[702] != undefined) {
            for (var i = 0 ; i < obj[702].length ; i++) {
                if (obj[702][i][108] != undefined) {
                    images += "<li> <img src='" + obj[702][i][108] + "'> </li>";
                }
            }
            $(tmpl.querySelector(".imageLibrary_elements")).html(images);
        }

        document.getElementById(containerId).appendChild(tmpl);
        var t = document.getElementById(parentId);
        if ($(t).offset() != undefined && $(t).offset().left != 0) {
            obj.OffsetX = $(t).offset().left;
        }
        if ($(t).offset() != undefined && $(t).offset().top != 0) {
            obj.OffsetY = $(t).offset().top;
        }
        obj.OuterHeight = $(t).outerHeight();
        obj.EditModalTitle = "Image Library";
        AssignObjectProperties(editorContent, obj);
        return objId;
    }
    return null;
}