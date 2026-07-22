using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;

namespace LidyDecorApp.Application.Services
{
    public class ServicoService(IServicoRepository servicoRepository, IMapper mapper) : IServicoService
    {
        private readonly IServicoRepository _servicoRepository = servicoRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ServicoDTO>> GetServicosAsync()
        {
            var servicos = await _servicoRepository.GetServicosAsync();
            return _mapper.Map<IEnumerable<ServicoDTO>>(servicos);
        }

        public async Task<ServicoDTO?> GetServicoByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido.", nameof(id));

            var servico = await _servicoRepository.GetServicoByIdAsync(id);
            return servico != null ? _mapper.Map<ServicoDTO>(servico) : null;
        }

        public async Task<ServicoDTO> AddServicoAsync(ServicoDTO dto)
        {
            var servico = _mapper.Map<Servico>(dto);
            await _servicoRepository.AddServicoAsync(servico);
            return _mapper.Map<ServicoDTO>(servico);
        }

        public async Task<ServicoDTO> UpdateServicoAsync(ServicoDTO dto)
        {
            var servico = _mapper.Map<Servico>(dto);
            await _servicoRepository.UpdateServicoAsync(servico);
            return dto;
        }

        public async Task DeleteServicoAsync(int id)
        {
            try
            {
                await _servicoRepository.DeleteServicoAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
