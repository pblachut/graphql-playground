using GraphqlApi.Dtos;
using GraphqlApi.SampleDomain;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;

namespace GraphqlApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            RavenDbConfig = configuration.GetSection("RavenDb").Get<RavenDbConfig>();
        }

        
        private RavenDbConfig RavenDbConfig { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(c => DocumentStoreFactory.Create(RavenDbConfig.Url, RavenDbConfig.Database))
                .As<IDocumentStore>();
            
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<EmployeeObjectType>();
            services.AddSingleton<UserObjectType>();
            services.AddSingleton<IdentitySchemaQuery>();
            services.AddSingleton<ISchema, IdentitySchema>();

            services.AddGraphQL(_ =>
                {
                    _.EnableMetrics = true;
                    _.ExposeExceptions = true;
                });
        }
        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDocumentStore documentStore)
        {
            app.UseDeveloperExceptionPage();
            
            app.UseGraphQL<ISchema>();

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });

            app.UseHttpsRedirection();
            app.UseMvc();
            
            app.Run(async context =>
            {
                if (context.Request.Path == "/")
                    context.Response.Redirect("/ui/playground");
            });

            documentStore.Initialize();
            documentStore.EnsureDatabaseExist();
            new EmployeeQueryIndex().Execute(documentStore);
        }
    }
}