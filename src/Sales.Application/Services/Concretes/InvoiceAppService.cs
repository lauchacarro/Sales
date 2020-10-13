using System;

using Abp.Application.Services;

using AutoMapper;

using Sales.Application.Dtos.Invoices;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.Repositories;

namespace Sales.Application.Services.Concretes
{
    public class InvoiceAppService : ApplicationService, IInvoiceAppService
    {
        private readonly IInvoicePaymentProviderRepository _invoicePaymentProviderRepository;
        private readonly IMapper _mapper;

        public InvoiceAppService(IInvoicePaymentProviderRepository invoicePaymentProviderRepository, IMapper mapper)
        {
            _invoicePaymentProviderRepository = invoicePaymentProviderRepository ?? throw new ArgumentNullException(nameof(invoicePaymentProviderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public InvoicePaymentProviderDto GetInvoicePaymentProviderByOrder(Guid orderId)
        {
            InvoicePaymentProvider invoicePaymentProveider = _invoicePaymentProviderRepository.GetByOrder(orderId);
            return _mapper.Map<InvoicePaymentProviderDto>(invoicePaymentProveider);
        }
    }
}
