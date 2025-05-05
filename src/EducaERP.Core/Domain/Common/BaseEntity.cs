namespace EducaERP.Core.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataAtualizacao { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
        }

        public void UpdateTimestamp()
        {
            DataAtualizacao = DateTime.UtcNow;
        }
    }
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}