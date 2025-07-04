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
            .SetPagingOptions(new PagingOptions
            {
                InferConnectionNameFromField = false,
                IncludeTotalCount = true,
                DefaultPageSize = 20,
                MaxPageSize = 20,
                RequirePagingBoundaries = true,
            })
            .AddTypes()
            .AddFiltering()
            .AddProjections()
            // .AddSpatialFiltering()
            // .AddSpatialProjections()
            .AddMutationConventions()
            .RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
            // .AddErrorFilter<ErrorFilter>()
            // .AddDiagnosticEventListener<MyExecutionDiagnosticEventListener>()
            // .AddDiagnosticEventListener<MyDataLoaderEventListener>()
            .AddGlobalObjectIdentification()
            .AddAuthorization()
            // .AddType(new TimeSpanType(TimeSpanFormat.DotNet))
            .AddType<OffsetDateTimeType>()
            .AddType<LocalDateType>()
            .AddType<LocalTimeType>()
            .AddType<UploadType>();



        return services;
    }
}
