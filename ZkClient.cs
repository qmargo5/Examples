using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using ZakKonClient.Models;
using HtmlAgilityPack;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace ZakKonClient
{
    /// <summary>
    /// Клиент для Контур.Закупок.
    /// </summary>
    public class ZkClient
    {
        public string[] startEndDate = File.ReadAllText("D:/ZakKonClientWB/ZakKonClient/bin/Release/StartEndDate.txt").Split('|');
        public DateTime publishDate = DateTime.Now;
        private readonly HttpClient httpClient = new HttpClient();
        public CookieContainer cookieContainer = new CookieContainer();
        private bool isAuthorized = false;
        public DateTime startDate = DateTime.Today;
        public DateTime endDate = DateTime.Today;
        private int numberPurchaces;
        public const string dealineTimeTitle_1 = "Окончание подачи заявок";
        public const string dealineTimeTitle_2 = "Подача заявки";
        public const string auctionTimeTitle1 = "Проведение аукциона";
        public const string auctionTimeTitle2 = "Время начала срока подачи ценовых предложений";
        public string KOTIROVKA = "котиров";
        public string AUCTION = "аукцион";

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ZkClient()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer
            };
            startDate = startDate.AddDays(Int32.Parse(startEndDate[0]));
            endDate = endDate.AddDays(Int32.Parse(startEndDate[1]));
            if (startEndDate.Count() == 3)
                publishDate = Convert.ToDateTime(startEndDate[2]);
            this.httpClient = new HttpClient(handler);

        }

        /// <summary>
        /// Авторизоваться.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Успешна ли авторизация.</returns>
        public bool Auth(string login, string password)
        {
            var csrfToken = GetCsrfToken();
            httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", csrfToken);

            var payload = new
            {
                Login = login,
                Password = password,
                Remember = false
            };
            var response = httpClient
                .PostAsync("https://auth.kontur.ru/api/authentication/password/auth-by-password",
                new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"))
                .Result;
            var returnedContent = response.Content
                .ReadAsStringAsync()
                .Result;

            isAuthorized = returnedContent == "{}";
            GetPurchases();
            return isAuthorized;
        }

        /// <summary>
        /// Получить закуки с указанной страницы с выбором даты и времени начала аукциона и срока подачи заявки
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <returns>Массив с закупками.</returns>
        public List<PurchaseModel> GetPurchases(int pageNumber, List<string> oldPurchases)
        {
            if (!isAuthorized)
                throw new Exception("Перед этим действием необходимо авторизоваться");

            var query = new Dictionary<string, object>
            {
                { "Query.PublishDateFrom", startDate.ToShortDateString() },
                { "Query.PublishDateTo", endDate.ToShortDateString() },
                { "Query.PurchaseStatuses", "1" },
                { "Query.PageNumber", pageNumber },
                { "Query.SortOrder", "0" },
                { "templateId", "4124c41c8dd04abaaf8dd8f27e48ac5b" },
                { "cacheReset", "false"}
            };

            var jsonQueryString = JsonConvert.SerializeObject(query);
            var queryContent = new StringContent(jsonQueryString, Encoding.UTF8, "application/json");

            var response = httpClient
                .PostAsync("https://zakupki.kontur.ru/grid/templates/api", queryContent)
                .Result;

            var result = response.Content.ReadAsStringAsync().Result;
            var temproot = JsonConvert.DeserializeObject<GetPurchasesResultModel>(result);
            var root = JsonConvert.DeserializeObject<GetPurchasesResultModel>(result);
            root.Purchases.Clear();

            foreach (var purchase in temproot.Purchases)
            {
                if (!oldPurchases.Contains(purchase.NotificationId))
                {
                    GetTendersInfoAsync(purchase);
                    root.Purchases.Add(purchase);
                }
            }
            if (pageNumber == 0)
            {
                numberPurchaces = Convert.ToInt32(Math.Ceiling(root.Total / 50.0));
                Console.OutputEncoding = Encoding.GetEncoding(1251);
                Console.WriteLine(DateTime.Now.ToString() + " Tender count for seven days: " + root.Total + '\n');
            }
            return root.Purchases;
        }

        /// <summary>
        /// Получить закуки по запросу с первой страницы
        /// </summary>
        /// <returns>Массив с закупками</returns>
        public List<PurchaseModel> GetPurchases()
        {
            if (!isAuthorized)
                throw new Exception("Перед этим действием необходимо авторизоваться");

            var query = new Dictionary<string, object>
            {
                { "Query.PublishDateFrom", startDate.ToShortDateString() },
                { "Query.PublishDateTo", endDate.ToShortDateString() },
                { "Query.PurchaseStatuses", "1" },
                { "Query.PageNumber", "0" },
                { "Query.SortOrder", "0" },
                { "templateId", "4124c41c8dd04abaaf8dd8f27e48ac5b" },
                { "cacheReset", "false"}
            };
            var jsonQueryString = JsonConvert.SerializeObject(query);
            var queryContent = new StringContent(jsonQueryString, Encoding.UTF8, "application/json");

            var response = httpClient
                .PostAsync("https://zakupki.kontur.ru/grid/templates/api", queryContent)
                .Result;

            var result = response.Content.ReadAsStringAsync().Result;
            var root = JsonConvert.DeserializeObject<GetPurchasesResultModel>(result);
            return root.Purchases;

        }

        /// <summary>
        /// Получить все документы (вместе с содержимым), прикрепленные к закупке.
        /// </summary>
        /// <param name="purchase">Закупка.</param>
        /// <returns>Массив с документами.</returns>
        public GetDocsResultModel GetDocuments(PurchaseModel purchase)
        {
            if (!isAuthorized)
                throw new Exception("Перед этим действием необходимо авторизоваться");

            var response = httpClient.GetAsync("https://zakupki.kontur.ru/" + purchase.PurchaseUrl).Result;
            var pageContent = response.Content.ReadAsStringAsync().Result;
            var jsonWithDocs = Regex.Match(pageContent, @"new PurchasePageView\((\{.*?\})\);").Groups[1].Value;
            var docsRootModel = JsonConvert.DeserializeObject<GetDocsResultModel>(jsonWithDocs);
            var docs = docsRootModel.Attachments;

            foreach (DocModel docModel in docs)
            {
                try
                {
                    var trueFileLink = docModel.KonturDriveLink ?? docModel.OosFileLink; // Иногда ссылка на КонтурДиск пуста
                    docModel.FileRawContent = httpClient.GetByteArrayAsync(trueFileLink).Result;
                }
                catch (Exception)
                {
                    // Например, если файл не найден
                    docModel.FileRawContent = new byte[0];
                }
            }
            docsRootModel.Attachments = docs;
            return docsRootModel;
        }

        /// <summary>
        /// Получить подробную информацию по конкурсу
        /// </summary>
        /// <param name="purchase">Закупка.</param>
        public void GetTendersInfoAsync(PurchaseModel purchase)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            // целевой сайт
            string url = "https://zakupki.kontur.ru/" + purchase.PurchaseUrl;
            List<string> dateTimeAtridutes = new List<string>();

            var result = String.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    StreamReader streamReader;
                    if (response.CharacterSet != null)
                        streamReader = new StreamReader(responseStream, Encoding.GetEncoding(response.CharacterSet));
                    else
                        streamReader = new StreamReader(responseStream);
                    result = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                response.Close();
            }
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result);
            //Получение времени и ЧП (время там теперь всегда по мск) окночания подачи заявки (если оно есть)
            //Здесь смотреть тег tender_endDateFull, но если его нет провеять по старой схеме.
            try
            {
                string text__time;
                var tenderField_title = doc.DocumentNode.SelectNodes("//dl[@class='clearfix tenderField tenderField__m']").
                    FirstOrDefault(x => x.SelectSingleNode("dt[@class='tenderField_title']").InnerText == dealineTimeTitle_1
                    || x.SelectSingleNode("dt[@class='tenderField_title']").InnerText == dealineTimeTitle_2);
                //                var tenderField_title = doc.DocumentNode.SelectNodes("//dt[@class='tenderField_title']").FirstOrDefault(x => x.InnerText == dealineTimeTitle_1 || x.InnerText == dealineTimeTitle_2);
                if (tenderField_title != null)
                {
                    var tender_endDateFull = tenderField_title.SelectSingleNode(".//span[@class='tender_endDateFull']");
                    if (tender_endDateFull != null)
                    {
                        var text__time_node = tender_endDateFull.SelectSingleNode(".//span[@class='text__time']");
                        text__time = text__time_node.InnerText;
                    }
                    else
                    {
                        var text__time_node = tenderField_title.SelectSingleNode(".//span[@class='text__time']");
                        text__time = text__time_node.InnerText;
                    }
                    purchase.ApplicationDeadlineTime = text__time;
                }

            }
            catch (Exception ex)
            {

            }

            //Получение времени аукциона
            try
            {
                string text__time;
                var tenderField_title = doc.DocumentNode.SelectNodes("//dl[@class='clearfix tenderField tenderField__m']").
                    FirstOrDefault(x => x.SelectSingleNode("dt[@class='tenderField_title']").InnerText == auctionTimeTitle1);
                if (tenderField_title != null)
                {
                    var tender_date = tenderField_title.SelectSingleNode(".//span[@class='tender_date']").InnerText;
                    purchase.AuctionStart = tender_date;
                    text__time = tenderField_title.SelectSingleNode(".//span[@class='text__time']").InnerText;
                    purchase.AuctionStartTime = text__time;
                }
                else
                {
                    tenderField_title = doc.DocumentNode.SelectNodes("//dl[@class='clearfix tenderField tenderField__m']").
                                        FirstOrDefault(x => x.SelectSingleNode("dt[@class='tenderField_title']").InnerText == auctionTimeTitle2);
                    if (tenderField_title != null)
                    {
                        var tender_date_time = tenderField_title.SelectSingleNode(".//div[@class='tenderField_data_in']").InnerText.Replace('\r', ' ').Replace('\n', ' ').Trim();
                        string[] tender_date_time_arr = tender_date_time.Split(new string[] { "&nbsp;" }, StringSplitOptions.RemoveEmptyEntries);
                        var tender_date = tender_date_time_arr[0];
                        purchase.AuctionStart = tender_date;
                        //text__time = tenderField_title.SelectSingleNode(".//span[@class='text__time']").InnerText;
                        purchase.AuctionStartTime = tender_date_time_arr[1];
                    }
                }
            }
            catch (Exception)
            {

            }

            try
            {
                //Получим ИНН
                try
                {
                    var tender_customer = doc.DocumentNode.SelectSingleNode("//p[@class='tender_subtitle__s']");
                    var sibling = tender_customer.NextSibling; //Переход к текстовому тегу
                    var INN_KPP_node = sibling.NextSibling.ChildNodes.FirstOrDefault(x => x.Name == "dd"); //Переход к тегу с ИНН и КПП
                    purchase.INN_KPP = INN_KPP_node.InnerText.ToString().Trim();

                }
                catch (Exception e)
                {
                    //Обработка ошибок не нужна будет, т.к. сюда заходим, если нет заказчика
                    //Но сейчас лучше потестить
                    ////string Err = "Error INN_KPP \n" + DateTime.Now + " " + e.Message + '\n' + e.StackTrace + '\n';//purchase.PurchaseUrl + "\n" +
                    ////Console.WriteLine(Err);
                }

                //Получим Объекты закупки
                try
                {
                    var table = doc.DocumentNode.SelectSingleNode("//table[@class='purchaseInfo']");
                    purchase.PurchaseTable = new List<string>();
                    foreach (HtmlNode row in table.SelectNodes(".//tr"))
                    {
                        //Console.WriteLine("row");
                        string allRow = "";
                        foreach (HtmlNode cell in row.SelectNodes(".//th|td"))
                        {
                            var currsell = Regex.Replace(HtmlEntity.DeEntitize(cell.InnerText.Trim()), @"( |\t|\r?\n)\1+", "");
                            var parts = Regex.Split(currsell, "(\r|\n|\t)", RegexOptions.Multiline).Where(x => x.Count() > 0).Select(x => x.Trim());
                            var joined = String.Join("; ", parts);
                            joined = joined.Replace("; ; ", "");
                            allRow = allRow + joined + "|";
                        }
                        purchase.PurchaseTable.Add(allRow);
                    }

                }
                catch (Exception e)
                {

                }
                var link = doc.GetElementbyId("headerSourceLink").GetAttributeValue("href", "");
                purchase.Ep.Url = link;
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Получить все закупки по запросу
        /// </summary>
        /// <returns>Лист закупок</returns>
        public List<PurchaseModel> GetAllPurchases(List<string> oldPurchases)
        {
            List<PurchaseModel> tempPurchases = GetPurchases(0, oldPurchases).ToList<PurchaseModel>();
            List<PurchaseModel> root = tempPurchases;

            for (int i = 1; i < numberPurchaces; i++)
            {
                tempPurchases = GetPurchases(i, oldPurchases).ToList<PurchaseModel>();
                root.AddRange(tempPurchases);
            }
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.WriteLine(DateTime.Now.ToString() + "Count of new tenders: " + root.Count() + '\n');
            return root;
        }

        /// <summary>
        /// Получение токена
        /// </summary>
        /// <returns></returns>
        private string GetCsrfToken()
        {
            var a = httpClient.GetAsync("https://auth.kontur.ru/").Result;
            var token = cookieContainer.GetCookies(new Uri("https://auth.kontur.ru"))["AntiForgery"].Value;

            return token;
        }
    }
}
