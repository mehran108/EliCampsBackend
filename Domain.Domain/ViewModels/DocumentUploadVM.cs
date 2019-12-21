using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class DocumentUploadVM
    {
        public List<IFormFile>  Files { get; set; }
    }
}
