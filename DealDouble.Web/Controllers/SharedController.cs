using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class SharedController : Controller
    {
        SharedService service = new SharedService();

        List<object> picturesJSON = new List<object>();

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            var pictures = Request.Files;
            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var path = Server.MapPath("~/Content/images/") + fileName;

                var picUrl = "/Content/images/" + fileName;
                picture.SaveAs(path);
                
                var dbPicture = new Picture();
                dbPicture.URL = picUrl;

                int pictureID = service.SavePicture(dbPicture);
                picturesJSON.Add(new { ID = pictureID, pictureURL = fileName });
            }

            result.Data = picturesJSON;
            return result;
        }

        [HttpPost]
        public JsonResult LeaveComment(CommentViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (User.Identity.IsAuthenticated)
            {
                var comment = new Comment();

                comment.Body = model.Body;
                comment.EntityId = model.EntityId;
                comment.RecordId = model.RecordId;
                comment.UserId = User.Identity.GetUserId();
                comment.Timestamp = DateTime.Now;

                var commentResult = service.LeaveComment(comment);

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Please Login to proceed" };
            }

            return result;
        }
    }
}