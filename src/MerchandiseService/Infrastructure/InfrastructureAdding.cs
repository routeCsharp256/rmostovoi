﻿using MediatR;
using MerchandiseService.Application.Commands.MerchItemAggregate;
using MerchandiseService.Infrastructure.HttpFilters;
using MerchandiseService.Infrastructure.Interceptors;
using MerchandiseService.Infrastructure.StartupFilters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MerchandiseService.Infrastructure
{
    public static class InfrastructureAdding
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            return builder
                .AddHttpApi()
                .AddGrpcApi()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                    services.AddMediatR(typeof(IssueMerchItemCommandHandler).Assembly);
                    services.AddSwagger(false);
                });
        }

        public static IHostBuilder AddHttpApi(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); });
            });

            return builder;
        }

        public static IHostBuilder AddGrpcApi(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
            });

            return builder;
        }
    }
}