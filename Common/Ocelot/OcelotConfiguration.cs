using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
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
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
                        string? userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                        string? userRoleIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userRoleId")?.Value;
                        string? userOrganizationIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userOrganizationId")?.Value;

                        if (!userIdClaim.IsNullOrEmpty())
                            context.Items["userId"] = userIdClaim;

                        if (!userRoleIdClaim.IsNullOrEmpty())
                            context.Items["userRoleId"] = userRoleIdClaim;

                        if (!userOrganizationIdClaim.IsNullOrEmpty())
                            context.Items["userOrganizationId"] = userOrganizationIdClaim;
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