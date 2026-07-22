using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;
using MiniSoftware;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
            // 1. Busca os dados do orçamento com Clientes, TipoEvento, Servico e Produtos
            var orcamento = await _context.Orcamentos
                .Include(o => o.Clientes)
                .Include(o => o.TipoEvento)
                .Include(o => o.Servico)
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

            string tipoServicoText = "";
            if (orcamento.Servico != null)
            {
                tipoServicoText = string.IsNullOrWhiteSpace(orcamento.Servico.Inclusao)
                    ? orcamento.Servico.Nome
                    : $"{orcamento.Servico.Nome} ({orcamento.Servico.Inclusao})";
            }
            else
            {
                tipoServicoText = orcamento.TipoEvento?.Tipo ?? "";
            }

            // 5. Preenche o dicionário com as variáveis correspondentes às tags do Word
            var dadosContrato = new Dictionary<string, object>
            {
                { "CONTRATANTE_NOME", orcamento.Clientes?.Nome ?? "" },
                { "CONTRATANTE_CPF", orcamento.Clientes?.CpfCnpj ?? "" },
                { "DATA_CONTRATO", orcamento.Data.ToString("dd/MM/yyyy") },
                { "DATA_EVENTO", orcamento.DataEvento?.ToString("dd/MM/yyyy") ?? "" },
                { "VALOR_TOTAL", orcamento.ValorTotal.ToString("C") },
                { "TIPO_SERVICO", tipoServicoText },
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

        public async Task<byte[]> GerarContratoPdfAsync(int orcamentoId)
        {
            // 1. Busca os dados do orçamento
            var orcamento = await _context.Orcamentos
                .Include(o => o.Clientes)
                .Include(o => o.TipoEvento)
                .Include(o => o.Servico)
                .Include(o => o.ProdutosOrcamento)
                    .ThenInclude(po => po.Produtos)
                .FirstOrDefaultAsync(o => o.Id == orcamentoId);

            if (orcamento == null)
            {
                throw new KeyNotFoundException($"Orçamento com ID {orcamentoId} não encontrado.");
            }

            // 2. Extrai imagens do modelo DOCX
            var (logoBytes, watermarkBytes) = ExtrairImagensDoTemplate();

            // 3. Define variáveis para o preenchimento do contrato
            string nomeContratante = orcamento.Clientes?.Nome ?? "";
            string cpfContratante = orcamento.Clientes?.CpfCnpj ?? "";
            
            string tipoServico = "";
            if (orcamento.Servico != null)
            {
                tipoServico = string.IsNullOrWhiteSpace(orcamento.Servico.Inclusao)
                    ? orcamento.Servico.Nome
                    : $"{orcamento.Servico.Nome} ({orcamento.Servico.Inclusao})";
            }
            else
            {
                tipoServico = orcamento.TipoEvento?.Tipo ?? "";
            }

            string dataEvento = orcamento.DataEvento?.ToString("dd/MM/yyyy") ?? "";
            string enderecoEntrega = orcamento.EnderecoEntrega ?? "";
            string temaPacote = orcamento.TemaPacote ?? "";
            
            decimal valorSinal = orcamento.ValorSinal;
            decimal valorRestante = orcamento.ValorTotal - valorSinal;
            string porcentagemSinalStr = orcamento.PorcentagemSinal > 0 
                ? $"{orcamento.PorcentagemSinal:F0}" 
                : "0";
            
            string valorTotal = orcamento.ValorTotal.ToString("C");
            string formaPagamento = orcamento.FormaPagamento ?? "";
            string cidadeContrato = orcamento.CidadeContrato ?? "Campinas";
            string dataContrato = orcamento.Data.ToString("dd/MM/yyyy");

            // 4. Configura licença do QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            // 5. Cria o documento PDF com QuestPDF
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10f).FontFamily(Fonts.Arial));

                    // Marca d'água centralizada em plano de fundo (background)
                    if (watermarkBytes != null)
                    {
                        page.Background().AlignMiddle().AlignCenter().MaxWidth(380).Image(watermarkBytes);
                    }

                    // Logo no topo (header)
                    if (logoBytes != null)
                    {
                        page.Header().AlignCenter().Height(85).Image(logoBytes);
                    }

                    // Conteúdo principal
                    page.Content().PaddingTop(15).Column(column =>
                    {
                        column.Spacing(8);

                        // Título do Contrato
                        column.Item().AlignCenter().Text("CONTRATO DE PRESTAÇÃO DE SERVIÇOS DE DECORAÇÃO DE FESTA")
                            .FontSize(12f)
                            .Bold()
                            .Underline();

                        // Qualificação das Partes
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CONTRATANTE: ").Bold();
                            text.Span($"{nomeContratante}, inscrito(a) no CPF sob o nº {cpfContratante}.\n\n");

                            text.Span("CONTRATADA: ").Bold();
                            text.Span("Lidy Decor & Locações, inscrita no CNPJ sob o nº 61.375.825/0001-85, representada por Lidiane Martins Candeira, inscrita no CPF sob o nº 021.663.943-37, com sede em Campinas - SP.\n\n");

                            text.Span("Têm entre si, justo e contratado o que segue:");
                        });

                        // CLÁUSULA 1
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CLÁUSULA 1 – OBJETO\n").Bold();
                            text.Span($"O presente contrato tem por objeto a prestação de serviços de {tipoServico} (ex: pegue e monte), a ser entregue na data {dataEvento}, no endereço {enderecoEntrega}, conforme tema e pacote contratado: {temaPacote}.");
                        });

                        // CLÁUSULA 2
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CLÁUSULA 2 – VALOR E FORMA DE PAGAMENTO\n").Bold();
                            text.Span($"2.1 O valor total dos serviços é de R$ {valorTotal}, a ser pago da seguinte forma: {formaPagamento}.\n");
                            text.Span($"2.2 A reserva da data do evento será efetivada somente após o pagamento total ou do sinal de {porcentagemSinalStr}% (equivalente a R$ {valorSinal:C}), restando o saldo de R$ {valorRestante:C} a ser pago no ato da entrega.");
                        });

                        // CLÁUSULA 3
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CLÁUSULA 3 – CANCELAMENTO, REMARCAÇÃO E DESISTÊNCIA\n").Bold();
                            text.Span("3.1 Em caso de cancelamento por parte do CONTRATANTE, será permitida a remarcação da festa dentro do prazo de até 6 (seis) meses, respeitando a disponibilidade da agenda da CONTRATADA.\n");
                            text.Span("3.2 Após esse prazo, perder-se-á o direito de remarcação e qualquer valor pago será retido pela CONTRATADA.\n");
                            text.Span("3.3 Em caso de desistência definitiva por parte do CONTRATANTE, será devolvido 50% (cinquenta por cento) do valor pago até o momento.\n");
                            text.Span("3.4 Caso o cancelamento seja por parte da CONTRATADA (salvo em caso fortuito ou força maior), será devolvido 100% dos valores pagos pelo CONTRATANTE.");
                        });

                        // CLÁUSULA 4
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CLÁUSULA 4 – AVARIAS, DANOS E EXTRAVIO\n").Bold();
                            text.Span("4.1 O CONTRATANTE será responsável por qualquer dano, avaria, extravio ou perda de itens de decoração durante o período em que os mesmos estiverem sob sua responsabilidade, inclusive durante a festa.\n");
                            text.Span("4.2 Nesses casos, será cobrado o valor correspondente à reposição ou conserto do item danificado.");
                        });

                        // CLÁUSULA 5
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CLÁUSULA 5 – OBRIGAÇÕES DA CONTRATADA\n").Bold();
                            text.Span("5.1 Comparecer ao local do evento na data e horário combinados, realizando a montagem e desmontagem da decoração conforme acordado.\n");
                            text.Span("5.2 Garantir que todos os itens decorativos estejam em perfeito estado de conservação e limpeza.\n");
                            text.Span("5.3 Em caso de eventos ao ar livre, orientar o CONTRATANTE sobre os riscos em relação a condições climáticas adversas (chuvas, ventos, etc.), não se responsabilizando por danos causados por tais fatores, salvo acordo específico em contrário.");
                        });

                        // CLÁUSULA 6
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CLÁUSULA 6 – OBRIGAÇÕES DO CONTRATANTE\n").Bold();
                            text.Span("6.1 Garantir o acesso ao local da montagem na data e horário combinados, assegurando que o ambiente esteja apto para a instalação da decoração.\n");
                            text.Span("6.2 Manter vigilância e zelo pelos itens decorativos durante o evento.\n");
                            text.Span("6.3 Comunicar a CONTRATADA com o mínimo de 48 horas de antecedência sobre qualquer alteração relevante do evento.");
                        });

                        // CLÁUSULA 7
                        column.Item().Text(text =>
                        {
                            text.Justify();
                            text.Span("CLÁUSULA 7 – DIREITO DE IMAGEM\n").Bold();
                            text.Span("7.1 O CONTRATANTE autoriza a CONTRATADA a fotografar e divulgar imagens da decoração realizada, exclusivamente para fins de portfólio, marketing e divulgação do serviço em redes sociais, site ou material promocional.");
                        });

                        column.Item().PaddingTop(10);

                        // Cidade e Data
                        column.Item().AlignRight().Text($"{cidadeContrato}, {dataContrato}")
                            .FontSize(10f);

                        column.Item().PaddingTop(25);

                        // Assinaturas
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("______________________________________________________");
                                col.Item().AlignCenter().Text("CONTRATANTE").Bold().FontSize(8.5f);
                            });

                            row.ConstantItem(20);

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("______________________________________________________");
                                col.Item().AlignCenter().Text("CONTRATADA: Lidy Decor & Locações").Bold().FontSize(8.5f);
                                col.Item().AlignCenter().Text("Lidiane Martins Candeira").FontSize(8);
                            });
                        });
                    });
                });
            });

            return doc.GeneratePdf();
        }

        private (byte[]? logo, byte[]? watermark) ExtrairImagensDoTemplate()
        {
            byte[]? logoBytes = null;
            byte[]? watermarkBytes = null;

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LidyDecorApp.Infrastructure.Resource.Templates.contrato_modelo.docx";

            using (Stream? templateStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (templateStream == null)
                {
                    return (null, null);
                }

                using (var archive = new ZipArchive(templateStream, ZipArchiveMode.Read))
                {
                    // Extrai a logo
                    var logoEntry = archive.GetEntry("word/media/image1.jpeg");
                    if (logoEntry != null)
                    {
                        using (var s = logoEntry.Open())
                        using (var ms = new MemoryStream())
                        {
                            s.CopyTo(ms);
                            logoBytes = ms.ToArray();
                        }
                    }

                    // Extrai a marca d'água
                    var watermarkEntry = archive.GetEntry("word/media/image3.png");
                    if (watermarkEntry != null)
                    {
                        using (var s = watermarkEntry.Open())
                        using (var ms = new MemoryStream())
                        {
                            s.CopyTo(ms);
                            watermarkBytes = ms.ToArray();
                        }
                    }
                }
            }

            return (logoBytes, watermarkBytes);
        }
    }
}
