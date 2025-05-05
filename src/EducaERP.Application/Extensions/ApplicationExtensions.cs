using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Enrollments;
using EducaERP.Application.DTOs.Financial;
using EducaERP.Application.DTOs.Institutions;
using EducaERP.Application.DTOs.Library;
using EducaERP.Application.DTOs.Students;
using EducaERP.Application.DTOs.Teachers;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Authentication;
using EducaERP.Application.Interfaces.Enrollments;
using EducaERP.Application.Interfaces.Financial;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Application.Services.Academics;
using EducaERP.Application.Services.Authentication;
using EducaERP.Application.Services.Enrollments;
using EducaERP.Application.Services.Financial;
using EducaERP.Application.Services.Institutions;
using EducaERP.Application.Services.Library;
using EducaERP.Application.Services.Students;
using EducaERP.Application.Services.Teachers;
using EducaERP.Application.Validators.Academics;
using EducaERP.Application.Validators.Enrollments;
using EducaERP.Application.Validators.Financial;
using EducaERP.Application.Validators.Institutions;
using EducaERP.Application.Validators.Library;
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
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IInstallmentService, InstallmentService>();
        services.AddScoped<ITuitionService, TuitionService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookLoanService, BookLoanService>();
        services.AddScoped<IBookReservationService, BookReservationService>();

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
        services.AddScoped<IValidator<EnrollmentDTO>, EnrollmentValidator>();
        services.AddScoped<IValidator<InstallmentDTO>, InstallmentValidator>();
        services.AddScoped<IValidator<TuitionDTO>, TuitionValidator>();
        services.AddScoped<IValidator<BookDTO>, BookValidator>();
        services.AddScoped<IValidator<BookLoanDTO>, BookLoanValidator>();
        services.AddScoped<IValidator<BookReservationDTO>, BookReservationValidator>();

        return services;
    }
}