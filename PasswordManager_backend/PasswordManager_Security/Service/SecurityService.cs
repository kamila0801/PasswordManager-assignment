using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasswordManager_Security.IService;
using PasswordManager_Security.Models;

namespace PasswordManager_Security.Service;

public class SecurityService : ISecurityService
  {
    private readonly IAuthUserService _authServ;
    private IConfiguration Configuration { get; set; }
    public SecurityService(IConfiguration configuration, IAuthUserService authServ)
    {
      Configuration = configuration;
      _authServ = authServ;
    }

    public JwtToken GenerateJwtToken(string username, string password)
    {
      var user = _authServ.GetUser(username);
      if (Authenticate(user, password))
      {
        var securityKey =
          new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Configuration["jwtConfig:secret"]));
        var credentials =
          new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(Configuration["jwtConfig:issuer"],
          Configuration["jwtConfig:audience"],
          null,
          expires: DateTime.Now.AddMinutes(10),
          signingCredentials: credentials);
        return new JwtToken()
        {
          Jwt = new JwtSecurityTokenHandler().WriteToken(token),
          Message = "Ok"
        }; 
      }
      throw new AuthenticationException("username or password is incorrect");
    }

    private bool Authenticate(AuthUser user, string password)
    {
      if (user == null || user.HashedPassword.Length <= 0 || user.Salt.Length <= 0) return false;
      
        var hashedPassword = HashedPassword(user.Salt, password);

        return hashedPassword.Equals(user.HashedPassword);
    }

    public string HashedPassword(byte[] userSalt, string password)
    {
      return Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password,
        salt: userSalt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8));
    }

    public AuthUser GenerateNewAuthUser(string username, string password)
    {
      // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
      var salt = GenerateSalt();

      var hashedPassword = HashedPassword(salt, password);

      AuthUser newUser = _authServ.Create(new AuthUser()
      {
        Username = username,
        HashedPassword = hashedPassword,
        Salt = salt,
      });

      return newUser;
    }

    public byte[] GenerateSalt()
    {
      var salt = new byte[128 / 8];
      using (var rngCsp = new RNGCryptoServiceProvider())
      {
        rngCsp.GetNonZeroBytes(salt);
      }

      return salt;
    }
  }