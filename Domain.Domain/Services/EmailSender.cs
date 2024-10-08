﻿using System;
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
        private const string PassportNumberTag = "{{PassportNumber}}";
        private const string AddressTag = "{{Address}}";
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
        private const string ArrivalDateTag = "{{ArrivalDate}}";
        private const string ArrivalFlightNumberTag = "{{ArrivalFlightNumber}}";
        private const string ArrivalTimeTag = "{{ArrivalTime}}";
        private const string CountryTag = "{{Country}}";
        private const string TotalAddinsTag = "{{TotalAddins}}";
        private const string RegFee = "{{RegFee}}";
        private const string RegStyle = "{{RegStyle}}";
        private const string EmailBodyTag = "{{EmailBody}}";



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
    html,
    body {
      margin: 0;
      padding: 0;
      font-family: Arial, Helvetica, sans-serif;
      font-weight: 400 !important;
      font-size: .9rem;
      line-height: 1.5;
      background: #fff;
      color: black;
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
      padding: 2px;
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
            </div>
            <div class=""col-md-8 "">
              <h4 class="""">AGENCY INVOICE</h4>
            </div>
          </div>
          <div class=""row contacts"" style=""margin-bottom: 20px"">
            <div class=""col-md-12 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Invoice Date:</td>
                    <td class=""data"" style=""width:30%;font-size: 12px;"">{{CurrentDate}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 25%;"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Name:</td>
                    <td class=""data"" style=""width:30%;font-size: 12px;"">{{StudentFullName}}</td>
                    <td class=""text-right"" style=""font-size: 11px;width: 30%; vertical-align: text-top ;"">Agent Name:
                    </td>
                    <td class=""data"" style=""width: 25%;font-size: 12px;"">{{AgentName}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Number:</td>
                    <td class=""data"" style=""width:20%;font-size: 12px;"">{{Reg_Ref}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td class=""data"" style=""width: 35%;font-size: 12px;"">{{AgentAddress}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Date Of Birth:</td>
                    <td class=""data"" style=""width:20%;font-size: 12px;"">{{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">
                    </td>
                  </tr>
                  {{PassportNumber}}
                </tbody>
              </table>

            </div>


          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"" cellpadding=""0"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size:12px "">DATES</td>
              </tr>
              <!-- <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 10%;"">Start Date:</td>
                <td style=""width: 40%;"">{{ProgrameStartDate}}</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 10%;"">End Date:</td>
                <td style=""width: 40%;"">{{ProgrameEndDate}}</td>
              </tr> -->
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  Start Date: {{ProgrameStartDate}}<br>
                  End Date: {{ProgrameEndDate}}
                </td>
              </tr>
            </table>
          </div>

          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size:12px "">CAMPUS</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{CampusAddressOnReports}}</td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size:12px "">ACADEMIC PROGRAM
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{ProgramName}}<br>
                  {{SubProgramName}}
                </td>
              </tr>
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size:12px "">ACCOMODATION</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{FormatName}}<br>{{MealPlan}}</td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size:12px "">SERVICES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Included_Services}}
                </td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size:12px "">ADDITIONAL SERVICES
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
              <img style='page-break-inside: avoid'
                src=""data:image/png;base64,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""
                width=""300px"" alt=""signature"">
              <br>
              <p>Elvis Mrizi<br> Director </p>
            </div>
            <div class=""col-4 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" style=""line-height: 0.9;"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
               <tr {{RegStyle}}>
                    <td>Reg.Fee</td>
                    <td class=""text-right"">${{RegFee}}</td>
                  </tr>
                  <tr>
                    <td>Gross Price</td>
                    <td class=""text-right"">${{TotalGrossPrice}}</td>
                  </tr>
                  {{TotalAddins}}
                  <tr>
                    <td>Commission</td>
                    <td class=""text-right"">${{Commision}}</td>
                  </tr>
                  {{CommissionAddins}}
                  <tr>
                    <td>Paid</td>
                    <td class=""text-right"">${{Paid}} </td>
                  </tr>
                  <tr>
                    <td style="""">Balance due</td>
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

          <div class=""row mtable"" style="" background: #fff; border-bottom: 1px solid #fff"">
            <div class=""col-6"" style=""line-height: 1;"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td width=""250"" style="""">Canadian Dollar Transfers:</td>
                    <td class=""text-right"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""""> Business name:</td>
                    <td class="""">Eli Camps Inc.</td>
                  </tr>
                  <tr>
                    <td style="""">Business address:</td>
                    <td class="""">360 Ridelle Ave. Suite 307, Toronto Ontario M6B 1K1</td>
                  </tr>
                  <tr>
                    <td style=""""> Account Insitution number:</td>
                    <td class="""">004 </td>
                  </tr>
                  <tr>
                    <td style="""">Account number:</td>
                    <td class="""">5230919 </td>
                  </tr>
                  <tr>
                    <td style="""">Account transit:</td>
                    <td class="""">12242 </td>
                  </tr>
                  <tr>
                    <td style="""">SWIFT CODE:</td>
                    <td class="""">TDOMCATTTOR </td>
                  </tr>
                  <tr>
                    <td style="""">Bank Name:</td>
                    <td class="""">TD Canada Trust </td>
                  </tr>
                  <tr>
                    <td style="""">Bank Address:</td>
                    <td class="""">777 Bay Street Toronto ON M5G2C8 </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </main>
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
    src=""data:image/png;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCAL5BDQDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD9U6KKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAopryKiksQAO5qhcaqq5EQ3H+8elUouWxMpKO5eaRUGWIUepNULjVl5EQ3f7R6VnSzPM2XYt6e1MreNJLc5JVm/hHyzSTNl3LfyplFFbpW2OdtvVhRRRQAUUVZt7GW4wQNq/3jSbS3Gk5OyK3erdvp0k/Lfu19T1rRt7CO3Ocbm/vGrNc8qv8p1Qo9ZFe3sorcfKMt/ePWrNFFYtt7nSklogooopDCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKQ0tFAGbqlp8vnKOR94DvWXXSOodSCMg1z88XkTOh7HiumlK6scVaNnzIjooorc5wooooAnivZocYckejc1dg1Zekike61l0VDhFmkako7M6KOdJVyrBh7GpK5tHZGypKn1FXrfVWXiX5h/eHWsJUmtjpjWT+I1qKiinSZcowapAc1idG4tFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRVO51GODIHzv6Cmk3ohNpastswUEk4FULjVVj+WMb29e1Z9xdyXJ+Y4X+6OlQ10RpfzHJOt0iSTTyTtl2J9u1R0UVvsczbe4UUUUAFFFOjRpX2qpY+goDcbUsFtJcNhFyPU9KvW2lchpjn/ZHStFYwgAUAAdhWEqttInRGi3rIp22mRxYZ/3je/QVdCgUtFc7berOtRUVZBRRRSKCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKydYjw6SDvwa1qpaom61Y/wB0g1cHaSM6ivFmNRRRXaecFFFFABRRRQAUUUUAOR2ibchKt6itO11QNhZflb+92NZVFRKKluXGbhsdKCGGQcilrBtb17U4+8n92tiG4SdNyHI9PSuWUHE7oVFMmoooqDQKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKa7iNSzEADuaAFqG4u47dfnbB9O9UbnVScrCOP7xrPZizEk5J7mto029zmnWS0iWbnUJJzhT5aeg6mqtFFdKSWxyOTlqwooopiCiiigAoqWC3kuH2oufUnoK1rXT44OT87+p7fSs5TUTSFNzKFrprzYL5Rf1NasNvHAuEUD+tSYxS1zSm5bnbCChsFFFFQaBRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAVXvhm0l/3asVW1BsWkvPbFNbomXwsw6Siiu88wKKKKACiiigAooooAKKKKACnxTPA4ZDg+nrTKKTVwTtqjdtLxblfRx1WrNc3G7ROGU4IratLwXKejjqtcs4cuq2O6nU5tHuWqKKKyNwooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKQ8CmTTpApZ2wKybrUXmyqfIn6mrjFy2M5zUNy7dajHBkD539B2+tZU1w9w252z6DsKjorqjBROKdRzCiiirMwooooAKKKlt7Z7lsIOO7dhSbS3BJvREYGTgcn0q/a6YWw03A/u96uWtiltz95v7xq1XPKpfSJ2Qo21kMjjWNQFAA9BT6KKwOkKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKo6s+23C/3mq7WPq0u+4CDoo/WtIK8kZVXaDKVFFFdh54UUUUAFFFFABRRRQAUUUUAFFFFABT4pWhkDqcEUyijcE7ao37S5W5iDL17j0qeuftbg20oYcjuPWt2OQSoGU5BGc1xzjys9CnPnXmPooorM1CiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooqOWZYULOwCigNh+apXepLDlU+d/0FVLrUmmyseUT17mqVdEafWRyzrdIj5Znncs5yaZRRXQcm+4UUUUAFFFFABRT4ommcKgya1rPTlgwz/M/r2FRKaiaQpuexVtNMaTDS/Kv93ua1UjWNQqqFA7Clpa5JSctzujBQWgUUUVJYUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAMkcRoWPQDNc9I5kkZz1Y5rU1WfbEIweW6/SsmumktLnFWld8oUUUVuc4UUUUAFFFFABRRRQAUUUUAFFFFABRRRQAVe0y68t/Kb7rHj2NUaKmS5lYqMuV3R0tLVWwuftEAz95eDVquJqzsz0k1JXQUUUUhhRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFIxwCScVl3ep9UhP1f8AwqoxcnZESkoq7LV3fJbjH3n7KKyJ53uH3Oc+g7CmEkkknJPekrqjBROGdRz9AooorQzCiiigAooooAKsWtk90c/dTuxqxZ6aXAeUYHZf8a1VUKAAMAdqwnUtojpp0r6yIoLdIE2oMDue5qaiiuY60rBRRRQMKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACkY4BPSlqhqlx5cWxT8z/yppXdiZS5Vczbqb7ROz9ug+lRUUV3JWVjzW7u7CiiimIKKKKACiiigAooooAKKKKACiiigAooooAKKKKALFjcfZ5wT908Gt0dK5qtnTLjzoApPzJwa56sftHVRl9ll2iiiuc6wooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKjnnWBCzMAKiu71LZcH5nPRRWNNO9w+5zn0HYVpGDlr0MalRQ0W5Ld373J2j5Y/T1qtRRXWklojhcnJ3YUUUUxBRRRQAUUVLBbvcPtQfUnoKTdtWCTbshiI0jhVG5j2Fa1npywAM/zP+gqa1s0tV+Xlu7etWK5Z1HLRbHbTpcur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VD7Ve/8APmv/AH8o+1Xv/Pmv/fyjlYc6/pF+iqH2q9/581/7+Ufar3/nzX/v5RysOdf0i/RWa13fL/y6D/vum/bb7/n0H/fVPlYvaLs/uNSisv7bff8APoP++qPtt9/z6D/vqjkYe0XZ/calFZf22+/59B/31R9tvv8An0H/AH1RyMPaLs/uNSisv7bff8+g/wC+qPtt9/z6D/vqjkYe0XZ/calFZf22+/59B/31R9tvv+fQf99UcjD2i7P7jUorL+233/PoP++qPtt9/wA+g/76o5GHtF2f3GpRWX9tvv8An0H/AH1R9tvv+fQf99UcjD2i7P7jUorL+233/PoP++qUXl8f+XQf99UcjD2i7P7jToqh9qvf+fRf+/lH2q9/581/7+UuVj51/SL9FUPtV7/z5r/38o+1Xv8Az5j/AL+UcrDnX9Iv0VSFxdkc2oz/ANdBS+fd/wDPqP8Av4KVmVzIuUVT8+7/AOfUf9/BUnmXH/PAf9/KLBzIsUVX824/54D/AL+U5HmLDdEFHrvzQO5NRRRSGFNkQSKVYAg9QadRQBiXtgbc7ky0f6iqldKRkYNZt3peSWh4PUrXRCp0kcdSj1iZlFOZSjFWBDDsabXQcwUUUUAFFFFABRRRQAUUUUAFFFFABRRV210x5vmk+RfTualtRV2VGLk7IrRQPO21Fz6nsK17SxS2AP3n9anigSFNqKAPangYrmlUctOh2wpKOr3FooorI2CiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACsbU7jzZtgPyp/OtG9uPs0DN36D61hZJ5PJrelH7Ry1pacolFFFdJyBRRRQAUUUUAFFFFAAeK2NMt/Lh3n7z8/hWbaQ/aJ1THy9T9K31GBiuerL7J1UI/aFooornOsKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKy/EniXTfCOi3eravdx2On2qGSWaU4AH9T6AcmsL4ZfFXQ/i1pF1qWgm5a2trg2zm5hMTbgA2QD2IYGgDsaKKKACiiigAooooAKKKKACiiigAorK8TeJtM8H6Ldatq93HY6fapvlmlOAB/UnsB1rE+GnxU0P4saTdajoLXDW1tObeT7TCYm3BQ3Q9iGBoA7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ABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAZWry5KJ/wI1nVYv5PMun54HFV67YK0UebUd5MKKKKsgKKKKACiiigAooooAsafH5l3H6Dmt0DFZejx5aR/TAFatclR3kd1FWiFFFFZG4UUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAU2R/LQt2AzTqqak+y1fsTxTSu7Eydk2YrHcxY9Sc0lFFd55gUUUUAFFFFABRRRQAUUUGgDZ0uPbag45Y5q7UVunlwRr6AVLXA3dtnpxVkkFFFFIoKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAoppcCgMDQA6im7wKVWDdKAFooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACs3WXwkajuc1pVjas+65C/3VrSmryRjVdoMpUUUV2HAFFFFABRRRQAUUUUAFPhXfKi+rAUyrOnpvu4/bmlJ2TZUVeSRuLwKWkHSlrgPTCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooqtqOoQaVZzXd1MkFtCpeSWRtqoo6knsKaV9EJtRV3sTSSLGpJYAAZyTXknjH9ovRdG1F9H0C2ufFWug7Ba6cm5Fb0Z+n5Zrn5b/X/wBou9mg06ef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YTcZju4gPduHH1/OvscgHrWF4o8EaH4ytPs2saVbahF285AWX6N1H4GvpqGcqpSWGzCn7WC2e0l6P+vmfnuL4VlRxMsfklb6vVe8bXpy9Y9Pl+BzHhf4/wDgbxXGn2fXbe1nbA+z3zeQ4J7fNgH8Ca7q31O1uYw8NxDKhGQ0cgIP4ivEdd/Y98HakxfT59Q0liekcolX8nBP61gH9jCKF3Ft4uvIY/4V8gZ/HDCs5YfKamtKvKHlKN/xTNqeO4moe5iMFCp5wqct/lJH0Te69p2nRmS7vra1QdWmmVB+przfxZ+0r4M8O74LS9Ov6h91LTTB5pZvTcPl/WuU039jjw5HcLJqms6pqigD92XWME+5AJ/WvUvBvwn8K+A0zo2jW9tN3uHHmSn/AIG2TXO4ZbR155VH2S5V97bf3I7o1c/xfu+yp0F3cnUl8klGP3s8wh8KeOPjnNDN4qLeFPCe7f8A2NAx8+6XqBKeMA+4/DvXuGh6HY+HNLg07TraKzs4F2xwwrtVR/nvV0KF6DFOrixGKnXSgkowW0Vsv1b83d+mx7GCy2lg3Kq5OdWW85ayfl2iu0YpL1eoUUUVxHrBRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFAFHV/+PZf94Vj1r6r81sD6MKyK66XwnBW+IKKKK1MQooooAKKKKACiiigArc03/j0T8f51h1taWc2i+2R+tY1djoofEXKKKK5TtCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigClqq/6IT6EVjVuamM2b456fzrDFdVL4Thr/EFFFFbGAUUUUAFFFFABRRRQAVsaQc23/AjWPWrozfu5B6Gsqnwm1H4zRooorkO8KKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAIL1d1rKPasAdK6OZd0Tj1BrnB0rppPRo46+6CiiitzmCiiigAooooAKKKKACtHRmw8i+wNZ1XNLbFzj1U1E/hZpTdpo2qKQdKWuI9EKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAEPTmuckXZI6+hIroz0rBvk2Xcg9Tmt6W7OautEyCiiiuk4wooooAKKKKACiiigAqazfZdRn3xUNKrbWDehzSaurDTs0zpB0paap3KD606uA9QKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACkLAd6G6V81fEv9m37Xda14k134oa7Z6MpkupIpGykCZ3bQd2NoHAG30oA991zxjofhlrZdW1a009rmYW8IuJgpkkPRRnvTIPHPhy5vI7SHXtNlupDtSFLuMuxzjAAOTXxl8D/ANnfQviLp2u+L/EtzqNt4Sjdxp73NwEmljQ/PLI+MbcDHA659Kb8BfCPgWT4rN4quruz8P8Ahy3udnh+w1G7UT3sgO1ZvmOSAeR23HH8JpiPurNLXnfxM8U+JrG90bSfBQ0W81qebzLm01S62OtqPvOqAhjyeozj0NbbfEnQIvGFt4TN8JvEMsJma0t43k8pQMkyMBhAe24jPHrSGdTSZArlPiZ8StI+FnhS513WJCsMfyxQr9+eQ/dRB6n9BknpXg3gjRfH37S2df8AE2sXvhTwVIxNnpGkyGGS5XPDNJ1K+569gBzQB9SBgehzS185fAfwtr/gbxd4u1nVYrrwr4ESIQWVjq9+ZSSjD9+xZjsyAT2+/wBOK9Vg+N3gS60vVdRg8U6bcWelgG8lhm3iIE4BIHJyeBjOTxQB3FFcXZfGfwLqGmtfw+LNJa2SFbh2a6VSkZIAZlJBXllHI6kV5D4Q+NE/xN+MOraja+KLXR/A3hyCQLayXCRnUWxgzOG5EYJBDcD7vcmgD6RJx1qra6vY31zPb215BPcW5AmiikDNGT03AdPxr5c+JXx58Z6J4GXRtVk0fTfE2vzMbS60q4MkdppxAzcFsnk8hcdeoGap/Anwh4m8D+JLabw7aaSzamY21i21O7xqFtZqfklePdlZJCzucjj5Vx1JYH11RXK6d8U/COreIDoVl4j0271cZH2OG5V5MjqMA8kYORUeofFvwZpWuxaLd+J9Lt9VkkEQtHuV8zeeikZ4P1pAddRXlfxu+PGh/Cjw3fMt9aXPiMwg2mmeaDI7Nwrso5CDrk+lZfww+Idn4K+Guman4/8AHlhd6jqrPd+bLdRlE3H/AFUW37yr0OM85oA9ooqppeq2et6fb39hcxXllcIJIriBw6Op6EEcEVVTxVo8mqXempqlm2oWkfm3FqJ1MkKf3nXOVHuaANWisNPHHh+XVLHTU1qwe/v4zNa26XCF50GcsgB+YcHkehrOv/i14N0rXY9Fu/E+lwarJJ5S2j3S+Zv/ALpGeD9aAOtoqhq2vafoOlT6nqF5BZ2EKb5LmZwqKvrmqng/xfpnjrQLfWtHlkn0643eVLLC8RcAkZCsAcccHvQBtUVgeKfHnh3wRAs2v61Y6RG4JQ3c6oXx12g8n8KsWHi7RtU0JdatNUtLjSTH5v21JlMQTuS2cAfWgDXory/wD8Q9XmNzc+M7zw7p1hqF55egSWN6rfbIySByWIYn5enqeK7G28feHLy11O5g13T5bfTHMV7Ktym23YdQ5zhT9aAN+iuU0H4q+EPE9lfXeleI9OvbWwBN1LHcLthA/iYnoODz0px+KPhEajY6f/wkulm+vgrWtut2heYN90qAec9vWgDqaKazhELMQqgZJPavMPDnxF1e58Y6tcateeG7fwK8otdIv4L9WmuJ8gFD823Od3y8EYHWgD1GkJA61z2j/EPwz4h1m40nTNe0/UNTtwTLaW1wryIAcHIB7EgGuN+MnwYv/ipc2Ulr4z1bw3DboUktrFv3UwJB3EBl+bjGcn6UAel3mo2unwPNdXEVvCil2eVwoAAySSfSsG2+J3hG7sYL2LxNpTWsyh45DdoAwJxnk+vFfF7fAWw8V/Gm38E6P4h1XX7bTwZtd1S5kBSHnmNP9voCSTyf9k16yfgV+z/4X1qPSdT1G1m1Z3EYtb7Vj5m4ngFVIwTnHNMD6bilSeNZI3DowBVlOQR6g0+vMtG+MPw+8O6zeeC4tVttGl0JI7fybtvIiUYwqIzkbsDHSut8T+P/AA34LtY7jXdbsdKhkGY2up1Tf/ujqfwpAdBRWDF488Oz6Laaumt2B0u7dY7e8NwoilYnAVWJwTnjHWsy2+MXgm81i90uDxPpkt/ZxvNcRLcA+WicuSeny9+eKAOxor5t1P41S/FH436L4W8I+J7fTfDunst1fX8UybtQYc+RFn7y4HO3/aPYV7LYfFjwdqniFdCs/E2mXersSFs4blXckdQMHqPTrxQB1tFJmvDvj9+0LL8Pb208L+FrIa14z1DCxWwBcQbuFLKOrHsvtk8UAe4lgOppa+VvEXwQ8WL4IvfEnirUNc8ceM5EH2XSNOvWgtrWRuAcKVyEzk4wOOB3r1L4Y6ncfCf4S6LD8SfEVtbakisJJ9QulyBklY95PzlVwMjNAHq9FYuieM9D8SaKNX0vVbS+0wqXN1DKCigdST2xg5z0rNufiz4MtDYCbxTpMRvwDa7ryP8AfAnAK88jPGaAOsormvF/xI8MeAbeKbxDrdnpKSnEf2mQBn+i9T+ArV0fxBpviDSINU02+gvdPmTzI7mFwyMvrmgDQqodVsl1BbA3cAvmTzFtjIPMK/3gvXHvXA+Jf2hfAPh6w1SQ+J9Nu7uxgaZrS2uVeRyP4FAPLE8Yrx34SXek+FNRvfi38UdYttJ8QeIATp1ncud9vbHpsTluQFA46Y7k0AfVdFZPhfxXpHjTRodV0S/h1LT5s7J4GypIOCPYj0Na1ABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABWPq67blT/AHlrYrN1hMojDscGtKbtJGVVXgzLooorsPPCiiigAooooAKKKKACiiigDespN9rEe+MVYrP0h90TL/dNaFcMlaTR6UHeKYUUUVJYUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAnSvlj9oDX9R+M/xR034RaBO0dhE63OtXCdFC4bafZQQfdmUdq+qKzLPwxpGn6veara6ba2+p3gUXN3HCqyzAdNzAZOKAPnz49TXEyeGPgl4JhFlLqUK/aXVfkt7FeDk+hKkn1xj+KvEPF48IeF/Ho8OQ2DTaT4OAkuY1jL32s3kak8tg7IV5J5CqoOAcivv77DbfbPtf2eL7Vs8vz9g37c527uuM9qg/sTTvtc119gtvtMy7JZvJXfIvoxxkj60wPgbQviReXGrL4n/t22sfFnii6Ntd+Ip4S1rodrjIhjYjb5hVenOAAOpJr0D9nDwNY+IPij4svtC8Q6/daNaLBjXFnMRv7gMTIH4IdDycHt9RX1jN4T0S40z+zpdHsJNPzu+yNbIYs+u3GM1dsNNtNKtkt7K1htLdPuxQRhFX6AcUAfLv7d2k30+keEdRMcsuhWd263gjXIQvs2sfqA6j3PvXpdt+0j8LPD/heze28R2aWsUCJDZWylpQAAAgiAyD2xXrV5ZW+oW0ltdQR3NvIpV4pUDKw9CDwRXOaV8K/BuhXZutP8LaRZ3Jbd5sNlGrA+oOOPwoA+RPjf8AE3Vfij4p8OaT4hE/gDwLe7rmCTU4iTcBc4eZFIO0kABc8Bs+lJfWFk/hvX/EkIbVvB1raxQ3mpLYCwhvEjcGGytYRyIzIVMkp5I4Br7X1DQ9O1dUW+sLa9VDlRcQrIFPqMg4qWbTbS4szaS2sMlqV2mB4wUI9NvTFFwPzifxBZ2vhKQlYBqHjC5S31TxA1vizsIdyt9mgGMFlG1nIHGABkjNdJ4z8V+BL/VNM8K6fAdG8DaXarcXt1Ha+Xe66VxtCnG4qxGcsQOrdhX3hL4Z0ieyis5dLspLSI7o4Ht0MaH1C4wOteXftA/CzWPiXYaLomiW1jaWM9wq6rqJ2rPFbLyI4+MkE9s9h2JouB8paJ8QdAurzXfiFq+nf2rrtoEh0bw/DCzWmmQoAsUszY27F+UAdzk4yeK994ou9J8DSXdjdXDX3ii9SPxB4xYEIpf5jaxN1KqDlyOpG0dK/QHTPCmkaVokOkwadbrYRwrB5BiUqyKMAMMc8DvViTQdNmsFsZNPtXslxi2aFTGMdPlxii4j4g+JmteDfCPhfTLj4Z29zM2mJ/ZzeKFDJawPMDvcHGZJmVSN2CFB45xXM+I9S8LWWi6B4U0LzYNK1d/N1fxZc2zNc6m8ZBZYAcvt3HAGBuYj3J/QVtB019OGntp9q1gP+XUwr5Xr93GKG0LTWa2ZtPtS1qMQEwrmH/c4+X8KLjPhrwl4p8Oa/wDEq8u9d0CSaTQLNNH8P+Dmh8+5uHG5R52RgkckljgFvbnD8HalpuuarrviDXvEMek+J2uHsrfQrTSPtd1CmAAlqhwkZHzICVOMZ4JNfoHDomnW99JexWFtHeSDD3CRKJG+rYyaZD4f0u3v3votOtI718lrlIFEjZ65bGaLgeLWGt658GP2cNS1G805LCTT7YjTNNJMklshwsYnfOGfcS7YwBkjtXzC3i6wsfBFlp1rqj/2j4vulbxR4rnVtsakhmtUbGTtDZfHHbvx+iNzZwXtu8FxDHPBIpV45FDKwPUEHgiqT+GNHksYrJ9Ksms4jujtzboY0PqFxgUAfDXxYjiGgaf448MWE/h7w/pyweH9CvyzwTyL87SXLY5CfeVc9d7HngVheI9R8L2ehaD4V0NZYNN1WQy6t4uubVjcamyMC624ILbQ2ABxubGe5P6F3elWV/afZbm0guLbgeTLGGTjp8p4qNtE05jbFrC2Jtf9QTCv7r/c4+X8KLgfAHivxZbeNvAV/b6p4gn0y202X+zvD3ga0XNxvQBUe5yMnvnPfOMV9i/CzwteeAfh3DLc3mpXl0dPjmksdQuTMLaRYstHGcZVc8Y56DFdmPCmijVTqf8AZFj/AGket59nTzT/AMDxn9ayvih/wk48C6sfBoh/4SPy/wDRBPjaTkZ68ZxnGeM4zQB8LWPxZ0lrLVfHXiBz4q+IN5I8Wn2lxEWtNIXna5z8pP8AdUZ6epJqjpesxQaVY+Dr6/u9P0/XpF1bXr/yn82+GSUgtkx8wzkZAwzEn7o59m+EHwC8TeKfHtr4g8e6VcWWm6cPMWyvrlJvtl1n/WGNQEVBknGOw6819XS6Lp89zBcyWNvJcQDEUzRKXjH+ycZH4UwPh7wL420TVPiNPqms6XO954fC6X4W8D20TPLG4zhmGMAgjLMTwST2Fc/43+H6+DvH+leG/FGrW/hax1aJtY1Jx5slgZy0jLEIwRuVAAmc5+Y89K/QCDQ9Otr6S9hsLaK8k+/cJCokb6tjJo1HQ9N1cxm+sLW9MZyn2iFZNp9sg4pXEfBlvpU3xX8cyeC/CV9Jc2+oxwtqmstZCziFnFkokUCj5YhuzzkuxHbrZsoNDf4uJo3hnRhdp4WuDa6ZpqKTcanfKcG4uZMZWJGXJJOAFAUc4r7vh061t7hp4raGOdlCNKiAMVHQE+gyeKZbaNYWd3PdW9lbwXM5zLNHEqvJ/vEDJ/Gi4z51/ad+I2seFvB3hzwdd6pBZ6t4hTytS1lIykMUQAExQDJyS2ABzj3NeS+EfC+mfFWWSVYLrTvhT4Dgd/LAK3F9MAXkZsHiRyMnByo2gYzx9y3uk2OptC15Z2900Lb4zPErlG9RkcH6U6HTbS2jkjhtoYo5GLOiIAGJ6kgdSe9AHxX+zxdXXifx9ca94X0yyg1S8xCFiiIstE09XGVbGPMncKAFz6sTzivf/wBpf4tzfCj4fl7DLa9qjm0sAq52sR8z49VB4HckV6np+k2WkQmGxs4LKIncY7eMRqT64AqDVvDela9NZy6lptrfyWUnnWz3ESuYX/vLkcH3FAHyzqnhbWv2d/2YNRv7IOPFmtSxvqeoLkyW4l/2uuVBxn+8xNctpvgnSvG+meGvCXwyvjqGqJIt/wCI/ETQsYGbAZVklPzNh+ka9ep55r7cuLWG7geGeJJoXG1o5FDKw9CD1qHT9JsdIh8mxs4LKLO7y7eMRrn1wBRcD4K8Siz134xajo2tzXHim40+6867hhtgt1rN8AFWFFA/dwIPl5OAoJOSwp3hDxBoviyx8Sa34iubvWvibqUr6do2j2se6Wx4wrRqw2oBnG4/dC+pzX3lHo2nw38l9HY26Xsgw9ysSiRh6FsZNMt9A0y0vZLyDTrWG7kzvuI4VWRs9csBk0XA+FPA3w71Hxvez6Fr2qy3XgbwEstzf/Z49m+55knhRhyx3bxvPOAcYyK55fiNFbWHiDxfp2lQC6mtjpWn2VpEPseiWkmVBlIG1pXGcA56sx6gV+iUOm2lusqxW0MaysXkCIAHY9SfUn3qrD4X0a20+Swi0mxisZDue2S3QRsc5yVxg0XA+D/EOpaD4b8A+F/D/hb/AEbTbq5S11vxyLbaJHcAyxRSEbtqgnOMDCgetdlbeGdJ1XXrLXfh1fXmh+CfBdnJPcayLMSpdXKqdzRI2DM5UsGZuBxivsKTQdNm09bCTT7V7FcbbZoVMYx0wuMVznjf4fTeLtJg0iz1y68N6TylzBpUSI80ZH3A5B2Dr90Z5ouBR+B3jHT/ABz8PrPU9NvtT1GEySRPc6sqrcPIGO4kL8oGTwBwBgV842OoaZ8Ov2ydf1PxzN9jgnEkum3tyD5S7lUIcnoAu5c9jX1v4T8KaX4J8P2ei6NaJZ6faJsjiT9ST3JOST3JpviLwdoXi63WDW9HstWiQ5VbyBZNp9sjigDzfxF+074UhkXTfCzy+M/EEwxb6fpCGQEnu8n3VUdznivlbSviNbeJdV8QeNfGcM3izxVYeZ/Z/h4RMbOxVR800o+6EU9BySRzyQR956D4Q0LwtEY9H0ex0tCACLS3WLIHrgDNWYtC02Brlo9PtYzc588rCo83PXdx8340Afnz4SvL3xTaDwh4aumvvFfjiXz9b1BVKQW1vksYkHAIGWLkcfwCtXxHpXhXRfiLaeCtP0yS/tPDLI9zDHHuvtcvQMhAedkS555CgbjzkV942miadYNG1tYW1u0aeWhihVSqZztGBwM84py6PYJfPerZW63kg2vcCJRIw9C2Mmi4HwJ4a+J9hKvib4h69ajxV43ZnFhpkkZe10qAEATPkbVUFgqjqce5NY8XiMaVoGm6Euv3emaV4lujca74hVHWCTBy8FugHKrvIJUfMzY+6K/QqHwnolvHeRxaPYRx3pJuUS2QCcnrvGPm/GpJPDulTW9vBJpto8FuQYYmgUrFjptGMD8KLiPg2/1Xwvq/jfStAtfDU+neFNGiW7s9ES2Jv9cnYDy2k4yAwIJ3Hhc+uBU8OfEfT5T4m8f67ZN4n8coWFlpzwFrTSYlIVZZMjaACwVV9vUk1+gY0mxF99tFnALzZ5f2jyx5m3+7uxnHtUE3hvSbi3uoJdMs5ILv/j4jaBSs3++MfN+NFxnzv+yZDqjxTXFjKLvSrl3vNY1aZGC3d644htl4ASMfecDDNwOBX01UNpZ2+n20dvawR29vGoVIolCqoHQADgCpqQBRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABVXUY99q/qOatU113qR6jFNOzuJq6sc3RTnUo7KeoOKbXeeWFFFFABRRRQAUUUUAFFFFAF3SpNtyV7MK2a523k8qdH9DXQg5FctVWlc7aLvGwtFFFYnQFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAIAB0paKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKQ0tFAGFqEfl3TcYDc1WrT1aP5Uf04NZldkHeKPOqK0mFFFFaGYUUUUAFFFFABRRRQAVvWMvnWyN3xg1g1p6PLw8ZPuKxqq6ub0XaVjTooorlO4KKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAILyIzW7qOuM1gCulPIrAu4vJuHXtnI+ldFJ9Dkrx2ZDRRRXQcoUUUUAFFFFABRRRQAVPZS+TcoexOD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MqkOeOm5h0UYwcHrRXYeeFFFFABRRRQAUUUUAFFFFAGnpV1j9yx91rSHNc2rFWDKcEcg1vWlwLiFWHXoR6GuWrGzudlGd1ysnooorE6QooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigApDzS0UAZGqWvlv5qj5W6/WqFdHJGJUZG5BGKwLiE28pQ9uh9RXVTldWZxVoWfMiOiiitjnCiiigAooooAKKKKACrNjdG2l5PyHr/AI1WopNJqzGm4u6OlBBGRS1m6ZefKIWPI+7WiK4pLldj0oyUldC0UUVJQUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAVTv7T7RFkffXkf4VcpMZpp2d0S0pKzOa6cUVoanabGMqDg/eH9az67YyUldHnSi4OzCiiiqJCiiigAooooAKKKKAFVirAqcEcg1uWd0LmIHow4IrCqW3uGtpQ6/iPWs5x5ka058j8joaKZFKsyB1OQafXGegFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFACEAjBGab5Sf3F/Kn0UBYZ5Sf3F/Kjyk/uL+VPop3FZDPKT+4v5UeUn9xfyp9FFwshnlJ/cX8qPKT+4v5U+ii4WQzyk/uL+VHlJ/cX8qfRRcLIZ5Sf3F/Kjyk/uL+VPoouFkIqhRgAAegpaKKQwooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAP/2Q=="">
  <div class=""centered"" style=""position: absolute;
    top: 35%;;
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
    html,
    body {
      margin: 0;
      padding: 0;
      font-family: Arial, Helvetica, sans-serif;
      font-weight: 500 !important;
      font-size: .9rem;
      line-height: 1.5;
      background: #fff;
      color: black;
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
      padding: 2px;
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
          <div class=""row"">
            <div class=""col-md-4  invoice-to"" style="" margin-top: 0;
            margin-bottom: 0"">
              <!-- <div class=""text-gray-light"">Invoice Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                {{currentDate | date:'longDate' }}
              </div> -->
            </div>
            <div class=""col-md-8 "">
              <h4 class="""">INVOICE</h4>
            </div>
          </div>
          <div class=""row"">
            <div class=""col-md-12 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Name:</td>
                    <td style=""width:30%;font-size: 12px;"">{{StudentFullName}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 25%;"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Number:</td>
                    <td style=""width:25%;font-size: 12px;"">{{Reg_Ref}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Date Of Birth:</td>
                    <td style=""width:20%;font-size: 12px;"">{{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <!-- <td style=""width: 35%;"">Ciudad de México.
                    </td> -->
                  </tr>
				  					{{PassportNumber}}
                </tbody>
              </table>

            </div>


          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"" cellpadding=""0"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">DATES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  Start Date: {{ProgrameStartDate}}<br>
                  End Date: {{ProgrameEndDate}}
                </td>
              </tr>
            </table>
          </div>

          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">CAMPUS</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{CampusAddressOnReports}}</td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">ACADEMIC PROGRAM
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{ProgramName}}<br>
                  {{SubProgramName}}
                </td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">ACCOMODATION</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{MealPlan}}<br>{{FormatName}}</td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">INCLUDED SERVICES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Included_Services}}
                </td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">ADDITIONAL SERVICES
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
              <img style='page-break-inside: avoid'
                src=""data:image/png;base64,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""
                width=""300px"" alt=""signature"">
              <br>
              <p>Elvis Mrizi<br> Director </p>
            </div>
            <div class=""col-4 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" style=""line-height: 0.9;"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
               <tr {{RegStyle}}>
                    <td>Reg.Fee</td>
                    <td class=""text-right"">${{RegFee}}</td>
                  </tr>
                  <tr>
                    <td>Total Package Price </td>
                    <td class=""text-right"">${{TotalGrossPrice}}</td>
                  </tr>
				  {{TotalAddins}}
                  <tr>
                    <td>Paid</td>
                    <td class=""text-right"">${{Paid}} </td>
                  </tr>
                  <tr>
                    <td>Balance due</td>
                    <td class=""text-right"">${{Balance}} </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <div class=""row"">
            <div class=""col"">
              *All fees above are in Canadian Dollars 
            </div>
          </div>
          <hr style=""width: 100%; border-width: 2px; border-color: #000;"">

          <div class=""row mtable"" style="" background: #fff; border-bottom: 1px solid #fff"">
            <div class=""col-6"" style=""line-height: 1;"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td width=""250"" style="""">Canadian Dollar Transfers:</td>
                    <td class=""text-right"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""""> Business name:</td>
                    <td class="""">Eli Camps Inc.</td>
                  </tr>
                  <tr>
                    <td style="""">Business address:</td>
                    <td class="""">360 Ridelle Ave. Suite 307, Toronto Ontario M6B 1K1 </td>
                  </tr>
                  <tr>
                    <td style=""""> Account Insitution number:</td>
                    <td class="""">004 </td>
                  </tr>
                  <tr>
                    <td style="""">Account number:</td>
                    <td class="""">5230919 </td>
                  </tr>
                  <tr>
                    <td style="""">Account transit:</td>
                    <td class="""">12242 </td>
                  </tr>
                  <tr>
                    <td style="""">SWIFT CODE:</td>
                    <td class="""">TDOMCATTTOR </td>
                  </tr>
                  <tr>
                    <td style="""">Bank Name:</td>
                    <td class="""">TD Canada Trust </td>
                  </tr>
                  <tr>
                    <td style="""">Bank Address:</td>
                    <td class="""">777 Bay Street Toronto ON M5G2C8 </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </main>
      </div>

    </div>
  </div>

</body>

</html>

";

        string AirportInvoiceHTML = @"<!DOCTYPE html>
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
    html,
    body {
      margin: 0;
      padding: 0;
      font-family: Arial, Helvetica, sans-serif;
      font-weight: 500 !important;
      font-size: .9rem;
      line-height: 1.5;
      background: #fff;
      color: black;
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
      /* margin-bottom: 20px */
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
      /* padding-bottom: 50px */
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
      /* padding: 15px; */
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
      padding: 2px;
      background: #fff;
      border-bottom: 1px solid #fff
    }
    /* .data {
      font-weight: bolder !important;
    } */
  </style>
</head>

<body>
  <div id=""invoice"">
    <div class=""invoice overflow-auto"" style=""position: relative;
    background-color: #FFF;
    min-height: 680px;
    padding: 15px"">
      <div class=""container"" style=""min-width: 600px"">
        <main>
          <div class=""row contacts"" style=""margin-bottom: 10px;"">
            <div class=""col-md-12 "">
              <h4 style=""text-align: center;"" class=""heading"">AIRPORT SERVICE AND ACCOMMODATION CONFIRMATION</h4>
            </div>
          </div>
          <div class=""row contacts"">
            <div class=""col-md-12 mtable"">
              <table style=""
              background: #fff;
              border-bottom: 1px solid #fff"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 11px;"">Student Name:</td>
                    <td class=""data"" style=""width:25%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 12px;"">{{StudentFullName}}</td>
                    <td class=""text-right;
                    background: #fff;
                    border-bottom: 1px solid #fff"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;
                    background: #fff;
                    border-bottom: 1px solid #fff"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 11px;"">Flight Information:</td>
                    <td style=""width:10%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 12px;"" class=""data"">Date: &nbsp;&nbsp;{{ArrivalDate}}</td>
                    <td style=""width:10%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 12px;"" class=""data"">Flight: &nbsp;&nbsp;{{ArrivalFlightNumber}}</td>
                    <td style=""width: 10%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 12px;"" class=""data"">Arrival Time: &nbsp;&nbsp;{{ArrivalTime}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 11px;"">Person picking up:</td>
                    <td style=""width: 35%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 12px;"">Eli Camps staff member holding out sign and wearing our uniform
                    </td>
                  </tr>
				   <tr>
                    <td style=""width: 15%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 11px;"">Accommdation address:</td>
                    <td style=""width: 25%;
                    background: #fff;
                    border-bottom: 1px solid #fff;font-size: 12px;"">{{Address}}
                    </td>
                  </tr>
				  		   {{PassportNumber}}
                </tbody>
              </table>
            </div>
          </div>
          <div class="""" style=""    margin-bottom: 10px;"">
            <p> The staff will pick you up when you exit the baggage claim area. They have your name and your
              information
            </p>
            <p>
              If you cannot see the logo or find the staff don’t worry. It’s ok. Please go to the information desk of
              the
              terminal (picture below). Out staff member will come there to meet you. Emergency information is provided
              below.
            </p>
            <p>Should you have any questions, please feel free to contact us anytime. We are more than happy to help.
            </p>
            <div class=""row"">
              <div class=""col-7"">
                <img
                  src=""data:image/png;base64,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""
                  width=""350px"" alt=""signature"">
              </div>
              <div class=""col-4"">
                <img
                  src=""data:image/png;base64,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""
                  width=""350px"" alt=""signature"">
              </div>
            </div>
          </div>
        </main>
        <footer>
          <div class=""row"">
            <div class=""col-md-3"">
              <h5 style=""color: #007bff;"">Eli Camps</h5>
            </div>
            <div class=""col-md-6"">
              <h5 style=""color: #007bff;"">EliCamps emergency contact information</h5>
              <h5 style=""color: #007bff;"">Telephone, WhatsApp, WeChat: +1.416.305.3143</h5>
            </div>
            <div class=""col-md-3"">
              <h5 style=""color: #007bff;"">www.elicamps.com</h5>
            </div>
          </div>
        </footer>
      </div>

    </div>
  </div>


</body>

</html>";

        string LOAInvoiceHTML = @" <!DOCTYPE html>
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
    html,
    body {
      margin: 0;
      padding: 0;
      font-family: Arial, Helvetica, sans-serif;
      font-weight: 500 !important;
      font-size: .9rem;
      line-height: 1.5;
      background: #fff;
      color: black;
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
      /* padding-bottom: 50px */
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
      padding: 2px;
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
        <main>
          <div class=""row"">
            <div class=""col-md-4  invoice-to"" style="" margin-top: 0;
            margin-bottom: 0"">
            </div>
            <div class=""col-md-8 "">
              <h4>OFFICIAL LETTER OF ACCEPTANCE </h4>
            </div>
          </div>
          <div class=""row"">
            <div class=""col-md-12 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Name:</td>
                    <td style=""width:30%;font-size: 11px;"">{{StudentFullName}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top;font-size: 11px;"">Student Number:</td>
                    <td style=""width: 25%;font-size: 12px;"">{{Reg_Ref}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Country:</td>
                    <td style=""width:20%;font-size: 12px;"">{{Country}}</td>
                    <!-- <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">{{student?.homeAddress}}
                    </td> -->
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Date Of Birth:</td>
                    <td style=""width:20%;font-size: 12px;"">{{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <!-- <td style=""width: 35%;"">Ciudad de México.
                    </td> -->
                  </tr>
                 {{PassportNumber}}
                </tbody>
              </table>

            </div>


          </div>
          <div>
            <h6>Dear {{StudentFullName}}</h6>
            <p> This letter is to confirm your acceptance and registration at Eli Camps as
              described in the following Letter Of Acceptance (LOA). A space has been reserved for your commencement on
              the date as mentioned below.</p>
            <p>
              You may use this Letter Of Acceptance for the purpose of obtaining a tourist visa to Canada. Please note,
              this
              acceptance does not guarantee your admission to Canada and is only an acceptance to study at Eli Camps.

            </p>
            <h6> Dear Visa Officer </h6>
            <p>Please accept this letter as a formal certification and acceptance of the above mentioned
              student at Eli Camps for studying English as a Second Language as outlined below.</p>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"" cellpadding=""0"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px; "">DATES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  Start Date: {{ProgrameStartDate}}<br>
                  End Date: {{ProgrameEndDate}}
                </td>
              </tr>
            </table>
          </div>

          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px; "">CAMPUS</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{CampusAddressOnReports}}</td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px; "">ACADEMIC PROGRAM
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{ProgramName}}<br>
                  {{SubProgramName}}
                </td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px; "">ACCOMODATION</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{MealPlan}}<br>{{FormatName}}</td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px; "">SERVICES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Included_Services}}
                </td>
              </tr>

            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px; "">ADDITIONAL SERVICES
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
              <img style='page-break-inside: avoid'
                src=""data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAATgAAAAzCAYAAAADzxQdAAAABHNCSVQICAgIfAhkiAAAABl0RVh0U29mdHdhcmUAZ25vbWUtc2NyZWVuc2hvdO8Dvz4AACAASURBVHic7Z1pUFRX9sB/3U2vdDfQdLM1DSiLAoIoGkUlatSYUuMkZkrNNpkk4yxJTU3NJPk2NTVfplIzqZqaVGoqTiqpmWwa45jEaBLjvqECLqgsArKI2KwNDb1A7/8P/t8bQNxBMelfVZfYy333vXfvueece855klAoFCJMmDBhfoBE3O8OhAkT5ofHUL1JIpHct35I79uRw4QJ84MgFAoRCoUIBoO43W7a2trud5dEwgIuTJgwd00wGKS5uZnf/va3vPzyy5SUlNzvLgFhEzVMmDB3SSgUoqOjgz//+c989dVXyGQy9u7dy4IFC+5318IaXJgwYW4fwSwFcLlcbNmyhe3bt+NwOPD5fPT399/nHl4lLODChAlzxwSDQbq6utiyZYso1ARf3EQgLODChAlzx4RCIQYHB+no6BDfCwQCtLe3MxEi0MI+uDBhHlDuZyjG0ONJpVKkUumw/8fGxt7T/lyPB1aDE3wAQ19hwvwYCYVC9Pb2cuDAAY4dO4bX671nx5ZKpeh0OjIyMgDQaDQ8++yzbNiw4b7Gvwk8sAIuTJgfO4IAcTgc7NixgzfeeIO//OUv7N+/f9yPPVSp0Gq1LFy4EAC5XE5OTg4zZ86cEIrHAyvgJBLJNa8wYX5sSCQSXC4XFRUVnDp1ilOnTnHu3Ll72ge1Ws2iRYuIi4tjcHCQ48ePEwwG72kfrseE98H5/X5cLhcOhwOXy4XNZqOvr4+2tjb8fv+w74ZCIQwGA0lJSaSkpJCYmEhExIQ/RZGRq51EIpkwKS+3ymjncL3PH4TzeRDQarXMnj2buXPnYjQamT179g2/L9yDG13/W/mO8LlcLic9PZ1HH32UL774Aq/XO2EEnGSiJdv39vbS3NxMS0sL7e3ttLe309bWRnd3N263G6fTycDAAP39/ddcxFAohEajISoqisTERKZNm0ZhYSFz584lJiZGvFkTdWKNvBV+v3+YA3ei9nsoI8/B6/XS2dmJRCLBZDKhUCjEzx6E85noCClSPT091NTUoFQqyc3NRavV3vA38L/rHwqFCAQCosKgUqluWcAJeDwezp49y+HDh8nPz2fJkiXIZLK7ObUxYUIIuLa2Ni5cuEBlZSU1NTVcvHhR1NQcDgdOpxOPx0MwGBQvfEREBHK5fFg7wWAQj8cjfh4bG4vFYqGoqIhf//rXZGZmEhERMWEnlnBuHo+HPXv2cObMGXJzc1m0aBEGg2HC9nsoQydGd3c3u3fvZteuXQQCAXJzc5k/fz4FBQVERUXd554++NxI47/ZWPF6vVy+fJnq6mouXbpET08Pg4ODABiNRqZOnUpxcTE6ne6W+xIIBHA4HERGRg5byO4n981+s9vt1NTUUF5ezunTp2loaODy5ct0d3fjcrkAUCqVaLVaLBYLRqORqKgoYmNjiYqKIioq6ppVKhgMYrPZuHz5MhcuXKC+vp6Ojg7q6up46KGHSE1NndAmqzAo29vb+fjjjzl06BDZ2dmYzWYeeuihMRNw11vTxlKA9vf38+WXX/Kvf/2LqqoqgsEgBw4cYO/evaxYsYInn3yS9PT0MTvezUzj+81ogicYDOJwOLBarQQCAYxGIwkJCbetPd0qwWAQp9PJ+fPnKSkp4eTJkzQ1NYlzLhAIAFd3QlNSUli1ahU/+9nPMJvNN21bIpEQERFBTEzMmPb5brnns72lpYXjx49z7NgxqqqqqK+vp729Ha/Xi1KpJD4+njlz5pCWlkZycjJJSUnExcURFRVFZGQkOp2OyMhIVCrVNatEKBRiYGCA7u5uWltbKS8v5+jRo/j9fhITE5HJZBNu4A9l6CR1uVz09vbidDrFgfcgIFzftrY29u/fT0VFBTqdDq1WS29vL4cOHaKpqYnW1lZeeOEF8vLyxmXRCYVCE/JeC/e4r6+PEydOsG/fPurq6ggGg6Snp7N+/Xpmz549LK7seox2fqO9JwjSY8eOceDAAU6ePEltbS2dnZ0EAgHUajV6vR6NRoPf76e3txer1YrVakUikfD6669PaMXgRtyzXnd1dbFv3z527drFmTNnaGhowOVyIZfLSUtLIycnh/z8fKZMmYLFYiEhIQGDwYBOp0OhUAy7cTdb4cxmM9OnT2fOnDksWbKEQCBAQUHBhFGbb4bJZOL5559n6tSp5OXlkZ6efseT1ev10t7eTldXF8FgEJVKhU6nw2AwEBkZiUwmG5et/N7eXmw2G4FAgDlz5rBixQqamprYuXMnDQ0NbNq0ia6uLp599lnmzZtHdHT0Lbct+JwaGxvFCPqEhAQyMzOHmb4TVcj19/ezZ88e3n77bSorK+nr6wMgOjoap9OJ2WzGYrGM2fHcbjf79+/nrbfeorq6mr6+PuRyORkZGcyYMYNp06YRHx+PSqXC5/NRV1fHJ598wuXLl/niiy9Yt24daWlpwMTTjG/GuAm4oUKotLSUzZs3s3//fmpqavD7/ZhMJtEfM3PmTNLT00lLS7vG1zTUETr0/zc6pvA9o9FIcXHxHff7fqHRaFi5ciXFxcXo9XoiIyPvqB2Xy8WuXbvYtWsXVquVUCiEQqEgKiqKpKQkEhMTiYuLE/+OjY1Fr9eLq/XdXIuh/lKLxcLKlSuRSqVkZmbyySefcPr0ab7++msuX77MqlWrWLx4MVlZWeh0ulG1l0AggN1up6mpierqaioqKqipqaGnpwe4uijMnTuXVatWMW3aNNHBfafnMNbjQGjH7/dTU1PDe++9R0lJCXq9ntmzZ+Nyuaiurqa8vJy6uroxFXA+n4+GhgZKS0vRarUUFxcza9Ys5s6dS3Z2NikpKWg0GqRSKcFgkNbWVvr7+/nnP//JlStXOHfunCjgHjTGVYPz+Xzs3r2bd999l0OHDuFyuYiNjWX27Nk89thjFBYWkpWVhdFovGZQj4VW4XK56OnpwWaz4XQ6xU0LoW2NRkN0dDTR0dHEx8djNBrvemKMFVqt9pYdvEMR+u33+9m9ezfvvPMOpaWlogMZQCaTieZ+TEwMcXFxxMXFYTabSUtLIysri2nTphEXFydqeLd7HdRqNSqVCriqsbjdbvLy8li/fj3x8fF8+OGHHD58mJKSEpqbmzly5AgFBQVkZ2djsVjQ6/VIJBLR5XDlyhXq6uq4cOECDQ0NtLa24nQ6xXETCoU4deoU9fX1vPjiixQVFQ3T2G/nHG429m4l1GW0NiQSCU6nk5MnT3Ls2DH0ej0rVqxg7dq1HD16lOrqavF8xxK1Ws38+fN59dVXiY2NZcGCBUydOpWEhIRrdjplMhnx8fEsWLCA9957D4/HQ0tLywOnuQmMuYATbmwgEGDTpk1s3LiRsrIyQqEQ8+fPZ/Xq1SxYsICCggLUavV1L9zI90fT6kb7zcDAAPX19eJkaG5uxmaz4XK56O/vx+VyieElGo0GvV4vajS5ubk8/PDDZGZmXrNDey8Yy0HU1tbGtm3bKCsrQ6VSMWfOHEwmE263G5vNRmdnJ729vbS3t1NZWQlcvR6xsbGkpKTwwgsv8OSTT95xTqHBYMBgMCCVSuno6MBmswEQExPD8uXLiYuLIyMjg++//56LFy/yzTffcOTIEZKTk0lMTESr1SKVShkYGKC3t5eOjg46OztxuVzIZDKMRiMzZ84kMzOTYDBIRUUFVVVVfPXVV+J9Xrhw4R1rvwKCJiqRSG7JL3ajdgKBgBjO4Xa7ycnJYd26dSxcuJCmpibgf7uRY4lKpaKwsJDU1FRUKhUGg+GG35fL5SQmJqLX6/H5fBOqQu/tMm4a3I4dO/jb3/7GhQsXUKlUrFy5kpdffpni4mI0Go34vbHykwwMDHDmzBmOHz9OaWkpdXV1tLS0YLfbRaErk8mGrepC6AlcvakJCQkcPXqU3/zmN8yaNUvUQB4khGtZW1tLbW0tAwMDLF26lF/84hekpaXhdrvp6emhra2N9vZ2rFYrnZ2ddHR00NHRQXt7O6dOnWLBggV3ldMoCEqtVktLSwuXLl3C7/eL2uO8efNISUmhoKCAI0eOUFlZSXNzM/X19VRVVV1zTpGRkcTHx2OxWEhPTyc3N5fp06czefJkAoEApaWlbNq0icOHD7Nnzx5cLhdOp5Nly5bd9s5eMBiku7ub2tpaWlpacDqdKBQKkpOTycvLIyEhAbjx2B3qWvH5fNTW1nL48GF6e3upqqpCJpNhNpvJz88nGAyK1/puBen1EITWrcw1iUSCSqVCLpfj8/nuaW7rWDOmAk4QJDU1Nbz55ptcuHABtVrN888/zyuvvEJubu41JuBYYLVa2blzJ9u3b6e8vFzUFoxGI/n5+SQlJaHVaomOjiY2NhapVEooFKK7uxuHw0F3dzdNTU1cvnyZLVu2EBMTQ0ZGhjiQH0QE4a5QKCguLqa4uHjYRPf7/Xg8Hvr6+rDZbGJQdX19PQ6Hg0ceeYTo6Og79l1pNBomT55MXFwcra2t1NbW0tvbi8lkIhQKIZVKSU1NZe3atcyfP18UyI2NjXR2djI4OEgwGEShUKDT6TCbzUyaNEl8JSQkDFsoBX+iRqNh9+7dHDlyBIfDQUdHBytWrCA1NfWmWrnX66Wrq4tz585x5MgRysvLuXTpEi6XC4VCgdlspri4mJUrV1JQUIBGo7mhK0P4zOl0cuDAAf76178CVxdWuVxOXFwcRqORUCiETqdDLpfjcrm4dOkSDofjjlwUox3/dvH7/aLVI5fLiY+Pv6t+3Khf4236jouJ+vHHH3Pq1CmkUilPPfUUb7zxBpMmTRIDEccyd7S6upqPPvqIrVu30tzcjFwuJzc3l/z8fKZPn86kSZNITk5Gr9eLL0HA9fX14XK56Ozs5PTp07z99ts0NzdTUVEhxuI9qAimlaAxjdzmj4iIICIigsjISJKSksjLyxPDCbxeLzqd7q40WKlUypQpU0hLS+PixYucOXOGixcvYjKZht17lUpFeno66enpLFmyhN7eXjHoVBBwer2emJiYUc9DIDIykuLiYlQqFRqNhm+//ZaysjK6urqoqalh4cKF5ObmEh8fP6wdn8+H0+nEarVSXV3N6dOnKSsr4/z58/T394vt9ff309zcTGVlJZWVlTz99NMsX75cNPduZol4vV5sNpsYiK7T6cQIAalUSlpaGgkJCXR1dfH1118jk8lIT0/HYDAME+QjEY6rUChQqVQolUpiYmLQ6XQ3dPPcCJ/PR2trKy6XC5PJRFJS0rDjBQIBPB6PmFnk9XqRSCTiohkIBIiIiEClUiGVSomMjESv16NWq+95uMmYHk0ikdDX18e3335LMBhkypQpvPbaa0yaNEkUKmNJXV0df//739m6dSv9/f2kpaWxePFili1bxqxZs65ZtUdGegsrpNFopKGhQRwAWq12QqSZ3A1xcXFotVo8Hg91dXXYbLabagRSqfSuMwyGTqKMjAzy8vI4fvw4Z86coaSkhJycnFGPIezwxsfHX6Mx3OrEVKlUzJ07l8jISGJjY9mxYweNjY385z//4ciRI0ybNo3U1FTi4uJE/6/D4aCrq4vGxkYqKyu5dOkSHo+H2NhYFi1aJArFnp4eysvLOX/+PLt378ZqtdLT08OaNWtITEy8Yb+0Wi1Tp04lJSWF+vp64Oq1lsvlSKVSFAoFmZmZLF68mK1bt1JaWkptbS1JSUmYTCa0Wu2omuLQ95RKJRqNBrVaLZrx6enp4s707eDxeGhubh62wFy6dIm2tja6urqw2Wx0d3fT09NDd3c3Ho8HqVSKz+djcHAQv9+PXC5HrVYjk8mIiooiISGB+Ph4EhISsFgsmM1moqKiHjwNzmq1cvnyZQCKiorIzs4eF59Cd3c37777Lps3b8btdjN9+nReeuklHn/8cSwWy7CVYjTBKsSHNTQ0cPToUT7//HMuX76MwWBg6dKl6PX6Me/znXA7u7ler5crV64QCoWwWCwkJydTVVXFgQMHWLBggWjC3W67d4qwY3fw4EEqKir47rvvmDFjBgsXLhy3lVwul1NQUIDBYGDSpEns3LmTyspK6urqqKqqQi6Xo9FoxNjKwcFBBgYG8Pl8otmYmZlJUVERy5YtIycnB71ej9PppLy8nP/+97989913nDlzBofDQX9/P+vWrWPy5MliH0aON0GAzZkzRxRwI0lOTubFF19EKpVSVlZGR0cH9fX1nD9//rbOXyKRoNVqSUxMFDfN5syZg9lsRqVSEQwG8fl86HS668Ye+nw+rly5AlwdU/v372f37t00NjZitVrFNMrBwcFb8s8JWpyQI56RkUFOTg4ZGRkkJiaSnJwsKkFjzZiPMr/fLybtjgysHavJFAgE2L59O5s2bcLtdpOfn8/rr7/OmjVrRlXnhbLKTqeTnp4eOjo6aG5u5syZM+LL4XBgMplYv349a9asGddcybuNzbreb8+fP8/WrVvp6+vjpz/9KfPnz+fcuXNUV1ezZcsWkpOTmTlz5j0LeJbJZMydO5dFixbR2NhIWVkZW7duxWKxkJmZec33x2p8CCbfz3/+c2bOnElJSQlnz56lubkZu92Oy+XC4/EQCoWIiYkRUwEtFgv5+fnMnj2bvLy8YTvIarWaZcuWkZqaitFo5LPPPqO+vp6NGzfS1dXFE088QW5u7rA4zqHnk5SUxMMPP8zOnTux2+3X9Fmj0TBv3jySk5M5ceKEKEw6OzvxeDxIJJJhsYVDx5CwQ+vz+USXS3NzMxcvXqSkpETcbdbpdKJ5mZ2dzdNPPz2qdhcMBsWwop6eHj744AMx+kBwa0RFRWE2m4mMjBR3qqVSKTKZTLTWfD4fgUAAp9OJ3W6np6cHq9VKeXk5Go2G+Ph4Mcj/97///Zim7gmMuYBLSEggMTERh8PByZMnOXv2LCaTCalUikajEeOqFAoFSqXyttuXSCQ0Njby3nvvYbPZiI6O5qWXXmLx4sXY7Xa6urpEX4BQZqmvr4+Ojg5aW1tpbW2lubmZhoYG2traCAQC6PV6Zs6cyWOPPcZLL71ESkqKeJPud/zPrR7f7/fz3Xff8eGHH9LR0YHZbGbJkiVUVVWxfft2vv/+e3HTID8//57tECclJfH4449TVVXFwYMH+eabb7BYLLz44os3Ne3uFq1Wy7x58ygsLMRqtdLQ0EBHRwd2ux2n00kwGBT9e8nJyaIf7HrjUijm+MorrxAZGcmnn35KQ0MDH3zwARUVFSxdupTCwsJr2hD8VhKJhNjY2FEFHFxVCDIyMkhPT8fn89HX10dPT88wASe0I+z+SyQSAoGAKLjsdjsXL17k1KlTnD17lpaWFjHYOyIiQvxdYWEhjz766KgCTqVSMX36dCoqKujv70cul5OamorJZCIhIYHk5GTMZjMGg0HMCxd2f+VyuTjHPR4PPp9PDEcShG5LSwsdHR10dXXR1NTEmTNnWL169YMh4NRqtahFnT9/njfffBOz2SzGLgkXWYg/u11kMhknTpygsrKSQCCAVqulu7ubjz/+WBwMgnCz2+10d3djs9lEtVoYIBqNhtTUVFJSUpgxYwbFxcUsWrTonvgFQqEQXq+XgYEBHA4HDocDt9uNz+cb1ZwWsg+0Wi0ajQalUik6p4XBXlNTQ2lpKT09PcTFxaHT6cjKyuK5556jra2NY8eO8fnnn+N2u3nuuecoKiq6rfSou2H27Nk888wztLe3c/78eT799FP0ej3r1q3DZDKN+/GVSiWTJ08eZkYGAgFCodAdmcqpqals2LABo9HI559/TkVFBQcPHuTkyZNkZmYyefLkYX4z4X63t7eLaVlCmaPr+aXlcjkmk+mWr89Qjc7r9dLS0sKxY8c4ceIE1dXV2O12/H4/EokEtVpNYWHhdTcv9Ho9a9euFTc9hBzx1NRULBYLMTExo5ZjulERh6FPva+urqahoYGmpiaampowGAykpaWNi0Ix5uWSbDYbixYtEoNHhyL4PUKhEEqlcljdqVtFKpXicDgYGBgQ24yIiBBXsJEolUrUarWYUBwVFYXJZMJisZCXl0dhYSE5OTlERkZesxEiXGxhMggxQVKplEAgQDAYRCaTIZPJxJVRaGNoMU6/3y/uOAnquhBz1t7eTmdnJ3a7nYGBgVGvh0ajISEhAZPJRFxcnBjuolKpkMlkDA4Osn37dr766ivsdjvPPvssr732GtnZ2fj9frZv384///lPysvLCQQCFBUV8cwzz/DII49gsVjuSJO+Xbq6uvjggw949913aWlpYdq0abz66qs89dRTGI3GcVlUxiMcYaggcTgclJaWsm3bNkpLS2ltbcVut+Pz+Ub9bUREBAqFgoGBAfR6PS+88AJvvfXWqEUjbrfPo/0mEAjQ19dHbW0tV65cwefzIZFI0Ov1TJkyZcw1ptupUiNUNmlvb0epVJKSknLd794NY67BabVa1q9fz6FDh6iqqsJqtYqfBYNBzGYzCoViWEbB7TDyAgQCAWJjY1Gr1WKVEblcjkKhQK1WExcXR3x8PNHR0SQmJpKUlERqauqwaHlB9Xc6nQwODuJ2u8WXoAkKta4cDgdSqRSPx4Pf70epVKJUKkWBJghbQVhJJBKxErGQQdDW1kZnZyd9fX3DhPL1wmeGmiOCsI6KihJN/sHBQXFbf8aMGaxdu5asrCxxh2716tUoFAref/99jh49Klb0OHfuHE8++SSzZs0a9/psJpOJtWvXYrfb+eijj6isrGTjxo3odDqeeOKJu844GI3xEJpD29RqtSxevJhp06ZRWlo6rErHSCEnWDAKhYITJ06I4TGjaZB30u/RfiOTyTAYDBQVFd12e3fC7fRbKpWKYVvjybgUvHS73bS0tLBt2zb+/e9/09jYSCgUQqVSsXz5cmbPnn2NH+F2OHLkCAcOHBBLLM2aNQuz2UxqaipJSUlERkaK6v3QOCLBL+D1ekWNT3gJQaGdnZ3i9ndvby+dnZ3D/HqCP8Tv94tO16EVOQSBOXR3yev1DsumELRKIT5Ip9OJuZsjw1MEYSmYs/39/QwMDIhZGIKZo9VqSUlJ4Ze//CVPPfXUMAe5cK2PHTvGRx99xL59+2htbUUikVBUVMTLL7/M8uXLxSDo8aSuro6NGzeyefNm2tvb2bBhA3/6059ITk4e1+OOByM1RI/HQ1dXF52dnbjd7mHZDAqFQnxmwc6dO/H5fKxZs4asrCzx92HGnnHZq9doNEydOpU//OEPxMbG8v7773P+/Hm8Xi+1tbXk5+ezdOlScnJyxHzF22HPnj10d3dz7tw5vF4vdXV19Pf3093dLSYGG41G1Gq16OAVBIGwiyY4cYVE8P7+fvHvocJKLpeLgkfYKBE0LcHcHrlGCEJMMFflcjlKpZLIyEgxuV2I97JYLCQlJREdHT1q/J2gOQo1ulpbW7HZbPT09ODz+fD5fASDQZKTk8WQAIPBcI3JIpVKWbBgAampqRQUFPDFF19w8uRJDh48SHd3N4FAgNWrV497wcKsrCx+9atfIZPJKCkpITMz84FMiYNrn5mhVCpJTk6+qbDOyMggEAg8MOW7HmTGtWS50PTevXt55513OHnypJi4O3PmTNavX8/ChQuZPHnyMEF3Pd+J8L7P52Pbtm188sknNDQ0iP6rwcHB6zrqR0MqlaJSqcSofiESXK1Wo9VqxR22hIQE0fQVSp4LvjfBPyeYo4J/USg7FAqFiIyMFCuWxMXFiVH5dxpMLIS9CLtnoVBIjO0a+h273Y7NZhvmDxTKuh86dIj333+fmpoagsEga9eu5Y9//CN5eXn3JEautbWVtrY2kpKSSEpKCmswYcaFcRNwI4VUY2Mjn332GV9++SX19fX09fWhVCrFYogLFy4k7f/rwY3MPhitTYCGhgbKysq4ePEiVquVtra2a0y4oW0IqUtChLVCoSAxMVEsHWQwGIiNjRVjooxGo5jDejfVRUb2+04n8+3UxHM4HHz55ZccPnxY3JCB/6Um9fb20tDQgM1mQ6VSsX79et544w2ys7Pvaamo+12WKswPm3EXcCMH7tGjR9m8eTOHDx+mubkZp9OJRCKhsLCQFStWUFRUxNSpUzGZTKI5OLLNYSfw/58PDg5it9vxeDz09/cP01yGCjiVSiU+3Ukmk6HX68WNguvVpBt5Drc7Ke+HgLtw4QK/+93v2Lt376gmtBCHqNfryc/PZ8OGDTzyyCM3fBrTeBAWcGHGk3v+VC0hJujAgQN89tlnlJWVYbVaxfig5ORkVq1axcMPP0x2djbx8fHExMQMC2UY6fsQ3huPvo7W9v0ScLdKKBSivb2df/zjH+zfvx+v1ytuhsDV3T+j0UhiYiJZWVksWrRIrIF3rwVNWMCFGU/ui4ATcLlcHD58mB07dnD8+HGsViu9vb34/X60Wi35+fksXLiQefPmMWnSJAwGg/hwjKFaGYQniMDQ69vZ2YnVasXr9Q7LvxwaZjIW5cnDhJmo3Jfnoo48pN/v5+zZs+zYsYMDBw6Iz2l0uVyEQiHi4uLIzc1l1qxZzJgxQ0yA1mg04sQVtI8f+wQdTVsc6Q+9kakfJswPifv24OfrmW3Nzc0cPXqUffv2cerUKbq7u+nr68PtdgNXQ1AyMjLIzs5m0qRJZGZmYjabSUhIQK1WiyEZQm6ckM4EV9NfhODeH/KEvpFACwu4MD8mJsST7UfD5/Nx4cIFDh06RElJCRUVFaKgE/I24erENBgMGI1GTCaTWB1CLpeLMWxKpZJgMIjJZOInP/kJ8fHxD3y9tzBhwtycCSvg4H+aiNfrpaamhlOnTlFVVUVFRQWXLl1icHBQzEwQshOEuLDrsWXLFh5//HHUavW9Oo0wYcLcJx6Ix1UrFAqmT59OQUEBcLXYZWNjI83NzeK/LS0tYjaDEPclpFMJgbhqtZqYmJhxT0cKEybMxGBCa3Bw6xUh3G43DoeDnp4e3G43gUCArq4usWa8z+cjKyuLwsJCVCpV2OcUJsyPgAkv4MaacDhEmDA/Hh4IE3Us0edF+gAAADVJREFUCQu2MGF+PISdUWHChPnBEhZwYcKE+cESFnBhwoT5wRIWcGHChPnBEhZwYcKE+cHyfywAOdP0+eiMAAAAAElFTkSuQmCC""
                width=""300px"" alt=""signature"">
              <br>
              <p>Elvis Mrizi<br> Director </p>
            </div>
            <div class=""col-4 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" style=""line-height: 0.9;"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
               <tr {{RegStyle}}>
                    <td>Reg.Fee</td>
                    <td class=""text-right"">${{RegFee}}</td>
                  </tr>
                  <tr>
                    <td>Gross Price</td>
                    <td class=""text-right"">${{TotalGrossPrice}}</td>
                  </tr>
                  {{TotalAddins}}
                  <tr>
                    <td>Paid</td>
                    <td class=""text-right"">${{Paid}} </td>
                  </tr>
                  <tr>
                    <td>Balance due</td>
                    <td class=""text-right"">${{Balance}} </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <div class=""row"">
            <div class=""col"">
              *All fees above are in Canadian Dollars 
            </div>
          </div>
          <hr style=""width: 100%; border-width: 2px; border-color: #000;"">

          <div class=""row mtable"" style="" background: #fff; border-bottom: 1px solid #fff"">
            <div class=""col-6"" style=""line-height: 1;"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td width=""250"">Canadian Dollar Transfers:</td>
                    <td class=""text-right"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td> Business name:</td>
                    <td>Eli Camps Inc.</td>
                  </tr>
                  <tr>
                    <td>Business address:</td>
                    <td>360 Ridelle Ave. Suite 307, Toronto Ontario M6B 1K1 </td>
                  </tr>
                  <tr>
                    <td> Account Insitution number:</td>
                    <td>004 </td>
                  </tr>
                  <tr>
                    <td>Account number:</td>
                    <td>5230919 </td>
                  </tr>
                  <tr>
                    <td>Account transit:</td>
                    <td>12242 </td>
                  </tr>
                  <tr>
                    <td>SWIFT CODE:</td>
                    <td>TDOMCATTTOR </td>
                  </tr>
                  <tr>
                    <td>Bank Name:</td>
                    <td>TD Canada Trust </td>
                  </tr>
                  <tr>
                    <td>Bank Address:</td>
                    <td>777 Bay Street Toronto ON M5G2C8 </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </main>
      </div>
    </div>
  </div>

</body>

</html>

";

        string LOAInvoiceWOPHTML = @"<!DOCTYPE html>
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
      padding: 0;
      font-family: Arial, Helvetica, sans-serif;
      font-weight: 500 !important;
      font-size: .9rem;
      line-height: 1.5;
      background: #fff;
      color: black;
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
     /* padding-bottom: 50px */
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
      padding: 2px;
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
        <main>
          <div class=""row"">
            <div class=""col-md-4  invoice-to"" style="" margin-top: 0;
            margin-bottom: 0"">
            </div>
            <div class=""col-md-8 "">
              <h4 class="""">OFFICIAL LETTER OF ACCEPTANCE </h4>
            </div>
          </div>
          <div class=""row"">
            <div class=""col-md-12 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Name:</td>
                    <td style=""width:30%;font-size: 12px;"">{{StudentFullName}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;font-size: 11px;"">Student Number:</td>
                    <td style=""width: 25%;font-size: 11px;"">{{Reg_Ref}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Country:</td>
                    <td style=""width:20%;font-size: 12px;"">	{{Country}}</td>
                    <!-- <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">{{student?.homeAddress}}
                    </td> -->
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Date Of Birth:</td>
                    <td style=""width:20%;font-size: 12px;"">{{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <!-- <td style=""width: 35%;"">Ciudad de México.
                    </td> -->
                  </tr>
        					{{PassportNumber}}
                </tbody>
              </table>
  
            </div>
  
  
          </div>
          <div class="""">
            <h6>Dear {{StudentFullName}}</h6>
            <p> This letter is to confirm your acceptance and registration at Eli Camps as
              described in the following Letter Of Acceptance (LOA). A space has been reserved for your commencement on
              the date as mentioned below.</p>
            <p>
              You may use this Letter Of Acceptance for the purpose of obtaining a tourist visa to Canada. Please note,
              this
              acceptance does not guarantee your admission to Canada and is only an acceptance to study at Eli Camps.
  
            </p>
            <h6> Dear Visa Officer </h6>
            <p>Please accept this letter as a formal certification and acceptance of the above mentioned
              student at Eli Camps for studying English as a Second Language as outlined below.</p>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"" cellpadding=""0"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">DATES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  Start Date: {{ProgrameStartDate}}<br>
                  End Date: {{ProgrameEndDate}}
                </td>
              </tr>
            </table>
          </div>
  
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">CAMPUS</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{CampusAddressOnReports}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">ACADEMIC PROGRAM
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{ProgramName}}<br>
                  {{SubProgramName}}
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">ACCOMODATION</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{MealPlan}}<br>{{FormatName}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">SERVICES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  	{{Included_Services}}
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: lightgray;font-size: 11px;"">ADDITIONAL SERVICES
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
              <img style='page-break-inside: avoid'
                src=""data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAATgAAAAzCAYAAAADzxQdAAAABHNCSVQICAgIfAhkiAAAABl0RVh0U29mdHdhcmUAZ25vbWUtc2NyZWVuc2hvdO8Dvz4AACAASURBVHic7Z1pUFRX9sB/3U2vdDfQdLM1DSiLAoIoGkUlatSYUuMkZkrNNpkk4yxJTU3NJPk2NTVfplIzqZqaVGoqTiqpmWwa45jEaBLjvqECLqgsArKI2KwNDb1A7/8P/t8bQNxBMelfVZfYy333vXfvueece855klAoFCJMmDBhfoBE3O8OhAkT5ofHUL1JIpHct35I79uRw4QJ84MgFAoRCoUIBoO43W7a2trud5dEwgIuTJgwd00wGKS5uZnf/va3vPzyy5SUlNzvLgFhEzVMmDB3SSgUoqOjgz//+c989dVXyGQy9u7dy4IFC+5318IaXJgwYW4fwSwFcLlcbNmyhe3bt+NwOPD5fPT399/nHl4lLODChAlzxwSDQbq6utiyZYso1ARf3EQgLODChAlzx4RCIQYHB+no6BDfCwQCtLe3MxEi0MI+uDBhHlDuZyjG0ONJpVKkUumw/8fGxt7T/lyPB1aDE3wAQ19hwvwYCYVC9Pb2cuDAAY4dO4bX671nx5ZKpeh0OjIyMgDQaDQ8++yzbNiw4b7Gvwk8sAIuTJgfO4IAcTgc7NixgzfeeIO//OUv7N+/f9yPPVSp0Gq1LFy4EAC5XE5OTg4zZ86cEIrHAyvgJBLJNa8wYX5sSCQSXC4XFRUVnDp1ilOnTnHu3Ll72ge1Ws2iRYuIi4tjcHCQ48ePEwwG72kfrseE98H5/X5cLhcOhwOXy4XNZqOvr4+2tjb8fv+w74ZCIQwGA0lJSaSkpJCYmEhExIQ/RZGRq51EIpkwKS+3ymjncL3PH4TzeRDQarXMnj2buXPnYjQamT179g2/L9yDG13/W/mO8LlcLic9PZ1HH32UL774Aq/XO2EEnGSiJdv39vbS3NxMS0sL7e3ttLe309bWRnd3N263G6fTycDAAP39/ddcxFAohEajISoqisTERKZNm0ZhYSFz584lJiZGvFkTdWKNvBV+v3+YA3ei9nsoI8/B6/XS2dmJRCLBZDKhUCjEzx6E85noCClSPT091NTUoFQqyc3NRavV3vA38L/rHwqFCAQCosKgUqluWcAJeDwezp49y+HDh8nPz2fJkiXIZLK7ObUxYUIIuLa2Ni5cuEBlZSU1NTVcvHhR1NQcDgdOpxOPx0MwGBQvfEREBHK5fFg7wWAQj8cjfh4bG4vFYqGoqIhf//rXZGZmEhERMWEnlnBuHo+HPXv2cObMGXJzc1m0aBEGg2HC9nsoQydGd3c3u3fvZteuXQQCAXJzc5k/fz4FBQVERUXd554++NxI47/ZWPF6vVy+fJnq6mouXbpET08Pg4ODABiNRqZOnUpxcTE6ne6W+xIIBHA4HERGRg5byO4n981+s9vt1NTUUF5ezunTp2loaODy5ct0d3fjcrkAUCqVaLVaLBYLRqORqKgoYmNjiYqKIioq6ppVKhgMYrPZuHz5MhcuXKC+vp6Ojg7q6up46KGHSE1NndAmqzAo29vb+fjjjzl06BDZ2dmYzWYeeuihMRNw11vTxlKA9vf38+WXX/Kvf/2LqqoqgsEgBw4cYO/evaxYsYInn3yS9PT0MTvezUzj+81ogicYDOJwOLBarQQCAYxGIwkJCbetPd0qwWAQp9PJ+fPnKSkp4eTJkzQ1NYlzLhAIAFd3QlNSUli1ahU/+9nPMJvNN21bIpEQERFBTEzMmPb5brnns72lpYXjx49z7NgxqqqqqK+vp729Ha/Xi1KpJD4+njlz5pCWlkZycjJJSUnExcURFRVFZGQkOp2OyMhIVCrVNatEKBRiYGCA7u5uWltbKS8v5+jRo/j9fhITE5HJZBNu4A9l6CR1uVz09vbidDrFgfcgIFzftrY29u/fT0VFBTqdDq1WS29vL4cOHaKpqYnW1lZeeOEF8vLyxmXRCYVCE/JeC/e4r6+PEydOsG/fPurq6ggGg6Snp7N+/Xpmz549LK7seox2fqO9JwjSY8eOceDAAU6ePEltbS2dnZ0EAgHUajV6vR6NRoPf76e3txer1YrVakUikfD6669PaMXgRtyzXnd1dbFv3z527drFmTNnaGhowOVyIZfLSUtLIycnh/z8fKZMmYLFYiEhIQGDwYBOp0OhUAy7cTdb4cxmM9OnT2fOnDksWbKEQCBAQUHBhFGbb4bJZOL5559n6tSp5OXlkZ6efseT1ev10t7eTldXF8FgEJVKhU6nw2AwEBkZiUwmG5et/N7eXmw2G4FAgDlz5rBixQqamprYuXMnDQ0NbNq0ia6uLp599lnmzZtHdHT0Lbct+JwaGxvFCPqEhAQyMzOHmb4TVcj19/ezZ88e3n77bSorK+nr6wMgOjoap9OJ2WzGYrGM2fHcbjf79+/nrbfeorq6mr6+PuRyORkZGcyYMYNp06YRHx+PSqXC5/NRV1fHJ598wuXLl/niiy9Yt24daWlpwMTTjG/GuAm4oUKotLSUzZs3s3//fmpqavD7/ZhMJtEfM3PmTNLT00lLS7vG1zTUETr0/zc6pvA9o9FIcXHxHff7fqHRaFi5ciXFxcXo9XoiIyPvqB2Xy8WuXbvYtWsXVquVUCiEQqEgKiqKpKQkEhMTiYuLE/+OjY1Fr9eLq/XdXIuh/lKLxcLKlSuRSqVkZmbyySefcPr0ab7++msuX77MqlWrWLx4MVlZWeh0ulG1l0AggN1up6mpierqaioqKqipqaGnpwe4uijMnTuXVatWMW3aNNHBfafnMNbjQGjH7/dTU1PDe++9R0lJCXq9ntmzZ+Nyuaiurqa8vJy6uroxFXA+n4+GhgZKS0vRarUUFxcza9Ys5s6dS3Z2NikpKWg0GqRSKcFgkNbWVvr7+/nnP//JlStXOHfunCjgHjTGVYPz+Xzs3r2bd999l0OHDuFyuYiNjWX27Nk89thjFBYWkpWVhdFovGZQj4VW4XK56OnpwWaz4XQ6xU0LoW2NRkN0dDTR0dHEx8djNBrvemKMFVqt9pYdvEMR+u33+9m9ezfvvPMOpaWlogMZQCaTieZ+TEwMcXFxxMXFYTabSUtLIysri2nTphEXFydqeLd7HdRqNSqVCriqsbjdbvLy8li/fj3x8fF8+OGHHD58mJKSEpqbmzly5AgFBQVkZ2djsVjQ6/VIJBLR5XDlyhXq6uq4cOECDQ0NtLa24nQ6xXETCoU4deoU9fX1vPjiixQVFQ3T2G/nHG429m4l1GW0NiQSCU6nk5MnT3Ls2DH0ej0rVqxg7dq1HD16lOrqavF8xxK1Ws38+fN59dVXiY2NZcGCBUydOpWEhIRrdjplMhnx8fEsWLCA9957D4/HQ0tLywOnuQmMuYATbmwgEGDTpk1s3LiRsrIyQqEQ8+fPZ/Xq1SxYsICCggLUavV1L9zI90fT6kb7zcDAAPX19eJkaG5uxmaz4XK56O/vx+VyieElGo0GvV4vajS5ubk8/PDDZGZmXrNDey8Yy0HU1tbGtm3bKCsrQ6VSMWfOHEwmE263G5vNRmdnJ729vbS3t1NZWQlcvR6xsbGkpKTwwgsv8OSTT95xTqHBYMBgMCCVSuno6MBmswEQExPD8uXLiYuLIyMjg++//56LFy/yzTffcOTIEZKTk0lMTESr1SKVShkYGKC3t5eOjg46OztxuVzIZDKMRiMzZ84kMzOTYDBIRUUFVVVVfPXVV+J9Xrhw4R1rvwKCJiqRSG7JL3ajdgKBgBjO4Xa7ycnJYd26dSxcuJCmpibgf7uRY4lKpaKwsJDU1FRUKhUGg+GG35fL5SQmJqLX6/H5fBOqQu/tMm4a3I4dO/jb3/7GhQsXUKlUrFy5kpdffpni4mI0Go34vbHykwwMDHDmzBmOHz9OaWkpdXV1tLS0YLfbRaErk8mGrepC6AlcvakJCQkcPXqU3/zmN8yaNUvUQB4khGtZW1tLbW0tAwMDLF26lF/84hekpaXhdrvp6emhra2N9vZ2rFYrnZ2ddHR00NHRQXt7O6dOnWLBggV3ldMoCEqtVktLSwuXLl3C7/eL2uO8efNISUmhoKCAI0eOUFlZSXNzM/X19VRVVV1zTpGRkcTHx2OxWEhPTyc3N5fp06czefJkAoEApaWlbNq0icOHD7Nnzx5cLhdOp5Nly5bd9s5eMBiku7ub2tpaWlpacDqdKBQKkpOTycvLIyEhAbjx2B3qWvH5fNTW1nL48GF6e3upqqpCJpNhNpvJz88nGAyK1/puBen1EITWrcw1iUSCSqVCLpfj8/nuaW7rWDOmAk4QJDU1Nbz55ptcuHABtVrN888/zyuvvEJubu41JuBYYLVa2blzJ9u3b6e8vFzUFoxGI/n5+SQlJaHVaomOjiY2NhapVEooFKK7uxuHw0F3dzdNTU1cvnyZLVu2EBMTQ0ZGhjiQH0QE4a5QKCguLqa4uHjYRPf7/Xg8Hvr6+rDZbGJQdX19PQ6Hg0ceeYTo6Og79l1pNBomT55MXFwcra2t1NbW0tvbi8lkIhQKIZVKSU1NZe3atcyfP18UyI2NjXR2djI4OEgwGEShUKDT6TCbzUyaNEl8JSQkDFsoBX+iRqNh9+7dHDlyBIfDQUdHBytWrCA1NfWmWrnX66Wrq4tz585x5MgRysvLuXTpEi6XC4VCgdlspri4mJUrV1JQUIBGo7mhK0P4zOl0cuDAAf76178CVxdWuVxOXFwcRqORUCiETqdDLpfjcrm4dOkSDofjjlwUox3/dvH7/aLVI5fLiY+Pv6t+3Khf4236jouJ+vHHH3Pq1CmkUilPPfUUb7zxBpMmTRIDEccyd7S6upqPPvqIrVu30tzcjFwuJzc3l/z8fKZPn86kSZNITk5Gr9eLL0HA9fX14XK56Ozs5PTp07z99ts0NzdTUVEhxuI9qAimlaAxjdzmj4iIICIigsjISJKSksjLyxPDCbxeLzqd7q40WKlUypQpU0hLS+PixYucOXOGixcvYjKZht17lUpFeno66enpLFmyhN7eXjHoVBBwer2emJiYUc9DIDIykuLiYlQqFRqNhm+//ZaysjK6urqoqalh4cKF5ObmEh8fP6wdn8+H0+nEarVSXV3N6dOnKSsr4/z58/T394vt9ff309zcTGVlJZWVlTz99NMsX75cNPduZol4vV5sNpsYiK7T6cQIAalUSlpaGgkJCXR1dfH1118jk8lIT0/HYDAME+QjEY6rUChQqVQolUpiYmLQ6XQ3dPPcCJ/PR2trKy6XC5PJRFJS0rDjBQIBPB6PmFnk9XqRSCTiohkIBIiIiEClUiGVSomMjESv16NWq+95uMmYHk0ikdDX18e3335LMBhkypQpvPbaa0yaNEkUKmNJXV0df//739m6dSv9/f2kpaWxePFili1bxqxZs65ZtUdGegsrpNFopKGhQRwAWq12QqSZ3A1xcXFotVo8Hg91dXXYbLabagRSqfSuMwyGTqKMjAzy8vI4fvw4Z86coaSkhJycnFGPIezwxsfHX6Mx3OrEVKlUzJ07l8jISGJjY9mxYweNjY385z//4ciRI0ybNo3U1FTi4uJE/6/D4aCrq4vGxkYqKyu5dOkSHo+H2NhYFi1aJArFnp4eysvLOX/+PLt378ZqtdLT08OaNWtITEy8Yb+0Wi1Tp04lJSWF+vp64Oq1lsvlSKVSFAoFmZmZLF68mK1bt1JaWkptbS1JSUmYTCa0Wu2omuLQ95RKJRqNBrVaLZrx6enp4s707eDxeGhubh62wFy6dIm2tja6urqw2Wx0d3fT09NDd3c3Ho8HqVSKz+djcHAQv9+PXC5HrVYjk8mIiooiISGB+Ph4EhISsFgsmM1moqKiHjwNzmq1cvnyZQCKiorIzs4eF59Cd3c37777Lps3b8btdjN9+nReeuklHn/8cSwWy7CVYjTBKsSHNTQ0cPToUT7//HMuX76MwWBg6dKl6PX6Me/znXA7u7ler5crV64QCoWwWCwkJydTVVXFgQMHWLBggWjC3W67d4qwY3fw4EEqKir47rvvmDFjBgsXLhy3lVwul1NQUIDBYGDSpEns3LmTyspK6urqqKqqQi6Xo9FoxNjKwcFBBgYG8Pl8otmYmZlJUVERy5YtIycnB71ej9PppLy8nP/+97989913nDlzBofDQX9/P+vWrWPy5MliH0aON0GAzZkzRxRwI0lOTubFF19EKpVSVlZGR0cH9fX1nD9//rbOXyKRoNVqSUxMFDfN5syZg9lsRqVSEQwG8fl86HS668Ye+nw+rly5AlwdU/v372f37t00NjZitVrFNMrBwcFb8s8JWpyQI56RkUFOTg4ZGRkkJiaSnJwsKkFjzZiPMr/fLybtjgysHavJFAgE2L59O5s2bcLtdpOfn8/rr7/OmjVrRlXnhbLKTqeTnp4eOjo6aG5u5syZM+LL4XBgMplYv349a9asGddcybuNzbreb8+fP8/WrVvp6+vjpz/9KfPnz+fcuXNUV1ezZcsWkpOTmTlz5j0LeJbJZMydO5dFixbR2NhIWVkZW7duxWKxkJmZec33x2p8CCbfz3/+c2bOnElJSQlnz56lubkZu92Oy+XC4/EQCoWIiYkRUwEtFgv5+fnMnj2bvLy8YTvIarWaZcuWkZqaitFo5LPPPqO+vp6NGzfS1dXFE088QW5u7rA4zqHnk5SUxMMPP8zOnTux2+3X9Fmj0TBv3jySk5M5ceKEKEw6OzvxeDxIJJJhsYVDx5CwQ+vz+USXS3NzMxcvXqSkpETcbdbpdKJ5mZ2dzdNPPz2qdhcMBsWwop6eHj744AMx+kBwa0RFRWE2m4mMjBR3qqVSKTKZTLTWfD4fgUAAp9OJ3W6np6cHq9VKeXk5Go2G+Ph4Mcj/97///Zim7gmMuYBLSEggMTERh8PByZMnOXv2LCaTCalUikajEeOqFAoFSqXyttuXSCQ0Njby3nvvYbPZiI6O5qWXXmLx4sXY7Xa6urpEX4BQZqmvr4+Ojg5aW1tpbW2lubmZhoYG2traCAQC6PV6Zs6cyWOPPcZLL71ESkqKeJPud/zPrR7f7/fz3Xff8eGHH9LR0YHZbGbJkiVUVVWxfft2vv/+e3HTID8//57tECclJfH4449TVVXFwYMH+eabb7BYLLz44os3Ne3uFq1Wy7x58ygsLMRqtdLQ0EBHRwd2ux2n00kwGBT9e8nJyaIf7HrjUijm+MorrxAZGcmnn35KQ0MDH3zwARUVFSxdupTCwsJr2hD8VhKJhNjY2FEFHFxVCDIyMkhPT8fn89HX10dPT88wASe0I+z+SyQSAoGAKLjsdjsXL17k1KlTnD17lpaWFjHYOyIiQvxdYWEhjz766KgCTqVSMX36dCoqKujv70cul5OamorJZCIhIYHk5GTMZjMGg0HMCxd2f+VyuTjHPR4PPp9PDEcShG5LSwsdHR10dXXR1NTEmTNnWL169YMh4NRqtahFnT9/njfffBOz2SzGLgkXWYg/u11kMhknTpygsrKSQCCAVqulu7ubjz/+WBwMgnCz2+10d3djs9lEtVoYIBqNhtTUVFJSUpgxYwbFxcUsWrTonvgFQqEQXq+XgYEBHA4HDocDt9uNz+cb1ZwWsg+0Wi0ajQalUik6p4XBXlNTQ2lpKT09PcTFxaHT6cjKyuK5556jra2NY8eO8fnnn+N2u3nuuecoKiq6rfSou2H27Nk888wztLe3c/78eT799FP0ej3r1q3DZDKN+/GVSiWTJ08eZkYGAgFCodAdmcqpqals2LABo9HI559/TkVFBQcPHuTkyZNkZmYyefLkYX4z4X63t7eLaVlCmaPr+aXlcjkmk+mWr89Qjc7r9dLS0sKxY8c4ceIE1dXV2O12/H4/EokEtVpNYWHhdTcv9Ho9a9euFTc9hBzx1NRULBYLMTExo5ZjulERh6FPva+urqahoYGmpiaampowGAykpaWNi0Ix5uWSbDYbixYtEoNHhyL4PUKhEEqlcljdqVtFKpXicDgYGBgQ24yIiBBXsJEolUrUarWYUBwVFYXJZMJisZCXl0dhYSE5OTlERkZesxEiXGxhMggxQVKplEAgQDAYRCaTIZPJxJVRaGNoMU6/3y/uOAnquhBz1t7eTmdnJ3a7nYGBgVGvh0ajISEhAZPJRFxcnBjuolKpkMlkDA4Osn37dr766ivsdjvPPvssr732GtnZ2fj9frZv384///lPysvLCQQCFBUV8cwzz/DII49gsVjuSJO+Xbq6uvjggw949913aWlpYdq0abz66qs89dRTGI3GcVlUxiMcYaggcTgclJaWsm3bNkpLS2ltbcVut+Pz+Ub9bUREBAqFgoGBAfR6PS+88AJvvfXWqEUjbrfPo/0mEAjQ19dHbW0tV65cwefzIZFI0Ov1TJkyZcw1ptupUiNUNmlvb0epVJKSknLd794NY67BabVa1q9fz6FDh6iqqsJqtYqfBYNBzGYzCoViWEbB7TDyAgQCAWJjY1Gr1WKVEblcjkKhQK1WExcXR3x8PNHR0SQmJpKUlERqauqwaHlB9Xc6nQwODuJ2u8WXoAkKta4cDgdSqRSPx4Pf70epVKJUKkWBJghbQVhJJBKxErGQQdDW1kZnZyd9fX3DhPL1wmeGmiOCsI6KihJN/sHBQXFbf8aMGaxdu5asrCxxh2716tUoFAref/99jh49Klb0OHfuHE8++SSzZs0a9/psJpOJtWvXYrfb+eijj6isrGTjxo3odDqeeOKJu844GI3xEJpD29RqtSxevJhp06ZRWlo6rErHSCEnWDAKhYITJ06I4TGjaZB30u/RfiOTyTAYDBQVFd12e3fC7fRbKpWKYVvjybgUvHS73bS0tLBt2zb+/e9/09jYSCgUQqVSsXz5cmbPnn2NH+F2OHLkCAcOHBBLLM2aNQuz2UxqaipJSUlERkaK6v3QOCLBL+D1ekWNT3gJQaGdnZ3i9ndvby+dnZ3D/HqCP8Tv94tO16EVOQSBOXR3yev1DsumELRKIT5Ip9OJuZsjw1MEYSmYs/39/QwMDIhZGIKZo9VqSUlJ4Ze//CVPPfXUMAe5cK2PHTvGRx99xL59+2htbUUikVBUVMTLL7/M8uXLxSDo8aSuro6NGzeyefNm2tvb2bBhA3/6059ITk4e1+OOByM1RI/HQ1dXF52dnbjd7mHZDAqFQnxmwc6dO/H5fKxZs4asrCzx92HGnnHZq9doNEydOpU//OEPxMbG8v7773P+/Hm8Xi+1tbXk5+ezdOlScnJyxHzF22HPnj10d3dz7tw5vF4vdXV19Pf3093dLSYGG41G1Gq16OAVBIGwiyY4cYVE8P7+fvHvocJKLpeLgkfYKBE0LcHcHrlGCEJMMFflcjlKpZLIyEgxuV2I97JYLCQlJREdHT1q/J2gOQo1ulpbW7HZbPT09ODz+fD5fASDQZKTk8WQAIPBcI3JIpVKWbBgAampqRQUFPDFF19w8uRJDh48SHd3N4FAgNWrV497wcKsrCx+9atfIZPJKCkpITMz84FMiYNrn5mhVCpJTk6+qbDOyMggEAg8MOW7HmTGtWS50PTevXt55513OHnypJi4O3PmTNavX8/ChQuZPHnyMEF3Pd+J8L7P52Pbtm188sknNDQ0iP6rwcHB6zrqR0MqlaJSqcSofiESXK1Wo9VqxR22hIQE0fQVSp4LvjfBPyeYo4J/USg7FAqFiIyMFCuWxMXFiVH5dxpMLIS9CLtnoVBIjO0a+h273Y7NZhvmDxTKuh86dIj333+fmpoagsEga9eu5Y9//CN5eXn3JEautbWVtrY2kpKSSEpKCmswYcaFcRNwI4VUY2Mjn332GV9++SX19fX09fWhVCrFYogLFy4k7f/rwY3MPhitTYCGhgbKysq4ePEiVquVtra2a0y4oW0IqUtChLVCoSAxMVEsHWQwGIiNjRVjooxGo5jDejfVRUb2+04n8+3UxHM4HHz55ZccPnxY3JCB/6Um9fb20tDQgM1mQ6VSsX79et544w2ys7Pvaamo+12WKswPm3EXcCMH7tGjR9m8eTOHDx+mubkZp9OJRCKhsLCQFStWUFRUxNSpUzGZTKI5OLLNYSfw/58PDg5it9vxeDz09/cP01yGCjiVSiU+3Ukmk6HX68WNguvVpBt5Drc7Ke+HgLtw4QK/+93v2Lt376gmtBCHqNfryc/PZ8OGDTzyyCM3fBrTeBAWcGHGk3v+VC0hJujAgQN89tlnlJWVYbVaxfig5ORkVq1axcMPP0x2djbx8fHExMQMC2UY6fsQ3huPvo7W9v0ScLdKKBSivb2df/zjH+zfvx+v1ytuhsDV3T+j0UhiYiJZWVksWrRIrIF3rwVNWMCFGU/ui4ATcLlcHD58mB07dnD8+HGsViu9vb34/X60Wi35+fksXLiQefPmMWnSJAwGg/hwjKFaGYQniMDQ69vZ2YnVasXr9Q7LvxwaZjIW5cnDhJmo3Jfnoo48pN/v5+zZs+zYsYMDBw6Iz2l0uVyEQiHi4uLIzc1l1qxZzJgxQ0yA1mg04sQVtI8f+wQdTVsc6Q+9kakfJswPifv24OfrmW3Nzc0cPXqUffv2cerUKbq7u+nr68PtdgNXQ1AyMjLIzs5m0qRJZGZmYjabSUhIQK1WiyEZQm6ckM4EV9NfhODeH/KEvpFACwu4MD8mJsST7UfD5/Nx4cIFDh06RElJCRUVFaKgE/I24erENBgMGI1GTCaTWB1CLpeLMWxKpZJgMIjJZOInP/kJ8fHxD3y9tzBhwtycCSvg4H+aiNfrpaamhlOnTlFVVUVFRQWXLl1icHBQzEwQshOEuLDrsWXLFh5//HHUavW9Oo0wYcLcJx6Ix1UrFAqmT59OQUEBcLXYZWNjI83NzeK/LS0tYjaDEPclpFMJgbhqtZqYmJhxT0cKEybMxGBCa3Bw6xUh3G43DoeDnp4e3G43gUCArq4usWa8z+cjKyuLwsJCVCpV2OcUJsyPgAkv4MaacDhEmDA/Hh4IE3Us0edF+gAAADVJREFUCQu2MGF+PISdUWHChPnBEhZwYcKE+cESFnBhwoT5wRIWcGHChPnBEhZwYcKE+cHyfywAOdP0+eiMAAAAAElFTkSuQmCC""
                width=""300px"" alt=""signature"">
              <br>
              <p>Elvis Mrizi<br> Director </p>
            </div>
          </div>
          <hr style=""width: 100%; border-width: 2px; border-color: #000;"">
        </main>
      </div>
    </div>
  </div>
</body>
</html>
";

        string LOAGroupInvoiceHTML = @"<!DOCTYPE html>
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
    html,
    body {
      margin: 0;
      padding: 0;
      font-family: Arial, Helvetica, sans-serif;
      font-weight: 400 !important;
      font-size: .9rem;
      line-height: 1.5;
      background: #fff;
      color: black;
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

    /* .data {
      font-weight: bolder !important;
    } */

    .mtable table td,
    .mtable table th {
      padding: 2px;
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
        <main>
          <div class=""row"">
            <div class=""col-md-4  invoice-to"" style="" margin-top: 0;
            margin-bottom: 0"">
            </div>
            <div class=""col-md-8 "">
              <h4 class="""">OFFICIAL LETTER OF ACCEPTANCE </h4>
            </div>
          </div>
          <div class=""row"">
            <div class=""col-md-12 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Name:</td>
                    <td style=""width:30%;font-size: 12px;"">{{StudentFullName}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;font-size: 11px;"">Student Number:</td>
                    <td style=""width: 25%;font-size: 12px;"">{{Reg_Ref}}
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Country:</td>
                    <td style=""width:20%;font-size: 12px;"">{{Country}}</td>
                    <!-- <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">{{student?.homeAddress}}
                    </td> -->
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Date Of Birth:</td>
                    <td style=""width:20%;font-size: 12px;"">{{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <!-- <td style=""width: 35%;"">Ciudad de México.
                    </td> -->
                  {{PassportNumber}}
                </tbody>
              </table>
  
            </div>
  
  
          </div>
          <div class="""" style=""font-size:12px"">
            <h6>Dear {{StudentFullName}}</h6>
            <p> This letter is to confirm your acceptance and registration at Eli Camps as
              described in the following Letter Of Acceptance (LOA). A space has been reserved for your commencement on
              the date as mentioned below.</p>
            <p>
              You may use this Letter Of Acceptance for the purpose of obtaining a tourist visa to Canada. Please note,
              this
              acceptance does not guarantee your admission to Canada and is only an acceptance to study at Eli Camps.
  
            </p>
            <h6> Dear Visa Officer </h6>
            <p>Please accept this letter as a formal certification and acceptance of the above mentioned group leader at Eli Camps for studying
            English as a Second Language as outlined below.
            </p>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"" cellpadding=""0"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee;font-size:12px"">DATES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  Start Date: {{ProgrameStartDate}}<br>
                  End Date: {{ProgrameEndDate}}
                </td>
              </tr>
            </table>
          </div>
  
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee;font-size:12px"">CAMPUS</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{CampusAddressOnReports}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee;font-size:12px"">ACADEMIC PROGRAM
                </td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{ProgramName}}<br>
                  {{SubProgramName}}
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee;font-size:12px"">ACCOMODATION</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">{{MealPlan}}<br>{{FormatName}}</td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee;font-size:12px"">SERVICES</td>
              </tr>
              <tr>
                <td style=""width: 50%;""></td>
                <td style=""width: 50%;"" colspan=""2"">
                  {{Included_Services}}
                </td>
              </tr>
  
            </table>
          </div>
          <div class=""row mtable"" style=""
          background: #fff;
          border-bottom: 1px solid #fff"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
            border-collapse: collapse;
            border-spacing: 0;"">
              <tr>
                <td colspan=""3"" style=""background-color: #eee;font-size:12px"">ADDITIONAL SERVICES
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
              <img style='page-break-inside: avoid'
                src=""data:image/png;base64,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""
                width=""300px"" alt=""signature"">
              <br>
              <p>Elvis Mrizi<br> Director </p>
            </div>
            <div class=""col-4 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" style=""line-height: 0.9;"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
               <tr {{RegStyle}}>
                    <td>Reg.Fee</td>
                    <td class=""text-right"">${{RegFee}}</td>
                  </tr>
                  <tr>
                    <td>Gross Price</td>
                    <td class=""text-right"">${{TotalGrossPrice}}</td>
                  </tr>
                  <tr>
				  {{TotalAddins}}
                    <td>Paid</td>
                    <td class=""text-right"">${{Paid}} </td>
                  </tr>
                  <tr>
                    <td style="""">Balance due</td>
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
  
          <div class=""row mtable"" style="" background: #fff; border-bottom: 1px solid #fff"">
            <div class=""col-6"" style=""line-height: 1;"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>
                  <tr>
                    <td width=""250"" style="""">Canadian Dollar Transfers:</td>
                    <td class=""text-right"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""""> Business name:</td>
                    <td class="""">Eli Camps Inc.</td>
                  </tr>
                  <tr>
                    <td style="""">Business address:</td>
                    <td class="""">360 Ridelle Ave. Suite 307, Toronto Ontario M6B 1K1</td>
                  </tr>
                  <tr>
                    <td style=""""> Account Insitution number:</td>
                    <td class="""">004 </td>
                  </tr>
                  <tr>
                    <td style="""">Account number:</td>
                    <td class="""">5230919 </td>
                  </tr>
                  <tr>
                    <td style="""">Account transit:</td>
                    <td class="""">12242 </td>
                  </tr>
                  <tr>
                    <td style="""">SWIFT CODE:</td>
                    <td class="""">TDOMCATTTOR </td>
                  </tr>
                  <tr>
                    <td style="""">Bank Name:</td>
                    <td class="""">TD Canada Trust </td>
                  </tr>
                  <tr>
                    <td style="""">Bank Address:</td>
                    <td class="""">777 Bay Street Toronto ON M5G2C8 </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </main>
      </div>
    </div>
  </div>

</body>
</html>

";

        string StudentInvitationHTML = @"<!DOCTYPE html>
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
    html,
    body {
      margin: 0;
      padding: 0;
      font-family: Arial, Helvetica, sans-serif;
      font-weight: 500 !important;
      font-size: .9rem;
      line-height: 1.5;
      background: #fff;
      color: black;
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
      margin-bottom: 10px
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
      padding: 2px;
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
          <div class=""row contacts"" style=""margin-bottom: 10px"">
            <div class=""col-md-4  invoice-to"" style="" margin-top: 0;
            margin-bottom: 0"">
              <!-- <div class=""text-gray-light"">Invoice Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                {{currentDate | date:'longDate' }}
              </div> -->
            </div>
            <div class=""col-md-8 "">
              <h4 class="""">LETTER OF INVITATION</h4>
            </div>
          </div>
          <div class=""row contacts"" style=""margin-bottom: 10px"">
            <div class=""col-md-12 mtable"" style=""
            background: #fff;
            border-bottom: 1px solid #fff"">
              <table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""width: 100%;
              border-collapse: collapse;
              border-spacing: 0;"">
                <tbody>

                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Name:</td>
                    <td style=""width:30%;font-size: 12px;"">{{StudentFullName}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 25%;"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Student Number:</td>
                    <td style=""width:25%;font-size: 12px;"">{{Reg_Ref}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <td style=""width: 35%;"">
                    </td>
                  </tr>
                  <tr>
                    <td style=""width: 15%;font-size: 11px;"">Date Of Birth:</td>
                    <td style=""width:20%;font-size: 12px;""> {{DOB}}</td>
                    <td class=""text-right"" style=""width: 30%; vertical-align: text-top ;""></td>
                    <!-- <td style=""width: 35%;"">Ciudad de México.
                    </td> -->
                  </tr>
                  {{PassportNumber}}
                </tbody>
              </table>
            </div>
          </div>
          <div class="""">
            <h6>Dear {{StudentFullName}}</h6>
            <p> On behalf of EliCamps I would like to extend our invitation to you at our summer camp in Toronto,
              Ontario
              from {{ProgrameStartDate}} to {{ProgrameEndDate}}. The program will be hosted and held at the
              {{CampusAddressOnReports}}.</p>
            <p>
              The program is a combination of English lessons and visits to renowed attractions in Toronto and Ontario.
              All preparetions have been made and a place has been reserved for you and your group in our campus.
            </p>
            <p>Should you have any questions, please feel free to contact us anytime. We are more than happy to help.
            </p>
            <div class=""row"">
              <div class=""col-8"">
                <p>Sincerely<br>Eli Camps Admissions</p>
                <br>
                <img style='page-break-inside: avoid'
                  src=""data:image/png;base64,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""
                  width=""350px"" alt=""signature"">
                <br>
                <p>Elvis Mrizi<br> Director </p>
              </div>
            </div>
          </div>
        </main>
      </div>

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
            studentPDFDataVM.Address = emailSendVM.Address;
            var subject = emailSendVM.Subject;
            if (subject == "")
            {
                subject = $"Registration confirmation { studentPDFDataVM.FirstName} {studentPDFDataVM.LastName}";
            }
            if (emailTemplate != null && studentPDFDataVM != null && !string.IsNullOrEmpty(emailSendVM.StudentEmail))
            {
                var email = new EmailViewModel();

                email.Subject = subject;
                email.Message = emailTemplate;
                email.Message = email.Message.Replace(EmailSender.EmailBodyTag, emailSendVM.EmailBody);
                email.To = emailSendVM.StudentEmail;
                response = true;
                Task.Factory.StartNew(() => SendRegistrationEmailWithDocument(emailSendVM, email, studentPDFDataVM));
            }
            return response;
        }

        private void SendRegistrationEmailWithDocument(EmailSendVM emailSendVM, EmailViewModel email, StudentPDFDataVM studentPDFDataVM)
        {
            if (emailSendVM.IsAgentInvoice)
            {
                email.AgentInvoiceTemplate = AgentInvoiceHTML;
                var agencyData = studentPDFDataVM.Clone();
                AgentInvoiceTemplateRendrer(agencyData, email);
                PDFCreator(email, "AgentInvoice.pdf", email.AgentInvoiceTemplate);
            }
            if (emailSendVM.IsStudentCertificate)
            {
                email.StudentCertificateTemplate = StudentCertificateHTML;
                email.StudentCertificateTemplate = email.StudentCertificateTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentPDFDataVM.FirstName} {studentPDFDataVM.LastName}");

                PDFCreator(email, "Certificate.pdf", email.StudentCertificateTemplate, true);
            }
            if (emailSendVM.IsStudentInvoice)
            {
                email.StudentInvoiceTemplate = StudentInvoiceHTML;
                var studentInvoiceData = studentPDFDataVM.Clone();
                StudentInvoiceTemplateRendrer(studentInvoiceData, email);
                PDFCreator(email, "Invoice.pdf", email.StudentInvoiceTemplate);
            }
            if (emailSendVM.IsAirportInvoice)
            {
                email.AirportInvoiceTemplate = AirportInvoiceHTML;
                var studentAirportData = studentPDFDataVM.Clone();
                AirportInvoiceTemplateRendrer(studentAirportData, email);
                PDFCreator(email, "AirportDoc.pdf", email.AirportInvoiceTemplate, true);
            }
            if (emailSendVM.IsLoaInvoice)
            {
                email.LOAInvoiceTemplate = LOAInvoiceHTML;
                var studentLoaData = studentPDFDataVM.Clone();
                LOAInvoiceTemplateRendrer(studentLoaData, email);
                PDFCreator(email, "LOAWithPrice.pdf", email.LOAInvoiceTemplate);
            }
            if (emailSendVM.IsLoaInvoiceWithNoPrice)
            {
                email.LOAInvoiceWOPTemplate = LOAInvoiceWOPHTML;
                var studentLoaWithPriceData = studentPDFDataVM.Clone();
                LOAWOPInvoiceTemplateRendrer(studentLoaWithPriceData, email);
                PDFCreator(email, "LOANoPrice.pdf", email.LOAInvoiceWOPTemplate);
            }
            if (emailSendVM.IsLoaGroupInvoice)
            {
                email.LOAInvoiceTemplate = LOAGroupInvoiceHTML;
                var groupLoaPriceData = studentPDFDataVM.Clone();
                LOAInvoiceTemplateRendrer(groupLoaPriceData, email);
                PDFCreator(email, "LOAGroup.pdf", email.LOAInvoiceTemplate);
            }
            if (emailSendVM.IsStudentInvitation)
            {
                email.StudentInvitationTemplate = StudentInvitationHTML;
                var studentInvitationData = studentPDFDataVM.Clone();
                StudentInvitationTemplateRendrer(studentInvitationData, email);
                PDFCreator(email, "StudentInvitation.pdf", email.StudentInvitationTemplate);
            }

            sendEmail(email);
        }

        public void SendEmail(EmailViewModel message)
        {
            Task.Factory.StartNew(() => sendEmail(message));
            //sendEmail(message);
        }

        private bool sendEmail(EmailViewModel message)
        {
            string clientPassword = _configuration.GetSection("Email").GetSection("Password").Value;
            string FromEmail = _configuration.GetSection("Email").GetSection("EmailID").Value;
            string EmailHost =  _configuration.GetSection("Email").GetSection("Host").Value;
            int EmailPort = int.Parse(_configuration.GetSection("Email").GetSection("Port").Value);
            string EmailCC = _configuration.GetSection("Email").GetSection("EmailCCID").Value;
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                (message.Message, null, MediaTypeNames.Text.Html);
            //var smtpClient = new SmtpClient
            //{
            //    Host = "smtp.gmail.com", // set your SMTP server name here
            //    Port = 587, // Port 
            //    EnableSsl = true,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("elicampswork@gmail.com", "abcd@1234")
            //};
            var smtpClient = new SmtpClient
            {
                Host = EmailHost, // set your SMTP server name here
                Port = EmailPort, // Port 
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail, clientPassword)
            };


            //using (var mail = new MailMessage("elicampswork@gmail.com", message.To))
            using (var mail = new MailMessage(FromEmail, message.To))
            {
                mail.AlternateViews.Add(avHtml);
                mail.Subject = message.Subject;
                mail.IsBodyHtml = true;
                if (!String.IsNullOrEmpty(EmailCC))
                {
                    MailAddress emailCC = new MailAddress(EmailCC);
                    mail.CC.Add(emailCC);
                }
               
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
        
        private void AgentInvoiceTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {
            string addinsInc = "";
            string addinsAdd = "";
            
            studentVM.Commision = ((studentVM.Commision * studentVM.TotalGrossPrice) / 100) + studentVM.CommissionAddins;

            //studentVM.Balance = (studentVM.TotalGrossPrice + studentVM.CommissionAddins - studentVM.Commision - studentVM.Paid);
            //if(studentVM.Balance < 0)
            //{
            //    studentVM.Balance = 0;
            //}
            if (studentVM.StudentPDFAddinAdd.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinAdd)
                {
                    if(count > 1)
                    {
                        addinsAdd = $"{addinsAdd} <br> {addin}";
                    }
                    else
                    {
                        addinsAdd = $"{addin}";
                    }
                    
                    count ++;
                }
            }
            if (studentVM.StudentPDFAddinInc.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinInc)
                {
                    if (count > 1)
                    {
                        addinsInc = $"{addinsInc} <br> {addin}";
                    }
                    else
                    {
                        addinsInc = $"{addin}";
                    }
                    
                    count++;
                }
            }
            string totalAddins = "";
            string commissionAddins = "";
            if (studentVM.TotalAddins > 0)
            {
                totalAddins = $"<tr><td> Additional Services </td><td class=\"text-right\">${String.Format("{0:0.00}", studentVM.TotalAddins)}</td></tr>";
            }
            if (studentVM.CommissionAddins > 0)
            {
                commissionAddins = $"<tr><td> Commission Addins </td><td class=\"text-right\">${String.Format("{0:0.00}", studentVM.CommissionAddins)}</td></tr>";
            }

            string passportNumber = "";
            if (!string.IsNullOrEmpty(studentVM.PassportNumber))
            {
                passportNumber = $"<tr><td style = \"width: 15%;font-size: 11px;\"> Passport Number:</td><td class=\"data\" style=\"width:20%;font-size: 12px;\">{studentVM.PassportNumber}</td><td class=\"text-right\" style=\"width: 30%; vertical-align: text-top ;\"></td><td style = \"width: 35%;\" ></td></tr>";
            }
            var grossPrice = studentVM.TotalGrossPrice - studentVM.Commision;
            studentVM.Balance = grossPrice + studentVM.RegistrationFee + studentVM.TotalAddins - studentVM.Paid;
            if (studentVM.RegistrationFee == 0)
            {
                template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.RegStyle, "style='display:none'");
                
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
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.TotalGrossPriceTag, $"{String.Format("{0:0.00}", studentVM.TotalGrossPrice)}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.RegFee, $"{String.Format("{0:0.00}", studentVM.RegistrationFee)}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.TotalAddinsTag, totalAddins);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.CommissionAddinsTag, commissionAddins);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.CommisionTag, $"{String.Format("{0:0.00}", studentVM.Commision)}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.PaidTag, $"{String.Format("{0:0.00}", studentVM.Paid)}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.BalanceTag, $"{String.Format("{0:0.00}", studentVM.Balance)}");
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.Reg_RefTag, studentVM.Reg_Ref);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.AdditionalServices_Tag, addinsAdd);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.IncludedServicesTag, addinsInc);
            template.AgentInvoiceTemplate = template.AgentInvoiceTemplate.Replace(EmailSender.PassportNumberTag, passportNumber);

            
        }


        private void StudentInvoiceTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {
            string addinsInc = "";
            string addinsAdd = "";
            string totalAddins = "";

            if(studentVM.TotalAddins > 0)
            {
                totalAddins = $"<tr><td> Additional Services </td><td class=\"text-right\">${String.Format("{0:0.00}", studentVM.TotalAddins)}</td></tr>";
            }
            studentVM.Commision = ((studentVM.Commision * studentVM.TotalGrossPrice) / 100);
            var calculatedCommission = (studentVM.TotalGrossPrice + studentVM.TotalAddins) - studentVM.Commision;
            studentVM.NetPrice = calculatedCommission;
            if (studentVM.TotalPayment >= studentVM.NetPrice)
            {
                studentVM.Paid += studentVM.Commision;
            }
            studentVM.Balance = (studentVM.TotalGrossPrice + studentVM.TotalAddins + studentVM.RegistrationFee - studentVM.Paid);
            if (studentVM.Balance < 0)
            {
                studentVM.Balance = 0;
            }
            studentVM.Balance = Math.Round(studentVM.Balance);
            studentVM.Paid = Math.Round(studentVM.Paid);
            if (studentVM.StudentPDFAddinAdd.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinAdd)
                {
                    if (count > 1)
                    {
                        addinsAdd = $"{addinsAdd} <br> {addin}";
                    }
                    else
                    {
                        addinsAdd = $"{addin}";
                    }

                    count++;
                }
            }
            if (studentVM.StudentPDFAddinInc.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinInc)
                {
                    if (count > 1)
                    {
                        addinsInc = $"{addinsInc} <br> {addin}";
                    }
                    else
                    {
                        addinsInc = $"{addin}";
                    }

                    count++;
                }
            }

            string passportNumber = "";
            if (!string.IsNullOrEmpty(studentVM.PassportNumber))
            {
                passportNumber = $"<tr><td style = \"width: 15%;font-size: 11px;\"> Passport Number:</td><td class=\"data\" style=\"width:20%;font-size: 12px;\">{studentVM.PassportNumber}</td><td class=\"text-right\" style=\"width: 30%; vertical-align: text-top ;\"></td><td style = \"width: 35%;\" ></td></tr>";
            }
            if (studentVM.RegistrationFee == 0)
            {
                template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.RegStyle, "style='display:none'");

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
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.RegFee, $"{String.Format("{0:0.00}", studentVM.RegistrationFee)}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.TotalGrossPriceTag, $"{String.Format("{0:0.00}", studentVM.TotalGrossPrice)}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.TotalAddinsTag, totalAddins);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.PaidTag, $"{String.Format("{0:0.00}", studentVM.Paid)}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.BalanceTag, $"{String.Format("{0:0.00}", studentVM.Balance)}");
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.AdditionalServices_Tag, addinsAdd);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.IncludedServicesTag, addinsInc);
            template.StudentInvoiceTemplate = template.StudentInvoiceTemplate.Replace(EmailSender.PassportNumberTag, passportNumber);



        }

        private void AirportInvoiceTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {
            if(studentVM.ArrivalDate == null)
            {
                studentVM.ArrivalDate = studentVM.ProgrameStartDate;
            }
            string passportNumber = "";
            if (!string.IsNullOrEmpty(studentVM.PassportNumber))
            {
                passportNumber = $"<tr><td style = \"width: 15%;font-size: 11px;\"> Passport Number:</td><td class=\"data\" style=\"width:20%;font-size: 12px;\">{studentVM.PassportNumber}</td><td class=\"text-right\" style=\"width: 30%; vertical-align: text-top ;\"></td><td style = \"width: 35%;\" ></td></tr>";
            }
            template.AirportInvoiceTemplate = template.AirportInvoiceTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentVM.FirstName} {studentVM.LastName}");
            template.AirportInvoiceTemplate = template.AirportInvoiceTemplate.Replace(EmailSender.ArrivalFlightNumberTag, studentVM.FlightNumber);
            template.AirportInvoiceTemplate = template.AirportInvoiceTemplate.Replace(EmailSender.ArrivalDateTag, $"{studentVM.ArrivalDate?.Date.ToString("MM/dd/yyyy")}");
            template.AirportInvoiceTemplate = template.AirportInvoiceTemplate.Replace(EmailSender.ArrivalTimeTag, $"{studentVM.ArrivalTime?.ToString("hh:mm:ss tt")}");
            template.AirportInvoiceTemplate = template.AirportInvoiceTemplate.Replace(EmailSender.PassportNumberTag, passportNumber);
            template.AirportInvoiceTemplate = template.AirportInvoiceTemplate.Replace(EmailSender.AddressTag, studentVM.Address);
        }

        private void LOAInvoiceTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {
            string addinsInc = "";
            string addinsAdd = "";
            string totalAddins = "";

            if (studentVM.TotalAddins > 0)
            {
                totalAddins = $"<tr><td> Additional Services </td><td class=\"text-right\">${String.Format("{0:0.00}", studentVM.TotalAddins)}</td></tr>";
            }
            studentVM.Commision = ((studentVM.Commision * studentVM.TotalGrossPrice) / 100);

            var calculatedCommission = (studentVM.TotalGrossPrice + studentVM.TotalAddins) - studentVM.Commision;
            studentVM.NetPrice = calculatedCommission;
            if (studentVM.TotalPayment >= studentVM.NetPrice)
            {
                studentVM.Paid += studentVM.Commision;
            }
            studentVM.Balance = (studentVM.TotalGrossPrice + studentVM.RegistrationFee - studentVM.Paid);
            if (studentVM.Balance < 0)
            {
                studentVM.Balance = 0;
            }
            studentVM.Balance = Math.Round(studentVM.Balance);
            studentVM.Paid = Math.Round(studentVM.Paid);
            if (studentVM.StudentPDFAddinAdd.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinAdd)
                {
                    if (count > 1)
                    {
                        addinsAdd = $"{addinsAdd} <br> {addin}";
                    }
                    else
                    {
                        addinsAdd = $"{addin}";
                    }

                    count++;
                }
            }
            if (studentVM.StudentPDFAddinInc.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinInc)
                {
                    if (count > 1)
                    {
                        addinsInc = $"{addinsInc} <br> {addin}";
                    }
                    else
                    {
                        addinsInc = $"{addin}";
                    }

                    count++;
                }
            }

            string passportNumber = "";
            if (!string.IsNullOrEmpty(studentVM.PassportNumber))
            {
                passportNumber = $"<tr><td style = \"width: 15%;font-size: 11px;\"> Passport Number:</td><td class=\"data\" style=\"width:20%;font-size: 12px;\">{studentVM.PassportNumber}</td><td class=\"text-right\" style=\"width: 30%; vertical-align: text-top ;\"></td><td style = \"width: 35%;\" ></td></tr>";
            }

            if (studentVM.RegistrationFee == 0)
            {
                template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.RegStyle, "style='display:none'");

            }
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.CurrentDateTag, DateTime.Now.ToString("MMMM dd, yyyy"));
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentVM.FirstName} {studentVM.LastName}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.CountryTag, $"{studentVM.Country}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.Reg_RefTag, studentVM.Reg_Ref);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.DOBTag, $"{studentVM.DOB?.Date.ToString("MM/dd/yyyy")}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.ProgrameStartDateTag, $"{studentVM.ProgrameStartDate?.Date.ToString("MM/dd/yyyy")}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.ProgrameEndDateTag, $"{studentVM.ProgrameEndDate?.ToString("MM/dd/yyyy")}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.CampusAddressOnReportsTag, studentVM.CampusAddressOnReports);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.ProgramNameTag, studentVM.ProgramName);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.SubProgramNameTag, studentVM.SubProgramName);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.FormatNameTag, studentVM.FormatName);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.MealPlanTag, studentVM.MealPlan);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.RegFee, $"{String.Format("{0:0.00}", studentVM.RegistrationFee)}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.TotalGrossPriceTag, $"{String.Format("{0:0.00}", studentVM.TotalGrossPrice)}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.TotalAddinsTag, totalAddins);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.PaidTag, $"{String.Format("{0:0.00}", studentVM.Paid)}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.BalanceTag, $"{String.Format("{0:0.00}", studentVM.Balance)}");
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.AdditionalServices_Tag, addinsAdd);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.IncludedServicesTag, addinsInc);
            template.LOAInvoiceTemplate = template.LOAInvoiceTemplate.Replace(EmailSender.PassportNumberTag, passportNumber);



        }

        private void StudentInvitationTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {

            string passportNumber = "";
            if (!string.IsNullOrEmpty(studentVM.PassportNumber))
            {
                passportNumber = $"<tr><td style = \"width: 15%;font-size: 11px;\"> Passport Number:</td><td class=\"data\" style=\"width:20%;font-size: 12px;\">{studentVM.PassportNumber}</td><td class=\"text-right\" style=\"width: 30%; vertical-align: text-top ;\"></td><td style = \"width: 35%;\" ></td></tr>";
            }

            template.StudentInvitationTemplate = template.StudentInvitationTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentVM.FirstName} {studentVM.LastName}");
            template.StudentInvitationTemplate = template.StudentInvitationTemplate.Replace(EmailSender.Reg_RefTag, studentVM.Reg_Ref);
            template.StudentInvitationTemplate = template.StudentInvitationTemplate.Replace(EmailSender.DOBTag, $"{studentVM.DOB?.Date.ToString("MM/dd/yyyy")}");
            template.StudentInvitationTemplate = template.StudentInvitationTemplate.Replace(EmailSender.ProgrameStartDateTag, $"{studentVM.ProgrameStartDate?.Date.ToString("MMMM dd, yyyy")}");
            template.StudentInvitationTemplate = template.StudentInvitationTemplate.Replace(EmailSender.ProgrameEndDateTag, $"{studentVM.ProgrameEndDate?.ToString("MMMM dd, yyyy")}");
            template.StudentInvitationTemplate = template.StudentInvitationTemplate.Replace(EmailSender.CampusAddressOnReportsTag, studentVM.CampusAddressOnReports);
            template.StudentInvitationTemplate = template.StudentInvitationTemplate.Replace(EmailSender.PassportNumberTag, passportNumber);

        }

        private void LOAWOPInvoiceTemplateRendrer(StudentPDFDataVM studentVM, EmailViewModel template)
        {
            string addinsInc = "";
            string addinsAdd = "";

            if (studentVM.StudentPDFAddinAdd.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinAdd)
                {
                    if (count > 1)
                    {
                        addinsAdd = $"{addinsAdd} <br> {addin}";
                    }
                    else
                    {
                        addinsAdd = $"{addin}";
                    }

                    count++;
                }
            }
            if (studentVM.StudentPDFAddinInc.Count > 0)
            {
                int count = 1;
                foreach (var addin in studentVM.StudentPDFAddinInc)
                {
                    if (count > 1)
                    {
                        addinsInc = $"{addinsInc} <br> {addin}";
                    }
                    else
                    {
                        addinsInc = $"{addin}";
                    }

                    count++;
                }
            }

            string passportNumber = "";
            if (!string.IsNullOrEmpty(studentVM.PassportNumber))
            {
                passportNumber = $"<tr><td style = \"width: 15%;font-size: 11px;\"> Passport Number:</td><td class=\"data\" style=\"width:20%;font-size: 12px;\">{studentVM.PassportNumber}</td><td class=\"text-right\" style=\"width: 30%; vertical-align: text-top ;\"></td><td style = \"width: 35%;\" ></td></tr>";
            }


            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.CurrentDateTag, DateTime.Now.ToString("MMMM dd, yyyy"));
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.StudentFullNameTag, $"{studentVM.FirstName} {studentVM.LastName}");
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.CountryTag, $"{studentVM.Country}");
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.Reg_RefTag, studentVM.Reg_Ref);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.DOBTag, $"{studentVM.DOB?.Date.ToString("MM/dd/yyyy")}");
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.ProgrameStartDateTag, $"{studentVM.ProgrameStartDate?.Date.ToString("MM/dd/yyyy")}");
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.ProgrameEndDateTag, $"{studentVM.ProgrameEndDate?.ToString("MM/dd/yyyy")}");
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.CampusAddressOnReportsTag, studentVM.CampusAddressOnReports);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.ProgramNameTag, studentVM.ProgramName);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.SubProgramNameTag, studentVM.SubProgramName);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.FormatNameTag, studentVM.FormatName);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.MealPlanTag, studentVM.MealPlan);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.AdditionalServices_Tag, addinsAdd);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.IncludedServicesTag, addinsInc);
            template.LOAInvoiceWOPTemplate = template.LOAInvoiceWOPTemplate.Replace(EmailSender.PassportNumberTag, passportNumber);

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
            ContentType ct = new ContentType();
            ct.Name = filename;
            ct.MediaType = MediaTypeNames.Application.Pdf;
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(pdfStream, ct);
            template.emailAttachment.Add(att);

        }
        public async Task<MemoryStream> DocumentGet(EmailSendVM emailSendVM)
        {
            MemoryStream memoryStream = new MemoryStream();
            StudentPDFDataVM studentFilesDataAsync = await this._ELIService.GetStudentFilesDataAsync(emailSendVM.StudentID);
            studentFilesDataAsync.RegistrationFee = emailSendVM.RegistrationFee;
            studentFilesDataAsync.Address = emailSendVM.Address;
            studentFilesDataAsync.EmailBody = emailSendVM.EmailBody;
            if (studentFilesDataAsync != null)
            {
                EmailViewModel emailViewModel = new EmailViewModel()
                {
                    Subject = "",
                    Message = ""
                };
                emailViewModel.Message = "";
                emailViewModel.To = "";
                memoryStream = this.GetPdf(emailSendVM, emailViewModel, studentFilesDataAsync);
            }
            return memoryStream;
        }
        public async Task<MemoryStream> GetCertificate(string studentName)
        {
                MemoryStream memoryStream = new MemoryStream();
                var StudentCertificateTemplate = this.StudentCertificateHTML;
                StudentCertificateTemplate = StudentCertificateTemplate.Replace("{{StudentFullName}}", studentName);
                memoryStream = this.PDFCreatorForClient(null, string.Concat("Eli Student Certificate-", studentName, ".pdf"), StudentCertificateTemplate, true);
                return memoryStream;
        }
        private MemoryStream GetPdf(EmailSendVM emailSendVM, EmailViewModel email, StudentPDFDataVM studentPDFDataVM)
        {
            MemoryStream memoryStream = new MemoryStream();
            if (emailSendVM.IsAgentInvoice)
            {
                email.AgentInvoiceTemplate = this.AgentInvoiceHTML;
                this.AgentInvoiceTemplateRendrer(studentPDFDataVM, email);
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli Agent Invoice-", studentPDFDataVM.Reg_Ref, ".pdf"), email.AgentInvoiceTemplate, false);
            }
            else if (emailSendVM.IsStudentCertificate)
            {
                email.StudentCertificateTemplate = this.StudentCertificateHTML;
                email.StudentCertificateTemplate = email.StudentCertificateTemplate.Replace("{{StudentFullName}}", string.Concat(studentPDFDataVM.FirstName, " ", studentPDFDataVM.LastName));
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli Student Certificate-", studentPDFDataVM.Reg_Ref, ".pdf"), email.StudentCertificateTemplate, true);
            }
            else if (emailSendVM.IsStudentInvoice)
            {
                email.StudentInvoiceTemplate = this.StudentInvoiceHTML;
                this.StudentInvoiceTemplateRendrer(studentPDFDataVM, email);
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli Student Invoice-", studentPDFDataVM.Reg_Ref, ".pdf"), email.StudentInvoiceTemplate, false);
            }
            else if (emailSendVM.IsAirportInvoice)
            {
                email.AirportInvoiceTemplate = this.AirportInvoiceHTML;
                this.AirportInvoiceTemplateRendrer(studentPDFDataVM, email);
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli Student Airport Doc-", studentPDFDataVM.Reg_Ref, ".pdf"), email.AirportInvoiceTemplate, true);
            }
            else if (emailSendVM.IsLoaInvoice)
            {
                email.LOAInvoiceTemplate = this.LOAInvoiceHTML;
                this.LOAInvoiceTemplateRendrer(studentPDFDataVM, email);
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli Student LOA With Price-", studentPDFDataVM.Reg_Ref, ".pdf"), email.LOAInvoiceTemplate, false);
            }
            else if (emailSendVM.IsLoaInvoiceWithNoPrice)
            {
                email.LOAInvoiceWOPTemplate = this.LOAInvoiceWOPHTML;
                this.LOAWOPInvoiceTemplateRendrer(studentPDFDataVM, email);
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli Student LOA No Price-", studentPDFDataVM.Reg_Ref, ".pdf"), email.LOAInvoiceWOPTemplate, false);
            }
            else if (emailSendVM.IsLoaGroupInvoice)
            {
                email.LOAInvoiceTemplate = this.LOAGroupInvoiceHTML;
                this.LOAInvoiceTemplateRendrer(studentPDFDataVM, email);
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli LOA - Group-", studentPDFDataVM.Reg_Ref, ".pdf"), email.LOAInvoiceTemplate, false);
            }
            else if (emailSendVM.IsStudentInvitation)
            {
                email.StudentInvitationTemplate = this.StudentInvitationHTML;
                this.StudentInvitationTemplateRendrer(studentPDFDataVM, email);
                memoryStream = this.PDFCreatorForClient(email, string.Concat("Eli Student Invitation-", studentPDFDataVM.Reg_Ref, ".pdf"), email.StudentInvitationTemplate, false);
            }
            return memoryStream;
        }
        private MemoryStream PDFCreatorForClient(EmailViewModel template, string filename, string htmlContent, bool isLandscape = false)
        {
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.MarginLeft = 10;
            htmlToPdf.Options.MarginRight = 10;
            htmlToPdf.Options.MarginTop = 15;
            htmlToPdf.Options.MarginBottom = 15;
            if (isLandscape)
            {
                htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            }
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(htmlContent);
            MemoryStream memoryStream = new MemoryStream();
            pdfDocument.Save(memoryStream);
            memoryStream.Position = ((long)0);
            return memoryStream;
        }
    }
}
