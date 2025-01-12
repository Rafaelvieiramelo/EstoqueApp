using AutoMapper;
using EstoqueApp.Application.DTOs;
using EstoqueApp.Domain;

namespace EstoqueApp.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>();
            CreateMap<FornecedorDTO, Fornecedor>();
            CreateMap<Fornecedor, FornecedorDTO>();

            CreateMap<ProdutoDTO, Produto>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.FornecedorId, opt => opt.MapFrom(src => src.FornecedorId));
        }
    }
}
