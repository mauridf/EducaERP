using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaERP.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cursos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    cargahorariatotal = table.Column<int>(type: "integer", nullable: false),
                    nivel = table.Column<int>(type: "integer", nullable: false),
                    modalidade = table.Column<int>(type: "integer", nullable: false),
                    duracaomeses = table.Column<int>(type: "integer", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cursos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "disciplinas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    cargahoraria = table.Column<int>(type: "integer", nullable: false),
                    periodo = table.Column<int>(type: "integer", nullable: false),
                    obrigatoria = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplinas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "instituicoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    endereco = table.Column<string>(type: "text", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    uf = table.Column<string>(type: "text", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instituicoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "livros",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    autor = table.Column<string>(type: "text", nullable: false),
                    editora = table.Column<string>(type: "text", nullable: false),
                    anopublicacao = table.Column<int>(type: "integer", nullable: false),
                    isbn = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    categoria = table.Column<int>(type: "integer", nullable: false),
                    quantidadetotal = table.Column<int>(type: "integer", nullable: false),
                    quantidadedisponivel = table.Column<int>(type: "integer", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cursodisciplinas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cursoid = table.Column<Guid>(type: "uuid", nullable: false),
                    disciplinaid = table.Column<Guid>(type: "uuid", nullable: false),
                    ordem = table.Column<int>(type: "integer", nullable: false),
                    obrigatoria = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cursodisciplinas", x => x.id);
                    table.ForeignKey(
                        name: "FK_cursodisciplinas_cursos_cursoid",
                        column: x => x.cursoid,
                        principalTable: "cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cursodisciplinas_disciplinas_disciplinaid",
                        column: x => x.disciplinaid,
                        principalTable: "disciplinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alunos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    instituicaoid = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    endereco = table.Column<string>(type: "text", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    uf = table.Column<string>(type: "text", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunos", x => x.id);
                    table.ForeignKey(
                        name: "FK_alunos_instituicoes_instituicaoid",
                        column: x => x.instituicaoid,
                        principalTable: "instituicoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    instituicaoid = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    endereco = table.Column<string>(type: "text", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    uf = table.Column<string>(type: "text", nullable: false),
                    cargo = table.Column<int>(type: "integer", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_funcionarios_instituicoes_instituicaoid",
                        column: x => x.instituicaoid,
                        principalTable: "instituicoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "professores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    instituicaoid = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    endereco = table.Column<string>(type: "text", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    uf = table.Column<string>(type: "text", nullable: false),
                    titulacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professores", x => x.id);
                    table.ForeignKey(
                        name: "FK_professores_instituicoes_instituicaoid",
                        column: x => x.instituicaoid,
                        principalTable: "instituicoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "frequencias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    alunoid = table.Column<Guid>(type: "uuid", nullable: false),
                    disciplinaid = table.Column<Guid>(type: "uuid", nullable: false),
                    dataaula = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    presenca = table.Column<int>(type: "integer", nullable: false),
                    observacoes = table.Column<string>(type: "text", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frequencias", x => x.id);
                    table.ForeignKey(
                        name: "FK_frequencias_alunos_alunoid",
                        column: x => x.alunoid,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_frequencias_disciplinas_disciplinaid",
                        column: x => x.disciplinaid,
                        principalTable: "disciplinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matriculas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    alunoid = table.Column<Guid>(type: "uuid", nullable: false),
                    cursoid = table.Column<Guid>(type: "uuid", nullable: false),
                    anoletivo = table.Column<string>(type: "text", nullable: false),
                    periodoletivo = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matriculas", x => x.id);
                    table.ForeignKey(
                        name: "FK_matriculas_alunos_alunoid",
                        column: x => x.alunoid,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matriculas_cursos_cursoid",
                        column: x => x.cursoid,
                        principalTable: "cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mensalidades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    alunoid = table.Column<Guid>(type: "uuid", nullable: false),
                    cursoid = table.Column<Guid>(type: "uuid", nullable: false),
                    referencia = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    valor = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    datavencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    statuspagamento = table.Column<int>(type: "integer", nullable: false),
                    datapagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    formapagamento = table.Column<int>(type: "integer", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mensalidades", x => x.id);
                    table.ForeignKey(
                        name: "FK_mensalidades_alunos_alunoid",
                        column: x => x.alunoid,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mensalidades_cursos_cursoid",
                        column: x => x.cursoid,
                        principalTable: "cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    alunoid = table.Column<Guid>(type: "uuid", nullable: false),
                    disciplinaid = table.Column<Guid>(type: "uuid", nullable: false),
                    tipoavaliacao = table.Column<int>(type: "integer", nullable: false),
                    descricaoavaliacao = table.Column<string>(type: "text", nullable: false),
                    dataavaliacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    notaobtida = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    peso = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notas", x => x.id);
                    table.ForeignKey(
                        name: "FK_notas_alunos_alunoid",
                        column: x => x.alunoid,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notas_disciplinas_disciplinaid",
                        column: x => x.disciplinaid,
                        principalTable: "disciplinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emprestimos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    livroid = table.Column<Guid>(type: "uuid", nullable: false),
                    alunoid = table.Column<Guid>(type: "uuid", nullable: true),
                    professorid = table.Column<Guid>(type: "uuid", nullable: true),
                    funcionarioid = table.Column<Guid>(type: "uuid", nullable: true),
                    dataemprestimo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataprevistadevolucao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    datadevolucao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emprestimos", x => x.id);
                    table.ForeignKey(
                        name: "FK_emprestimos_alunos_alunoid",
                        column: x => x.alunoid,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_emprestimos_funcionarios_funcionarioid",
                        column: x => x.funcionarioid,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_emprestimos_livros_livroid",
                        column: x => x.livroid,
                        principalTable: "livros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emprestimos_professores_professorid",
                        column: x => x.professorid,
                        principalTable: "professores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "professordisciplinas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    professorid = table.Column<Guid>(type: "uuid", nullable: false),
                    disciplinaid = table.Column<Guid>(type: "uuid", nullable: false),
                    ehresponsavel = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    datainicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    datafim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professordisciplinas", x => x.id);
                    table.ForeignKey(
                        name: "FK_professordisciplinas_disciplinas_disciplinaid",
                        column: x => x.disciplinaid,
                        principalTable: "disciplinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_professordisciplinas_professores_professorid",
                        column: x => x.professorid,
                        principalTable: "professores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    livroid = table.Column<Guid>(type: "uuid", nullable: false),
                    alunoid = table.Column<Guid>(type: "uuid", nullable: true),
                    professorid = table.Column<Guid>(type: "uuid", nullable: true),
                    funcionarioid = table.Column<Guid>(type: "uuid", nullable: true),
                    datareserva = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservas", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservas_alunos_alunoid",
                        column: x => x.alunoid,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_reservas_funcionarios_funcionarioid",
                        column: x => x.funcionarioid,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_reservas_livros_livroid",
                        column: x => x.livroid,
                        principalTable: "livros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservas_professores_professorid",
                        column: x => x.professorid,
                        principalTable: "professores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    senhahash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tipousuario = table.Column<int>(type: "integer", nullable: false),
                    alunoid = table.Column<Guid>(type: "uuid", nullable: true),
                    professorid = table.Column<Guid>(type: "uuid", nullable: true),
                    funcionarioid = table.Column<Guid>(type: "uuid", nullable: true),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_alunos_alunoid",
                        column: x => x.alunoid,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_usuarios_funcionarios_funcionarioid",
                        column: x => x.funcionarioid,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_usuarios_professores_professorid",
                        column: x => x.professorid,
                        principalTable: "professores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "parcelamentos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    mensalidadeid = table.Column<Guid>(type: "uuid", nullable: false),
                    parcelanumero = table.Column<int>(type: "integer", nullable: false),
                    valorparcela = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    datavencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pago = table.Column<bool>(type: "boolean", nullable: false),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataatualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parcelamentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_parcelamentos_mensalidades_mensalidadeid",
                        column: x => x.mensalidadeid,
                        principalTable: "mensalidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alunos_cpf",
                table: "alunos",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_alunos_instituicaoid",
                table: "alunos",
                column: "instituicaoid");

            migrationBuilder.CreateIndex(
                name: "IX_cursodisciplinas_cursoid_disciplinaid",
                table: "cursodisciplinas",
                columns: new[] { "cursoid", "disciplinaid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cursodisciplinas_disciplinaid",
                table: "cursodisciplinas",
                column: "disciplinaid");

            migrationBuilder.CreateIndex(
                name: "IX_cursos_codigo",
                table: "cursos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_disciplinas_codigo",
                table: "disciplinas",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_emprestimos_alunoid",
                table: "emprestimos",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_emprestimos_funcionarioid",
                table: "emprestimos",
                column: "funcionarioid");

            migrationBuilder.CreateIndex(
                name: "IX_emprestimos_livroid",
                table: "emprestimos",
                column: "livroid");

            migrationBuilder.CreateIndex(
                name: "IX_emprestimos_professorid",
                table: "emprestimos",
                column: "professorid");

            migrationBuilder.CreateIndex(
                name: "IX_frequencias_alunoid",
                table: "frequencias",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_frequencias_disciplinaid",
                table: "frequencias",
                column: "disciplinaid");

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_cpf",
                table: "funcionarios",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_instituicaoid",
                table: "funcionarios",
                column: "instituicaoid");

            migrationBuilder.CreateIndex(
                name: "IX_instituicoes_cnpj",
                table: "instituicoes",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_livros_isbn",
                table: "livros",
                column: "isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_matriculas_alunoid",
                table: "matriculas",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_matriculas_cursoid",
                table: "matriculas",
                column: "cursoid");

            migrationBuilder.CreateIndex(
                name: "IX_mensalidades_alunoid",
                table: "mensalidades",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_mensalidades_cursoid",
                table: "mensalidades",
                column: "cursoid");

            migrationBuilder.CreateIndex(
                name: "IX_notas_alunoid",
                table: "notas",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_notas_disciplinaid",
                table: "notas",
                column: "disciplinaid");

            migrationBuilder.CreateIndex(
                name: "IX_parcelamentos_mensalidadeid",
                table: "parcelamentos",
                column: "mensalidadeid");

            migrationBuilder.CreateIndex(
                name: "IX_professordisciplinas_disciplinaid",
                table: "professordisciplinas",
                column: "disciplinaid");

            migrationBuilder.CreateIndex(
                name: "IX_professordisciplinas_professorid_disciplinaid",
                table: "professordisciplinas",
                columns: new[] { "professorid", "disciplinaid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_professores_cpf",
                table: "professores",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_professores_instituicaoid",
                table: "professores",
                column: "instituicaoid");

            migrationBuilder.CreateIndex(
                name: "IX_reservas_alunoid",
                table: "reservas",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_reservas_funcionarioid",
                table: "reservas",
                column: "funcionarioid");

            migrationBuilder.CreateIndex(
                name: "IX_reservas_livroid",
                table: "reservas",
                column: "livroid");

            migrationBuilder.CreateIndex(
                name: "IX_reservas_professorid",
                table: "reservas",
                column: "professorid");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_alunoid",
                table: "usuarios",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_funcionarioid",
                table: "usuarios",
                column: "funcionarioid");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_professorid",
                table: "usuarios",
                column: "professorid");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_usuario",
                table: "usuarios",
                column: "usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cursodisciplinas");

            migrationBuilder.DropTable(
                name: "emprestimos");

            migrationBuilder.DropTable(
                name: "frequencias");

            migrationBuilder.DropTable(
                name: "matriculas");

            migrationBuilder.DropTable(
                name: "notas");

            migrationBuilder.DropTable(
                name: "parcelamentos");

            migrationBuilder.DropTable(
                name: "professordisciplinas");

            migrationBuilder.DropTable(
                name: "reservas");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "mensalidades");

            migrationBuilder.DropTable(
                name: "disciplinas");

            migrationBuilder.DropTable(
                name: "livros");

            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "professores");

            migrationBuilder.DropTable(
                name: "alunos");

            migrationBuilder.DropTable(
                name: "cursos");

            migrationBuilder.DropTable(
                name: "instituicoes");
        }
    }
}
