using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Library.Responses
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int AnoPublicacao { get; set; }
        public string ISBN { get; set; }
        public BookCategory Categoria { get; set; }
        public string CategoriaDescricao => Categoria.ToString();
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int EmprestimosAtivos { get; set; }
        public int ReservasAtivas { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}