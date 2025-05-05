using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Institutions;
using EducaERP.Application.DTOs.Students;
using EducaERP.Application.DTOs.Teachers;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Authentication;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Application.Services.Academics;
using EducaERP.Application.Services.Authentication;
using EducaERP.Application.Services.Institutions;
using EducaERP.Application.Services.Students;
using EducaERP.Application.Services.Teachers;
using EducaERP.Application.Validators.Academics;
using EducaERP.Application.Validators.Institutions;
using EducaERP.Application.Validators.Students;
using EducaERP.Application.Validators.Teachers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EducaERP.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registro automático de serviços
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IInstitutionService, InstitutionService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseDisciplineService, CourseService>();
        services.AddScoped<IDisciplineService, DisciplineService>();
        services.AddScoped<IGradeService, GradeService>();

        // AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Validações FluentValidation
        services.AddScoped<IValidator<InstitutionDTO>, InstitutionValidator>();
        services.AddScoped<IValidator<EmployeeDTO>, EmployeeValidator>();
        services.AddScoped<IValidator<StudentDTO>, StudentValidator>();
        services.AddScoped<IValidator<TeacherDTO>, TeacherValidator>();
        services.AddScoped<IValidator<AttendanceDTO>, AttendanceValidator>();
        services.AddScoped<IValidator<CourseDTO>, CourseValidator>();
        services.AddScoped<IValidator<DisciplineDTO>, DisciplineValidator>();
        services.AddScoped<IValidator<GradeDTO>, GradeValidator>();

        return services;
    }
}