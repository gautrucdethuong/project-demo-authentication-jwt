﻿using Microsoft.IdentityModel.Tokens;
using RoleBasedAuthorization.Data;
using RoleBasedAuthorization.Model;
using RoleBasedAuthorization.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace RoleBasedAuthorization.Reponsitory
{
    public class AuthenReponsitory : IAuthenticateService
    {
        private readonly DBContext db;

        public AuthenReponsitory(DBContext dbContext)
        {
            db = dbContext;
        }
        
        // generation token
        public User Login(string username, string password)
        {

            List<User> users = db.Users.ToList();
            var user = users.SingleOrDefault(x => x.user_username == username && x.user_password == password);


            // return null if user not found
            if (user == null)
            {
                return null;
            }

            GenerateJwtToken(user);
            GenerateRefreshToken(user);
            return user;
            
        }

        public User GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // We get our secret from the appsettings
            var key = Encoding.UTF8.GetBytes(Constant.Secret);

            // we define our token descriptor
            // so it could contain their id, name, email the good part is that these information
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.user_id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.user_username),
                    new Claim(JwtRegisteredClaimNames.Email, user.user_email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
                Expires = DateTime.UtcNow.AddHours(1),

                // here we are adding the encryption alogorithim information which will be used to decrypt our token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Audience = Constant.Audiance,
                Issuer = Constant.Issuer
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            user.user_token = jwtTokenHandler.WriteToken(token);
            user.user_exprires_at = (DateTime)tokenDescriptor.Expires;

            return user;
        }

        //generation refresh token
        public string GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken()
            {
                JwtId = Guid.NewGuid().ToString(),
                IsUsed = false,
                IsRevoked = false,
                UserId = user.user_id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                Token = RandomStringRefresh(35) + Guid.NewGuid(),

            };
            user.user_refreshToken = refreshToken.Token;
            user.user_refresh_token_expires_at = refreshToken.ExpiryDate;

            db.RefreshTokens.Add(refreshToken);
            db.SaveChanges();
            return user.user_refreshToken;
        }

        // generation random string refresh
        private string RandomStringRefresh(int lenght)
        {
            var random = new Random();
            var chars = Constant.randomString;
            return new string(Enumerable.Repeat(chars, lenght).Select(x => x[random.Next(x.Length)]).ToArray());
        }

    }
}
