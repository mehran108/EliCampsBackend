using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ELI.Domain.Helpers;
using ELI.Domain.Contracts.Auth;
using ELI.Domain.Contracts.Main;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;
using SelectPdf;

namespace ELI.Domain.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IELIService _ELIService;
        private IConfiguration _configuration { get; }
        private IUserRepository _userRepository { get; }
        private ILookupTableRepository _lookupTableRepository { get; }
        string RegionId;
        string AuthConString;
        string ConString;
        string DefaultPassword;

        private const string RequesterNameTag = "{{EmailBody}}";

        private const string CurrentDateTag = "{{CurrentDate}}";
        private const string StudentFullNameTag = "{{StudentFullName}}";
        private const string AgentNameTag = "{{AgentName}}";
        private const string AgentAddressTag = "{{AgentAddress}}";
        private const string AgentCountryTag = "{{AgentCountry}}";
        private const string DOBTag = "{{DOB}}";
        private const string ProgrameStartDateTag = "{{ProgrameStartDate}}";
        private const string ProgrameEndDateTag = "{{ProgrameEndDate}}";
        private const string CampusAddressOnReportsTag = "{{CampusAddressOnReports}}";
        private const string ProgramNameTag = "{{ProgramName}}";
        private const string SubProgramNameTag = "{{SubProgramName}}";
        private const string FormatNameTag = "{{FormatName}}";
        private const string MealPlanTag = "{{MealPlan}}";
        private const string TotalGrossPriceTag = "{{TotalGrossPrice}}";
        private const string CommissionAddinsTag = "{{CommissionAddins}}";
        private const string CommisionTag = "{{Commision}}";
        private const string PaidTag = "{{Paid}}";
        private const string BalanceTag = "{{Balance}}";
        private const string Reg_RefTag = "{{Reg_Ref}}";
        private const string AdditionalServices_Tag = "{{Additional_Services}}";
        private const string IncludedServicesTag = "{{Included_Services}}";
        private const string NetPriceTag = "{{NetPrice}}";



        string StudentRegEmailBody = @"<div lang=""en-US"" link=""blue"" vlink=""purple"">
    <div>
        <div>
        </div>
        <p style=""font-size:11pt;font-family:Calibri,sans-serif;margin:0;"">&nbsp;</p>
        <p style=""margin-right:0;margin-bottom:12pt;margin-left:0;""><span
                style=""font-size:12pt;font-family:Times New Roman,serif;"">Dear Partner,&nbsp;</span></p>
        <p style=""margin-right:0;margin-bottom:12pt;margin-left:0;""><span
                style=""font-size:12pt;font-family:Times New Roman,serif;"">{{EmailBody}}&nbsp;</span></p>
        <p style=""margin-right:0;margin-bottom:12pt;margin-left:0;""><span
                style=""font-size:12pt;font-family:Times New Roman,serif;"">Kind regards,&nbsp;</span></p>
        <p style=""margin-right:0;margin-bottom:12pt;margin-left:0;"">&nbsp;</p>
        <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width:100%;border-collapse:collapse;"">
            <tbody>
                <tr>
                    <td style=""padding:0;"">
                        <table width=""100%"" style=""max-width:420px;"" border=""0"" cellspacing=""0"" cellpadding=""0""
                            style=""border-collapse:collapse;"">
                            <tbody>
                                <tr>
                                    <td style=""padding:0 5.25pt 0 0;"">
                                        <table border=""0"" cellspacing=""0"" cellpadding=""0""
                                            style=""width:100%;border-collapse:collapse;"">
                                            <tbody>
                                                <tr>
                                                    <td style=""padding:0;"">
                                                        <p style=""margin:0;""><span
                                                                style=""font-size:10pt;font-family:Times New Roman,serif;""><img
                                                                    data-imagetype=""External""
                                                                    src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/logo1.png?alt=media&token=feba3b9a-1ab9-4762-9c0f-49b78c991849""
                                                                    id=""_x0000_i1025"" width=""70"" height=""70""></span></p>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign=""top""
                                        style=""padding:0 7.5pt 2.25pt 5.25pt;border-style:none none none solid;border-left-width:2.25pt;border-left-color:#00B4E5;"">
                                        <table border=""0"" cellspacing=""0"" cellpadding=""0""
                                            style=""width:100%;border-collapse:collapse;"">
                                            <tbody>
                                                <tr>
                                                    <td valign=""top"" style=""padding:0;"">
                                                        <p style=""margin:0;line-height:12.0pt;""><b><span
                                                                    style=""color:#00B4E5;font-family:Arial,sans-serif;"">Eli
                                                                    Camps</span></b></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign=""top"" style=""padding:3.75pt 0 0 0;"">
                                                        <table width=""100%"" style=""max-width:640px;"" border=""0""
                                                            cellspacing=""0"" cellpadding=""0""
                                                            style=""border-collapse:collapse;"">
                                                            <tbody>
                                                                <tr style=""height:15pt;"">
                                                                    <td style=""height:15pt;padding:0 3.75pt 0 0;"">
                                                                        <p style=""margin:0;""><span
                                                                                style=""font-size:10pt;font-family:Times New Roman,serif;""><img
                                                                                    data-imagetype=""External""
                                                                                    src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/icons8-phone-100.png?alt=media&token=fb52b8ed-1bc3-4578-802d-eec0b2666115""
                                                                                    id=""_x0000_i1026""
                                                                                    width=""12"" height=""12""></span>
                                                                        </p>
                                                                    </td>
                                                                    <td style=""height:15pt;padding:0;"">
                                                                        <p style=""margin:0;""><span
                                                                                style=""font-size:10pt;font-family:Arial,sans-serif;""><a
                                                                                    href=""tel:+14163053143""
                                                                                    target=""_blank""
                                                                                    rel=""noopener noreferrer""
                                                                                    data-auth=""NotApplicable""><span
                                                                                        style=""color:#6C6C6C;"">+1.416.305.3143</span></a></span><span
                                                                                style=""font-size:10pt;""></span></p>
                                                                    </td>
                                                                </tr>
                                                                <tr style=""height:15pt;"">
                                                                    <td style=""height:15pt;padding:0 3.75pt 0 0;"">
                                                                        <p style=""margin:0;""><span
                                                                                style=""font-size:10pt;font-family:Times New Roman,serif;""><img
                                                                                    data-imagetype=""External""
                                                                                    src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/icons8-email-100.png?alt=media&token=e15668c4-7211-4533-b17f-8a4eb38d11bc""
                                                                                    border=""0"" id=""_x0000_i1027""
                                                                                    width=""12"" height=""12""></span>
                                                                        </p>
                                                                    </td>
                                                                    <td style=""height:15pt;padding:0;"">
                                                                        <p style=""margin:0;""><span
                                                                                style=""font-size:10pt;font-family:Arial,sans-serif;""><a
                                                                                    href=""mailto:info@elicamps.com""
                                                                                    target=""_blank""
                                                                                    rel=""noopener noreferrer""
                                                                                    data-auth=""NotApplicable""><span
                                                                                        style=""color:#6C6C6C;"">info@elicamps.com</span></a></span><span
                                                                                style=""font-size:10pt;""></span></p>
                                                                    </td>
                                                                </tr>
                                                                <tr style=""height:15pt;"">
                                                                    <td style=""height:15pt;padding:0;"">
                                                                        <p style=""margin:0;""><span
                                                                                style=""font-size:10pt;font-family:Times New Roman,serif;""><img
                                                                                    data-imagetype=""External""
                                                                                    src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/icons8-website-100.png?alt=media&token=8ef7410e-6797-4d62-bf39-dbfe0bf6d5e0""
                                                                                    border=""0"" id=""_x0000_i1028""
                                                                                    width=""12"" height=""12""></span>
                                                                        </p>
                                                                    </td>
                                                                    <td style=""height:15pt;padding:0 3.75pt 0 0;"">
                                                                        <p style=""margin:0;""><span
                                                                                style=""font-size:10pt;font-family:Arial,sans-serif;""><a
                                                                                    href=""https://www.elicamps.com/""
                                                                                    target=""_blank""
                                                                                    rel=""noopener noreferrer""
                                                                                    data-auth=""NotApplicable""><span
                                                                                        style=""color:#6C6C6C;"">www.elicamps.com</span></a></span><span
                                                                                style=""font-size:10pt;""></span></p>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td
                                        style=""padding:0 0 0 5.25pt;border-style:none none none solid;border-left-width:1pt;border-left-color:#EEEEEE;"">
                                        <table width=""100%"" style=""max-width:640px;"" border=""0"" cellspacing=""0""
                                            cellpadding=""0"" style=""width:100%;border-collapse:collapse;"">
                                            <tbody>
                                                <tr>
                                                    <td style=""padding:0;"">
                                                        <p style=""margin:0;""><a href=""https://www.elicamps.com/""
                                                                target=""_blank"" rel=""noopener noreferrer""
                                                                data-auth=""NotApplicable"">
                                                                <span
                                                                    style=""color:windowtext;font-size:10pt;font-family:Times New Roman,serif;text-decoration:none;"">
                                                                    <img data-imagetype=""External""
                                                                        src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/logo.png?alt=media&token=1ad62b42-3c49-4d7c-bea7-4ad08917e160""
                                                                        border=""0"" id=""_x0000_i1029""
                                                                        width=""172"" height=""42""></span></a>
                                                        </p>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan=""3""
                                        style=""background-color:#00B4E5;padding:3.75pt 2.25pt 3.75pt 7.5pt;"">
                                        <table width=""100%"" style=""max-width:640px;"" border=""0"" cellspacing=""0""
                                            cellpadding=""0"" style=""width:100%;border-collapse:collapse;"">
                                            <tbody>
                                                <tr>
                                                    <td style=""padding:2.25pt 0 0 0;"">
                                                        <p style=""margin:0;""><a
                                                                href=""https://www.facebook.com/theEliCamps""
                                                                target=""_blank"" rel=""noopener noreferrer""
                                                                data-auth=""NotApplicable""><span
                                                                    style=""color:windowtext;font-size:10pt;font-family:Times New Roman,serif;text-decoration:none;""><img
                                                                        data-imagetype=""External""
                                                                        src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/icons8-facebook-100.png?alt=media&token=84df0c46-91f5-4f21-8874-4642a6e91a74""
                                                                        border=""0"" id=""_x0000_i1030""
                                                                        width=""24"" height=""24""></span></a>&nbsp;&nbsp;<a
                                                                href=""https://www.youtube.com/channel/UC8qGDaByfndSqiyqUhvu4Hw""
                                                                target=""_blank"" rel=""noopener noreferrer""
                                                                data-auth=""NotApplicable""><span
                                                                    style=""color:windowtext;font-size:10pt;font-family:Times New Roman,serif;text-decoration:none;""><img
                                                                        data-imagetype=""External""
                                                                        src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/icons8-youtube-100.png?alt=media&token=d766e766-f9d8-4b24-9941-05850a4e75ac""
                                                                        border=""0"" id=""_x0000_i1031""
                                                                        width=""24"" height=""24""></span></a>&nbsp;&nbsp;<a
                                                                href=""https://www.instagram.com/elicamps/""
                                                                target=""_blank"" rel=""noopener noreferrer""
                                                                data-auth=""NotApplicable""><span
                                                                    style=""color:windowtext;font-size:10pt;font-family:Times New Roman,serif;text-decoration:none;""><img
                                                                        data-imagetype=""External""
                                                                        src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/icons8-instagram-100.png?alt=media&token=36925d98-3c47-442c-9447-1427024f6cdc""
                                                                        border=""0"" id=""_x0000_i1032""
                                                                        width=""24"" height=""24""></span></a>
                                                        </p>
                                                    </td>
                                                    <td style=""padding:0;"">
                                                        <p align=""right"" style=""text-align:right;margin:0;""><span
                                                                style=""font-size:10pt;font-family:Times New Roman,serif;""><img
                                                                    data-imagetype=""External""
                                                                    src=""https://firebasestorage.googleapis.com/v0/b/practice-7cbde.appspot.com/o/logo.png?alt=media&token=1ad62b42-3c49-4d7c-bea7-4ad08917e160""
                                                                    border=""0"" id=""_x0000_i1033""
                                                                    width=""184"" height=""18""></span></p>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>";

        string AgentInvoiceHTML = @"<!DOCTYPE html>
<html lang=""en"">
<head>
  <title>Bootstrap Example</title>
  <meta charset=""utf-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"">
  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>
  <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js""></script>
  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js""></script>
  <style>
    html,body {
  margin: 0;
  color: #3a405b;
  padding: 0;
  font-weight: 400;
  font-size: .875rem;
  line-height: 1.5;
  font-family: Roboto,sans-serif;
  background: #fff;
  height: 100%;
  background-image: none !important;
  background-repeat: no-repeat;
}
    .invoice {
      position: relative;
      background-color: #FFF;
      min-height: 680px;
      padding: 15px
    }
  
    .invoice header {
      padding: 10px 0;
      margin-bottom: 20px;
      border-bottom: 1px solid #3989c6
    }
  
    .invoice .company-details {
      text-align: right
    }
  
    .invoice .company-details .name {
      margin-top: 0;
      margin-bottom: 0
    }
  
    .invoice .contacts {
      margin-bottom: 20px
    }
  
    .invoice .invoice-to {
      text-align: left
    }
  
    .invoice .invoice-to .to {
      margin-top: 0;
      margin-bottom: 0
    }
  
    .invoice .invoice-details {
      text-align: right
    }
  
    .invoice .invoice-details .invoice-id {
      margin-top: 0;
      color: #3989c6
    }
  
    .invoice main {
      padding-bottom: 50px
    }
  
    .invoice main .thanks {
      margin-top: -100px;
      font-size: 2em;
      margin-bottom: 50px
    }
  
    .invoice main .notices {
      padding-left: 6px;
      border-left: 6px solid #3989c6
    }
  
    .invoice main .notices .notice {
      font-size: 1.2em
    }
  
    .invoice table {
      width: 100%;
      border-collapse: collapse;
      border-spacing: 0;
    }
  
    .invoice table td,
    .invoice table th {
      padding: 15px;
      background: #eee;
      border-bottom: 1px solid #fff
    }
  
    .invoice table th {
      white-space: nowrap;
      font-weight: 400;
      font-size: 16px
    }
  
    .invoice table td h3 {
      margin: 0;
      font-weight: 400;
      color: #3989c6;
      font-size: 1.2em
    }
  
    .invoice table .qty,
    .invoice table .total,
    .invoice table .unit {
      text-align: right;
      font-size: 1.2em
    }
  
    .invoice table .no {
      color: #fff;
      font-size: 1.6em;
      background: #3989c6
    }
  
    .invoice table .unit {
      background: #ddd
    }
  
    .invoice table .total {
      background: #3989c6;
      color: #fff
    }
  
    .invoice table tbody tr:last-child td {
      border: none
    }
  
    .invoice table tfoot td {
      background: 0 0;
      border-bottom: none;
      white-space: nowrap;
      text-align: right;
      padding: 10px 20px;
      font-size: 1.2em;
      border-top: 1px solid #aaa
    }
  
    .invoice table tfoot tr:first-child td {
      border-top: none
    }
  
    .invoice table tfoot tr:last-child td {
      color: #3989c6;
      font-size: 1.4em;
      border-top: 1px solid #3989c6
    }
  
    .invoice table tfoot tr td:first-child {
      border: none
    }
  
    .invoice footer {
      width: 100%;
      text-align: center;
      color: #777;
      border-top: 1px solid #aaa;
      padding: 8px 0
    }
  
    .mtable table td,
    .mtable table th {
      padding: 6px;
      background: #fff;
      border-bottom: 1px solid #fff
    }
  </style>
</head>
<body>
  <div id=""invoice"">
    <div class=""invoice overflow-auto"" style=""position: relative;
    background-color: #FFF;
    min-height: 680px;
    padding: 15px"">
      <div class=""container"" style=""min-width: 600px"">
        <header style=""padding: 10px 0;
        margin-bottom: 20px;
        border-bottom: 1px solid #3989c6"">
          <div class=""row"">
            <div class=""col"">
              <a target=""_blank"" href=""#"">
                <img
                  src=""data:image/png;base64,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""
                  width=""270px"" data-holder-rendered=""true"" />
              </a>
            </div>
            <div class=""col company-details"" style=""text-align: right"">
              <h3 class=""name"" style=""margin-top: 0;
              margin-bottom: 0"">
                Eli Camps
              </h3>
              <div>1.416.305.3143</div>
              <div>www.elicamps.com</div>
              <div>info@elicamps.com</div>
            </div>
          </div>
        </header>
        <main style=""padding-bottom: 50px"">
          <div class=""row contacts"" style=""margin-bottom: 20px"">
            <div class=""col-md-4  invoice-to"" style="" margin-top: 0;
            margin-bottom: 0"">
              <div class=""text-gray-light""><b>Invoice Date:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                {{CurrentDate}}
              </div>
            </div>
            <div class=""col-md-8 "">
              <h4 class="""">AGENCY INVOICE</h4>
            </div>
          </div>
          <div class=""row contacts"" style=""margin-bottom: 20px"">
            <div class=""col-md-12 mtable"" style=""padding: 10px;
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;""><b>Student Name:</b></td>
                    <td style=""width:25%"">cc</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""><b>Agent Name:</b></td>
                    <td style=""width: 35%;"">{{AgentName}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;""><b>Student Number:</b></td>
                    <td style=""width:20%"">{{Reg_Ref}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""><b></b></td>
                    <td style=""width: 35%;"">{{AgentAddress}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;""><b>Date Of Birth:</b></td>
                    <td style=""width:20%"">{{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;""><b></b></td>
                    <td style=""width:20%""></td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">{{AgentCountry}}
                    </td>
                  </tr>
                </tbody>
              </table>
  
            </div>
  
  
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"" cellpadding=""0"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">DATES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 10%;"">Start Date:</td>
                <td style=""width: 40%;"">{{ProgrameStartDate}}</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 10%;"">End Date:</td>
                <td style=""width: 40%;"">{{ProgrameEndDate}}</td>
              </tr>
            </table>
          </div>
  
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">CAMPUS</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{CampusAddressOnReports}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">ACADEMIC PROGRAM
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  <p>{{ProgramName}}</p>
                  <p>{{SubProgramName}}</p>
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">ACCOMODATION</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{FormatName}}<br>{{MealPlan}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">SERVICES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Included_Services}}
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">ADDITIONAL SERVICES
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Additional_Services}}
                </td>
              </tr>
  
            </table>
          </div>
          <hr style=""width: 100%; border-width: 2px; border-color: #000;"">
          <div class=""row"">
            <div class=""col"">
              <p>If you have any questions, please contact us by phone, mail or email </p>
            </div>
          </div>
          <div class=""row"">
            <div class=""col-8"">
              <p>Sincerely<br>Eli Camps Admissions</p>
              <br>
              <img
                src=""data:image/png;base64,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""
                width=""350px"" alt=""signature"">
              <br>
              <p>Elvis Mrizi<br> Director </p>
            </div>
            <div class=""col-4 mtable"" style=""padding: 10px;
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" style=""line-height: 0.9;"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td>Gross Price</td>
                    <td class=""text-right"">${{TotalGrossPrice}}</td>
                  </tr>
                  <tr>
                    <td>Additional Services </td>
                    <td class=""text-right"">${{CommissionAddins}}</td>
                  </tr>
                  <tr>
                    <td>Commission</td>
                    <td class=""text-right"">${{Commision}}</td>
                  </tr>
                  <tr>
                    <td>Paid</td>
                    <td class=""text-right"">${{Paid}} </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Balance due</td>
                    <td class=""text-right"">${{Balance}} </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <div class=""row"">
            <div class=""col"">
              <b>*All fees above are in Canadian Dollars </b>
            </div>
          </div>
          <hr style=""width: 100%; border-width: 2px; border-color: #000;"">
  
          <div class=""row mtable"" style=""padding: 10px; background: #fff; border-bottom: 1px solid #fff"">
            <div class=""col-6"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td width=""250"" style=""font-weight: bolder;"">Canadian Dollar Transfers:</td>
                    <td class=""text-right"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;""> Business name:</td>
                    <td class="""">Eli Camps</td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Business address:</td>
                    <td class="""">111 Ridelle Ave. Suite 203 Toronto Ontario M6B1J7 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;""> Account Insitution number:</td>
                    <td class="""">004 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Account number:</td>
                    <td class="""">5230919 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Account transit:</td>
                    <td class="""">12242 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">SWIFT CODE:</td>
                    <td class="""">TDOMCATTTOR </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Bank Name:</td>
                    <td class="""">TD Canada Trust </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Bank Address:</td>
                    <td class="""">777 Bay Street Toronto ON M5G2C8 </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </main>
        <footer style="" width: 100%;
        text-align: center;
        color: #777;
        border-top: 1px solid #aaa;
        padding: 8px 0"">
        </footer>
      </div>
  
    </div>
  </div>

</body>
</html>

";

        string StudentCertificateHTML = @"<!DOCTYPE html>
<html lang=""en"">

<head>
  <title></title>
  <meta charset=""utf-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"">
  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>
  <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js""></script>
  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js""></script>
</head>

<body>
  <img style=""width: 100%;height:100%;"" id=""mydiv""
    src=""data:image/png;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCAL5BDQDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD9U6KKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAopryKiksQAO5qhcaqq5EQ3H+8elUouWxMpKO5eaRUGWIUepNULjVl5EQ3f7R6VnSzPM2XYt6e1MreNJLc5JVm/hHyzSTNl3LfyplFFbpW2OdtvVhRRRQAUUVZt7GW4wQNq/3jSbS3Gk5OyK3erdvp0k/Lfu19T1rRt7CO3Ocbm/vGrNc8qv8p1Qo9ZFe3sorcfKMt/ePWrNFFYtt7nSklogooopDCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKQ0tFAGbqlp8vnKOR94DvWXXSOodSCMg1z88XkTOh7HiumlK6scVaNnzIjooorc5wooooAnivZocYckejc1dg1Zekike61l0VDhFmkako7M6KOdJVyrBh7GpK5tHZGypKn1FXrfVWXiX5h/eHWsJUmtjpjWT+I1qKiinSZcowapAc1idG4tFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRVO51GODIHzv6Cmk3ohNpastswUEk4FULjVVj+WMb29e1Z9xdyXJ+Y4X+6OlQ10RpfzHJOt0iSTTyTtl2J9u1R0UVvsczbe4UUUUAFFFOjRpX2qpY+goDcbUsFtJcNhFyPU9KvW2lchpjn/ZHStFYwgAUAAdhWEqttInRGi3rIp22mRxYZ/3je/QVdCgUtFc7berOtRUVZBRRRSKCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKydYjw6SDvwa1qpaom61Y/wB0g1cHaSM6ivFmNRRRXaecFFFFABRRRQAUUUUAOR2ibchKt6itO11QNhZflb+92NZVFRKKluXGbhsdKCGGQcilrBtb17U4+8n92tiG4SdNyHI9PSuWUHE7oVFMmoooqDQKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKa7iNSzEADuaAFqG4u47dfnbB9O9UbnVScrCOP7xrPZizEk5J7mto029zmnWS0iWbnUJJzhT5aeg6mqtFFdKSWxyOTlqwooopiCiiigAoqWC3kuH2oufUnoK1rXT44OT87+p7fSs5TUTSFNzKFrprzYL5Rf1NasNvHAuEUD+tSYxS1zSm5bnbCChsFFFFQaBRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAVXvhm0l/3asVW1BsWkvPbFNbomXwsw6Siiu88wKKKKACiiigAooooAKKKKACnxTPA4ZDg+nrTKKTVwTtqjdtLxblfRx1WrNc3G7ROGU4IratLwXKejjqtcs4cuq2O6nU5tHuWqKKKyNwooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKQ8CmTTpApZ2wKybrUXmyqfIn6mrjFy2M5zUNy7dajHBkD539B2+tZU1w9w252z6DsKjorqjBROKdRzCiiirMwooooAKKKlt7Z7lsIOO7dhSbS3BJvREYGTgcn0q/a6YWw03A/u96uWtiltz95v7xq1XPKpfSJ2Qo21kMjjWNQFAA9BT6KKwOkKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKo6s+23C/3mq7WPq0u+4CDoo/WtIK8kZVXaDKVFFFdh54UUUUAFFFFABRRRQAUUUUAFFFFABT4pWhkDqcEUyijcE7ao37S5W5iDL17j0qeuftbg20oYcjuPWt2OQSoGU5BGc1xzjys9CnPnXmPooorM1CiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooqOWZYULOwCigNh+apXepLDlU+d/0FVLrUmmyseUT17mqVdEafWRyzrdIj5Znncs5yaZRRXQcm+4UUUUAFFFFABRT4ommcKgya1rPTlgwz/M/r2FRKaiaQpuexVtNMaTDS/Kv93ua1UjWNQqqFA7Clpa5JSctzujBQWgUUUVJYUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAMkcRoWPQDNc9I5kkZz1Y5rU1WfbEIweW6/SsmumktLnFWld8oUUUVuc4UUUUAFFFFABRRRQAUUUUAFFFFABRRRQAVe0y68t/Kb7rHj2NUaKmS5lYqMuV3R0tLVWwuftEAz95eDVquJqzsz0k1JXQUUUUhhRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFIxwCScVl3ep9UhP1f8AwqoxcnZESkoq7LV3fJbjH3n7KKyJ53uH3Oc+g7CmEkkknJPekrqjBROGdRz9AooorQzCiiigAooooAKsWtk90c/dTuxqxZ6aXAeUYHZf8a1VUKAAMAdqwnUtojpp0r6yIoLdIE2oMDue5qaiiuY60rBRRRQMKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACkY4BPSlqhqlx5cWxT8z/yppXdiZS5Vczbqb7ROz9ug+lRUUV3JWVjzW7u7CiiimIKKKKACiiigAooooAKKKKACiiigAooooAKKKKALFjcfZ5wT908Gt0dK5qtnTLjzoApPzJwa56sftHVRl9ll2iiiuc6wooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKjnnWBCzMAKiu71LZcH5nPRRWNNO9w+5zn0HYVpGDlr0MalRQ0W5Ld373J2j5Y/T1qtRRXWklojhcnJ3YUUUUxBRRRQAUUVLBbvcPtQfUnoKTdtWCTbshiI0jhVG5j2Fa1npywAM/zP+gqa1s0tV+Xlu7etWK5Z1HLRbHbTpcur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VD7Ve/8APmv/AH8o+1Xv/Pmv/fyjlYc6/pF+iqH2q9/581/7+Ufar3/nzX/v5RysOdf0i/RWa13fL/y6D/vum/bb7/n0H/fVPlYvaLs/uNSisv7bff8APoP++qPtt9/z6D/vqjkYe0XZ/calFZf22+/59B/31R9tvv8An0H/AH1RyMPaLs/uNSisv7bff8+g/wC+qPtt9/z6D/vqjkYe0XZ/calFZf22+/59B/31R9tvv+fQf99UcjD2i7P7jUorL+233/PoP++qPtt9/wA+g/76o5GHtF2f3GpRWX9tvv8An0H/AH1R9tvv+fQf99UcjD2i7P7jUorL+233/PoP++qUXl8f+XQf99UcjD2i7P7jToqh9qvf+fRf+/lH2q9/581/7+UuVj51/SL9FUPtV7/z5r/38o+1Xv8Az5j/AL+UcrDnX9Iv0VSFxdkc2oz/ANdBS+fd/wDPqP8Av4KVmVzIuUVT8+7/AOfUf9/BUnmXH/PAf9/KLBzIsUVX824/54D/AL+U5HmLDdEFHrvzQO5NRRRSGFNkQSKVYAg9QadRQBiXtgbc7ky0f6iqldKRkYNZt3peSWh4PUrXRCp0kcdSj1iZlFOZSjFWBDDsabXQcwUUUUAFFFFABRRRQAUUUUAFFFFABRRV210x5vmk+RfTualtRV2VGLk7IrRQPO21Fz6nsK17SxS2AP3n9anigSFNqKAPangYrmlUctOh2wpKOr3FooorI2CiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACsbU7jzZtgPyp/OtG9uPs0DN36D61hZJ5PJrelH7Ry1pacolFFFdJyBRRRQAUUUUAFFFFAAeK2NMt/Lh3n7z8/hWbaQ/aJ1THy9T9K31GBiuerL7J1UI/aFooornOsKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKy/EniXTfCOi3eravdx2On2qGSWaU4AH9T6AcmsL4ZfFXQ/i1pF1qWgm5a2trg2zm5hMTbgA2QD2IYGgDsaKKKACiiigAooooAKKKKACiiigAorK8TeJtM8H6Ldatq93HY6fapvlmlOAB/UnsB1rE+GnxU0P4saTdajoLXDW1tObeT7TCYm3BQ3Q9iGBoA7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ABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAZWry5KJ/wI1nVYv5PMun54HFV67YK0UebUd5MKKKKsgKKKKACiiigAooooAsafH5l3H6Dmt0DFZejx5aR/TAFatclR3kd1FWiFFFFZG4UUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAU2R/LQt2AzTqqak+y1fsTxTSu7Eydk2YrHcxY9Sc0lFFd55gUUUUAFFFFABRRRQAUUUGgDZ0uPbag45Y5q7UVunlwRr6AVLXA3dtnpxVkkFFFFIoKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAoppcCgMDQA6im7wKVWDdKAFooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACs3WXwkajuc1pVjas+65C/3VrSmryRjVdoMpUUUV2HAFFFFABRRRQAUUUUAFPhXfKi+rAUyrOnpvu4/bmlJ2TZUVeSRuLwKWkHSlrgPTCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooqtqOoQaVZzXd1MkFtCpeSWRtqoo6knsKaV9EJtRV3sTSSLGpJYAAZyTXknjH9ovRdG1F9H0C2ufFWug7Ba6cm5Fb0Z+n5Zrn5b/X/wBou9mg06ef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YTcZju4gPduHH1/OvscgHrWF4o8EaH4ytPs2saVbahF285AWX6N1H4GvpqGcqpSWGzCn7WC2e0l6P+vmfnuL4VlRxMsfklb6vVe8bXpy9Y9Pl+BzHhf4/wDgbxXGn2fXbe1nbA+z3zeQ4J7fNgH8Ca7q31O1uYw8NxDKhGQ0cgIP4ivEdd/Y98HakxfT59Q0liekcolX8nBP61gH9jCKF3Ft4uvIY/4V8gZ/HDCs5YfKamtKvKHlKN/xTNqeO4moe5iMFCp5wqct/lJH0Te69p2nRmS7vra1QdWmmVB+przfxZ+0r4M8O74LS9Ov6h91LTTB5pZvTcPl/WuU039jjw5HcLJqms6pqigD92XWME+5AJ/WvUvBvwn8K+A0zo2jW9tN3uHHmSn/AIG2TXO4ZbR155VH2S5V97bf3I7o1c/xfu+yp0F3cnUl8klGP3s8wh8KeOPjnNDN4qLeFPCe7f8A2NAx8+6XqBKeMA+4/DvXuGh6HY+HNLg07TraKzs4F2xwwrtVR/nvV0KF6DFOrixGKnXSgkowW0Vsv1b83d+mx7GCy2lg3Kq5OdWW85ayfl2iu0YpL1eoUUUVxHrBRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFAFHV/+PZf94Vj1r6r81sD6MKyK66XwnBW+IKKKK1MQooooAKKKKACiiigArc03/j0T8f51h1taWc2i+2R+tY1djoofEXKKKK5TtCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigClqq/6IT6EVjVuamM2b456fzrDFdVL4Thr/EFFFFbGAUUUUAFFFFABRRRQAVsaQc23/AjWPWrozfu5B6Gsqnwm1H4zRooorkO8KKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAIL1d1rKPasAdK6OZd0Tj1BrnB0rppPRo46+6CiiitzmCiiigAooooAKKKKACtHRmw8i+wNZ1XNLbFzj1U1E/hZpTdpo2qKQdKWuI9EKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAEPTmuckXZI6+hIroz0rBvk2Xcg9Tmt6W7OautEyCiiiuk4wooooAKKKKACiiigAqazfZdRn3xUNKrbWDehzSaurDTs0zpB0paap3KD606uA9QKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACkLAd6G6V81fEv9m37Xda14k134oa7Z6MpkupIpGykCZ3bQd2NoHAG30oA991zxjofhlrZdW1a009rmYW8IuJgpkkPRRnvTIPHPhy5vI7SHXtNlupDtSFLuMuxzjAAOTXxl8D/ANnfQviLp2u+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i3uoJdMs5ILv/j4jaBSs3++MfN+NFxnzv+yZDqjxTXFjKLvSrl3vNY1aZGC3d644htl4ASMfecDDNwOBX01UNpZ2+n20dvawR29vGoVIolCqoHQADgCpqQBRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABVXUY99q/qOatU113qR6jFNOzuJq6sc3RTnUo7KeoOKbXeeWFFFFABRRRQAUUUUAFFFFAF3SpNtyV7MK2a523k8qdH9DXQg5FctVWlc7aLvGwtFFFYnQFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAIAB0paKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKQ0tFAGFqEfl3TcYDc1WrT1aP5Uf04NZldkHeKPOqK0mFFFFaGYUUUUAFFFFABRRRQAVvWMvnWyN3xg1g1p6PLw8ZPuKxqq6ub0XaVjTooorlO4KKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAILyIzW7qOuM1gCulPIrAu4vJuHXtnI+ldFJ9Dkrx2ZDRRRXQcoUUUUAFFFFABRRRQAVPZS+TcoexOD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MqkOeOm5h0UYwcHrRXYeeFFFFABRRRQAUUUUAFFFFAGnpV1j9yx91rSHNc2rFWDKcEcg1vWlwLiFWHXoR6GuWrGzudlGd1ysnooorE6QooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigApDzS0UAZGqWvlv5qj5W6/WqFdHJGJUZG5BGKwLiE28pQ9uh9RXVTldWZxVoWfMiOiiitjnCiiigAooooAKKKKACrNjdG2l5PyHr/AI1WopNJqzGm4u6OlBBGRS1m6ZefKIWPI+7WiK4pLldj0oyUldC0UUVJQUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAVTv7T7RFkffXkf4VcpMZpp2d0S0pKzOa6cUVoanabGMqDg/eH9az67YyUldHnSi4OzCiiiqJCiiigAooooAKKKKAFVirAqcEcg1uWd0LmIHow4IrCqW3uGtpQ6/iPWs5x5ka058j8joaKZFKsyB1OQafXGegFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFACEAjBGab5Sf3F/Kn0UBYZ5Sf3F/Kjyk/uL+VPop3FZDPKT+4v5UeUn9xfyp9FFwshnlJ/cX8qPKT+4v5U+ii4WQzyk/uL+VHlJ/cX8qfRRcLIZ5Sf3F/Kjyk/uL+VPoouFkIqhRgAAegpaKKQwooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAP/2Q=="">
  <div class=""centered"" style=""position: absolute;
    top: 35%;
    left: 50%;
    transform: translate(-50%, -50%);""><h3><b>{{StudentFullName}}<b/><h3/></div>
  </div>
</body>

</html>";

        string StudentInvoiceHTML = @"<!DOCTYPE html>
<html lang=""en"">
<head>
  <title>Bootstrap Example</title>
  <meta charset=""utf-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"">
  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>
  <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js""></script>
  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js""></script>
  <style>
    html,body {
  margin: 0;
  color: #3a405b;
  padding: 0;
  font-weight: 400;
  font-size: .875rem;
  line-height: 1.5;
  font-family: Roboto,sans-serif;
  background: #fff;
  height: 100%;
  background-image: none !important;
  background-repeat: no-repeat;
}
    .invoice {
      position: relative;
      background-color: #FFF;
      min-height: 680px;
      padding: 15px
    }
  
    .invoice header {
      padding: 10px 0;
      margin-bottom: 20px;
      border-bottom: 1px solid #3989c6
    }
  
    .invoice .company-details {
      text-align: right
    }
  
    .invoice .company-details .name {
      margin-top: 0;
      margin-bottom: 0
    }
  
    .invoice .contacts {
      margin-bottom: 20px
    }
  
    .invoice .invoice-to {
      text-align: left
    }
  
    .invoice .invoice-to .to {
      margin-top: 0;
      margin-bottom: 0
    }
  
    .invoice .invoice-details {
      text-align: right
    }
  
    .invoice .invoice-details .invoice-id {
      margin-top: 0;
      color: #3989c6
    }
  
    .invoice main {
      padding-bottom: 50px
    }
  
    .invoice main .thanks {
      margin-top: -100px;
      font-size: 2em;
      margin-bottom: 50px
    }
  
    .invoice main .notices {
      padding-left: 6px;
      border-left: 6px solid #3989c6
    }
  
    .invoice main .notices .notice {
      font-size: 1.2em
    }
  
    .invoice table {
      width: 100%;
      border-collapse: collapse;
      border-spacing: 0;
    }
  
    .invoice table td,
    .invoice table th {
      padding: 15px;
      background: #eee;
      border-bottom: 1px solid #fff
    }
  
    .invoice table th {
      white-space: nowrap;
      font-weight: 400;
      font-size: 16px
    }
  
    .invoice table td h3 {
      margin: 0;
      font-weight: 400;
      color: #3989c6;
      font-size: 1.2em
    }
  
    .invoice table .qty,
    .invoice table .total,
    .invoice table .unit {
      text-align: right;
      font-size: 1.2em
    }
  
    .invoice table .no {
      color: #fff;
      font-size: 1.6em;
      background: #3989c6
    }
  
    .invoice table .unit {
      background: #ddd
    }
  
    .invoice table .total {
      background: #3989c6;
      color: #fff
    }
  
    .invoice table tbody tr:last-child td {
      border: none
    }
  
    .invoice table tfoot td {
      background: 0 0;
      border-bottom: none;
      white-space: nowrap;
      text-align: right;
      padding: 10px 20px;
      font-size: 1.2em;
      border-top: 1px solid #aaa
    }
  
    .invoice table tfoot tr:first-child td {
      border-top: none
    }
  
    .invoice table tfoot tr:last-child td {
      color: #3989c6;
      font-size: 1.4em;
      border-top: 1px solid #3989c6
    }
  
    .invoice table tfoot tr td:first-child {
      border: none
    }
  
    .invoice footer {
      width: 100%;
      text-align: center;
      color: #777;
      border-top: 1px solid #aaa;
      padding: 8px 0
    }
  
    .mtable table td,
    .mtable table th {
      padding: 6px;
      background: #fff;
      border-bottom: 1px solid #fff
    }
  </style>
</head>
<body>
  <div id=""invoice"">
    <div class=""invoice overflow-auto"" style=""position: relative;
    background-color: #FFF;
    min-height: 680px;
    padding: 15px"">
      <div class=""container"" style=""min-width: 600px"">
        <header style=""padding: 10px 0;
        margin-bottom: 20px;
        border-bottom: 1px solid #3989c6"">
          <div class=""row"">
            <div class=""col"">
              <a target=""_blank"" href=""#"">
                <img
                  src=""data:image/png;base64,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""
                  width=""270px"" data-holder-rendered=""true"" />
              </a>
            </div>
            <div class=""col company-details"" style=""text-align: right"">
              <h3 class=""name"" style=""margin-top: 0;
              margin-bottom: 0"">
                Eli Camps
              </h3>
              <div>1.416.305.3143</div>
              <div>www.elicamps.com</div>
              <div>info@elicamps.com</div>
            </div>
          </div>
        </header>
        <main style=""padding-bottom: 50px"">
          <div class=""row contacts"" style=""margin-bottom: 20px"">
            <div class=""col-md-4  invoice-to"" style="" margin-top: 0;
            margin-bottom: 0"">
              <!-- <div class=""text-gray-light""><b>Invoice Date:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                {{currentDate | date:'longDate' }}
              </div> -->
            </div>
            <div class=""col-md-8 "">
              <h4 class="""">INVOICE</h4>
            </div>
          </div>
          <div class=""row contacts"" style=""margin-bottom: 20px"">
            <div class=""col-md-12 mtable"" style=""padding: 10px;
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;""><b>Invoice Date:</b></td>
                    <td style=""width:25%"">{{CurrentDate}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""><b></b></td>
                    <td style=""width: 35%;"">
                    </td>
                  </tr>
				  <tr>
                    <td style=""width: 15%;""><b>Student Name:</b></td>
                    <td style=""width:25%"">{{StudentFullName}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""><b></b></td>
                    <td style=""width: 35%;"">
                    </td>
                  </tr>
                  
				 <tr>
                    <td style=""width: 15%;""><b>Student Number:</b></td>
                    <td style=""width:25%"">{{Reg_Ref}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""><b></b></td>
                    <td style=""width: 35%;"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;""><b>Date Of Birth:</b></td>
                    <td style=""width:20%"">{{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <!-- <td style=""width: 35%;"">Ciudad de México.
                    </td> -->
                  </tr>
                </tbody>
              </table>
  
            </div>
  
  
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"" cellpadding=""0"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">DATES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 10%;"">Start Date:</td>
                <td style=""width: 40%;"">{{ProgrameStartDate}}</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 10%;"">End Date:</td>
                <td style=""width: 40%;"">{{ProgrameEndDate}}</td>
              </tr>
            </table>
          </div>
  
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">CAMPUS</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{CampusAddressOnReports}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">ACADEMIC PROGRAM
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  <p>{{ProgramName}}</p>
                  <p>{{SubProgramName}}</p>
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">ACCOMODATION</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{MealPlan}}<br>{{FormatName}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">INCLUDED SERVICES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Included_Services}}
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""padding: 10px;
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee; font-weight: bolder;"">ADDITIONAL SERVICES
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Additional_Services}}
                </td>
              </tr>
  
            </table>
          </div>
          <hr style=""width: 100%; border-width: 2px; border-color: #000;"">
          <div class=""row"">
            <div class=""col"">
              <p>If you have any questions, please contact us by phone, mail or email </p>
            </div>
          </div>
          <div class=""row"">
            <div class=""col-8"">
              <p>Sincerely<br>Eli Camps Admissions</p>
              <br>
              <img
                src=""data:image/png;base64,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""
                width=""350px"" alt=""signature"">
              <br>
              <p>Elvis Mrizi<br> Director </p>
            </div>
            <div class=""col-4 mtable"" style=""padding: 10px;
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" style=""line-height: 0.9;"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td>Total Package Price </td>
                    <td class=""text-right"">${{NetPrice}}</td>
                  </tr>
                  <tr>
                    <td>Additional Services </td>
                    <td class=""text-right"">${{CommissionAddins}}</td>
                  </tr>
                  <tr>
                    <td>Paid</td>
                    <td class=""text-right"">${{Paid}} </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Balance due</td>
                    <td class=""text-right"">${{Balance}} </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <div class=""row"">
            <div class=""col"">
              <b>*All fees above are in Canadian Dollars </b>
            </div>
          </div>
          <hr style=""width: 100%; border-width: 2px; border-color: #000;"">
  
          <div class=""row mtable"" style=""padding: 10px; background: #fff; border-bottom: 1px solid #fff"">
            <div class=""col-6"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td width=""250"" style=""font-weight: bolder;"">Canadian Dollar Transfers:</td>
                    <td class=""text-right"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;""> Business name:</td>
                    <td class="""">Eli Camps</td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Business address:</td>
                    <td class="""">111 Ridelle Ave. Suite 203 Toronto Ontario M6B1J7 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;""> Account Insitution number:</td>
                    <td class="""">004 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Account number:</td>
                    <td class="""">5230919 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Account transit:</td>
                    <td class="""">12242 </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">SWIFT CODE:</td>
                    <td class="""">TDOMCATTTOR </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Bank Name:</td>
                    <td class="""">TD Canada Trust </td>
                  </tr>
                  <tr>
                    <td style=""font-weight: bolder;"">Bank Address:</td>
                    <td class="""">777 Bay Street Toronto ON M5G2C8 </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </main>
        <footer style="" width: 100%;
        text-align: center;
        color: #777;
        border-top: 1px solid #aaa;
        padding: 8px 0"">
        </footer>
      </div>
  
    </div>
  </div>

</body>
</html>

";

        public EmailSender(IELIService ELISupervisor, IConfiguration iconfiguration, IUserRepository iuserRepository,
            ILookupTableRepository ilookupTableRepository)
        {
            _ELIService = ELISupervisor;
            _configuration = iconfiguration;
            _userRepository = iuserRepository;
            _lookupTableRepository = ilookupTableRepository;
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.json", true)
           .AddEnvironmentVariables();
            var config = builder.Build();
            ConString = config.GetConnectionString("ELIDb");
            AuthConString = config.GetConnectionString("ELIAuthDb");
            RegionId = config.GetConnectionString("RegionId");
            DefaultPassword = config.GetSection("ConnectionStrings").GetSection("DefaultPassword").Value;

        }
        public async Task SendEmaill(string SecurityKey, string Email, EmailTemplate emailTemplateId)
        {
            var region = await _ELIService.GetRegionById(Convert.ToInt32(RegionId));

            try
            {


                if (emailTemplateId == EmailTemplate.Welcome)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var CompanyTel = _lookupTableRepository.getpath(LookupValueEnum.CompanyTel);
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var regionUrl = _lookupTableRepository.getpath(LookupValueEnum.region_url);

                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, regionUrl.Description, CompanyTel.Description, CompanyAddress.Description, imgSourceLink.Description);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }
                else if (emailTemplateId == EmailTemplate.ForgetPasswordEmail)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var resetUrl = _lookupTableRepository.getpath(LookupValueEnum.ResetUrl);
                    resetUrl.Description = resetUrl.Description + "?Key=" + SecurityKey + "&email=" + Email;
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, resetUrl.Description, CompanyAddress.Description, imgSourceLink.Description);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }
                else if (emailTemplateId == EmailTemplate.ResetSuccessfully)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, CompanyAddress.Description, imgSourceLink.Description);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }
                else if (emailTemplateId == EmailTemplate.BulkCodeAccount)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var CompanyTel = _lookupTableRepository.getpath(LookupValueEnum.CompanyTel);
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var regionUrl = _lookupTableRepository.getpath(LookupValueEnum.region_url);
                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, regionUrl.Description, CompanyTel.Description, CompanyAddress.Description, imgSourceLink.Description, DefaultPassword);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }

            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return;
            }

        }
        public EmailTemplateViewModel GetEmailTemplate(EmailTemplate Email_templateId, string RegionName)
        {
            EmailTemplateViewModel da = new EmailTemplateViewModel();

            DataTable dt = new DataTable();
            List<EmailTemplateViewModel> dal = new List<EmailTemplateViewModel>();
            using (SqlConnection sqlConn = new SqlConnection(ConString))
            {
                string sql = "spTemplateSelection";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    //sqlCmd.Parameters.AddWithValue("@regionname,", RegionName);
                    sqlCmd.Parameters.Add("@regionname", SqlDbType.NVarChar);
                    sqlCmd.Parameters["@regionname"].Value = RegionName;
                    sqlCmd.Parameters.Add("@TemplateTypeid", SqlDbType.Int);
                    sqlCmd.Parameters["@TemplateTypeid"].Value = Email_templateId;
                    //sqlCmd.Parameters.AddWithValue("@TemplateTypeid,", TemplateTypeId);

                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {

                        sqlAdapter.Fill(dt);
                    }
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                da.Body = dt.Rows[i]["Body"].ToString();
                da.RegionId = dt.Rows[i]["RegionId"].ToString();
                da.Subject = dt.Rows[i]["Subject"].ToString();
                da.TemplateType = dt.Rows[i]["Name"].ToString();
                // dal.Add(da);
                //return da;
            }
            return da;
        }
        public ResetPasswordViewModel GeneratePasswordResetTokenAsync()
        {
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                // TempPassword = Guid.NewGuid().ToString()
            };
            return resetPasswordViewModel;
        }


        public async Task<bool> SendRegistrationEmailWithDocument(EmailSendVM emailSendVM)
        {
            var response = false;
            var emailTemplate = StudentRegEmailBody;
            var studentPDFDataVM = await _ELIService.GetStudentFilesDataAsync(emailSendVM.StudentID);

            if (emailTemplate != null && studentPDFDataVM != null && !string.IsNullOrEmpty(studentPDFDataVM.Email))
            {
                var email = new EmailViewModel();

                email.Subject = "Registration confirmation SANTINO COVARRUBIAS GALLEGOS";
                email.Message = emailTemplate;
                email.Message = email.Message.Replace(EmailSender.RequesterNameTag, emailSendVM.EmailBody);
                email.To = emailSendVM.StudentEmail;
                if (emailSendVM.IsAgentInvoice)
                {
                    email.AgentInvoiceTemplate = AgentInvoiceHTML;
                    AgentInvoiceTemplateRendrer(studentPDFDataVM, email);
                    PDFCreator(email, $"Eli Agent Invoice-{studentPDFDataVM.Reg_Ref}.pdf", email.AgentInvoiceTemplate);
                }
                if (emailSendVM.IsStudentCertificate)
                {
                    email.StudentCertificateTemplate = StudentCertificateHTML;
                    email.StudentCertificateTemplate = email.StudentCertificateTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentPDFDataVM.FirstName} {studentPDFDataVM.LastName}");

                    PDFCreator(email, $"Eli Student Certificate-{studentPDFDataVM.Reg_Ref}.pdf", email.StudentCertificateTemplate, true);
                }
                if (emailSendVM.IsStudentInvoice)
                {
                    email.StudentInvoiceTemplate = StudentInvoiceHTML;
                    StudentInvoiceTemplateRendrer(studentPDFDataVM, email);
                    PDFCreator(email, $"Eli Student Invoice-{studentPDFDataVM.Reg_Ref}.pdf", email.StudentInvoiceTemplate);
                }
                SendEmail(email);
                response = true;
            }
            return response;
        }

        public void SendEmail(EmailViewModel message)
        {
            Task.Factory.StartNew(() => sendEmail(message));
            //sendEmail(message);
        }

        private bool sendEmail(EmailViewModel message)
        {

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                (message.Message, null, MediaTypeNames.Text.Html);
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com", // set your SMTP server name here
                Port = 587, // Port 
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("elicampswork@gmail.com", "abcd@1234")
            };


            using (var mail = new MailMessage("elicampswork@gmail.com", message.To))
            {
                mail.AlternateViews.Add(avHtml);
                mail.Subject = message.Subject;
                mail.IsBodyHtml = true;

                foreach (var att in message.emailAttachment)
                {
                    mail.Attachments.Add(att);
                }

                try
                {
                    smtpClient.Send(mail);
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                }
            }

            return true;

        }
        //private async Task<bool> sendEmail(EmailViewModel message)
        //{

        //    string htmlBody = "<html><body><h1>Picture</h1><br><img src=\"cid:filename\"></body></html>";
        //    AlternateView avHtml = AlternateView.CreateAlternateViewFromString
        //       (htmlBody, null, MediaTypeNames.Text.Html);
        //    ContentType ct = new ContentType();
        //    ct.Name = "FileTest.png";
        //    ct.MediaType = MediaTypeNames.Image.Jpeg;
        //    //LinkedResource inline = new LinkedResource("filename.jpg", MediaTypeNames.Image.Jpeg);
        //    //inline.ContentId = Guid.NewGuid().ToString();
        //    //avHtml.LinkedResources.Add(inline);

        //    MailMessage mail = new MailMessage("elicampswork@gmail.com", message.To);
        //    mail.AlternateViews.Add(avHtml);
        //    mail.Subject = "Test";
        //    //try
        //    //{
        //    //    byte[] myByteArray = System.Convert.FromBase64String("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");
        //    //    //Stream stream = new MemoryStream(bytes);
        //    //    //byte[] myByteArray = new byte[10];
        //    //    MemoryStream stream = new MemoryStream();
        //    //    stream.Write(myByteArray, 0, myByteArray.Length);
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //}
        //    //byte[] myByteArray = System.Convert.FromBase64String("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");
        //    //Stream stream = new MemoryStream(bytes);
        //    //byte[] myByteArray = new byte[10];
        //    //MemoryStream stream = new MemoryStream();
        //    //stream.Write(myByteArray, 0, myByteArray.Length);
        //    //Stream stream = new MemoryStream(bytes);
        //    //byte[] myByteArray = new byte[10];
        //    //MemoryStream stream = new MemoryStream();
        //    //stream.Write(myByteArray, 0, myByteArray.Length);
        //    //System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(stream, ct);
        //    //System.Net.Mail.Attachment att = new System.Net.Mail.Attachment("15894665_1371534639565075_1418634479093817837_n.jpeg");

        //    //This attachment is only for AuctionWon Email
        //    if (File.Exists("15894665_1371534639565075_1418634479093817837_n.jpeg"))
        //    {
        //        System.Net.Mail.Attachment att = new System.Net.Mail.Attachment("15894665_1371534639565075_1418634479093817837_n.jpeg");
        //        att.ContentDisposition.Inline = true;
        //        att.ContentId = "filename";
        //        mail.Attachments.Add(att);
        //    }
        //    //System.Net.Mail.Attachment att = System.Net.Mail.Attachment.CreateAttachmentFromString("/9j/4AAQSkZJRgABAgAAAQABAAD/7QCcUGhvdG9zaG9wIDMuMAA4QklNBAQAAAAAAIAcAmcAFDU2QWRyQWJYclkwRXY1aTFoX0IzHAIoAGJGQk1EMDEwMDBhYzAwMzAwMDA3MTA4MDAwMDJiMGYwMDAwNTkxMDAwMDBiNjExMDAwMDFjMTYwMDAwNDYxZTAwMDAzYzFmMDAwMGI4MjAwMDAwNWYyMjAwMDA2ODMwMDAwMP/iAhxJQ0NfUFJPRklMRQABAQAAAgxsY21zAhAAAG1udHJSR0IgWFlaIAfcAAEAGQADACkAOWFjc3BBUFBMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD21gABAAAAANMtbGNtcwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACmRlc2MAAAD8AAAAXmNwcnQAAAFcAAAAC3d0cHQAAAFoAAAAFGJrcHQAAAF8AAAAFHJYWVoAAAGQAAAAFGdYWVoAAAGkAAAAFGJYWVoAAAG4AAAAFHJUUkMAAAHMAAAAQGdUUkMAAAHMAAAAQGJUUkMAAAHMAAAAQGRlc2MAAAAAAAAAA2MyAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHRleHQAAAAARkIAAFhZWiAAAAAAAAD21gABAAAAANMtWFlaIAAAAAAAAAMWAAADMwAAAqRYWVogAAAAAAAAb6IAADj1AAADkFhZWiAAAAAAAABimQAAt4UAABjaWFlaIAAAAAAAACSgAAAPhAAAts9jdXJ2AAAAAAAAABoAAADLAckDYwWSCGsL9hA/FVEbNCHxKZAyGDuSRgVRd13ta3B6BYmxmnysab9908PpMP///9sAQwAGBAUGBQQGBgUGBwcGCAoQCgoJCQoUDg8MEBcUGBgXFBYWGh0lHxobIxwWFiAsICMmJykqKRkfLTAtKDAlKCko/9sAQwEHBwcKCAoTCgoTKBoWGigoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgo/8IAEQgBNQDOAwAiAAERAQIRAf/EABsAAAEFAQEAAAAAAAAAAAAAAAMBAgQFBgcA/8QAGQEAAwEBAQAAAAAAAAAAAAAAAQIDAAQF/8QAGQEAAwEBAQAAAAAAAAAAAAAAAQIDAAQF/9oADAMAAAERAhEAAAHT+cvVaTICd6BhnFkYRr5g7wrU+DLGNHciwX3lTaQWKeznY8IdkiM9kJFLGUkiuagcr2WWQ8LmYr0LNq9fJpyyMeKNj0cDJpXVZ0E9sgDbzmKwI1PHKqIuUL2IU8aQzCCkiiucrlrFRW0mjn+QLzq65rJWsesFKom49gs+Rddszfe9s4jC0xop45YaOSAVQq7FIJ1lliG/OxJA8ovPQjlFMs3hW2vLibHqxmR6rkkbF9h491vq4rlzPVYnke+Rrhrk97yELkch8UTutZJEXO4KiOIrZAXmM22mcF+eX9+SVOdTdQVXwPVcduevk97y1kiP8ub5W4+RfDBchywSMf0qd4DBxeI8gTiDBgUGwx/LQEYweW6nr4aNotZHkenwkD5GVfJ6Qc3yA+VfLlLDLVmE8t1eeOZaCPG8UlKEyOGhFUzWBISk4emXdxt6y8r1WPj9HN1Q/ObOg2jqG0YyWuRAi+VSJzSUZSjlWaP4wMvve50F1OSryQV+lzG1UUBtViuftu9Wyuvy4qLZxyjSxJwIq+wq1fa6DnXRrBF8igRo6s8o4D2fwldljcp6dzCcnvYSQd0bnfTiJ3PtrzaXZ1XN6TKV46IEkONVZ0NpN2xJIlaJ2Pi/T+gWiKmWO5vpvJPFN2ZXMIrV3MOi4CEwyIMxUN1jk3XcKDHaCmj6PTcNvOeX86PDkNxzUmMWVLAJEOq95hNVQ7R7j2WuV45sT3mXWR4BcMtk9HmecRJ1ZYoJvWeS9XOwZs3f8/p9E5x0Lm/T5cQkOQrU0WfTzfQEjy6JUX1Bal+lmC7oAmOIGjGA9UaZBHY2hmworV2dXZgWnS+bb3PzvS5bXc/qabmPQuY9Pk2TWtGh0V7To9vZ0d9RKKbFKKdTcx1Ax4Tu7QeOkvMGEbnLfOQVk6sscL3X5u0FsftsHuY9svAdS5RbzpfkQEFVawFYOkyGqrKveg1r1nzmUwSgJmYZY+lJo72i2wYJMdFjTquz22PkGK5PcYLZx695yfq3OejgqvI6bih2FaGgXtPdViONLAtesJ5tdGkRDzczRSnjGqLjJDZ+qtq5Vi2ddY7auVXbsvxva+2UeiTi9hR35MG5pZ0j1s2rUv0ef0NJx4UpM/TG2Eboetc18D46AMZeA3/PQI9Da1ADJ8KYNp9/wLTbdgDgIxPRa7ndFtoEpZM6Di+dhI0FFfURixj6nVYZI3VSAUUye8FkiUB883XPMPUtjWBSSI5Rq6XFsNgM0LMaItsLCD40ZKe95NrK8q7Sy1drUaPU24WTOulVaNgq0WUFnLzLznpnPBoEG2MBAhT67KCRHn7TjwiEtGeZtCqruiVnscIDRzADfQdxhehNWynw3dLljgcS0YZfHzvpZ9PO41zE+VK2rt6bo4mSgzgGPcDMVo5ZwYd5Rg+nQ7JWuq7RR49uf3NXeWhMWBJuAyYRGIZUWTGDaHQZpLUWxx+zmchn9JnLcpZIZR0Pw3jTRzIZxYkgQaNaVNyj3VfaVvN6Npd0l11chV8mULmuof/EAC4QAAEDAgUDBAICAwEBAAAAAAEAAgMEEQUQEiExEyIyBiAzQSMkFDQlMEIVQ//aAAgBAAABBQIoJmT0cggVqR39zDlZOFsm2RUltcoaHHIJidwiMgrZAJ9r+wJpV0TldEqTm9zkECiU0JzUUE0K2T8h7b53yk5aNkR7GlEpyCYiqvFoYHw41BK+apZCKOrhqSRb3AZycx+DWgp5BVkwItyvkFwMUxd063QvcueWwTPglwqvFdTH2BWFnZScs8AV9ttYbIlO5QbcL1FU9KkCJQ4IsXc4ZUfxqs5hBXT+VIhnH5ZEezHJjLiLVS4T1QcHiDarC26ZoXROcsOlMtB7b5PzGV0FZWy+6y/8qjge9UwsFIsTZqa7d2DD/F2yv7LJ/sCDdmp2dlj1ForqiHQx0k7Hw/yXUb6mpJpnPc6ppXvrqeMQwe+T2N5AuhsnnIILHT3MsQ+KNqt+BsDHgtZG2k3xn/Q/ho3Lc2GydkBkFW0oqR1bJsmt8pqA2KbQnT3WHUrxVJzbe9/EBs95FwhlZOFk0IjPEGdGeOJjk9u5gY0F+p9I5jqb/Q/gK+QTcimO2VkVi5Bgjqemv5wU1RrGE0a/kS0VfBjEUiimjmyHtdwzm+YyOQV9qzFI2k9WoppY2vT4nA4VhvWNrCcapG9rgo6uojUOJMKjqIZPY7xZymq3suAqnFIIVVVk1SiQG0k5o8MhqKeVg6DVhmLGSasdopNF2vi0Jg7U4bT7LB6ozwZO8WeWxTQgnZ4pOZ6kBck84bF1YqygimhooOtUwUsMDcV7KJSpp/IeCbvl3WCy6K7jJ3iMm8oop50tcgmIeeFC1Go5WjGVjJ2Tk46aku7I/FyidolNkQneIQQX1fKv2o3fH/zGmedC3TSVsnRpaQfmZxjBvVo+d71Mx0xxf1ncLDJerQXTvEZBBOTViu2HOH4V9s84BaDHXWosPF6sLETqrgmlN+eod+tF8D+Dx6fN6WyPigEEMmFY8+1AbdFmUXmzwx9/5MLH7o4nOqomOmM2ayTtnqrCnp/glRXp5/eN1sIzkUDnj77h20cR2+oOW+OMPviOEAGs+uVI/VUPKqeah92Uh/DInLATauR8TmU05Y5883xQocQKM9tc8yVnp4k1Mhsx7tEVN4lTi4kPZRH8MnDucHNsRujwPK1y05FNN1i79VdP8UPkFTN1STHpQyju9O/NVPtT1rrpmwKkU3OH7xuG0nOFn/II8R+bvJwTTlwal+urn2YzZ4WH91diZtQay4eneMSIZSA65U5PUyw47OPbLzQG1cCj4x+TueUdk03T9m3uiWuc8/mCwUasTxp2mkOxwD4Mev8A+a3hvCcpxtRmxv8AjkVD/eIsj4t5ubsKK4NY61GOC9sTAS6VenW3rPUb7NcV6eN6WuZ1aRoyKcpBswlro3iSB6pzapuncDlOFk03DhdYo7TQtU41P/8AovTvl6l2eV6aN4ip2dOa+TlImC77iOaTmP5ijx9go7rggrGW/otU57o/k+vT7g1epN5QV6bd3ErF22rvqyKk4p955W6o37th3qSjx9hN4cE02WNPtSAKU63s+X69PfL6iYSTz6daeqsdbZ+T+Hqk/sMbqbIwtbT/ANtFfaYcnBYzJd52Y9oYyPzXp49zo+tG7ApDVQQCBhusVZ1aUHcbqd1gSqHeobspn3VE29foUgsncp2xYUViJvXucI2yuL3xcndYHPpDZHlCRGcBdZyqKl7Wu2dE7umdeZ3lQfPe6c6yw868VCmR5ThsNiFV91fU7NeocmPcx1NjlXGh6gajjzyp8Wlcp55ZjTv/ABl6bu5yoOW8TlYHvil09HnplBPCjO8h1VNSbJxUXB8WpjNZLC3JrHOXQcowAnlMT/GhHaBtNzgLf22vGlwTwtYUbkVL2tYqt3cQmiyf8YUZDk27k9jEItnCMHXqL03iTijHY7iVenxaNM+Odo0lyOxBuqsfrt4m8ra31gtWS/Gxad6d7gnPUjyS2O5eyzXpqdu+j+OR1hIsGjth7TpLD2zO2Gi7txGbGVuuHoyNdJFIVhlO84jWf3JvBiav+k0bjZSnsdym+cAtFJuZeML2w/YrhshRTDcPCjdcN8q6pEQpJP2Kv+5Pw0INVytS6tl13FBpIPJ4w6ETTOo3tD46hqjglnfSt6EOzh/y7m4TDpcdw06XOvqkPXqmseI8QGmpl5Yg0FdNaEI0xoVtvt3jhTtM4maU6oZehdqer2V+x3kU9Rm7XBPdZYZ3ywNBZjLdM/20ItIT3vCi710guE1ykbaR/GEtvPK25I7KDxaUVw0+T1//xAAnEQACAgIBBAEDBQAAAAAAAAAAAQIQETEhAxIgQQQiMjMTIzBCcf/aAAgBAhEBPwEQ3SZi4sYmNjtVFcDfkjGaWrb9Vozm3TpvJioruZ2JE+knHIvF0iTMZOnFIhHBGGGdVJS8EO0uRrBCXJHlmScu5+LqLGJmV3CiyU8DM+D0KI1is10mpxeXonJN8XG3og/RIdM+JDKk3bIDp6NC5J7pnSXZ0M/6KmQHT0ZpvNMk+3odoqeiJKnrwRFZkkfLf7bwRp6EOnrwR0Pyo+Y/oI6r0IlT1bEdD8iPkcwI16pvKr1bEdOXZLuOt1otYQqzxS0YPXgq4OBcqkIkY4/gWj1a5NLisN6HF+K0PQhbFwS5VdDY3nK8UQX1o/RiycVF8V6ro7P6j3SpEPvVT+6vR//EACQRAAICAQQDAAIDAAAAAAAAAAABAhEQAyExQRIgMiJRM2Fx/9oACAEBEQE/AYkuRrbC3HvsNCOhDVj2LtYod2PnFpHZsJo/w3RORFLx9dSXSwn2hStYjiRGdIW5VCLJvsTbRGbUqIllnkxiIjEhmr9Ehs0la9YxYhrssuzXhtaK2xppRQ8s08MatD2NR/ieRGNkW0eRt1hkOCTFuSlQ5N4lHxZFVzixkd0MjKiRwajwjVfGU+SRpksND4JYRLeeUSNMokqYpdFk+cIW8/RrYgWNWx/sTseOjT+iWOzohiHBx6S+TS+ieeiHIyLOfSfwaX0S4yuCOzGR5w8teUaIQd2x8ZQvoaT4O/VM8jyLLEL6KoUblhjz4lD5xVGl9j2KT3w2WvWXIsaPIv0f1jUFz6IkPg82jTk6sUyE6bxqcEP5MLLHxiPzhdn/xAA2EAABAgQDBQcEAQMFAAAAAAABAAIDEBEhEiBRIjFBYXEEMDJSYnKBEyNAQpEzgqEUYJKxwf/aAAgBAAAGPwL8caIYf9gYfEVheCzmsT3b9yww3jHp3ZlbuKowuzHDC83Fy0kA4l1BQckIkI7QW1T6zfEP/e6d3FZCC07UX/rNCf8AqTR34kTRmwEFV76clSpWw6625QHu3lv4cbFvxlF1LAW5oTsKy7PXTva5mRB4Yxv1Vg76Y4N4r7X1Gild6MTF/K8T6VpZeJ51BX04IqXJkNu5op+HAr5wrrFRG1luWyEOn4lzuvLadfgFgD3H1LadV3GX+p/V0t/cX7uv6OuEXPaCdVTBF/5LHQ4lZMME4mU/FY39q2VCFuVNyEd/9oUf6DrYzs8CvuQ3s/yvtPDvwbot7OPqO14IdoJxOhRNr20VZfViikHh6l0T3auJlUKz8Q0cvvNLDqLhbMRua+baNAqQ/uu5bl911GeVu5bK7TFa3E6tGj4Q2sDzvbSwRdFLXU3Cq+jHbsnwEDw9VFd6VQq29WyFjzts/wAjLbMRXYbYCYTa+FpJ+U5rWhruBTILx7kRCYAiG7nOApLnMDS6KAO5+z3ROgTTrWYTJfVZ4TEIlAZ1dKqH8Se74kxw4GqqOPcxz6SofXJCHJRH6BM90mjysk0f3GTimc7zhHiBh7mP0R5XkJMGjQsPmKhDnKLyoJPeeO5FE6pnScRuj+5p5nAJ3SQkFCZyqoXzKK7V5RpvNgsIXVUTJxm8gZc88BnUp3RCYTvSAFXQSrqg3gy8g6QmRqyQzwvajMe4IKK71J1fKn9FU8Ai473XkcsP5EwjlI8goiqSgN8z07kKyinRtFFPpKbDHHflPWcD3TCOWM7V0hLsrdHKORvIoJdod0Ci10Tnn4ymcD3dwToETqm4uCAkz0tJTG6uRoop1cn01C6Zqyge8dxGPpW9En4CxO3mUR2jKLs45kyf71FZq1DLUKolCPqHcROeWMei7P0Moo9Unt0OWnNW8LrSZ7h3DuoyxeoUD2mUcdDJ/qvkKCBHBAqF7h3Ab5nSOiEotVAfTZGyZRXDwkShv5UzUV1B947iHD0ujqqcUTKJyRa8bJVnD6GqwQ22kdWmuQo9J9m9/cP5WWIivJYjZGRYxgcV4ZcFbCflHHCFOqtuV10RRPKfZ669xG90zLFDcWnUIB5EVuhC/oO/lWgD5KsyGOirEeSiNJEyceU2HQHJbJEdq4oWujIyp+3BXErNW1Ro5o0JOR04jvK1U4zvNx5SAldOlQ2KoQvDfkqkKrr8luo3Ies4z9TSQVskT2mVU1o4mijAcHUyUK4KgV0ZjI0+Ykq8qL7lZvaN5FEW4SaaL+m/+FBxsIDdq6j+8oZSZOyNnBpoqFUMhkPVBQn12XWUb3lCVirz2QiXG8yHbqL7cU/KuAVhw0TIda4eOQVVJkhU0TQ2tzbqq7i8YiOaEt0t6ucrui5ScaUzVm/knPO9FtLYqplPLOzit6vMIycdAqcKI8k4/GUL/8QAJxAAAgIBBAICAwEBAQEAAAAAAAERITEQQVFhcYGRobHB8NEg8eH/2gAIAQAAAT8hFGWIhFkZCHKDDSa0RBuRJitkCwblFrQsHcxSJQinRLFsjgNyyzRBCGK3ERknPei03HjRwNvRQGD0Gymx5wNGZASIkdiWir0TDC1IQNWIWhsT0N3oSTGtBCEK3EQWYo9DJJyOS15hijLcNaEhFLAtzHgTPOiedJJHoM30FTm5EHI04aJUQIdEtUpzOElLbHrhLtfo6HWh5AlWRE4EC/GydQUqGWq0rzRb1GoIB0JE50qtSZkoXZgdBTrjjknsIbteyszzyJw2HzjXpR0/4QFCj4NMhoT0di0TaSe7HA9CBoTsWT2f2/YmKuRynJXAijyFq7yHlLDJRE3lFwiMvxQnOkaSx6c1pI2iYvocGMSoZBN75LhzH2DsJUNslA5bqDyt5GLIv8Sxi0IymND7HLBs1VDySAtDQNyKxDZSIcbvkWpZq84cU+OOVERVzsYsTN9gwezC5hRbSMKz0QQQTpBgmIh6UDkhEnJBPRzWSBtt8akLwkjeBQYk9Gnem16LLQtY/wCMA50Go3EpWmcdlhEDibvEnlYEyknQu99lmHbgNXOCKPLQ1QutEQo5uh6J6NHkwi6klYdijIFyEMb2hEaPVDdO90MmpxuRxB1QnixuyrzbwhGmgmv32Mkn/idMJmQi0RZYyHgzEKJKb0VZvLO/RWOunA3NrEfAyJmvWXb9k5gvNdnBSMN2qC6em3+NE0ekcGPRgsiU6NZNGYhyKZskllsQ0Xnb/wBHktEEsNs8CzBN4Y9y68jFRR0eX/wjQkkh/Awx3IiLSmXDQg9ASJ4GA0h9vaYY9cGnWweUWGoZI0ZauRuBo5uF6exHPpb/AOhTW/cmhaO2mxcdaX2umOrbEUKCOhnHQQt2aGJUImiAt577C4IrMEQaULYmJUy87GkmDS5EF2hK0WYJnJ3sqyy4ijZCSVQXTvD8IjY2pRDFt5Zh4FJd5hZLSa4C5EYK7VBCqElHuL482GkNj2/8ENinoZEPbQVaEJHwxjT5j7N7Lr2LKuzzqX9iUm+QPh1IjzBs/vYlkWG96VeRCiwhc6HlvdxFyPzJXwZMlYklHBrsNGRkZMsjeMzhlowZhPAEhbLY8nxMSUbP7Ilmwvir/Ak7zpMscyaLgYyeU9CkLoQuRhxYYkkZH9kZ5SCc0KvAZPDJayh9HbFr9m9OZij+CaEJoz2eCKK5HdwWAkPoeXL5Q7C0IgQ0NY1Dco5hD/4ElhMQGtCYyeD6SJEdmMy1E5UJVaZCefuDIiyFEkksJyVH0BKehyGGSDgZORMaINJhyh6TlpmsCTxiYyeUvscEpPAkDLKyY8CbNubICtEvLKBY4zHFJ1mODI7zjfgcLwKHkmShs6evL8k/jHrwMPsX9iJNkpOWbCH2CdcM/oQyiwm8nSHogXJJ3PiMSTmPLV9ApZGhbHlQk6BssdoSGRjilP7H2ENDDE6vREbeb0JbN5diNt7H20kvezwKKHyW8Ikm4kdJaOsGLuh9ENDV0UVtEDlEo1Oh3QDw9suuZMSmcn7H/vwmbTaE8h/mGnjh8nPvDhCZYgUZSjN2lNOupPUVNjASZAHT/DGKRu6S7DWwwKI2Ho/tvj9nX39IQRYU2fwKyN8/iSLrQZuRe+sSK6HNGLyRswDmGoNBaGgjpiShSXXrCVlI4Fh2/Oze+CwM/wDRDIBA92sncP8ABKCmSBFGo57FVDUeBNE3Epm9rdEL5OjvzF5kTYMG2nRIlaZAIG+C+x+oGISWFlipEhYK8pJ+Rofb+B3EpnQyfgSVDJl30INiSzA8wEQseXlbiWkTH/DEsxElG4kBFMhTZiHfZBDUzwNKvQonloS9E5P8MZhjkqFB0yBSCGOkWF3JvkcXwFiwP6kzMelJGnRSIHLtpP1ZtD4GAsI0N0k0l5ExlMzicFGFrJFT2OxL9ZYpy9Hhy6blCeUIQ1sjSFlC/hjzYlDRbadELgyiJjNqVt7KBZIiS3PVI/8Ackj9ml+SMRphpiIpK3O/ECOgl4G4D6ThL+xTZPYX0FxbsmS8QLC7jWTyOeJkiTWUIOxQoF2IEqElEAdqP0K4AdkJNkiTg6kSEMnijlsVKUQyNhw03IbVXxixhUB0ymcdGFvKvYNFNNGA2YW4vYicKR/EO49MaENgcJN4ZIHB5RECqlcs4GHkTRFxdyGM+jqfyjhPqxzEE6HumbOEkSVIi5aUMSHlDBimIIV2J3z+ia8DONLGGlaGEyGNx9iUiDnYoWh3Qk4Fyah7DbE+0RCwZIfoUM9gx7dEQigxZVj6JaB5E/Sflj0iDonYgciTCbY2mWQ0ZszHuSRDMD4sbVIaG+IVq0L1Q4YjEw+oF1g+AQ6DqTMgsIqG7kkRgfS+4x9tigdJsmHb+P8A6QnaLyC75QWLdQZ5ElFmv4RwCfATyZfyNjH0Cr9vTwUSu0cK0dH3CtqkkZ6HgCNK1uzzo7abZsMxWwCZVvJlCN+CoECkY6Ly0kuWvD2kgCVmm8OC7gf5BlJBmEsYvtEcFLJp+NBOCw9TnWEUO37G9lfJNr7GzDcj5NTWkQOVoDKbli/pc4Gqzve9KYIt5HJDAsK3Mcj5h04O2P2tFKWpJclBiwWEp9UaF9UbLbeEPcVopEifYHKVDbBRdJaNJ4QRXlKR+/QTi01eDwYQ0DG24eVdC3a0w1im0Y0hZQS4mRWJUmtCCTHKSuw20NQsdmJSKIlClsdUjBsTwY8iAEQx6qE3Acm9YeDE217vJBt3ESaQh3+iNM+SDDLemMnyEKLyOZh4YFsxtFLhlBfhNIvWgkoT+TS/Af/aAAwDAAABEQIRAAAQnfn4N8HrdU0S2VMDEHiWytb/AHeU3qGrRHu8DTIhJAIqKtCW8GBMJ01AK0l31605DC52wHnoV8iHzEgK+Sef5HOGIPchkk7Ejx7FYxqi6LMWe4aWlJx6RQpeioUwUPZHLMcPmUZsuZjsQa927qidfpVRVOu+1S3nIeCZx1bQzpNxGRfRfb1g8RWsZnMHOwEPnfspU6/E++eehlo3zQ8dW8Ds9kZCWYPSWk5FeVNcxAOP4S3KyANFOv30WBEI5XDYj/uo1f/EACIRAQACAwEAAwACAwAAAAAAAAEAERAhMUEgUbFhcZHB0f/aAAgBAhEBPxCOjkubwgais+MXs+yCs8mBaqUojAg+YddzudRVI2soUj3UqOFIE2rJ6OCIrUOx6INQOIo3Fi0kvaIqBuaPwOzkwE0SyEAEgL+5Zq3FCe3+4vHUChgtKnBFVk5mLYs1UZsPgdmheACUbGW9hUILkDS7YvYQmCcJZuXKi1FM1e42IUuJK0YqquG7iUwnCa7TrMQeYRP9wwdkQ2ziGAbWRD3m4lz9j8P/ACdQnOot1iFuApplo3GSsOTiXP8AAg3PYoVJFBgh3L8y/kjX+YF3qv2C1Z7DfxryORu37/Jth9ztEticzqDXxOML2f3+R3pGjGDAezY4ZY8w4lL9Ih33NEuOtM0g4w8RBe44sZxLHyV6ZrAWTpc+0JQxFalnSMYw+GuM+oeEHEHZgI0gOmQwofEFsEK1zax0g3OciVphLxxAQZ9aUnD1h7xNWJ2wncJ1jMSvHqf/xAAdEQEBAQADAQEBAQAAAAAAAAABABEQITFBUWHw/9oACAEBEQE/EA71LxeQuyANYFIXVg3UHbJl0a3eCAHuRsoJHdyfa9wricHZw/ZqdyNkLYBK71Hcgsowso6d3RPAw27BZbZASxkQpmaq0Dah5OID42DxeLCW6bdU0m+JL1anRNpJ+zayyDTiAzqbYHCmAdMsAT6yTkjNYMj3DgwfEaw9kfZcMLkD7OjtJwPuwepYsvGP8szD2UTCNDkwyEHLNl6gZthyxQTfLqnwS7SWvcHfFqPvk8D8zhn7H7LsZHW2qRPacSN5x6n2/wBno2FTWzu2j8gOQMz3pYs+z1WLcTHTPALereSuS7MOsZ6dOereL0Xl9hjn4lBC8W9ZZ+xe3++3QEdhPsT1Dpj5k0VuN42YixRvyQ1Edz7fG3jKxMyXpPmSxYGPDXyUN+LxltBGm2on8h4euCZ094A7ler7MDtCEfb2A9lPGfItg+t9Idy4QNbfs9Sio+ceSWDPZfyy7grkMb1b5ngGvvs6I84u2GSHVncmeXi9ePKG8z//xAAmEAEAAgICAQQDAQEBAQAAAAABABEhMUFRYXGBkaEQscHR4fDx/9oACAEAAAE/EDZLhKbErojKxmz8KI5DalQdI1fgYhF1+EFiYYsBmrYCAjXJphUAO5sxnTEGotXAYaSxdu5qLrwxJfAmUoyt8xEOYw7JUzRm6gTmKoVC5a1KUS8Uq5i25p+K/DheZTRKm6BzFGGKxc1HDGAXjMNcn1EJhDyuKHcJcKzAYJmkoWwdygRZalXMEMZRAxAmpY7jrc5CeRLTXcdr1lh0Ze5xEDM2hxAjBY2hOOpkesQcQbHMdkAGVhN0rF1eMafVl4L0v5XiNXNujQ5vquZZwNrUjs7jRug1CkIClalBRfvN5dD1m31moUZqEaiJMsQMkuEmSUXNsECueQoA2sqRaujHHT7MKWq9bZhDfBxLMwnbkxDUttAd7ls6TFD0PD9M3/g/ADhgFYt7HgIJjjuUiZdwYMP2SR9GvSXozuHed+otOILtFaqVHp1abylC3cdRtSky/Jo9LlicDdYlJboGCBS1i15IyUUjS8sCIBpdBiOipodppvxp9pRbVjplP4ReYHqILZIktFjBhjiUDup6WQgs3BdzTQS4wj5Ic+Il3DpFIjRmoSBeqLXumYAaH1Of7AbjwbxKASt2q66mYEaOcSSWDrEFjoD5IsKKbl2fUGAGXNQs7InzctwkA8eIcnicSg7TwwZWMQWLYmGxVfiBiDY6sjq1NvtHDHWkS2jAEVaUBV6xEWKNeZUgpTVRU7dtFxOZMnXiHDEoeFUTogw7/wAFDUroTEtVZjXLdSsTDxFaUq3FW+uIVnfOJrn4ApFWgV8SlYDiZvD4M+twhUHBp5TNRDsDo/dW/G4mEgRak9JRirKZ8JhOyDB5R4jKoAMAvK8EyzDXtDL83K/DOWNQRuKO4jZiCxbr5mR5noSlKJfJMxW+DmBQ4TiWsTJmKnEeRaDYyjbRLd2wIAlS5Xtjm4DSxVS7dQGG5WCHlqU4METurY3E0z+WHG5hmsT68pVldwE89xKgLDcxrx5nMZhhWFxWwMYoVW76N4zKlzVVzLDTUHIfDmbcRg08dR+4l8MalEKa1dQ0GaKWSxK3a8xMkPpXHgm0udksZbpplpg+8y9GWQAlZlcGkzn8AZZJUZo1hKm7lhAVM2pdTIWrwj/wfeaNBh6FPEAyPZ7vaXCFdtbLXTFp1pA38EDaT3RhBxa7GYzpcFecwzqV+MXMsYDD1Df3xFQZzndy0KmdwYXCQ9JboZ55PxGRgixxM15jT/QQBeKvaMilOTmYmWVu2oqmFoNu/jqWDYz08qe3kzMF2GGfxn6lnRyWj6rMR95QSoJXca7TK/qC/ZNAozYlUyoxvCi+/wAFWjWigO1jgCpFPf5emPMSG0xQQ0OFX2ylqKx0wAx9Ve42LBKgfo98x4KIBQBLb7ju1CXptcky8KtVJ7wTeXS/O/uFNVI2P7PuAGtQ7ns5mE3PUuegxHj6S0V6DUx0xRIHeXZlOr8Ih8srFFhQTzz9riRBOLvcNr1mGMHGDDEdg1qcgKpzUeRoRvINwdDqJvLNqw5ay3wfMZXVdlXoTXR40xFjTm7Uor5gwhQzLRxA515a5eoOzU4u31fWDYo1E0W3R5hcplfmJoeXrRs+Sk+JTsgXDhh+CIBcORfJcoucQ0TaImY36IWEuvthT2twQFQRVBqolQOGq6naT4wzh5APmWsDTkeQvp17wRpb0pqnzNgZ+T1vMoGJi3S7V1riCAoEpSkoG8oHkMf6zatGtSwWehUa+2Kl3d7yyy1JfVL+x9x8REHzLA9NTG5BgVXvB8onaKyUw09KXoC5cXNUXlymFmaOYb1W2C4LaWR/7UyyREJcQ7/uZio9rlPWx6AH7TJ0O5pJoHywfuOFdI88TPtCqxDzahHxl+2WWOmI1Q99BiVQRjkSyUN4MWFnE2ZXlTFSS4YIeIYRUmCr2RAflfcE/U1buYDzucIurfqYK5/qImq++WD7ZZcqv7SLhN1Kd18Nr/J4G5otiexj7X8S6HEjbc2fEWzsP1Wba3Exb0ZZeQ3l1+qmg4ZQKn8QbVHmeSY6ZSpPYQpKsQ9URVGQXOkc/Uw+ErxhpM/B+qFugH2QFLXscy/RMDZFegQW3GorDVT2K/bO3XpFdQaVcFH9Zfk1d9S6EwBU1ripN1NOoK9GYN3mgdAf0YhRvwxXA6llMi3KMpZYY9TCuuEEe6Tsu36hK6kUeSWrnJcVUbGvSBaXlOb1fRLZbY8qB+mZkA4EOsW8wv2Ijbhnemv5FUBXd6B/7xHZlCjWCVFhwGEBbZLcNgVXP3h1GWodn8nA5q+in9lCxczjI0YxU8ceBSCmyKq4giWQXITUkkegB+2ZWue5feWKG8mYATv5QhJHRGSH021b+4IJjPeiVudC5cIwp6qyiH6Qj6v5iX6nBzn9IqNGJmmbJl8xZYVrEWWCXs0FDtEf9gBll3OQ49YLAlChqFHiFiltHxAvSY9ZSMUGj8zTOSTI9J9MfaeUYY6l1/QaPogeuBt5LgXX+olZwuXzNLl3cHB8Q7Wtks0xgLMC8qYDV5T9TO2YjweZcTmvdwRQGCKxxBY7gUIKlD+ANcTAO4hLZPwuX7lqDkJk7ZcWEFFTNroYgKxQ9C5pAFNbX/syg4j1/wCIDOMBfpUQOyyrr/2IAKowQ6CX2RIeUs9oBTa0sdVW+pn3dViWmKuZLyfJRTeyb5i+aKkbX+MKtO4IbgJVq4m9KfoNH6mXUNdstwgFxWInHrHXi/4mqp+0H+wGXCp8xVGH6Rf2YSmBPCi/q4hpB0nBoiUVdzZpuOiY13CI95glusiQFNK5JzTxqXzZ/YphzGUqIXvMuY53MeFi2kGzmYAEPsXLwNo/duZYuu9LFawNoEoi4g0lZzQIKwoM+bH/ACGrAW66gBKpfB/2cCAfVNyzagV6ztLVxbPDzKihmbEcJpbldjKkZ1O1zKA3WfMVpq5qM9yofMYL5fwI5rK1BLQpnqlf2DaKumWDCCoD3Hom7CH0/AomwQuU4TxiWCzbPQD+wXKrcpK2Ffwg2QoPIWfZLuiFryu/Eoq+xBaVCHC+5aUzazKDjdwX4DcJMIb8GcE3/pKIbXKCpPE4/cuGUSJuNwbOZYeinfNiNso4XcY0h2nvEUbA3XpBvElFo/2lsZbnuwHRdRVud8/8QEyCUxwwU/stRDq+5Zec+ItluCKmld1yTBtkfMdYaFVWAh8qpYCy/qQlphuJaq1CvujbJOiJkBpLV/KowXb5ZgVhVDLYdjBr2Rwz7gZRc9/gn9dMiwiHyQdhIrjUPuU/YylXviJqBCm+59SGloC9p4YG+Yy+Fp6oXZH9Zt9YjJfqcRcPWUcdyml5cew4lRaMHg4PqWUgrl6mHtlE4ww+2I3b0hrL50EUNQow6NvXMJScDATbDFbahBirmxTDXI2ftls05j4D1mEHHEVsPGWbWLK16MqUE6qAJwaXmcvmn4QKqcxg5+ZR7owVUSxMQWwhscFecD6ILNTweoSQ2YsjOD5ai2IU4tYNb3EOL9aK6PqHwSxEjSKGLJ+zzqHiWjtAOw+kva4IODH0fqVkdoFgKEoXAVH8gHvcQmrsLj6m14mddOKl8AfXz/JQwzCAMRIV3Kt5TLE2W4cjAIvi/wCohpWClqupavVCoAlFpVpP6hbQdkZoHepGLQ/JcKLR46gIRazYS6eOgUKUscjuJllkWnLi5Xaxosujm4I+cQlh1DdpFhZK1yJjwt6hfXEjimn7SGqOppvcUcDPUpjpTJNjxYv0CAfKxnpNFbpYgY1gTC3vLdWsWHxB5yhEp4zX63G0Ic3VB/8Aserp3r+CJOH3/wA1BVKLBA6iWDyjeHf2SzjSGIiCKklHYp8s8THMtKDLctzX3M/sUrVwrBE6Z4OZdalJDNOZS2SkJi5hIgmfKPlNHcQSylRAnvLy4b9RMtpS25NHDiazRWD2ZZLteBpxBE5+yPUaBJgcoXnPBEHUxTtjsSm3dF+4whJeHT3GLNIfI/5LJjXwyhXLZFBd7idVZRlEgVMRNT8QM2TzeYouzdergMlcsSubQ+ckqR8fuUG3qZgld1UwqjVqH7ludRY788Qou+i6Dy3CLnF4f9h7UsHcxURVCsnKCBZcE10RLFJ5mRh5gEcmPgXFCFN1u7lkAAbCswiwKFDxCkdXmCMFcI7j1TRYwahoVYOZRJRAc2CGSpgOgB/IqfQSqgyqMleGWFtAFuyVPGeDmCrrC1uWKzu5ZKs571FdCPQyoeG4udWsKjrmIM1CoFQt3mj9QZqdMMJoKohhW/cSg0BH4NLqNCH1YtKLjb0ji7Hl3GQYisuWxdFJhj7qerP2Sou/5hjmYa63BEFLKgtG6L5jkVlVs0w8kUtBri/MVxVVizPSxAp5r7jszVcRUw1zLTf6KWCUdEAJhnB4iBYDvuImQQMk0nMrr2RCto35lCWKU3Xcf5sVarT7qVx6/pFYu1mAwl+QPLKYu6GB9zUKueM0TBr3DFR4sUfEGHMxjJEFzSK7IsaZRiekc2V9n1E37SDkf+T5ugguuJlVQ7BdLh5h9HhgAXhUfyNQFh1EBeWZTARnpbUNXUVXUtLA3VQR9PvBvPpLvwfSy/eIQairIfa5UZnkxLK605i9/nXMPP0q6gALugolIAgX3iJkeZUljiC7oestt1tYZZAFPJMKMPytiJIAkOGWWVzsnYv0laYGwWtoljkjn6YdC7oeQLhiyxXjMPVKDOcFo8TNhZarXLmZVVYNaR6NSwtcOZ2qazCVBpwf5EFqe0cT6Y+zOsRCxfZcrEJnYmDd/wDyFPJiDm4alYQroibKgM0Bb+xoGTMQAydpojzD/9k=", ct);
        //    //att.ContentDisposition.Inline = true;
        //    //att.ContentId = "filename";

        //    //mail.Body = String.Format(
        //    //           "<h3>Client: " + data.client_id + " Has Sent You A Screenshot</h3>" +
        //    //           @"<img src=""cid:{0}"" />", att.ContentId);


        //    //mail.AddAttachment("YoutubeImage.jpeg", "data: image / jpeg; base64,/ 9j / 4AAQSkZJRgABAgAAAQABAAD / 7QCcUGhvdG9zaG9wIDMuMAA4QklNBAQAAAAAAIAcAmcAFDU2QWRyQWJYclkwRXY1aTFoX0IzHAIoAGJGQk1EMDEwMDBhYzAwMzAwMDA3MTA4MDAwMDJiMGYwMDAwNTkxMDAwMDBiNjExMDAwMDFjMTYwMDAwNDYxZTAwMDAzYzFmMDAwMGI4MjAwMDAwNWYyMjAwMDA2ODMwMDAwMP / iAhxJQ0NfUFJPRklMRQABAQAAAgxsY21zAhAAAG1udHJSR0IgWFlaIAfcAAEAGQADACkAOWFjc3BBUFBMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD21gABAAAAANMtbGNtcwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACmRlc2MAAAD8AAAAXmNwcnQAAAFcAAAAC3d0cHQAAAFoAAAAFGJrcHQAAAF8AAAAFHJYWVoAAAGQAAAAFGdYWVoAAAGkAAAAFGJYWVoAAAG4AAAAFHJUUkMAAAHMAAAAQGdUUkMAAAHMAAAAQGJUUkMAAAHMAAAAQGRlc2MAAAAAAAAAA2MyAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHRleHQAAAAARkIAAFhZWiAAAAAAAAD21gABAAAAANMtWFlaIAAAAAAAAAMWAAADMwAAAqRYWVogAAAAAAAAb6IAADj1AAADkFhZWiAAAAAAAABimQAAt4UAABjaWFlaIAAAAAAAACSgAAAPhAAAts9jdXJ2AAAAAAAAABoAAADLAckDYwWSCGsL9hA / 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",
        //    //        "image/jpeg", "inline", "EmailYoutubeImage");
        //    mail.IsBodyHtml = true;
        //    //mail.Attachments.Add(att);
        //    var smtpClient = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com", // set your SMTP server name here
        //        Port = 587, // Port 
        //        EnableSsl = true,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("elicampswork@gmail.com", "abcd@1234")
        //    };


        //    //using (var mailMessage = new MailMessage("elicampswork@gmail.com", message.To, message.Subject, message.Message))
        //    //{
        //    //    mailMessage.IsBodyHtml = true;

        //    //    ////This attachment is only for AuctionWon Email
        //    //    //if (message.HasAttachment && File.Exists(AuctionWonEmailAttachment))
        //    //    //{
        //    //    //    mailMessage.Attachments.Add(new Attachment(AuctionWonEmailAttachment));
        //    //    //}

        //    try
        //    {
        //        smtpClient.Send(mail);
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionHandlingService(ex, null, null).LogException();
        //    }
        //    //}

        //    //return true;

        //    //try
        //    //{

        //    //    var apiKey = "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";
        //    //    var client = new SendGridClient(apiKey);
        //    //    //client.po
        //    //    var from = new EmailAddress("elicampswork@gmail.com", "elicamps");
        //    //    //var subject = "Sending with Twilio SendGrid is Fun";
        //    //    var to = new EmailAddress(message.To);

        //    //    var attachments = new List<SendGrid.Helpers.Mail.Attachment>();


        //    //    //attachments.Add(new SendGrid.Helpers.Mail.Attachment()
        //    //    //{
        //    //    //    Content = "data:image/jpeg;base64,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",
        //    //    //    Type = "image/jpeg",
        //    //    //    Filename = "YoutubeImage.jpeg",
        //    //    //    Disposition = "inline",
        //    //    //    ContentId = "EmailYoutubeImage"
        //    //    //}
        //    //    //);

        //    //    //foreach (var attchment in document)
        //    //    //{
        //    //    //    attachments.Add(new SendGrid.Helpers.Mail.Attachment()
        //    //    //    {
        //    //    //        Content = Convert.ToBase64String(attchment.DocumentByte, 0, attchment.DocumentByte.Length),
        //    //    //        Type = "application/pdf",
        //    //    //        Filename = attchment.DocumentName,
        //    //    //        Disposition = "attachment"
        //    //    //    }
        //    //    //        );
        //    //    //}



        //    //    var msg = MailHelper.CreateSingleEmail(from, to, message.Subject, "", message.Message);
        //    //    msg.AddAttachment("YoutubeImage.jpeg", "data: image / jpeg; base64,/ 9j / 4AAQSkZJRgABAgAAAQABAAD / 7QCcUGhvdG9zaG9wIDMuMAA4QklNBAQAAAAAAIAcAmcAFDU2QWRyQWJYclkwRXY1aTFoX0IzHAIoAGJGQk1EMDEwMDBhYzAwMzAwMDA3MTA4MDAwMDJiMGYwMDAwNTkxMDAwMDBiNjExMDAwMDFjMTYwMDAwNDYxZTAwMDAzYzFmMDAwMGI4MjAwMDAwNWYyMjAwMDA2ODMwMDAwMP / iAhxJQ0NfUFJPRklMRQABAQAAAgxsY21zAhAAAG1udHJSR0IgWFlaIAfcAAEAGQADACkAOWFjc3BBUFBMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD21gABAAAAANMtbGNtcwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACmRlc2MAAAD8AAAAXmNwcnQAAAFcAAAAC3d0cHQAAAFoAAAAFGJrcHQAAAF8AAAAFHJYWVoAAAGQAAAAFGdYWVoAAAGkAAAAFGJYWVoAAAG4AAAAFHJUUkMAAAHMAAAAQGdUUkMAAAHMAAAAQGJUUkMAAAHMAAAAQGRlc2MAAAAAAAAAA2MyAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHRleHQAAAAARkIAAFhZWiAAAAAAAAD21gABAAAAANMtWFlaIAAAAAAAAAMWAAADMwAAAqRYWVogAAAAAAAAb6IAADj1AAADkFhZWiAAAAAAAABimQAAt4UAABjaWFlaIAAAAAAAACSgAAAPhAAAts9jdXJ2AAAAAAAAABoAAADLAckDYwWSCGsL9hA / FVEbNCHxKZAyGDuSRgVRd13ta3B6BYmxmnysab9908PpMP///9sAQwAGBAUGBQQGBgUGBwcGCAoQCgoJCQoUDg8MEBcUGBgXFBYWGh0lHxobIxwWFiAsICMmJykqKRkfLTAtKDAlKCko/9sAQwEHBwcKCAoTCgoTKBoWGigoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgo/8IAEQgBNQDOAwAiAAERAQIRAf/EABsAAAEFAQEAAAAAAAAAAAAAAAMBAgQFBgcA/8QAGQEAAwEBAQAAAAAAAAAAAAAAAQIDAAQF/8QAGQEAAwEBAQAAAAAAAAAAAAAAAQIDAAQF/9oADAMAAAERAhEAAAHT+cvVaTICd6BhnFkYRr5g7wrU+DLGNHciwX3lTaQWKeznY8IdkiM9kJFLGUkiuagcr2WWQ8LmYr0LNq9fJpyyMeKNj0cDJpXVZ0E9sgDbzmKwI1PHKqIuUL2IU8aQzCCkiiucrlrFRW0mjn+QLzq65rJWsesFKom49gs+Rddszfe9s4jC0xop45YaOSAVQq7FIJ1lliG/OxJA8ovPQjlFMs3hW2vLibHqxmR6rkkbF9h491vq4rlzPVYnke+Rrhrk97yELkch8UTutZJEXO4KiOIrZAXmM22mcF+eX9+SVOdTdQVXwPVcduevk97y1kiP8ub5W4+RfDBchywSMf0qd4DBxeI8gTiDBgUGwx/LQEYweW6nr4aNotZHkenwkD5GVfJ6Qc3yA+VfLlLDLVmE8t1eeOZaCPG8UlKEyOGhFUzWBISk4emXdxt6y8r1WPj9HN1Q/ObOg2jqG0YyWuRAi+VSJzSUZSjlWaP4wMvve50F1OSryQV+lzG1UUBtViuftu9Wyuvy4qLZxyjSxJwIq+wq1fa6DnXRrBF8igRo6s8o4D2fwldljcp6dzCcnvYSQd0bnfTiJ3PtrzaXZ1XN6TKV46IEkONVZ0NpN2xJIlaJ2Pi/T+gWiKmWO5vpvJPFN2ZXMIrV3MOi4CEwyIMxUN1jk3XcKDHaCmj6PTcNvOeX86PDkNxzUmMWVLAJEOq95hNVQ7R7j2WuV45sT3mXWR4BcMtk9HmecRJ1ZYoJvWeS9XOwZs3f8/p9E5x0Lm/T5cQkOQrU0WfTzfQEjy6JUX1Bal+lmC7oAmOIGjGA9UaZBHY2hmworV2dXZgWnS+bb3PzvS5bXc/qabmPQuY9Pk2TWtGh0V7To9vZ0d9RKKbFKKdTcx1Ax4Tu7QeOkvMGEbnLfOQVk6sscL3X5u0FsftsHuY9svAdS5RbzpfkQEFVawFYOkyGqrKveg1r1nzmUwSgJmYZY+lJo72i2wYJMdFjTquz22PkGK5PcYLZx695yfq3OejgqvI6bih2FaGgXtPdViONLAtesJ5tdGkRDzczRSnjGqLjJDZ+qtq5Vi2ddY7auVXbsvxva+2UeiTi9hR35MG5pZ0j1s2rUv0ef0NJx4UpM/TG2Eboetc18D46AMZeA3/PQI9Da1ADJ8KYNp9/wLTbdgDgIxPRa7ndFtoEpZM6Di+dhI0FFfURixj6nVYZI3VSAUUye8FkiUB883XPMPUtjWBSSI5Rq6XFsNgM0LMaItsLCD40ZKe95NrK8q7Sy1drUaPU24WTOulVaNgq0WUFnLzLznpnPBoEG2MBAhT67KCRHn7TjwiEtGeZtCqruiVnscIDRzADfQdxhehNWynw3dLljgcS0YZfHzvpZ9PO41zE+VK2rt6bo4mSgzgGPcDMVo5ZwYd5Rg+nQ7JWuq7RR49uf3NXeWhMWBJuAyYRGIZUWTGDaHQZpLUWxx+zmchn9JnLcpZIZR0Pw3jTRzIZxYkgQaNaVNyj3VfaVvN6Npd0l11chV8mULmuof/EAC4QAAEDAgUDBAICAwEBAAAAAAEAAgMEEQUQEiExEyIyBiAzQSMkFDQlMEIVQ//aAAgBAAABBQIoJmT0cggVqR39zDlZOFsm2RUltcoaHHIJidwiMgrZAJ9r+wJpV0TldEqTm9zkECiU0JzUUE0K2T8h7b53yk5aNkR7GlEpyCYiqvFoYHw41BK+apZCKOrhqSRb3AZycx+DWgp5BVkwItyvkFwMUxd063QvcueWwTPglwqvFdTH2BWFnZScs8AV9ttYbIlO5QbcL1FU9KkCJQ4IsXc4ZUfxqs5hBXT+VIhnH5ZEezHJjLiLVS4T1QcHiDarC26ZoXROcsOlMtB7b5PzGV0FZWy+6y/8qjge9UwsFIsTZqa7d2DD/F2yv7LJ/sCDdmp2dlj1ForqiHQx0k7Hw/yXUb6mpJpnPc6ppXvrqeMQwe+T2N5AuhsnnIILHT3MsQ+KNqt+BsDHgtZG2k3xn/Q/ho3Lc2GydkBkFW0oqR1bJsmt8pqA2KbQnT3WHUrxVJzbe9/EBs95FwhlZOFk0IjPEGdGeOJjk9u5gY0F+p9I5jqb/Q/gK+QTcimO2VkVi5Bgjqemv5wU1RrGE0a/kS0VfBjEUiimjmyHtdwzm+YyOQV9qzFI2k9WoppY2vT4nA4VhvWNrCcapG9rgo6uojUOJMKjqIZPY7xZymq3suAqnFIIVVVk1SiQG0k5o8MhqKeVg6DVhmLGSasdopNF2vi0Jg7U4bT7LB6ozwZO8WeWxTQgnZ4pOZ6kBck84bF1YqygimhooOtUwUsMDcV7KJSpp/IeCbvl3WCy6K7jJ3iMm8oop50tcgmIeeFC1Go5WjGVjJ2Tk46aku7I/FyidolNkQneIQQX1fKv2o3fH/zGmedC3TSVsnRpaQfmZxjBvVo+d71Mx0xxf1ncLDJerQXTvEZBBOTViu2HOH4V9s84BaDHXWosPF6sLETqrgmlN+eod+tF8D+Dx6fN6WyPigEEMmFY8+1AbdFmUXmzwx9/5MLH7o4nOqomOmM2ayTtnqrCnp/glRXp5/eN1sIzkUDnj77h20cR2+oOW+OMPviOEAGs+uVI/VUPKqeah92Uh/DInLATauR8TmU05Y5883xQocQKM9tc8yVnp4k1Mhsx7tEVN4lTi4kPZRH8MnDucHNsRujwPK1y05FNN1i79VdP8UPkFTN1STHpQyju9O/NVPtT1rrpmwKkU3OH7xuG0nOFn/II8R+bvJwTTlwal+urn2YzZ4WH91diZtQay4eneMSIZSA65U5PUyw47OPbLzQG1cCj4x+TueUdk03T9m3uiWuc8/mCwUasTxp2mkOxwD4Mev8A+a3hvCcpxtRmxv8AjkVD/eIsj4t5ubsKK4NY61GOC9sTAS6VenW3rPUb7NcV6eN6WuZ1aRoyKcpBswlro3iSB6pzapuncDlOFk03DhdYo7TQtU41P/8AovTvl6l2eV6aN4ip2dOa+TlImC77iOaTmP5ijx9go7rggrGW/otU57o/k+vT7g1epN5QV6bd3ErF22rvqyKk4p955W6o37th3qSjx9hN4cE02WNPtSAKU63s+X69PfL6iYSTz6daeqsdbZ+T+Hqk/sMbqbIwtbT/ANtFfaYcnBYzJd52Y9oYyPzXp49zo+tG7ApDVQQCBhusVZ1aUHcbqd1gSqHeobspn3VE29foUgsncp2xYUViJvXucI2yuL3xcndYHPpDZHlCRGcBdZyqKl7Wu2dE7umdeZ3lQfPe6c6yw868VCmR5ThsNiFV91fU7NeocmPcx1NjlXGh6gajjzyp8Wlcp55ZjTv/ABl6bu5yoOW8TlYHvil09HnplBPCjO8h1VNSbJxUXB8WpjNZLC3JrHOXQcowAnlMT/GhHaBtNzgLf22vGlwTwtYUbkVL2tYqt3cQmiyf8YUZDk27k9jEItnCMHXqL03iTijHY7iVenxaNM+Odo0lyOxBuqsfrt4m8ra31gtWS/Gxad6d7gnPUjyS2O5eyzXpqdu+j+OR1hIsGjth7TpLD2zO2Gi7txGbGVuuHoyNdJFIVhlO84jWf3JvBiav+k0bjZSnsdym+cAtFJuZeML2w/YrhshRTDcPCjdcN8q6pEQpJP2Kv+5Pw0INVytS6tl13FBpIPJ4w6ETTOo3tD46hqjglnfSt6EOzh/y7m4TDpcdw06XOvqkPXqmseI8QGmpl5Yg0FdNaEI0xoVtvt3jhTtM4maU6oZehdqer2V+x3kU9Rm7XBPdZYZ3ywNBZjLdM/20ItIT3vCi710guE1ykbaR/GEtvPK25I7KDxaUVw0+T1//xAAnEQACAgIBBAEDBQAAAAAAAAAAAQIQETEhAxIgQQQiMjMTIzBCcf/aAAgBAhEBPwEQ3SZi4sYmNjtVFcDfkjGaWrb9Vozm3TpvJioruZ2JE+knHIvF0iTMZOnFIhHBGGGdVJS8EO0uRrBCXJHlmScu5+LqLGJmV3CiyU8DM+D0KI1is10mpxeXonJN8XG3og/RIdM+JDKk3bIDp6NC5J7pnSXZ0M/6KmQHT0ZpvNMk+3odoqeiJKnrwRFZkkfLf7bwRp6EOnrwR0Pyo+Y/oI6r0IlT1bEdD8iPkcwI16pvKr1bEdOXZLuOt1otYQqzxS0YPXgq4OBcqkIkY4/gWj1a5NLisN6HF+K0PQhbFwS5VdDY3nK8UQX1o/RiycVF8V6ro7P6j3SpEPvVT+6vR//EACQRAAICAQQDAAIDAAAAAAAAAAABAhEQAyExQRIgMiJRM2Fx/9oACAEBEQE/AYkuRrbC3HvsNCOhDVj2LtYod2PnFpHZsJo/w3RORFLx9dSXSwn2hStYjiRGdIW5VCLJvsTbRGbUqIllnkxiIjEhmr9Ehs0la9YxYhrssuzXhtaK2xppRQ8s08MatD2NR/ieRGNkW0eRt1hkOCTFuSlQ5N4lHxZFVzixkd0MjKiRwajwjVfGU+SRpksND4JYRLeeUSNMokqYpdFk+cIW8/RrYgWNWx/sTseOjT+iWOzohiHBx6S+TS+ieeiHIyLOfSfwaX0S4yuCOzGR5w8teUaIQd2x8ZQvoaT4O/VM8jyLLEL6KoUblhjz4lD5xVGl9j2KT3w2WvWXIsaPIv0f1jUFz6IkPg82jTk6sUyE6bxqcEP5MLLHxiPzhdn/xAA2EAABAgQDBQcEAQMFAAAAAAABAAIDEBEhEiBRIjFBYXEEMDJSYnKBEyNAQpEzgqEUYJKxwf/aAAgBAAAGPwL8caIYf9gYfEVheCzmsT3b9yww3jHp3ZlbuKowuzHDC83Fy0kA4l1BQckIkI7QW1T6zfEP/e6d3FZCC07UX/rNCf8AqTR34kTRmwEFV76clSpWw6625QHu3lv4cbFvxlF1LAW5oTsKy7PXTva5mRB4Yxv1Vg76Y4N4r7X1Gild6MTF/K8T6VpZeJ51BX04IqXJkNu5op+HAr5wrrFRG1luWyEOn4lzuvLadfgFgD3H1LadV3GX+p/V0t/cX7uv6OuEXPaCdVTBF/5LHQ4lZMME4mU/FY39q2VCFuVNyEd/9oUf6DrYzs8CvuQ3s/yvtPDvwbot7OPqO14IdoJxOhRNr20VZfViikHh6l0T3auJlUKz8Q0cvvNLDqLhbMRua+baNAqQ/uu5bl911GeVu5bK7TFa3E6tGj4Q2sDzvbSwRdFLXU3Cq+jHbsnwEDw9VFd6VQq29WyFjzts/wAjLbMRXYbYCYTa+FpJ+U5rWhruBTILx7kRCYAiG7nOApLnMDS6KAO5+z3ROgTTrWYTJfVZ4TEIlAZ1dKqH8Se74kxw4GqqOPcxz6SofXJCHJRH6BM90mjysk0f3GTimc7zhHiBh7mP0R5XkJMGjQsPmKhDnKLyoJPeeO5FE6pnScRuj+5p5nAJ3SQkFCZyqoXzKK7V5RpvNgsIXVUTJxm8gZc88BnUp3RCYTvSAFXQSrqg3gy8g6QmRqyQzwvajMe4IKK71J1fKn9FU8Ai473XkcsP5EwjlI8goiqSgN8z07kKyinRtFFPpKbDHHflPWcD3TCOWM7V0hLsrdHKORvIoJdod0Ci10Tnn4ymcD3dwToETqm4uCAkz0tJTG6uRoop1cn01C6Zqyge8dxGPpW9En4CxO3mUR2jKLs45kyf71FZq1DLUKolCPqHcROeWMei7P0Moo9Unt0OWnNW8LrSZ7h3DuoyxeoUD2mUcdDJ/qvkKCBHBAqF7h3Ab5nSOiEotVAfTZGyZRXDwkShv5UzUV1B947iHD0ujqqcUTKJyRa8bJVnD6GqwQ22kdWmuQo9J9m9/cP5WWIivJYjZGRYxgcV4ZcFbCflHHCFOqtuV10RRPKfZ669xG90zLFDcWnUIB5EVuhC/oO/lWgD5KsyGOirEeSiNJEyceU2HQHJbJEdq4oWujIyp+3BXErNW1Ro5o0JOR04jvK1U4zvNx5SAldOlQ2KoQvDfkqkKrr8luo3Ies4z9TSQVskT2mVU1o4mijAcHUyUK4KgV0ZjI0+Ykq8qL7lZvaN5FEW4SaaL+m/+FBxsIDdq6j+8oZSZOyNnBpoqFUMhkPVBQn12XWUb3lCVirz2QiXG8yHbqL7cU/KuAVhw0TIda4eOQVVJkhU0TQ2tzbqq7i8YiOaEt0t6ucrui5ScaUzVm/knPO9FtLYqplPLOzit6vMIycdAqcKI8k4/GUL/8QAJxAAAgIBBAICAwEBAQEAAAAAAAERITEQQVFhcYGRobHB8NEg8eH/2gAIAQAAAT8hFGWIhFkZCHKDDSa0RBuRJitkCwblFrQsHcxSJQinRLFsjgNyyzRBCGK3ERknPei03HjRwNvRQGD0Gymx5wNGZASIkdiWir0TDC1IQNWIWhsT0N3oSTGtBCEK3EQWYo9DJJyOS15hijLcNaEhFLAtzHgTPOiedJJHoM30FTm5EHI04aJUQIdEtUpzOElLbHrhLtfo6HWh5AlWRE4EC/GydQUqGWq0rzRb1GoIB0JE50qtSZkoXZgdBTrjjknsIbteyszzyJw2HzjXpR0/4QFCj4NMhoT0di0TaSe7HA9CBoTsWT2f2/YmKuRynJXAijyFq7yHlLDJRE3lFwiMvxQnOkaSx6c1pI2iYvocGMSoZBN75LhzH2DsJUNslA5bqDyt5GLIv8Sxi0IymND7HLBs1VDySAtDQNyKxDZSIcbvkWpZq84cU+OOVERVzsYsTN9gwezC5hRbSMKz0QQQTpBgmIh6UDkhEnJBPRzWSBtt8akLwkjeBQYk9Gnem16LLQtY/wCMA50Go3EpWmcdlhEDibvEnlYEyknQu99lmHbgNXOCKPLQ1QutEQo5uh6J6NHkwi6klYdijIFyEMb2hEaPVDdO90MmpxuRxB1QnixuyrzbwhGmgmv32Mkn/idMJmQi0RZYyHgzEKJKb0VZvLO/RWOunA3NrEfAyJmvWXb9k5gvNdnBSMN2qC6em3+NE0ekcGPRgsiU6NZNGYhyKZskllsQ0Xnb/wBHktEEsNs8CzBN4Y9y68jFRR0eX/wjQkkh/Awx3IiLSmXDQg9ASJ4GA0h9vaYY9cGnWweUWGoZI0ZauRuBo5uF6exHPpb/AOhTW/cmhaO2mxcdaX2umOrbEUKCOhnHQQt2aGJUImiAt577C4IrMEQaULYmJUy87GkmDS5EF2hK0WYJnJ3sqyy4ijZCSVQXTvD8IjY2pRDFt5Zh4FJd5hZLSa4C5EYK7VBCqElHuL482GkNj2/8ENinoZEPbQVaEJHwxjT5j7N7Lr2LKuzzqX9iUm+QPh1IjzBs/vYlkWG96VeRCiwhc6HlvdxFyPzJXwZMlYklHBrsNGRkZMsjeMzhlowZhPAEhbLY8nxMSUbP7Ilmwvir/Ak7zpMscyaLgYyeU9CkLoQuRhxYYkkZH9kZ5SCc0KvAZPDJayh9HbFr9m9OZij+CaEJoz2eCKK5HdwWAkPoeXL5Q7C0IgQ0NY1Dco5hD/4ElhMQGtCYyeD6SJEdmMy1E5UJVaZCefuDIiyFEkksJyVH0BKehyGGSDgZORMaINJhyh6TlpmsCTxiYyeUvscEpPAkDLKyY8CbNubICtEvLKBY4zHFJ1mODI7zjfgcLwKHkmShs6evL8k/jHrwMPsX9iJNkpOWbCH2CdcM/oQyiwm8nSHogXJJ3PiMSTmPLV9ApZGhbHlQk6BssdoSGRjilP7H2ENDDE6vREbeb0JbN5diNt7H20kvezwKKHyW8Ikm4kdJaOsGLuh9ENDV0UVtEDlEo1Oh3QDw9suuZMSmcn7H/vwmbTaE8h/mGnjh8nPvDhCZYgUZSjN2lNOupPUVNjASZAHT/DGKRu6S7DWwwKI2Ho/tvj9nX39IQRYU2fwKyN8/iSLrQZuRe+sSK6HNGLyRswDmGoNBaGgjpiShSXXrCVlI4Fh2/Oze+CwM/wDRDIBA92sncP8ABKCmSBFGo57FVDUeBNE3Epm9rdEL5OjvzF5kTYMG2nRIlaZAIG+C+x+oGISWFlipEhYK8pJ+Rofb+B3EpnQyfgSVDJl30INiSzA8wEQseXlbiWkTH/DEsxElG4kBFMhTZiHfZBDUzwNKvQonloS9E5P8MZhjkqFB0yBSCGOkWF3JvkcXwFiwP6kzMelJGnRSIHLtpP1ZtD4GAsI0N0k0l5ExlMzicFGFrJFT2OxL9ZYpy9Hhy6blCeUIQ1sjSFlC/hjzYlDRbadELgyiJjNqVt7KBZIiS3PVI/8Ackj9ml+SMRphpiIpK3O/ECOgl4G4D6ThL+xTZPYX0FxbsmS8QLC7jWTyOeJkiTWUIOxQoF2IEqElEAdqP0K4AdkJNkiTg6kSEMnijlsVKUQyNhw03IbVXxixhUB0ymcdGFvKvYNFNNGA2YW4vYicKR/EO49MaENgcJN4ZIHB5RECqlcs4GHkTRFxdyGM+jqfyjhPqxzEE6HumbOEkSVIi5aUMSHlDBimIIV2J3z+ia8DONLGGlaGEyGNx9iUiDnYoWh3Qk4Fyah7DbE+0RCwZIfoUM9gx7dEQigxZVj6JaB5E/Sflj0iDonYgciTCbY2mWQ0ZszHuSRDMD4sbVIaG+IVq0L1Q4YjEw+oF1g+AQ6DqTMgsIqG7kkRgfS+4x9tigdJsmHb+P8A6QnaLyC75QWLdQZ5ElFmv4RwCfATyZfyNjH0Cr9vTwUSu0cK0dH3CtqkkZ6HgCNK1uzzo7abZsMxWwCZVvJlCN+CoECkY6Ly0kuWvD2kgCVmm8OC7gf5BlJBmEsYvtEcFLJp+NBOCw9TnWEUO37G9lfJNr7GzDcj5NTWkQOVoDKbli/pc4Gqzve9KYIt5HJDAsK3Mcj5h04O2P2tFKWpJclBiwWEp9UaF9UbLbeEPcVopEifYHKVDbBRdJaNJ4QRXlKR+/QTi01eDwYQ0DG24eVdC3a0w1im0Y0hZQS4mRWJUmtCCTHKSuw20NQsdmJSKIlClsdUjBsTwY8iAEQx6qE3Acm9YeDE217vJBt3ESaQh3+iNM+SDDLemMnyEKLyOZh4YFsxtFLhlBfhNIvWgkoT+TS/Af/aAAwDAAABEQIRAAAQnfn4N8HrdU0S2VMDEHiWytb/AHeU3qGrRHu8DTIhJAIqKtCW8GBMJ01AK0l31605DC52wHnoV8iHzEgK+Sef5HOGIPchkk7Ejx7FYxqi6LMWe4aWlJx6RQpeioUwUPZHLMcPmUZsuZjsQa927qidfpVRVOu+1S3nIeCZx1bQzpNxGRfRfb1g8RWsZnMHOwEPnfspU6/E++eehlo3zQ8dW8Ds9kZCWYPSWk5FeVNcxAOP4S3KyANFOv30WBEI5XDYj/uo1f/EACIRAQACAwEAAwACAwAAAAAAAAEAERAhMUEgUbFhcZHB0f/aAAgBAhEBPxCOjkubwgais+MXs+yCs8mBaqUojAg+YddzudRVI2soUj3UqOFIE2rJ6OCIrUOx6INQOIo3Fi0kvaIqBuaPwOzkwE0SyEAEgL+5Zq3FCe3+4vHUChgtKnBFVk5mLYs1UZsPgdmheACUbGW9hUILkDS7YvYQmCcJZuXKi1FM1e42IUuJK0YqquG7iUwnCa7TrMQeYRP9wwdkQ2ziGAbWRD3m4lz9j8P/ACdQnOot1iFuApplo3GSsOTiXP8AAg3PYoVJFBgh3L8y/kjX+YF3qv2C1Z7DfxryORu37/Jth9ztEticzqDXxOML2f3+R3pGjGDAezY4ZY8w4lL9Ih33NEuOtM0g4w8RBe44sZxLHyV6ZrAWTpc+0JQxFalnSMYw+GuM+oeEHEHZgI0gOmQwofEFsEK1zax0g3OciVphLxxAQZ9aUnD1h7xNWJ2wncJ1jMSvHqf/xAAdEQEBAQADAQEBAQAAAAAAAAABABEQITFBUWHw/9oACAEBEQE/EA71LxeQuyANYFIXVg3UHbJl0a3eCAHuRsoJHdyfa9wricHZw/ZqdyNkLYBK71Hcgsowso6d3RPAw27BZbZASxkQpmaq0Dah5OID42DxeLCW6bdU0m+JL1anRNpJ+zayyDTiAzqbYHCmAdMsAT6yTkjNYMj3DgwfEaw9kfZcMLkD7OjtJwPuwepYsvGP8szD2UTCNDkwyEHLNl6gZthyxQTfLqnwS7SWvcHfFqPvk8D8zhn7H7LsZHW2qRPacSN5x6n2/wBno2FTWzu2j8gOQMz3pYs+z1WLcTHTPALereSuS7MOsZ6dOereL0Xl9hjn4lBC8W9ZZ+xe3++3QEdhPsT1Dpj5k0VuN42YixRvyQ1Edz7fG3jKxMyXpPmSxYGPDXyUN+LxltBGm2on8h4euCZ094A7ler7MDtCEfb2A9lPGfItg+t9Idy4QNbfs9Sio+ceSWDPZfyy7grkMb1b5ngGvvs6I84u2GSHVncmeXi9ePKG8z//xAAmEAEAAgICAQQDAQEBAQAAAAABABEhMUFRYXGBkaEQscHR4fDx/9oACAEAAAE/EDZLhKbErojKxmz8KI5DalQdI1fgYhF1+EFiYYsBmrYCAjXJphUAO5sxnTEGotXAYaSxdu5qLrwxJfAmUoyt8xEOYw7JUzRm6gTmKoVC5a1KUS8Uq5i25p+K/DheZTRKm6BzFGGKxc1HDGAXjMNcn1EJhDyuKHcJcKzAYJmkoWwdygRZalXMEMZRAxAmpY7jrc5CeRLTXcdr1lh0Ze5xEDM2hxAjBY2hOOpkesQcQbHMdkAGVhN0rF1eMafVl4L0v5XiNXNujQ5vquZZwNrUjs7jRug1CkIClalBRfvN5dD1m31moUZqEaiJMsQMkuEmSUXNsECueQoA2sqRaujHHT7MKWq9bZhDfBxLMwnbkxDUttAd7ls6TFD0PD9M3/g/ADhgFYt7HgIJjjuUiZdwYMP2SR9GvSXozuHed+otOILtFaqVHp1abylC3cdRtSky/Jo9LlicDdYlJboGCBS1i15IyUUjS8sCIBpdBiOipodppvxp9pRbVjplP4ReYHqILZIktFjBhjiUDup6WQgs3BdzTQS4wj5Ic+Il3DpFIjRmoSBeqLXumYAaH1Of7AbjwbxKASt2q66mYEaOcSSWDrEFjoD5IsKKbl2fUGAGXNQs7InzctwkA8eIcnicSg7TwwZWMQWLYmGxVfiBiDY6sjq1NvtHDHWkS2jAEVaUBV6xEWKNeZUgpTVRU7dtFxOZMnXiHDEoeFUTogw7/wAFDUroTEtVZjXLdSsTDxFaUq3FW+uIVnfOJrn4ApFWgV8SlYDiZvD4M+twhUHBp5TNRDsDo/dW/G4mEgRak9JRirKZ8JhOyDB5R4jKoAMAvK8EyzDXtDL83K/DOWNQRuKO4jZiCxbr5mR5noSlKJfJMxW+DmBQ4TiWsTJmKnEeRaDYyjbRLd2wIAlS5Xtjm4DSxVS7dQGG5WCHlqU4METurY3E0z+WHG5hmsT68pVldwE89xKgLDcxrx5nMZhhWFxWwMYoVW76N4zKlzVVzLDTUHIfDmbcRg08dR+4l8MalEKa1dQ0GaKWSxK3a8xMkPpXHgm0udksZbpplpg+8y9GWQAlZlcGkzn8AZZJUZo1hKm7lhAVM2pdTIWrwj/wfeaNBh6FPEAyPZ7vaXCFdtbLXTFp1pA38EDaT3RhBxa7GYzpcFecwzqV+MXMsYDD1Df3xFQZzndy0KmdwYXCQ9JboZ55PxGRgixxM15jT/QQBeKvaMilOTmYmWVu2oqmFoNu/jqWDYz08qe3kzMF2GGfxn6lnRyWj6rMR95QSoJXca7TK/qC/ZNAozYlUyoxvCi+/wAFWjWigO1jgCpFPf5emPMSG0xQQ0OFX2ylqKx0wAx9Ve42LBKgfo98x4KIBQBLb7ju1CXptcky8KtVJ7wTeXS/O/uFNVI2P7PuAGtQ7ns5mE3PUuegxHj6S0V6DUx0xRIHeXZlOr8Ih8srFFhQTzz9riRBOLvcNr1mGMHGDDEdg1qcgKpzUeRoRvINwdDqJvLNqw5ay3wfMZXVdlXoTXR40xFjTm7Uor5gwhQzLRxA515a5eoOzU4u31fWDYo1E0W3R5hcplfmJoeXrRs+Sk+JTsgXDhh+CIBcORfJcoucQ0TaImY36IWEuvthT2twQFQRVBqolQOGq6naT4wzh5APmWsDTkeQvp17wRpb0pqnzNgZ+T1vMoGJi3S7V1riCAoEpSkoG8oHkMf6zatGtSwWehUa+2Kl3d7yyy1JfVL+x9x8REHzLA9NTG5BgVXvB8onaKyUw09KXoC5cXNUXlymFmaOYb1W2C4LaWR/7UyyREJcQ7/uZio9rlPWx6AH7TJ0O5pJoHywfuOFdI88TPtCqxDzahHxl+2WWOmI1Q99BiVQRjkSyUN4MWFnE2ZXlTFSS4YIeIYRUmCr2RAflfcE/U1buYDzucIurfqYK5/qImq++WD7ZZcqv7SLhN1Kd18Nr/J4G5otiexj7X8S6HEjbc2fEWzsP1Wba3Exb0ZZeQ3l1+qmg4ZQKn8QbVHmeSY6ZSpPYQpKsQ9URVGQXOkc/Uw+ErxhpM/B+qFugH2QFLXscy/RMDZFegQW3GorDVT2K/bO3XpFdQaVcFH9Zfk1d9S6EwBU1ripN1NOoK9GYN3mgdAf0YhRvwxXA6llMi3KMpZYY9TCuuEEe6Tsu36hK6kUeSWrnJcVUbGvSBaXlOb1fRLZbY8qB+mZkA4EOsW8wv2Ijbhnemv5FUBXd6B/7xHZlCjWCVFhwGEBbZLcNgVXP3h1GWodn8nA5q+in9lCxczjI0YxU8ceBSCmyKq4giWQXITUkkegB+2ZWue5feWKG8mYATv5QhJHRGSH021b+4IJjPeiVudC5cIwp6qyiH6Qj6v5iX6nBzn9IqNGJmmbJl8xZYVrEWWCXs0FDtEf9gBll3OQ49YLAlChqFHiFiltHxAvSY9ZSMUGj8zTOSTI9J9MfaeUYY6l1/QaPogeuBt5LgXX+olZwuXzNLl3cHB8Q7Wtks0xgLMC8qYDV5T9TO2YjweZcTmvdwRQGCKxxBY7gUIKlD+ANcTAO4hLZPwuX7lqDkJk7ZcWEFFTNroYgKxQ9C5pAFNbX/syg4j1/wCIDOMBfpUQOyyrr/2IAKowQ6CX2RIeUs9oBTa0sdVW+pn3dViWmKuZLyfJRTeyb5i+aKkbX+MKtO4IbgJVq4m9KfoNH6mXUNdstwgFxWInHrHXi/4mqp+0H+wGXCp8xVGH6Rf2YSmBPCi/q4hpB0nBoiUVdzZpuOiY13CI95glusiQFNK5JzTxqXzZ/YphzGUqIXvMuY53MeFi2kGzmYAEPsXLwNo/duZYuu9LFawNoEoi4g0lZzQIKwoM+bH/ACGrAW66gBKpfB/2cCAfVNyzagV6ztLVxbPDzKihmbEcJpbldjKkZ1O1zKA3WfMVpq5qM9yofMYL5fwI5rK1BLQpnqlf2DaKumWDCCoD3Hom7CH0/AomwQuU4TxiWCzbPQD+wXKrcpK2Ffwg2QoPIWfZLuiFryu/Eoq+xBaVCHC+5aUzazKDjdwX4DcJMIb8GcE3/pKIbXKCpPE4/cuGUSJuNwbOZYeinfNiNso4XcY0h2nvEUbA3XpBvElFo/2lsZbnuwHRdRVud8/8QEyCUxwwU/stRDq+5Zec+ItluCKmld1yTBtkfMdYaFVWAh8qpYCy/qQlphuJaq1CvujbJOiJkBpLV/KowXb5ZgVhVDLYdjBr2Rwz7gZRc9/gn9dMiwiHyQdhIrjUPuU/YylXviJqBCm+59SGloC9p4YG+Yy+Fp6oXZH9Zt9YjJfqcRcPWUcdyml5cew4lRaMHg4PqWUgrl6mHtlE4ww+2I3b0hrL50EUNQow6NvXMJScDATbDFbahBirmxTDXI2ftls05j4D1mEHHEVsPGWbWLK16MqUE6qAJwaXmcvmn4QKqcxg5+ZR7owVUSxMQWwhscFecD6ILNTweoSQ2YsjOD5ai2IU4tYNb3EOL9aK6PqHwSxEjSKGLJ+zzqHiWjtAOw+kva4IODH0fqVkdoFgKEoXAVH8gHvcQmrsLj6m14mddOKl8AfXz/JQwzCAMRIV3Kt5TLE2W4cjAIvi/wCohpWClqupavVCoAlFpVpP6hbQdkZoHepGLQ/JcKLR46gIRazYS6eOgUKUscjuJllkWnLi5Xaxosujm4I+cQlh1DdpFhZK1yJjwt6hfXEjimn7SGqOppvcUcDPUpjpTJNjxYv0CAfKxnpNFbpYgY1gTC3vLdWsWHxB5yhEp4zX63G0Ic3VB/8Aserp3r+CJOH3/wA1BVKLBA6iWDyjeHf2SzjSGIiCKklHYp8s8THMtKDLctzX3M/sUrVwrBE6Z4OZdalJDNOZS2SkJi5hIgmfKPlNHcQSylRAnvLy4b9RMtpS25NHDiazRWD2ZZLteBpxBE5+yPUaBJgcoXnPBEHUxTtjsSm3dF+4whJeHT3GLNIfI/5LJjXwyhXLZFBd7idVZRlEgVMRNT8QM2TzeYouzdergMlcsSubQ+ckqR8fuUG3qZgld1UwqjVqH7ludRY788Qou+i6Dy3CLnF4f9h7UsHcxURVCsnKCBZcE10RLFJ5mRh5gEcmPgXFCFN1u7lkAAbCswiwKFDxCkdXmCMFcI7j1TRYwahoVYOZRJRAc2CGSpgOgB/IqfQSqgyqMleGWFtAFuyVPGeDmCrrC1uWKzu5ZKs571FdCPQyoeG4udWsKjrmIM1CoFQt3mj9QZqdMMJoKohhW/cSg0BH4NLqNCH1YtKLjb0ji7Hl3GQYisuWxdFJhj7qerP2Sou/5hjmYa63BEFLKgtG6L5jkVlVs0w8kUtBri/MVxVVizPSxAp5r7jszVcRUw1zLTf6KWCUdEAJhnB4iBYDvuImQQMk0nMrr2RCto35lCWKU3Xcf5sVarT7qVx6/pFYu1mAwl+QPLKYu6GB9zUKueM0TBr3DFR4sUfEGHMxjJEFzSK7IsaZRiekc2V9n1E37SDkf+T5ugguuJlVQ7BdLh5h9HhgAXhUfyNQFh1EBeWZTARnpbUNXUVXUtLA3VQR9PvBvPpLvwfSy/eIQairIfa5UZnkxLK605i9/nXMPP0q6gALugolIAgX3iJkeZUljiC7oestt1tYZZAFPJMKMPytiJIAkOGWWVzsnYv0laYGwWtoljkjn6YdC7oeQLhiyxXjMPVKDOcFo8TNhZarXLmZVVYNaR6NSwtcOZ2qazCVBpwf5EFqe0cT6Y+zOsRCxfZcrEJnYmDd/wDyFPJiDm4alYQroibKgM0Bb+xoGTMQAydpojzD/9k=",
        //    //        "image/jpeg", "inline", "EmailYoutubeImage");
        //    //    msg.AddAttachments(attachments);

        //    //    var response = await client.SendEmailAsync(msg);

        //    //}
        //    //catch (AppException ex)
        //    //{
        //    //    new ExceptionHandlingService(ex, null, null).LogException();
        //    //}
        //    return true;

        //}

        private void AgentInvoiceTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {
            string addinsInc = "";
            string addinsAdd = "";

            if (studentVM.StudentPDFAddinAdd.Count > 0)
            {
                foreach (var addin in studentVM.StudentPDFAddinAdd)
                {
                    addinsAdd = $"{addinsAdd} <p>{addin}</p>";

                }
            }
            if (studentVM.StudentPDFAddinInc.Count > 0)
            {
                foreach (var addin in studentVM.StudentPDFAddinInc)
                {
                    addinsInc = $"{addinsInc} <p>{addin}</p>";

                }
            }
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.CurrentDateTag, DateTime.Now.ToString("MMMM dd, yyyy"));
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentVM.FirstName} {studentVM.LastName}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.AgentNameTag, studentVM.AgentName);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.AgentAddressTag, studentVM.AgentAddress);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.AgentCountryTag, studentVM.AgentCountry);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.DOBTag, $"{studentVM.DOB?.Date.ToString("MM/dd/yyyy")}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.ProgrameStartDateTag, $"{studentVM.ProgrameStartDate?.Date.ToString("MM/dd/yyyy")}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.ProgrameEndDateTag, $"{studentVM.ProgrameEndDate?.ToString("MM/dd/yyyy")}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.CampusAddressOnReportsTag, studentVM.CampusAddressOnReports);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.ProgramNameTag, studentVM.ProgramName);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.SubProgramNameTag, studentVM.SubProgramName);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.FormatNameTag, studentVM.FormatName);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.MealPlanTag, studentVM.MealPlan);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.TotalGrossPriceTag, $"{studentVM.TotalGrossPrice}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.CommissionAddinsTag, $"{studentVM.CommissionAddins}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.CommisionTag, $"{studentVM.Commision}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.PaidTag, $"{studentVM.Paid}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.BalanceTag, $"{studentVM.Balance}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.Reg_RefTag, studentVM.Reg_Ref);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.AdditionalServices_Tag, addinsAdd);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.IncludedServicesTag, addinsInc);



        }


        private void StudentInvoiceTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {
            string addinsInc = "";
            string addinsAdd = "";

            if (studentVM.StudentPDFAddinAdd.Count > 0)
            {
                foreach (var addin in studentVM.StudentPDFAddinAdd)
                {
                    addinsAdd = $"{addinsAdd} <p>{addin}</p>";

                }
            }
            if (studentVM.StudentPDFAddinInc.Count > 0)
            {
                foreach (var addin in studentVM.StudentPDFAddinInc)
                {
                    addinsInc = $"{addinsInc} <p>{addin}</p>";

                }
            }
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.CurrentDateTag, DateTime.Now.ToString("MMMM dd, yyyy"));
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentVM.FirstName} {studentVM.LastName}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.Reg_RefTag, studentVM.Reg_Ref);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.DOBTag, $"{studentVM.DOB?.Date.ToString("MM/dd/yyyy")}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.ProgrameStartDateTag, $"{studentVM.ProgrameStartDate?.Date.ToString("MM/dd/yyyy")}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.ProgrameEndDateTag, $"{studentVM.ProgrameEndDate?.ToString("MM/dd/yyyy")}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.CampusAddressOnReportsTag, studentVM.CampusAddressOnReports);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.ProgramNameTag, studentVM.ProgramName);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.SubProgramNameTag, studentVM.SubProgramName);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.FormatNameTag, studentVM.FormatName);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.MealPlanTag, studentVM.MealPlan);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.NetPriceTag, $"{studentVM.NetPrice}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.CommissionAddinsTag, $"{studentVM.CommissionAddins}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.PaidTag, $"{studentVM.Paid}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.BalanceTag, $"{studentVM.Balance}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.AdditionalServices_Tag, addinsAdd);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.IncludedServicesTag, addinsInc);



        }


        private void PDFCreator(EmailViewModel template, string filename,string htmlContent, bool isLandscape = false)
        {
            //var pdfPrintOptions = new PdfPrintOptions()
            //{
            //    MarginTop = 50,
            //    MarginBottom = 50,
            //    CssMediaType = PdfPrintOptions.PdfCssMediaType.Print
            //};
            //HtmlToPdf Renderer = new HtmlToPdf();
            //var pdf = Renderer.RenderHtmlAsPdf(template.AgentInvoiceTemplate);

            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 15;
            converter.Options.MarginBottom = 15;
            if(isLandscape)
            {
                converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape; 
            }
            // convert the url to pdf
            PdfDocument doc = converter.ConvertHtmlString(htmlContent);
            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            var ab = doc.Save();
            ContentType ct = new ContentType();
            ct.Name = filename;
            ct.MediaType = MediaTypeNames.Application.Pdf;
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(pdfStream, ct);
            template.emailAttachment.Add(att);

        }
    }
}
