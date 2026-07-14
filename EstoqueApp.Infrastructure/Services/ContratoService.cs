using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;
using MiniSoftware;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LidyDecorApp.Infrastructure.Services
{
    public class ContratoService : IContratoService
    {
        private readonly LidyDecorDbContext _context;

        public ContratoService(LidyDecorDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GerarContratoAsync(int orcamentoId)
        {
            // 1. Busca os dados do orçamento com Clientes, TipoEvento e Produtos
            var orcamento = await _context.Orcamentos
                .Include(o => o.Clientes)
                .Include(o => o.TipoEvento)
                .Include(o => o.ProdutosOrcamento)
                    .ThenInclude(po => po.Produtos)
                .FirstOrDefaultAsync(o => o.Id == orcamentoId);

            if (orcamento == null)
            {
                throw new KeyNotFoundException($"Orçamento com ID {orcamentoId} não encontrado.");
            }

            // 2. Lê o arquivo de modelo (.docx) embutido no Assembly
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LidyDecorApp.Infrastructure.Resource.Templates.contrato_modelo.docx";

            byte[] templateBytes;
            using (Stream templateStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (templateStream == null)
                {
                    throw new FileNotFoundException($"Recurso embutido '{resourceName}' não encontrado no Assembly.");
                }

                using (var ms = new MemoryStream())
                {
                    await templateStream.CopyToAsync(ms);
                    templateBytes = ms.ToArray();
                }
            }

            // 3. Monta a lista de produtos formatada para o template
            var listaProdutos = orcamento.ProdutosOrcamento
                .Select(po => new { NomeProduto = po.Produtos.Nome })
                .ToList();

            // 4. Calcula os valores sinal e restante
            decimal valorSinal = orcamento.ValorSinal;
            decimal valorRestante = orcamento.ValorTotal - valorSinal;
            string porcentagemSinalStr = orcamento.PorcentagemSinal > 0 
                ? $"{orcamento.PorcentagemSinal:F0}" 
                : "";

            // 5. Preenche o dicionário com as variáveis correspondentes às tags do Word
            var dadosContrato = new Dictionary<string, object>
            {
                { "CONTRATANTE_NOME", orcamento.Clientes?.Nome ?? "" },
                { "CONTRATANTE_CPF", orcamento.Clientes?.CpfCnpj ?? "" },
                { "DATA_CONTRATO", orcamento.Data.ToString("dd/MM/yyyy") },
                { "DATA_EVENTO", orcamento.DataEvento?.ToString("dd/MM/yyyy") ?? "" },
                { "VALOR_TOTAL", orcamento.ValorTotal.ToString("C") },
                { "TIPO_SERVICO", orcamento.TipoEvento?.Tipo ?? "" },
                { "CIDADE_CONTRATO", orcamento.CidadeContrato ?? "Campinas" },
                { "ENDERECO_ENTREGA", orcamento.EnderecoEntrega ?? "" },
                { "FORMA_PAGAMENTO", orcamento.FormaPagamento ?? "" },
                { "PORCENTAGEM_SINAL", porcentagemSinalStr },
                { "TEMA_E_PACOTE", orcamento.TemaPacote ?? "" },
                { "VALOR_SINAL", valorSinal.ToString("C") },
                { "VALOR_RESTANTE", valorRestante.ToString("C") }
            };

            // 6. Gera o arquivo preenchido em memória
            using (var outputStream = new MemoryStream())
            {
                MiniWord.SaveAsByTemplate(outputStream, templateBytes, dadosContrato);
                return outputStream.ToArray();
            }
        }
    }
}
