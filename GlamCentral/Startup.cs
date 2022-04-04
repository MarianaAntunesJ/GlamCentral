using GlamCentral.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GlamCentral.Repository;
using GlamCentral.Repository.Interfaces;
using GlamCentral.Libraries.Session;
using GlamCentral.Libraries.Login;
using System.Net.Mail;
using System.Net;
using GlamCentral.Libraries.Email;
using GlamCentral.Libraries.Middleware;

namespace GlamCentral
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddScoped<IImagemRepository, ImagemRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();

            services.AddScoped<SmtpClient>(options =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };

                return smtp;
            });
            services.AddScoped<GerenciarEmail>();

            services.AddMemoryCache();
            services.AddSession(options =>
            {
            });

            services.AddScoped<Sessao>();
            services.AddScoped<LoginFuncionario>();

            services.AddControllersWithViews();
            services.AddMvc(_ =>
            {
                _.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "O Campo deve ser preenchido!");
            });

            services.AddDbContext<GCContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("GCContext")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMiddleware<ValidateAntiforgeryTokenMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Login}/{id?}",
                    defaults: new { area = "Funcionario"});

                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
