
using AutoMapper;

using Sales.Application.Dtos.Invoices;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Invoices;

namespace Sales.Application.MapperProfiles.Invoices
{
    public class InvoicePaymentProviderDtoProfile : Profile
    {
        public InvoicePaymentProviderDtoProfile()
        {
            CreateMap<InvoicePaymentProviderDto, InvoicePaymentProvider>()
                    .ForMember(u => u.InvoceId, options => options.MapFrom(input => input.InvoceId))
                    .ForMember(u => u.Link, options => options.MapFrom(input => input.Link))
                    .ForMember(u => u.Transaction, options => options.MapFrom(input => input.Transaction))
                    .ForMember(u => u.PaymentProvider, options => options.MapFrom(input => new PaymentProvider(input.PaymentProvider.ParseToEnum<PaymentProvider.PaymentProviderValue>())))
                    .ForMember(u => u.Id, options => options.MapFrom(input => input.Id));


            CreateMap<InvoicePaymentProvider, InvoicePaymentProviderDto>()
                     .ForMember(u => u.InvoceId, options => options.MapFrom(input => input.InvoceId))
                    .ForMember(u => u.Link, options => options.MapFrom(input => input.Link))
                    .ForMember(u => u.Transaction, options => options.MapFrom(input => input.Transaction))
                    .ForMember(u => u.PaymentProvider, options => options.MapFrom(input => input.PaymentProvider.Provider.ToString()))
                    .ForMember(u => u.Id, options => options.MapFrom(input => input.Id));
        }
    }
}
