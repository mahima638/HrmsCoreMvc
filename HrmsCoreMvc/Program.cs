using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories.Promotions;
using HrmsCoreMvc.Repositories.Reports;
using HrmsCoreMvc.Services.Promotions;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Services.Reports;
using Microsoft.EntityFrameworkCore;

using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

using HrmsCoreMvc.Exceptions;
using HrmsCoreMvc.Repositories.Resignations;
using HrmsCoreMvc.Services.Resignations;
using HrmsCoreMvc.Repositories.Terminations;
using HrmsCoreMvc.Services.Terminations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAttendanceReports, AttendanceReportService>();
builder.Services.AddScoped<IPromotionRepository, PromotionService>();
builder.Services.AddScoped<IResignationRepository, ResignationService>();
builder.Services.AddScoped<IPromotionRepository, PromotionService>();
builder.Services.AddScoped<ITerminationRepository, TerminationService>();
builder.Services.AddScoped<ILeaveReports, LeaveReportService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventTypeService, EventTypeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskBoardService, TaskBoardService>();
builder.Services.AddScoped<ITaskMembersService, TaskMembersService>();
builder.Services.AddScoped<IDailyReportService, DailyReportService>();
builder.Services.AddScoped<ITaskReportService, TaskReportService>();
//builder.Services.AddScoped<IAllProjectsService,AllProjectsReportService>();
builder.Services.AddScoped<IEmployeeReportService, EmployeeReportService>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();


//builder.Services
//    .AddAuthentication(options =>
//    {
//        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//    })
//    .AddCookie()
//    .AddGoogle(options =>
//    {
//        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//    });

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDepartmentService, Departmentservice>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<ILeaveService, LeaveService>();

var app = builder.Build();

//app.UseMiddleware<GlobalExceptionsFile>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
