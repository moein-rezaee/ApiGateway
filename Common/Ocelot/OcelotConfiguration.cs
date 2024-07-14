using System.IdentityModel.Tokens.Jwt;
<<<<<<< HEAD
using Microsoft.IdentityModel.Tokens;
=======
using System.Security.Claims;
using CustomResponce.Models;
using Ocelot.Errors;
>>>>>>> b04a80f980d7b4acd248f91f06fde1e1a51ffdad
using Ocelot.Middleware;

namespace ApiGateway.Common.Ocelot
{
    public class OcelotConfiguration
    {
        public static OcelotPipelineConfiguration‍ GetInstance(ILogger logger)
        {
            return new OcelotPipelineConfiguration‍
            {
                PreErrorResponderMiddleware = async (context, next) =>
                {
                    await next.Invoke();

                    // string? token = context.Request.Headers["Token"];
                    // var isValid = JWTValidator.ValidateToken(token, secretKey);
                    // if (isValid)
                    // {
                    //     await next.Invoke();
                    // }
                    // else
                    // {
                    //     var result = CustomErrors.InvalidToken();
                    //     context.Response.StatusCode = result.StatusCode;
                    //     await context.Response.WriteAsJsonAsync(result);
                    // }
                },
                AuthorizationMiddleware = async (context, next) =>
                {
                    string? token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
                    if (token != null)
                    {
                        JwtSecurityTokenHandler handler = new();
                        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
                        string? userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                        string? roleIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "roleId")?.Value;
                        string? organizationIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "organizationId")?.Value;

                        if (userIdClaim != null)
                            context.Items["userId"] = userIdClaim;

                        if (roleIdClaim != null)
                            context.Items["roleId"] = roleIdClaim;

                        if (organizationIdClaim != null)
                            context.Items["organizationId"] = organizationIdClaim;
                    }

                    // string? token = context.Request.Headers["Token"];

                    // if (token != null)
                    // {
                    //     string base64EncodedData = token.Split(".")[1];
                    //     // var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                    //     // var data = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                    // }
                    await next.Invoke();
                }
            };
        }
    }
}