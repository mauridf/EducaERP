using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Domain.Authentication;
using EducaERP.Core.Domain.Enrollments;
using EducaERP.Core.Domain.Financial;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Domain.Teachers;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Data
{
    public class EducaERPDbContext : DbContext
    {
        public EducaERPDbContext(DbContextOptions<EducaERPDbContext> options) : base(options)
        {
        }

        // DbSets para todas as entidades
        public DbSet<Institution> Instituicoes { get; set; }
        public DbSet<Employee> Funcionarios { get; set; }
        public DbSet<Student> Alunos { get; set; }
        public DbSet<Teacher> Professores { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Course> Cursos { get; set; }
        public DbSet<Discipline> Disciplinas { get; set; }
        public DbSet<CourseDiscipline> CursoDisciplinas { get; set; }
        public DbSet<TeacherDiscipline> ProfessorDisciplinas { get; set; }
        public DbSet<Enrollment> Matriculas { get; set; }
        public DbSet<Attendance> Frequencias { get; set; }
        public DbSet<Grade> Notas { get; set; }
        public DbSet<Tuition> Mensalidades { get; set; }
        public DbSet<Installment> Parcelamentos { get; set; }
        public DbSet<Book> Livros { get; set; }
        public DbSet<BookLoan> Emprestimos { get; set; }
        public DbSet<BookReservation> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para PostgreSQL (nomes em snake_case)
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToLower());
                }
            }

            ConfigurarInstituicao(modelBuilder);
            ConfigurarFuncionario(modelBuilder);
            ConfigurarAluno(modelBuilder);
            ConfigurarProfessor(modelBuilder);
            ConfigurarUsuario(modelBuilder);
            ConfigurarCurso(modelBuilder);
            ConfigurarDisciplina(modelBuilder);
            ConfigurarCursoDisciplina(modelBuilder);
            ConfigurarProfessorDisciplina(modelBuilder);
            ConfigurarMatricula(modelBuilder);
            ConfigurarFrequencia(modelBuilder);
            ConfigurarNota(modelBuilder);
            ConfigurarMensalidade(modelBuilder);
            ConfigurarParcela(modelBuilder);
            ConfigurarLivro(modelBuilder);
            ConfigurarEmprestimo(modelBuilder);
            ConfigurarReserva(modelBuilder);
        }

        private void ConfigurarInstituicao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institution>(entity =>
            {
                entity.HasIndex(i => i.Cnpj).IsUnique();
                entity.Property(i => i.Cnpj).IsRequired().HasMaxLength(14);
                entity.Property(i => i.Nome).IsRequired().HasMaxLength(200);
                entity.Property(i => i.Email).IsRequired().HasMaxLength(100);
                entity.Property(i => i.Telefone).IsRequired().HasMaxLength(20);
            });
        }

        private void ConfigurarFuncionario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.Cpf).IsUnique();
                entity.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.Instituicao)
                    .WithMany(i => i.Funcionarios)
                    .HasForeignKey(e => e.InstituicaoId);
            });
        }

        private void ConfigurarAluno(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(a => a.Cpf).IsUnique();
                entity.Property(a => a.Cpf).IsRequired().HasMaxLength(11);
                entity.Property(a => a.Nome).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Email).IsRequired().HasMaxLength(100);

                entity.HasOne(a => a.Instituicao)
                    .WithMany(i => i.Alunos)
                    .HasForeignKey(a => a.InstituicaoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigurarLivro(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(b => b.ISBN).IsUnique();
                entity.Property(b => b.ISBN).IsRequired().HasMaxLength(13);
                entity.Property(b => b.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(b => b.QuantidadeTotal).IsRequired();
                entity.Property(b => b.QuantidadeDisponivel).IsRequired();
            });
        }

        private void ConfigurarProfessor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(p => p.Cpf).IsUnique();
                entity.Property(p => p.Cpf).IsRequired().HasMaxLength(11);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Titulacao).IsRequired().HasMaxLength(100);

                entity.HasOne(p => p.Instituicao)
                    .WithMany(i => i.Professores)
                    .HasForeignKey(p => p.InstituicaoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigurarUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Usuario).IsUnique();
                entity.Property(u => u.Usuario).IsRequired().HasMaxLength(50);
                entity.Property(u => u.SenhaHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.TipoUsuario).IsRequired();

                // Relacionamentos opcionais
                entity.HasOne(u => u.Aluno)
                    .WithMany()
                    .HasForeignKey(u => u.AlunoId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(u => u.Professor)
                    .WithMany()
                    .HasForeignKey(u => u.ProfessorId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(u => u.Funcionario)
                    .WithMany()
                    .HasForeignKey(u => u.FuncionarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }

        private void ConfigurarCurso(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(c => c.Codigo).IsUnique();
                entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Codigo).IsRequired().HasMaxLength(20);
                entity.Property(c => c.CargaHorariaTotal).IsRequired();
                entity.Property(c => c.DuracaoMeses).IsRequired();
                entity.Property(c => c.Ativo).HasDefaultValue(true);
            });
        }

        private void ConfigurarDisciplina(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.HasIndex(d => d.Codigo).IsUnique();
                entity.Property(d => d.Nome).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Codigo).IsRequired().HasMaxLength(20);
                entity.Property(d => d.CargaHoraria).IsRequired();
                entity.Property(d => d.Obrigatoria).HasDefaultValue(true);
                entity.Property(d => d.Ativo).HasDefaultValue(true);
            });
        }

        private void ConfigurarCursoDisciplina(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseDiscipline>(entity =>
            {
                entity.HasKey(cd => cd.Id);
                entity.HasIndex(cd => new { cd.CursoId, cd.DisciplinaId }).IsUnique();

                entity.Property(cd => cd.Ordem).IsRequired();
                entity.Property(cd => cd.Obrigatoria).HasDefaultValue(true);

                entity.HasOne(cd => cd.Curso)
                    .WithMany(c => c.Disciplinas)
                    .HasForeignKey(cd => cd.CursoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cd => cd.Disciplina)
                    .WithMany(d => d.Cursos)
                    .HasForeignKey(cd => cd.DisciplinaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurarProfessorDisciplina(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherDiscipline>(entity =>
            {
                entity.HasKey(td => td.Id);
                entity.HasIndex(td => new { td.ProfessorId, td.DisciplinaId }).IsUnique();

                entity.Property(td => td.EhResponsavel).HasDefaultValue(false);
                entity.Property(td => td.DataInicio).IsRequired();

                entity.HasOne(td => td.Professor)
                    .WithMany(p => p.Disciplinas)
                    .HasForeignKey(td => td.ProfessorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(td => td.Disciplina)
                    .WithMany(d => d.Professores)
                    .HasForeignKey(td => td.DisciplinaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurarMatricula(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.Property(m => m.Status).IsRequired();

                entity.HasOne(m => m.Aluno)
                    .WithMany(a => a.Matriculas)
                    .HasForeignKey(m => m.AlunoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Curso)
                    .WithMany(c => c.Matriculas)
                    .HasForeignKey(m => m.CursoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurarFrequencia(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(f => f.DataAula).IsRequired();
                entity.Property(f => f.Presenca).IsRequired();

                entity.HasOne(f => f.Aluno)
                    .WithMany(a => a.Frequencias)
                    .HasForeignKey(f => f.AlunoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Disciplina)
                    .WithMany()
                    .HasForeignKey(f => f.DisciplinaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurarNota(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(n => n.TipoAvaliacao).IsRequired();
                entity.Property(n => n.DataAvaliacao).IsRequired();
                entity.Property(n => n.NotaObtida).HasColumnType("decimal(5,2)").IsRequired();
                entity.Property(n => n.Peso).HasColumnType("decimal(3,2)").IsRequired();

                entity.HasOne(n => n.Aluno)
                    .WithMany(a => a.Notas)
                    .HasForeignKey(n => n.AlunoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(n => n.Disciplina)
                    .WithMany()
                    .HasForeignKey(n => n.DisciplinaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurarMensalidade(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tuition>(entity =>
            {
                entity.Property(m => m.Referencia).IsRequired().HasMaxLength(20);
                entity.Property(m => m.Valor).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(m => m.DataVencimento).IsRequired();
                entity.Property(m => m.StatusPagamento).IsRequired();

                entity.HasOne(m => m.Aluno)
                    .WithMany(a => a.Mensalidades)
                    .HasForeignKey(m => m.AlunoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Curso)
                    .WithMany()
                    .HasForeignKey(m => m.CursoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurarParcela(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Installment>(entity =>
            {
                entity.Property(p => p.ParcelaNumero).IsRequired();
                entity.Property(p => p.ValorParcela).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(p => p.DataVencimento).IsRequired();

                entity.HasOne(p => p.Mensalidade)
                    .WithMany(m => m.Parcelamentos)
                    .HasForeignKey(p => p.MensalidadeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurarEmprestimo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLoan>(entity =>
            {
                entity.Property(e => e.DataEmprestimo).IsRequired();
                entity.Property(e => e.DataPrevistaDevolucao).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                // Configuração dos relacionamentos
                entity.HasOne(e => e.Livro)
                    .WithMany(l => l.Emprestimos)
                    .HasForeignKey(e => e.LivroId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Aluno)
                    .WithMany(a => a.Emprestimos)
                    .HasForeignKey(e => e.AlunoId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Professor)
                    .WithMany(p => p.Emprestimos)
                    .HasForeignKey(e => e.ProfessorId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Funcionario)
                    .WithMany(f => f.Emprestimos)
                    .HasForeignKey(e => e.FuncionarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }

        private void ConfigurarReserva(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookReservation>(entity =>
            {
                entity.Property(r => r.DataReserva).IsRequired();
                entity.Property(r => r.Status).IsRequired();

                // Configuração dos relacionamentos
                entity.HasOne(r => r.Livro)
                    .WithMany(l => l.Reservas)
                    .HasForeignKey(r => r.LivroId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Aluno)
                    .WithMany(a => a.ReservaLivros)
                    .HasForeignKey(r => r.AlunoId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(r => r.Professor)
                    .WithMany(p => p.ReservaLivros)
                    .HasForeignKey(r => r.ProfessorId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(r => r.Funcionario)
                    .WithMany(f => f.ReservaLivros)
                    .HasForeignKey(r => r.FuncionarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}