using AutoMapper;
using ELI.Domain.ViewModels;
using ELI.Entity.Auth;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELI.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserViewModel,Users>();
            CreateMap<ShowViewModel,Show>();
            CreateMap<ShowViewModel, Pricing>();
            CreateMap<ShowViewModel, ShowPricing>();
            CreateMap<ActiveShowViewModel, Sduactivation>();
            CreateMap<ActiveShowViewModel, Device>();
            CreateMap<CreateQualifierVIewModel, Qualifier>();
            CreateMap<Show,ShowViewModel>();
            CreateMap<CreateDiscountViewModel, Discount>();
            CreateMap<CreateQuestionViewModel, Question>();
            CreateMap<CreateQuestionWebViewModel, Question>();
            CreateMap<UpdateQualifierViewModel, Qualifier>();
            CreateMap<CreateQualifierWebViewModel, Qualifier>();
            CreateMap<CreateInvoiceViewModel, Invoice>();
            CreateMap<BulkActivationViewModel, UserViewModel>();
            CreateMap<Leads, GetLeadsInfoViewModel>()
                .ForMember(d => d.LastName, s => s.MapFrom(l => l.SurName))
                .ForMember(d => d.FirstName, s => s.MapFrom(l => l.FirstName))
                .ForMember(d => d.Company, s => s.MapFrom(l => l.Company))
                .ForMember(d => d.Designation, s => s.MapFrom(l => l.Designation))
                .ForMember(d => d.Address, s => s.MapFrom(l => l.Address))
                .ForMember(d => d.Country, s => s.MapFrom(l => l.Country))
                .ForMember(d => d.CountryCode, s => s.MapFrom(l => l.CountryCode))
                .ForMember(d => d.Phone, s => s.MapFrom(l => l.Phone))
                .ForMember(d => d.Email, s => s.MapFrom(l => l.Email))
                .ForMember(d => d.Address2, s => s.MapFrom(l => l.Address2))
                .ForMember(d => d.State, s => s.MapFrom(l => l.State))
                .ForMember(d => d.Suburb, s => s.MapFrom(l => l.Suburb))
                .ForMember(d => d.Landline, s => s.MapFrom(l => l.Landline));

            //    public string DeviceIdentifier { get; set; }
            //public string ShowId { get; set; }
            //public string QualifierId { get; set; }
            //public string Barcode { get; set; }
            //sCreateMap<object, SaveLeadViewModel>()
            //    ;
            //CreateMap<object, QualifierDetailViewModel>();
        }
    }
}
