using AutoMapper;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using EndecoDemo.Models.Member;
using EndecoDemo.Models.Stock;
using EndecoDemo.Models.ViewModels;
using EndecoDemo.Models.ViewModels.Stock;
using EndecoDemo.Services.Services;
using EndecoDemo2017.ViewModels.Response;
using log4net;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EndecoDemo2017.Controllers
{
    public class StockController : Controller
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IStockHeaderService _stockHeaderService;
        private readonly IStockDetailService _stockDetailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemberService _membersService;
        private int stockHeaderId = 0;
        private MemberModel member;
        private const string DATEFORMAT = "{0:ddd MMM d yyyy HH:mm:00 'GMT'zzz '(GMT Standard Time)'}";

        public StockController(IStockHeaderService stockHeaderService, IStockDetailService stockDetailService, IMemberService membersService, IUnitOfWork unitOfWork)
        {
            this._stockHeaderService = stockHeaderService;
            this._stockDetailService = stockDetailService;
            this._membersService = membersService;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public JsonResult GetStockHeaderList(int fetchCount = 10)
        {
            var response = new BaseDto<IEnumerable<StockHeaderModel>>();
            if (!User.Identity.IsAuthenticated)
            {
                response.Success = false;
                response.Message = "Please login to site";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    member = _membersService.GetMemberByEmail(this.User.Identity.Name);
                    var stockHeaders = _stockHeaderService.GetStockHeadersForMember(member.Id, fetchCount);

                    response.Success = true;
                    response.Message = "";
                    response.Data = stockHeaders;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    log.Error(e);
                    response.Success = false;
                    response.Message = e.Message;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public JsonResult GetStockDetails(int headerId = 0)
        {
            var response = new BaseDto<StockChartViewModel>();
            if (!User.Identity.IsAuthenticated)
            {
                response.Success = false;
                response.Message = "Please login to site";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try { 
                    if (headerId == 0)
                    {
                        member = _membersService.GetMemberByEmail(this.User.Identity.Name);
                        headerId = _stockHeaderService.GetLastStockHeaderForMember(member.Id).Id;
                    }
                }
                catch(Exception e)
                {
                    log.Error(e);
                    response.Success = false;
                    response.Message = "There was an error fetching member or stock header details";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                IEnumerable<StockDetailViewModel> detailsVM = null;
                IEnumerable<StockDetailModel> stockDetails = null;
                try
                {
                    stockDetails = _stockDetailService.GetStockDetailsForStockHeader(headerId);
                    detailsVM = Mapper.Map<IEnumerable<StockDetailViewModel>>(stockDetails);
                }
                catch (Exception e)
                {
                    log.Error(e);
                    response.Success = false;
                    response.Message = "There was an error fetching stock details";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                try
                {
                    StockChartViewModel scVm = new StockChartViewModel();
                    scVm.Stock = detailsVM;
                    scVm.MinPrice = detailsVM.Where(x => x.Value == detailsVM.Min(y => y.Value)).FirstOrDefault();
                    scVm.MaxPrice = detailsVM.Where(x => x.Value == detailsVM.Max(y => y.Value)).FirstOrDefault();

                    for (int i = 0; i < stockDetails.Count(); i++)
                    {
                        scVm.Stock.ElementAt(i).Date = String.Format(DATEFORMAT, stockDetails.ElementAt(i).Date);
                    }

                    var testggg = stockDetails.Where(x => x.Price == scVm.MinPrice.Value).FirstOrDefault().Date;

                    scVm.MinPrice.Date = stockDetails.Where(x => x.Price == scVm.MinPrice.Value).FirstOrDefault().Date.ToUniversalTime().ToString("o");
                    scVm.MaxPrice.Date = stockDetails.Where(x => x.Price == scVm.MaxPrice.Value).FirstOrDefault().Date.ToUniversalTime().ToString("o");
                    scVm.MostCostlyHour = stockDetails.ElementAt(GetMostExpensiveHour(detailsVM)).Date.ToUniversalTime().ToString("o");

                    response.Success = true;
                    response.Message = "";
                    response.Data = scVm;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    log.Error(e);
                    response.Success = false;
                    response.Message = "There was an error building chart data.";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
        }

        
        [HttpPost]
        public JsonResult UploadFile(object data)
        {
            var response = new BaseDto<string>();
            bool isUserAuthenticated = false;
            try
            {
                isUserAuthenticated = User.Identity.IsAuthenticated;
            }
            catch (Exception e)
            {
                log.Error(e);
                response.Success = false;
                response.Message = "There was a problem authenticating member";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (!isUserAuthenticated)
            {
                response.Success = false;
                response.Message = "Please login to site";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<StockDetailModel> sdList = new List<StockDetailModel>();
                try
                {
                    var queryString = Request.Form;
                    if (queryString.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "No data in QueryString";
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                    // Read parameters
                    var uploadToken = queryString.Get("upload_Token");
                    int resumableChunkNumber = int.Parse(queryString.Get("resumableChunkNumber"));
                    var resumableFilename = queryString.Get("resumableFilename");
                    var priceType = queryString.Get("pricetype");

                    byte[] fileDataArray = null;
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        fileDataArray = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                        string chunkString = Encoding.ASCII.GetString(fileDataArray, 0, fileDataArray.Length);

                        if (resumableChunkNumber > 1)
                        {
                            stockHeaderId = int.Parse(Session["stockHeaderId"].ToString());
                            if (Session["partialRecord"] != null)
                            {
                                chunkString = Session["partialRecord"].ToString() + chunkString;
                            }
                        }
                        var elements = chunkString.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

                        //remove first line if contains word "date" or "price"
                        if (elements.Count() > 0 && resumableChunkNumber == 1)
                        {
                            if (elements[0].ToLower().Contains("date") || elements[0].ToLower().Contains("price"))
                            {
                                elements.RemoveAt(0);
                            }
                        }

                        if (resumableChunkNumber == 1)
                        {
                            //if this is the first chunk then get member details 
                            //and create a stockheader
                            member = _membersService.GetMemberByEmail(this.User.Identity.Name);
                            StockHeaderModel skhr = new StockHeaderModel()
                            {
                                MemberId = member.Id,
                                DateUploaded = DateTime.Now,
                                StockType = (priceType != null) ? priceType : "ex",
                                FileNameUploaded = resumableFilename
                            };
                            stockHeaderId = _stockHeaderService.CreateStockHeader(skhr);
                            Session["stockHeaderId"] = stockHeaderId;
                        }
                        //as the file is uploaded in chunks some lines will be partial
                        //last line of previous chunk needs to added to first line of this chunk
                        foreach (string item in elements)
                        {
                            if (item.Contains("\r"))
                            {
                                var valArray = item.Split(new[] { ',' });
                                sdList.Add(new StockDetailModel() { Date = parseDate(valArray[0]), Price = parsePrice(valArray), StockHeaderId = stockHeaderId });
                                Session["partialRecord"] = "";
                            }
                            else
                            {
                                Session["partialRecord"] = item;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    log.Error(e);
                    response.Success = false;
                    response.Message = "There was an error reading file data or creating the stock header record";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                try
                {
                    _stockDetailService.CreateStockDetails(sdList);
                    _stockDetailService.CommitStockDetail();
                }
                catch(Exception e)
                {
                    log.Error(e);
                    response.Success = false;
                    response.Message = "There was an error saving stock details data.";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            
            response.Success = true;
            response.Message = "Data has been saved successfully";
            return Json(response, JsonRequestBehavior.AllowGet);
        } 

        private DateTime parseDate(string dateString)
        {
            var date = DateTime.Today;
            DateTime.TryParseExact(dateString, new[] { "dd/MM/yyyy", "dd/MM/yyyy HH:mm" }, null, DateTimeStyles.None, out date);
            return date;
        }

        private decimal parsePrice(string[] recordArray)
        {
            decimal price = 0;
            if (recordArray.Length > 1)
            {
                decimal.TryParse(recordArray[1], out price);
            }
            return price;
        }

        private int GetMostExpensiveHour(IEnumerable<StockDetailViewModel> stocks)
        {
            int indexMax = 0;
            decimal currentMax = 0;
            if (stocks.Count() > 1)
            {
                for (int i = 1; i < stocks.Count(); i++)
                {
                    if ((stocks.ElementAt(i - 1).Value + stocks.ElementAt(i).Value) > currentMax)
                    {
                        currentMax = stocks.ElementAt(i - 1).Value + stocks.ElementAt(i).Value;
                        indexMax = i;
                    }
                }
            }
            return indexMax;
        }
    }
}