using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestTask.Shortener.BE;
using TestTask.Shortener.BL;

namespace Shortener.Web.Controllers
{
    public class ShortenerDataController : ApiController
    {
        private readonly IShortenerBL ShortenerLogic;
        public ShortenerDataController(IShortenerBL shortenerLogic)
        {
            ShortenerLogic = shortenerLogic;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> CreateShortenerUserId()
        {
            Guid id;
            try
            {
                var user  = await ShortenerLogic.GetUser(Guid.NewGuid());
                id = user.StorageId;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            }
            return new HttpResponseMessage()
            {
                Content = new StringContent(id.ToString()),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpGet]
        public async Task<HttpResponseMessage> CreateShortenLink([FromUri]ShortenLinkData model)
        {
            ShortenerUserLink link = null;
            ShortenLinkResponseData result;
            Uri uriResult;
            bool isUri = Uri.TryCreate(model.OriginalLink, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            try
            {
                if(isUri)
                    link = await ShortenerLogic.GetShortenerUserLink(model.UserId, model.OriginalLink);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            result = new ShortenLinkResponseData
            {
                IsSuccess = isUri,
                Link = link
            };

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(result))
            };
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetLinks([FromUri]LinksQueryData model)
        {
            PagedResults<ShortenerUserLinkData> result = null;
            try
            {
                //TODO Exceptions Handling
                var dataQuery = await ShortenerLogic.GetUserLinks(model.UserId, model.PageNumber, model.PageSize);
                result = new PagedResults<ShortenerUserLinkData>()
                {
                    PageNumber = dataQuery.Page,
                    PageSize = dataQuery.PageSize,
                    TotalNumberOfPages = dataQuery.PageCount,
                    TotalNumberOfRecords = dataQuery.TotalCount,
                    Results = dataQuery.Select(
                        x =>
                        new ShortenerUserLinkData
                        {
                            UserId = x.UserId,
                            OriginalLink = x.OriginalLink,
                            ShortLink = x.ShortLink,
                            ClickCount = x.ClickCount,
                            CreateDate = x.CreateDate.ToShortDateString()
                            
                        }).ToList()
                };
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { result = result, IsSuccess = false }))
                };
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(result))
            };
        }

    }

    public class LinksQueryData
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class PagedResults<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalNumberOfPages { get; set; }

        public int TotalNumberOfRecords { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
    public class ShortenLinkResponseData
    {
        public bool IsSuccess { get; set; }
        public ShortenerUserLink Link { get; set; }

    }


    public class ShortenerUserLinkData
    {
        public Int32 UserId { get; set; }
        public String OriginalLink { get; set; }
        public String ShortLink { get; set; }
        public Int32 ClickCount { get; set; }
        public String CreateDate { get; set; }
    }

    public class ShortenLinkData
    {
        public Guid UserId { get; set; }
        public string OriginalLink { get; set; }
    }

}
