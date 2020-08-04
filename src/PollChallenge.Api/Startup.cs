using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PollChallenge.Api.Extensions;
using PollChallenge.Api.Services;
using PollChallenge.Api.ViewModels;
using PollChallenge.Model.Data;
using PollChallenge.Model.Entities;
using System.Linq;

namespace PollChallenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options 
                => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.ConfigureSwaggerDoc("**API for Poll Management of PollChallenge.Api**");

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureSwaggerUI();
        }

        private void RegisterServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Option, OptionGetVM>();
                cfg.CreateMap<Poll, PollGetVM>();

                cfg.CreateMap<PollPostVM, Poll>().ForMember(dst => dst.Options,
                        map => map.MapFrom(scr => scr.Options.Select
                        ((desc, index) => new Option { Id = index + 1, Description = desc })));

                cfg.CreateMap<Option, VoteGetVM>();
                cfg.CreateMap<Poll, StatGetVM>();
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddDbContext<PollDbContext>(options 
                => options.UseSqlite(Configuration.GetConnectionString("Sqlite")));

            services.AddScoped<IPollSrv, PollSrv>();
        }
    }
}
