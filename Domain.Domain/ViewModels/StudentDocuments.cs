using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class StudentDocuments
    {
        public List<IFormFile> Files { get; set; }
        public string FileName { get; set; }
        public List<string> FilePath { get; set; }
    }
}
