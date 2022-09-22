
$(document).ready(function () {
    var url = 'https://www.starmicronics.com/ChatService/GetChatSettings';
    $.ajax({
        type: 'GET',
        url: url,
        datatype: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (document.referrer.split('/')[3] == 'support') {
                    if (data[i].Key == 'EnableChatOnGSS') {
                        if (data[i].Value == 'True') {
                            $("#chat_offline_message").show();
                            $("#customer_widget_main").hide();
                        }
                    }
                }
                else {

                    if (data[i].Key == 'EnableChatOnSMNSite') {
                        if (data[i].Value == 'True') {
                            $("#chat_offline_message").show();
                            $("#customer_widget_main").hide();
                        }
                    }

                }
            }
        },
        error: function (data) {
            return false;
        }
    });
});