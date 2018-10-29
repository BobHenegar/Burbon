using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace ProjectMoonshine.ViewModels
{
    public class FileUploadViewModel
    {
        [DisplayName("Description")]
        public string FupFileDescription { get; set; }

        [DisplayName("Document Type")]
        public Guid? FupDtpKey { get; set; }
    }
}
