using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

namespace OneToManyFlows.Api.Core;

public class OpenApiSecuritySchemeTransformer(IOptions<EntraIdOptions> options) : IOpenApiDocumentTransformer
{
    readonly EntraIdOptions _entraIdOptions = options.Value;

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var securitySchema = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{_entraIdOptions.Instance}/{_entraIdOptions.TenantId}/oauth2/v2.0/authorize"),
                    TokenUrl = new Uri($"{_entraIdOptions.Instance}/{_entraIdOptions.TenantId}/oauth2/v2.0/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        //{ $"{_entraIdOptions.ClientId}/.default", "Access the API" },
                        { "api://e366a756-46f6-4f4d-bf41-3d37d5b14107/SzubiCustom.Read", "Read your profile information" },
                    }
                }
            }
        };

        document.Security ??= [];
        document.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme)] = []
        });

        //var securityRequirement = new OpenApiSecurityRequirement
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Id = JwtBearerDefaults.AuthenticationScheme,
        //                Type = ReferenceType.SecurityScheme
        //            },
        //        }, []
        //    }
        //};

        //document.SecurityRequirements.Add(securityRequirement);

        var securitySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            [JwtBearerDefaults.AuthenticationScheme] = securitySchema
        };


        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = securitySchemes;

        //document.Components = new OpenApiComponents()
        //{
        //    SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>()
        //    {
        //        { JwtBearerDefaults.AuthenticationScheme, securitySchema }
        //    }
        //};

        return Task.CompletedTask;
    }
}