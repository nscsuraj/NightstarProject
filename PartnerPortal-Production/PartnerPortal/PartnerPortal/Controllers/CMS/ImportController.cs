using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PartnerPortal.Core.Attributes;
using PartnerPortal.Domain.Import;
using PartnerPortal.Repository;
using PartnerPortal.Utility;

namespace PartnerPortal.Controllers.CMS
{
    [AuthorizeCms]
    public class ImportController : Controller
    {

        private readonly IEFRepository<ProductDetails> _productRepository;
        private readonly IEFRepository<ImportAudit> _importAudit;

        public ImportController(IEFRepository<ProductDetails> productRepository,
            IEFRepository<ImportAudit> importAudit)
        {
            _productRepository = productRepository;
            _importAudit = importAudit;
        }

        [HttpPost]
        public ActionResult ImportProductNumber()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string query = null;
                    string connString = "";
                    var extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ImportedFiles/"), fileName);
                    DataTable dt = null;
                    if (validFileTypes.Contains(extension))
                    {
                        if (System.IO.File.Exists(path))
                        { System.IO.File.Delete(path); }
                        file.SaveAs(path);
                        if (extension == ".csv")
                        {
                            dt = Converter.ConvertCsvToDataTable(path);
                        }
                        //Connection String to Excel Workbook  
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = Converter.ConvertXslxToDataTable(path, connString);
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = Converter.ConvertXslxToDataTable(path, connString);
                        }
                        if (dt != null)
                        {
                            if (dt.Columns.Count == 1)
                            {
                                ViewBag.Error = dt.Rows[0][0];
                                LogError(dt.Rows[0][0].ToString());
                            }
                            //modify column names
                            foreach (DataColumn dc in dt.Columns)
                            {
                                dc.ColumnName = dc.ColumnName.ToLower();
                            }

                            //new (Add all)
                            //Delete existing Records
                            var ps = _productRepository.GetAll().ToList();
                            ps.ToList().ForEach(x=>_productRepository.Delete(x));

                            foreach (DataRow dr in dt.Rows)
                            {
                                var newProduct = new ProductDetails();
                                newProduct.BindingPart = dr["u_bindingpart"].ToString();
                                newProduct.ItemCode = dr["itemcode"].ToString();
                                newProduct.ItemName = dr["itemname"].ToString();
                                newProduct.FrozenFor = dr["frozenfor"].ToString();
                                newProduct.ProductClass = dr["u_productclass"].ToString();
                                newProduct.ProductSubClass = dr["u_subclass"].ToString();
                                newProduct.PurchaseItem = dr["prchseitem"].ToString();
                                newProduct.SellItem = dr["sellitem"].ToString();
                                newProduct.InventoryItem = dr["invntitem"].ToString();
                                newProduct.InspectionFlag = dr["u_inspectionflag"].ToString();
                                newProduct.CountryOfOrigin = dr["u_countryoforigin"].ToString();
                                newProduct.DiscontinueDate = dr["u_discontinuedate"].ToString();
                                newProduct.CreateDate = dr["createdate"].ToString();
                                newProduct.SalesDescription = dr["u_salesdescription"].ToString();
                                newProduct.SparePartFor = dr["u_sparepartfor"].ToString();
                                newProduct.PalletQty = dr["u_palletqty"].ToString();
                                newProduct.PSIExclude = dr["u_psi_exclude"].ToString();
                                newProduct.CustomerSpecific = dr["u_custspecific"].ToString();
                                newProduct.CSpecialOrder = dr["u_cspecialorder"].ToString();
                                newProduct.UPC = dr["u_upc"].ToString();
                                newProduct.Exclusive = dr["u_exclusive"].ToString();
                                newProduct.PricelistExclude = dr["u_pricelist_exclude"].ToString();
                                newProduct.NonStandard = dr["u_nonstandard"].ToString();
                                newProduct.VirtualPN = dr["u_virtual_pn"].ToString();
                                newProduct.UWebsite = dr["u_website"].ToString();
                                _productRepository.Add(newProduct);
                            }
                            LogSuccess();
                            ViewBag.Error = "Records imported successfully";
                            //verify template
                            /*string[] validColumns = { "itemcode", "itemname", "frozenfor", "u_productclass", "u_subclass", "prchseitem", "sellitem", "invntitem", "u_inspectionflag", "u_countryoforigin", "u_discontinuedate", "createdate",
                             * "u_salesdescription","u_sparepartfor","u_palletqty","u_psi_exclude","u_custspecific", "u_cspecialorder","u_upc","u_exclusive", "u_pricelist_exclude", "u_bindingpart","u_nonstandard","u_virtual_pn","u_website" };

                            var proceed = dt.Columns.Cast<DataColumn>().Where(x => validColumns.Contains(x.ColumnName)).Select(m=> m.ColumnName).ToArray().Count() == validColumns.Count();
                         

                            if (proceed)
                            {

                                var tblFilteredNext =
                                    dt.AsEnumerable()
                                        .Where(
                                            row =>
                                                ((row.Field<String>("frozenfor") != null && (row.Field<String>("frozenfor").ToLower() == "n" || row.Field<String>("frozenfor").ToLower() == "") )
                                                || (row.Field<String>("u_discontinuedate") != null && (row.Field<String>("u_discontinuedate").ToLower() == ""))
                                                || (row.Field<String>("u_custspecific") != null && (row.Field<String>("u_custspecific").ToLower() == "n" || row.Field<String>("u_custspecific").ToLower() == ""))
                                                || (row.Field<String>("u_cspecialorder") != null && (row.Field<String>("u_cspecialorder").ToLower() == "n" || row.Field<String>("u_cspecialorder").ToLower() == ""))
                                                || (row.Field<String>("u_pricelist_exclude") != null && (row.Field<String>("u_pricelist_exclude").ToLower() == "n" || row.Field<String>("u_pricelist_exclude").ToLower() == ""))
                                                    ))
                                        .CopyToDataTable();
                                var duplicates = tblFilteredNext.AsEnumerable().GroupBy(r => r["u_bindingpart"]).Where(gr => gr.Count() > 1).Select(m=> new {BindingPart = m.Key}).ToArray();
                                var products = _productRepository.GetAll().ToList();
                                foreach (DataRow dr in tblFilteredNext.Rows)
                                {
                                    var bindingPart = dr["u_bindingpart"].ToString();
                                    if (duplicates.AsEnumerable().Any(x => x.BindingPart.ToString() == bindingPart))
                                    {

                                        var dup =
                                            dt.AsEnumerable()
                                                .Where(
                                                    row => row.Field<object>("u_bindingpart").ToString() == bindingPart)
                                                .CopyToDataTable();
                                        //correct the date field data
                                        foreach (DataRow drp in dup.Rows)
                                        {
                                            var dts = drp["createdate"].ToString();
                                            if (!dts.Contains("/"))
                                            {
                                                if (dts.Length == 5)
                                                {
                                                    dts = "0" + dts;
                                                }
                                                dts = dts.Substring(0, 2) + "/" + dts.Substring(2, 2) + "/19" +
                                                      dts.Substring(4, 2);
                                            }
                                            drp["createdate"] = dts;
                                        }

                                        var bpart =
                                            dup.AsEnumerable()
                                                .OrderByDescending(
                                                    row => DateTime.ParseExact(row.Field<String>("createdate"),"MM/dd/yyyy",null))
                                                .First();

                                        if (products.Any(x => x.BindingPart == bindingPart))
                                        {
                                            var newProduct = _productRepository.Get(x => x.BindingPart == bindingPart);
                                            newProduct.BindingPart = bpart["u_bindingpart"].ToString();
                                            newProduct.ItemCode = bpart["itemcode"].ToString();
                                            newProduct.ItemName = bpart["itemname"].ToString();
                                            newProduct.ProductClass = bpart["u_productclass"].ToString();
                                            newProduct.ProductSubClass = bpart["u_subclass"].ToString();
                                            newProduct.CountryOfOrigin = bpart["u_countryoforigin"].ToString();
                                            newProduct.CreateDate = bpart["createdate"].ToString();
                                            newProduct.SalesDescription = bpart["u_salesdescription"].ToString();
                                            _productRepository.Update(newProduct);
                                        }
                                        else
                                        {
                                            var newProduct = new ProductDetails();
                                            newProduct.BindingPart = bpart["u_bindingpart"].ToString();
                                            newProduct.ItemCode = bpart["itemcode"].ToString();
                                            newProduct.ItemName = bpart["itemname"].ToString();
                                            newProduct.ProductClass = bpart["u_productclass"].ToString();
                                            newProduct.ProductSubClass = bpart["u_subclass"].ToString();
                                            newProduct.CountryOfOrigin = bpart["u_countryoforigin"].ToString();
                                            newProduct.CreateDate = bpart["createdate"].ToString();
                                            newProduct.SalesDescription = bpart["u_salesdescription"].ToString();
                                            _productRepository.Add(newProduct);

                                        }
                                    }
                                    else
                                    {
                                        if (products.Any(x => x.BindingPart == dr["u_bindingpart"].ToString()))
                                        {
                                            var newProduct = _productRepository.Get(x => x.BindingPart == bindingPart);
                                            newProduct.BindingPart = dr["u_bindingpart"].ToString();
                                            newProduct.ItemCode = dr["itemcode"].ToString();
                                            newProduct.ItemName = dr["itemname"].ToString();
                                            newProduct.ProductClass = dr["u_productclass"].ToString();
                                            newProduct.ProductSubClass = dr["u_subclass"].ToString();
                                            newProduct.CountryOfOrigin = dr["u_countryoforigin"].ToString();
                                            newProduct.CreateDate = dr["createdate"].ToString();
                                            newProduct.SalesDescription = dr["u_salesdescription"].ToString();
                                            _productRepository.Update(newProduct);
                                        }
                                        else
                                        {
                                            var newProduct = new ProductDetails();
                                            newProduct.BindingPart = dr["u_bindingpart"].ToString();
                                            newProduct.ItemCode = dr["itemcode"].ToString();
                                            newProduct.ItemName = dr["itemname"].ToString();
                                            newProduct.ProductClass = dr["u_productclass"].ToString();
                                            newProduct.ProductSubClass = dr["u_subclass"].ToString();
                                            newProduct.CountryOfOrigin = dr["u_countryoforigin"].ToString();
                                            newProduct.CreateDate = dr["createdate"].ToString();
                                            newProduct.SalesDescription = dr["u_salesdescription"].ToString();
                                            _productRepository.Add(newProduct);
                                            
                                        }

                                    }
                                  
                                }

                            
                                ViewBag.Error = "Records imported successfully";
                            }
                            else
                            {
                                ViewBag.Error += "Choosen file does not match with specified template format. Please verify.";
                            }
                            */
                        }
                        else
                        {
                            LogError("There was error in processing records. Please verify file and try again.");
                            ViewBag.Error = "There was error in processing records. Please verify file and try again.";    
                        }
                        
                    }
                    else
                    {
                        LogError("Please Upload Files in .xls, .xlsx or .csv format");
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";

                    }  
                }
            }
           return View();
        }

        private void LogSuccess()
        {
            var au = new ImportAudit();
            au.ImportDate = DateTime.Now;
            au.Comment = "Import Successed";
            au.ImportStatus = "Success";
            au.ErrorDetail = string.Empty;
            au.ImportSource = "Web";

            _importAudit.Add(au);
        }
        private void LogError(string err)
        {
            var au = new ImportAudit();
            au.ImportDate = DateTime.Now;
            au.Comment = "Import Failed";
            au.ImportStatus = "Failed";
            au.ErrorDetail = err.Replace("'","''");
            au.ImportSource = "Web";

            _importAudit.Add(au);
        }

        public string UploadCompanySCSLogo()
        {
            string path = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic != null && pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    //var fileExt = Path.GetExtension(pic.FileName);

                    //_imgname = Guid.NewGuid().ToString();
                    path = Server.MapPath("/Images/Supports/SoftwarePartners/") + fileName;
                    //_imgname = "MVC_" + _imgname + _ext;

                    //ViewBag.Msg = _comPath;
                    //var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    //// resizing image
                    //MemoryStream ms = new MemoryStream();
                    //WebImage img = new WebImage(_comPath);

                    //if (img.Width > 200)
                    //    img.Resize(200, 200);
                    //img.Save(_comPath);
                    // end resize
                }
            }
            return path;
        }
    }
}
