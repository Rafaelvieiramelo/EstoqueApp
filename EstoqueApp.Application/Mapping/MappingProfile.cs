using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Domain;

namespace LidyDecorApp.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuarios, UsuariosBaseDTO>()
                .ReverseMap();

            CreateMap<Usuarios, UsuarioWriteDTO>()
                .ReverseMap();

            CreateMap<Usuarios, UsuarioReadDTO>()
               .ReverseMap();

            CreateMap<Clientes, ClientesDTO>()
                .ReverseMap();

            CreateMap<Produtos, ProdutosDTO>()
                .ReverseMap();

            // Orcamentos -> OrcamentosDTO
            CreateMap<Orcamentos, OrcamentosDTO>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.DataEvento, opt => opt.MapFrom(src => src.DataEvento))
                .ForMember(dest => dest.ProdutosOrcamentos, opt => opt.MapFrom(src => src.ProdutosOrcamento))
                .PreserveReferences() // Adicione esta linha
                .ReverseMap();

            // ProdutossOrcamentos -> ProdutossOrcamentosDTO
            CreateMap<ProdutosOrcamento, ProdutosOrcamentosDTO>()
                .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos))
                .ForMember(dest => dest.Orcamentos, opt => opt.MapFrom(src => src.Orcamentos))
                .PreserveReferences() // Adicione esta linha
                .ReverseMap();

            CreateMap<TipoEventos, TipoEventoDTO>()
                .ReverseMap(); ;
        }
    }
}
