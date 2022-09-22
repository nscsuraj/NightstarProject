using PartnerPortal.Domain.CMS;
using PartnerPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using PartnerPortal.Domain.Import;
using PartnerPortal.Domain.SiteUtility;

namespace PartnerPortal.Utility
{
    public class JsonToHtmlParser
    {
        private IEFRepository<CMS_ElementProperty> _cmsElementProperty;
        private IEFRepository<ProductDetails> _productDetails;
        private IEFRepository<UploadInformation> _uploadInformationRepository;
        private StringBuilder parsedHtml = null;
        private bool sliderLoaded = false;
        private bool layerSliderLoaded = false;
        private bool imageHorCarouselLoaded = false;
        private bool imageVerCarouselLoaded = false;
        private bool pricingTableLoaded = false;
        private bool sliderWithThumbLoaded = false;
        private bool lightSliderLoaded = false;

        public JsonToHtmlParser(IEFRepository<CMS_ElementProperty> cmsElementProperty, 
            IEFRepository<ProductDetails> productDetails,
            IEFRepository<UploadInformation> uploadInformationRepository)
        {
            parsedHtml = new StringBuilder();
            _cmsElementProperty = cmsElementProperty;
            _productDetails = productDetails;
            _uploadInformationRepository = uploadInformationRepository;
        }

        public string Parse(dynamic obj)
        {
            var html = string.Empty;
            foreach (var item in obj.Children)
            {
                html += DoParse(item, string.Empty);
            }
            return html;
        }

        private string DoParse(dynamic obj, string html)
        {
            var tempHtml = ChooseParser(obj);
            var h = string.Empty;
            foreach (var item in obj.Children)
            {
                h += DoParse(item, h);
            }
           return tempHtml.Replace("Child", h);
        }

        private string ChooseParser(dynamic obj)
        {
            string type = obj.Type;
            switch (type.ToUpper())
            {
                case "FULLWIDTHCONTAINER":
                    return ParseFullWidthContainer(obj);
                case "GRIDONECONTAINER":
                    return ParseGridOneContainer(obj);
                case "GRIDTWOCONTAINER":
                    return ParseGridTwoContainer(obj);
                case "GRIDTHREECONTAINER":
                    return ParseGridThreeContainer(obj);
                case "GRIDTWOTHIRDCONTAINER":
                    return ParseGridTwoThirdContainer(obj);
                case "GRIDFOURCONTAINER":
                    return ParseGridFourContainer(obj);
                case "GRIDFIVESIXTHCONTAINER":
                    return ParseGridFiveSixthContainer(obj);
                case "GRIDSIXCONTAINER":
                    return ParseGridSixContainer(obj);
                case "GRIDFOURFIFTHCONTAINER":
                      return ParseGridFourFifthContainer(obj);
                case "GRIDTHREEFIFTHCONTAINER":
                      return ParseGridThreeFifthContainer(obj);
                case "GRIDTWOFIFTHCONTAINER":
                    return ParseGridTwoFifthContainer(obj);
                case "GRIDFIVECONTAINER":
                    return ParseGridFiveContainer(obj);
                case "GRIDTHREEFOURTHCONTAINER":
                    return ParseGridThreeFourthContainer(obj);                    
                case "TEXTBOX":
                    return ParseTextBox(obj);
                case "TITLEBOX":
                    return ParseTitleBox(obj);
                case "VIDEOYOUTUBE":
                    return ParseYouTubeVideo(obj);
                case "VIDEOVIMEO":
                    return ParseVimeoVideo(obj);
                case "TOGGLESBOX":
                    return ParseTogglesBox(obj);
                case "MENUANCHOR":
                    return ParseMenuAnchor(obj);
                case "ALERTBOX":
                    return ParseAlertBox(obj);
                case "SOUNDCLOUD":
                    return ParseSoundCloud(obj);
                case "CHECKLIST":
                    return ParseCheckList(obj);
                case "SHARINGBOX":
                    return ParseSharingBox(obj);
                case "TESTIMONIALBOX":
                    return ParseTestimonialBox(obj);
                case "TAGLINEBOX":
                    return ParseTaglineBox(obj);
                case "MODAL":
                    return ParseModalBox(obj);
                case "BUTTONBLOCK":
                    return ParseButtonBlock(obj);
                case "CONTENTBOXES":
                    return ParseContentBoxes(obj);
                case "COUNTDOWNBOX":
                    return ParseCountdownBox(obj);
                case "COUNTERBOX":
                    return ParseCounterBox(obj);
                case "FUSIONCODE":
                    return ParseFusionCode(obj);
                case "TABSBOX":
                    return ParseTabsBox(obj);
                case "TABLEBOX":
                    return ParseTableBox(obj);
                case "SLIDERELEMENT":
                    return ParseSlider(obj);
                case "LAYERSLDIER":
                    return ParseLayerSlider(obj);
                case "SOCIALLINKS":
                    return ParseSocialIcons(obj);
                case "SEPARATORELEMENT":
                    return ParseSeparatorElement(obj);
                case "SECTIONSEPARATOR":
                    return ParseSectionSeparator(obj);
                case "GOOGLEMAP":
                    return ParseGoogleMap(obj);
                case "FLIPBOXES":
                    return ParseFlipBox(obj);
                case "IMAGEFRAME":
                    return ParseImageFrame(obj);
                case "IMAGECAROUSEL":
                    return ParseImageCarousel(obj);
                case "PRICINGTABLE":
                    return ParsePricingTable(obj);
                case "VIDEO":
                    return ParseVideo(obj);
                case "SLIDERWITHTHUMB":
                    return ParseSliderWithThumb(obj);
                    //return ParseLightSlider(obj);
                case "BUTTONWITHICON":
                    return ParseButtonWithIcon(obj);
                case "PAGESCROLLER":
                    return ParsePageScroller(obj);
                case "BACKTOTOPBLOCK":
                    return ParseBackToTop(obj);
                case "APN":
                    return ParseAPN(obj);
                case "IMAGELIBRARY":
                    return ParseImageLibrary(obj);
                default:
                    return string.Empty;
            }
        }

        public string ParseFullWidthContainer(dynamic obj)
        {
            var html = @"<div class='{0}fusion-fullwidth fullwidth-box' style='{1}-webkit-background-size:cover;-moz-background-size:cover;-o-background-size:cover;background-size:cover;' id='{2}'>{3}<div class='fusion-row'>Child</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = string.Empty; 
            if (obj["1"] == "yes")
            {
                css = "hundred-percent-fullwidth ";
            }
            if (obj["2"] == "yes")
            {
                css += "fusion-equal-height-columns ";
            }
            if (obj["3"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["26"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["4"] != string.Empty && obj["4"] != null)
            {
                html = "<div id='" + obj["4"] + "'>" + html + "</div>";
            }
            if (obj["5"] != string.Empty && obj["5"] != null)
            {
                css += obj["5"] + " ";
            }
            if (obj["6"] != string.Empty && obj["6"] != null)
            {
                divId = obj["6"];
            }
            if (obj["7"] != string.Empty && obj["7"] != null)
            {
                style += "background-color:" + obj["7"] + ";";
            }
            if (obj["8"] != string.Empty && obj["8"] != null)
            {
                style += "background-image:url(" + obj["8"] + ");";
            }
            if (obj["12"] != string.Empty && obj["12"] != null)
            {
                style += "background-repeat:" + obj["12"] + ";";
            }
            if (obj["13"] != string.Empty && obj["13"] != null)
            {
                style += "background-position:" + obj["13"] + ";";
            }
            string s = Convert.ToString(obj["25"]);
            style += "border:" + obj["25"] + " " + obj["28"] + " " + obj["27"] + ";";
            if (obj["29"] != string.Empty && obj["29"] != null)
            {
                if (!obj["29"].ToString().Contains("px"))
                {
                    style += "padding-top:" + obj["29"] + "px;";
                }
                else
                {
                    style += "padding-top:" + obj["29"] + ";";
                }
            }
            if (obj["30"] != string.Empty && obj["30"] != null)
            {
                if (!obj["30"].ToString().Contains("px"))
                {
                    style += "padding-bottom:" + obj["30"] + "px;";
                }
                else
                {
                    style += "padding-bottom:" + obj["30"] + ";";
                }
            }
            if (obj["31"] != string.Empty && obj["31"] != null)
            {
                style += "padding-left:" + obj["31"] + ";";
            }
            if (obj["33"] != string.Empty && obj["33"] != null)
            {
                style += "padding-right:" + obj["33"] + ";";
            }
            return string.Format(html, css, style, divId, content);
        }

        public string ParseGridOneContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-one-full fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["34"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["35"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["36"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["37"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["52"] != string.Empty && obj["52"] != null)
            {
                css += obj["52"] + " ";
            }
            if (obj["53"] != string.Empty && obj["53"] != null)
            {
                divId = obj["53"];
            }
            if (obj["39"] != string.Empty && obj["39"] != null)
            {
                style += "background-color:" + obj["39"] + ";";
            }
            if (obj["40"] != string.Empty && obj["40"] != null)
            {
                style += "background-image:url(" + obj["40"] + ");";
            }
            if (obj["41"] != string.Empty && obj["41"] != null)
            {
                style += "background-repeat:" + obj["41"] + ";";
            }
            if (obj["42"] != string.Empty && obj["42"] != null)
            {
                style += "background-position:" + obj["42"] + ";";
            }
            style += "border:" + obj["43"] + " " + obj["44"] + " " + obj["45"] + ";";
            if (obj["46"] != string.Empty && obj["46"] != null)
            {
                style += "padding:" + obj["46"] + ";";
            }
            if (obj["47"] != string.Empty && obj["47"] != null)
            {
                style += "margin-top:" + obj["47"] + ";";
            }
            if (obj["48"] != string.Empty && obj["48"] != null)
            {
                style += "margin-bottom:" + obj["48"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridTwoContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-one-half fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["55"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["56"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["57"] == "yes")
            {
                style += "text-align: center; ";
            }
            else if(obj["76"] == "yes")
            {
                style += "text-align: right; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["58"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["73"] != string.Empty && obj["73"] != null)
            {
                css += obj["73"] + " ";
            }
            if (obj["74"] != string.Empty && obj["74"] != null)
            {
                divId = obj["74"];
            }
            if (obj["59"] != string.Empty && obj["59"] != null)
            {
                style += "background-color:" + obj["59"] + ";";
            }
            if (obj["60"] != string.Empty && obj["60"] != null)
            {
                style += "background-image:url(" + obj["60"] + ");";
            }
            if (obj["61"] != string.Empty && obj["61"] != null)
            {
                style += "background-repeat:" + obj["61"] + ";";
            }
            if (obj["62"] != string.Empty && obj["62"] != null)
            {
                style += "background-position:" + obj["62"] + ";";
            }
            style += "border:" + obj["63"] + " " + obj["65"] + " " + obj["64"] + ";";
            if (obj["66"] != string.Empty && obj["66"] != null)
            {
                style += "padding-left:" + obj["66"] + ";";
            }
            if (obj["77"] != string.Empty && obj["77"] != null)
            {
                style += "padding-right:" + obj["77"] + ";";
            }
            if (obj["78"] != string.Empty && obj["78"] != null)
            {
                style += "padding-top:" + obj["78"] + ";";
            }
            if (obj["79"] != string.Empty && obj["79"] != null)
            {
                style += "padding-bottom:" + obj["79"] + ";";
            }
            if (obj["67"] != string.Empty && obj["67"] != null)
            {
                style += "margin-top:" + obj["67"] + ";";
            }
            if (obj["69"] != string.Empty && obj["69"] != null)
            {
                style += "margin-bottom:" + obj["69"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridThreeContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-one-third one_third fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["75"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["76"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["77"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["78"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["92"] != string.Empty && obj["92"] != null)
            {
                css += obj["92"] + " ";
            }
            if (obj["93"] != string.Empty && obj["93"] != null)
            {
                divId = obj["93"];
            }
            if (obj["79"] != string.Empty && obj["79"] != null)
            {
                style += "background-color:" + obj["79"] + ";";
            }
            if (obj["80"] != string.Empty && obj["80"] != null)
            {
                style += "background-image:url(" + obj["80"] + ");";
            }
            if (obj["81"] != string.Empty && obj["81"] != null)
            {
                style += "background-repeat:" + obj["81"] + ";";
            }
            if (obj["82"] != string.Empty && obj["82"] != null)
            {
                style += "background-position:" + obj["82"] + ";";
            }
            style += "border:" + obj["83"] + " " + obj["85"] + " " + obj["84"] + ";";
            if (obj["86"] != string.Empty && obj["86"] != null)
            {
                style += "padding:" + obj["86"] + ";";
            }
            if (obj["87"] != string.Empty && obj["87"] != null)
            {
                style += "margin-top:" + obj["87"] + ";";
            }
            if (obj["88"] != string.Empty && obj["88"] != null)
            {
                style += "margin-bottom:" + obj["88"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridTwoThirdContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-two-third fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["95"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["96"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["97"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["98"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["112"] != string.Empty && obj["112"] != null)
            {
                css += obj["112"] + " ";
            }
            if (obj["113"] != string.Empty && obj["113"] != null)
            {
                divId = obj["113"];
            }
            if (obj["99"] != string.Empty && obj["99"] != null)
            {
                style += "background-color:" + obj["99"] + ";";
            }
            if (obj["100"] != string.Empty && obj["100"] != null)
            {
                style += "background-image:url(" + obj["100"] + ");";
            }
            if (obj["101"] != string.Empty && obj["101"] != null)
            {
                style += "background-repeat:" + obj["101"] + ";";
            }
            if (obj["102"] != string.Empty && obj["102"] != null)
            {
                style += "background-position:" + obj["102"] + ";";
            }
            style += "border:" + obj["103"] + " " + obj["105"] + " " + obj["104"] + ";";
            if (obj["106"] != string.Empty && obj["106"] != null)
            {
                style += "padding:" + obj["106"] + ";";
            }
            if (obj["107"] != string.Empty && obj["107"] != null)
            {
                style += "margin-top:" + obj["107"] + ";";
            }
            if (obj["108"] != string.Empty && obj["108"] != null)
            {
                style += "margin-bottom:" + obj["108"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridFourContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-one-fourth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["114"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["115"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["117"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["118"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["133"] != string.Empty && obj["133"] != null)
            {
                css += obj["133"] + " ";
            }
            if (obj["134"] != string.Empty && obj["134"] != null)
            {
                divId = obj["134"];
            }
            if (obj["119"] != string.Empty && obj["119"] != null)
            {
                style += "background-color:" + obj["119"] + ";";
            }
            if (obj["120"] != string.Empty && obj["120"] != null)
            {
                style += "background-image:url(" + obj["120"] + ");";
            }
            if (obj["121"] != string.Empty && obj["121"] != null)
            {
                style += "background-repeat:" + obj["121"] + ";";
            }
            if (obj["122"] != string.Empty && obj["122"] != null)
            {
                style += "background-position:" + obj["122"] + ";";
            }
            style += "border:" + obj["123"] + " " + obj["125"] + " " + obj["124"] + ";";
            if (obj["126"] != string.Empty && obj["126"] != null)
            {
                style += "padding:" + obj["126"] + ";";
            }
            if (obj["127"] != string.Empty && obj["127"] != null)
            {
                style += "margin-top:" + obj["127"] + ";";
            }
            if (obj["128"] != string.Empty && obj["128"] != null)
            {
                style += "margin-bottom:" + obj["128"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridFiveSixthContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-five-sixth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["257"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["258"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["259"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["260"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["275"] != string.Empty && obj["275"] != null)
            {
                css += obj["275"] + " ";
            }
            if (obj["276"] != string.Empty && obj["276"] != null)
            {
                divId = obj["276"];
            }
            if (obj["261"] != string.Empty && obj["261"] != null)
            {
                style += "background-color:" + obj["261"] + ";";
            }
            if (obj["262"] != string.Empty && obj["262"] != null)
            {
                style += "background-image:url(" + obj["262"] + ");";
            }
            if (obj["263"] != string.Empty && obj["263"] != null)
            {
                style += "background-repeat:" + obj["263"] + ";";
            }
            if (obj["264"] != string.Empty && obj["264"] != null)
            {
                style += "background-position:" + obj["264"] + ";";
            }
            style += "border:" + obj["266"] + " " + obj["268"] + " " + obj["267"] + ";";
            if (obj["269"] != string.Empty && obj["269"] != null)
            {
                style += "padding:" + obj["269"] + ";";
            }
            if (obj["270"] != string.Empty && obj["270"] != null)
            {
                style += "margin-top:" + obj["270"] + ";";
            }
            if (obj["271"] != string.Empty && obj["271"] != null)
            {
                style += "margin-bottom:" + obj["271"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridSixContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-one-sixth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["237"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["238"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["239"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["240"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["255"] != string.Empty && obj["255"] != null)
            {
                css += obj["255"] + " ";
            }
            if (obj["256"] != string.Empty && obj["256"] != null)
            {
                divId = obj["256"];
            }
            if (obj["241"] != string.Empty && obj["241"] != null)
            {
                style += "background-color:" + obj["241"] + ";";
            }
            if (obj["242"] != string.Empty && obj["242"] != null)
            {
                style += "background-image:url(" + obj["242"] + ");";
            }
            if (obj["243"] != string.Empty && obj["243"] != null)
            {
                style += "background-repeat:" + obj["243"] + ";";
            }
            if (obj["244"] != string.Empty && obj["244"] != null)
            {
                style += "background-position:" + obj["244"] + ";";
            }
            style += "border:" + obj["245"] + " " + obj["248"] + " " + obj["247"] + ";";
            if (obj["249"] != string.Empty && obj["249"] != null)
            {
                style += "padding-left:" + obj["249"] + ";";
            }
            if (obj["250"] != string.Empty && obj["250"] != null)
            {
                style += "margin-top:" + obj["250"] + ";";
            }
            if (obj["251"] != string.Empty && obj["251"] != null)
            {
                style += "margin-bottom:" + obj["251"] + ";";
            }
            if (obj["258"] != string.Empty && obj["258"] != null)
            {
                style += "padding-right:" + obj["258"] + ";";
            }
            if (obj["259"] != string.Empty && obj["259"] != null)
            {
                style += "padding-top:" + obj["259"] + ";";
            }
            if (obj["260"] != string.Empty && obj["260"] != null)
            {
                style += "padding-bottom:" + obj["260"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridFourFifthContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-four-fifth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["216"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["217"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["218"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["219"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["234"] != string.Empty && obj["234"] != null)
            {
                css += obj["234"] + " ";
            }
            if (obj["236"] != string.Empty && obj["236"] != null)
            {
                divId = obj["236"];
            }
            if (obj["220"] != string.Empty && obj["220"] != null)
            {
                style += "background-color:" + obj["220"] + ";";
            }
            if (obj["221"] != string.Empty && obj["221"] != null)
            {
                style += "background-image:url(" + obj["221"] + ");";
            }
            if (obj["222"] != string.Empty && obj["222"] != null)
            {
                style += "background-repeat:" + obj["222"] + ";";
            }
            if (obj["223"] != string.Empty && obj["223"] != null)
            {
                style += "background-position:" + obj["223"] + ";";
            }
            string s = Convert.ToString(obj["224"]);
            style += "border:" + obj["224"] + " " + obj["226"] + " " + obj["225"] + ";";
            if (obj["227"] != string.Empty && obj["227"] != null)
            {
                style += "padding:" + obj["227"] + ";";
            }
            if (obj["228"] != string.Empty && obj["228"] != null)
            {
                style += "margin-top:" + obj["228"] + ";";
            }
            if (obj["230"] != string.Empty && obj["230"] != null)
            {
                style += "margin-bottom:" + obj["230"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridThreeFifthContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-three-fifth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["195"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["196"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["197"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["198"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["214"] != string.Empty && obj["214"] != null)
            {
                css += obj["214"] + " ";
            }
            if (obj["215"] != string.Empty && obj["215"] != null)
            {
                divId = obj["215"];
            }
            if (obj["199"] != string.Empty && obj["199"] != null)
            {
                style += "background-color:" + obj["199"] + ";";
            }
            if (obj["201"] != string.Empty && obj["201"] != null)
            {
                style += "background-image:url(" + obj["201"] + ");";
            }
            if (obj["202"] != string.Empty && obj["202"] != null)
            {
                style += "background-repeat:" + obj["202"] + ";";
            }
            if (obj["203"] != string.Empty && obj["203"] != null)
            {
                style += "background-position:" + obj["203"] + ";";
            }
            string s = Convert.ToString(obj["204"]);
            style += "border:" + obj["204"] + " " + obj["206"] + " " + obj["205"] + ";";
            if (obj["207"] != string.Empty && obj["207"] != null)
            {
                style += "padding:" + obj["207"] + ";";
            }
            if (obj["208"] != string.Empty && obj["208"] != null)
            {
                style += "margin-top:" + obj["208"] + ";";
            }
            if (obj["209"] != string.Empty && obj["209"] != null)
            {
                style += "margin-bottom:" + obj["209"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridTwoFifthContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-two-fifth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["174"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["175"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["176"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["177"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["193"] != string.Empty && obj["193"] != null)
            {
                css += obj["193"] + " ";
            }
            if (obj["194"] != string.Empty && obj["194"] != null)
            {
                divId = obj["194"];
            }
            if (obj["178"] != string.Empty && obj["178"] != null)
            {
                style += "background-color:" + obj["178"] + ";";
            }
            if (obj["180"] != string.Empty && obj["180"] != null)
            {
                style += "background-image:url(" + obj["180"] + ");";
            }
            if (obj["181"] != string.Empty && obj["181"] != null)
            {
                style += "background-repeat:" + obj["181"] + ";";
            }
            if (obj["182"] != string.Empty && obj["182"] != null)
            {
                style += "background-position:" + obj["182"] + ";";
            }
            style += "border:" + obj["183"] + " " + obj["185"] + " " + obj["186"] + ";";
            if (obj["187"] != string.Empty && obj["187"] != null)
            {
                style += "padding-left:" + obj["187"] + ";";
            }
            if (obj["188"] != string.Empty && obj["188"] != null)
            {
                style += "margin-top:" + obj["188"] + ";";
            }
            if (obj["189"] != string.Empty && obj["189"] != null)
            {
                style += "margin-bottom:" + obj["189"] + ";";
            }
            if (obj["195"] != string.Empty && obj["195"] != null)
            {
                style += "padding-right:" + obj["195"] + ";";
            }
            if (obj["196"] != string.Empty && obj["196"] != null)
            {
                style += "padding-top:" + obj["196"] + ";";
            }
            if (obj["197"] != string.Empty && obj["197"] != null)
            {
                style += "padding-bottom:" + obj["197"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridFiveContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-one-fifth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["154"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["156"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["157"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["158"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["172"] != string.Empty && obj["172"] != null)
            {
                css += obj["172"] + " ";
            }
            if (obj["173"] != string.Empty && obj["173"] != null)
            {
                divId = obj["173"];
            }
            if (obj["159"] != string.Empty && obj["159"] != null)
            {
                style += "background-color:" + obj["159"] + ";";
            }
            if (obj["160"] != string.Empty && obj["160"] != null)
            {
                style += "background-image:url(" + obj["160"] + ");";
            }
            if (obj["161"] != string.Empty && obj["161"] != null)
            {
                style += "background-repeat:" + obj["161"] + ";";
            }
            if (obj["162"] != string.Empty && obj["162"] != null)
            {
                style += "background-position:" + obj["162"] + ";";
            }
            string s = Convert.ToString(obj["163"]);
            style += "border:" + obj["163"] + " " + obj["165"] + " " + obj["164"] + ";";
            if (obj["166"] != string.Empty && obj["166"] != null)
            {
                style += "padding:" + obj["166"] + ";";
            }
            if (obj["167"] != string.Empty && obj["167"] != null)
            {
                style += "margin-top:" + obj["167"] + ";";
            }
            if (obj["168"] != string.Empty && obj["168"] != null)
            {
                style += "margin-bottom:" + obj["168"] + ";";
            }
            return (string.Format(html, css, style, divId, content));
        }

        public string ParseGridThreeFourthContainer(dynamic obj)
        {
            var html = @"<div class='{0} fusion-three-fourth fusion-layout-column fusion-spacing-no' style='{1}' id='{2}'><div class='fusion-column-wrapper'>{3}</div></div>";
            var css = string.Empty;
            var divId = string.Empty;
            var style = string.Empty;
            var content = "Child";
            if (obj["135"] == "yes")
            {
                css = "fusion-column-last ";
                html += "<div class='fusion-clearfix'></div>";
            }
            if (obj["136"] == "yes")
            {
                html = html.Replace("fusion-spacing-no", "fusion-spacing-yes");
            }
            if (obj["137"] == "yes")
            {
                style += "text-align: center; ";
            }
            else
            {
                style += "text-align: left; ";
            }
            if (obj["138"] == "yes")
            {
                css += "hideOnMobile ";
            }
            if (obj["152"] != string.Empty && obj["152"] != null)
            {
                css += obj["152"] + " ";
            }
            if (obj["153"] != string.Empty && obj["153"] != null)
            {
                divId = obj["153"];
            }
            if (obj["139"] != string.Empty && obj["139"] != null)
            {
                style += "background-color:" + obj["139"] + ";";
            }
            if (obj["140"] != string.Empty && obj["140"] != null)
            {
                style += "background-image:url(" + obj["140"] + ");";
            }
            if (obj["141"] != string.Empty && obj["141"] != null)
            {
                style += "background-repeat:" + obj["141"] + ";";
            }
            if (obj["142"] != string.Empty && obj["142"] != null)
            {
                style += "background-position:" + obj["142"] + ";";
            }
            string s = Convert.ToString(obj["143"]);
            style += "border:" + obj["143"] + " " + obj["145"] + " " + obj["144"] + ";";
            if (obj["146"] != string.Empty && obj["146"] != null)
            {
                style += "padding:" + obj["146"] + ";";
            }
            if (obj["147"] != string.Empty && obj["147"] != null)
            {
                style += "margin-top:" + obj["147"] + ";";
            }
            if (obj["148"] != string.Empty && obj["148"] != null)
            {
                style += "margin-bottom:" + obj["148"] + ";";
            }

            return (string.Format(html, css, style, divId, content));
        }

        public string ParseTextBox(dynamic obj)
        {
            var html = @"<div>{0}</div>";
            return (string.Format(html, obj["353"] != null?obj["353"]:""));
        }

        public string ParseTitleBox(dynamic obj)
        {
            var html = @"<div class='fusion-title title {3}' {4}>";
            if (obj["597"] == "left")
            {
                html += "<h" + obj["596"] + " class='title-heading-" + obj["597"] + "'>{2}</h" + obj["596"] + "><div class='title-sep-container'><div class='title-sep {0}' style='{1}'></div></div></div>";
            }
            else
            {
                html += "<div class='title-sep-container'><div class='title-sep {0}' style='{1}'></div></div>" + "<h" + obj["596"] + " class='title-heading-" + obj["597"] + "'>{2}</h" + obj["596"] + "></div>";
            }

            string sep = Convert.ToString(obj["598"]);
            var sepR = "";
            if (sep != "Default")
            {
                var sepA = sep.Split(' ');
                foreach (var item in sepA)
                {
                    sepR += "sep-" + item + " ";
                }
            }

            var style = "";
            if (obj["599"] != string.Empty && obj["599"] != null)
            {
                style = "border-color:" + obj["599"] + ";";
            }

            var divid = "";
            if (obj["602"] != string.Empty && obj["602"] != null)
            {
                divid = " id='" + obj["602"] + "'";
            }

            return (string.Format(html, sepR, style, obj["600"], obj["601"], divid));
        }

        public string ParseYouTubeVideo(dynamic obj)
        {
            var html = @"<div class='fusion-video fusion-youtube {3}' style='width:{1}; height:{2}; margin: 0 auto;'><div class='video-shortcode video-responsive'><iframe title='YouTube video player' src='https://www.youtube.com/embed/{0}?wmode=transparent{4}' width='{1}' height='{2}' frameborder='0' allowfullscreen=''></iframe></div></div>";
            var videoid = obj["605"];
            var width = "";
            var height = "";
            var css = "";
            var additionalParam = "";
            if (obj["606"] != null && obj["606"] != "")
            {
               // style += "max-width:" + obj["606"] + "px;";
                width = obj["606"] + "px;";
            }
            else
            {
                //style += "max-width:600px;";
                width = "100" + "%;";
            }

            if (obj["607"] != null && obj["607"] != "")
            {
                //style += "max-height:" + obj["607"] + "px;";
                height = obj["607"] + "px;";
            }
            else
            {
                //style += "max-height:350px;";
                height = "100" + "%;";
            }

            if (obj["608"] == "yes")
            {
                additionalParam += "&autoplay=1";
            }
            if (obj["609"] != null && obj["609"] != "")
            {
                additionalParam += obj["609"];
            }

            if (obj["610"] != null && obj["610"] != "")
            {
                css = obj["610"];
            }
            return (string.Format(html, videoid, width, height, css, additionalParam));
        }

        public string ParseVimeoVideo(dynamic obj)
        {
            var html = @"<div class='fusion-video fusion-vimeo {4}' style='{1}'><div class='video-shortcode video-responsive'><iframe src='https://player.vimeo.com/video/{0}?{5}' width='{2}' height='{3}' frameborder='0'></iframe></div></div>";
            var videoid = obj["325"];
            var style = "";
            var width = "";
            var height = "";
            var css = "";
            var additionalParam = "&autoplay=0";
            style += "text-align:center;";
            if (obj["326"] != null && obj["326"] != "")
            {
               // style += "max-width:" + obj["326"] + "px;";
                width = obj["326"];
            }
            else
            {
               // style += "max-width:600px;";
                width = "600";
            }

            if (obj["327"] != null && obj["327"] != "")
            {
               // style += "max-height:" + obj["327"] + "px;";
                height = obj["327"];
            }
            else
            {
               // style += "max-height:350px;";
                height = "350";
            }

            if (obj["328"] == "yes")
            {
                additionalParam = "&autoplay=1";
            }
            if (obj["329"] != null && obj["329"] != "")
            {
                additionalParam += obj["329"];
            }

            if (obj["330"] != null && obj["330"] != "")
            {
                css = obj["330"];
            }
            return (string.Format(html, videoid, style, width, height, css, additionalParam));
        }

        public string ParseTogglesBox(dynamic obj)
        {
            var divId = "";
            if (obj["604"] != string.Empty && obj["604"] != null)
            {
                divId = "id='" + obj["604"] + "'";
            }
            var html = "<div class='accordian fusion-accordian " + obj["603"] + "' "  + divId + ">";
            var accordionId = "accordion-" + Guid.NewGuid().ToString().Replace("-", "_");
            html += "<div class='panel-group' id='" + accordionId + "'>";
            var boxes = obj["620"];
            if (boxes != null)
            {
                foreach (var item in boxes)
                {
                    html += "<div class='fusion-panel panel-default'>";
                    html += "<div class='panel-heading'>";
                    var id = Guid.NewGuid().ToString().Replace("-", "_");
                    var status = "collapsed";
                    var st = "";
                    if (item["84"] == "yes")
                    {
                        status = "active";
                        st = "in";
                    }
                    html +=
                        "<h4 class='panel-title toggle' data-fontsize='14' data-lineheight='20'><a data-toggle='collapse' data-parent='#" +
                        accordionId + "' data-target='#" + id + "' href='#" + id + "' class='" + status +
                        "' onclick=cms_toggleBoxClicked(this)>";
                   // html += "<div class='fusion-toggle-icon-wrapper'>";
                   // html += "<i class='fa-fusion-box'></i>";
                   // html += "</div>";
                    html += "<div class='fusion-toggle-heading'>" + item["83"] + "</div>";
                    html += "</a></h4>";
                    html += "</div>";
                    html += "<div id='" + id + "' class='panel-collapse collapse " + st + "' style='height: auto;'>";
                    html += "<div class='panel-body toggle-content'>";
                    html += item["85"];
                    html += "</div>";
                    html += "</div>";
                }
            }
            html += "</div></div></div>";
            return html;
        }

        public string ParseMenuAnchor(dynamic obj)
        {
            var html = @"<div class='fusion-menu-anchor' {0}></div>";
            var divId = "";
            if (obj["446"] != string.Empty && obj["446"] != null)
            {
                divId = "id='" + obj["446"] + "'";
            }
            return (string.Format(html, divId));
        }

        public string ParseAlertBox(dynamic obj)
        {
            var generalHtml = @"<div class='{1} fusion-alert alert general alert-dismissable alert-info' {2}> <button type='button' class='close toggle-alert' data-dismiss='alert' aria-hidden='true' onclick='cms_closeAlertBox(this)'>×</button><span class='alert-icon'><i class='fa fa-lg fa-info-circle'></i></span>{0}</div>";
            var errorHtml = @"<div class='{1} fusion-alert alert error alert-dismissable alert-danger' {2}> <button type='button' class='close toggle-alert' data-dismiss='alert' aria-hidden='true' onclick='cms_closeAlertBox(this)'>×</button><span class='alert-icon'><i class='fa fa-lg fa-exclamation-triangle'></i></span>{0}</div>";
            var successHtml = @"<div class='{1} fusion-alert alert success alert-dismissable alert-success' {2}> <button type='button' class='close toggle-alert' data-dismiss='alert' aria-hidden='true' onclick='cms_closeAlertBox(this)'>×</button><span class='alert-icon'><i class='fa fa-lg fa-check-circle'></i></span>{0}</div>";
            var noticeHtml = @"<div class='{1} fusion-alert alert notice alert-dismissable alert-warning' {2}> <button type='button' class='close toggle-alert' data-dismiss='alert' aria-hidden='true' onclick='cms_closeAlertBox(this)'>×</button><span class='alert-icon'><i class='fa fa-lg fa-lg fa-cog'></i></span>{0}</div>";
            var customHtml = @"<div class='{1} fusion-alert alert custom alert-dismissable alert-custom' style='{3}' id='{2}'> <button style='{4}' type='button' class='close toggle-alert' data-dismiss='alert' aria-hidden='true' onclick='cms_closeAlertBox(this)'>×</button><span class='alert-icon'><i class='fa fa-lg {5}'></i></span>{0}</div>";

            var returnHtml = string.Empty;
            var css = string.Empty;
            var divId = string.Empty;
            if (obj["351"] != string.Empty && obj["351"] != null)
            {
                css = obj["351"] + " ";
            }
            if (obj["346"] == "yes")
            {
                css += "alert-shadow ";
            }
            if (obj["352"] != string.Empty && obj["352"] != null)
            {
                divId = "id='" + obj["352"] + "'";
            }

            if (obj["341"] == "general")
            {
                returnHtml = string.Format(generalHtml, obj["347"], css, divId);
            }
            else if (obj["341"] == "error")
            {
                returnHtml = string.Format(errorHtml, obj["347"], css, divId);
            }
            else if (obj["341"] == "success")
            {
                returnHtml = string.Format(successHtml, obj["347"], css, divId);
            }
            else if (obj["341"] == "notice")
            {
                returnHtml = string.Format(noticeHtml, obj["347"], css, divId);
            }
            else if (obj["341"] == "custom")
            {
                var style = string.Empty;
                var style1 = string.Empty;
                if (obj["343"] != string.Empty && obj["343"] != null)
                {
                    style = "background-color:" + obj["343"] + ";";
                }
                if (obj["342"] != string.Empty && obj["342"] != null)
                {
                    style += "color:" + obj["342"] + ";border-color:" + obj["342"] + ";";
                    style1 = "color:" + obj["342"] + ";border-color:" + obj["342"] + ";";
                }
                if (obj["344"] != string.Empty && obj["344"] != null)
                {
                    style += "border-width:" + obj["344"] + ";";
                }
                
                returnHtml = string.Format(customHtml, obj["347"], css, divId, style, style1, obj["345"]);
            }
            return returnHtml;

        }

        public string ParseSoundCloud(dynamic obj)
        {
            //var html = @"<div class='fusion-soundcloud {4}' style='{1}'><div class='video-shortcode'><iframe title='YouTube video player' src='http://www.youtube.com/embed/{0}?wmode=transparent{5}' width='{2}' height='{3}' frameborder='0' allowfullscreen=''></iframe></div></div>";
            var html = @"<div class='{0}' {1}><iframe scrolling='no' frameborder='no' width='{2}' height='{3}' src='https://w.soundcloud.com/player/?url={4}&auto_play={5}&hide_related={6}&show_comments={7}&show_user={8}&show_reposts=false&visual={9}&amp;color={10}'></iframe></div>";

            var videoid = obj["605"];
            var width = "100%";
            var height = "150";
            var css = "fusion-soundcloud";
            var divId = string.Empty;
            var autoplay = "false";
            var hideRelated = "true";
            var showComments = "false";
            var showUser = "false";
            var visual = "false";
            var color = "ff7700";

            if (obj["552"] != null && obj["552"] != "")
            {
                css = obj["552"];
            }

            if (obj["553"] != null && obj["553"] != "")
            {
                divId = "id='" + obj["553"] + "'";
            }

            if (obj["550"] != null && obj["550"] != "")
            {
                width = obj["550"];
            }

            if (obj["551"] != null && obj["551"] != "")
            {
                height = obj["551"];
            }
            if (obj["548"] == "yes")
            {
                autoplay = "true";
            }
            if (obj["546"] == "yes")
            {
                hideRelated = "false";
            }
            if (obj["545"] == "yes")
            {
                showComments = "true";
            }
            if (obj["547"] == "yes")
            {
                showUser = "true";
            }
            if (obj["544"] == "visual")
            {
                visual = "true";
            }
            if (obj["549"] != null && obj["549"] != "")
            {
                string s = obj["549"];
                color = s.Replace("#", "");
            }
            return (string.Format(html, css, divId, width, height, obj["543"], autoplay, hideRelated, showComments, showUser, visual, color));
        }

        public string ParseCheckList(dynamic obj)
        {
            var checkListHtml = @"<li id='{0}' class='{1}'>{2}</li>";

            var selectIcon = string.Empty;
            var iconColor = string.Empty;
            var iconInCircle = string.Empty;
            var checkBoxStyle = string.Empty;
            var circleColor = string.Empty;
            var itemSize = string.Empty;
            var cssClass = string.Empty;
            var cssDivID = string.Empty;

            var checkList = obj["622"];
            if (obj["404"] != string.Empty && obj["404"] != null)
            {
                selectIcon = obj["404"];
            }
            if (obj["405"] != string.Empty && obj["405"] != null)
            {
                iconColor = obj["405"];
            }
            if (obj["406"] != string.Empty && obj["406"] != null)
            {
                iconInCircle = obj["406"];
                if (iconInCircle.ToLower() == "yes")
                {
                    checkBoxStyle = "border-radius: 50%; background-clip: padding-box;";
                }
            }

            if (obj["407"] != string.Empty && obj["407"] != null)
            {
                circleColor = obj["407"];
            }
            if (obj["408"] != string.Empty && obj["408"] != null)
            {
                itemSize = obj["408"];
            }
            if (obj["409"] != string.Empty && obj["409"] != null)
            {
                cssClass = obj["409"];
            }
            if (obj["410"] != string.Empty && obj["410"] != null)
            {
                cssDivID = obj["410"];
            }
            var boxesHtml = string.Empty;
            if (checkList != null)
            {
                foreach (var item in checkList)
                {
                    if (item["86"] != string.Empty && item["86"] != null)
                    {
                        selectIcon = item["86"];
                    }
                    var pmdivId = Guid.NewGuid().ToString().Replace("-", "_");
                    boxesHtml +=
                        string.Format(
                            "<span style='{4} background-color:{5};font-size:{6}; margin-right:9.1px; display: table; float: left;' " +
                            "class='circle-{3}'><i class='fusion-li-icon fa {2}' style='color:{7};'></i></span><div id='{0}' style='margin-left:31.2px;'>{1}</div>",
                            pmdivId, item["87"], selectIcon, iconInCircle, checkBoxStyle, circleColor, itemSize,
                            iconColor);
                }
            }
            return (string.Format(checkListHtml, cssDivID, cssClass, boxesHtml));
        }

        public string ParseSharingBox(dynamic obj)
        {
            string[] iconColors = new string[9];
            string[] iconBackColors = new string[9];
            var defaultColor = "#ffffff";
            for (var i = 0; i < 9; i++)
            {
                iconColors[i] = defaultColor;
            }

            iconBackColors[0] = "#3B5998";
            iconBackColors[1] = "#4298ed";
            iconBackColors[2] = "#bf0018";
            iconBackColors[3] = "#27354b";
            iconBackColors[4] = "#d23123";
            iconBackColors[5] = "#0064a9";
            iconBackColors[6] = "#000000";
            iconBackColors[7] = "#3b6094";
            iconBackColors[8] = "#b7b7b7";

            if (obj["498"] != null && obj["498"] != string.Empty)
            {
                string s = obj["498"];
                var colors = s.Split('|');
                if (colors.Count() == 1)
                {
                    defaultColor = colors[0];
                    for (var i = 0; i < 9; i++)
                    {
                        iconColors[i] = defaultColor;
                    }
                }
                else
                {
                    for (var j = 0; j < colors.Count(); j++)
                    {
                        iconColors[j] = colors[j];
                    }
                }
            }
            if (obj["499"] != null && obj["499"] != "")
            {
                string s = obj["499"];
                var colors = s.Split('|');
                if (colors.Count() == 1)
                {
                    for (var i = 0; i < 9; i++)
                    {
                        iconBackColors[i] = colors[0];
                    }
                }
                else
                {
                    for (var j = 0; j < colors.Count(); j++)
                    {
                        iconBackColors[j] = colors[j];
                    }
                }
            }

            var generalHtml = @"<div class='share-box fusion-sharing-box {1}' style='background-color: {2};padding:5px;' {8}>" +
                "<h4 class='tagline' style='color: {3};' data-fontsize='13' data-lineheight='30'>{0}</h4>" +
                "<div class='fusion-social-networks {9}' data-title='{4}' data-description='{5}' data-link='{6}' data-image='{7}'>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-facebook fusion-icon-facebook' title='Share to facebook' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='fb' href='#' style='color:" + iconColors[0] + ";background-color:" + iconBackColors[0] + ";border-color:#4298ed;border-radius:0px;'></a>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-twitter fusion-icon-twitter' title='Share to twitter' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='tw' href='#' style='color:" + iconColors[1] + ";background-color:" + iconBackColors[1] + ";border-color:#4298ed;border-radius:0px;'></a>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-pinterest fusion-icon-pinterest' title='Share to pinterest' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='pint' href='#' style='color:" + iconColors[2] + ";background-color:" + iconBackColors[2] + ";border-color:#bf0018;border-radius:0px;'></a>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-tumblr fusion-icon-tumblr' title='Share to tumbler' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='tumb' href='#' style='color:" + iconColors[3] + ";background-color:" + iconBackColors[3] + ";border-color:#27354b;border-radius:0px;'></a>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-googleplus fusion-icon-googleplus' title='Share to googleplus' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='gp' href='#' style='color:" + iconColors[4] + ";background-color:" + iconBackColors[4] + ";border-color:#d23123;border-radius:0px;'></a>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-reddit fusion-icon-reddit' title='Share to reddit' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='redd' href='#' style='color:" + iconColors[6] + ";background-color:" + iconBackColors[6] + ";border-color:#000000;border-radius:0px;'></a>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-vk fusion-icon-vk' title='Share to vk' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='vk' href='#' style='color:" + iconColors[7] + ";background-color:" + iconBackColors[7] + ";border-color:#3b6094;border-radius:0px;'></a>" +
                //"<a class='fusion-social-network-icon fusion-tooltip fusion-mail fusion-icon-mail' title='Mail this story' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='mail' href='#' style='color:" + iconColors[8] + ";background-color:" + iconBackColors[8] + ";border-color:#b7b7b7;border-radius:0px;'></a>" +
                "<a class='rightFloat fusion-social-network-icon fusion-tooltip fusion-mail fusion-icon-mail' title='Mail this story' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='mail' href='#' style='color:" + iconColors[5] + ";background-color:#8194aa;border-color:#8194aa;border-radius:0px;'></a>" +
                "<a class='rightFloat fusion-social-network-icon fusion-tooltip fusion-instagram' title='Share to instagram' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='intgm' href='#' style='color: " + iconColors[4] + "; background-color: transparent; border-color: transparent; background-image: url(/Images/instagram36x36.png) !important; background-repeat: no-repeat; width: 20px; height: 18px; border-width: 0px; border-radius: 4px;'></a>" +
                "<a class='rightFloat fusion-social-network-icon fusion-tooltip fusion-youtube' title='Share to youtube' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='ytb' href='#' style='color: " + iconColors[3] + "; background-color: transparent; border-color: transparent; background-image: url(/Images/youtube36x36.png) !important; background-repeat: no-repeat; width: 20px; height: 18px; border-width: 0px; border-radius: 4px;'></a>" +
                "<a class='rightFloat fusion-social-network-icon fusion-tooltip fusion-twitter fusion-icon-twitter' title='Share to twitter' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='tw' href='#' style='color:" + iconColors[2] + ";background-color:#4691ca;border-color:#4691ca;border-radius:4px;'></a>" +
                "<a class='rightFloat fusion-social-network-icon fusion-tooltip fusion-facebook fusion-icon-facebook' title='Share to facebook' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='fb' href='#' style='color:" + iconColors[1] + ";background-color:#3e5a96;border-color:#3e5a96;border-radius:4px;'></a>" +
                "<a class='rightFloat fusion-social-network-icon fusion-tooltip fusion-linkedin fusion-icon-linkedin' title='Share to linkedin' onclick=cms_sharingBoxIconClicked(event,this) data-boxType='linkd' href='#' style='color:" + iconColors[0] + ";background-color:#104980;border-color:#104980;border-radius:4px;'></a>" +
               "</div>" +
               "</div>";
            var ccss = "fusion-soundcloud";
            var divId = string.Empty;
            var boxedIcons = string.Empty;

            if (obj["502"] != null && obj["502"] != "")
            {
                ccss = obj["502"];
            }

            if (obj["503"] != null && obj["503"] != "")
            {
                divId = "id='" + obj["503"] + "'";
            }
            if (obj["496"] != null && obj["496"] == "yes")
            {
                boxedIcons = "boxed-icons";
            }


            if (obj["497"] != null && obj["497"] != "")
            {
                generalHtml = generalHtml.Replace("border-radius:0px;", "border-radius:" + obj["497"] + ";");
            }
            return string.Format(generalHtml, obj["490"], ccss, obj["492"], obj["491"], obj["493"], obj["495"], obj["494"], obj["501"], divId, boxedIcons);

        }

        public string ParseTestimonialBox(dynamic obj)
        {
            var html = string.Empty;
            var testimonials = obj["619"];
            var counter = 1;
            if (testimonials != null)
            {
                foreach (var item in testimonials)
                {
                    var divId = "";
                    if (obj["595"] != string.Empty && obj["595"] != null)
                    {
                        divId = "id='" + obj["595"] + "'";
                    }
                    var bColor = "";
                    var color = "#747474";
                    if (obj["591"] != string.Empty && obj["591"] != null)
                    {
                        bColor = obj["591"];
                    }
                    if (obj["592"] != string.Empty && obj["592"] != null)
                    {
                        color = obj["592"];
                    }

                    html += "<div class='fusion-testimonials " + obj["590"] + " fusion-testimonials-" + counter + " " +
                            obj["594"] + "' data-random='0' " + divId + ">";
                    html += "<style type='text/css' scoped='scoped'>";
                    html += "#fusion-testimonials-" + counter + " a { border-color: " + bColor + "; }";
                    html +=
                        "#fusion-testimonials-" + counter + " a:hover, #fusion-testimonials-" + counter +
                        " .activeSlide { background-color: " + bColor +
                        "; }.fusion-testimonials.clean.fusion-testimonials-" + counter +
                        " .author:after { border-top-color: transparent !important; }";
                    html += "</style>";
                    html += "<div class='reviews'>";
                    var image = item["76"];
                    if (image == "image")
                    {
                        image = "avatar-image";
                    }

                    html += "<div class='review '" + image + " style='display: block;'>";
                    if (obj["590"] == "clean")
                    {
                        html += "<div class='testimonial-thumbnail'>";
                        var tImagePath = "";
                        if (item["76"] == "male")
                        {
                            tImagePath = "/images/testimonial-male.jpg";
                        }
                        if (item["76"] == "female")
                        {
                            tImagePath = "/images/testimonial-female.jpg";
                        }
                        if (item["76"] == "image")
                        {
                            tImagePath = item["77"];
                        }
                        var borderRadious = item["78"];
                        if (borderRadious == "round")
                        {
                            borderRadious = "50%";
                        }
                        var upInfo =
                            _uploadInformationRepository.GetAll().Where(x => x.FilePath == tImagePath).FirstOrDefault();
                        var altText = string.Empty;
                        if (upInfo != null)
                        {
                            altText = !string.IsNullOrEmpty(upInfo.AltText)? upInfo.AltText: string.Empty;
                        }
                        html +=
                            "<img class='testimonial-image' src='" + tImagePath + "' style='-webkit-border-radius: " +
                            borderRadious + "; -moz-border-radius: " + borderRadious + "; border-radius: " +
                            borderRadious + ";' alt='" + item["175"] + ";" + altText + "'>";
                        html += "</div>";
                    }
                    html += "<blockquote style='margin: -25px; background-color: transparent;'>";
                    html +=
                        "<q style='background-color: transparent; color: " + color + ";'>" + item["82"] + "</q>";
                    html += "</blockquote>";
                    html += "<div class='author' style='color: " + color + ";'>";
                    if (obj["590"] == "classic")
                    {
                        var cs = "fusion-icon-user";
                        if (item["76"] == "female")
                        {
                            cs = "fusion-icon-user2";
                        }
                        html += "<span class='testimonial-thumbnail doe " + cs + "' style='color: " + color +
                                ";'></span>";
                    }
                    string s = item["80"];
                    //if (!string.IsNullOrEmpty(s) && !s.Contains(GetBaseUrl()))
                    //{
                    //    s = s.StartsWith("/") ? (GetBaseUrl() + s) : (GetBaseUrl() + "/" + s);
                    //}
                    html +=
                        "<span class='company-name'><strong>" + item["75"] + "</strong>, <a href='" + RelativeToAbsoluteUrl(s) + "' target='" +
                        item["81"] + "'><span>" + item["79"] + "</span></a></span>";
                    html += "</div></div></div></div>";
                    counter = counter + 1;
                }
            }

            return html;
        }

        public string ParseTaglineBox(dynamic obj)
        {
            var divId = "";
            if (obj["589"] != string.Empty && obj["589"] != null)
            {
                divId = "id='" + obj["589"] + "'";
            }
            var opacity = "0.7";
            if (obj["567"] != string.Empty && obj["567"] != null)
            {
                opacity = obj["567"];
            }
            var html = "<div class='fusion-column-wrapper'>";
            html += "<style type='text/css' scoped='scoped'>";
            html +=
                ".reading-box-container .element-bottomshadow:before, .reading-box-container .element-bottomshadow:after { opacity: " + opacity + "; }  .button{color:#ffffff} a:link{color:#ffffff} .button-round{ -webkit-border-radius: 50px;-moz-border-radius: 50px;border-radius: 50px;} .button-pill{ border: 1px solid;border-radius: 25px;-moz-border-radius: 25px;-webkit-border-radius: 25px;} .button-3D{box-shadow:6px 6px 2px #c7c7c7; }";
            html += "</style>";
            var shadow = "";
            if (obj["566"] == "yes")
            {
                shadow = "element-bottomshadow";
            }
            html +=
                "<div class='fusion-reading-box-container reading-box-container " + obj["588"] + "' style='border-color:transparent !important;margin-top: " + obj["583"] + "; margin-bottom: " + obj["584"] + ";' " + divId + ">";
            var backColor = "#f6f6f6";
            
            if (obj["565"] != string.Empty && obj["565"] != null)
            {
                backColor = obj["565"];
            }
            var borderColor = "transparent";
            var borderTopColor = "transparent";
            var titleColor = "#000000";
            var borderTopWidth = "1px";
            if (obj["569"] != string.Empty && obj["569"] != null)
            {
                borderColor = obj["569"];
            }
            if (obj["1"] != string.Empty && obj["1"] != null)
            {
                borderTopColor = obj["1"];
            }
            if (obj["2"] != string.Empty && obj["2"] != null)
            {
                titleColor = obj["2"];
            }
            if (obj["3"] != string.Empty && obj["3"] != null)
            {
                borderTopWidth= obj["3"];
            }
            html +=
                "<div class='reading-box " + shadow + "' style='background-color: " + backColor + "; border-width: " + obj["568"] + "; border-color: " + borderColor + "; border-top-width: " + borderTopWidth + "; border-top-color: " + borderTopColor + "; border-style: solid;'>";
            string s = obj["573"];
                //if (s != null && !s.Contains("http"))
                //{
                //    s = "http://" + s;
                //}
                var button = "button fusion-button button-" + obj["578"] + " button-" + obj["576"] + " button-" + obj["577"] + " continue continue-right";

                var onclickFunction = string.Empty;
                if (obj["573"] == string.Empty)
                {
                    s = "#";
                    onclickFunction = "cms_openModalFor(event,'" + obj["575"] + "')";
                }
            html +=
                "<a class='" + button + "' style='background-color:" + obj["579"] + "' href='" + RelativeToAbsoluteUrl(s) + "'   target='" + obj["574"] + "'  onclick=" + onclickFunction + ">";
            html += "<span>" + obj["572"] + "</span>";
            html += "</a>";
            html +=
                "<h2 style='font-size:24px;line-height:2;font-weight:bold;text-shadow:4px 4px 6px #c7c7c7;color:" + titleColor + ";'>" + obj["580"] + "</h2>";
            html +=
                "<div class='reading-box-description' style='font-size:18px;'>" + obj["581"] + "</div><br/>";
            html += "<div class='reading-box-additional'>" + obj["582"] + "</div>";
            html += "</div></div><div class='fusion-clearfix'></div></div>";
           return html;
        }

        public string ParseContentBoxes(dynamic obj)
        {
            //var contentSeprator = "<div class='fusion-separator fusion-full-width-sep sep-shadow' style='background:radial-gradient(ellipse at 50% -50% , #e0dede 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0);background:-webkit-radial-gradient(ellipse at 50% -50% , #e0dede 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0);background:-moz-radial-gradient(ellipse at 50% -50% , #e0dede 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0);background:-o-radial-gradient(ellipse at 50% -50% , #e0dede 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0);margin-left: auto;margin-right: auto;margin-top:-20px;margin-bottom:55px;'></div>";
            var contentSeprator = "<div style='height:10px;width:100%;'>&nbsp;</div>";
            var contentBoxHtml = @"{5}<div class='fusion-content-boxes content-boxes fusion-columns-{0} fusion-columns-total-{0} content-boxes-" + obj["412"] + " {1}' {2} {3}>{4}</div>";

            var style = string.Empty;
            var cssClass = string.Empty;
            var cssDivId = string.Empty;

            var iconInCircle = string.Empty;
            var checkBoxStyle = string.Empty;
            var circleColor = string.Empty;
            var itemSize = string.Empty;

            var contentList = obj["611"];
            if (obj["418"] != string.Empty && obj["418"] != null)
            {
                style += "margin-top:" + obj["418"] + ";";
            }
            if (obj["419"] != string.Empty && obj["419"] != null)
            {
                style += "margin-bottom:" + obj["419"] + ";";
            }
            if (string.IsNullOrEmpty(style))
            {
                style += string.Format("style='{0}'", style);
            }

            if (obj["420"] != string.Empty && obj["420"] != null)
            {
                cssClass = obj["420"] + " content-" + obj["416"];
            }

            if (obj["421"] != string.Empty && obj["421"] != null)
            {
                cssDivId = obj["421"];
            }

            var boxesHtml = string.Empty;
            var i = 0;
            var columnWidthClass = string.Empty;
            int columnCount = Convert.ToInt16(obj["417"]);
            switch (columnCount)
            {
                case 1:
                    columnWidthClass = "col-lg-99 col-md-99 col-sm-99";
                    break;
                case 2:
                    columnWidthClass = "col-lg-49 col-md-49 col-sm-49";
                    break;
                case 3:
                    columnWidthClass = "col-lg-32 col-md-32 col-sm-32";
                    break;
                case 4:
                    columnWidthClass = "col-lg-24 col-md-24 col-sm-24";
                    break;
                case 5:
                    columnWidthClass = "col-lg-19 col-md-19 col-sm-19";
                    break;
                case 6:
                    columnWidthClass = "col-lg-16 col-md-16 col-sm-16";
                    break;
            }

            var iconClass = string.Empty;
            var headerStyle = string.Empty;
            var columnStyle = string.Empty;
            var iconStyle = string.Empty;
            var firstLastColumnClass = string.Empty;
            var blankRow = string.Empty;
            var iconOnSideSpecial = (obj["412"] != "icon-on-side" ? "" : "style='padding-left:64px;'");
            if (contentList != null)
            {
                foreach (var item in contentList)
                {
                    var iconTitle = string.Empty;
                    if (i == 0)
                    {
                        firstLastColumnClass = "content-box-column-first-in-row";
                        blankRow = string.Empty;
                    }
                    else if (i == columnCount - 1)
                    {
                        firstLastColumnClass = "content-box-column-last content-box-column-last-in-row";
                        blankRow = "<div class='fusion-clearfix'></div><div class='fusion-clearfix'></div>";
                        i = -1;
                    }
                    var learnMore = string.Empty;
                    iconClass = ((item["2"] != string.Empty && item["2"] != null) ? item["2"] : "fa-tablet") +
                                ((obj["414"] != string.Empty && obj["414"] != null && obj["414"] == "yes")
                                    ? " circle-yes "
                                    : "") +
                                ((item["7"] != string.Empty && item["7"] != null) ? ("fa-flip-" + item["7"]) : "") +
                                ((item["8"] != string.Empty && item["8"] != null) ? (" fa-rotate-" + item["8"]) : "") +
                                ((item["9"] != string.Empty && item["9"] != null && item["9"] == "yes")
                                    ? " fa-spin"
                                    : "");

                    headerStyle = (((obj["413"] != string.Empty && obj["413"] != null)
                        ? "style='font-size:" + obj["413"] + ";"
                        : "style='font-size:24px;") + (obj["412"] != "icon-on-side" ? "'" : "padding-left:64px;'"));
                    var contentBoxHeight = "";
                    if (item["49"] != string.Empty && item["49"] != null)
                    {
                        if (!item["49"].ToString().Contains("px"))
                        {
                            contentBoxHeight = " height:" + item["49"] + "px;";
                        }
                        else
                        {
                            contentBoxHeight = " height:" + item["49"] + ";";
                        }
                    }
                    columnStyle = string.Format("style='background-color:{0};{1}'",
                        (item["3"] != string.Empty && item["3"] != null) ? item["3"] : (obj["412"] == "icon-boxed" ? " rgb(246, 246, 246)" : "transparent"), contentBoxHeight);

                    iconStyle =
                        string.Format(
                            "style='border-color:{2};border-width:1px;background-color:{1};height:auto;width:{3};line-height:42px;border-radius:50%;color:{0};font-size:21px;'",
                            (item["4"] != string.Empty && item["4"] != null) ? item["4"] : "#000000",
                            (item["5"] != string.Empty && item["5"] != null) ? item["5"] : "transparent",
                            (item["6"] != string.Empty && item["6"] != null) ? item["6"] : "#000000",
                            (obj["415"] != string.Empty && obj["415"] != null) ? obj["415"] : "42px");

                    if (item["10"] != string.Empty && item["10"] != null)
                    {
                        var imgPath = Convert.ToString(item["10"]);
                        var upInfo =
                          _uploadInformationRepository.GetAll().Where(x => x.FilePath == imgPath).FirstOrDefault();
                        var altText = string.Empty;
                        if (upInfo != null)
                        {
                            altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                        }
                        iconTitle =
                            string.Format(
                                "<div class='image'><img src='{0}' width='{1}' height='{2}' alt='{5}'></img></div><h2 class='content-box-heading' {3}>{4}</h2>",
                                item["10"], item["11"], item["12"], headerStyle, item["1"],altText);
                    }
                    else
                    {
                        iconTitle =
                            string.Format(
                                "<div class='icon'><i {0} class='fa fontawesome-icon {1}'></i></div><h2 class='content-box-heading' {2}>{3}</h2>",
                                iconStyle, iconClass, headerStyle, item["1"]);
                    }

                    if (item["13"] != string.Empty && item["13"] != null && item["14"] != string.Empty &&
                        item["14"] != null)
                    {
                        learnMore =
                            string.Format(
                                "<div class='fusion-clearfix'></div><a class='fusion-read-more' href='{0}' target='{1}'>{2}</a><div class='fusion-clearfix'></div>"
                                , RelativeToAbsoluteUrl(item["13"]), item["15"], item["14"]);
                        iconTitle = string.Format("<a class='heading-link' href='{0}' target='{1}'>{2}</a>"
                            , RelativeToAbsoluteUrl(item["13"]), item["15"], iconTitle);
                    }
                    i++;
                    boxesHtml +=
                        string.Format(
                            "<div class='fusion-column content-box-column content-box-column-" + i +
                            " {0} fusion-content-box-hover {1}' style='padding-left:2px !important;padding-right:2px !important;'>" +
                            "<div class='col content-wrapper-background content-wrapper-boxed link-area-link-icon' {2}><div class='heading heading-with-icon icon-left'>{3}</div>" +
                            "<div class='fusion-clearfix'></div><div class='content-container' {7}>{4}{5}" +
                            "</div></div></div>{6}"
                            , columnWidthClass, firstLastColumnClass, columnStyle, iconTitle, item["16"], learnMore,
                            blankRow, iconOnSideSpecial);
                }
            }
            return (string.Format(contentBoxHtml, obj["417"], cssClass, cssDivId, style, boxesHtml, contentSeprator));
            
        }
        
        public string ParseModalBox(dynamic obj)
        {
            var divId = "";
            if (obj["631"] != string.Empty && obj["631"] != null)
            {
                divId = "id='" + obj["631"] + "'";
            }

            var modalId = Guid.NewGuid().ToString().Replace("-", "_");
            var cs = string.Empty;
            if (obj["623"] != string.Empty && obj["623"] != null)
            {
                modalId = obj["623"];
            }
            if (obj["630"] != string.Empty && obj["630"] != null)
            {
                cs = obj["630"];
            }
            var html = "<div class='fusion-modal modal fade in " + modalId + " " + cs + "' tabindex='-1' role='dialog' aria-labelledby='modal-heading' aria-hidden='false' style='display: none;' " + divId + ">";
            var borderColor = "#ffffff";
            if (obj["627"] != string.Empty && obj["627"] != null)
            {
                borderColor = obj["627"];
            }
            html +=
                "<style type='text/css'>.modal .modal-header, .modal .modal-footer { border-color: " + borderColor + "; }</style>";
            var sizeOfModal = "sm";
            if (obj["625"] == "large")
            {
                sizeOfModal = "lg";
            }
            html += "<div class='modal-dialog modal-" + sizeOfModal + "'>";
            var backColor = "ffffff";
            if (obj["626"] != string.Empty && obj["626"] != null)
            {
                backColor = obj["626"];
            }
            html += "<div class='modal-content fusion-modal-content' style='background-color: " + backColor + "'>";
            html +=
                "<div class='modal-header'><button class='close' type='button' data-dismiss='modal' aria-hidden='true' onclick=cms_closeModal(event,'" + modalId + "')>×</button>";
            html +=
                "<h3 class='modal-title' id='modal-heading-" + Guid.NewGuid().ToString().Replace("-", "_") +"' data-dismiss='modal' aria-hidden='true' data-fontsize='17' data-lineheight='23'>" + obj["624"] + "</h3>";
            html += "</div>";
            html += "<div class='modal-body'>";
            html += obj["629"];
            html += "</div>";
            if (obj["628"] == "yes")
            {
                html += "<div class='modal-footer'>";
                html +=
                    "<a class='fusion-button button-default button-medium button default medium' data-dismiss='modal' onclick=cms_closeModal(event,'" +
                    modalId + "')>Close</a>";
                html += "</div>";
            }
            html += "</div></div></div>";
            return html;
        }

        public string ParseButtonBlock(dynamic obj)
        {
            var divId = "";
            if (obj["403"] != string.Empty && obj["403"] != null)
            {
                divId = "id='" + obj["403"] + "'";
            }
            var html = "<div style='text-align:" + obj["401"] + ";' class='" + obj["402"] + "' " + divId + ">";
            html+= "<style type='text/css' scoped='scoped'>";
            var color = "#ffffff";
            if (obj["390"] != string.Empty && obj["390"] != null)
            {
                color = obj["390"];
            }
            html += "a:link{color:#ffffff} .fusion-button.button .fusion-button-text, .fusion-button.button i { border-width: " + obj["393"] + ";color: " + color + ";border-color: " + color + "; }";
            html += ".fusion-button.button .fusion-button-icon-divider { border-color: " + color + "; }";
            html += ".fusion-button.button:hover .fusion-button-text, .fusion-button.button:hover i, .fusion-button.button:focus .fusion-button-text, .fusion-button.button:focus i, .fusion-button.button:active .fusion-button-text, .fusion-button.button:active { border-color: #a8a8a8;color: #a8a8a8; }";
            var backGradTopColor = "#ffffff";
            var backGradBottomColor = "#000000";
            var backGradTopHoverColor = "#ffffff";
            var backGradBottomHoverColor = "#000000";

            //button style
            if (obj["379"] != "custom")
            {
                html += ".fusion-button.button {background: " + obj["387"] + ";}";
            }
            else
            {
                if (obj["386"] != string.Empty && obj["386"] != null)
                {
                    backGradTopColor = obj["386"];
                }
                if (obj["387"] != string.Empty && obj["387"] != null)
                {
                    backGradBottomColor = obj["387"];
                }

                if (obj["388"] != string.Empty && obj["388"] != null)
                {
                    backGradTopHoverColor = obj["388"];
                }
                if (obj["389"] != string.Empty && obj["389"] != null)
                {
                    backGradBottomHoverColor = obj["389"];
                }

                html += ".fusion-button.button {background: " + backGradTopColor + ";background-image: -webkit-gradient(linear, left bottom, left top, from(" + backGradTopColor + "), to(" + backGradBottomColor + "));background-image: -webkit-linear-gradient(bottom, " + backGradTopColor + ", " + backGradBottomColor + ");background-image: -moz-linear-gradient(bottom, " + backGradTopColor + ", " + ");background-image: -o-linear-gradient(bottom, " + backGradTopColor + ", " + backGradBottomColor + ");background-image: linear-gradient(to top, " + backGradTopColor + ", " + backGradBottomColor + ");}";
                html += ".fusion-button.button-1:hover, .button-1:focus, .fusion-button.button-1:active {background: " + backGradTopHoverColor + ";background-image: -webkit-gradient(linear, left bottom, left top, from(" + backGradTopHoverColor + "), to(" + backGradBottomHoverColor + "));background-image: -webkit-linear-gradient(bottom, " + backGradTopHoverColor + ", " + backGradBottomHoverColor + ");background-image: -moz-linear-gradient(bottom, " + backGradTopHoverColor + ", " + backGradBottomHoverColor + ");background-image: -o-linear-gradient(bottom, " + backGradTopHoverColor + ", " + backGradBottomHoverColor + ");background-image: linear-gradient(to top, " + backGradTopHoverColor + ", " + backGradBottomHoverColor + ");}";
            }
            var bevelColor = "#000000";
            if (obj["392"] != string.Empty && obj["392"] != null)
            {
                bevelColor = obj["392"];
            }
            html += ".button-round{ -webkit-border-radius: 50px;-moz-border-radius: 50px;border-radius: 50px;} .button-pill{ border: 1px solid;border-radius: 25px;-moz-border-radius: 25px;-webkit-border-radius: 25px;} .button-3D{box-shadow:6px 6px 2px " + bevelColor + "; }";
            html += "</style>";
            string s = obj["378"];
            //if (s != null && !s.Contains("http"))
            //{
            //    s = "http://" + s;
            //}
            var button = "button fusion-button button-" + obj["380"] + " button-" + obj["381"] + " button-" + obj["382"];

            var onclickFunction = string.Empty;
            if (obj["397"] != string.Empty && obj["397"] != null)
            {
                s = "#";
                onclickFunction = "cms_openModalFor(event,'" + obj["397"] + "')";
            }
            html += "<a style='background: " + obj["387"] + ";' class='" + button + "' target='" + obj["383"] + "' href='" + RelativeToAbsoluteUrl(s) + "' title='" + obj["384"] + "' onclick=" + onclickFunction + ">";

            if (obj["396"] == "yes")
            {
                html += "<span class='fusion-button-icon-divider button-icon-divider-" + obj["395"] + "'><i class='fa " + obj["394"] + "'></i></span>";
            }

            html += "<span class='fusion-button-text fusion-button-text-" + obj["395"] + "'>" + obj["385"] + "</span></a>";
            html += "</div>";
            return html;
        }

        public string ParseCountdownBox(dynamic obj)
        {
            var countdownBoxHtml = string.Empty;
            var countdownId = string.Empty;
            var countdownClass = string.Empty;
            var numberOfColumns = string.Empty;
            var countdownEventName = string.Empty;
            var countdownEventDate = string.Empty;
            var countdownTitleFontSize = string.Empty;
            var countdownTitleFontColor = string.Empty;
            var countdownBodyFontSize = string.Empty;
            var countdownBodyFontColor = string.Empty;
            var countdownBorderColor = string.Empty;
            var countdownBorderRadius = string.Empty;
            var countdownBackgroundColor = string.Empty;

            if (obj["649"] != string.Empty && obj["649"] != null)
            {
                countdownId = obj["649"];
            }
            if (obj["650"] != string.Empty && obj["650"] != null)
            {
                countdownClass = obj["650"];
            }
            if (obj["650"] != string.Empty && obj["650"] != null)
            {
                countdownClass = obj["650"];
            }
            if (obj["651"] != string.Empty && obj["651"] != null)
            {
                numberOfColumns = "width: " + (100/Int32.Parse(obj["651"].ToString())) + '%';
            }
            if (obj["652"] != string.Empty && obj["652"] != null)
            {
                countdownEventName = obj["652"];
            }
            if (obj["653"] != string.Empty && obj["653"] != null)
            {
                countdownEventDate = obj["653"];
            }
            if (obj["657"] != string.Empty && obj["657"] != null)
            {
                countdownTitleFontSize = obj["657"];
            }
            if (obj["658"] != string.Empty && obj["658"] != null)
            {
                countdownTitleFontColor = obj["658"];
            }
            if (obj["659"] != string.Empty && obj["659"] != null)
            {
                countdownBodyFontSize = obj["659"];
            }
            if (obj["660"] != string.Empty && obj["660"] != null)
            {
                countdownBodyFontColor = obj["660"];
            }
            if (obj["661"] != string.Empty && obj["661"] != null)
            {
                countdownBorderColor = obj["661"] + " 2px solid";
            }
            if (obj["662"] != string.Empty && obj["662"] != null)
            {
                countdownBorderRadius = obj["662"];
            }
            if (obj["663"] != string.Empty && obj["663"] != null)
            {
                countdownBackgroundColor = obj["663"];
            }

            countdownBoxHtml += "<div id='{0}' class='{1}' style='{2}; text-align: center;'>";
            countdownBoxHtml += "<div style='padding: 30px; font-size: " + countdownTitleFontSize + ";  color: " +
                                countdownTitleFontColor + "; line-height: " + countdownTitleFontSize + ";'>" + countdownEventName + "</div> ";
            countdownBoxHtml += "<div id='clockdiv' style='width: 100%; font-weight: 100; text-align: center;' >";

            countdownBoxHtml +=
                "<div style='min-width: 100px; min-height: 100px; padding-top: 25px; padding-left: 25px;  padding-right: 25px; " +
                "font-size: " + countdownBodyFontSize + ";  color: " + countdownBodyFontColor +
                "; -moz-border-radius: " + countdownBorderRadius + "; -webkit-border-radius: " + countdownBorderRadius +
                "; border-radius: " + countdownBorderRadius + "; display: inline-block; border: " + countdownBorderColor +
                "; background-color: " + countdownBackgroundColor +
                ";'><span id='txtDays' class='days' style='padding: 20px;'></span>" +
                "<div style='padding-top: 35px; font-size: 14px; text-transform: uppercase;'>Days</div></div> ";

            countdownBoxHtml +=
                "<div style='min-width: 100px; min-height: 100px; padding-top: 25px; padding-left: 25px; padding-right: 25px; " +
                "font-size: " + countdownBodyFontSize + ";  color: " + countdownBodyFontColor +
                "; -moz-border-radius: " + countdownBorderRadius + "; -webkit-border-radius: " + countdownBorderRadius +
                "; border-radius: " + countdownBorderRadius + "; display: inline-block; border: " + countdownBorderColor +
                "; background-color: " + countdownBackgroundColor +
                ";'><span id='txtHours' class='hours' style='padding: 20px;'></span>" +
                "<div style='padding-top: 35px; font-size: 14px; text-transform: uppercase;'>Hours</div></div> ";

            countdownBoxHtml +=
                "<div style='min-width: 100px; min-height: 100px; padding-top: 25px; padding-left: 25px;  padding-right: 25px; " +
                "font-size: " + countdownBodyFontSize + ";  color: " + countdownBodyFontColor +
                "; -moz-border-radius: " + countdownBorderRadius + "; -webkit-border-radius: " + countdownBorderRadius +
                "; border-radius: " + countdownBorderRadius + "; display: inline-block; border: " + countdownBorderColor +
                "; background-color: " + countdownBackgroundColor +
                ";'><span id='txtMinutes' class='minutes' style='padding: 20px;'></span>" +
                "<div style='padding-top: 35px; font-size: 14px; text-transform: uppercase;'>Minutes</div></div> ";

            countdownBoxHtml +=
                "<div style='min-width: 100px; min-height: 100px; padding-top: 25px; padding-left: 25px;  padding-right: 25px; " +
                "font-size: " + countdownBodyFontSize + ";  color: " + countdownBodyFontColor +
                "; -moz-border-radius: " + countdownBorderRadius + "; -webkit-border-radius: " + countdownBorderRadius +
                "; border-radius: " + countdownBorderRadius + "; display: inline-block; border: " + countdownBorderColor +
                "; background-color: " + countdownBackgroundColor +
                ";'><span id='txtSeconds' class='seconds' style='padding: 20px;'></span>" +
                "<div style='padding-top: 35px; font-size: 14px; text-transform: uppercase;'>Seconds</div></div> ";
            countdownBoxHtml += "</div></div>";

            countdownBoxHtml += "<script>" +
                                " window.setTimeout('counterBoxEventDate();', 1000); " +
                                " function counterBoxEventDate() {{" +
                                " var objDiff = cms_setCounterBoxEventDate('" + countdownEventDate + "', new Date());" +
                                " document.getElementById('txtDays').innerHTML = objDiff.days;" +
                                " document.getElementById('txtHours').innerHTML = objDiff.hours;" +
                                " document.getElementById('txtMinutes').innerHTML = objDiff.minutes;" +
                                " document.getElementById('txtSeconds').innerHTML = objDiff.seconds;" +
                                " window.setTimeout('counterBoxEventDate();', 1000);" +
                                "}}" +
                                "</script>";
            return (string.Format(countdownBoxHtml, countdownId, countdownClass, numberOfColumns));
        }

        public string ParseCounterBox(dynamic obj)
        {
            var counterBoxHtml = @"<div id='{0}' class='{1}'>{2}</div>";

            var numberOfColumns = string.Empty;
            var counterBoxTitleFontColor = string.Empty;
            var counterBoxTitleFontSize = string.Empty;
            var counterBoxIconSize = string.Empty;
            var counterBoxIconTop = string.Empty;
            var counterBoxBodyFontColor = string.Empty;
            var counterBoxBodyFontSize = string.Empty;
            var counterBoxBorderColor = string.Empty;
            var cssClass = string.Empty;
            var cssDivId = string.Empty;

            var counterBox = obj["613"];
            if (obj["331"] != string.Empty && obj["331"] != null)
            {
                numberOfColumns = "width: " + (100 / Int32.Parse(obj["331"].ToString()) - 1.8) + '%';
            }
            if (obj["332"] != string.Empty && obj["332"] != null)
            {
                counterBoxTitleFontColor = obj["332"];
            }
            if (obj["333"] != string.Empty && obj["333"] != null)
            {
                counterBoxTitleFontSize = obj["333"];
            }
            if (obj["334"] != string.Empty && obj["334"] != null)
            {
                counterBoxIconSize = obj["334"];
            }
            if (obj["335"] != string.Empty && obj["335"] != null)
            {
                counterBoxIconTop = obj["335"];
            }
            if (obj["336"] != string.Empty && obj["336"] != null)
            {
                counterBoxBodyFontColor = obj["336"];
            }
            if (obj["337"] != string.Empty && obj["337"] != null)
            {
                counterBoxBodyFontSize = obj["337"];
            }
            if (obj["338"] != string.Empty && obj["338"] != null)
            {
                counterBoxBorderColor = obj["338"];
            }
            if (obj["339"] != string.Empty && obj["339"] != null)
            {
                cssClass = obj["339"];
            }
            if (obj["340"] != string.Empty && obj["340"] != null)
            {
                cssDivId = obj["340"];
            }
            var boxesHtml = string.Empty;
            if (counterBox != null)
            {
                foreach (var item in counterBox)
                {
                    var pmdivId = Guid.NewGuid().ToString().Replace("-", "_");
                    var suffixPrefix = (item["31"] == "suffix" ? "after" : "before");
                    var unitPosition =
                        string.Format(
                            "<style type='text/css'>.counterBoxUnit" + pmdivId + "::" + suffixPrefix +
                            "{{content: '{0}';}}</style>", item["30"]);
                    boxesHtml += string.Format(
                        "<div style='border: 1px solid {5}; padding: 10px; text-align: center; {6}; float: left;'><div style='line-height:normal;'>" +
                        "<i class='fa {9}' style='font-size:{2}px; padding-right: 10px;'></i>{12}" +
                        "<span id='{13}' class='counterBoxUnit{13}' data-value='{7}' data-direction='{10}' " +
                        "style='color:{0}; font-size:{1}px;'>{7}{8}</span>" +
                        "</div><span style='font-size:{3}px; color: {4}'>{11}</span></div>",
                        counterBoxTitleFontColor, counterBoxTitleFontSize, counterBoxIconSize, counterBoxBodyFontSize,
                        counterBoxBodyFontColor,
                        counterBoxBorderColor, numberOfColumns, item["28"], item["29"], item["32"], item["33"],
                        item["34"], unitPosition, pmdivId);
                }
            }
            return (string.Format(counterBoxHtml, cssDivId, cssClass, boxesHtml));
        }

        public string ParseFusionCode(dynamic obj)
        {
            var html = "<div>" + obj["411"] + "</div>";
            return html;
        }

        public string ParseTableBox(dynamic obj)
        {
            var html = "<div>" + obj["556"] + "</div>";
            return html;
        }

        public string ParseTabsBox(dynamic obj)
        {
            var divId = "";
            if (obj["564"] != string.Empty && obj["564"] != null)
            {
                divId = "id='" + obj["564"] + "'";
            }
            var justify = "nav-justified";
            if (obj["559"] != "yes")
            {
                justify = "";
            }
            var inactiveColor = "#f1f2f2";
            if (obj["561"] != string.Empty && obj["561"] != null)
            {
                inactiveColor = obj["561"];
            }
            var html = "<div class='fusion-tabs fusion-tabs-1 " + obj["557"] + " " + obj["558"] + "-tabs " + obj["563"] + "' " + divId + ">";
            html += "<style type='text/css' scoped='scoped'>";
            html += ".fusion-tabs.fusion-tabs-1 .nav-tabs li a {";
            html += " border-top-color:" + inactiveColor + ";";
            html += "background-color:" + inactiveColor + ";}";
            html += ".fusion-tabs.fusion-tabs-1 .nav-tabs {";

            var backgroundColor = "#ffffff";
            if (obj["560"] != string.Empty && obj["560"] != null)
            {
                backgroundColor = obj["560"];
            }
            html += "background-color:" + backgroundColor + ";}";
            html += ".fusion-tabs.fusion-tabs-1 .nav-tabs li.active a, .fusion-tabs.fusion-tabs-1 .nav-tabs li.active a:hover, .fusion-tabs.fusion-tabs-1 .nav-tabs li.active a:focus {";
            html += "border-right-color:" + backgroundColor + ";}";
            html +=
                ".fusion-tabs.fusion-tabs-1 .nav-tabs li.active a, .fusion-tabs.fusion-tabs-1 .nav-tabs li.active a:hover, .fusion-tabs.fusion-tabs-1 .nav-tabs li.active a:focus {";
            html += "background-color:" + backgroundColor + ";}";
            html += ".fusion-tabs.fusion-tabs-1 .nav-tabs li a:hover {";
            html += "background-color:" + backgroundColor + ";";
            html += "border-top-color:" + backgroundColor + ";}";
            html += ".fusion-tabs.fusion-tabs-1 .tab-pane {";
            html += "background-color:" + backgroundColor + ";}";
            html +=
                ".fusion-tabs.fusion-tabs-1 .nav, .fusion-tabs.fusion-tabs-1 .nav-tabs, .fusion-tabs.fusion-tabs-1 .tab-content .tab-pane {";
            var borderColor = "#ebeaea";
            if (obj["562"] != string.Empty && obj["562"] != null)
            {
                borderColor = obj["562"];
            }
            html += "border-color:" + borderColor + ";}";
            html += "</style>";
            html += "<div class='nav'>";
            html += "<ul class='nav-tabs " + justify + "'>";
            var tabs = obj["618"];
            var active = "active";
            var tabIds = new List<string>();
            var tabIcons = new List<string>();
            var firstTab = "";
            if (tabs != null)
            {
                foreach (var item in tabs)
                {
                    html += "<li class='" + active + "'>";
                    active = "";
                    var id = Guid.NewGuid().ToString().Replace("-", "_");
                    tabIds.Add(id);
                    var tabIcon = string.Empty;
                    if (item["73"] != string.Empty && item["73"] != null)
                    {
                        tabIcon = "<i class='fa fontawesome-icon " + item["73"] + "'></i>";
                    }
                    firstTab = "fusion-tab-" + item["72"];
                    tabIcons.Add(tabIcon);
                    html += "<a class='tab-link' id='fusion-tab-" + item["72"] + "' href='#" + id +
                            "' data-toggle='tab'>";
                    html += "<h4 class='fusion-tab-heading' data-fontsize='14' data-lineheight='20'>" + tabIcon +
                            item["72"] + "</h4>";
                    html += "</a></li>";
                }
            }
            var minHeight = "style='min-height: " + (60*tabIds.Count()).ToString() + "px;'";
            if (obj["558"] != "vertical")
            {
                minHeight = "";
            }
            html += "</ul></div>";
            html += "<div class='tab-content'>";
            active = "active";
            var active1 = " active in";
            var counter = 0;
            var style = "display:block;";
            if (tabs != null)
            {
                foreach (var item in tabs)
                {
                    html += "<div class='nav fusion-mobile-tab-nav'>";
                    html += "<ul class='nav-tabs " + justify + "'>";
                    html += "<li class='" + active + "'>";

                    html += "<a class='tab-link' id='A" + counter + 1 + "' href='#" + tabIds[counter] +
                            "' data-toggle='tab'>";
                    html += "<h4 class='fusion-tab-heading' data-fontsize='14' data-lineheight='20'>" +
                            tabIcons[counter] + item["72"] + "</h4>";
                    html += "</a></li></ul></div>";
                    html += "<div class='tab-pane fade " + active1 + "' id='" + tabIds[counter] + "' " + minHeight + ">";
                    if (obj["558"] != "vertical")
                    {
                        html += "<div class='fusion-sep-clear'></div>";
                        html +=
                            "<div class='fusion-separator fusion-full-width-sep sep-none' style='border-color: " +
                            borderColor +
                            "; margin-left: auto; margin-right: auto; margin-top: 5px; margin-bottom: 5px;'></div>";
                    }
                    html += item["74"];
                    html += "</div>";
                    active = "";
                    active1 = "";
                    counter += 1;
                }
            }
            html += "</div>";
            html += "</div>";
            html += "<div class='fusion-clearfix'></div>";

            return html;
        }

        public string ParseSlider(dynamic obj)
        {
            var html = string.Empty;
            if (!sliderLoaded)
            {
                html += "<link href='/Styles/Sliders/flexslider.css' rel='stylesheet' />";
                html += "<script src='/Scripts/Sliders/jquery.flexslider.js'></script>";
            }
            var id = Guid.NewGuid().ToString().Replace("-", "_");
            if (obj["507"] != null && obj["507"] != string.Empty)
            {
                id = obj["507"];
            }
            html += "<div class='fusion-slider-sc flexslider " + obj["506"] + "' style='max-width: " + obj["504"] + "; height: " + obj["505"] + ";' id='" + id + "'>";
            html += "<ul class='slides'>";
            if (obj["614"] != null)
            {
                var slides = obj["614"];
                foreach (var item in slides)
                {
                    var type = item["35"];
                    if (type == "image")
                    {
                        html += "<li class='image'>";
                        var lightBox = "";
                        string s = item["37"];
                        //if (s != null && !s.Contains("http"))
                        //{
                        //    s = "http://" + s;
                        //}
                        if (item["39"] != null && item["39"] != string.Empty)
                        {
                            lightBox = "lightbox-enabled";
                        }
                        if (item["37"] != null && item["37"] != string.Empty)
                        {
                            html += "<a class='" + lightBox + "' href='" + RelativeToAbsoluteUrl(s) + "' target='" + item["38"] + "'>";
                        }
                        var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(item["36"])).FirstOrDefault();
                        var altText = string.Empty;
                        if (upInfo != null)
                        {
                            altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                        }


                        html += "<img src='" + item["36"] + "' alt='" + item["175"] + ";" + altText + "' width='" + (obj["504"]!=null && obj["504"] != string.Empty ? obj["504"]: "100%") + "' height='" + (obj["505"] != null && obj["505"] != string.Empty ? obj["505"] : "100%") + "'/>";
                        //html += "<img src='" + item["36"] + "' />";

                        if (item["37"] != null && item["37"] != string.Empty)
                        {
                            html += "</a>";
                        }
                        html += "</li>";
                    }
                    if (type == "video")
                    {
                        if (item["88"] != null && item["88"] != string.Empty)
                        {
                            string s = item["88"];
                            var parameters = s.Split(' ');
                            var videoId = "";
                            var width = "";
                            var height = "";
                            if (parameters.Count() > 0)
                            {
                                videoId = parameters[1].Replace("id=", "").Replace("'", "");
                            }
                            if (parameters.Count() > 1)
                            {
                                width = parameters[2].Replace("width=", "").Replace("'", "");
                            }
                            if (parameters.Count() > 2)
                            {
                                height = parameters[3].Replace("height=", "").Replace("'", "");
                            }
                            html += "<li class='video'>";
                            html += "<div class='full-video'>";
                            if (s.Contains("youtube"))
                            {
                                html +=
                                    "<div class='fusion-video fusion-youtube' style='max-width: " + width +
                                    "px; max-height: " + height + "px;'>";
                                html += "<div class='video-shortcode video-responsive'>";
                                html +=
                                    "<iframe title='YouTube video player' src='https://www.youtube.com/embed/" + videoId +
                                    "?wmode=transparent' width='" + width + "' height='" + height +
                                    "' frameborder='0' allowfullscreen=''></iframe>";
                            }
                            if (s.Contains("vimeo"))
                            {
                                html +=
                                    "<div class='fusion-video fusion-vimeo' style='max-width: " + width +
                                    "px; max-height: " + height + "px;'>";
                                html += "<div class='video-shortcode'>";
                                html +=
                                    "<iframe src='https://player.vimeo.com/video/" + videoId + "?autoplay=0' width='" +
                                    width + "' height='" + height + "' frameborder='0' allowfullscreen=''></iframe>";
                            }
                            html += "</div>";
                            html += "</div>";
                            html += "</div>";
                            html += "</li>";
                        }

                    }
                }
            }
            html += "</ul>";
            html += "</div>";
            html += "<script>";
            html += "$(window).load(function () {";
            html += "$('#" + id + "').flexslider({";
            html += "animation: 'slide',";
            html += "controlsContainer: '.flex-container'";
            html += "});";
            html += "});";
            html += "</script>";

            sliderLoaded = true;               
            return html;
        }

        public string ParseLayerSlider(dynamic obj)
        {

            var html = string.Empty;
            if (!layerSliderLoaded)
            {
            html += "<script src='/Scripts/Sliders/jquery.eislideshow.js'></script>";
            html += "<script src='/Scripts/Sliders/jquery.easing.1.3.js'></script>";
            }
            var id = Guid.NewGuid().ToString().Replace("-", "_");
            if (obj["634"] != null && obj["634"] != string.Empty)
            {
                id = obj["634"];
            }
            html += "<div class='" + obj["633"] + "' id='" + id + "'>";
            html += "<div class='ei-slider' id='ei-slider-" + id + "'>";
            html += "<div class='ei-slider-loading' style='display: none;'>Loading</div>";
            html += "<div class='fusion-slider-loading'>Loading...</div>";
            html += "<ul class='ei-slider-large'>";

            if (obj["635"] != null)
            {
                var slides = obj["635"];
                foreach (var item in slides)
                {
                        html += "<li style='opacity: 0; z-index: 1; left: 0px;'>";
                        string s = item["91"];
                        //if (s != null && !s.Contains("http"))
                        //{
                        //    s = "http://" + s;
                        //}
                        if (item["91"] != null && item["91"] != string.Empty)
                        {
                            html += "<a href='" + RelativeToAbsoluteUrl(s) + "' target='" + item["92"] + "'>";
                        }
                        var upInfo =
                               _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(item["90"])).FirstOrDefault();
                        var altText = string.Empty;
                        if (upInfo != null)
                        {
                            altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                        }
                        html += "<img src='" + item["90"] + "' alt='" + item["175"] + ";" + altText + "' class='attachment-full size-full wp-post-image' width='" + (obj["447"] != null && obj["447"] != string.Empty ? obj["447"] : "100%") + "' height='" + (obj["632"] != null && obj["632"] != string.Empty ? obj["632"] : "100%") + "'>";
                        if (item["91"] != null && item["91"] != string.Empty)
                        {
                            html += "</a>";
                        }
                        html += "<div class='ei-title'>";

                        var largeFontSize = "42";
                        if (item["96"] != null && item["96"] != string.Empty)
                        {
                            largeFontSize = item["96"];
                        }
                        var smallFontSize = "20";
                        if (item["98"] != null && item["98"] != string.Empty)
                        {
                            smallFontSize = item["98"];
                        }
                        var largeLineHeight = Convert.ToInt32(largeFontSize) + 20;
                        var smallLineHeight = Convert.ToInt32(smallFontSize) + 20;
                        html +=
                            "<h2 style='opacity: 0; display: block; margin-right: 0px;font-size:" + largeFontSize + "px;line-height:" + largeLineHeight + "px;'>";
                        html += item["95"];
                        html += "</h2>";
                        html +=
                            "<h3 style='opacity: 0; display: block; margin-right: 0px;font-size:" + smallFontSize + "px;line-height:" + smallLineHeight + "px;'>";
                        html += item["97"];
                        html += "</h3>";
                        html += "</div>";
                        
                        html += "</li>";
                    

                    
                }
            }
            html += "</ul>";
            html += "<ul class='ei-slider-thumbs' style='max-width: 1000px;'>";
            html += "<li class='ei-slider-element' style='max-width: 200px; width: 20%; left: 400px;'>Current</li>";
            var c = 1;
            if (obj["635"] != null)
            {
                var slides = obj["635"];
                foreach (var item in slides)
                {
                    html +=
                        "<li style='max-width: 200px; width: 20%;'><a href='#'>Slide " + c + "</a>";

                    var upInfo =
                            _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(item["93"])).FirstOrDefault();
                    var altText = string.Empty;
                    if (upInfo != null)
                    {
                        altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                    }

                    html +=
                        "<img width='1200' height='550' src='" + item["93"] + "' alt='" + item["176"] + ";" + altText + "' class='attachment-full size-full wp-post-image' alt='' title='' /></li>";
                }
            }

            html += "</ul>";
            html += "</div>";
            html += "</div>";
            html += "<script>";
            html += "$(window).load(function () {";
            html += "$('#" + id + "').eislideshow({";
            html += "animation: 'center',";
            html += "autoplay			: true,";
            html += "slideshow_interval	: 3000,";
            html += "titlesFactor		: 0";
            html += "});";
            html += "});";
            html += "</script>";
            layerSliderLoaded = true;
            return html;
        }

        public string ParseSocialIcons(dynamic obj)
        {
            var divId = "";
            if (obj["542"] != null && obj["542"] != string.Empty)
            {
                divId = "id='" + obj["542"] + "'";
            }
            var html = "<div class='fusion-social-links " + obj["541"] + "' " + divId + ">";

            var boxedIcons = "";
            if (obj["508"] != null && obj["508"] == "yes")
            {
                boxedIcons = "boxed-icons";
            }
            html += "<div class='fusion-social-networks " + boxedIcons + "'>";
            html += "<div class='fusion-social-networks-wrapper'>";

            var iconColors = new string[0];
            var iconBoxColors = new string[0];
            if (obj["510"] != null && obj["510"] != string.Empty)
            {
                string s = obj["510"];
                iconColors = s.Split('|');
            }
            if (obj["511"] != null && obj["511"] != string.Empty)
            {
                string s = obj["511"];
                iconBoxColors = s.Split('|');
            }
            var borderRadious = "4px";
            if (obj["509"] != null && obj["509"] != string.Empty)
            {
                borderRadious = obj["509"];
            }
            var toolTipPos = "";
            if (obj["512"] != null && obj["512"] != string.Empty)
            {
                toolTipPos = obj["512"];
            }
            var i = 0;
            if (obj["518"] != null && obj["518"] != string.Empty)
            {
                string s = obj["518"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#10528d";
                    color = "ffffff";
                }
                else
                {
                    color = "#0274B3";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Linkedin");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-linkedin fusion-icon-linkedin' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4}; border-width: 0px;' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='Linkedin' data-original-title='Linkedin'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["513"] != null && obj["513"] != string.Empty)
            {
                string s = obj["513"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#3e5a96";
                    color = "ffffff";
                }
                else
                {
                    color = "#4c71bc";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Facebook");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-facebook fusion-icon-facebook' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4}; border-width: 0px;' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='Facebook' data-original-title='Facebook'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }    
            if (obj["514"] != null && obj["514"] != string.Empty)
            {
                string s = obj["514"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#428cc5";
                    color = "ffffff";
                }
                else
                {
                    color = "#2EACF6";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Twitter");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-twitter fusion-icon-twitter' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4}; border-width: 0px;' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='Twitter' data-original-title='Twitter'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["526"] != null && obj["526"] != string.Empty)
            {
                string s = obj["526"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#D82624";
                    color = "ffffff";
                }
                else
                {
                    color = "#D82624";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Youtube");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-youtube' target='_blank' href='{0}' style='color: {1}; background-color: transparent; border-color: transparent; border-radius: {4}; background-image: url(/Images/youtube36x36.png) !important; background-repeat: no-repeat; width: 16px; height: 16px; border-width: 0px;' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='Youtube' data-original-title='Youtube'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }            
            if (obj["515"] != null && obj["515"] != string.Empty)
            {
                string s = obj["515"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#000";
                    color = "ffffff";
                }
                else
                {
                    color = "#000";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Instagram");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-instagram' target='_blank' href='{0}' style='color: {1}; background-color: transparent; border-color: transparent; border-radius: {4}; background-image: url(/Images/instagram36x36.png) !important; background-repeat: no-repeat; width: 16px; height: 16px; border-width: 0px;' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='Instagram' data-original-title='Instagram'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["519"] != null && obj["519"] != string.Empty)
            {
                string s = obj["519"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#F60";
                    color = "ffffff";
                }
                else
                {
                    color = "#F60";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Blogger");
                var temp =
                     "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-blogger' target='_blank' href='{0}' style='color: {1}; background-color: transparent; border-color: transparent; border-radius: {4}; background-image: url(/Images/blog36x36.png) !important; background-repeat: no-repeat; width: 16px; height: 16px; border-width: 0px;' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='Blogger' data-original-title='Blogger'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["516"] != null && obj["516"] != string.Empty)
            {
                string s = obj["516"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#E54D88";
                    color = "ffffff";
                }
                else
                {
                    color = "#E54D88";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Dribbble");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-dribbble fusion-icon-dribbble' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Dribbble'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["517"] != null && obj["517"] != string.Empty)
            {
                string s = obj["517"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#DC4A38";
                    color = "ffffff";
                }
                else
                {
                    color = "#DC4A38";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Google+");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-googleplus fusion-icon-googleplus' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Google+'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }    
            if (obj["521"] != null && obj["521"] != string.Empty)
            {
                string s = obj["521"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#3D5A70";
                    color = "ffffff";
                }
                else
                {
                    color = "#3D5A70";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Tumblr");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-tumblr fusion-icon-tumblr' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Tumblr'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["522"] != null && obj["522"] != string.Empty)
            {
                string s = obj["522"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#1286D9";
                    color = "ffffff";
                }
                else
                {
                    color = "#1286D9";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Reddit");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-reddit fusion-icon-reddit' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Reddit'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["523"] != null && obj["523"] != string.Empty)
            {
                string s = obj["523"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#54267E";
                    color = "ffffff";
                }
                else
                {
                    color = "#54267E";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Yahoo");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-yahoo fusion-icon-yahoo' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Yahoo'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["524"] != null && obj["524"] != string.Empty)
            {
                string s = obj["524"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8"; 
                if (boxedIcons.Length > 0)
                {
                    backColor = "#4C5E51";
                    color = "ffffff";
                }
                else
                {
                    color = "#4C5E51";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Deviantart");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-deviantart fusion-icon-deviantart' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Deviantart'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["525"] != null && obj["525"] != string.Empty)
            {
                string s = obj["525"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#17AEE1";
                    color = "ffffff";
                }
                else
                {
                    color = "#17AEE1";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Vimeo");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-vimeo fusion-icon-vimeo' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Vimeo'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title,title);
                i = i + 1;
            }            
            if (obj["527"] != null && obj["527"] != string.Empty)
            {
                string s = obj["527"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#E71F28";
                    color = "ffffff";
                }
                else
                {
                    color = "#E71F28";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Pinterest");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-pinterest fusion-icon-pinterest' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Pinterest'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title,title);
                i = i + 1;
            }
            if (obj["528"] != null && obj["528"] != string.Empty)
            {
                string s = obj["528"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#F8991D";
                    color = "ffffff";
                }
                else
                {
                    color = "#F8991D";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Rss");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-rss fusion-icon-rss' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Rss'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["529"] != null && obj["529"] != string.Empty)
            {
                string s = obj["529"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#006699";
                    color = "ffffff";
                }
                else
                {
                    color = "#006699";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Digg");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-digg fusion-icon-digg' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Digg'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["530"] != null && obj["530"] != string.Empty)
            {
                string s = obj["530"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#FF0084";
                    color = "ffffff";
                }
                else
                {
                    color = "#FF0084";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Flickr");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-flickr fusion-icon-flickr' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Flickr'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["531"] != null && obj["531"] != string.Empty)
            {
                string s = obj["531"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#009D6D";
                    color = "ffffff";
                }
                else
                {
                    color = "#009D6D";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Forrst");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-forrst fusion-icon-forrst' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Forrst'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["532"] != null && obj["532"] != string.Empty)
            {
                string s = obj["532"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#003399";
                    color = "ffffff";
                }
                else
                {
                    color = "#003399";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Myspace");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-myspace fusion-icon-myspace' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Myspace'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["533"] != null && obj["533"] != string.Empty)
            {
                string s = obj["533"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#00AFF0";
                    color = "ffffff";
                }
                else
                {
                    color = "#00AFF0";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Skype");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-skype fusion-icon-skype' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Skype'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["534"] != null && obj["534"] != string.Empty)
            {
                string s = obj["534"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#316EA7";
                    color = "ffffff";
                }
                else
                {
                    color = "#316EA7";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Paypal");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-paypal fusion-icon-paypal' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Paypal'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["535"] != null && obj["535"] != string.Empty)
            {
                string s = obj["535"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#0089D1";
                    color = "ffffff";
                }
                else
                {
                    color = "#0089D1";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Dropbox");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-dropbox fusion-icon-dropbox' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Dropbox'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["536"] != null && obj["536"] != string.Empty)
            {
                string s = obj["536"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#FF6A22";
                    color = "ffffff";
                }
                else
                {
                    color = "#FF6A22";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Soundcloud");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-soundcloud fusion-icon-soundcloud' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Soundcloud'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["537"] != null && obj["537"] != string.Empty)
            {
                string s = obj["537"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#4C75A3";
                    color = "ffffff";
                }
                else
                {
                    color = "#4C75A3";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Vk");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-vk fusion-icon-vk' target='_blank' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Vk'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            if (obj["538"] != null && obj["538"] != string.Empty)
            {
                string s = obj["538"];
                if (s != null && !s.Contains("http"))
                {
                    s = "http://" + s;
                }
                var color = "#bebdbd";
                var backColor = "#e8e8e8";
                if (boxedIcons.Length > 0)
                {
                    backColor = "#f854aa";
                    color = "ffffff";
                }
                else
                {
                    color = "#E91416";
                    backColor = "#ffffff";
                }
                if (iconColors.Length > i)
                {
                    color = iconColors[i];
                }
                if (iconBoxColors.Length > i)
                {
                    backColor = iconBoxColors[i];
                }
                var title = (boxedIcons != string.Empty ? "" : "Mail");
                var temp =
                    "<a class='leftFloat fusion-social-network-icon fusion-tooltip fusion-mail fusion-icon-mail' target='_self' href='{0}' style='color: {1}; background-color: {2}; border-color: {3}; border-radius: {4};' data-placement='{5}' data-title='{6}' data-toggle='tooltip' title='{7}' data-original-title='Mail'></a>";
                html += string.Format(temp, s, color, backColor, backColor, borderRadious, toolTipPos, title, title);
                i = i + 1;
            }
            html += "</div>";
            html += "</div>";
            html += "</div>";


            return html;
        }

        public string ParseSeparatorElement(dynamic obj)
        {
            var html = "<div class='fusion-sep-clear'></div>";
            var divId = "";
            if (obj["489"] != string.Empty && obj["489"] != null)
            {
                divId = "id='" + obj["489"] + "'";
            }

            var sepClasses = "";
            if (obj["478"] != null && obj["478"] != string.Empty)
            {
                string s = obj["478"];
                var seps = s.Split('|');
                foreach (var sep in seps)
                {
                    sepClasses = "sep-" + sep + " ";
                }
            }
            var sepColor = "";
            if (obj["481"] != null && obj["481"] != string.Empty)
            {
                sepColor = obj["481"];
            }

            var alignment = "";
            if (obj["487"] != null && obj["487"] != string.Empty)
            {
                if (obj["487"] != "center")
                {
                    alignment = "float:" + obj["487"] + ";";
                }
            }


            if (sepClasses.Contains("sep-shadow"))
            {
                html +=
                    "<div class='fusion-separator sep-shadow " + obj["488"] + "' style='background: radial-gradient(ellipse at 50% - 50%, " + sepColor + " 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0); background: -webkit-radial-gradient(ellipse at 50% - 50%, " + sepColor + " 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0); background: -moz-radial-gradient(ellipse at 50% - 50%, " + sepColor + " 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0); background: -o-radial-gradient(ellipse at 50% - 50%, " + sepColor + " 0px, rgba(255, 255, 255, 0) 80%) repeat scroll 0 0 rgba(0, 0, 0, 0); margin-left: auto; margin-right: auto;" + alignment +" margin-top: " + obj["479"] + "; margin-bottom: " + obj["480"] + "; width: 100%; max-width: " + obj["486"] + "' " + divId + ">";
            }
            else
            {
                html +=
                    "<div class='fusion-separator " + sepClasses + " " + obj["488"] + "' style='border-color: " + sepColor + "; border-top-width: " + obj["482"] + "; border-bottom-width: " + obj["482"] + "; margin-left: auto; margin-right: auto;" + alignment + " margin-top: " + obj["479"] + "; margin-bottom: " + obj["480"] + "; width: 100%; max-width: " + obj["486"] + "' " + divId + ">";
            }
            if (obj["483"] != null && obj["483"] != string.Empty)
            {
                html +=
                    "<span class='icon-wrapper' style='border-color: " + (obj["484"] == "no" ? "transparent" : sepColor) + "; background-color: " + obj["485"] + ";'><i class='fa " + obj["483"] + "' style='color: " + sepColor + ";'></i></span>";
            }
            html += "</div>";
           return html;
        }

        public string ParseSectionSeparator(dynamic obj)
        {
            var divId = "";
            if (obj["477"] != string.Empty && obj["477"] != null)
            {
                divId = "id='" + obj["477"] + "'";
            }
            
            var sepPositions = new string[0];
            if (obj["470"] != null && obj["470"] != string.Empty)
            {
                string s = obj["470"];
                sepPositions = s.Split(',');
            }
            var style = "border";
            if (sepPositions.Length == 1)
            {
                style += "-" + sepPositions[0];
            }
            style += ":" + obj["473"] + " solid " + obj["474"] + ";";

            var html = "<div class='fusion-section-separator section-separator " + obj["476"] + "' style='"  + style + "' " + divId  + ">";
            
            if (obj["471"] != null && obj["471"] != string.Empty)
            {
                html += "<div class='section-separator-icon icon fa " + obj["471"] + "' style='color:" + obj["472"] + ";'></div>";
            }

            if (sepPositions.Length >= 1)
            {
                for (var i = 0; i < sepPositions.Length; i++)
                {
                    html += "<div class='divider-candy-arrow " + sepPositions[i] + "' style='border-" +
                            (sepPositions[i] == "top" ? "bottom" : "top") + "-color: " + obj["475"] + "'></div>";
                    html+="<div class='divider-candy " + sepPositions[i] + "' style='border-bottom: " + obj["473"] + " solid " + obj["474"] + "; border-left: " + obj["473"] + " solid " + obj["474"] +";'></div>";
                }
            }
            
            html += "</div>";
            return html;
        }

        public string ParseGoogleMap(dynamic obj)
        {
            var mapType = string.Empty;
            var mapWidth = string.Empty;
            var mapHeight = string.Empty;
            var zoomLevel = string.Empty;
            var scrollwheelonMap = string.Empty;
            var showScaleControlonMap = string.Empty;
            var showPanControlonMap = string.Empty;
            var addressPinAnimation = string.Empty;
            var showtooltipbydefault = string.Empty;
            var selecttheMapStylingSwitch = string.Empty;
            var mapOverlayColor = string.Empty;
            var infoboxStyling = string.Empty;
            var infoboxContent = string.Empty;
            var infoBoxTextColor = string.Empty;
            var infoBoxBackgroundColor = string.Empty;
            var customMarkerIcon = string.Empty;
            var address = string.Empty;
            var cssClass = string.Empty;
            var cssId = string.Empty;

            if (obj["427"] != string.Empty && obj["427"] != null)
            {
                mapType = obj["427"];
            }
            if (obj["428"] != string.Empty && obj["428"] != null)
            {
                mapWidth = obj["428"];
            }
            if (obj["429"] != string.Empty && obj["429"] != null)
            {
                mapHeight = obj["429"];
            }
            if (obj["430"] != string.Empty && obj["430"] != null)
            {
                zoomLevel = obj["430"];
            }
            if (obj["431"] != string.Empty && obj["431"] != null)
            {
                scrollwheelonMap = obj["431"];
            }
            if (obj["432"] != string.Empty && obj["432"] != null)
            {
                showScaleControlonMap = obj["432"];
            }
            if (obj["433"] != string.Empty && obj["433"] != null)
            {
                showPanControlonMap = obj["433"];
            }
            if (obj["434"] != string.Empty && obj["434"] != null)
            {
                addressPinAnimation = obj["434"];
            }
            if (obj["435"] != string.Empty && obj["435"] != null)
            {
                showtooltipbydefault = obj["435"];
            }
            if (obj["436"] != string.Empty && obj["436"] != null)
            {
                selecttheMapStylingSwitch = obj["436"];
            }
            if (obj["437"] != string.Empty && obj["437"] != null)
            {
                mapOverlayColor = obj["437"];
            }
            if (obj["438"] != string.Empty && obj["438"] != null)
            {
                infoboxStyling = obj["438"];
            }
            if (obj["439"] != string.Empty && obj["439"] != null)
            {
                infoboxContent = obj["439"];
            }
            if (obj["440"] != string.Empty && obj["440"] != null)
            {
                infoBoxTextColor = obj["440"];
            }
            if (obj["441"] != string.Empty && obj["441"] != null)
            {
                infoBoxBackgroundColor = obj["441"];
            }
            if (obj["442"] != string.Empty && obj["442"] != null)
            {
                customMarkerIcon = obj["442"];
            }
            if (obj["442"] != string.Empty && obj["442"] != null)
            {
                customMarkerIcon = obj["442"];
            }
            if (obj["443"] != string.Empty && obj["443"] != null)
            {
                address = obj["443"];
            }
            if (obj["444"] != string.Empty && obj["444"] != null)
            {
                cssClass = obj["444"];
            }
            if (obj["445"] != string.Empty && obj["445"] != null)
            {
                cssId = obj["445"];
            }
            var googleMapHtml = string.Empty;
            googleMapHtml += @"<script type='text/javascript' src='https://maps.googleapis.com/maps/api/js?sensor=false'></script>";
            googleMapHtml += "<script language='javascript' type='text/javascript'> window.onload =function() {cms_googleMap(";
            googleMapHtml += "'" + address + "',";
            googleMapHtml += "'" + mapType + "',";
            googleMapHtml += "'" + zoomLevel + "',";
            googleMapHtml += "'" + scrollwheelonMap + "',";
            googleMapHtml += "'" + showScaleControlonMap + "',";
            googleMapHtml += "'" + showPanControlonMap + "',";
            googleMapHtml += "'" + addressPinAnimation + "',";
            googleMapHtml += "'" + showtooltipbydefault + "',";
            googleMapHtml += "'" + selecttheMapStylingSwitch + "',";
            googleMapHtml += "'" + mapOverlayColor + "',";
            googleMapHtml += "'" + infoboxStyling + "',";
            googleMapHtml += "'" + infoboxContent + "',";
            googleMapHtml += "'" + infoBoxTextColor + "',";
            googleMapHtml += "'" + infoBoxBackgroundColor + "',";
            googleMapHtml += "'" + customMarkerIcon + "'";
            googleMapHtml += ");};</script>";
            googleMapHtml += "<div id='" + cssId + "' class='" + cssClass + "'><div id ='divGoogleMapShow' style='height: " + mapHeight + "; width: " + mapWidth + ";'></div></div>";
            return googleMapHtml;
        }

        public string ParseFlipBox(dynamic obj)
        {
            var flipID = obj["426"];
            var flipClass = obj["425"];
            var flipBoxHtml = "<div class='fusion-flip-boxes flip-boxes row fusion-columns-" + obj["424"] + "{0}' id='{1}'>{2}</div>";

            var flipBoxFrontHtml = string.Empty;
            var flipBoxBackHtml = string.Empty;
            var flipBoxList = obj["615"];
            var boxHtml = string.Empty;
            var flipBoxFrontStyle = string.Empty;
            var flipBoxBackStyle = string.Empty;
            var columnWidthClass = string.Empty;
            var iconClass = string.Empty;
            var iconStyle = string.Empty;
            var frontBoxIconImageHtml = string.Empty;
            int columnCount = Convert.ToInt16(obj["424"]);
            switch (columnCount)
            {
                case 1:
                    columnWidthClass = "col-lg-99 col-md-99 col-sm-99";
                    break;
                case 2:
                    columnWidthClass = "col-lg-49 col-md-49 col-sm-49";
                    break;
                case 3:
                    columnWidthClass = "col-lg-32 col-md-32 col-sm-32";
                    break;
                case 4:
                    columnWidthClass = "col-lg-24 col-md-24 col-sm-24";
                    break;
                case 5:
                    columnWidthClass = "col-lg-19 col-md-19 col-sm-19";
                    break;
                case 6:
                    columnWidthClass = "col-lg-16 col-md-16 col-sm-16";
                    break;
            }
            if (flipBoxList != null)
            {
                foreach (var flipbox in flipBoxList)
                {
                    flipBoxFrontStyle =
                        string.Format(
                            "style='border-width:{0}; border-style:solid; border-color:{1};border-radius:{2};color:{3};background-color:{4};width:80%;text-align:center;min-height: 206px;'",
                            (flipbox["51"] != null && flipbox["51"] != string.Empty ? flipbox["51"] : "1px"),
                            (flipbox["52"] != null && flipbox["52"] != string.Empty ? flipbox["52"] : "#FFFFFF"),
                            (flipbox["53"] != null && flipbox["53"] != string.Empty ? flipbox["53"] : "4px"),
                            (flipbox["46"] != null && flipbox["46"] != string.Empty ? flipbox["46"] : "#747474"),
                            (flipbox["44"] != null && flipbox["44"] != string.Empty ? flipbox["44"] : "#f6f6f6"));
                    flipBoxBackStyle =
                        string.Format(
                            "style='border-width:{0}; border-style:solid; border-color:{1};border-radius:{2};color:{3};min-height: 206px;background-color:{4};width:80%;text-align:center;'",
                            (flipbox["51"] != null && flipbox["51"] != string.Empty ? flipbox["51"] : "1px"),
                            (flipbox["52"] != null && flipbox["52"] != string.Empty ? flipbox["52"] : "#FFFFFF"),
                            (flipbox["53"] != null && flipbox["53"] != string.Empty ? flipbox["53"] : "4px"),
                            (flipbox["50"] != null && flipbox["50"] != string.Empty ? flipbox["50"] : "#FFFFFF"),
                            (flipbox["47"] != null && flipbox["47"] != string.Empty ? flipbox["47"] : "#A0CE4E"));
                    iconClass = ((flipbox["54"] != string.Empty && flipbox["54"] != null) ? flipbox["54"] : "fa-trophy") +
                                ((obj["56"] != string.Empty && obj["56"] != null && obj["56"] == "yes")
                                    ? " circle-yes "
                                    : "") +
                                ((flipbox["59"] != string.Empty && flipbox["59"] != null)
                                    ? ("fa-flip-" + flipbox["59"])
                                    : "") +
                                ((flipbox["60"] != string.Empty && flipbox["60"] != null)
                                    ? (" fa-rotate-" + flipbox["60"])
                                    : "") +
                                ((flipbox["61"] != string.Empty && flipbox["61"] != null && flipbox["61"] == "yes")
                                    ? " fa-spin"
                                    : "");
                    iconStyle =
                        string.Format(
                            "style='border-color:{0};border-width:1px;background-color:{1};line-height:42px;border-radius:50%;color:{2};font-size:21px;'",
                            (flipbox["58"] != string.Empty && flipbox["58"] != null) ? flipbox["58"] : "#000000",
                            (flipbox["57"] != string.Empty && flipbox["57"] != null) ? flipbox["57"] : "#000000",
                            (flipbox["55"] != string.Empty && flipbox["55"] != null) ? flipbox["55"] : "#ffffff");
                    if (flipbox["62"] != string.Empty && flipbox["62"] != null)
                    {
                        var imgPath = (flipbox["62"] != string.Empty && flipbox["62"] != null) ? flipbox["62"] : "";
                        var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == imgPath).FirstOrDefault();
                        var altText = string.Empty;
                        if (upInfo != null)
                        {
                            altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                        }

                        frontBoxIconImageHtml =
                            string.Format(
                                "<div class='flip-box-grafix  flip-box-image'><img src='{0}' width='{1}' height='{2}' alt='{3}'></img></div>",
                                imgPath,
                                (flipbox["63"] != string.Empty && flipbox["63"] != null) ? flipbox["63"] : "35",
                                (flipbox["64"] != string.Empty && flipbox["64"] != null) ? flipbox["64"] : "35",
                                altText);
                    }
                    else
                    {
                        frontBoxIconImageHtml =
                            string.Format(
                                "<div class='flip-box-grafix flip-box-circle' {0}><i class='fa {1}' style='color: #ffffff;'></i></div>",
                                iconStyle,
                                iconClass);
                    }

                    flipBoxFrontHtml =
                        string.Format(
                            "<div class='flip-box-front' {0}><div class='flip-box-front-inner'>{1}<h2 class='flip-box-heading' style='color: {2};' data-fontsize='18' data-lineheight='27'>{3}</h2>{4}</div></div>",
                            flipBoxFrontStyle,
                            frontBoxIconImageHtml,
                            (flipbox["45"] != null && flipbox["45"] != string.Empty ? flipbox["45"] : "#eeeded"),
                            (flipbox["40"] != null && flipbox["40"] != string.Empty ? flipbox["40"] : ""),
                            (flipbox["42"] != null && flipbox["42"] != string.Empty ? flipbox["42"] : ""));
                    flipBoxBackHtml =
                        string.Format(
                            "<div class='flip-box-back' {0}><div class='flip-box-back-inner'><h3 class='flip-box-heading-back' style='color: {1};' data-fontsize='14' data-lineheight='20'>{2}</h3>{3}</div></div>",
                            flipBoxBackStyle,
                            (flipbox["48"] != null && flipbox["48"] != string.Empty ? flipbox["48"] : "#eeeded"),
                            (flipbox["41"] != null && flipbox["41"] != string.Empty ? flipbox["41"] : ""),
                            (flipbox["43"] != null && flipbox["43"] != string.Empty ? flipbox["43"] : ""));
                    boxHtml +=
                        string.Format(
                            "<div class='fusion-flip-box-wrapper fusion-column {0}'><div class='fusion-flip-box' onmouseover='{3}' onmouseout='{4}'><div class='flip-box-inner-wrapper' style='min-height: 208px;'>{1}{2}</div></div></div>",
                            columnWidthClass, flipBoxFrontHtml, flipBoxBackHtml, @"$(this).addClass(""hover"");",
                            @"$(this).removeClass(""hover"")");
                }
            }
            //return html;
            return (string.Format(flipBoxHtml, flipClass, flipID, boxHtml));
        }

        public string ParseImageFrame(dynamic obj)
        {
            var divId = "";
            if (obj["307"] != string.Empty && obj["307"] != null)
            {
                divId = "id='" + obj["307"] + "'";
            }
            var html = "";

            var align = "";
            if (obj["296"] != null && obj["296"] != string.Empty)
            {
                if (obj["296"] == "center")
                {
                    align = obj["296"];
                }
            }

            if (align == "center")
            {
                html += "<div class='imageframe-align-center'>";
            }

            html += "<span class='fusion-imageframe " + obj["306"] + " imageframe-" + obj["291"] + " ";
            
            if (obj["291"] != null && obj["291"] != string.Empty)
            {
                if (obj["291"] == "bottomshadow")
                {
                    html += " element-bottomshadow ";
                }
            }
            //if (obj["306"] != null && obj["306"] != string.Empty)
            //{
            //    html += obj["306"] + " ";
            //}

            if (obj["636"] != null && obj["636"] != string.Empty)
            {
                if (obj["636"] == "yes")
                {
                    html += "fusion-hide-on-mobile hover-type-zoomin";
                }
            }

            html += "' style='";

            if (obj["291"] != "none")
            {
                html += "border: 6px solid #f6f6f6;";
            }

            if (obj["296"] == "right")
            {
                html += "margin-left: 25px; text-align: right;";
            }
            if (obj["296"] == "left")
            {
                html += "margin-right: 25px; text-align: left;";
            }

            html += "' " + divId +">";
            if (obj["301"] != null && obj["301"] != string.Empty)
            {
                string s = obj["301"];
                //if (s != null && !s.Contains("http"))
                //{
                //    s = "http://" + s;
                //}
                html += "<a href='" + RelativeToAbsoluteUrl(s) + "' target='" + obj["302"] + "'>";
            }
            var imgPath = Convert.ToString(obj["299"]);
            var upInfo =
                    _uploadInformationRepository.GetAll().Where(x => x.FilePath == imgPath).FirstOrDefault();
            var altText = string.Empty;
            if (upInfo != null)
            {
                altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
            }

            html += "<img " + divId + " alt='" + obj["300"] + ";" + altText + "' class='img-responsive' style='";

            if (obj["291"] == "dropshadow")
            {
                html += "-moz-box-shadow: 0 0 3px rgba(0, 0, 0, .3); -webkit-box-shadow: 0 0 3px rgba(0, 0, 0, .3); box-shadow: 0 0 3px rgba(0, 0, 0, .3);";
            }
            if (obj["291"] == "bottomshadow")
            {
                html += "moz-box-shadow: 2px 3px 7px rgba(30, 115, 190, .3);-webkit-box-shadow: 2px 3px 7px rgba(30, 115, 190, .3); box-shadow: 2px 3px 7px rgba(30, 115, 190, .3);";
            }

            if (obj["293"] != null && obj["293"] != string.Empty)
            {
                html += " border:" + obj["293"] + " solid " + obj["292"] + ";";
            }

            if (obj["294"] != null && obj["294"] != string.Empty)
            {
                html += " -webkit-border-radius: " + obj["294"] +  "; -moz-border-radius: " + obj["294"] + "; border-radius: " + obj["294"] + "; cursor:pointer;";
            }
            html += "'";

            if (obj["299"] != null && obj["299"] != string.Empty)
            {
                html += " src='" + obj["299"] + "'";
            }

            if (obj["297"] != null && obj["297"] == "yes")
            {
                html += " onmouseover=\"this.src='" + obj["298"] + "'\" onmouseout=\"this.src='" + obj["299"] + "'\"";
            }

            html += "/>";

            if (obj["301"] != null && obj["301"] != string.Empty)
            {
                html += "</a>";
            }
            html += "</span>";
            if (align == "center")
            {
                html += "</div>";
            }
            return html;
        }

        public string ParseImageCarousel(dynamic obj)
        {
            if (obj["448"] == "horizontal")
            {
                return ImageCarouselHorizontal(obj);
            }
            else
            {
                return ImageCarouselVertical(obj);
            }
            
        }

        string ImageCarouselHorizontal(dynamic obj)
        {
            var html = string.Empty;
            if (!imageHorCarouselLoaded)
            {
                html += "<script src='/Scripts/Sliders/jssor.slider.js'></script>";
                html += "<link href='/Styles/Sliders/JsorHorizontalSlider.css' rel='stylesheet' />";
            }
            var id = Guid.NewGuid().ToString().Replace("-", "_");
            if (obj["459"] != null && obj["459"] != string.Empty)
            {
                id = obj["459"];
            }
            html += "<div class='" + obj["458"] + "' id='" + id + "' style='position: relative; margin: 0 auto; top: 0px; left: 0px; width: 600px; height: 300px; overflow: hidden; visibility: hidden;'>";
            html += "<div data-u='loading' style='position: absolute; top: 0px; left: 0px;'>";
            html += "<div style='filter: alpha(opacity=70); opacity: 0.7; position: absolute; display: block; top: 0px; left: 0px; width: 100%; height: 100%;'></div>";
            html += "<div style='position:absolute;display:block;background:url(/images/loading.gif) no-repeat center center;top:0px;left:0px;width:100%;height:100%;'></div>";
            html += "</div>";
            html +=
                "<div data-u='slides' style='cursor: default; position: relative; top: 0px; left: 0px; width: 600px; height: 300px; overflow: hidden;'>";
            var border = string.Empty;
            if (obj["456"] == "yes")
            {
                border = "border:1px solid #000000;";
            }
            if (obj["617"] != null)
            {
                var slides = obj["617"];
                foreach (var item in slides)
                {
                    html += "<div data-p='112.50' style='display: none;'>";
                    string s = item["68"];
                    if (item["68"] != null && item["68"] != string.Empty)
                    {
                        html += "<a href='" + RelativeToAbsoluteUrl(s) + "' target='" + item["69"] + "'>";
                    }


                    var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(item["70"])).FirstOrDefault();
                    var altText = string.Empty;
                    if (upInfo != null)
                    {
                        altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                    }
                    altText = item["71"] + ";" + altText;
                    html += "<img class='clickToStopPlay' src='" + item["70"] + "' alt='" + altText + "' data-u='image'";
                    if (border != "")
                    {
                        html += " style='" + border + "'";
                    }
                    html += "/>";
                    if (item["68"] != null && item["68"] != string.Empty)
                    {
                        html += "</a>";
                    }
                    html += "<img class='clickToStartPlay' src='" + item["70"] + "' alt='" + altText + "' data-u='thumb'/>";
                    html += "</div>";
                }
            }
            html += "</div>";
            html += "<div u='thumbnavigator' class='jssort03' style='position:absolute;left:0px;bottom:0px;width:100%;height:60px;' data-autocenter='1'>";
            html += "<div style='position: absolute; top: 0; left: 0; width: 100%; height:100%; filter:alpha(opacity=30.0); opacity:0.3;'></div>";
            html += "<div u='slides' style='cursor: default;'>";
            html += "<div u='prototype' class='p'>";
            html += "<div class='w'>";
            html += "<div data-u='thumbnailtemplate' class='t'></div>";
            html += "</div>";
            html += "<div class='c'></div>";
            html += "</div>";
            html += "</div>";
            html += "</div>";
            html +=
                "<span data-u='arrowleft' class='jssora02l clickToStartPlay' style='top:0px;left:8px;width:55px;height:55px;' data-autocenter='2'></span>";
            html +=
                "<span data-u='arrowright' class='jssora02r clickToStartPlay' style='top:0px;right:8px;width:55px;height:55px;' data-autocenter='2'></span>";
            html += "</div>";
            var autoplay = "false";
            var maxCols = "5";
            var colSpacing = "13";
            if (obj["450"] == "yes")
            {
                autoplay = "true";
            }
            if (obj["451"] != null && obj["451"] != string.Empty)
            {
                maxCols = obj["451"];
            }
            if (obj["452"] != null && obj["452"] != string.Empty)
            {
                colSpacing = obj["452"];
            }

            html += "<script>";
            html += "$(document).ready(function ($) {";
            html += "var jssor_" + id + "_options = {";
            html += "$AutoPlay: " + autoplay + ",";
            html += "$ArrowNavigatorOptions: {";
            html += "$Class: $JssorArrowNavigator$";
            html += "},";
            html += "$ThumbnailNavigatorOptions: {";
            html += "$Class: $JssorThumbnailNavigator$,";
            html += "$Cols: " + maxCols + ",";
            html += "$SpacingX: " + colSpacing + ",";
            html += "$SpacingY: " + colSpacing + ",";
            html += "$Align: 260";
            html += "}";
            html += "};";
            html += "var jssor_" + id + "_slider = new $JssorSlider$('" + id + "', jssor_" + id + "_options);";
            html += "function ScaleSlider() {";
            html += "var refSize = jssor_" + id + "_slider.$Elmt.parentNode.clientWidth;";
            html += "if (refSize) {";
            html += "refSize = Math.min(refSize, 600);";
            html += "jssor_" + id + "_slider.$ScaleWidth(refSize);";
            html += "}";
            html += "else {";
            html += "window.setTimeout(ScaleSlider, 30);";
            html += "}}";
            html += "ScaleSlider();";
            html += "$(window).bind('load', ScaleSlider);";
            html += "$(window).bind('resize', ScaleSlider);";
            html += "$(window).bind('orientationchange', ScaleSlider);";
            html += "function Play() {jssor_" + id + "_slider.$Play();}; function Pause() {jssor_" + id + "_slider.$Pause();};";
            html += "$('.clickToStartPlay').click(Play); $('.clickToStopPlay').click(Pause);";
            html += "});";

            html += "</script>";

            imageHorCarouselLoaded = true;
            return html;
        }

        string ImageCarouselVertical(dynamic obj)
        {
            var html = string.Empty;
            if (!imageVerCarouselLoaded)
            {
                html += "<script src='/Scripts/Sliders/jssor.slider.js'></script>";
                html += "<link href='/Styles/Sliders/JsorVerticalSlider.css' rel='stylesheet' />";
            }
            var id = Guid.NewGuid().ToString().Replace("-", "_");
            if (obj["459"] != null && obj["459"] != string.Empty)
            {
                id = obj["459"];
            }
            html += "<div class='" + obj["458"] + "' id='" + id + "' style='position: relative; margin: 0 auto; top: 0px; left: 0px; width: 810px; height: 300px; overflow: hidden; visibility: hidden;'>";
            html += "<div data-u='loading' style='position: absolute; top: 0px; left: 0px;'>";
            html += "<div style='filter: alpha(opacity=70); opacity: 0.7; position: absolute; display: block; top: 0px; left: 0px; width: 100%; height: 100%;'></div>";
            html += "<div style='position:absolute;display:block;background:url(/images/loading.gif) no-repeat center center;top:0px;left:0px;width:100%;height:100%;'></div>";
            html += "</div>";
            html +=
                "<div data-u='slides' style='cursor: default; position: relative; top: 0px; left: 0px; width: 600px; height: 300px; overflow: hidden;'>";
            var border = string.Empty;
            if (obj["456"] == "yes")
            {
                border = "border:1px solid #000000;";
            }
            if (obj["617"] != null)
            {
                var slides = obj["617"];
                foreach (var item in slides)
                {
                    html += "<div data-p='112.50' style='display: none;'>";
                    string s = item["68"];
                    if (item["68"] != null && item["68"] != string.Empty)
                    {
                        html += "<a href='" + RelativeToAbsoluteUrl(s) + "' target='" + item["69"] + "'>";
                    }

                    var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(item["70"])).FirstOrDefault();
                    var altText = string.Empty;
                    if (upInfo != null)
                    {
                        altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                    }
                    altText = item["71"] + ";" + altText;
                    html += "<img class='clickToStopPlay' src='" + item["70"] + "' alt='" + altText + "' data-u='image'";
                    if (border != "")
                    {
                        html += " style='" + border + "'";
                    }
                    html += "/>";
                    if (item["68"] != null && item["68"] != string.Empty)
                    {
                        html += "</a>";
                    }
                    html += "<div data-u='thumb'>";

                    html += "<img class='t clickToStartPlay' src='" + item["70"] + "' alt='" + altText + "' />";
                    html += "</div>";
                    html += "</div>";
                }
            }
            html += "</div>";
            html += "<div u='thumbnavigator' class='jssort11' style='position:absolute;right:5px;top:0px;font-family:Arial, Helvetica, sans-serif;-moz-user-select:none;-webkit-user-select:none;-ms-user-select:none;user-select:none;width:200px;height:300px;' data-autocenter='2'>";
            //html += "<div style='position: absolute; top: 0; left: 0; width: 100%; height:100%; filter:alpha(opacity=30.0); opacity:0.3;'></div>";
            html += "<div u='slides' style='cursor: default;'>";
            html += "<div u='prototype' class='p'>";
            html += "<div data-u='thumbnailtemplate' class='w'></div>";
            html += "</div>";
            html += "</div>";
            html += "</div>";
            html +=
                "<span data-u='arrowleft' class='jssora02l clickToStartPlay' style='top:0px;left:8px;width:55px;height:55px;' data-autocenter='2'></span>";
            html +=
                "<span data-u='arrowright' class='jssora02r clickToStartPlay' style='top:0px;right:218px;width:55px;height:55px;' data-autocenter='2'></span>";
            html += "</div>";
            var autoplay = "false";
            var maxCols = "5";
            var colSpacing = "13";
            if (obj["450"] == "yes")
            {
                autoplay = "true";
            }
            if (obj["451"] != null && obj["451"] != string.Empty)
            {
                maxCols = obj["451"];
            }
            if (obj["452"] != null && obj["452"] != string.Empty)
            {
                colSpacing = obj["452"];
            }

            html += "<script>";
            html += "$(document).ready(function ($) {";
            html += "var jssor_" + id + "_options = {";
            html += "$AutoPlay: " + autoplay + ",";
            html += "$ArrowNavigatorOptions: {";
            html += "$Class: $JssorArrowNavigator$";
            html += "},";
            html += "$ThumbnailNavigatorOptions: {";
            html += "$Class: $JssorThumbnailNavigator$,";
            html += "$Cols: " + maxCols + ",";
            html += "$SpacingX: " + colSpacing + ",";
            html += "$SpacingY: " + colSpacing + ",";
            html += "$Orientation: 2,";
            html += "$Align: 0";
            html += "}";
            html += "};";
            html += "var jssor_" + id + "_slider = new $JssorSlider$('" + id + "', jssor_" + id + "_options);";
            html += "function ScaleSlider() {";
            html += "var refSize = jssor_" + id + "_slider.$Elmt.parentNode.clientWidth;";
            html += "if (refSize) {";
            html += "refSize = Math.min(refSize, 810);";
            html += "jssor_" + id + "_slider.$ScaleWidth(refSize);";
            html += "}";
            html += "else {";
            html += "window.setTimeout(ScaleSlider, 30);";
            html += "}}";
            html += "ScaleSlider();";
            html += "$(window).bind('load', ScaleSlider);";
            html += "$(window).bind('resize', ScaleSlider);";
            html += "$(window).bind('orientationchange', ScaleSlider);";
            html += "function Play() {jssor_" + id + "_slider.$Play();}; function Pause() {jssor_" + id + "_slider.$Pause();};";
            html += "$('.clickToStartPlay').click(Play); $('.clickToStopPlay').click(Pause);";
            html += "});";
            html += "</script>";

            imageVerCarouselLoaded = true;
            return html;
        }

        public string ParsePricingTable(dynamic obj)
        {
            var html = string.Empty;
            var backgroundColor = string.Empty;
            var borderColor = string.Empty;
            var dividerColor = string.Empty;
            if (obj["462"] != string.Empty && obj["462"] != null)
            {
                backgroundColor = obj["462"];
            }
            if (obj["463"] != string.Empty && obj["463"] != null)
            {
                borderColor = obj["463"];
            }
            if (obj["464"] != string.Empty && obj["464"] != null)
            {
                dividerColor = obj["464"];
            }
            if (!imageVerCarouselLoaded)
            {
                html += "<script src='/Scripts/PricingTables/pricing_tables_main.js'></script>";
                html += "<script src='/Scripts/PricingTables/pricing_tables_modernizr.js'></script>";
                html += "<link href='/Styles/PricingTables/pricing_tables_style.css' rel='stylesheet' />";
            }
            html += "<style type='text/css' scoped='scoped'>";
            html += ".cd-pricing-wrapper {";
            html += " border: 1px solid " + borderColor + ";";
            html += " background-color:" + backgroundColor + ";}";
            html += ".cd-pricing-features li {";
            html += " border-top: 1px solid " + dividerColor + ";";
            html += "</style>";

            var cssClass = string.Empty;
            var cssId = string.Empty;
            var shortCode = string.Empty;
            if (obj["466"] != string.Empty && obj["466"] != null)
            {
                cssClass = obj["466"];
            }
            if (obj["467"] != string.Empty && obj["467"] != null)
            {
                cssId = obj["467"];
            }
            if (obj["468"] != string.Empty && obj["468"] != null)
            {
                shortCode = obj["468"];
            }

            Regex regex = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
            Match match;
            for (match = regex.Match(shortCode); match.Success; match = match.NextMatch())
            {
                if (!match.Groups[1].Value.Contains("http") && !match.Groups[1].Value.Contains("#"))
                {
                    shortCode = shortCode.Replace(match.Groups[1].Value, "http://" + match.Groups[1].Value);
                }
            }
            html += "<div id='" + cssId + "' class='" + cssClass + "'>" + shortCode + "</div>";
            imageVerCarouselLoaded = true;
            return html;
        }

        public string ParseVideo(dynamic obj)
        {
            var html = string.Empty; 
            var video = obj["642"];
            string videoLink = obj["639"];
            var style = "";
            var width = "";
            var height = "";
            var alignmentType = "";
            var autoplayVideo = "";
            var id = Guid.NewGuid().ToString().Replace("-", "_");

            if (obj["637"]!=null && obj["637"] != "")
            {
                style += "max-width:" + obj["637"] + "px;";
                width = obj["637"];
            }
            else
            {
                style += "max-width:600px;";
                width = "600";
            }

            if (obj["638"] != null && obj["638"] != "")
            {
                style += "max-height:" + obj["638"] + "px;";
                height = obj["638"];
            }
            else
            {
                style += "max-height:350px;";
                height = "350";
            }

            if (obj["641"] == "yes")
            {
                autoplayVideo = "autoplay";
            }

            if (obj["645"] != null && obj["645"] != "")
            {
                alignmentType = obj["645"];
            }
            if (videoLink != null && !videoLink.Contains("http"))
            {
                videoLink = "http://" + videoLink;
            }
            if (obj["644"] != null && obj["644"] != string.Empty)
            {
                id = obj["644"];
            }

            html += @"<div style='text-align: " + alignmentType + ";'>";
            if (videoLink != null && videoLink != "")
            {
                html += "<a href='" + videoLink + "' target='" + obj["640"] + "'>";
            }
            html += "<video class='" + obj["643"] + "' id='" + id + "' width='" + width + "' height='" + height + "' style='object-fit:fill;' controls " + autoplayVideo + ">";

            html += "<source src='" + obj["642"] + "' type='video/mp4'>";
            html += "</video>";
            if (videoLink != null && videoLink != "")
            {
                html += "</a>";
            }
            html += "</div>";
            return html;
        }

        public string ParseSliderWithThumb(dynamic obj)
        {
            var html = string.Empty;
            var html1 = string.Empty;
            var html2 = string.Empty;
            if (!sliderWithThumbLoaded)
            {
                html += "<script src='/Scripts/Sliders/Slider.Thumb.js'></script>";
                html += "<link href='/Styles/Sliders/Slider.Thumb.css' rel='stylesheet' />";
            }
            var id = Guid.NewGuid().ToString().Replace("-", "_");
            var videoId = "0";
            var numberVideo = 0;
            var colWidthMain = "1200px";
            var colWidthSub = "width: 800px";
            var colHeightMain = "600px";
            var colHeightSub = "600px";
            if (obj["667"] != null && obj["667"] != string.Empty)
            {
                id = obj["667"];
            }
            if (obj["664"] != null && obj["664"] != string.Empty)
            {
                colWidthMain = obj["664"].ToString() + "px";
                colWidthSub = (Int32.Parse(obj["664"].ToString()) - 200) + "px";
            }
            if (obj["665"] != null && obj["665"] != string.Empty)
            {
                colHeightMain = obj["665"].ToString() + "px";
                colHeightSub = (Int32.Parse(obj["665"].ToString())) + "px";
            }
            html += "<div id='" + id + "' class='mainHeight " + obj["666"] + "' style='width:" + colWidthMain + "; height: 500px; position: relative; margin: 0 .5em .6em; top: 0px; left: 0px; overflow: hidden; visibility: hidden;'>" +
                "<div data-u='slides' style='cursor: default; position: relative; top: 0px; left: 0px;border:1px solid #D4D3D3; width: " + colWidthSub + "; height: " + colHeightSub + "; overflow: hidden;'>";
            if (obj["668"] != null)
            {
                var slidesThumb = obj["668"];
                foreach (var slidesImg in slidesThumb)
                {
                    videoId = Guid.NewGuid().ToString().Replace("-", "_");

                    var upInfo =
                    _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(slidesImg["100"])).FirstOrDefault();
                    var altText = string.Empty;
                    if (upInfo != null)
                    {
                        altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                    }
                    var upInfo1 =
                        _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(slidesImg["99"])).FirstOrDefault();
                    var altText1 = string.Empty;
                    if (upInfo1 != null)
                    {
                        altText1 = !string.IsNullOrEmpty(upInfo1.AltText) ? upInfo1.AltText : string.Empty;
                    }

                    if (slidesImg["110"] == 2)
                    {
                        if (slidesImg["111"] == 1 || slidesImg["111"] == 2)
                        {
                            if (!string.IsNullOrEmpty(slidesImg["112"].ToString()))
                            {
                                var videoSrc = "";
                                if (slidesImg["112"].ToString().Contains("?"))
                                {
                                    videoSrc = slidesImg["112"] + "&enablejsapi=1";
                                }
                                else
                                {
                                    videoSrc = slidesImg["112"] + "?enablejsapi=1";
                                }
                                numberVideo = numberVideo + 1;
                                html += "<div data-p='112.50' style='display: none;'>" +
                                        "<iframe id='" + videoId + "' src='" + videoSrc + "' width='100%' height='600' frameborder='0'" +
                                        "style='z-index: 1; margin-top: 0px; margin-left: 0px; transform: translate3d(0px, 0px, 0px);'></iframe>" +
                                        "<div data-u='thumb'><img class='clickToStartPlay' data-u='image' style='width: 150px; height: 100px; margin-top: 1px; margin-left: 1px;' src='" + slidesImg["100"] + "' alt='" + slidesImg["176"] + ";" + altText + "' />" +
                                        "</div></div>";
                                html1 += " var player" + numberVideo + ";";
                                html2 += " player" + numberVideo + " = new YT.Player('" + videoId + "', {events: {'onStateChange': pauseYouTubeVideo}});";                                       
                            }
                        }
                        if (slidesImg["111"] == 3)
                        {
                            html += "<div data-p='112.50' style='display: none;'>" +
                                    "<video id='" + videoId + "' width='100%' height='600' controls><source src='" + slidesImg["112"] + "' type='video/mp4'></video>" +
                                    "<div data-u='thumb'><img class='clickToStartPlay' data-u='image' style='width: 150px; height: 100px; margin-top: 1px; margin-left: 1px;' src='" + slidesImg["100"] + "' alt='" + slidesImg["176"] + ";" + altText + "' />" +
                                    "</div></div>";
                        }
                    }
                    else
                    {
                        html += "<div data-p='112.50' style='display: none;'>" +
                                "<img class='clickToStopPlay' data-u='image' src='" + slidesImg["99"] + "' alt='" + slidesImg["175"] + ";" + altText1 + "' style='width: 600px; height: 400px;'/>" +
                                "<div data-u='thumb'><img data-u='image' class='clickToStartPlay' style='width: 150px; height: 100px; margin-top: 1px; margin-left: 1px;' src='" + slidesImg["100"] + "' alt='" + slidesImg["176"] + ";" + altText + "' />" +
                                "</div>" +
                                "</div>";
                    }
                }
            }
            html += "</div>";
            html += "<div id='thumbnavigatorNsc' data-u='thumbnavigator' class='jssort12-200-69 thumbTop' style='position: absolute; left: 0px; top: 410px; width: 600px; height: 100px;' data-autocenter='2'>";
            html += "<div id='thumbnavigatorSildes'data-u='slides' style='cursor: default; top: 0px; left: 120px;'>" +
                    "<div data-u='prototype' class='p'><div class='w'><div data-u='thumbnailtemplate' class='c'></div></div></div></div></div>";
            html += "<div data-u='navigator' class='jssorb20' style='bottom: 0px; right: 16px;' data-autocenter='1'>" +
                    "<div data-u='prototype' style='width: 19px; height: 19px;'><div data-u='numbertemplate'></div></div></div>";
            html += "<span data-u='arrowleft' class='jssora02l' style='top: 0px; left: 115px; width: 55px; height: 55px;' data-autocenter='2'></span>";
            html += "<span data-u='arrowright' class='jssora02r' style='top: 0px; right: 2px; width: 55px; height: 55px;' data-autocenter='2'></span>";
            html += "</div>";
           // html += "<style>#" + videoId + "{height:600px !important;}</style>";
            if (html1 != string.Empty && html2 != string.Empty)
            {
                html += "<script type='text/javascript' src='https://www.youtube.com/player_api'></script>";
            }
            html += "<script>sliderWithThumb_init('" + id + "');";
            if (html1 != string.Empty && html2 != string.Empty)
            {             
                html += html1;
                html += " function onYouTubePlayerAPIReady() {" + html2 + "};";
            }
            html += "</script>";
            sliderWithThumbLoaded = true;
            return html;
        }

        public string ParseLightSlider(dynamic obj)
        {
            var html = string.Empty;
            var html1 = string.Empty;
            var html2 = string.Empty;
            if (!sliderWithThumbLoaded)
            {
                html += "<script src='/Scripts/Sliders/lightslider.min.js'></script>";
                html += "<link href='/Styles/Sliders/lightslider.min.css' rel='stylesheet' />";
            }
            var id = Guid.NewGuid().ToString().Replace("-", "_");
            var videoId = "0";
            var numberVideo = 0;
            var colWidthMain = "800px";
            var colWidthSub = "width: 600px";
            var colHeightMain = "400px";
            var colHeightSub = "400px";
            if (obj["667"] != null && obj["667"] != string.Empty)
            {
                id = obj["667"];
            }
            if (obj["664"] != null && obj["664"] != string.Empty)
            {
                colWidthMain = obj["664"].ToString() + "px";
                colWidthSub = (Int32.Parse(obj["664"].ToString()) - 200) + "px";
            }
            if (obj["665"] != null && obj["665"] != string.Empty)
            {
                colHeightMain = obj["665"].ToString() + "px";
                colHeightSub = (Int32.Parse(obj["665"].ToString())) + "px";
            }

            if (!sliderWithThumbLoaded)
            {
                html += "<style>";
                html += ".lightslderNSC {width: " + colWidthMain + ";height:" + colHeightMain + ";} .lightslderNSC ul {list-style: none outside none;padding-left: 0;margin-bottom:0;} .lightslderNSC  li {display: block;float: left;margin-right: 6px;cursor:pointer;}" +
                    " .lightslderNSC img {display: block;height: auto;max-width: 100%;width:60%;}";
                html += "</style>";
            }
            html += "<div class='lightslderNSC " + obj["666"] + "'>";
            html += "<ul id='" + id + "'>";

            if (obj["668"] != null)
            {
                var slidesThumb = obj["668"];
                foreach (var slidesImg in slidesThumb)
                {
                    videoId = Guid.NewGuid().ToString().Replace("-", "_");
                    //if (slidesImg["110"] == 2)
                    //{
                    //    if (slidesImg["111"] == 1 || slidesImg["111"] == 2)
                    //    {
                    //        if (!string.IsNullOrEmpty(slidesImg["112"].ToString()))
                    //        {
                    //            var videoSrc = "";
                    //            if (slidesImg["112"].ToString().Contains("?"))
                    //            {
                    //                videoSrc = slidesImg["112"] + "&enablejsapi=1";
                    //            }
                    //            else
                    //            {
                    //                videoSrc = slidesImg["112"] + "?enablejsapi=1";
                    //            }
                    //            numberVideo = numberVideo + 1;
                    //            html += "<div data-p='112.50' style='display: none;'>" +
                    //                    "<iframe id='" + videoId + "' src='" + videoSrc + "' width='100%' height='377' frameborder='0'" +
                    //                    "style='z-index: 1; margin-top: 0px; margin-left: 0px; transform: translate3d(0px, 0px, 0px);'></iframe>" +
                    //                    "<div data-u='thumb'><img class='clickToStartPlay' data-u='image' style='width: 90px; height: 60px; margin-top: 1px; margin-left: 1px;' src='" + slidesImg["100"] + "' />" +
                    //                    "</div></div>";
                    //            html1 += " var player" + numberVideo + ";";
                    //            html2 += " player" + numberVideo + " = new YT.Player('" + videoId + "', {events: {'onStateChange': pauseYouTubeVideo}});";
                    //        }
                    //    }
                    //    if (slidesImg["111"] == 3)
                    //    {
                    //        html += "<div data-p='112.50' style='display: none;'>" +
                    //                "<video id='" + videoId + "' width='100%' height='377' controls><source src='" + slidesImg["112"] + "' type='video/mp4'></video>" +
                    //                "<div data-u='thumb'><img class='clickToStartPlay' data-u='image' style='width: 90px; height: 60px; margin-top: 1px; margin-left: 1px;' src='" + slidesImg["100"] + "' />" +
                    //                "</div></div>";
                    //    }
                    //}
                    //else
                    //{

                    var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(slidesImg["99"])).FirstOrDefault();
                    var altText = string.Empty;
                    if (upInfo != null)
                    {
                        altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                    }

                    html += "<li data-thumb='" + slidesImg["99"] + "'>";
                    html += "<img src='" + slidesImg["99"] + "' alt='" + altText + "'/>";
                    html += "</li>";    
                   // }
                }
            }
            html += "</ul>";
            html += "</div>";
            if (html1 != string.Empty && html2 != string.Empty)
            {
                html += "<script type='text/javascript' src='https://www.youtube.com/player_api'></script>";
            }
            html += "<script>$('#" + id + "').lightSlider({gallery:true,item:1,loop:true,slideMargin:0,thumbItem:4});";
            if (html1 != string.Empty && html2 != string.Empty)
            {
                html += html1;
                html += " function onYouTubePlayerAPIReady() {" + html2 + "};";
            }
            html += "</script>";
            sliderWithThumbLoaded = true;
            return html;
        }

        public string ParseButtonWithIcon(dynamic obj)
        {
            var divId = Guid.NewGuid().ToString().Replace("-", "_");
            var html = string.Empty;
            var height = "auto";
            if (obj["669"] != string.Empty && obj["669"] != null)
            {
                divId = obj["669"];
            }
            if (obj["684"] != string.Empty && obj["684"] != null)
            {
                height = obj["684"];
            }
            string buttonURL = obj["671"];          

            html += "<div style='padding-right:15px' class='fusion-imageframe imageframe-none " + obj["670"] + "'>";

            html += "<style> .cls" + divId + " {background-color: " + obj["675"] + "; color: " + obj["673"] + ";}";
            html += " .cls" + divId + ":hover {";
            if ((obj["712"] != string.Empty && obj["712"] != null))
            {
                html += "background-color:  " + obj["712"] + ";";
            }
            if ((obj["711"] != string.Empty && obj["711"] != null))
            {
                html += "color:  " + obj["711"] + ";";
            }
            html += "}";
            html += " </style>";
            html += "<div id='" + divId + "' class='cls" + divId + "' ";
            var urlOpenTargetAt = obj["1"];
            if (IsSpanishSite())
            {
                string buttonURLSpanish = obj["999"];
                if (string.IsNullOrEmpty(buttonURLSpanish))
                {
                    buttonURLSpanish = buttonURL;
                }
                if (buttonURLSpanish != string.Empty && buttonURLSpanish != null)
                {
                    html += " onclick =location.href='" + RelativeToAbsoluteUrl(buttonURLSpanish) + "'";
                    if (urlOpenTargetAt != string.Empty && urlOpenTargetAt != null)
                    {
                        if (urlOpenTargetAt == "_blank")
                        {
                            html += " onclick =window.open('" + buttonURLSpanish + "','_blank');";
                        }
                    }
                }
            }
            else
            {
                if (buttonURL != string.Empty && buttonURL != null)
                {
                    html += " onclick =location.href='" + RelativeToAbsoluteUrl(buttonURL) + "'";
                    if (urlOpenTargetAt != string.Empty && urlOpenTargetAt != null)
                    {
                        if (urlOpenTargetAt == "_blank")
                        {
                            html += " onclick =window.open('" + buttonURL + "','_blank');";
                        }
                    }
                }
            }
            html += " style='width: " + obj["676"] + "; height: " + height + ";";// background-color:" + obj["675"] + "; color:" + obj["673"] + ";";
            if (obj["680"] != string.Empty && obj["680"] != null)
            {
                html += " border: " + obj["680"] + " solid " + obj["681"] + ";"; 
            }
            if (obj["679"] != string.Empty && obj["679"] != null)
            {
                html += " -moz-border-radius: " + obj["679"] + "; -webkit-border-radius: " + obj["679"] + "; border-radius: " + obj["679"] + ";";
            }
            if (obj["693"] != string.Empty && obj["693"] != null)
            {
                html += " padding: " + obj["693"] + ";"; 
            }
            if (obj["674"] != string.Empty && obj["674"] != null)
            {
                html += " font-size: " + obj["674"] + ";";
            }
            html += " text-align: center; vertical-align: middle; display: table-cell; cursor: pointer;'>";

            var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(obj["683"])).FirstOrDefault();
            var altText = string.Empty;
            if (upInfo != null)
            {
                altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
            }

            if (obj["682"] != string.Empty && obj["682"] != null)
            {
                if (obj["677"] != string.Empty && obj["677"] != null)
                {
                    if (obj["677"] == "left")
                    {
                        html += "<i class='fa " + obj["682"] + " ' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></i><span class=' paddingleft10'>" + obj["672"] + "</span>";
                    }
                    if (obj["677"] == "right")
                    {
                        html += "<span class=' paddingright10'>" + obj["672"] + "</span><i class='fa " + obj["682"] + " ' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></i>";
                    }
                    if (obj["677"] == "top")
                    {
                        html += "<i class='fa " + obj["682"] + "  width100p' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></i><br/><span class=' width100p paddingtop10'>" + obj["672"] + "</span>";
                    }
                    if (obj["677"] == "bottom")
                    {
                        html += "<span class=' width100p'>" + obj["672"] + "</span><br/><i class='fa " + obj["682"] + "  width100p paddingtop10' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></i>";
                    }
                }
                else
                {
                    if (obj["672"] != string.Empty && obj["672"] != null)
                    {
                        html += "<i class='fa " + obj["682"] + " ' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></i><span class=' paddingleft10'>" + obj["672"] + "</span>";
                    }
                    else {
                        html += "<i class='fa " + obj["682"] + " ' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></i>";
                    }
                }
            }
            else if (obj["683"] != string.Empty && obj["683"] != null)
            {
                if (obj["677"] != string.Empty && obj["677"] != null)
                {
                    if (obj["677"] == "left")
                    {
                        html += "<img class='' src='" + obj["683"] + "' alt='" + altText + "' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></img><span class=' paddingleft10' style='height: " + obj["692"] + "; line-height: " + obj["692"] + ";'>" + obj["672"] + "</span>";
                    }
                    if (obj["677"] == "right")
                    {
                        html += "<span class=' paddingright10' style='height: " + obj["692"] + "; line-height: " + obj["692"] + ";'>" + obj["672"] + "</span><img class='' src='" + obj["683"] + "' alt='" + altText + "' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></img>";
                    }
                    if (obj["677"] == "top")
                    {
                        html += "<span class='width100p TextAlignCenter'><img src='" + obj["683"] + "' alt='" + altText + "' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></img></span><br/><span class=' width100p paddingtop10'>" + obj["672"] + "</span>";
                    }
                    if (obj["677"] == "bottom")
                    {
                        html += "<span class=' width100p'>" + obj["672"] + "</span><br/><span class='width100p TextAlignCenter'><img class='width100p paddingtop10' src='" + obj["683"] + "' alt='" + altText + "' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></img></span>";
                    }
                }
                else
                {
                    if (obj["672"] != string.Empty && obj["672"] != null)
                    {
                        html += "<img class='' src='" + obj["683"] + "' alt='" + altText + "' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></img><span class=' paddingleft10' style='height: " + obj["692"] + "; line-height: " + obj["692"] + ";'>" + obj["672"] + "</span>";
                }
                    else
                    {
                        html += "<img class='' src='" + obj["683"] + "' alt='" + altText + "' style='height: " + obj["692"] + "; width: " + obj["678"] + ";'></img>";
                    }
                }
            }
            else
            {
                html += obj["672"];
            }
            html += "</div></div>";
            return html;
        }

        public string ParsePageScroller(dynamic obj)
        {
            var backColor = "#ededed";
            var borderColor = "";
            var borderWidth = "";
            var mobileBackColor = "#000000";
            var border = "";
            if (obj["685"] != null && obj["685"] != string.Empty)
            {
                backColor = obj["685"];
            }
            if (obj["686"] != null && obj["686"] != string.Empty)
            {
                borderColor = obj["686"];
            }
            if (obj["687"] != null && obj["687"] != string.Empty)
            {
                borderWidth = obj["687"];
            }
            if (obj["688"] != null && obj["688"] != string.Empty)
            {
                mobileBackColor = obj["688"];
            }
            if (borderWidth != "")
            {
                border = borderWidth + " solid " + (borderColor == "" ? "#000000" : borderColor);
            }
            var divId = Guid.NewGuid().ToString().Replace("-", "_");
            var html = string.Empty;

            if (obj["690"] != null && obj["690"] != string.Empty)
            {
                divId = obj["690"];
            }

            html += "<div class='header' style='width: 100%; background-color: " + backColor + "; padding: 0; border: " + border + "; ' id='scrollerContainer" + divId  + "'>";
            html += "<div class='container' style='width: 100%; background-color: " + backColor + "; padding: 0;'>";
            html += "<nav class='navbar navbar-inverse " + obj["689"] + "' role='navigation'style='width: 100%; background-color: " + backColor + ";padding: 0;border:none;'>";
            html += "<div class='navbar-header' style='width: 100%; background-color: " + backColor + "; padding: 0;'>";
            html += "<button type='button' id='nav-toggle' class='navbar-toggle' data-toggle='collapse' data-target='#" + divId + "' style='float: left; background-color: " + mobileBackColor + ";'>";
            html += "<span class='sr-only'>Toggle navigation</span>";
            html += "<span class='icon-bar'></span>";
            html += "<span class='icon-bar'></span>";
            html += "<span class='icon-bar'></span>";
            html += "</button>";
            html += "</div>";
            html += " <div id='" + divId + "' class='collapse navbar-collapse' style='width: 100%; text-align:center; background-color: " + backColor + "; border: none !important; font-size: 12pt; padding: 0;'>";

            var links = obj["691"];
            if (links != null)
            {
                html += "<ul class='nav navbar-nav' style='display: inline-block; float: none!important;'>";
                foreach (var link in links)
                {
                    html += "<li style='text-align: center;'>";
                    var image = "";
                    if (link["101"] != null && link["101"] != string.Empty)
                    {
                        image = link["101"];
                    }
                    var imagePosition = "left";
                    if (link["102"] != null && link["102"] != string.Empty)
                    {
                        imagePosition = link["102"];
                    }
                    var linkText = "";
                    if (link["103"] != null && link["103"] != string.Empty)
                    {
                        linkText = link["103"];
                    }
                    var linkTextColor = "#000000";
                    if (link["104"] != null && link["104"] != string.Empty)
                    {
                        linkTextColor = link["104"];
                    }
                    var linkTarget = "";
                    if (link["105"] != null && link["105"] != string.Empty)
                    {
                        linkTarget = link["105"];
                    }
                    html += "<a href='#' class='scroll-link' data-id='" + linkTarget + "' style='color: " +
                            linkTextColor + ";'>";
                    if (image != "")
                    {
                        var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == image).FirstOrDefault();
                        var altText = string.Empty;
                        if (upInfo != null)
                        {
                            altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                        }
                        altText = link["175"] + ";" + altText;
                        if (imagePosition == "top")
                        {
                            html += "<img src='" + image + "' alt='" + altText + "' style='padding: 5px;'/><br/>" + linkText;
                        }
                        if (imagePosition == "left")
                        {
                            html += linkText + "<img src='" + image + "' alt='" + altText + "' style='padding: 5px;'/>";
                        }
                        if (imagePosition == "right")
                        {
                            html += "<img src='" + image + "' alt='" + altText + "' style='padding: 5px;'/>" + linkText;
                        }
                        if (imagePosition == "bottom")
                        {
                            html += linkText + "<br/><img src='" + image + "' alt='" + altText + "' style='padding: 5px;'/>";
                        }
                    }
                    else
                    {
                        html += linkText;
                    }
                    html += "</a>";
                    html += "</li>";
                }
                html += "</ul>";
            }
            html += "</div></nav></div>";
            html += "</div>";
            html += "<script type='text/javascript'>";
            html += "$(document).ready(function () {";
            html += "$('.scroll-link').on('click', function (event) {";
            html += "event.preventDefault();";
            html += "var sectionID = $(this).attr('data-id');";
            html += "scrollToID('#' + sectionID, 750);";
            html += "$('#nav-toggle').on('click', function (event) {";
            html += "event.preventDefault();";
            html += "$('#" + divId + "').toggleClass('open');";
            html += "});";
            html += "});";
            html += "function scrollToID(id, speed) {";
            html += "var offSet = 50;";
            html += "var targetOffset = $(id).offset().top - offSet;";
            html += "var mainNav = $('#" + divId + "');";
            html += "$('html,body').animate({ scrollTop: targetOffset }, speed);";
            html += "if (mainNav.hasClass('open')) {";
            html += "mainNav.css('height', '1px').removeClass('in').addClass('collapse');";
            html += "mainNav.removeClass('open');";
            html += "}} });";
            html += "</script>";

            return html;
        }

        public string ParseAPN(dynamic obj)
        {
            var showToggle = obj["2"];
            var html = "";

            if (showToggle != null && showToggle == "yes")
            {
                html += "<div class='accordian fusion-accordian'>";
                var accordionId = "accordion-" + Guid.NewGuid().ToString().Replace("-", "_");
                html += "<div class='panel-group' id='" + accordionId + "'>";

                html += "<div class='fusion-panel panel-default'>";
                html += "<div class='panel-heading'>";
                var id = Guid.NewGuid().ToString().Replace("-", "_");
                var status = "collapsed";
                var st = "";
                if (obj["4"] == "yes")
                {
                    status = "active";
                    st = "in";
                }
                html +=
                    "<h1 class='panel-title toggle' data-fontsize='14' data-lineheight='20'><a data-toggle='collapse' data-parent='#" +
                    accordionId + "' data-target='#" + id + "' href='#" + id + "' class='" + status +
                    "' onclick=cms_toggleBoxClicked(this)>";
                html += "<div class='fusion-toggle-icon-wrapper' style='padding-top:10px;'>";
                html += "<i class='fa-fusion-box'></i>";
                html += "</div>";
                html += "<div class='fusion-toggle-heading' style='font-size:32px;color:#3888cc;'>" + obj["3"] + "</div>";
                html += "</a></h1>";
                html += "</div>";
                html += "<div id='" + id + "' class='panel-collapse collapse " + st + "' style='height: auto;'>";
                html += "<div class='panel-body toggle-content'>";

            }

            html += @"<div style='background-color:" + obj["695"] + ";'>";
            var pns = obj["700"];
            var optionRow = obj["1"];
            var pns2 = obj["701"];
            html += @"<table class='" + obj["698"] + "' style='width:100%;' id='" + obj["699"] + "'>";
            html += @"<tr style='width:100%'>";
            html += @"<td style='width:10%;font-weight:bold;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] + ";font-size: 12pt;'>Part Number</td>";
            html += @"<td style='width:25%;font-weight:bold;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] + ";font-size: 12pt;'>Model</td>";
            html += @"<td style='width:65%;font-weight:bold;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] + ";font-size: 12pt;'>Description</td>";
            html += @"</tr>";
            if (pns != null)
            {
                foreach (var pn in pns)
                {
                    var pd = new ProductDetails();
                    pd = null;
                    var pnd = _productDetails.GetAll().Where(x => x.BindingPart == Convert.ToString(pn) && x.UWebsite.ToLower() != "exclude").ToList();
                    if (pnd.Count == 1)
                    {
                        pd = pnd[0];
                    }
                    else if(pnd.Count>1)
                    {
                        var t = pnd.ToList().FirstOrDefault(x => x.UWebsite.ToLower() == "include");
                        if (t == null)
                        {
                            var g =
                                pnd.ToList()
                                    .Where(
                                        x =>
                                            x.UWebsite.ToLower() == "exclude" || x.UWebsite.ToLower() == "default" ||
                                            x.UWebsite.ToLower() == "");
                            if (!g.Any())
                            {
                                pd.ItemName = "Error. No valid P/N found.";
                            }
                            else
                            {
                                pd = g.OrderByDescending(x => DateTime.Parse(x.CreateDate)).FirstOrDefault();
                            }
                        }
                        else
                        {
                            pd = t;
                        }
                    }
                    if (pd == null)
                    {
                        //pd = new ProductDetails
                        //{
                        //    ItemCode = "Error",
                        //    ItemName = "Error",
                        //    SalesDescription = "No valid P/N found"
                        //};
                    }
                    else
                    {
                        if (pd.ItemName != null)
                        {
                            if (pd.ItemName.ToLower().Contains("nla cbs"))
                            {
                                pd.ItemName = pd.ItemName.Substring(19, pd.ItemName.Length - 19);
                            }
                            else if (pd.ItemName.ToLower().Contains("nla"))
                            {
                                pd.ItemName = pd.ItemName.Replace("NLA ", "");
                            }
                            else if (pd.ItemName.ToLower().Contains("cbs"))
                            {
                                pd.ItemName = pd.ItemName.Replace("CBS #", "");
                            }
                        }
                        else
                        {
                            pd.ItemName = string.Empty;
                        }
                    }
                    if (pd != null)
                    {
                        html += @"<tr style='width:100%'>";
                        html += @"<td style='width:10%;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] +
                                ";'>" + pd.ItemCode + "</td>";
                        html += @"<td style='width:25%;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] +
                                ";'>" + pd.ItemName + "</td>";
                        html += @"<td style='width:65%;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] +
                                ";'>" + pd.SalesDescription + "</td>";
                        html += @"</tr>";
                    }

                }
                
            }
            if (optionRow != null && optionRow == "yes")
            {
                html += @"<tr style='width:100%'>";
                html += @"<td style='width:20%;font-weight:bold;padding:8px;text-align:left;border: " + obj["697"] + " solid " + obj["696"] + ";font-weight:bold;' colspan='3'>Options</td>";
                html += @"</tr>";

                if (pns2 != null)
                {
                    foreach (var pn in pns2)
                    {
                        var pd = new ProductDetails();
                        pd = null;
                        var pnd = _productDetails.GetAll().Where(x => x.BindingPart == Convert.ToString(pn) && x.UWebsite.ToLower() != "exclude").ToList();

                        if (pnd.Count == 1)
                        {
                            pd = pnd[0];
                        }
                        else if (pnd.Count > 1)
                        {
                            var t = pnd.ToList().FirstOrDefault(x => x.UWebsite.ToLower() == "include");
                            if (t == null)
                            {
                                var g =
                                    pnd.ToList()
                                        .Where(
                                            x =>
                                                x.UWebsite.ToLower() == "exclude" || x.UWebsite.ToLower() == "default" ||
                                                x.UWebsite.ToLower() == "");
                                if (!g.Any())
                                {
                                    pd.ItemName = "Error. No valid P/N found.";
                                }
                                else
                                {
                                    pd = g.OrderByDescending(x => DateTime.Parse(x.CreateDate)).FirstOrDefault();
                                }
                            }
                            else
                            {
                                pd = t;
                            }
                        }
                        if (pd == null)
                        {
                            //pd = new ProductDetails
                            //{
                            //    ItemCode = "Error",
                            //    ItemName = "Error",
                            //    SalesDescription = "No valid P/N found"
                            //};
                        }
                        else
                        {
                            if (pd.ItemName != null)
                            {
                                if (pd.ItemName.ToLower().Contains("nla cbs"))
                                {
                                    pd.ItemName = pd.ItemName.Substring(19, pd.ItemName.Length - 19);
                                }
                                else if (pd.ItemName.ToLower().Contains("nla"))
                                {
                                    pd.ItemName = pd.ItemName.Replace("NLA ", "");
                                }
                                else if (pd.ItemName.ToLower().Contains("cbs"))
                                {
                                    pd.ItemName = pd.ItemName.Replace("CBS #", "");
                                }
                            }
                            else
                            {
                                pd.ItemName = string.Empty;
                            }
                        }



                        if (pd != null)
                        {
                            html += @"<tr style='width:100%'>";
                            html += @"<td style='width:10%;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] +
                                    ";'>" + pd.ItemCode + "</td>";
                            html += @"<td style='width:25%;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] +
                                    ";'>" + pd.ItemName + "</td>";
                            html += @"<td style='width:65%;padding:8px;border-bottom: " + obj["697"] + " solid " + obj["696"] +
                                    ";'>" + pd.SalesDescription + "</td>";
                            html += @"</tr>";
                        }
                    }

                }

            }
            html += @"</table>";
            html += @"</div>";

            if (showToggle != null && showToggle == "yes")
            {
                html += "</div>";
                html += "</div>";

                html += @"</div>";
                html += @"</div>";
            }
            return html;
        }

        public string ParseImageLibrary(dynamic obj)
        {
            var html = string.Empty;
            var borderColor = "";
            var borderWidth = "";
            var border =  "";
            if (obj["703"] != null && obj["703"] != string.Empty)
            {
                if (obj["710"] != null && obj["710"] != string.Empty)
                {
                    html += "<div><h1 style='text-align:" + obj["710"] +";'><span style='font-size: 32px; color: #3888cc;'>" + obj["703"] + "</span></h1></div>";
                }
                else
                {
                    html += "<div><h1><span style='font-size: 32px; color: #3888cc;'>" + obj["703"] + "</span></h1></div>";
                }
            }
            var id = Guid.NewGuid().ToString().Replace("-", "_");
            if (obj["709"] != null && obj["709"] != string.Empty)
            {
                id = obj["709"];
            }
            var columnsWidth = "100%";
            if (obj["705"] != string.Empty && obj["705"] != null)
            {
                columnsWidth = "width: " + (100 / Int32.Parse(obj["705"].ToString())) + '%';
            }
            if (obj["707"] != null && obj["707"] != string.Empty)
            {
                borderColor = obj["707"];
            }
            if (obj["706"] != string.Empty && obj["706"] != null)
            {
                if (!obj["706"].ToString().Contains("px"))
                {
                    borderWidth = obj["706"] + "px";
                }
                else
                {
                    borderWidth = obj["706"];
                }
            }
            if (borderWidth != "")
            {
                border = borderWidth + " solid " + (borderColor == "" ? "#000000" : borderColor);
            }
            if (obj["702"] != null)
            {
                html += "<div class='" + obj["708"] + "' id='" + id + "' ";
                html += "style='float: left; padding-bottom: 10px; width: 100%; text-align:" + obj["704"] + "; border:" + border + ";' >";
                var slides = obj["702"];
                int numberOfImage = 0;
                foreach (var item in slides)
                {
                    html += "<div style='float: left; margin: 0 auto;" + columnsWidth + ";'>";
                    string s = item["106"];
                    //if (!string.IsNullOrEmpty(s) && !s.Contains("http"))
                    //{
                    //    s = "http://" + s;
                    //}
                    if (item["106"] != null && item["106"] != string.Empty)
                    {
                        html += "<a href='" + RelativeToAbsoluteUrl(s) + "' target='" + item["107"] + "'>";
                    }

                    var upInfo =
                           _uploadInformationRepository.GetAll().Where(x => x.FilePath == Convert.ToString(item["108"])).FirstOrDefault();
                    var altText = string.Empty;
                    if (upInfo != null)
                    {
                        altText = !string.IsNullOrEmpty(upInfo.AltText) ? upInfo.AltText : string.Empty;
                    }

                    html += "<img src='" + item["108"] + "' alt='" + item["109"] + ";" + altText + "' data-u='image' alt='" + item["109"] + "' />";
                    if (item["106"] != null && item["106"] != string.Empty)
                    {
                        html += "</a>";
                    }
                    html += "</div>";
                    numberOfImage = numberOfImage + 1;
                    if (numberOfImage == Int32.Parse(obj["705"].ToString()) && slides.Count > Int32.Parse(obj["705"].ToString()))
                    {
                        numberOfImage = 0;
                        html += "</div>";
                        html += "<div class='" + obj["708"] + "' id='" + id + "' ";
                        html += "style='float: left; padding-bottom: 10px; width: 100%; text-align:" + obj["704"] + "; border: " + border + ";' >";
                    }
                }
                html += "</div>";
            }
            return html;
        }

        public string ParseBackToTop(dynamic obj)
        {
            var target = "body";
            if (obj["694"] != null && obj["694"] != string.Empty)
            {
                target = obj["694"];
            }
            var html = string.Empty;
            var htm = "<a href='#" + target + "' class='back-to-top'></a>";
            html += "<style>";
            html +=
                "a.back-to-top {display: none;width: 120px;height: 30px;text-indent: -9999px;position: fixed;z-index: 999;left: 20px;bottom: 50px;background: transparent url('/Images/BackToTop2.png') no-repeat center;-webkit-border-radius: 4px;-moz-border-radius: 4px;border-radius: 4px;}";
            html += "</style>";
            
            html += "<script type='text/javascript'>";
            html += "$('body').prepend(\"" + htm + "\");";
            html += "var amountScrolled = 300;";
            html += "        $(window).scroll(function () {";
            html += "if ($(window).scrollTop() > amountScrolled) {";
            html += "$('a.back-to-top').fadeIn('slow');";
            html += "} else {";
            html += "$('a.back-to-top').fadeOut('slow');";
            html += "}";
            html += "});";
            html += "</script>";

            return html;
        }
        
        private string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            if(baseUrl.Substring(baseUrl.Length-1,1) == "/") 
                baseUrl = baseUrl.Remove(baseUrl.Length - 1);

            return baseUrl;
        }

        private bool IsSpanishSite()
        {
            var request = HttpContext.Current.Request;
            return request.UserHostAddress == "205.216.24.23";
        }

        private string RelativeToAbsoluteUrl(object url)
        {
            string outPut = Convert.ToString(url);
            if (String.IsNullOrEmpty(outPut))
            {
                return outPut;
            }
            if (outPut == "#")
            {
                return outPut;
            }
            if (!Uri.IsWellFormedUriString(outPut, UriKind.Absolute))
            {
                if (!outPut.Contains(GetBaseUrl()))
                {
                    outPut = outPut.StartsWith("/") ? (GetBaseUrl() + outPut) : (GetBaseUrl() + "/" + outPut);
                }
            }
            return outPut;
        }
    }
}