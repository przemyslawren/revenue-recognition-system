using AutoMapper;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.models;

namespace RevenueRecognitionSystem.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<IndividualClient, IndividualClientDto>().ReverseMap();
        CreateMap<CompanyClient, CompanyClientDto>().ReverseMap();
    }
}