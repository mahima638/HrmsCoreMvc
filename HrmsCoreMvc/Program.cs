using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Repositories.Reports;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Services.Reports;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAttendanceReports, AttendanceReportService>();
builder.Services.AddScoped<ILeaveReports, LeaveReportService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventTypeService, EventTypeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskBoardService, TaskBoardService>();
builder.Services.AddScoped<ITaskMembersService, TaskMembersService>();



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

app.UseStaticFiles();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Event}/{action=Holidays}/{id?}")
    .WithStaticAssets();


app.Run();
