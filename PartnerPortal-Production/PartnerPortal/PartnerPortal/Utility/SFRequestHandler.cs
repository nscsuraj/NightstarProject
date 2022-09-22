using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using PartnerPortal.Domain.Accounts;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Repository;
using RestSharp;

namespace PartnerPortal.Utility
{
    public class SFRequestHandler
    {
        private IEFRepository<SalesforceAuthentication> _authenticationToken;
        private IEFRepository<SFTempSessionOpportunityData> _sessionOpportunityData;
        private IEFRepository<SFTempSessionOpportunityProducts> _sessionOpportunityProduct;
        private string sessionKey;
        private readonly IEFRepository<SFTempSessionPurchaseByDistributors> _sessionPurchaseByDistributors;
        private readonly IEFRepository<SFTempSessionPurchaseByProductClasses> _sessionPurchaseByProductClasses;
        private readonly IEFRepository<SFTempSessionMdfData> _sessionMdfRepository;
        private readonly IEFRepository<SFAccounts> _sfAccounts;
        private readonly IEFRepository<SFTempSessionDelegateData> _delegateReport;
        private readonly IEFRepository<SFTempSessionDelegateRebateItemData> _delegateRebateItemReport;

        public SFRequestHandler(
            IEFRepository<SalesforceAuthentication> authenticationToken,
            IEFRepository<SFTempSessionOpportunityData> sessionOpportunityData,
            IEFRepository<SFTempSessionOpportunityProducts> sessionOpportunityProduct,
            IEFRepository<SFTempSessionPurchaseByDistributors> sessionPurchaseByDistributors,
            IEFRepository<SFTempSessionPurchaseByProductClasses> sessionPurchaseByProductClasses,
            IEFRepository<SFTempSessionMdfData> sessionMdfRepository,
            IEFRepository<SFAccounts> sfAccounts,
            IEFRepository<SFTempSessionDelegateData> delegateReport,
            IEFRepository<SFTempSessionDelegateRebateItemData> delegateRebateItemReport,
            string _sessionKey
            )
        {
            _authenticationToken = authenticationToken;
            _sessionOpportunityData = sessionOpportunityData;
            _sessionOpportunityProduct = sessionOpportunityProduct;
            sessionKey = _sessionKey;
            _sessionPurchaseByDistributors = sessionPurchaseByDistributors;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            _sessionPurchaseByProductClasses = sessionPurchaseByProductClasses;
            _sessionMdfRepository = sessionMdfRepository;
            _sfAccounts = sfAccounts;
            _delegateReport = delegateReport;
            _delegateRebateItemReport = delegateRebateItemReport;
        }

        private SalesforceAuthentication GenerateSalesForceAccessToken()
        {
                String id = ConfigurationManager.AppSettings["SFClientId"];
                String secret = ConfigurationManager.AppSettings["SFClientSecret"];


                //var client = new RestClient("https://test.salesforce.com/services/oauth2/token");
                var client = new RestClient(ConfigurationManager.AppSettings["SFRestTokenUrl"]);
                var userName = ConfigurationManager.AppSettings["SFUserName"];
                var userPwd = ConfigurationManager.AppSettings["SFUserPassword"];

                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded",
                    string.Format("grant_type=password&client_id={0}&client_secret={1}&username={2}&password={3}", id,
                        secret, userName, userPwd), ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                var authObj =
                    new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SalesforceAuthentication>(
                        response.Content);

                var auth = _authenticationToken.GetAll().FirstOrDefault();
                if (auth == null)
                {
                    _authenticationToken.Add(authObj);
                }
                else
                {
                    _authenticationToken.Delete(auth);
                    _authenticationToken.Add(authObj);
                }
                return authObj;
            //dynamic resp = JObject.Parse(response.Content);
            //String token = resp.access_token;

        }

        private SalesforceAuthentication GetSalesforceAccessToken()
        {
            var auth = _authenticationToken.GetAll().FirstOrDefault();
            if (auth == null)
            {
                return GenerateSalesForceAccessToken();
            }
                return auth;
        }
        
        //public object GetAccountDetail(string AccountId)
        //{
        //    var salesforce_auth = GetSalesforceAccessToken();

            
        //    var url = string.Format(
        //        "{0}services/data/v48.0/query/?q=SELECT+Id,+Name,+Partner_Email__c,+Partner_Type__c,+Account_Type__c,+Partner_Number__c,+Account_Expert__c,+Support_Expert__c,+Discount__c,+Loyalty_Level__c+FROM+Account+WHERE+Id+='{1}'", ConfigurationManager.AppSettings["SFRestUrl"], AccountId);

        //    var client = new RestClient(url);
        //    var request = new RestRequest(Method.GET);

        //    request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
        //     request.AddHeader("cache-control", "no-cache");
        //     request.AddHeader("content-type", "application/json");

        //    var response = client.Execute(request);
        //    if (response.Content.Contains("Session expired or invalid"))
        //    {
        //        GenerateSalesForceAccessToken();
        //        return GetAccountDetail(AccountId);
        //    }
        //    dynamic resp = JObject.Parse(response.Content);
        //    return resp;

        //}

        public void GetSFDataForMyAccount(SFAccounts account)
        {
            ProcessAccountData(account);
            if (!string.IsNullOrEmpty(account.PartnerType))
            {
                if (account.PartnerType.ToLower().Contains("distributor"))
                {
                    var oppUrl = string.Format(
                "{0}services/data/v48.0/query/?q=SELECT+Id,+AccountId,+Name,+Type,+Description,+TotalOpportunityQuantity,+Quantity__c,+Amount,+StageName,+CloseDate,+IsClosed,+CreatedDate,+Business_case__c,+Product_s_of_Interest__c,+Distributor_Mark__c,+Product_Quantity__c,+Status__c,+How_can_we_support_you__c,+Account_Expert_Email__c,+Account_Expert__c,+Star_Update__c,+Cancelled_Date__c,+(+SELECT+Quantity,+UnitPrice,+TotalPrice,+PricebookEntry.Name,+PricebookEntry.Product2.Family,+PricebookEntry.Product2.ProductCode+FROM+OpportunityLineItems+)+FROM+Opportunity+WHERE+(+Distributor__c+='{1}'+OR+Distributor_Mark__c+='{2}'+)+AND+LeadSource+='Partner Portal'", ConfigurationManager.AppSettings["SFRestUrl"], account.AccountId, account.AccountName);
                    ProcessOpportunitiesForDistributor(account, oppUrl);

                    var distPurchUrl = string.Format(
                        "{0}services/data/v48.0/analytics/reports/{1}", ConfigurationManager.AppSettings["SFRestUrl"],
                        ConfigurationManager.AppSettings["SFPurchaseByDistributorReportId"]);
                    ProcessPurchaseByDistributorsForAccount(account, distPurchUrl);

                    //var mdfUrl = string.Format(
                    //"{0}services/data/v48.0/analytics/reports/{1}", ConfigurationManager.AppSettings["SFRestUrl"],
                    //ConfigurationManager.AppSettings["SFMDFReportId"]);
                    //ProcessMdfForAccount(account, mdfUrl);

                }
                else
                {

                    var oppUrl = string.Format(
                "{0}services/data/v48.0/query/?q=SELECT+Id,+AccountId,+Name,+Type,+Description,+TotalOpportunityQuantity,+Quantity__c,+Amount,+StageName,+CloseDate,+IsClosed,+CreatedDate,+Business_case__c,+Product_s_of_Interest__c,+Distributor_Mark__c,+Product_Quantity__c,+Status__c,+How_can_we_support_you__c,+Account_Expert_Email__c,+Account_Expert__c,+Star_Update__c,+Cancelled_Date__c,+(+SELECT+Quantity,+UnitPrice,+TotalPrice,+PricebookEntry.Name,+PricebookEntry.Product2.Family,+PricebookEntry.Product2.ProductCode+FROM+OpportunityLineItems+)+FROM+Opportunity+WHERE+AccountId+='{1}'+AND+LeadSource+='Partner Portal'", ConfigurationManager.AppSettings["SFRestUrl"], account.AccountId);
                    ProcessOpportunitiesForAccount(account, oppUrl);

                    var purchByProductUrl = string.Format(
                        "{0}services/data/v48.0/analytics/reports/{1}", ConfigurationManager.AppSettings["SFRestUrl"],
                        ConfigurationManager.AppSettings["SFPurchaseByProductClassReportId"]);
                    ProcessPurchaseByProductClassesForAccount(account, purchByProductUrl);

                    var mdfUrl = string.Format(
                        "{0}services/data/v48.0/analytics/reports/{1}", ConfigurationManager.AppSettings["SFRestUrl"],
                        ConfigurationManager.AppSettings["SFMDFReportId"]);
                    ProcessMdfForAccount(account, mdfUrl);
                    if(account.PartnerType.ToLower() == "technology partner" || account.PartnerType.ToLower() == "solution partner" || account.PartnerType.ToLower() == "fulfillment partner" || account.PartnerType.ToLower() == "services partner")
                    {
                        var delegateUrl = string.Format(
                            "{0}services/data/v48.0/analytics/reports/{1}", ConfigurationManager.AppSettings["SFRestUrl"],
                            ConfigurationManager.AppSettings["DelegateReportId"]);
                        ProcessDelegateForAccount(account, delegateUrl);

                        var delegateRebateUrl = string.Format(
                            "{0}services/data/v48.0/analytics/reports/{1}", ConfigurationManager.AppSettings["SFRestUrl"],
                            ConfigurationManager.AppSettings["DelegateRebateItemReportId"]);
                        ProcessDelegateRebateItemForAccount(account, delegateRebateUrl);
                    }

                }
            }
        }


        public void ReloadSFOpportunityData(SFAccounts account)
        {
            if (account.PartnerType.ToLower().Contains("distributor"))
            {
                var oppUrl = string.Format(
            "{0}services/data/v48.0/query/?q=SELECT+Id,+AccountId,+Name,+Type,+Description,+TotalOpportunityQuantity,+Quantity__c,+Amount,+StageName,+CloseDate,+IsClosed,+CreatedDate,+Business_case__c,+Product_s_of_Interest__c,+Distributor_Mark__c,+Product_Quantity__c,+Status__c,+How_can_we_support_you__c,+Account_Expert_Email__c,+Account_Expert__c,+Star_Update__c,+Cancelled_Date__c,+(+SELECT+Quantity,+UnitPrice,+TotalPrice,+PricebookEntry.Name,+PricebookEntry.Product2.Family,+PricebookEntry.Product2.ProductCode+FROM+OpportunityLineItems+)+FROM+Opportunity+WHERE+(+Distributor__c+='{1}'+OR+Distributor_Mark__c+='{2}'+)+AND+LeadSource+='Partner Portal'", ConfigurationManager.AppSettings["SFRestUrl"], account.AccountId, account.AccountName);
                ProcessOpportunitiesForDistributor(account, oppUrl);
            }
            else
            {
                var oppUrl = string.Format(
            "{0}services/data/v48.0/query/?q=SELECT+Id,+AccountId,+Name,+Type,+Description,+TotalOpportunityQuantity,+Quantity__c,+Amount,+StageName,+CloseDate,+IsClosed,+CreatedDate,+Business_case__c,+Product_s_of_Interest__c,+Distributor_Mark__c,+Product_Quantity__c,+Status__c,+How_can_we_support_you__c,+Account_Expert_Email__c,+Account_Expert__c,+Star_Update__c,+Cancelled_Date__c,+(+SELECT+Quantity,+UnitPrice,+TotalPrice,+PricebookEntry.Name,+PricebookEntry.Product2.Family,+PricebookEntry.Product2.ProductCode+FROM+OpportunityLineItems+)+FROM+Opportunity+WHERE+AccountId+='{1}'+AND+LeadSource+='Partner Portal'", ConfigurationManager.AppSettings["SFRestUrl"], account.AccountId);
                ProcessOpportunitiesForAccount(account, oppUrl);
            }
        }

        public void ProcessOpportunitiesForAccount(SFAccounts account,string url)
        {
            try
            {
                dynamic resOpportunity = GetOpportunitiesForAccount(url);
                _sessionOpportunityData.Delete(
                               x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                if (resOpportunity.records != null && Enumerable.Count(resOpportunity.records) > 0)
                {
                    foreach (var res in resOpportunity.records)
                    {
                        var tsd = new SFTempSessionOpportunityData
                        {
                            SessionKey = sessionKey,
                            AccountId = account.AccountId,
                            AccountName = account.AccountName,
                            PartnerNumber = account.PartnerNumber,
                            OpportunityId = res.Id,
                            OpportunityName = res.Name,
                            OpportunityType = res.Type,
                            Description = res.Description,
                            TotalOpportunityQuantity = res.Quantity__c,
                            TotalProductQuantity = res.TotalOpportunityQuantity,
                            Amount = res.Amount,
                            StageName = res.StageName,
                            CloseDate = res.CloseDate,
                            IsClosed = res.IsClosed,
                            CreatedDate = res.CreatedDate,

                            BusinessCase = res.Business_case__c,
                            ProductInterest = res.Product_s_of_Interest__c,
                            Distributor = res.Distributor_Mark__c,
                            ProductQuantity = res.Product_Quantity__c,
                            Status = res.Status__c,
                            SupportRequest = res.How_can_we_support_you__c,
                            StarExpert = res.Account_Expert__c,
                            StarExpertEmail = res.Account_Expert_Email__c,
                            StarUpdate = res.Star_Update__c,
                            CancelledDate = res.Cancelled_Date__c,
                        };

                        _sessionOpportunityData.Add(tsd);
                        if (res.OpportunityLineItems != null)
                        {
                            foreach (var prod in res.OpportunityLineItems.records)
                            {
                                var tsdp = new SFTempSessionOpportunityProducts
                                {
                                    SessionKey = sessionKey,
                                    AccountId = account.AccountId,
                                    PartnerNumber = account.PartnerNumber,
                                    RefId = tsd.Id,
                                    OpportunityId = tsd.OpportunityId,
                                    ProductName = prod.PricebookEntry.Name,
                                    ProductFamily = prod.PricebookEntry.Product2.Family,
                                    ProductCode = prod.PricebookEntry.Product2.ProductCode,
                                    Quantity = prod.Quantity,
                                    UnitPrice = prod.UnitPrice,
                                    TotalPrice = prod.TotalPrice
                                };
                                _sessionOpportunityProduct.Add(tsdp);
                            }
                        }

                    }
                    if (!string.IsNullOrEmpty(resOpportunity.NextRecordsUrl))
                    {
                        var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resOpportunity.NextRecordsUrl);
                        ProcessOpportunitiesForAccount(account, newUrl);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ProcessOpportunitiesForDistributor(SFAccounts account, string url)
        {
            try
            {
                dynamic resOpportunity = GetOpportunitiesForAccount(url);
                _sessionOpportunityData.Delete(
                               x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                if (resOpportunity.records != null && Enumerable.Count(resOpportunity.records) > 0)
                {
                    foreach (var res in resOpportunity.records)
                    {
                        var acId = (string)res.AccountId;
                        var tsd = new SFTempSessionOpportunityData
                        {
                            SessionKey = sessionKey,
                            AccountId = account.AccountId,
                            AccountName = _sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == acId) != null ? _sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == acId).AccountName : string.Empty,
                            PartnerNumber = account.PartnerNumber,
                            OpportunityId = res.Id,
                            OpportunityName = res.Name,
                            OpportunityType = res.Type,
                            Description = res.Description,
                            TotalOpportunityQuantity = res.Quantity__c,
                            TotalProductQuantity = res.TotalOpportunityQuantity,
                            Amount = res.Amount,
                            StageName = res.StageName,
                            CloseDate = res.CloseDate,
                            IsClosed = res.IsClosed,
                            CreatedDate = res.CreatedDate,

                            BusinessCase = res.Business_case__c,
                            ProductInterest = res.Product_s_of_Interest__c,
                            Distributor = res.Distributor_Mark__c,
                            ProductQuantity = res.Product_Quantity__c,
                            Status = res.Status__c,
                            SupportRequest = res.How_can_we_support_you__c,
                            StarExpert = res.Account_Expert__c,
                            StarExpertEmail = res.Account_Expert_Email__c,
                            StarUpdate = res.Star_Update__c,
                            CancelledDate = res.Cancelled_Date__c,
                        };

                        _sessionOpportunityData.Add(tsd);
                        if (res.OpportunityLineItems != null)
                        {
                            foreach (var prod in res.OpportunityLineItems.records)
                            {
                                var tsdp = new SFTempSessionOpportunityProducts
                                {
                                    SessionKey = sessionKey,
                                    AccountId = prod.AccountId,
                                    PartnerNumber = account.PartnerNumber,
                                    RefId = tsd.Id,
                                    OpportunityId = tsd.OpportunityId,
                                    ProductName = prod.PricebookEntry.Name,
                                    ProductFamily = prod.PricebookEntry.Product2.Family,
                                    ProductCode = prod.PricebookEntry.Product2.ProductCode,
                                    Quantity = prod.Quantity,
                                    UnitPrice = prod.UnitPrice,
                                    TotalPrice = prod.TotalPrice
                                };
                                _sessionOpportunityProduct.Add(tsdp);
                            }
                        }

                    }
                    if (!string.IsNullOrEmpty(resOpportunity.NextRecordsUrl))
                    {
                        var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resOpportunity.NextRecordsUrl);
                        ProcessOpportunitiesForAccount(account, newUrl);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public object GetOpportunitiesForAccount(string url)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetOpportunitiesForAccount(url);
            }
            dynamic resp = JObject.Parse(response.Content);
            return resp;

        }

        public void ProcessAccountData(SFAccounts account)
        {
            try
            {
                dynamic resOpportunity = GetAccount(account);
                if (resOpportunity != null)
                {
                    account.LoyaltyLevel = resOpportunity.Loyalty_level__c;                   
                }
            }
            catch (Exception ex)
            {
            }
        }

        public object GetAccount(SFAccounts account)
        {
            var url = string.Format(
                "{0}services/data/v48.0/sobjects/{1}/{2}", ConfigurationManager.AppSettings["SFRestUrl"], ConfigurationManager.AppSettings["SFLoyaltyLeadObject"], account.AccountId);
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetAccount(account);
            }
            dynamic resp = JObject.Parse(response.Content);
            return resp;

        }
        public object AddOpportunity(dynamic opp,string AccountId)
        {
            try
            {
                var salesforce_auth = GetSalesforceAccessToken();


                var url = string.Format(
                    "{0}services/data/v48.0/sobjects/opportunity", ConfigurationManager.AppSettings["SFRestUrl"]);

                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);

                request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", opp,ParameterType.RequestBody);

                var response = client.Execute(request);
                if (response.Content.Contains("Session expired or invalid"))
                {
                    GenerateSalesForceAccessToken();
                    return AddOpportunity(opp,AccountId);
                }
                dynamic resp = JObject.Parse(response.Content);
                return resp;
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }


        public void ProcessPurchaseByDistributorsForAccount(SFAccounts account, string url)
        {
            try
            {
                dynamic resp = GetPurchaseByDistributorsForAccount(account,url);
                dynamic rows = resp.factMap != null ? ((resp.factMap["T!T"]) != null ? (resp.factMap["T!T"]).rows : null): null;
                if (rows != null && Enumerable.Count(rows) > 0)
                {
                    foreach (var rowItem in rows)
                    {
                        dynamic dcl = rowItem.dataCells;
                        if (dcl != null && Enumerable.Count(dcl) >= 10)
                        {
                            var tg = Enumerable.Count(dcl);
                            var tsd = new SFTempSessionPurchaseByDistributors()
                            {
                                SessionKey = sessionKey,
                                AccountId = account.AccountId,
                                PartnerNumber = account.PartnerNumber,
                                ResellerAccountId = dcl[0].value,
                                ResellerAccountName = dcl[1].label,
                                ResellerDiscount = dcl[2].value,
                                ResellerPartnerType = dcl[3].value,
                                ResellerPartnerNumber = dcl[4].value,
                                CustomerName = dcl[5].label,
                                DistributorShortName = dcl[6].value,
                                SixMonthsSale = dcl[7].label,
                                TwelveMonthsSale = dcl[8].label,
                                TwentyFourMonthsSale = dcl[9].label,
                                SuggestedMarkup = dcl[10].label,
                                DistributorDiscount = Enumerable.Count(dcl) > 11 ? dcl[11].label : string.Empty,
                            };
                            _sessionPurchaseByDistributors.Add(tsd);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(resp.NextRecordsUrl))
                {
                    var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resp.NextRecordsUrl);
                    ProcessPurchaseByDistributorsForAccount(account, newUrl);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public object GetPurchaseByDistributorsForAccount(SFAccounts account, string url)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var filter = new
            {
                reportMetadata = new
                {
                    reportFilters = new []
                    {
                        new
                            {
                                column = ConfigurationManager.AppSettings["SFPurchaseByDistributorReportDistributorColumnName"],//"Purchases_By_Distributor__c.Distributor__c",
                                filterType = "fieldValue",
                                isRunPageEditable = true,
                                @operator = "contains",
                                value = account.AccountShortName
                            }
                    }
                }
            };
            var json = new JavaScriptSerializer().Serialize(filter);
            //var filter = new object();
            //var reportMetadata = new object();
            //var reportFilters = new object[1];
            //var f = new
            //{
            //    column = "Purchases_By_Distributor__c.Distributor__c",
            //    filterType = "fieldValue",
            //    isRunPageEditable = true,
            //    @operator = "equals",
            //    value = account.AccountShortName
            //};


            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetPurchaseByDistributorsForAccount(account,url);
            }
            dynamic resp = JObject.Parse(response.Content);
            return resp;

        }



        public void ProcessPurchaseByProductClassesForAccount(SFAccounts account, string url)
        {
            
            dynamic resp = GetPurchaseByProductClassesForAccount(account, url);
            if (resp != null)
            {
                dynamic rows = (resp.factMap != null) ? ((resp.factMap["T!T"]) != null ? (resp.factMap["T!T"]).rows : null) : null;
                if (rows != null && Enumerable.Count(rows) > 0)
                {
                    foreach (var rowItem in rows)
                    {
                        dynamic dcl = rowItem.dataCells;
                        if (dcl != null && Enumerable.Count(dcl) >= 7)
                        {
                            var tsd = new SFTempSessionPurchaseByProductClasses()
                            {
                                SessionKey = sessionKey,
                                AccountId = account.AccountId,
                                PartnerNumber = account.PartnerNumber,
                                AccountName = dcl[1].label,
                                ProductClass = dcl[3].label,
                                SixMonthsSale = dcl[4].label,
                                TwelveMonthsSale = dcl[5].label,
                                TwentyFourMonthsSale = dcl[6].label,
                                GrowthRate = dcl[7].value
                            };
                            _sessionPurchaseByProductClasses.Add(tsd);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(resp.NextRecordsUrl))
                {
                    var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resp.NextRecordsUrl);
                    ProcessPurchaseByProductClassesForAccount(account, newUrl);
                }
            }
        }

        public object GetPurchaseByProductClassesForAccount(SFAccounts account, string url)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var filter = new
            {
                reportMetadata = new
                {
                    reportFilters = new[]
                    {
                        new
                            {
                                column = ConfigurationManager.AppSettings["SFPurchaseByProductClassesReportFilterColumnName"],//"Account.Partner_Number__c",
                                filterType = "fieldValue",
                                isRunPageEditable = true,
                                @operator = "equals",
                                value = account.PartnerNumber
                            }
                    }
                }
            };
            var json = new JavaScriptSerializer().Serialize(filter);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetPurchaseByProductClassesForAccount(account, url);
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic resp = JObject.Parse(response.Content);
                return resp;
            }
            else
            {
                return null;
            }

        }



        public void ReloadSFMDFData(SFAccounts account)
        {
            var mdfUrl = string.Format(
            "{0}services/data/v48.0/analytics/reports/{1}", ConfigurationManager.AppSettings["SFRestUrl"], ConfigurationManager.AppSettings["SFMDFReportId"]);
            ProcessMdfForAccount(account, mdfUrl);

        }

        public void ProcessMdfForAccount(SFAccounts account, string url)
        {
            try
            {


                if (!string.IsNullOrEmpty(account.LoyaltyLevel))
                {
                    if (account.LoyaltyLevel.ToLower().Contains("gold") ||
                        account.LoyaltyLevel.ToLower().Contains("platinum") ||
                        account.LoyaltyLevel.ToLower().Contains("diamond"))
                   // if (!account.PartnerType.ToLower().Contains("distributor"))
                    {

                        _sessionMdfRepository.Delete(
                               x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);

                        dynamic resp = GetMdfForAccount(account, url);
                        dynamic rows = resp.factMap != null ? ((resp.factMap["T!T"]) != null ? (resp.factMap["T!T"]).rows : null) : null;
                        if (rows != null && Enumerable.Count(rows) > 0)
                        {
                            foreach (var rowItem in rows)
                            {
                                dynamic dcl = rowItem.dataCells;
                                if (dcl != null && Enumerable.Count(dcl) >= 7)
                                {
                                    var tsd = new SFTempSessionMdfData()
                                    {
                                        SessionKey = sessionKey,
                                        AccountId = account.AccountId,
                                        PartnerNumber = account.PartnerNumber,
                                        AccountName = dcl[1].label,
                                        MdfContact = dcl[2].label,
                                        MdfContactEmail = dcl[3].label,
                                        MdfId = dcl[4].value,
                                        MdfName = dcl[4].label,
                                        MdfAwardYear = dcl[5].label,
                                        MdfAwardQuarter = dcl[6].label,
                                        MdfAwardPriorYear = dcl[7].label,
                                        MdfAwardCurrentYear = dcl[8].label,
                                        MdfAwardPercent = dcl[9].label,
                                        MdfAwardDollar = dcl[10].label,
                                        MdfAwardTotalClaimed = dcl[11].label,
                                        MdfAwardBalance = dcl[12].label,
                                        MdfTotalAmount = dcl[13].label,
                                        MdfPaymentStatus = dcl[14].label
                                    };
                                    _sessionMdfRepository.Add(tsd);

                                    //var mdfReqUrl = string.Format(
                                    //    "{0}services/data/v48.0/query/?q=SELECT+Id,+Name,+CreatedDate,+MDF__c,+Amount__c,+Market_Activity_Description__c,+Project_Status__c,+Payment_Status__c+FROM+MDF_Request__c+WHERE+MDF__c+='{1}'", ConfigurationManager.AppSettings["SFRestUrl"], tsd.MdfId);
                                    //ProcessMdfRequestsForAccount(account, mdfReqUrl);
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(resp.NextRecordsUrl))
                        {
                            var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resp.NextRecordsUrl);
                            ProcessMdfForAccount(account, newUrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        public object GetMdfForAccount(SFAccounts account, string url)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var filter = new
            {
                reportMetadata = new
                {
                    reportFilters = new[]
                    {
                        new
                            {
                                column = ConfigurationManager.AppSettings["SFMDFReportFilterColumnName"],//"Account.Id",
                                filterType = "fieldValue",
                                isRunPageEditable = true,
                                @operator = "equals",
                                value = account.PartnerNumber
                            }
                    }
                }
            };
            var json = new JavaScriptSerializer().Serialize(filter);
            //var filter = new object();
            //var reportMetadata = new object();
            //var reportFilters = new object[1];
            //var f = new
            //{
            //    column = "Purchases_By_Distributor__c.Distributor__c",
            //    filterType = "fieldValue",
            //    isRunPageEditable = true,
            //    @operator = "equals",
            //    value = account.AccountShortName
            //};


            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetMdfForAccount(account, url);
            }
            dynamic resp = JObject.Parse(response.Content);
            return resp;

        }



        public void ProcessDelegateForAccount(SFAccounts account, string url)
        {
            dynamic resp = GetDelegateForAccount(account, url);
            if (resp != null)
            {
                dynamic rows = resp.factMap != null ? ((resp.factMap["T!T"]) != null ? (resp.factMap["T!T"]).rows : null) : null;
                if (rows != null && Enumerable.Count(rows) > 0)
                {
                    foreach (var rowItem in rows)
                    {
                        dynamic dcl = rowItem.dataCells;
                        if (dcl != null && Enumerable.Count(dcl) >= 5)
                        {
                            var tsd = new SFTempSessionDelegateData
                            {
                                SessionKey = sessionKey,
                                AccountId = account.AccountId,
                                PartnerNumber = account.PartnerNumber,
                                AccountName = account.AccountName,
                                TechnologyPartnerEPN = dcl[0].label,
                                DelegatePartnerEPN = dcl[1].label,
                                TechnologyPartnerAccountName = dcl[2].label,
                                DelegatePartnerAccountName = dcl[3].label,
                                DPN = dcl[4].value
                            };
                            _delegateReport.Add(tsd);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(resp.NextRecordsUrl))
                {
                    var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resp.NextRecordsUrl);
                    ProcessDelegateForAccount(account, newUrl);
                }
            }
        }

        public object GetDelegateForAccount(SFAccounts account, string url)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var filter = new
            {
                reportMetadata = new
                {
                    reportFilters = new[]
                    {
                        new
                            {
                                column = account.PartnerType.ToLower() == "technology partner"?"Delegate_Relationship__c.Technology_Partner_EPN__c":"Delegate_Relationship__c.Delegate_Partner_EPN__c",//"Account.Partner_Number__c",
                                filterType = "fieldValue",
                                isRunPageEditable = true,
                                @operator = "equals",
                                value = account.PartnerNumber
                            }
                    }
                }
            };
            var json = new JavaScriptSerializer().Serialize(filter);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetDelegateForAccount(account, url);
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic resp = JObject.Parse(response.Content);
                return resp;
            }
            else
            {
                return null;
            }
        }


        public void ProcessDelegateRebateItemForAccount(SFAccounts account, string url)
        {
            dynamic resp = GetDelegateRebateItemForAccount(account, url);
            if (resp != null)
            {
                dynamic rows = resp.factMap != null ? ((resp.factMap["T!T"]) != null ? (resp.factMap["T!T"]).rows : null) : null;
                if (rows != null && Enumerable.Count(rows) > 0)
                {
                    foreach (var rowItem in rows)
                    {
                        dynamic dcl = rowItem.dataCells;
                        if (dcl != null && Enumerable.Count(dcl) >= 5)
                        {
                            var tsd = new SFTempSessionDelegateRebateItemData
                            {
                                SessionKey = sessionKey,
                                AccountId = account.AccountId,
                                PartnerNumber = account.PartnerNumber,
                                AccountName = account.AccountName,
                                TechnologyPartnerEPN = dcl[0].label,
                                DelegatePartnerEPN = dcl[1].label,
                                TechnologyPartnerAccountName = dcl[2].label,
                                DelegatePartnerAccountName = dcl[3].label,
                                DPN = dcl[4].value,
                                PartNumber = dcl[5].label,
                                ItemName = dcl[6].value,
                                DelegateRebate = dcl[7].label,
                                DelegateRelationship = dcl[8].label,
                                SapCardCode = dcl[9].value,
                            };
                            _delegateRebateItemReport.Add(tsd);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(resp.NextRecordsUrl))
                {
                    var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resp.NextRecordsUrl);
                    ProcessDelegateForAccount(account, newUrl);
                }
            }
        }

        public object GetDelegateRebateItemForAccount(SFAccounts account, string url)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var filter = new
            {
                reportMetadata = new
                {
                    reportFilters = new[]
                    {
                        new
                            {
                                column = account.PartnerType.ToLower() == "technology partner"?"FK_Delegate_Relationship__c.Technology_Partner_EPN__c":"FK_Delegate_Relationship__c.Delegate_Partner_EPN__c",//"Account.Partner_Number__c",
                                filterType = "fieldValue",
                                isRunPageEditable = true,
                                @operator = "equals",
                                value = account.PartnerNumber
                            }
                    }
                }
            };
            var json = new JavaScriptSerializer().Serialize(filter);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetDelegateForAccount(account, url);
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic resp = JObject.Parse(response.Content);
                return resp;
            }
            else
            {
                return null;
            }
        }
        /*

        public void ProcessMdfRequestsForAccount(SFAccounts account, string url)
        {
            dynamic resMdfReq = GetMdfRequestsForAccount(url);
            if (resMdfReq.records != null && Enumerable.Count(resMdfReq.records) > 0)
            {
                foreach (var res in resMdfReq.records)
                {
                    var tsd = new SFTempSessionMdfRequestData
                    {
                        SessionKey = sessionKey,
                        AccountId = account.AccountId,
                        AccountName = account.AccountName,
                        PartnerNumber = account.PartnerNumber,
                        MdfRequestId = res.Id,
                        MdfRequestName = res.Name,
                        MdfId = res.MDF__c,
                        Amount = res.Amount__c,
                        ActivityDescription = res.Market_Activity_Description__c,
                        ProjectStatus = res.Project_Status__c,
                        PaymentStatus = res.Payment_Status__c,
                        CreatedDate = res.CreatedDate
                    };

                    _sessionMdfRequestRepository.Add(tsd);

                }
                if (!string.IsNullOrEmpty(resMdfReq.NextRecordsUrl))
                {
                    var newUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["SFRestUrl"], resMdfReq.NextRecordsUrl);
                    ProcessMdfRequestsForAccount(account, newUrl);
                }
            }
        }

        public object GetMdfRequestsForAccount(string url)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return GetMdfRequestsForAccount(url);
            }
            dynamic resp = JObject.Parse(response.Content);
            return resp;

        }

        */

        public object AddMdfRequest(dynamic opp)
        {
            var salesforce_auth = GetSalesforceAccessToken();


            var url = string.Format(
                "{0}services/data/v48.0/sobjects/{1}", ConfigurationManager.AppSettings["SFRestUrl"], ConfigurationManager.AppSettings["SFMDFRequestObjectName"]);

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", opp, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return AddMdfRequest(opp);
            }
            dynamic resp = JObject.Parse(response.Content);
            return resp;

        }

        public object AddLoyaltyLead(dynamic opp,string accountId)
        {
            var salesforce_auth = GetSalesforceAccessToken();
            
            var url = string.Format(
                "{0}services/data/v48.0/sobjects/{1}/{2}", ConfigurationManager.AppSettings["SFRestUrl"], ConfigurationManager.AppSettings["SFLoyaltyLeadObject"],accountId);

           // opp.Id = accountId;

            var client = new RestClient(url);
            var request = new RestRequest(Method.PATCH);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", opp, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return AddLoyaltyLead(opp,accountId);
            }
            try
            {
                if (response.Content == "") return null;
                dynamic resp = JObject.Parse(response.Content);
                return resp;
            }
            catch (Exception ex) { return null; }


        }

        public object AddDemoUnitRequest(dynamic opp, string accountId)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var url = string.Format(
                "{0}services/data/v48.0/sobjects/{1}", ConfigurationManager.AppSettings["SFRestUrl"], ConfigurationManager.AppSettings["SFDemoUnitObject"]);

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", opp, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return AddDemoUnitRequest(opp, accountId);
            }
            try
            {
                if (response.Content == "") return null;
                dynamic resp = JObject.Parse(response.Content);
                return resp;
            }
            catch (Exception ex) { return null; }


        }

        public object AddTrainingRequest(dynamic opp, string accountId)
        {
            var salesforce_auth = GetSalesforceAccessToken();

            var url = string.Format(
                "{0}services/data/v48.0/sobjects/{1}", ConfigurationManager.AppSettings["SFRestUrl"], ConfigurationManager.AppSettings["SFTrainingObject"]);

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("authorization", "Bearer " + salesforce_auth.access_token);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", opp, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.Content.Contains("Session expired or invalid"))
            {
                GenerateSalesForceAccessToken();
                return AddTrainingRequest(opp, accountId);
            }
            try
            {
                if (response.Content == "") return null;
                dynamic resp = JObject.Parse(response.Content);
                return resp;
            }
            catch (Exception ex) { return null; }


        }

    }
}