
var uploadedElements = {};

var allowMultiSelect = false;

var selectedUploadedObjectsToUse = [];

var currentlySelectedObject = {};

var audioImagePath = "/Images/FileTypes/audio.png";
var docxImagePath = "/Images/FileTypes/docx.png";
var excelImagePath = "/Images/FileTypes/excel.png";
var htmlImagePath = "/Images/FileTypes/html.png";
var txtImagePath = "/Images/FileTypes/txt.png";
var videoImagePath = "/Images/FileTypes/video.png";
var unknownImagePath = "/Images/FileTypes/txt.png";

function uploadMediaFiles(files) {
    var data = new FormData();

    var okToUpload = true;
    // Add the uploaded image content to the form data collection
    for (var i = 0; i < files.length; i++) {
        if (files[i].size <= 52428800) {
            data.append("UploadedImage" + i, files[i]);
        } else {
            alert("File " + files[i].name + " exceeds the size limit. Please select files having maximum size 50 MB.\n\nUpload cancelled.");
            okToUpload = false;
        }
    }

    if (okToUpload) {
        data.append("UploadedType", currState);
        $.ajax({
            type: 'POST',
            url: root + "api/ppeditor/UploadFiles",
            data: data,
            processData: false,
            contentType: false,
            success: function (data) {
                $("#mediaLibraryTab").click();
                setUpMediaLibrary();
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
}

function setUpMediaLibrary() {
    $.ajax({
        type: 'GET',
        url: root + "api/ppeditor/GetUploadInformations",
        success: function (data) {
            uploadedElements = data;
            var s = document.getElementById('media-attachment-date-filters');
            s.options.length = 0;
            s.options[0] = new Option("All Dates", 'all');
            for (var i = 0; i < data.Dates.length; i++) {
                //Fill date filter dropdown
                s.options[s.options.length] = new Option($.format.date(new Date(data.Dates[i]), 'MMM dd yyyy'), new Date(data.Dates[i]));
            }
            renderElements(uploadedElements.Data);
            if (selectedUploadedObjectsToUse.length > 0) {
                for (var i = 0; i < selectedUploadedObjectsToUse.length; i++) {
                    $(".uploadedElement").each(function() {
                        if ($(this).attr("data-objId") == selectedUploadedObjectsToUse[i].Id) {
                            $(this).click();
                        }
                    });
                }
            }
            if (currentlySelectedObject.Id != undefined || currentlySelectedObject.ID != null) {
                $(".uploadedElement").each(function () {
                    if ($(this).attr("data-objId") == currentlySelectedObject.Id) {
                        $(this).click();
                    }
                });
            }


            $("#detailDisplayFileId").val('');
            $("#detailDisplayFileName").text('');
            $("#detailDisplayFileDate").text('');
            $("#detailDisplayFileSize").text('');
            $("#detailDisplayFilePath").val('');
            $("#detailDisplayFileTitle").val('');
            $('#detailDisplayFileImgAltText').val('');

            $("#detailDisplayFileCaption").val('');
            $("#detailDisplayFileDescription").val('');
            $("#noOfFilesSelected").html("");
            $("#clearSelectedFiles").hide();
            $("#detailDisplayFileDelete").hide();
            
            $("#detailDisplayFileImage").attr("src", "");
        },
        error: function (data) {
        }
    });
}

function renderElements(obj) {
    
    $("#displayContainerOfUploadedElements").empty();
    //alert(obj.length);
    if (obj.length === 0) {
        $("#displayElementContainerTd").css("vertical-align", "middle");
        $("#displayElementContainerTd").css("text-align", "center");
        $("#displayElementNoItem").show();
    } else {
        $("#displayElementContainerTd").css("vertical-align", "top");
        $("#displayElementContainerTd").css("text-align", "left");
        $("#displayElementNoItem").hide();
        for (var j = 0; j < obj.length; j++) {
            //Fill date filter dropdown
            var tmpl = document.getElementById('uploaded_element_display_template').content.cloneNode(true);
            $(tmpl.querySelector('.uploadedElementText')).html(obj[j].OriginalFileName);
            $(tmpl.querySelector('.uploadedElement')).attr("data-objId", obj[j].Id);

            if (obj[j].FileType.indexOf("image") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", obj[j].FilePath);
            } else if (obj[j].FileType.indexOf("audio") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", audioImagePath);
            } else if (obj[j].FileType.indexOf("video") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", videoImagePath);
            } else if (obj[j].FileType.indexOf(".document") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", docxImagePath);
            } else if (obj[j].FileType.indexOf(".sheet") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", excelImagePath);
            } else if (obj[j].FileType.indexOf("text/plain") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", txtImagePath);
            } else if (obj[j].FileType.indexOf("pdf") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", docxImagePath);
            } else if (obj[j].FileType.indexOf("html") > -1) {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", htmlImagePath);
            } else {
                $(tmpl.querySelector('.uploadedElementImage')).attr("src", unknownImagePath);
            }
            document.getElementById("displayContainerOfUploadedElements").appendChild(tmpl);
        }
    }
}

$(document).ready(function() {
    $("#media-attachment-filters").change(function () {
        filterElementsToDisplay();
    });
    $("#media-attachment-date-filters").change(function () {
        filterElementsToDisplay();
    });
    $("#media-search-input").keyup(function () {
        filterElementsToDisplay();
    });

    $("#insertElementIntoPageButton").click(function() {
        $("#dialog_uploadMedia").dialog('close');
    });

    $("#fileDelete").click(function () {
        alert(1);
        deleteUploadedFile();
    });

    allowMultiSelect = $("#dialog_uploadMedia").attr("data-allowMultiSelect") === undefined ? false : $("#dialog_uploadMedia").attr("data-allowMultiSelect");
    $("#clearSelectedFiles").hide();
    $("#detailDisplayFileDelete").hide();
});

function uploadedElementClicked(e, ctrl) {
    try {

        var objs = uploadedElements.Data;
        var obj = {};
        if ((allowMultiSelect === true) && (e.ctrlKey === true)) {
            obj = $.grep(objs, function(x) {
                return (x.Id == $(ctrl).attr("data-objId"));
            });
            var okToPush = true;
            for (var i = 0; i < selectedUploadedObjectsToUse.length; i++) {
                if (selectedUploadedObjectsToUse[i].ID === obj[0].Id) {
                    okToPush = false;
                }
            }
            if (okToPush === true) {
                selectedUploadedObjectsToUse.push(obj[0]);
            }
            $(ctrl).find(".buttonCheck").addClass("show");
            $(ctrl).find(".buttonCheck").removeClass("hide");
        } else {
            selectedUploadedObjectsToUse = [];
            obj = $.grep(objs, function (x) {
                return (x.Id == $(ctrl).attr("data-objId"));
            });
            selectedUploadedObjectsToUse.push(obj[0]);
            $(".buttonCheck").each(function () {
                $(this).removeClass("show");
                $(this).addClass("hide");
            });
            $(ctrl).find(".buttonCheck").addClass("show");
            $(ctrl).find(".buttonCheck").removeClass("hide");
        }
        currentlySelectedObject = obj[0];
        $("#detailDisplayFileId").val(obj[0].Id);
        $("#detailDisplayFileName").text(obj[0].OriginalFileName);
        $("#detailDisplayFileDate").text($.format.date(new Date(obj[0].CreateDate), 'MMM dd yyyy'));
        $("#detailDisplayFileSize").text(Math.round(obj[0].FileSize / 1024) + " KB");
        $("#detailDisplayFilePath").val(obj[0].FilePath);
        $("#detailDisplayFileTitle").val(obj[0].Title == null ? "" : obj[0].Title);
    
        $("#detailDisplayFileCaption").val(obj[0].Caption == null ? "" : obj[0].Caption);
        $("#detailDisplayFileDescription").val(obj[0].Description == null ? "" : obj[0].Description);
        $("#detailDisplayFileImgAltText").val(obj[0].AltText == null ? "" : obj[0].AltText);

        $("#noOfFilesSelected").html(selectedUploadedObjectsToUse.length + " Selected");

        $("#clearSelectedFiles").hide();
        $("#detailDisplayFileDelete").hide();
        if (selectedUploadedObjectsToUse.length > 0) {
            $("#clearSelectedFiles").show();
            $("#detailDisplayFileDelete").show();
        }
        $("#detailDisplayFileImage").attr("src", $(ctrl).find(".uploadedElementImage").attr("src"));


    } catch (e) {

    }

}

function filterElementsToDisplayFromLeftClick(type) {
    $("#media-attachment-filters").val(type);
    filterElementsToDisplay();
}

function filterElementsToDisplay() {
    var filterVal1 = $("#media-attachment-filters").val();
    var filterVal2 = $("#media-attachment-date-filters").val();
    var filterVal3 = $("#media-search-input").val();

    var objs = uploadedElements.Data;
    switch (filterVal1) {
        case "all":
            break;
        //case "uploaded":
        //    objs = $.grep(objs, function (x) {
        //        return (x.UploadType === currState);
        //    });
        //    break;
        case "image":
            objs = $.grep(objs, function (x) {
                return (x.FileType.indexOf("image") > -1);
            });
            break;
        case "audio":
            objs = $.grep(objs, function (x) {
                return (x.FileType.indexOf("audio") > -1);
            });
            break;
        case "video":
            objs = $.grep(objs, function (x) {
                return (x.FileType.indexOf("video") > -1);
            });
            break;
        case "unattached":
            objs = $.grep(objs, function (x) {
                return (x.IsAttached === false);
            });
            break;
        case "ebooks":
            objs = $.grep(objs, function (x) {
                return (x.UploadType === 2);
            });
            break;
        case "ebookImages":
            objs = $.grep(objs, function (x) {
                return (x.UploadType === 3);
            });
            break;
    }
    //filter date
    if (filterVal2 !== "all") {
        objs = $.grep(objs, function(x) {
            var dt = new Date(x.CreateDate);
            var dt2 = new Date(filterVal2);
            return (dt.getMonth() === dt2.getMonth() && dt.getDay() === dt2.getDay() && dt.getYear() === dt2.getYear());
        });
    }

    if (filterVal3 !== "") {
        objs = $.grep(objs, function (x) {
            return (x.FileName.toLowerCase().indexOf(filterVal3.toLowerCase()) > -1);
        });
    }
    renderElements(objs);
}

function updateFileInfo() {
    if (currentlySelectedObject.Id != undefined || currentlySelectedObject.Id != null) {
        $(".updateFileInfo").each(function() {
            var ctrl = this;
            currentlySelectedObject[$(ctrl).attr("data-property")] = $(ctrl).val();
        });
            
        $.ajax({
            type: 'POST',
            url: root + "api/ppeditor/UpdateFileInfo",
            data: currentlySelectedObject,
            success: function(data) {
                setUpMediaLibrary();
            },
            error: function(data) {
                //alert(data.responseText);
            }
        });
    }
}

function deleteUploadedFile() {
    if (currentlySelectedObject.Id != undefined || currentlySelectedObject.Id != null) {
        if (confirm("Do you really want to delete this file?")) {
            $.ajax({
                type: 'POST',
                url: root + "api/ppeditor/DeleteFileInfo",
                data: currentlySelectedObject,
                success: function (data) {
                    setUpMediaLibrary();



                },
                error: function (data) {
                    //alert(data.responseText);
                }
            });
        }
        

    }
}

function clearSelection() {
    $(".buttonCheck").each(function () {
        $(this).removeClass("show");
        $(this).addClass("hide");
    });
    selectedUploadedObjectsToUse = [];
    currentlySelectedObject = {};

    $("#detailDisplayFileId").val("");
    $("#detailDisplayFileName").text("");
    $("#detailDisplayFileDate").text("");
    $("#detailDisplayFileSize").text("");
    $("#detailDisplayFilePath").val("");
    $("#detailDisplayFileTitle").val("");

    $("#detailDisplayFileCaption").val("");
    $("#detailDisplayFileDescription").val("");

    $("#noOfFilesSelected").html("");

    $("#clearSelectedFiles").hide();
    $("#detailDisplayFileDelete").hide();
    $("#detailDisplayFileImage").attr("src", "");

}