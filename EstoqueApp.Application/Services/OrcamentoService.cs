using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Shared.Extensions;

namespace LidyDecorApp.Application.Services
{
    public class OrcamentosService(IOrcamentosRepository orcamentosRepository, IMapper mapper) : IOrcamentosService
    {
        private readonly IOrcamentosRepository _orcamentosRepository = orcamentosRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TipoEventoDTO>> GetTiposEventoAsync()
        {
            var tiposEvento = await _orcamentosRepository.GetTiposEventoAsync();
            var tiposEventoDto = _mapper.Map<IEnumerable<TipoEventoDTO>>(tiposEvento);

            return tiposEventoDto;
        }

        public async Task<IEnumerable<OrcamentosDTO>> GetOrcamentosAsync()
        {
            var Orcamentos = await _orcamentosRepository.GetOrcamentossAsync();
            var OrcamentossDto = _mapper.Map<IEnumerable<OrcamentosDTO>>(Orcamentos);

            return OrcamentossDto;
        }

        public async Task<string> GetNumeroUltimoOrcamentosAsync()
        {
            var numeroUltimoOrcamentos = await _orcamentosRepository.GetNumeroUltimoOrcamentosAsync();

            return string.IsNullOrWhiteSpace(numeroUltimoOrcamentos) ? "0" : numeroUltimoOrcamentos;
        }


        public async Task<OrcamentosDTO> GetOrcamentosByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id não pode ser nulo ou zero");

            try
            {
                var orcamentos = await _orcamentosRepository.GetOrcamentosByIdAsync(id);
                return _mapper.Map<OrcamentosDTO>(orcamentos);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new InvalidOperationException("Erro ao mapear o orçamento para DTO.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao buscar o orçamento.", ex);
            }
        }

        public async Task<OrcamentosDTO> AddOrcamentosAsync(OrcamentosDTO orcamentosDTO)
        {
            var orcamentos = _mapper.Map<Orcamentos>(orcamentosDTO);
            await _orcamentosRepository.AddOrcamentosAsync(orcamentos);

            return _mapper.Map<OrcamentosDTO>(orcamentos);
        }

        public async Task<OrcamentosDTO> UpdateOrcamentosAsync(OrcamentosDTO orcamentos)
        {
            try
            {
                await _orcamentosRepository.UpdateOrcamentosAsync(_mapper.Map<Orcamentos>(orcamentos));
                return orcamentos;
            }
            catch (Exception)
            {
                return new OrcamentosDTO();
            }
        }

        public async Task DeleteOrcamentosAsync(int id)
        {
            try
            {
                await _orcamentosRepository.DeleteOrcamentosAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
