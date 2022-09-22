$(document).ready(function() {
    $(".editor-tab-button").click(function () {
        var index = $(this).attr("data-index");
        for (var i = 1; i <= 4; i++) {
            $("#editorElements-" + i).hide();
            $("#tabButton-" + i).removeClass("editor-tab-button-selected");
            $("#tabButton-" + i).find("a").removeClass("font-White font-Bold").addClass("font-Black");
        }
        $("#editorElements-" + index).show();
        $("#editorElements-" + index).css("display","inline-block");
        $(this).addClass("editor-tab-button-selected");
        $(this).find("a").removeClass("font-Black").addClass("font-White font-Bold");
    });

    $("#del_icon").click(function () {
        var isConfirm = confirm("Are you want to clear editor?");
        if (isConfirm) {
            var foo = document.getElementById("editor");
            while (foo.firstChild) foo.removeChild(foo.firstChild);
            editorContent.Children = [];
        };
    });

    $(".grid-option").click(function () {
        var xOffSet = window.pageXOffset;
        var yOffSet = window.pageYOffset;
        var elementId = $(this).attr("id");
        var obj = {};
        obj.ParentId = elementId + "_holder_" + Math.random();
        obj.Id = elementId + "_" + Math.random();
        obj.Type = elementId.replace(/_/g, "");
        obj.ContainerId = "editor";
        obj.Children = [];
        add_object(elementId, obj);
        window.scroll(xOffSet, yOffSet);
    });

    $(".element-option").click(function () {
        var xOffSet = window.pageXOffset;
        var yOffSet = window.pageYOffset;
        var elementId = $(this).attr("id");
        var obj = {};
        if (elementId.indexOf("Custom_Template") < 0) {
            obj.ParentId = elementId + "_holder_" + Math.random();
            obj.Id = elementId + "_" + Math.random();
            obj.Type = elementId.replace(/_/g, "");
            obj.ContainerId = "editor";
            obj.Children = [];
            add_object(elementId, obj);
        } else {
            var pageId = $(this).attr("data-pageId");
            // alert(pageId);
            $.ajax({
                type: 'GET',
                url: root + "api/pppageinfo/GetPageById/" + pageId,
                processData: false,
                contentType: false,
                success: function (data) {
                    obj = data.PageJson;
                    process_custom_object("editor", null, null, obj.Children);
                    window.scroll(xOffSet, yOffSet);
                },
                error: function (data) {
                    return false;
                }
            });
        }
        
        window.scroll(xOffSet, yOffSet);
    });
});

function deleteElement(ctrl) {
    var xOffSet = window.pageXOffset;
    // var yOffSet = window.pageYOffset;
    var yOffSet = $("#editor").scrollTop();
    var id = $(ctrl).attr("data-parent");
    var elem = document.getElementById(id);
    elem.parentNode.removeChild(elem);

    var objId = $(ctrl).attr("data-objId");
    DeleteObjectbyId(objId, editorContent);
    parseFullObject();
    //window.scroll(xOffSet, yOffSet);
    $("#editor").scrollTop(yOffSet);
}

function cloneElement(ctrl) {
    var xOffSet = window.pageXOffset;
    //var yOffSet = window.pageYOffset;
    var yOffSet = $("#editor").scrollTop();
    var elementId = $(ctrl).attr("id");
    var objId = $(ctrl).attr("data-objId");
    var parentObjId = $(ctrl).attr("data-parentObjId");

    var obj = {};
    var tempobj = GetObjectbyId(objId, editorContent);
    obj = $.extend(true, {}, tempobj);
    obj = PrepareObjectForCloning(obj, obj.ContainerId);


    swal({
        title: 'Are you sure?',
        text: "Where do you want to put cloned object?",
        type: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Put it after this object!',
        cancelButtonText: 'Put it as last object',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false
    },
    function(isConfirm) {
        if (isConfirm) {
            obj.DropX = tempobj.OffsetX + $("#" + tempobj.Id).outerWidth();
//            alert(obj.DropX);
            obj.DropY = tempobj.OffsetY + 10;
  //          alert(obj.DropY);
            add_object(elementId, obj);
            $("#editor").scrollTop(yOffSet);
        } else {
            add_object(elementId, obj);
            $("#editor").scrollTop(yOffSet);
        }
    });



    
    
    //window.scroll(xOffSet, yOffSet);
}

function editElement(ctrl) {
    var objId = $(ctrl).attr("data-objId");
    var parentObjId = $(ctrl).attr("data-parentObjId");
    var obj = {};
    obj = GetObjectbyId(objId, editorContent);
    OpenModalForEditElement(obj,ctrl);
}

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    
    ev.dataTransfer.setData("text/plain", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    ev.stopPropagation();
    var data = ev.dataTransfer.getData("text/plain");
    
    var node = document.getElementById(data);
    var elementId = $(node).attr("data-elementType");
   
    var xOffSet = $("#editor").scrollLeft();
    var yOffSet = $("#editor").scrollTop();
    //var yOffSet = window.pageYOffset;

    var editorOffsetTop = $("#editor").offset().top;
    // alert($("#editor").pageXOffset);
    var pageScrolled = window.pageYOffset;
    
    //alert(ev.clientY);
    //alert(editorOffsetTop);
    //alert(yOffSet);
    var yOffSetToPass = ((ev.clientY - editorOffsetTop) + yOffSet);
    //var yOffSetToPass = yOffSet;
    //alert(ev.clientY + yOffSetToPass);
    //alert(yOffSetToPass);
    //alert("Editor Offset Y :" + $("#editor").offset().top);
    //alert("Editor height :" + $("#editor").get(0).scrollHeight);
    //alert($(this).offset().top);
   // alert(yOffSet);
    //alert(ev.clientX);
    // alert(ev.clientY);
    //alert(yOffSetToPass);
    var obj = {};
    if (elementId == undefined) {
        //new object
        elementId = $(node).attr("id");
        
        if (elementId.indexOf("Custom_Template") < 0) {

            obj.ParentId = elementId + "_holder_" + Math.random();
            obj.Id = elementId + "_" + Math.random();
            obj.Type = elementId.replace(/_/g, "");
            obj.ContainerId = ev.target.id;
            obj.Children = [];
            obj.DropX = ev.clientX;
            obj.DropY = ev.clientY + yOffSet + pageScrolled;
            //obj.DropY = yOffSet;
            add_object(elementId, obj);
        } else {
            var pageId = $(node).attr("data-pageId");
           // alert(pageId);
            $.ajax({
                type: 'GET',
                url: root + "api/pppageinfo/GetPageById/" + pageId,
                processData: false,
                contentType: false,
                success: function (data) {
                    obj = data.PageJson;
                    process_custom_object(ev.target.id, ev.clientX, ev.clientY + yOffSet + pageScrolled, obj.Children);
                    //  window.scroll(xOffSet, yOffSet);
                    $("#editor").scrollTop(yOffSet);
                },
                error: function (data) {
                    return false;
                }
            });
        }
    } else {
        //existing object drag drop
        elementId = $(node).attr("id");
        var id = $(node).attr("data-objId");
        //alert("Calling from drop");
        var tempobj = GetObjectbyId(id, editorContent);
        obj = $.extend(true, {}, tempobj);
        
        if (obj.Id != undefined) {
            obj = PrepareObjectForCloning(obj, ev.target.id);
            //obj.ParentId = elementId;
            //obj.Type = $(node).attr("data-objType");
            //obj.ContainerId = ev.target.id;
            //obj.Children = [];
            //alert(xOffSet);
            //alert(yOffSet);
            //alert(ev.screenX);
            //alert(ev.screenY);
            obj.DropX = ev.clientX;
            obj.DropY = ev.clientY + yOffSet + pageScrolled;
            //alert(obj.DropY);
            var ty = add_object(elementId, obj);
            
            if (ty === true) {
                DeleteObjectbyId(tempobj.Id, editorContent);
                //parseFullObject();
            }
            parseFullObject();
        }
    }
    //window.scroll(xOffSet, yOffSet);
    $("#editor").scrollTop(yOffSet);
}

function objectToElementParser(obj) {
    var conId = 0;
    var containerId = obj.ContainerId;
    switch(obj.Type) {
        case ElementTypeEnum.FullWidthContainer.value.toLowerCase():
            full_width_container_dropped(containerId, obj.ParentId, obj.Id,obj);
            break;
        case ElementTypeEnum.GridOne.value.toLowerCase():
            grid_one_dropped(containerId,obj.ParentId,obj.Id,obj);
            break;
        case ElementTypeEnum.GridTwo.value.toLowerCase():
            grid_two_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridThree.value.toLowerCase():
            grid_three_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridTwoThird.value.toLowerCase():
            grid_two_third_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridFour.value.toLowerCase():
            grid_four_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridThreeFourth.value.toLowerCase():
            grid_three_four_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridFive.value.toLowerCase():
            grid_five_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridTwoFifth.value.toLowerCase():
            grid_two_fifth_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridThreeFifth.value.toLowerCase():
            grid_three_fifth_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridFourFifth.value.toLowerCase():
            grid_four_fifth_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridSix.value.toLowerCase():
            grid_one_six_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GridFiveSixth.value.toLowerCase():
            grid_five_sixth_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.ImageFrame.value.toLowerCase():
            element_image_frame_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.BackToTopBlock.value.toLowerCase():
            conId = element_back_to_top_block_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.ButtonBlock.value.toLowerCase():
            conId = element_button_block_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.ButtonWithIcon.value.toLowerCase():
            conId = element_button_with_icon_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.GoogleMap.value.toLowerCase():
            conId = element_google_map_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.MenuAnchor.value.toLowerCase():
            conId = element_menu_anchor_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.CheckList.value.toLowerCase():
            conId = element_check_list_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Modal.value.toLowerCase():
            conId = element_modal_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.ContentBoxes.value.toLowerCase():
            conId = element_content_boxes_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.TableBox.value.toLowerCase():
            conId = element_table_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.TabsBox.value.toLowerCase():
            conId = element_tabs_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Separator.value.toLowerCase():
            conId = element_separator_element_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.ProgressBar.value.toLowerCase():
            conId = element_progress_bar_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.TextBox.value.toLowerCase():
            conId = element_text_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Toggles.value.toLowerCase():
            conId = element_toggles_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Title.value.toLowerCase():
            conId = element_title_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.VideoYoutube.value.toLowerCase():
            conId = element_video_youtube_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Video.value.toLowerCase():
            conId = element_video_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Alert.value.toLowerCase():
            element_alert_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Blog.value.toLowerCase():
            conId = element_wp_blog_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.WooShortcodes.value.toLowerCase():
            conId = element_woo_shortcodes_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.WooFeatured.value.toLowerCase():
            element_woo_featured_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.CodeBlock.value.toLowerCase():
            conId = element_fusion_code_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.CountdownBox.value.toLowerCase():
            conId = element_countdown_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        //case ElementTypeEnum.CounterBox.value.toLowerCase():
        //    conId = element_counter_box_dropped(containerId, obj.ParentId, obj.Id, obj);
        //    break;
        //case ElementTypeEnum.CounterCircle.value.toLowerCase():
        //    conId = element_counter_circle_dropped(containerId, obj.ParentId, obj.Id, obj);
        //    break;
        case ElementTypeEnum.WooCarousel.value.toLowerCase():
            conId = element_woo_carousel_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.Testimonial.value.toLowerCase():
            conId = element_testimonial_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.VideoVimeo.value.toLowerCase():
            conId = element_video_vimeo_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.TaglineBox.value.toLowerCase():
            conId = element_tagline_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.SoundCloud.value.toLowerCase():
            conId = element_sound_cloud_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.SocialLinks.value.toLowerCase():
            conId = element_social_link_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.SliderElement.value.toLowerCase():
            conId = element_slider_element_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.FlipBoxes.value.toLowerCase():
            conId = element_flip_boxes_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.SharingBox.value.toLowerCase():
            conId = element_sharing_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.FontAwesome.value.toLowerCase():
            conId = element_font_awesome_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.FusionSlider.value.toLowerCase():
            conId = element_fusionslider_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.SectionSeparator.value.toLowerCase():
            conId = element_section_separator_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.RevolutionSlider.value.toLowerCase():
            conId = element_revolution_slider_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.RecentWorks.value.toLowerCase():
            conId = element_recent_works_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.RecentPosts.value.toLowerCase():
            conId = element_recent_posts_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.PricingTable.value.toLowerCase():
            conId = element_pricing_table_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.PostSlider.value.toLowerCase():
            conId = element_post_slider_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.PersonBox.value.toLowerCase():
            conId = element_person_box_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.LayerSldier.value.toLowerCase():
            conId = element_layer_sldier_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.ImageCarousel.value.toLowerCase():
            conId = element_image_carousel_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.SliderWithThumb.value.toLowerCase():
            conId = element_slider_with_thumb_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.PageScroller.value.toLowerCase():
            conId = element_page_scroller_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.APN.value.toLowerCase():
            conId = element_apn_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        case ElementTypeEnum.ImageLibrary.value.toLowerCase():
            conId = element_imagelibrary_dropped(containerId, obj.ParentId, obj.Id, obj);
            break;
        default: break;
    }
}

function UploadMedia() {
    $("#dialog_uploadMedia").empty();
    var template = root + "/Templates/CMS/Editor/UploadMediaTemplate.html";
    $("#dialog_uploadMedia").load(template);
    var dialog = $("#dialog_uploadMedia").dialog({
        autoOpen: false,
        width: "96%",
        modal: true,
        resizable: false,
        position: [30, 30],
        dialogClass: "no-titlebar",
        create: function (event) { $(event.target).parent().css('position', 'fixed'); $(".ui-widget-header").hide(); },
        open: function () { $(".ui-widget-header").hide(); },
        close: function () {
            $(".ui-widget-header").show();
        }
    });
    dialog.dialog("open");
}

$(window).resize(function () {
    var wWidth = $(window).width();
    var dWidth = wWidth * 0.9;
    var wHeight = $(window).height();
    var dHeight = wHeight * 0.9;
    $("#dialog_uploadMedia").dialog("option", "width", dWidth);
    $("#dialog_uploadMedia").dialog("option", "height", dHeight);
});

function loadTemplate(ctrl) {
    var pageId = $(ctrl).attr("data-id");
    if (confirm("Are you sure to replace the content with this template?")) {
        $.ajax({
            type: 'GET',
            url: root + "api/pppageinfo/GetPageById/" + pageId,
            processData: false,
            contentType: false,
            success: function (data) {
                editorContent = data.PageJson;
                if (editorContent.Children.length > 0) {
                    parseFullObject();
                }
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
}