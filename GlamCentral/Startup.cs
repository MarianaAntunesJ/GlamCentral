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
        #region Propriedade
        public IConfiguration Configuration { get; } 
        #endregion

        #region Construtor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Services
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            #region Entities Scopes
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddScoped<IImagemRepository, ImagemRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            #endregion

            #region Email Scope
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
            #endregion

            #region Cache
            services.AddMemoryCache();
            services.AddSession(options =>
            {
            });
            #endregion

            #region Login Scope
            services.AddScoped<Sessao>();
            services.AddScoped<LoginFuncionario>();
            #endregion

            #region MVC
            services.AddControllersWithViews();
            services.AddMvc(_ =>
            {
                _.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "O Campo deve ser preenchido!");
            });
            #endregion

            #region Database
            services.AddDbContext<GCContext>(options
                    => options.UseSqlServer(Configuration.GetConnectionString("GCContext")));
            #endregion
        } 
        #endregion

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Se ambiente de desenvolvimento, mostrar mensagem de erro personalizado
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Se prod/homolog erro padrão
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

            // Middleware criado para a utilização do AntiforgeryToken em todos os Forms
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
