using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories.Promotions;
using HrmsCoreMvc.Repositories.Reports;
using HrmsCoreMvc.Services.Promotions;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Repositories.Reports;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Services.Reports;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Repositories.Resignations;
using HrmsCoreMvc.Services.Resignations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAttendanceReports, AttendanceReportService>();
builder.Services.AddScoped<IPromotionRepository, PromotionService>();
builder.Services.AddScoped<IResignationRepository, ResignationService>();
builder.Services.AddScoped<ILeaveReports, LeaveReportService>();

builder.Services.AddScoped<IDailyReportService, DailyReportService>();

builder.Services.AddScoped<ITaskReportService, TaskReportService>();

builder.Services.AddScoped<IAllProjectsService,AllProjectsReportService>();

builder.Services.AddScoped<IEmployeeReportService, EmployeeReportService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRoleService, RoleService>();


builder.Services.AddScoped<IDepartmentService, Departmentservice>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<ILeaveService, LeaveService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
