using System;
using System.Collections.Generic;
using System.Drawing;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Repository;

namespace PartnerPortal.Controllers.CMS
{
    [RoutePrefix("api/ppeditor")]
    public class PPEditorController : BaseApiController
    {
        /// <summary>
        ///   Member Variables
        /// </summary>
        private readonly IEFRepository<UploadInformation> _uploadEfRepository;
        
        
        /// <summary>
        ///     Editor Controller
        /// </summary>
        public PPEditorController(IEFRepository<UploadInformation> uploEfRepository)
        {
            _uploadEfRepository = uploEfRepository;

        }

        /// <summary>
        ///     Upload Files
        /// </summary>
        [Route("UploadFiles")]
        [HttpPost]
        public string UploadFiles()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/CMSFiles/");
            var destinationUrl = Url.Content("~/Resources/CMSFiles/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            var t = System.Web.HttpContext.Current.Request.Form["UploadedType"];

            var filesNotSaved = new List<string>();

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    var uploadInfo = new UploadInformation();
                    var extension = hpf.FileName.Substring(hpf.FileName.LastIndexOf('.'), hpf.FileName.Length - hpf.FileName.LastIndexOf('.'));
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        //var s = hpf.FileName + hpf.ContentLength.ToString() + hpf.ContentType;
                        
                        // SAVE THE FILES IN THE FOLDER.
                        uploadInfo.OriginalFileName = hpf.FileName;
                        uploadInfo.FileName = hpf.FileName;
                        uploadInfo.FilePath = destinationUrl + hpf.FileName;
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                    }
                    else
                    {
                        uploadInfo.OriginalFileName = hpf.FileName;
                        uploadInfo.FileName = Guid.NewGuid() + extension;
                        uploadInfo.FilePath = destinationUrl + uploadInfo.FileName;
                        hpf.SaveAs(sPath + uploadInfo.FileName);
                    }
                    uploadInfo.CreateDate = DateTime.Now;
                    uploadInfo.FileSize = hpf.ContentLength.ToString();
                    uploadInfo.UploadType = Convert.ToInt32(t);
                    uploadInfo.FileType = hpf.ContentType;
                    if (uploadInfo.FileType.ToLower().Contains("image"))
                    {
                        var img = new Bitmap(hpf.InputStream);
                        uploadInfo.OriginalHeight = img.Height;
                        uploadInfo.OriginalWidth = img.Width;
                    }

                    _uploadEfRepository.Add(uploadInfo);

                }
            }

            // RETURN A MESSAGE.
            return "Files Uploaded Successfully";
        }

        /// <summary>
        ///     Upload Files
        /// </summary>
        [Route("UploadFiles")]
        [HttpPost]
        public void UpdateFileInfo(UploadInformation obj)
        {
            _uploadEfRepository.Update(obj);
        }

        /// <summary>
        ///     Delete File Info
        /// </summary>
        [Route("DeleteFileInfo")]
        [HttpPost]
        public void DeleteFileInfo(UploadInformation obj)
        {
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/CMSFiles/");

            var file = _uploadEfRepository.GetById(obj.Id);
            File.Delete(sPath + Path.GetFileName(file.FileName));
            _uploadEfRepository.Delete(file);
        }

        /// <summary>
        ///     Get Upload Informations
        /// </summary>
        [Route("GetUploadInformations")]
        [HttpGet]
        public object GetUploadInformations()
        {
            return new
            {
                Data = _uploadEfRepository.GetAll(),
                Dates = _uploadEfRepository.GetAll().GroupBy(x=> x.CreateDate.Date).Select(m=> m.Key)
            };
        }

       
    }
}
