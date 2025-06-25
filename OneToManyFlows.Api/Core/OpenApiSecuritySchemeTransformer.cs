using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
                        { $"{_entraIdOptions.ClientId}/.default", "Access the API" }
                    }
                }
            }
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    },
                }, []
            }
        };

        document.SecurityRequirements.Add(securityRequirement);

        document.Components = new OpenApiComponents()
        {
            SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>()
            {
                { JwtBearerDefaults.AuthenticationScheme, securitySchema }
            }
        };

        return Task.CompletedTask;
    }
}