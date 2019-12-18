using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class Documents
    {
        public string DocumentFile { get; set; }
        public string DocumentTypeCode { get; set; }
        public string DocumentName { get; set; }
        public string DisplayName { get; set; }
        public string DocumentExtension { get; set; }
        public string DocumentPath { get; set; }
        public byte[] DocumentByte { get; set; }
        public bool SecureDocument { get; set; }
        public uint AuctionId { get; set; }
        public bool PrimaryDocument { get; set; }
    }
}
