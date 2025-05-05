namespace EducaERP.Application.Interfaces.Academics
{
    public interface ICourseDisciplineService
    {
        Task UpdateCourseDisciplineOrder(Guid courseId, Guid disciplineId, int newOrder);
        Task ToggleDisciplineRequirement(Guid courseId, Guid disciplineId);
    }
}