// ---------------------------------------------------//
//Author: Carlos A. Paulino---------------------------//
//Email: cpaulino@gmail.com---------------------------//
// ---------------------------------------------------//

function SynchronousWebRequest(webmethod, webdata) {
    var returnValue;
    $.ajax({
        type: "POST",
        url: webmethod,
        data: webdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            returnValue = data.d;
        },
        fail: function () {
            returnValue = 'ERR';
        },
        async: false
    });
    return returnValue;
}

function ASynchronousWebRequest(webmethod, webdata, callback, currentmodal) {
    $('#modal-loading').modal({ "backdrop": "static", keyboard: false });
    if (currentmodal != undefined) {
        currentmodal.modal('hide');
    }
    $.ajax({
        type: "POST",
        url: webmethod,
        data: webdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            callback(data.d);
            $('#modal-loading').modal('hide')
            if (currentmodal != undefined) {
                currentmodal.modal('show');
            }
        },
        fail: function () {
            callback(data.d);
            $('#modal-loading').modal('hide');
            $('#modal-loading').modal('show');
        }
    });
}

function UpdateDisplay(dataobject, container, prefix) {
    for (var propertyName in dataobject) {
        var control = $(container).find('#' + prefix + propertyName);

        if (control.is(':checkbox') || control.is(':radio')) {
            if (dataobject[propertyName] != '') {
                control.prop('checked', true);
            }
        } else if (control.is(':text') || control.is('select') || control.is('textarea')) {
            control.val(dataobject[propertyName]);
        } else if (control.is('div')) {
            // it's a checkbox group or radio group
            // we split the string and bind by checkbox value
            var a = dataobject[propertyName].split(',');
            control.find('input:checkbox').prop('checked', false);
            for (i = 0; i < a.length; i++) {
                control.find('input[value=' + a[i] + ']').prop('checked', true);
            }
        }
    }
}

function UpdateModel(container) {
    return JSON.stringify({ data: $(container).find('input, textarea, button, select').serialize() })
}

function SerializeValue(value) {
    return JSON.stringify({ data: value });
}

function GetCurrentUrl() {
    if (window.location.href.indexOf('#') == -1) {
        return window.location.href;
    } else {
        return window.location.href.split('#')[0];
    }
}

function ModalMessage(title, message, alerttype, relocate) {
    // Setup the modal dialog
    var modal = $('#modal-message');
    modal.modal({ "backdrop": "static", keyboard: false, show: true });

    // Customize
    modal.find('.modal-header').find('h3').html(title);
    modal.find('.modal-body').html(title);
    modal.find('.modal-footer').find('a').addClass(alerttype);

    // Prevent infinite loop
    if (relocate != undefined && relocate != '' && relocate != window.location.href) {
        modal.on('hide', function () {
            window.location = relocate;
        })
    }
}

function HTMLMessage(message, alerttype) {
    var a = $('#alert-styled');
    a.addClass(alerttype);
    a.html('<button class=\"close\" data-dismiss=\"alert\">&times;</button>' + message);
}

function jSONdate(value) {
    return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
}

function GetBaseUrl() {
    return window.location.hostname;
}

/// <summary>
/// Resets all error messages and all input fields within a container
/// </summary>
/// <param name="container">Selector</param>
/// <param name="clearvalue">True to clear the value of the inputs</param>
/// <returns></returns>
function validationResetInputFields(container, clearvalue) {
    $(container + ' .form-group').each(function (i) {
        if (clearvalue) {
            $(this).find(':input[type=text]').val('');
            $(this).find(':input[type=checkbox]').removeAttr('checked');
        }
        $(this).removeClass('has-error');
        $(this).find('.help-block').html('');
    });
}

/// <summary>
/// Checks if the value is not empty
/// </summary>
/// <param name="a">Selector</param>
/// <param name="m">Error message</param>
/// <returns>true|false</returns>
function validationCheckEmpty(a, m) {
    if ($.trim(a.val()) === '') {
        a.closest('.form-group').addClass('has-error');
        a.next().html(m);
        return true;
    } else {
        a.closest('.form-group').removeClass('has-error');
        a.next().html('');
        return false;
    }
}

/// <summary>
/// Checks if the second element (b) value is equal to the first element (a)
/// </summary>
/// <param name="a">Selector</param>
/// <param name="b">Selector</param>
/// <param name="m">Error message</param>
/// <returns>true|false</returns>
function validationCheckEqual(a, b, m) {
    if (a.val() != b.val()) {
        b.closest('.form-group').addClass('has-error');
        b.next().html(m);
        return false;
    }
    else {
        b.closest('.form-group').removeClass('has-error');
        b.next().html('');
        return true;
    }
}

/// <summary>
/// Checks if the element (a) meets the min and max lenghts
/// </summary>
/// <param name="a">Selector</param>
/// <param name="m">Error Message</param>
/// <param name="min">Min length of field</param>
/// <param name="max">Max length of field</param>
/// <returns>true|false</returns>
function validationCheckLength(a, min, max) {
    if (a.val().length > max || a.val().length < min) {
        a.closest('.form-group').addClass('has-error');
        a.next().html("Length must be between " + min + " and " + max + ".");
        return false;
    } else {
        a.closest('.form-group').removeClass('has-error');
        a.next().html('');
        return true;
    }
}

/// <summary>
/// Checks if the element value (a) is equal to variable (b)
/// </summary>
/// <param name="a">Selector</param>
/// <param name="b">Value</param>
/// <param name="m">Error message</param>
/// <returns>true|false</returns>
function validationCheckValue(a, b, m) {
    if (a.val() != b) {
        b.closest('.form-group').addClass('has-error');
        b.next().html(m);
        return false;
    }
    else {
        b.closest('.form-group').removeClass('has-error');
        b.next().html('');
        return true;
    }
}

/// <summary>
/// Checks if at least one input check is checked
/// </summary>
/// <param name="s">Container</param>
/// <param name="m">Error message</param>
/// <returns>true|false</returns>
function validationCheckboxChecked(s, m) {
    var r = false;
    $(s).find(':input[type=checkbox]').each(function () {
        if ($(this).attr('checked')) {
            r = true;
        }

    });
    if (!r) {
        s.closest('.form-group').addClass('has-error');
        s.find('.help-block').html(m);
    }
    else {
        s.closest('.form-group').removeClass('has-error');
        s.find('.help-block').html('');
    }
    return r;
}

// String.Format in javascript
// First, checks if it isn't implemented yet.
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

function OpenInNewTab(url) {
    window.open(url, '_blank');
}