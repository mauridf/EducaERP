using EducaERP.Application.DTOs.Library.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Authentication;
using EducaERP.Application.Interfaces.Enrollments;
using EducaERP.Application.Interfaces.Financial;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Core.Interfaces;
using EducaERP.Infrastructure.Repositories;
using EducaERP.Infrastructure.Repositories.Academics;
using EducaERP.Infrastructure.Repositories.Authentication;
using EducaERP.Infrastructure.Repositories.Enrollments;
using EducaERP.Infrastructure.Repositories.Financial;
using EducaERP.Infrastructure.Repositories.Institutions;
using EducaERP.Infrastructure.Repositories.Library;
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
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IInstallmentRepository, InstallmentRepository>();
        services.AddScoped<ITuitionRepository, TuitionRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookLoanRepository, BookLoanRepository>();
        services.AddScoped<IBookReservationRepository, BookReservationRepository>();

        return services;
    }
}