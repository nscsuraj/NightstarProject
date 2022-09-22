if (DisplayChatSMN == 'False') {
    new DESK.Widget({
        version: 1,
        site: 'starmicronics.desk.com',
        port: '80',
        type: 'chat',
        displayMode: 0, //0 for popup, 1 for lightbox
        features: {
            offerAlways: true,
            offerAgentsOnline: false,
            offerRoutingAgentsAvailable: false,
            offerEmailIfChatUnavailable: false
        },
        fields: {
            ticket: {
                // desc: &#x27;&#x27;,
                // labels_new: &#x27;&#x27;,
                // priority: &#x27;&#x27;,
                // subject: &#x27;&#x27;

            },
            interaction: {
                // email: &#x27;&#x27;,
                // name: &#x27;&#x27;

            },
            chat: {
                //subject: '' 

            },
            customer: {
                // company: &#x27;&#x27;,
                // desc: &#x27;&#x27;,
                // first_name: &#x27;&#x27;,
                // last_name: &#x27;&#x27;,
                // locale_code: &#x27;&#x27;,
                // title: &#x27;&#x27;

            }
        }
    }).render();
}


$(document).ready(function() {
    $(".assistly-widget a").attr("onclick", "CheckChatStatus()");
    //$(".assistly-widget a").wrap("<div class='new'>Sexy Shabari</div>");
    $('.assistly-widget').each(function () {
        $(this).children("a").wrap("<div class='chatMinimizer'>Sexy Shabari</div>");
    });
});

function CheckChatStatus(){
	 var url = 'https://www.starmicronics.com/ChatService/GetChatSettings';
	 
	 var html = '<div id="chat_offline_message" style="background-color:#ffffff;text-align:center;float:left;width:550px;padding:10px;word-break: break-all; word-wrap: break-word;position:absolute;top:100px;border:1px solid black;border-radius:4px;box-shadow: 10px 10px 5px #888888;">  <br/><br/><br/>  <h3>Sorry, chat is currently unavailable.  Please call <span style="color:blue;font-weight:bold;">800 782 7636 </span>option 3 or submit an email using this link.    <a href="https://www.starmicronics.com/supports/technicalsupport.aspx" style="color:blue;font-weight:bold;" target="_parent">http://www.starmicronics.com/supports/technicalsupport.aspx</a></h3>  <br/><br/><br/>  <input type="button" value="Close" onclick="javascript:window.close()"/></div>';
	 
    $.ajax({
        type: 'GET',
        url: url,
        datatype: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
			//EnableChatOnGSS
			
			//alert(window.location.href.split('/')[3])
			//alert(window.location.href.indexOf('support/'));
			
			if (window.location.href.indexOf('support/') > 1) {
			
				if (data[i].Key == 'EnableChatOnGSS') {
				//alert(data[i].Key + data[i].Value);
						if (!data[i].Value) {
							var win = window.open("", "Chat Service Status", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=640, height=500");
							win.document.body.innerHTML = html;
							win.document.title = "Star Micronics America";
						}
						else{
							window.open('http://starmicronics.desk.com:80/customer/widget/chats/new?', 'assistly_chat','resizable=1, status=0, toolbar=0,width=640,height=700');
						}
					}
			}
			else{
					if (data[i].Key == 'EnableChatOnSMNSite') {
						//alert(data[i].Key + data[i].Value);
						if (!data[i].Value) {
							var win = window.open("", "Chat Service Status", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=640, height=500");
							win.document.body.innerHTML = html;
							win.document.title = "Star Micronics America";
						}
						else{
							window.open('http://starmicronics.desk.com:80/customer/widget/chats/new?', 'assistly_chat','resizable=1, status=0, toolbar=0,width=640,height=700');
						}
					}
				}
            }
        },
        error: function (data) {
            return false;
        }
	});
}