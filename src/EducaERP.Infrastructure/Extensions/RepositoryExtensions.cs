using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Authentication;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Core.Interfaces;
using EducaERP.Infrastructure.Repositories;
using EducaERP.Infrastructure.Repositories.Academics;
using EducaERP.Infrastructure.Repositories.Authentication;
using EducaERP.Infrastructure.Repositories.Institutions;
using EducaERP.Infrastructure.Repositories.Students;
using EducaERP.Infrastructure.Repositories.Teachers;
using Microsoft.Extensions.DependencyInjection;

namespace EducaERP.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IInstitutionRepository, InstitutionRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseDisciplineRepository, CourseRepository>();
        services.AddScoped<IDisciplineRepository, DisciplineRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();

        return services;
    }
}