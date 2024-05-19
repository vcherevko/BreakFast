﻿using System.Reflection;
using Microsoft.OpenApi.Models;

namespace BreakFast.Catalog.API.Extentsions;

public partial class Extensions
{
	public static IHostApplicationBuilder AddOpenApi(this IHostApplicationBuilder builder)
	{
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Catalog API",
				Version = "v1",
				Description = "An API to perform Catalog operations",
				Contact = new OpenApiContact
				{
					Email = "breakfast.delivery@mail.io",
					Name = "BreakFast Delivery",
					Url = new Uri("https://breakfast-delivery.io")
				},
				License = new OpenApiLicense
				{
					Name = "BreakFast Delivery License",
					Url = new Uri("https://breakfast-delivery.io/licence")
				},
				TermsOfService = new Uri("https://breakfast-delivery.io/terms")
			});
		});

		return builder;
	}

	public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API v1");
			});
		}

		return app;
	}
}
