
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

$(document).ready(function () {
    $("#media-attachment-date-filters").change(function () {
        filterDataSheetToDisplay();
    });
    $("#media-search-input").keyup(function () {
        filterDataSheetToDisplay();
    });

    $("#insertDataSheetIntoPageButton").click(function () {
        $("#dialog_uploadMedia").dialog('close');
    });

    $("#dataSheetDelete").click(function () {
        deleteUploadedDataSheet();
    });

    allowMultiSelect = $("#dialog_uploadMedia").attr("data-allowMultiSelect") === undefined ? false : $("#dialog_uploadMedia").attr("data-allowMultiSelect");
    $("#clearSelectedDataSheet").hide();
    $("#detailDisplayDataSheetDelete").hide();
});

function uploadDataSheet(files) {
    var data = new FormData();

    var okToUpload = true;
    // Add the uploaded image content to the form data collection
    for (var i = 0; i < files.length; i++) {
        if (files[i].size <= 104857600) {
            data.append("UploadedDataSheet" + i, files[i]);
        } else {
            alert("File " + files[i].name + " exceeds the size limit. Please select files having maximum size 100 MB.\n\nUpload cancelled.");
            okToUpload = false;
        }
    }
    if (okToUpload) {
        data.append("UploadedType", currState);
        $.ajax({
            type: 'POST',
            url: root + "api/ppeditor/UploadDataSheet",
            data: data,
            processData: false,
            contentType: false,
            success: function (data) {
                $("#dataSheetLibraryTab").click();
                setUpDataSheetLibrary();
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
}

function setUpDataSheetLibrary() {
    $.ajax({
        type: 'GET',
        url: root + "api/ppeditor/GetUploadDataSheetInformations?currState=" + currState,
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
            $("#detailDisplayDataSheetId").val('');
            $("#detailDisplayDataSheetName").text('');
            $("#detailDisplayDataSheetDate").text('');
            $("#detailDisplayDataSheetSize").text('');
            $("#detailDisplayDataSheetPath").val('');
            $("#noOfDataSheetSelected").html("");
            $("#clearSelectedDataSheet").hide();
            $("#detailDisplayDataSheetDelete").hide();
            $("#detailDisplayDataSheet").attr("src", "");
        },
        error: function (data) {
        }
    });
}

function renderElements(obj) {
    $("#displayContainerOfUploadedDataSheetElements").empty();
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
            $(tmpl.querySelector('.uploadedDataSheetText')).html(obj[j].OriginalFileName);
            $(tmpl.querySelector('.uploadedElement')).attr("data-objId", obj[j].Id);

            if (obj[j].FileType.indexOf("image") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", obj[j].FilePath);
            } else if (obj[j].FileType.indexOf("audio") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", audioImagePath);
            } else if (obj[j].FileType.indexOf("video") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", videoImagePath);
            } else if (obj[j].FileType.indexOf(".document") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", docxImagePath);
            } else if (obj[j].FileType.indexOf(".sheet") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", excelImagePath);
            } else if (obj[j].FileType.indexOf("text/plain") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", txtImagePath);
            } else if (obj[j].FileType.indexOf("pdf") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", docxImagePath);
            } else if (obj[j].FileType.indexOf("html") > -1) {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", htmlImagePath);
            } else {
                $(tmpl.querySelector('.uploadedDataSheetImage')).attr("src", unknownImagePath);
            }
            document.getElementById("displayContainerOfUploadedDataSheetElements").appendChild(tmpl);
        }
    }
}

function uploadedDataSheetClicked(e, ctrl) {
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
        $("#detailDisplayDataSheetId").val(obj[0].Id);
        $("#detailDisplayDataSheetName").text(obj[0].OriginalFileName);
        $("#detailDisplayDataSheetDate").text($.format.date(new Date(obj[0].CreateDate), 'MMM dd yyyy'));
        $("#detailDisplayDataSheetSize").text(Math.round(obj[0].FileSize / 1024) + " KB");
        $("#detailDisplayDataSheetPath").val(obj[0].FilePath);

        $("#noOfDataSheetSelected").html(selectedUploadedObjectsToUse.length + " Selected");

        $("#clearSelectedDataSheet").hide();
        $("#detailDisplayDataSheetDelete").hide();
        if (selectedUploadedObjectsToUse.length > 0) {
            $("#clearSelectedDataSheet").show();
            $("#detailDisplayDataSheetDelete").show();
        }
        $("#detailDisplayDataSheet").attr("src", $(ctrl).find(".uploadedDataSheetImage").attr("src"));
    } catch (e) {

    }

}

function filterDataSheetToDisplay() {
    var filterVal2 = $("#media-attachment-date-filters").val();
    var filterVal3 = $("#media-search-input").val();

    var objs = uploadedElements.Data;
    //filter date
    if (filterVal2 !== "all") {
        objs = $.grep(objs, function(x) {
            var dt = new Date(x.CreateDate);
            var dt2 = new Date(filterVal2);
            return (dt.getMonth() === dt2.getMonth() && dt.getDay() === dt2.getDay() && dt.getYear() === dt2.getYear());
        });
    }

    if (filterVal3 !== "") {
        objs = $.grep(objs, function(x) {
            return (x.FileName.toLowerCase().indexOf(filterVal3.toLowerCase()) > -1);
        });
    }
    renderElements(objs);
}

function deleteUploadedDataSheet() {
    if (currentlySelectedObject.Id != undefined || currentlySelectedObject.Id != null) {
        if (confirm("Do you really want to delete this file?")) {
            $.ajax({
                type: 'POST',
                url: root + "api/ppeditor/DeleteDataSheetInfo",
                data: currentlySelectedObject,
                success: function (data) {
                    setUpDataSheetLibrary();
                },
                error: function (data) {
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
    $("#detailDisplayDataSheetId").val("");
    $("#detailDisplayDataSheetName").text("");
    $("#detailDisplayDataSheetDate").text("");
    $("#detailDisplayDataSheetSize").text("");
    $("#detailDisplayDataSheetPath").val("");
    $("#detailDisplayFileTitle").val("");
    $("#detailDisplayFileCaption").val("");
    $("#detailDisplayFileDescription").val("");
    $("#noOfDataSheetSelected").html("");
    $("#clearSelectedDataSheet").hide();
    $("#detailDisplayDataSheetDelete").hide();
    $("#detailDisplayDataSheet").attr("src", "");
}