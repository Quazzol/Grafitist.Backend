using AutoMapper;
using Grafitist.Contracts.Payment.Request;
using Grafitist.Contracts.Payment.Response;
using Grafitist.Models.Payment;

namespace Grafitist.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        // Source -> Target
        CreateMap<PaymentModel, PaymentDTO>();
        CreateMap<PaymentInsertDTO, PaymentModel>();
    }
}