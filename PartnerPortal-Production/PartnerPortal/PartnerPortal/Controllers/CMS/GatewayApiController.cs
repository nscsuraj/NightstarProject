using PartnerPortal.Domain.Gateway;
using PartnerPortal.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using PartnerPortal.Core.Enumerations;
using PartnerPortal.Domain.SiteUtility;

namespace PartnerPortal.Controllers.CMS
{
    [RoutePrefix("api/gatewayapi")]
    public class GatewayApiController : BaseApiController
    {
        //
        // GET: /Editor/

      //  private readonly IEFRepository<FeaturedStarProduct> _fspRepository;
       // private readonly IEFRepository<FeaturedStarProductDetails> _fspDetailRepository;
       // private readonly IEFRepository<Slider> _sliderRepository;
     //   private readonly IEFRepository<SliderTopic> _sliderTopicRepository;
        //private readonly IEFRepository<News> _newsRepository;
        //private readonly IEFRepository<Videos> _videoRepository;
      //  private readonly IEFRepository<SEOLinks> _seoLinksRepository;
        //private readonly IEFRepository<SiteFooter> _siteFooter;
        //private readonly IEFRepository<ServingSolutions> _servingSolutions;
        //private readonly IEFRepository<OurIntegrations> _ourIntegrations;

        public GatewayApiController(
            //IEFRepository<Slider> sliderRepository,
            //IEFRepository<SliderTopic> sliderTopic,
            //IEFRepository<News> news, IEFRepository<Videos> video,
            //IEFRepository<FeaturedStarProduct> fspRepo,
            //IEFRepository<FeaturedStarProductDetails> fspdRepo, IEFRepository<SEOLinks> seoLinks,
           // IEFRepository<SiteFooter> siteFooter,
            //IEFRepository<ServingSolutions> servingSolutions,
            //IEFRepository<OurIntegrations> ourIntegrations
            )
        {
           // _sliderRepository = sliderRepository;
            //_sliderTopicRepository = sliderTopic;
            //_newsRepository = news;
            //_videoRepository = video;
            //_fspRepository = fspRepo;
            //_fspDetailRepository = fspdRepo;
            //_seoLinksRepository = seoLinks;
            //_siteFooter = siteFooter;
            //_servingSolutions = servingSolutions;
            //_ourIntegrations = ourIntegrations;
        }

        //[Route("SaveSliderInfo")]
        //[HttpPost]
        //public object SaveSliderInfo(dynamic sliders)
        //{
        //    var order = 1;
        //    foreach (var slide in sliders)
        //    {
        //        if (slide.Id > 0)
        //        {
        //            //update
        //            int id = slide.Id;
        //            var s = _sliderRepository.GetById(id);
        //            s.ImagePath = slide.ImagePath;
        //            s.MobileImagePath = slide.MobileImagePath;
        //            s.MobileLandscapeImagePath = slide.MobileLandscapeImagePath;
        //            s.ImgAltText = slide.ImgAltText;
        //            s.Title = slide.Title;
        //            s.SubTitle = slide.SubTitle;
        //            s.ButtonText = slide.ButtonText;
        //            s.ButtonLink = slide.ButtonLink;
        //            s.Type = (int) SliderTypes.GatewaySlider;
        //            s.OrderBy = order;
        //            _sliderRepository.Update(s);
        //            _sliderTopicRepository.Delete(x=> x.SliderId == id);
        //            foreach (var slideTopic in slide.SliderTopics)
        //            {
        //                _sliderTopicRepository.Add(new SliderTopic { Topic = slideTopic.Topic, TopicLink = slideTopic.TopicLink, SliderId = s.Id });
        //            }

        //        }
        //        else
        //        {
        //            //Add
        //            var s = new Slider
        //            {
        //                ImagePath = slide.ImagePath,
        //                MobileImagePath = slide.MobileImagePath,
        //                MobileLandscapeImagePath = slide.MobileLandscapeImagePath,
        //                Title = slide.Title,
        //                ImgAltText = slide.ImgAltText,
        //                SubTitle = slide.SubTitle,
        //                ButtonText = slide.ButtonText,
        //                ButtonLink = slide.ButtonLink,
        //                Type = (int) SliderTypes.GatewaySlider,
        //                OrderBy = order
        //            };
        //            _sliderRepository.Add(s);
        //            foreach (var slideTopic in slide.SliderTopics)
        //            {
        //                _sliderTopicRepository.Add(new SliderTopic{Topic = slideTopic.Topic, TopicLink = slideTopic.TopicLink, SliderId = s.Id});
        //            }
        //        }
        //        order = order + 1;
        //    }
        //    // RETURN A MESSAGE.
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewaySlider");
        //    return SliderList();
        //}

        //[Route("SliderUpdateOrderBy")]
        //[HttpPost]
        //public object SliderUpdateOrderBy(dynamic sliders)
        //{
        //    var order = 1;
        //    foreach (var slide in sliders)
        //    {
        //        if (slide.Id > 0)
        //        {
        //            //update
        //            int id = slide.Id;
        //            var s = _sliderRepository.GetById(id);
        //            s.OrderBy = order;
        //            _sliderRepository.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewaySlider");
        //    // RETURN A MESSAGE.
        //    return NewsList();
        //}

        //private object SliderList()
        //{
        //    return _sliderRepository.GetMany(y => y.Type == (int)SliderTypes.GatewaySlider).OrderBy(c => c.OrderBy).ToList()
        //        .Select(x => new
        //        {
        //            Id = x.Id,
        //            ImagePath = x.ImagePath,
        //            MobileImagePath = x.MobileImagePath,
        //            MobileLandscapeImagePath = x.MobileLandscapeImagePath,
        //            ImgAltText = x.ImgAltText,
        //            Title = x.Title, SubTitle = x.SubTitle, ButtonText = x.ButtonText, ButtonLink = x.ButtonLink, SliderTopics = _sliderTopicRepository.GetMany(c=> c.SliderId == x.Id).ToList().Select(m=> new {Id = m.Id, SliderId = m.SliderId, Topic = m.Topic, TopicLink = m.TopicLink }).ToList()}).ToList();
        //}

        //[Route("GetSliders")]
        //[HttpGet]
        //public dynamic GetSliders()
        //{
        //    return SliderList();
        //}

        //[Route("GetSliderInfo/{id:int}")]
        //[HttpGet]
        //public dynamic GetSliderInfo(int id)
        //{
        //    var slider = _sliderRepository.GetById(id);
        //    var sliderTopics = new List<SliderTopic>();
        //    if (slider != null)
        //    {
        //        sliderTopics = _sliderTopicRepository.GetMany(x => x.SliderId == slider.Id).ToList();
        //    }
        //    return new
        //    {
        //        Slider = slider,
        //        SliderTopics = sliderTopics
        //    };
        //}

        //[Route("DeleteSliderById")]
        //[HttpPost]
        //public object DeleteSliderById(dynamic slide)
        //{
        //    int id = slide.Id;
        //    _sliderTopicRepository.Delete(x => x.SliderId == id);
        //    _sliderRepository.Delete(x=> x.Id == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewaySlider");
        //    return SliderList();
        //}

        //[Route("SaveNewsInfo")]
        //[HttpPost]
        //public object SaveNewsInfo(dynamic newsList)
        //{
        //    var order = 1;
        //    foreach (var news in newsList)
        //    {
        //        if (news.Id > 0)
        //        {
        //            //update
        //            int id = news.Id;
        //            var s = _newsRepository.GetById(id);
        //            s.NewsThumbnailPath = news.NewsThumbnailPath;
        //            s.NewsTitle = news.NewsTitle;
        //            s.ImgAltText = news.ImgAltText;
        //            s.NewsContent = news.NewsContent;
        //            s.HeaderVideoOrImage = news.HeaderVideoOrImage;
        //            s.FooterVideoOrImage = news.FooterVideoOrImage;
        //            s.HeaderVideoType = news.HeaderVideoType;
        //            s.FooterVideoType = news.FooterVideoType;
        //            s.HeaderVideoPath = news.HeaderVideoPath;
        //            s.FooterVideoPath = news.FooterVideoPath;
        //            s.OrderBy = order;
        //            _newsRepository.Update(s);
        //        }
        //        else
        //        {
        //            //Add
        //            var s = new News
        //            {
        //                NewsThumbnailPath = news.NewsThumbnailPath,
        //                NewsTitle = news.NewsTitle,
        //                ImgAltText = news.ImgAltText,
        //                NewsType = (int)NewsTypes.LatestStarNews,
        //                NewsContent = news.NewsContent,
        //                HeaderVideoOrImage = news.HeaderVideoOrImage,
        //                FooterVideoOrImage = news.FooterVideoOrImage,
        //                HeaderVideoType = news.HeaderVideoType,
        //                FooterVideoType = news.FooterVideoType,
        //                HeaderVideoPath = news.HeaderVideoPath,
        //                FooterVideoPath = news.FooterVideoPath,
        //                OrderBy = order
        //            };

        //            _newsRepository.Add(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayNews");
        //    // RETURN A MESSAGE.
        //    return NewsList();
        //}

        //[Route("NewsUpdateOrderBy")]
        //[HttpPost]
        //public object NewsUpdateOrderBy(dynamic newsList)
        //{
        //    var order = 1;
        //    foreach (var news in newsList)
        //    {
        //        if (news.Id > 0)
        //        {
        //            //update
        //            int id = news.Id;
        //            var s = _newsRepository.GetById(id);
        //            s.OrderBy = order;
        //            _newsRepository.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayNews");
        //    // RETURN A MESSAGE.
        //    return NewsList();
        //}

        //[Route("GetNewsList")]
        //[HttpGet]
        //public dynamic GetNewsList()
        //{
        //    return NewsList();
        //}

        //private object NewsList()
        //{
        //    return
        //        _newsRepository.GetMany(y => y.NewsType == (int) NewsTypes.LatestStarNews)
        //        .OrderBy(c => c.OrderBy).Select(m => new
        //        {
        //            Id = m.Id,
        //            NewsThumbnailPath = m.NewsThumbnailPath,
        //            NewsTitle = m.NewsTitle,
        //            ImgAltText = m.ImgAltText,
        //            NewsType = m.NewsType, 
        //            NewsContent = m.NewsContent, 
        //            HeaderVideoOrImage = m.HeaderVideoOrImage.HasValue ? m.HeaderVideoOrImage.Value.ToString() : "0", 
        //            FooterVideoOrImage = m.FooterVideoOrImage.HasValue?m.FooterVideoOrImage.Value.ToString():"0",
        //            HeaderVideoType = m.HeaderVideoType.HasValue? m.HeaderVideoType.Value.ToString() : "-1",
        //            FooterVideoType = m.FooterVideoType.HasValue? m.FooterVideoType.Value.ToString(): "-1",
        //            HeaderVideoPath = m.HeaderVideoPath,
        //            FooterVideoPath = m.FooterVideoPath,
        //            OrderBy = m.OrderBy
        //        }).ToList();
        //}

        //[Route("GetNewsInfo/{id:int}")]
        //[HttpGet]
        //public dynamic GetNewsInfo(int id)
        //{
        //    return _newsRepository.GetById(id);
        //}

        //[Route("DeleteNewsById")]
        //[HttpPost]
        //public object DeleteNewsById(dynamic news)
        //{
        //    int id = news.Id;
        //    _newsRepository.Delete(x => x.Id == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayNews");
        //    return NewsList();
        //}

        //[Route("SaveVideoInfo")]
        //[HttpPost]
        //public object SaveVideoInfo(dynamic videoList)
        //{
        //    var order = 1;
        //    foreach (var video in videoList)
        //    {
        //        if (video.Id > 0)
        //        {
        //            //update
        //            int id = video.Id;
        //            var s = _videoRepository.GetById(id);
        //            s.VideoThumbnailPath = video.VideoThumbnailPath;
        //            s.VideoTitle = video.VideoTitle;
        //            s.VideoDescription = video.VideoDescription;
        //            s.VideoType = video.VideoType;
        //            s.VideoCategory = video.VideoCategory;
        //            s.VideoPath = video.VideoPath;
        //            s.OrderBy = order;
        //            _videoRepository.Update(s);
        //        }
        //        else
        //        {
        //            //Add
        //            var s = new Videos
        //            {
        //                VideoThumbnailPath = video.VideoThumbnailPath,
        //                VideoTitle = video.VideoTitle,
        //                VideoDescription = video.VideoDescription,
        //                VideoType = video.VideoType,
        //                VideoCategory = (int)VideoCategories.LatestStarVideo,
        //                VideoPath = video.VideoPath,
        //                OrderBy = order
        //            };

        //            _videoRepository.Add(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayNews");
        //    // RETURN A MESSAGE.
        //    return VideoList();
        //}

        //[Route("VideoUpdateOrderBy")]
        //[HttpPost]
        //public object VideoUpdateOrderBy(dynamic videoList)
        //{
        //    var order = 1;
        //    foreach (var video in videoList)
        //    {
        //        if (video.Id > 0)
        //        {
        //            //update
        //            int id = video.Id;
        //            var s = _videoRepository.GetById(id);
        //            s.OrderBy = order;
        //            _videoRepository.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayNews");
        //    // RETURN A MESSAGE.
        //    return VideoList();
        //}

        //[Route("GetVideoList")]
        //[HttpGet]
        //public dynamic GetVideoList()
        //{
        //    return VideoList();
        //}

        //private object VideoList()
        //{
        //    return
        //        _videoRepository.GetMany(y => y.VideoCategory == (int)VideoCategories.LatestStarVideo)
        //        .OrderBy(c => c.OrderBy).Select(m => new
        //        {
        //            Id = m.Id,
        //            VideoThumbnailPath = m.VideoThumbnailPath,
        //            VideoTitle = m.VideoTitle,
        //            VideoCategory = m.VideoCategory,
        //            VideoDescription = m.VideoDescription,
        //            VideoType = m.VideoType.HasValue ? m.VideoType.Value.ToString() : "-1",
        //            VideoPath = m.VideoPath,
        //            OrderBy = m.OrderBy
        //        }).ToList();
        //}

        //[Route("DeleteVideoById")]
        //[HttpPost]
        //public object DeleteVideoById(dynamic video)
        //{
        //    int id = video.Id;
        //    _videoRepository.Delete(x => x.Id == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayNews");
        //    return VideoList();
        //}


        //[Route("GetFSPList")]
        //[HttpGet]
        //public dynamic GetFSPList()
        //{
        //    return FSPList();
        //}

        //private object FSPList()
        //{
        //    return
        //        _fspRepository.GetMany(x => x.IsActive).OrderBy(m=> m.OrderBy).ToList();
        //}

        //[Route("SaveFSP")]
        //[HttpPost]
        //public object SaveFSP(dynamic fsp)
        //{
        //    var order = _fspRepository.GetMany(x => x.IsActive).OrderByDescending(m => m.OrderBy).FirstOrDefault() !=
        //                null
        //        ? _fspRepository.GetMany(x => x.IsActive).OrderByDescending(m => m.OrderBy).FirstOrDefault().OrderBy + 1
        //        : 1;
        //    if (fsp.Id > 0)
        //    {
        //        //update
        //        int id = fsp.Id;
        //        var s = _fspRepository.GetById(id);
        //        s.FSPTitle = fsp.FSPTitle;
        //        s.IsActive = true;
        //        _fspRepository.Update(s);
        //    }
        //    else
        //    {
        //        //Add
        //        var s = new FeaturedStarProduct
        //        {
        //            FSPTitle = fsp.FSPTitle,
        //            IsActive = true,
        //            OrderBy = order
        //        };

        //        _fspRepository.Add(s);
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayMiddleMenu");
        //    // RETURN A MESSAGE.
        //    return FSPList();
        //}

        //[Route("DeleteFSPById")]
        //[HttpPost]
        //public object DeleteFSPById(dynamic fsp)
        //{
        //    int id = fsp.Id;
        //    _fspRepository.Delete(x => x.Id == id);
        //    _fspDetailRepository.Delete(x => x.FSPDId == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayMiddleMenu");
        //    return FSPList();
        //}

        //[Route("FSPUpdateOrderBy")]
        //[HttpPost]
        //public object FSPUpdateOrderBy(dynamic fspList)
        //{
        //    var order = 1;
        //    foreach (var video in fspList)
        //    {
        //        if (video.Id > 0)
        //        {
        //            //update
        //            int id = video.Id;
        //            var s = _fspRepository.GetById(id);
        //            s.OrderBy = order;
        //            _fspRepository.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    // RETURN A MESSAGE.
        //    return FSPList();
        //}

        ////FSPD Detail
        //[Route("SaveSliderInfo")]
        //[HttpPost]
        //public object SaveFSPD(dynamic fspd)
        //{
        //    var order = 1;
        //    if (fspd.Id > 0)
        //        {
        //            //update
        //            int id = fspd.Id;
        //            var s = _fspDetailRepository.GetById(id);
        //            s.BackgroundImage = fspd.BackgroundImage;
        //            s.FSPDId = fspd.FSPDId;
        //            s.Title = fspd.Title;
        //            s.Description = fspd.Description;
        //            s.ButtonText = fspd.ButtonText;
        //            s.ButtonLink = fspd.ButtonLink;
        //            _fspDetailRepository.Update(s);
        //        }
        //        else
        //        {
        //            //Add
        //            var s = new FeaturedStarProductDetails
        //            {
        //                BackgroundImage = fspd.BackgroundImage,
        //                FSPDId = fspd.FSPDId,
        //                Title = fspd.Title,
        //                Description = fspd.Description,
        //                ButtonText = fspd.ButtonText,
        //                ButtonLink = fspd.ButtonLink,
        //                OrderBy = order
        //            };
        //            _fspDetailRepository.Add(s);
        //        }
        //        order = order + 1;
        //        HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayMiddleMenu");
        //    // RETURN A MESSAGE.
        //    return FSPDList();
        //}

        //[Route("FSPDUpdateOrderBy")]
        //[HttpPost]
        //public object FSPDUpdateOrderBy(dynamic fspdList)
        //{
        //    var order = 1;
        //    foreach (var fspd in fspdList)
        //    {
        //        if (fspd.Id > 0)
        //        {
        //            //update
        //            int id = fspd.Id;
        //            var s = _fspDetailRepository.GetById(id);
        //            s.OrderBy = order;
        //            _fspDetailRepository.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayMiddleMenu");
        //    // RETURN A MESSAGE.
        //    return FSPDList();
        //}

        //private int fspId = 0;
        //private object FSPDList()
        //{
        //    return _fspDetailRepository.GetMany(x=> x.FSPDId == fspId).OrderBy(c => c.OrderBy).ToList();
        //}

        //[Route("GetFSPD/{id:int}")]
        //[HttpGet]
        //public dynamic GetFSPD(int id)
        //{
        //    fspId = id;
        //    return FSPDList();
        //}

        //[Route("GetFSPInfo/{id:int}")]
        //[HttpGet]
        //public dynamic GetFSPInfo(int id)
        //{
        //    var fsp = _fspRepository.GetById(id);
        //    var fspd = new List<FeaturedStarProductDetails>();
        //    if (fsp != null)
        //    {
        //        fspd = _fspDetailRepository.GetMany(x => x.FSPDId == fsp.Id).ToList();
        //    }
        //    return new
        //    {
        //        FSP = fsp,
        //        FSPD = fspd
        //    };
        //}

        //[Route("DeleteSliderById")]
        //[HttpPost]
        //public object DeleteFSPDById(dynamic fspd)
        //{
        //    int id = fspd.Id;
        //    _fspDetailRepository.Delete(x => x.Id == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayMiddleMenu");
        //    return FSPDList();
        //}

        //[Route("SaveSEOLinkInfo")]
        //[HttpPost]
        //public object SaveSEOLinkInfo(dynamic seoLinkList)
        //{
        //    var order = 1;
        //    foreach (var seoLink in seoLinkList)
        //    {
        //        if (seoLink.Id > 0)
        //        {
        //            //update
        //            int id = seoLink.Id;
        //            var s = _seoLinksRepository.GetById(id);
        //            s.LinkTitle = seoLink.LinkTitle;
        //            s.SEOImage = seoLink.SEOImage;
        //            s.SEOLink = seoLink.SEOLink;
        //            s.HoverImage = seoLink.HoverImage;
        //            s.OrderBy = order;
        //            _seoLinksRepository.Update(s);
        //        }
        //        else
        //        {
        //            //Add
        //            var s = new SEOLinks
        //            {
        //                LinkTitle = seoLink.LinkTitle,
        //                SEOImage = seoLink.SEOImage,
        //                SEOLink = seoLink.SEOLink,
        //                HoverImage = seoLink.HoverImage,
        //                OrderBy = order
        //            };

        //            _seoLinksRepository.Add(s);
        //        }
        //        order = order + 1;
        //    }
        //    // RETURN A MESSAGE.
        //    return SEOLinkList();
        //}

        //[Route("GetSEOLinkList")]
        //[HttpGet]
        //public dynamic GetSEOLinkList()
        //{
        //    return SEOLinkList();
        //}

        //private object SEOLinkList()
        //{
        //    return
        //        _seoLinksRepository.GetAll()
        //        .OrderBy(c => c.OrderBy).Select(m => new
        //        {
        //            Id = m.Id,
        //            LinkTitle = m.LinkTitle,
        //            SEOImage = m.SEOImage,
        //            SEOLink = m.SEOLink,
        //            HoverImage = m.HoverImage,
        //            OrderBy = m.OrderBy
        //        }).ToList();
        //}

        //[Route("SEOLinkUpdateOrderBy")]
        //[HttpPost]
        //public object SEOLinkUpdateOrderBy(dynamic seoLinkList)
        //{
        //    var order = 1;
        //    foreach (var seoLink in seoLinkList)
        //    {
        //        if (seoLink.Id > 0)
        //        {
        //            //update
        //            int id = seoLink.Id;
        //            var s = _seoLinksRepository.GetById(id);
        //            s.OrderBy = order;
        //            _seoLinksRepository.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    // RETURN A MESSAGE.
        //    return SEOLinkList();
        //}

        //[Route("DeleteSEOLinkById")]
        //[HttpPost]
        //public object DeleteSEOLinkById(dynamic seoLink)
        //{
        //    int id = seoLink.Id;
        //    _seoLinksRepository.Delete(x => x.Id == id);
        //    return SEOLinkList();
        //}


        //[Route("GetFooter")]
        //[HttpGet]
        //public dynamic GetFooter()
        //{
        //    var footer = _siteFooter.GetAll().FirstOrDefault();
        //    var ser = new JavaScriptSerializer();
        //    if (footer != null)
        //    {
        //        return
        //            new
        //            {
        //                Id = footer.Id,
        //                BackColor = footer.BackColor,
        //                TextColor=footer.TextColor,
        //                MinimumHeight = footer.MinimumHeight,
        //                FooterCms = ser.Deserialize<dynamic>(footer.FooterCms)
        //            };
        //    }
        //    return null;
        //}

        //[Route("SaveFooter")]
        //[HttpPost]
        //public void SaveFooter(dynamic footer)
        //{
        //    if (footer.Id > 0)
        //    {
        //        //update
        //        int id = footer.Id;
        //        var s = _siteFooter.GetById(id);
        //        s.FooterCms = footer.FooterCms.ToString();
        //        s.BackColor = footer.BackColor;
        //        s.TextColor = footer.TextColor;
        //        s.MinimumHeight = footer.MinimumHeight;
        //        _siteFooter.Update(s);
        //    }
        //    else
        //    {
        //        //Add
        //        var s = new SiteFooter
        //        {
        //            FooterCms = footer.FooterCms.ToString(),
        //            BackColor = footer.BackColor,
        //            TextColor = footer.TextColor,
        //            MinimumHeight = footer.MinimumHeight
        //        };

        //        _siteFooter.Add(s);
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedFooter");
        //}

        //#region PrinterImages

        //[Route("SaveAllPrinterImages")]
        //[HttpPost]
        //public object SaveAllPrinterImages(dynamic sliders)
        //{
        //    var order = 1;
        //    foreach (var slide in sliders)
        //    {
        //        if (slide.Id > 0)
        //        {
        //            //update
        //            int id = slide.Id;
        //            var s = _sliderRepository.GetById(id);
        //            s.ImagePath = slide.ImagePath;
        //            s.ImgAltText = slide.ImgAltText;
        //            s.Title = slide.Title;
        //            s.SubTitle = slide.SubTitle;
        //            s.ButtonText = string.Empty;
        //            s.ButtonLink = slide.ButtonLink;
        //            s.Type = (int)SliderTypes.GatewayAllPrinterImages;
        //            s.OrderBy = order;
        //            _sliderRepository.Update(s);
        //        }
        //        else
        //        {
        //            //Add
        //            var s = new Slider
        //            {
        //                ImagePath = slide.ImagePath,
        //                Title = slide.Title,
        //                ImgAltText = slide.ImgAltText,
        //                SubTitle = slide.SubTitle,
        //                ButtonText = string.Empty,
        //                ButtonLink = slide.ButtonLink,
        //                Type = (int)SliderTypes.GatewayAllPrinterImages,
        //                OrderBy = order
        //            };
        //            _sliderRepository.Add(s);
        //        }
        //        order = order + 1;
        //    }
        //    // RETURN A MESSAGE.
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayAllPrinterImages");
        //    return _sliderRepository.GetMany(y => y.Type == (int)SliderTypes.GatewayAllPrinterImages).OrderBy(c => c.OrderBy).ToList()
        //       .Select(x => new { Id = x.Id, ImagePath = x.ImagePath, Title = x.Title, SubTitle = x.SubTitle, ButtonText = x.ButtonText, ButtonLink = x.ButtonLink }).ToList(); 
        //}


        //[Route("DeleteAllPrinterImageById")]
        //[HttpPost]
        //public object DeleteAllPrinterImageById(dynamic slide)
        //{
        //    int id = slide.Id;
        //    _sliderRepository.Delete(x => x.Id == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayAllPrinterImages");
        //    return _sliderRepository.GetMany(y => y.Type == (int)SliderTypes.GatewayAllPrinterImages).OrderBy(c => c.OrderBy).ToList()
        //        .Select(x => new { Id = x.Id, ImagePath = x.ImagePath, Title = x.Title, SubTitle = x.SubTitle, ButtonText = x.ButtonText, ButtonLink = x.ButtonLink  }).ToList(); 

        //}

        //[Route("GetAllPrinterImageList")]
        //[HttpGet]
        //public dynamic GetAllPrinterImageList()
        //{
        //    return _sliderRepository.GetMany(y => y.Type == (int)SliderTypes.GatewayAllPrinterImages).OrderBy(c => c.OrderBy).ToList()
        //       .Select(x => new { Id = x.Id, ImagePath = x.ImagePath,ImgAltText = x.ImgAltText, Title = x.Title, SubTitle = x.SubTitle, ButtonText = x.ButtonText, ButtonLink = x.ButtonLink }).ToList(); 
        //}

        //[Route("AllPrinterImageListUpdateOrderBy")]
        //[HttpPost]
        //public string AllPrinterImageListUpdateOrderBy(dynamic sliders)
        //{
        //    var order = 1;
        //    foreach (var slide in sliders)
        //    {
        //        if (slide.Id > 0)
        //        {
        //            //update
        //            int id = slide.Id;
        //            var s = _sliderRepository.GetById(id);
        //            s.OrderBy = order;
        //            _sliderRepository.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedGatewayAllPrinterImages");
        //    // RETURN A MESSAGE.
        //    return "Done";
        //}


        //#endregion

        //#region PrinterImages

        //[Route("SaveAllPrinterImages")]
        //[HttpPost]
        //public object SaveServingSolutions(dynamic sliders)
        //{
        //    var order = 1;
        //    foreach (var slide in sliders)
        //    {
        //        if (slide.Id > 0)
        //        {
        //            //update
        //            int id = slide.Id;
        //            var s = _servingSolutions.GetById(id);
        //            s.ImagePath = slide.ImagePath;
        //            s.ImgAltText = slide.ImgAltText;
        //            s.TitleText = slide.TitleText;
        //            s.TitleTextColor = slide.TitleTextColor;
        //            s.TitleTextHoverColor = slide.TitleTextHoverColor;
        //            s.HoverImagePath = slide.HoverImagePath;
        //            s.LinkUrl = slide.LinkUrl;
        //            s.OrderBy = order;
        //            _servingSolutions.Update(s);
        //        }
        //        else
        //        {
        //            //Add
        //            var s = new ServingSolutions
        //            {
        //                ImagePath = slide.ImagePath,
        //                ImgAltText = slide.ImgAltText,
        //                TitleText = slide.TitleText,
        //                TitleTextColor = slide.TitleTextColor,
        //                TitleTextHoverColor = slide.TitleTextHoverColor,
        //                HoverImagePath = slide.HoverImagePath,
        //                LinkUrl = slide.LinkUrl,
        //                OrderBy = order
        //            };
        //            _servingSolutions.Add(s);
        //        }
        //        order = order + 1;
        //    }
        //    // RETURN A MESSAGE.
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedServingSolutions");
        //    return _servingSolutions.GetAll().OrderBy(c => c.OrderBy).ToList()
        //       .Select(x => new { Id = x.Id, ImagePath = x.ImagePath, HoverImagePath = x.HoverImagePath, TitleText = x.TitleText, TitleTextColor = x.TitleTextColor, TitleTextHoverColor = x.TitleTextHoverColor, LinkUrl = x.LinkUrl }).ToList();
        //}

        //[Route("DeleteServingSolutionById")]
        //[HttpPost]
        //public object DeleteServingSolutionById(dynamic slide)
        //{
        //    int id = slide.Id;
        //    _servingSolutions.Delete(x => x.Id == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedServingSolutions");
        //    return _servingSolutions.GetAll().OrderBy(c => c.OrderBy).ToList()
        //       .Select(x => new { Id = x.Id, ImagePath = x.ImagePath, HoverImagePath = x.HoverImagePath, TitleText = x.TitleText, TitleTextColor = x.TitleTextColor, TitleTextHoverColor = x.TitleTextHoverColor, LinkUrl = x.LinkUrl }).ToList();

        //}

        //[Route("GetServingSolutionList")]
        //[HttpGet]
        //public dynamic GetServingSolutionList()
        //{
        //    return _servingSolutions.GetAll().OrderBy(c => c.OrderBy).ToList()
        //       .Select(x => new { Id = x.Id, ImagePath = x.ImagePath, HoverImagePath = x.HoverImagePath,ImgAltText = x.ImgAltText, TitleText = x.TitleText, TitleTextColor = x.TitleTextColor, TitleTextHoverColor = x.TitleTextHoverColor, LinkUrl = x.LinkUrl }).ToList();
        //}


        //[Route("ServingSolutionListUpdateOrderBy")]
        //[HttpPost]
        //public string ServingSolutionListUpdateOrderBy(dynamic sliders)
        //{
        //    var order = 1;
        //    foreach (var slide in sliders)
        //    {
        //        if (slide.Id > 0)
        //        {
        //            //update
        //            int id = slide.Id;
        //            var s = _servingSolutions.GetById(id);
        //            s.OrderBy = order;
        //            _servingSolutions.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedServingSolutions");
        //    // RETURN A MESSAGE.
        //    return "Done";
        //}

        //#endregion

        //#region Our Integrations
        //[Route("SaveOurIntegrations")]
        //[HttpPost]
        //public object SaveOurIntegrations(dynamic newsList)
        //{
        //    var order = 1;
        //    foreach (var news in newsList)
        //    {
        //        if (news.Id > 0)
        //        {
        //            //update
        //            int id = news.Id;
        //            var s = _ourIntegrations.GetById(id);
        //            s.Title = news.Title;
        //            s.CMSContent = news.CMSContent;
        //            s.OrderBy = order;
        //            _ourIntegrations.Update(s);
        //        }
        //        else
        //        {
        //            //Add
        //            var s = new OurIntegrations
        //            {
        //                Title = news.Title,
        //                CMSContent = news.CMSContent,
        //                OrderBy = order
        //            };

        //            _ourIntegrations.Add(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedOurIntegrations");
        //    // RETURN A MESSAGE.
        //    return _ourIntegrations.GetAll().OrderBy(x=> x.OrderBy).ToList();
        //}


        //[Route("GetOurIntegrationsList")]
        //[HttpGet]
        //public dynamic GetOurIntegrationsList()
        //{
        //    return _ourIntegrations.GetAll().OrderBy(x => x.OrderBy).ToList();
        //}

        //[Route("DeleteourIntegrationId")]
        //[HttpPost]
        //public object DeleteourIntegrationId(dynamic news)
        //{
        //    int id = news.Id;
        //    _ourIntegrations.Delete(x => x.Id == id);
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedOurIntegrations");
        //    return _ourIntegrations.GetAll().OrderBy(x => x.OrderBy).ToList();
        //}

        //[Route("OurIntegrationsUpdateOrderBy")]
        //[HttpPost]
        //public object OurIntegrationsUpdateOrderBy(dynamic newsList)
        //{
        //    var order = 1;
        //    foreach (var news in newsList)
        //    {
        //        if (news.Id > 0)
        //        {
        //            //update
        //            int id = news.Id;
        //            var s = _ourIntegrations.GetById(id);
        //            s.OrderBy = order;
        //            _ourIntegrations.Update(s);
        //        }
        //        order = order + 1;
        //    }
        //    HttpContext.Current.Cache.Remove("StarResponsiveCachedOurIntegrations");
        //    // RETURN A MESSAGE.
        //    return _ourIntegrations.GetAll().OrderBy(x => x.OrderBy).ToList();
        //}
        //#endregion
    }
}
