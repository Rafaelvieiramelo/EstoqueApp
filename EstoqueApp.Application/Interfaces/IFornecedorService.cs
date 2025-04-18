﻿using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Interfaces
{
    public interface IFornecedorService
    {
        Task<IEnumerable<FornecedorDTO>> GetFornecedoresAsync();
        Task<FornecedorDTO> GetFornecedorByIdAsync(int id);
        Task<FornecedorDTO> AddFornecedorAsync(FornecedorDTO fornecedor);
        Task<FornecedorDTO> UpdateFornecedorAsync(FornecedorDTO fornecedor);
        Task DeleteFornecedorAsync(int id);
    }
}
