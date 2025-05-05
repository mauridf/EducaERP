
# EducaERP - API Documentation

API completa para o sistema de gestão educacional EducaERP.

## Índice
1. [Autenticação](#autenticação)
2. [Módulo Acadêmico](#módulo-acadêmico)
3. [Módulo Financeiro](#módulo-financeiro)
4. [Módulo de Biblioteca](#módulo-de-biblioteca)
5. [Módulo de Recursos Humanos](#módulo-de-recursos-humanos)
6. [Módulo de Relatórios](#módulo-de-relatórios)
7. [Modelos de Dados](#modelos-de-dados)
8. [Configuração](#configuração)

---

## Autenticação

### `POST /api/auth/login`
Autentica usuários no sistema.

**Request:**
```json
{
    "email": "usuario@escola.com",
    "senha": "Senha@123"
}
```
**Response:**
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "validoAte": "2023-12-31T23:59:59",
    "usuario": {
        "id": "d3d3d3d3-5717-4562-b3fc-2c963f66afa6",
        "nome": "Maria Oliveira",
        "perfil": "Administrador"
    }
}
```

---

## Módulo Acadêmico

### `GET /api/alunos`
Lista todos os alunos matriculados.

### `POST /api/alunos`
Cadastra um novo aluno.

**Request:**
```json
{
    "nome": "João da Silva",
    "cpf": "123.456.789-00",
    "dataNascimento": "2010-05-15",
    "turmaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### `GET /api/turmas/{id}/boletim`
Obtém o boletim de uma turma específica.

---

## Módulo Financeiro

### `POST /api/financeiro/mensalidades`
Gera mensalidades para todos os alunos.

### `GET /api/financeiro/pendentes`
Lista pagamentos pendentes.

### `POST /api/financeiro/pagamentos`
Registra um pagamento.

**Request:**
```json
{
    "alunoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "valor": 1200.50,
    "metodoPagamento": "PIX",
    "referencia": "Mensalidade 10/2023"
}
```

---

## Módulo de Biblioteca

### `POST /api/biblioteca/reservas`
Cria nova reserva de livro (ver seção detalhada anterior).

### `GET /api/biblioteca/livros/disponiveis`
Lista livros disponíveis para empréstimo.

### `POST /api/biblioteca/devolucoes`
Registra devolução de livro.

---

## Módulo de Recursos Humanos

### `GET /api/rh/funcionarios`
Lista todos os funcionários.

### `POST /api/rh/folha-pagamento`
Gera folha de pagamento.

**Request:**
```json
{
    "mesReferencia": 10,
    "anoReferencia": 2023,
    "inclusaoBeneficios": true
}
```

---

## Módulo de Relatórios

### `GET /api/relatorios/matriculas?ano=2023`
Gera relatório de matrículas por ano.

### `POST /api/relatorios/customizado`
Executa relatório customizado.

**Request:**
```json
{
    "tipoRelatorio": "FinanceiroAnual",
    "parametros": {
        "ano": 2023,
        "mostrarDetalhes": true
    }
}
```

---

## Modelos de Dados Principais

### Aluno
```csharp
public class Aluno {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public Guid TurmaId { get; set; }
    public bool Ativo { get; set; }
}
```

### Turma
```csharp
public class Turma {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int AnoLetivo { get; set; }
    public Guid ProfessorResponsavelId { get; set; }
}
```

### Livro
```csharp
public class Livro {
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public int QuantidadeDisponivel { get; set; }
}
```

### Funcionario
```csharp
public class Funcionario {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cargo { get; set; }
    public decimal SalarioBase { get; set; }
    public DateTime DataAdmissao { get; set; }
}
```

---

## Configuração

### Variáveis de Ambiente

| Variável        | Descrição                     | Exemplo              |
|----------------|-------------------------------|----------------------|
| DB_CONNECTION   | String de conexão com o banco | "Server=..."         |
| JWT_SECRET      | Chave para tokens JWT         | "supersecretkey123"  |
| SMTP_HOST       | Servidor SMTP para e-mails    | "smtp.escola.com"    |

### Migrações do Banco
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Swagger

A documentação interativa está disponível em:
https://seuservidor.com/swagger

---

## Licença

EducaERP © 2023 - Licença MIT
