using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileUploadProject.WebAPI
{
    public class CheckMimeMultipartContent : ActionFilterAttribute
    {
        public CheckMimeMultipartContent()
        {
            Order = 1;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!IsMultipartContentType(context.HttpContext.Request.ContentType))
            {
                context.Result = new StatusCodeResult(415);
                return;
            }
            base.OnResultExecuting(context);
        }
        private bool IsMultipartContentType(string contentType)
        {
            if (!string.IsNullOrEmpty(contentType) && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0)
                return true;
            else
                return false;

        }
    }
}
