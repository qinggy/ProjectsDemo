using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FileUpload.WebApi.Controllers
{
    public class UploadController : ApiController
    {
        public async Task<HttpResponseMessage> Post()
        {
            // Check whether the POST operation is MultiPart?
            if (!Request.Content.IsMimeMultipartContent()) throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            // Prepare CustomMultipartFormDataStreamProvider in which our multipart form
            // data will be loaded.
            string serverSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(serverSaveLocation);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(System.IO.Path.Combine(file.LocalFileName));
                }

                // Send OK Response along with saved file names to the client.
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }
}
