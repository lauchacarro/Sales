
using AutoMapper;

using Sales.Application.Dtos.Invoices;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Application.MapperProfiles.Invoices
{
    public class InvoiceDtoProfile : Profile
    {
        public InvoiceDtoProfile()
        {
            CreateMap<InvoiceDto, Invoice>()
                     .ForMember(u => u.ExpirationDate, options => options.MapFrom(input => input.ExpirationDate))
                     .ForMember(u => u.OrderId, options => options.MapFrom(input => input.OrderId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => new InvoiceStatus(input.Status.ParseToEnum<InvoiceStatus.InvoiceStatusValue>())))
                     .ForMember(u => u.Type, options => options.MapFrom(input => new InvoiceType(input.Type.ParseToEnum<InvoiceType.InvoiceTypeValue>())));

            CreateMap<Invoice, InvoiceDto>()
                     .ForMember(u => u.ExpirationDate, options => options.MapFrom(input => input.ExpirationDate))
                     .ForMember(u => u.OrderId, options => options.MapFrom(input => input.OrderId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()))
                     .ForMember(u => u.Type, options => options.MapFrom(input => input.Type.Type.ToString()));
        }
    }
}
