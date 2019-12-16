using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class StudentDocuments
    {
        public int StudentId { get; set; }
        public List<IFormFile> Files { get; set; }
        public string FileName { get; set; }
        public List<string> FilePath { get; set; }
    }
}
