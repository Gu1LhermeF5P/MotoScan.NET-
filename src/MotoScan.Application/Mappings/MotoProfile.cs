using MotoScan.MotoScan.Application.DTOs;
using MotoScan.MotoScan.Domain.Entities;

namespace MotoScan.Application.Mappings;

public class MotoProfile : Profile
{
    public MotoProfile()
    {
        CreateMap<Moto, MotoDto>()
            .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.Placa.Valor))
            .ForMember(dest => dest.LocalizacaoAtual, opt => opt.MapFrom(src => src.LocalizacaoAtual.Descricao));

        CreateMap<RegistroMovimentacao, RegistroMovimentacaoDto>()
            .ForMember(dest => dest.Localizacao, opt => opt.MapFrom(src => src.Localizacao.Descricao));
    }
}

public class Profile
{
}