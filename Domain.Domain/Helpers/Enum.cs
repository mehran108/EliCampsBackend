using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.Helpers
{
    public enum EmailTemplate
    {
        ResetSuccessfully = 1,
        ForgetPasswordEmail = 2,
        Welcome = 3,
        BulkCodeAccount = 4
    }

    public enum LookupValueEnum
    {
        ImgSrcPath = 1,
        EncryptionKey = 2,
        region_url = 3,
        CompanyTel = 4,
        CompanyAddress = 5,
        PurchaseLink = 6,
        ResetUrl = 7,
        DefaultGateway = 9,
        DiscountTypePercentage = 12,
        DiscountTypeFixed = 13
    }
}
