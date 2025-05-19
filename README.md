
# LidyDecorApp

**LidyDecorApp** é um sistema interno para gestão de locação de artigos de decoração para festas e prestação de serviços relacionados. O sistema foi desenvolvido para uso exclusivo do administrador da empresa, permitindo o gerenciamento completo de produtos, usuários, clientes, orçamentos e contratos.

## ✨ Funcionalidades

- ✅ CRUD de Produtos
- ✅ CRUD de Usuários com gerenciamento de perfis (roles)
- ✅ CRUD de Clientes
- ✅ CRUD de Orçamentos, com possibilidade de gerar contratos automaticamente
- ✅ Geração e impressão de Contratos a partir dos orçamentos
- ✅ Autenticação com JWT e controle de permissões por perfil
- ✅ Swagger com autenticação para testar as APIs

## 🧱 Estrutura do Projeto

O projeto segue os princípios de **Clean Architecture** e está dividido em múltiplos projetos dentro da solution:

- `LidyDecorApp.API` – Backend com APIs RESTful (.NET 8)
- `LidyDecorApp.Application` – Camada de aplicação com regras de negócio
- `LidyDecorApp.Domain` – Entidades e interfaces de domínio
- `LidyDecorApp.Infrastructure` – Acesso a dados (SQLite) e serviços externos
- `LidyDecorApp.Shared` – Modelos e recursos compartilhados
- `LidyDecorApp.Tests` – Projeto de testes automatizados (xUnit + Moq)
- `LidyDecorApp.Web` – Frontend com Razor Pages

## 🔧 Tecnologias Utilizadas

- **.NET 8.0**
- **SQLite** como banco de dados local
- **JWT** para autenticação
- **Swagger** com suporte a autenticação para teste de APIs
- **AutoMapper** para mapeamento de DTOs
- **FluentValidation** para validação de dados
- **xUnit + Moq** para testes automatizados
- **Razor Pages** para interface web

## 🚀 Como Rodar Localmente

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/LidyDecorApp.git
   ```

2. Abra a solução no Visual Studio 2022 ou superior.

3. Execute os projetos simultaneamente:
   - `LidyDecorApp.API`
   - `LidyDecorApp.Web`

4. Acesse o frontend em:
   ```
   https://localhost:5001
   ```

5. Use o Swagger em:
   ```
   https://localhost:5001/swagger
   ```

> ⚠️ É necessário autenticar-se via JWT para utilizar os endpoints via Swagger.

## 🧪 Testes

Os testes automatizados cobrem:

- Controllers
- Services
- Validadores (FluentValidation)

Execute os testes usando o Test Explorer do Visual Studio.

## 👤 Autor

Desenvolvido por **Rafael Melo**.
