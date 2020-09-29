
using AutoMapper;

using Sales.Application.Dtos.Invoices;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Application.MapperProfiles.Invoices
{
    public class InvoiceWebhookDtoProfile : Profile
    {
        public InvoiceWebhookDtoProfile()
        {
            CreateMap<InvoiceWebhookDto, InvoiceWebhook>()
                     .ForMember(u => u.InvocePaymentProviderId, options => options.MapFrom(input => input.InvocePaymentProviderId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.CreationTime, options => options.MapFrom(input => input.CreationTime))
                     .ForMember(u => u.Status, options => options.MapFrom(input => new InvoiceWebhookStatus(input.Status.ParseToEnum<InvoiceWebhookStatus.InvoiceWebhookStatusValue>())));

            CreateMap<InvoiceWebhook, InvoiceWebhookDto>()
                     .ForMember(u => u.InvocePaymentProviderId, options => options.MapFrom(input => input.InvocePaymentProviderId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.CreationTime, options => options.MapFrom(input => input.CreationTime))
                     .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status));
        }
    }
}
