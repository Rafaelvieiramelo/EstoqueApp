using System.Threading.Tasks;

namespace LidyDecorApp.Application.Interfaces
{
    public interface IContratoService
    {
        Task<byte[]> GerarContratoAsync(int orcamentoId);
    }
}
