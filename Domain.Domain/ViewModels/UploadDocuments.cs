using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class UploadDocuments
    {
        public int StudentId { get; set; }
        public int DocumentId { get; set; }
        public List<IFormFile> Files { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
