using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FileUploader_Demo.Models;

namespace FileUploader_Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Sample File Uploader using jquery file uploader and MVC 3";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult uploader()
        {

            switch (Request.HttpMethod)
            {
                case "POST":
                    return uploader_Post();

          
                case "GET":
                    return this.uploader_Get();

            }

            return Json(true);

        }

        public ActionResult uploader_Get()
        {
            String fileName = Request.Params["file"];

            if (fileName == null)
            {
                return get_file_objects();
            }
            else
            {
                fileName = fileName.Replace("/", "");
                return Json(true);

            }

        }

        public ActionResult uploader_Post()
        {
            var files = Request.Files;
            List<FileUploadInfo> result = new List<FileUploadInfo>();
            for (int i = 0; i < files.Count; i++)
            {
                result.Add(this.handle_file_upload(files.Get(i)));
            }
            return Json(result);
        }


        private FileUploadInfo handle_file_upload(HttpPostedFileBase file)
        {
            FileUploadInfo info = new FileUploadInfo();
            if (file == null)
                throw new NullReferenceException("No file is passed");

            //Save the file first 
            String filename = Server.MapPath(UploadInfo.getUploadInfo().Upload_dir + "/" +
                file.FileName);
            file.SaveAs(filename);
            // Set the info 
            info.name = file.FileName;
            info.size = file.ContentLength;
            info.type = file.ContentType;
            info.url = Url.Content(UploadInfo.getUploadInfo().Upload_dir + "/" + file.FileName);
            info.delete_url = Url.Content("~/Home/uploader_Delete" + "/" + file.FileName);
            info.delete_type = FileUploadInfo.TYPE_DELETE;
            return info;

        }


        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult uploader_Delete(string id)
        {
            String fileName = id;
            if (!String.IsNullOrEmpty(id))
            {
                try
                {
                    FileInfo f = new FileInfo(Server.MapPath(UploadInfo.getUploadInfo().Upload_dir
                        + "/" + fileName));
                    f.Delete();
                }
                catch (Exception ex)
                {
                    return Json(false);
                }
                return Json(false);
            }

            return Json(false);

        }


        private JsonResult get_file_objects()
        {
            String[] files = Directory.GetFiles(Server.MapPath((UploadInfo.getUploadInfo().Upload_dir)));

            List<FileUploadInfo> fileList = new List<FileUploadInfo>();
            for (int i = 0; i < files.Length; i++)
            {
                fileList.Add(this.get_file_object(files[i]));
            }

            return Json(fileList, JsonRequestBehavior.AllowGet);

        }

        private FileUploadInfo get_file_object(string p)
        {
            FileUploadInfo result = new FileUploadInfo();
            try
            {
                FileInfo inf = new FileInfo(p);
                result.name = inf.Name;
                result.delete_type = FileUploadInfo.TYPE_DELETE;
                result.size = inf.Length;
                result.delete_url = Url.Content("~/Home/uploader_Delete" + "/" + inf.Name);
                result.url = Url.Content(UploadInfo.getUploadInfo().Upload_dir + "/" + inf.Name);
            }
            catch (Exception ex)
            {
                result.error = "Cannot find the file";
            }

            return result;
        }



    }
}
