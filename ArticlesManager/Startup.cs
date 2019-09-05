using System;
using ArticlesManager.Contexts;
using ArticlesManager.GraphQl.mutations;
using ArticlesManager.GraphQl.schemas;
using ArticlesManager.GraphQl.schemas.schemaGroups;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.RabbitMqController;
using ArticlesManager.Repositories;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesManager
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
            Environment.SetEnvironmentVariable("CS",Configuration.GetSection("ConnectionString").Value);
            Environment.SetEnvironmentVariable("crawlerFileId","1");
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions( 
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddDbContext<ArticlesContext>(); 
            
            services.AddScoped<ArticlesRepository>();

            services.AddScoped<PublisherRepository>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            
            services.AddScoped<ISchemaGroup, ArticleSchema>();
            services.AddScoped<ISchemaGroup, PublisherSchema>();


            services.AddScoped<RootSchema>();
            
            services.AddScoped<RootMutation>();

            services.AddSingleton<RabbitManager>();
            
            services.AddGraphQL(opt => { opt.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
             
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseRabbitListener();
            
            app.UseGraphQL<RootSchema>();
            
          
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            
            
        }
    }
}