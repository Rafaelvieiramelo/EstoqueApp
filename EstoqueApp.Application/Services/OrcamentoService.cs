using AutoMapper;
using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Exceptions;
using EstoqueApp.Application.Interfaces;
using EstoqueApp.Domain;
using EstoqueApp.Domain.Interfaces;

namespace EstoqueApp.Application.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IMapper _mapper;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository, IMapper mapper)
        {
            _orcamentoRepository = orcamentoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoEventoDTO>> GetTiposEventoAsync()
        {
            var tiposEvento = await _orcamentoRepository.GetTiposEventoAsync();
            var tiposEventoDto = _mapper.Map<IEnumerable<TipoEventoDTO>>(tiposEvento);

            return tiposEventoDto;
        }

        public async Task<IEnumerable<OrcamentoDTO>> GetOrcamentosAsync()
        {
            var Orcamentos = await _orcamentoRepository.GetOrcamentosAsync();
            var OrcamentosDto = _mapper.Map<IEnumerable<OrcamentoDTO>>(Orcamentos);

            return OrcamentosDto;
        }

        public async Task<OrcamentoDTO> GetOrcamentoByIdAsync(int id)
        {
            var orcamento = await _orcamentoRepository.GetOrcamentoByIdAsync(id);
            return _mapper.Map<OrcamentoDTO>(orcamento);
        }

        public async Task<OrcamentoDTO> AddOrcamentoAsync(OrcamentoDTO orcamentoDTO)
        {
            try
            {
                var orcamento = _mapper.Map<Orcamento>(orcamentoDTO);
                await _orcamentoRepository.AddOrcamentoAsync(orcamento);

                return _mapper.Map<OrcamentoDTO>(orcamento);
            }
            catch (Exception)
            {
                return new OrcamentoDTO();
            }
        }

        public async Task<OrcamentoDTO> UpdateOrcamentoAsync(OrcamentoDTO orcamento)
        {
            try
            {
                await _orcamentoRepository.UpdateOrcamentoAsync(_mapper.Map<Orcamento>(orcamento));
                return orcamento;
            }
            catch (Exception)
            {
                return new OrcamentoDTO();
            }            
        }

        public async Task DeleteOrcamentoAsync(int id)
        {
            try
            {
                await _orcamentoRepository.DeleteOrcamentoAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
