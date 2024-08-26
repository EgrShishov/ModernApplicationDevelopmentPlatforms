using System.Web.Mvc;

namespace WEB_253505_Shishov.Extensions;

public static class HttpRequestExtensions
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == "XMLHttpRequest"; //IsAjaxRequst()? missing
    }
}
