using System.Drawing;
using EducaERP.Core.Domain.Common;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Library
{
    public class Book : BaseEntity
    {
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public string Editora { get; private set; }
        public int AnoPublicacao { get; private set; }
        public string ISBN { get; private set; }
        public BookCategory Categoria { get; private set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }

        // Navigation properties
        public virtual ICollection<BookLoan>? Emprestimos { get; private set; } = new List<BookLoan>();
        public virtual ICollection<BookReservation>? Reservas { get; private set; } = new List<BookReservation>();

        protected Book() { }
        public Book(string titulo, string autor, string editora, int anoPublicacao,
                          string isbn, BookCategory categoria, int quantidadeTotal, int quantidadeDisponivel)
        {
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
            AnoPublicacao = anoPublicacao;
            ISBN = isbn;
            Categoria = categoria;
            QuantidadeTotal = quantidadeTotal;
            QuantidadeDisponivel = quantidadeDisponivel;

            Validate();
        }

        // Método para atualização
        public void Update(string titulo, string autor, string editora, int anoPublicacao,
                          string isbn, BookCategory categoria, int quantidadeTotal, int quantidadeDisponivel)
        {
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
            AnoPublicacao = anoPublicacao;
            ISBN = isbn;
            Categoria = categoria;
            QuantidadeTotal = quantidadeTotal;
            QuantidadeDisponivel = quantidadeDisponivel;
            UpdateTimestamp();

            Validate();
        }

        public void RegistrarEmprestimo()
        {
            if (QuantidadeDisponivel <= 0)
                throw new DomainException("Não há exemplares disponíveis para empréstimo");

            QuantidadeDisponivel--;
        }

        public void RegistrarDevolucao()
        {
            if (QuantidadeDisponivel >= QuantidadeTotal)
                throw new DomainException("Quantidade disponível inválida");

            QuantidadeDisponivel++;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                throw new DomainException("O titulo do livro é obrigatório");

            if (string.IsNullOrWhiteSpace(Autor))
                throw new DomainException("O autor do livro é obrigatório");

            if (string.IsNullOrWhiteSpace(Editora))
                throw new DomainException("A editora do livro é obrigatório");

            if (AnoPublicacao <= 0)
                throw new DomainException("Ano da publicação deve ser maior que zero");

            if (string.IsNullOrWhiteSpace(ISBN))
                throw new DomainException("O isbn do livro é obrigatório");

            if (QuantidadeTotal <= 0)
                throw new DomainException("Quantidade total deve ser maior que zero");

            if (QuantidadeDisponivel >= QuantidadeTotal)
                throw new DomainException("Quantidade disponível deve ser menor que a quantidade total");
        }
    }
}
