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
            CreateMap<ProdutoDTO, Produto>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.FornecedorId, opt => opt.MapFrom(src => src.FornecedorId));

            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>();

            CreateMap<Fornecedor, FornecedorDTO>();
            CreateMap<FornecedorDTO, Fornecedor>();

            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>();
        }
    }
}
