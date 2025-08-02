using HotChocolate.Types.NodaTime;
using HotChocolate.Types.Pagination;
using RentlystBackEnd.Domain;

namespace RentlystBackEnd.Presentation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraph(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .RegisterDbContextFactory<AppDbContext>()
            .ModifyPagingOptions(opts =>
            {
                opts.InferConnectionNameFromField = false;
                opts.IncludeTotalCount = true;
                opts.DefaultPageSize = 20;
                opts.MaxPageSize = 20;
                opts.RequirePagingBoundaries = true;
            })
            .AddTypes()
            .AddFiltering()
            .AddProjections()
            // .AddSpatialFiltering()
            // .AddSpatialProjections()
            .AddMutationConventions()
            // .AddErrorFilter<ErrorFilter>()
            // .AddDiagnosticEventListener<MyExecutionDiagnosticEventListener>()
            // .AddDiagnosticEventListener<MyDataLoaderEventListener>()
            .AddGlobalObjectIdentification()
            .AddAuthorization()
            .AddType(new TimeSpanType(TimeSpanFormat.DotNet))
            .AddType<UploadType>();


        return services;
    }
}
