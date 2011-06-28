using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace FileUploader_Demo.Controllers
{
    public class UploadInfo
    {

        private static UploadInfo _upload;
        private UploadInfo()
        {
            this.Upload_dir = "~/uploaded_files";
        }

        public static UploadInfo getUploadInfo()
        {
            if (UploadInfo._upload == null)
                UploadInfo._upload = new UploadInfo();

            return _upload;
        }
        private string param_name = "file";
        private int? max_file_size = null;

        [DefaultValue(null)]
        public int? Max_file_size
        {
            get { return max_file_size; }
            set { max_file_size = value; }
        }
        private int min_file_size = 1;

        [DefaultValue(1)]
        public int Min_file_size
        {
            get { return min_file_size; }
            set { min_file_size = value; }
        }
        private string accept_file_types = "/.+$/";

        [DefaultValue("/.+$/")]
        public string Accept_file_types
        {
            get { return accept_file_types; }
            set { accept_file_types = value; }
        }
        private int? max_number_of_files = null;

        [DefaultValue(null)]
        public int? Max_number_of_files
        {
            get { return max_number_of_files; }
            set { max_number_of_files = value; }
        }
        private Boolean discard_aborted_uploads = true;

        [DefaultValue(true)]
        public Boolean Discard_aborted_uploads
        {
            get { return discard_aborted_uploads; }
            set { discard_aborted_uploads = value; }
        }

        public string ScriptURL { get; set; }
        public string Upload_dir { get; set; }



        [DefaultValue("file")]
        public string Param_name
        {
            get
            {
                return this.param_name;
            }

            set
            {
                this.param_name = value;
            }
        }

    }

    /*
     * Note: Properties in this class start with small letters to 
     * follow Json format required by FileUpload component
     */
    public class FileUploadInfo
    {

        public static string TYPE_DELETE = "DELETE";
        public string name { set; get; }
        public long size { set; get; }
        public string url { set; get; }
        public string delete_url { set; get; }
        public string delete_type { set; get; }
        public string type { set; get; }
        public string error { set; get; }

    }


}