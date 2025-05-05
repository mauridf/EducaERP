using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Library
{
    public class BookDTO
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int AnoPublicacao { get; set; }
        public string ISBN { get; set; }
        public BookCategory Categoria { get; set; }
        public int QuantidadeTotal { get; set; }
    }
}