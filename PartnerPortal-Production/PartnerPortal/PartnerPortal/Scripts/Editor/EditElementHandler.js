function OpenModalForViewJson() {
    var dialog = $("#dialog_viewJson").dialog({
        autoOpen: false,
        height: $(window).height() - 70,
        width: "96%",
        modal: true,
        resizable: false,
        position: [30, 30],
        create: function (event) { $(event.target).parent().css('position', 'fixed'); },
        buttons: {
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
            dialog.dialog("close");
        }
    });
    dialog.dialog("open");
}

function OpenModalForEditElement(obj,ctrl) {
    $("#dialog_form").empty();
    var s = String(obj.Type);
    var template = rootUrl + "Templates/CMS/Editor/EditElement/Elements/" + s + "Template.html";
    if (obj.Id.indexOf('container') >= 0) {
        template = rootUrl + "/Templates/CMS/Editor/EditElement/Columns/" + s + "Template.html";
    }
    $("#dialog_form").load(template);
    $("#dialog_form").attr("data-obj", JSON.stringify(obj));
    var dialog = $("#dialog_form").dialog({
        autoOpen: false,
        title: obj.EditModalTitle,
        height: $(window).height() - 70,
        width: "96%",
        modal: true,
        resizable: false,
        position: [30, 30],
        create: function(event) {$(event.target).parent().css('position', 'fixed');},
        buttons: {
            Save: function () {
                $(".objectProperty").each(function () {
                    if ($(this).is('div')) {
                        obj[$(this).attr("data-PropertyIndex")] = $(this).html();
                    } else {
                        obj[$(this).attr("data-PropertyIndex")] = $(this).val();
                    }
                });
                
                if (obj.Type == "textbox") {
                    obj[353] = tinymce.get('textTextBoxTemplateArea').getContent();
                    $(ctrl).parent().parent().parent().find(".textboxContent").html("<small>" + obj[353] + "</small>");
                    var st = $(ctrl).parent().parent().parent().find(".textboxContent").text();
                    if (st.length > 36) {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(st.substring(0, 36));
                    } else {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(st);
                    }
                }
                else if (obj.Type == "alertbox") {
                    obj[347] = tinymce.get('textAlertBoxTemplateArea').getContent();
                    $(ctrl).parent().parent().parent().find(".textboxContent").html("<small>" + obj[347] + "</small>");
                    var ab = $(ctrl).parent().parent().parent().find(".textboxContent").text();
                    if (ab.length > 36) {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(ab.substring(0, 36));
                    } else {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(ab);
                    }
                }
                else if (obj.Type == "modal") {
                    obj[629] = tinymce.get('modalTextBoxTemplateArea').getContent();
                    $(ctrl).parent().parent().parent().find(".modal_name").html(obj[623]);
                }
                else if (obj.Type == "pricingtable") {
                    obj[468] = tinymce.get('textPricingTableTemplateArea').getContent();
                    $(ctrl).parent().parent().parent().find(".pricing_table_style").html(obj[460]);
                    if (obj[465].length == 218) {
                        $(ctrl).parent().parent().parent().find(".pricing_table_columns").html(1);
                    } else if (obj[465].length == 422) {
                        $(ctrl).parent().parent().parent().find(".pricing_table_columns").html(2);
                    } else if (obj[465].length == 626) {
                        $(ctrl).parent().parent().parent().find(".pricing_table_columns").html(3);
                    } else if (obj[465].length == 830) {
                        $(ctrl).parent().parent().parent().find(".pricing_table_columns").html(4);
                    } else if (obj[465].length == 1034) {
                        $(ctrl).parent().parent().parent().find(".pricing_table_columns").html(5);
                    } else if (obj[465].length == 1238) {
                        $(ctrl).parent().parent().parent().find(".pricing_table_columns").html(6);
                    }
                }
                else if (obj.Type == "tablebox") {
                    obj[556] = tinymce.get('textTableBoxTemplateArea').getContent();
                    $(ctrl).parent().parent().parent().find(".table_style").html(obj[554]);
                    $(ctrl).parent().parent().parent().find(".table_columns").html(obj[555]);
                }
                else if (obj.Type == "taglinebox") {
                    obj[582] = tinymce.get('textTaglineBoxTemplateArea').getContent();
                }
                else if (obj.Type == "titlebox") {
                    obj[600] = tinymce.get('textTitleBoxTemplateArea').getContent();
                    $(ctrl).parent().parent().parent().find(".title_text").html(obj[600]);
                    var st = $(ctrl).parent().parent().parent().find(".title_text").text();
                    if (st.length > 36) {
                        $(ctrl).parent().parent().parent().find(".title_text").text(st.substring(0, 36));
                    } else {
                        $(ctrl).parent().parent().parent().find(".title_text").text(st);
                    }
                }
                else if (obj.Type == "buttonblock") {
                    var buttonObj = $(ctrl).parent().parent().parent().find(".button");
                    $(buttonObj).removeClass();
                    $(buttonObj).addClass("button").addClass(obj[379]);
                    $(ctrl).parent().parent().parent().find(".textboxContent").html("<small>" + obj[385] + "</small>");
                    var bb = $(ctrl).parent().parent().parent().find(".textboxContent").text();
                    if (bb.length > 36) {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(bb.substring(0, 36));
                    } else {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(bb);
                    }
                }
                else if (obj.Type == "buttonwithicon") {
                    var buttonwithiconObj = $(ctrl).parent().parent().parent().find(".button");
                    $(buttonwithiconObj).removeClass();
                    $(buttonwithiconObj).css("background-color", obj[675]);
                    $(ctrl).parent().parent().parent().find(".textboxContent").html("<small>" + obj[672] + "</small>");
                    var bb = $(ctrl).parent().parent().parent().find(".textboxContent").text();
                    if (bb.length > 36) {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(bb.substring(0, 36));
                    } else {
                        $(ctrl).parent().parent().parent().find(".textboxContent").text(bb);
                    }
                }
                else if (obj.Type == "menuanchor") {
                    $(ctrl).parent().parent().parent().find(".anchor_name").html(obj[446]);
                }
                else if (obj.Type == "revolutionslider") {
                    $(ctrl).parent().parent().parent().find(".rev_slider_name").html(obj[469]);
                }
                else if (obj.Type == "layersldier") {
                    //$(ctrl).parent().parent().parent().find(".layer_slider_id").html(obj[447]);
                    var images = '';
                    var sliderElements = [];
                    $("#SliderContainer tr.child-clone-row").each(function () {
                        var sliderElement = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                sliderElement[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                sliderElement[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 90) {
                                images += "<li> <img src='" + $(this).val() + "'> </li>";
                            }
                            if ($(this).attr("data-subPropertyIndex") == 93) {
                                images += "<li> <img src='" + $(this).val() + "'> </li>";
                            }
                        });
                        sliderElements.push(sliderElement);
                        $(ctrl).parent().parent().parent().find(".layer_slider_id").html(images);
                    });
                    obj[635] = sliderElements;
                }
                else if (obj.Type == "sliderwiththumb") {
                    var images = '';
                    var sliderThumbElements = [];
                    $("#SliderContainer tr.child-clone-row").each(function () {
                        var sliderElement = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                sliderElement[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                sliderElement[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 99) {
                                images += "<li> <img src='" + $(this).val() + "'> </li>";
                            }
                            if ($(this).attr("data-subPropertyIndex") == 100) {
                                images += "<li> <img src='" + $(this).val() + "'> </li>";
                            }
                        });
                        sliderThumbElements.push(sliderElement);
                        $(ctrl).parent().parent().parent().find(".slider_Thumb_id").html(images);
                    });
                    obj[668] = sliderThumbElements;
                }
                else if (obj.Type == "sharingbox") {
                    $(ctrl).parent().parent().parent().find(".sharing_tagline").html(obj[490]);
                }
                else if (obj.Type == "videovimeo") {
                    $(ctrl).parent().parent().parent().find(".viemo_url").html(obj[325]);
                }
                else if (obj.Type == "videoyoutube") {
                    $(ctrl).parent().parent().parent().find(".youtube_url").html("https://www.youtube.com/" + obj[605]);
                }
                else if (obj.Type == "soundcloud") {
                    $(ctrl).parent().parent().parent().find(".soundcloud_url").html(obj[543]);
                }
                else if (obj.Type == "separatorelement") {                   
                    var separatorObj = $(ctrl).parent().parent().parent().find(".separator");
                    $(separatorObj).removeClass();
                    $(separatorObj).addClass("separator").addClass(obj[478]);
                }
               if (obj.Type == "contentboxes") {
                    $(ctrl).parent().parent().parent().find(".content_boxes_layout").html(obj[412]);
                    $(ctrl).parent().parent().parent().find(".content_boxes_columns").html(obj[417]);
                    
                    var contentBoxes = [];
                    $("#contentBoxesContainer tr.child-clone-row").each(function () {
                        var contentBox = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                contentBox[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                contentBox[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).is('textarea')) {
                                contentBox[16] = tinymce.get($(this).attr("id")).getContent();
                            }
                        });
                        contentBoxes.push(contentBox);
                    });
                    obj[611] = contentBoxes;
               }
               else if (obj.Type == "countdown") {
                   $(ctrl).parent().parent().parent().find(".countdown_url").html(obj[649]);
               }
               else if (obj.Type == "apn") {
                   obj[700] = selectedList1;
                   obj[701] = selectedList2;
                   var st = obj[700].toString();
                   if (st.length > 36) {
                       $(ctrl).parent().parent().parent().find(".productNumberContent").html(st.substring(0, 33) + "....");
                        //$(ctrl).parent().parent().parent().find(".textboxContent").text(st.substring(0, 36));
                    } else {
                       $(ctrl).parent().parent().parent().find(".productNumberContent").html(st);
                    }
               }
                else if (obj.Type == "sliderelement") {
                    var images = '';
                    var sliderElements = [];
                    $("#SliderContainer tr.child-clone-row").each(function () {
                        var sliderElement = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                sliderElement[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                sliderElement[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 36) {
                                images += "<li> <img src='" + $(this).val() + "'> </li>";
                            }
                        });
                        sliderElements.push(sliderElement);
                        $(ctrl).parent().parent().parent().find(".slider_elements").html(images);
                    });
                    obj[614] = sliderElements;
                }
                else if (obj.Type == "flipboxes") {
                    $(ctrl).parent().parent().parent().find(".flip_boxes_columns").html(obj[424]);
                    var flipBoxes = [];
                    $("#flipBoxesContainer tr.child-clone-row").each(function () {
                        var flipBox = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                flipBox[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                flipBox[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).is('textarea')) {
                                flipBox[43] = tinymce.get($(this).attr("id")).getContent();
                            }
                        });
                        flipBoxes.push(flipBox);
                    });
                    obj[615] = flipBoxes;
                }
                else if (obj.Type == "imagecarousel") {
                    var images='';
                    var imageCarousels = [];
                    $("#ImageCarouselContainer tr.child-clone-row").each(function () {
                        var imageCarousel = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                imageCarousel[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                imageCarousel[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 70) {
                                images += "<li> <img src='" + $(this).val() + "'> </li>";
                            }
                        });
                        imageCarousels.push(imageCarousel);
                        $(ctrl).parent().parent().parent().find(".image_carousel_elements").html(images);
                    });
                    obj[617] = imageCarousels;
                }
                else if (obj.Type == "imagelibrary") {
                    var imagesLib = '';
                    var imageLibrarys = [];
                    $("#ImageLibraryContainer tr.child-clone-row").each(function () {
                        var imageLibrary = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                imageLibrary[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                imageLibrary[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 108) {
                                imagesLib += "<li style='width: 5%; display: inline-block;'> <img style='width: 50px; height: 50px;' src='" + $(this).val() + "'> </li>";
                            }
                        });
                        imageLibrarys.push(imageLibrary);
                        $(ctrl).parent().parent().parent().find(".imagelibrary_elements").html(imagesLib);
                    });
                    obj[702] = imageLibrarys;
                }
                else if (obj.Type == "tabsbox") {
                    var tabsBoxes = [];
                    var tabname = '';
                    $("#tabBoxContainer tr.child-clone-row").each(function () {
                        var tabsBox = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                tabsBox[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                tabsBox[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).is('textarea')) {
                                tabsBox[74] = tinymce.get($(this).attr("id")).getContent();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 72) {
                                if ($(this).val() != '') {
                                    tabname += "<li>" + $(this).val() + "</li>";
                                }
                            }
                        });
                        tabsBoxes.push(tabsBox);
                    });
                    $(ctrl).parent().parent().parent().find(".tabs_elements").html(tabname);
                    obj[618] = tabsBoxes;
                }
                else if (obj.Type == "pagescroller") {
                    var tabsBoxes = [];
                    var tabname = '';
                    $("#tabBoxContainer tr.child-clone-row").each(function () {
                        var tabsBox = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                tabsBox[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                tabsBox[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).is('textarea')) {
                                tabsBox[74] = tinymce.get($(this).attr("id")).getContent();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 72) {
                                if ($(this).val() != '') {
                                    tabname += "<li>" + $(this).val() + "</li>";
                                }
                            }
                        });
                        tabsBoxes.push(tabsBox);
                    });
                    $(ctrl).parent().parent().parent().find(".tabs_elements").html(tabname);
                    obj[691] = tabsBoxes;
                }
                else if (obj.Type == "checklist") {
                    var icons = '';
                    var display = '';
                    var tinyText = '';
                    var checkLists = [];
                    $("#checkListContainer tr.child-clone-row").each(function () {
                        var checkList = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                checkList[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                checkList[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).is('textarea')) {
                                checkList[87] = tinymce.get($(this).attr("id")).getContent();
                                tinyText = tinymce.get($(this).attr("id")).getContent({ format: 'text' });
                            }
                            if ($(this).attr("data-subPropertyIndex") == 86) {
                                if ($(this).val() != '') {
                                    icons = "<i class='fa " + $(this).val() + "'></i>";
                                }
                                else {
                                    icons = "<i class='fa " + obj[404] + "'></i>";
                                }
                            }
                            if ($(this).attr("data-subPropertyIndex") == 87) {
                                if (tinyText.trim() != '' && tinyText.trim() != null) {
                                    display += "<li>" + icons + tinyText + "</li>";
                                }
                            }
                        });
                        $(ctrl).parent().parent().parent().find(".checklist_elements").html(display);
                        checkLists.push(checkList);
                    });
                    obj[622] = checkLists;
                }
                else if (obj.Type == "testimonialbox") {
                    var testimonialBoxes = [];
                    var TestimonialName = '';
                    $("#TestimonialBoxContainer tr.child-clone-row").each(function () {                      
                        var testimonialBox = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                testimonialBox[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                testimonialBox[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).attr("data-subPropertyIndex") == 75) {                                
                                TestimonialName += $(this).val() + ",<br>";                                                            
                            }                           
                        });
                        $(ctrl).parent().parent().parent().find(".testimonial_content").html(TestimonialName);
                        testimonialBoxes.push(testimonialBox);
                    });
                    obj[619] = testimonialBoxes;
                }
                else if (obj.Type == "togglesbox") {
                    var togglesboxes = [];
                    $("#toggleBoxesContainer tr.child-clone-row").each(function () {
                        var togglesbox = {};
                        $(this).find(".subObjectProperty").each(function () {
                            if ($(this).is('div')) {
                                togglesbox[$(this).attr("data-subPropertyIndex")] = $(this).html();
                            } else {
                                togglesbox[$(this).attr("data-subPropertyIndex")] = $(this).val();
                            }
                            if ($(this).is('textarea')) {
                                togglesbox[85] = tinymce.get($(this).attr("id")).getContent();
                            }
                        });
                        togglesboxes.push(togglesbox);
                    });
                    obj[620] = togglesboxes;
                }
                AssignObjectProperties(editorContent, obj);
                dialog.dialog("close");
            },
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
            //form[0].reset();
        }
    });
    dialog.dialog("open");
}