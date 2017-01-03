namespace LpsServer.Controllers
{
    using Lps.Contracts.ViewModel.Files;
    using Lps.Services.Helper;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    [RoutePrefix("api/files")]
    public class FilesController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        [Route("SetMainPhoto")]
        public IHttpActionResult SetMainPhoto(SetMainPhotoRequest setMainPhotoRequest)
        {
            PhotoDataResponse photoDataResponse = Repository.SetMainPhoto(setMainPhotoRequest);

            return Ok(photoDataResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("SetUserMainPhoto")]
        public IHttpActionResult SetUserMainPhoto(SetMainPhotoBase setMainPhotoBase)
        {
            PhotoDataResponse photoDataResponse = Repository.SetUserMainPhoto(setMainPhotoBase);

            return Ok(photoDataResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("DeletePhotoFromRoom")]
        public IHttpActionResult DeletePhotoFromRoom(DeletePhotoFromRoomRequest deletePhotoFromRoomRequest)
        {
            PhotoDataResponse photoDataResponse = Repository.DeletePhotoFromRoom(deletePhotoFromRoomRequest);

            return Ok(photoDataResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("DeletePhotoFromUser")]
        public IHttpActionResult DeletePhotoFromUser(DeletePhotoBase deletePhotoBase)
        {
            PhotoDataResponse photoDataResponse = Repository.DeletePhotoFromUser(deletePhotoBase);

            return Ok(photoDataResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("AddPhotoToRoom")]
        public IHttpActionResult AddPhotoToRoom(PhotoDataRequest photoDataRequest)
        {
            PhotoDataResponse photoDataResponse = Repository.AddPhotoToRoom(photoDataRequest);            

            return Ok(photoDataResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("AddPhotoToUser")]
        public IHttpActionResult AddPhotoToUser(PhotoDataRequest photoDataRequest)
        {
            PhotoDataResponse photoDataResponse = Repository.AddPhotoToUser(photoDataRequest);

            return Ok(photoDataResponse);
        }

        [HttpPost]
        [Route("Upload")]        
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            List<string> files = new List<string>();

            try
            {                
                var root = HttpContext.Current.Server.MapPath("~/FileUploads");                

                var streamProvider = this.GetMultipartProvider(root);

                // Read the MIME multipart content using the stream provider we just created.
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                foreach (MultipartFileData file in streamProvider.FileData)
                {                                        
                    string mediumFullPath = 
                        Path.Combine(Path.Combine(root, "medium"), Path.GetFileName(file.LocalFileName));
                    ImageTools.ResizeWithCut(file.LocalFileName, mediumFullPath, 600, 600);
                    
                    string thumbFullPath = 
                        Path.Combine(Path.Combine(root, "thumb"), Path.GetFileName(file.LocalFileName));
                    ImageTools.ResizeWithCut(file.LocalFileName, thumbFullPath, 150, 150);                    

                    files.Add(file.LocalFileName);
                }

                // Send OK Response along with saved file names to the client. 
                return Request.CreateResponse(HttpStatusCode.OK, Path.GetFileName(files.First()));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private CustomMultipartFormDataStreamProvider GetMultipartProvider(string root)
        {
            return new CustomMultipartFormDataStreamProvider(root);
        }

        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public CustomMultipartFormDataStreamProvider(string path)
                : base(path)
            { }

            public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                //var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName)
                //    ? Guid.NewGuid() + "_" + headers.ContentDisposition.FileName
                //    : "NoName";

                string tempName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                string fileExtension = Path.HasExtension(tempName) ? Path.GetExtension(tempName) : ".jpg";

                return Guid.NewGuid() + fileExtension;                
            }
        }
    }    
}