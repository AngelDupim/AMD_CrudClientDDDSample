using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.Auth;
using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Enum;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper.Configuration;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AMD_CrudClientDDDSample.Domain.Handlers
{
    public class AuthHandler : Notifiable, IAuthHandler, IHandler<GetAuthCommand>
    {
        private IUserRepository _userRepository;
        public AuthHandler(IUserRepository userRepository) => _userRepository = userRepository;
        public async Task<ICommandResult> Handle(GetAuthCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, AuthMessage.Validate, command.Notifications);

            var userEntity = await _userRepository.GetByField(GenericParameterHelper.SetParameter("Name", command.Name));

            if (userEntity is not null)
            {
                var user = userEntity.Where(u => u.IsActive).FirstOrDefault();
                if (user is not null && user.Password.Equals(command.Password))
                {
                    var authEntity = GenerateToken(user.Name, user.Role);
                    return new GenericCommandResult(true, AuthMessage.AuthSucess, authEntity);
                }
                else
                    return new GenericCommandResult(false, AuthMessage.UserOrPasswordError, null);
            }
            else
                return new GenericCommandResult(false, AuthMessage.UserOrPasswordError, null);
        }

        private Auth GenerateToken(string user, string roleEnum)
        {
            var secret = Encoding.ASCII.GetBytes(AppSettingsHelper.GetConfigurationAppSettings("SettingsJWT", "Secret"));

            ClaimsIdentity identity = new(
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.Sub,user),
                        new Claim(ClaimTypes.Role, roleEnum)
                }
            );

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddHours(2);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "AMD",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature),
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return new(true, createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                token,
                "OK");
        }
    }
}