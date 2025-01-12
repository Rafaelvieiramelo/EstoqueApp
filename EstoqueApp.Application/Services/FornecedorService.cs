using AutoMapper;
using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Exceptions;
using EstoqueApp.Application.Interfaces;
using EstoqueApp.Domain;
using EstoqueApp.Domain.Interfaces;

namespace EstoqueApp.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FornecedorDTO>> GetFornecedoresAsync()
        {
            var fornecedores = await _fornecedorRepository.GetFornecedoresAsync();
            var fornecedoresDto = _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores);

            return fornecedoresDto;
        }

        public async Task<FornecedorDTO> GetFornecedorByIdAsync(int id)
        {
            var fornecedor = await _fornecedorRepository.GetFornecedorByIdAsync(id);
            return _mapper.Map<FornecedorDTO>(fornecedor);
        }

        public async Task<FornecedorDTO> AddFornecedorAsync(FornecedorDTO FornecedorDTO)
        {
            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(FornecedorDTO);
                await _fornecedorRepository.AddFornecedorAsync(fornecedor);

                return _mapper.Map<FornecedorDTO>(fornecedor);
            }
            catch (Exception)
            {
                return new FornecedorDTO();
            }
        }

        public async Task<FornecedorDTO> UpdateFornecedorAsync(FornecedorDTO fornecedor)
        {
            try
            {
                await _fornecedorRepository.UpdateFornecedorAsync(_mapper.Map<Fornecedor>(fornecedor));
                return fornecedor;
            }
            catch (Exception)
            {
                return new FornecedorDTO();
            }            
        }

        public async Task DeleteFornecedorAsync(int id)
        {
            try
            {
                await _fornecedorRepository.DeleteFornecedorAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteProdutoException(ex.Message);
            }
        }
    }
}
