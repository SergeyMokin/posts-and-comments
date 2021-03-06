﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Company.PostsAndCommentsModels;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsServices.Extensions;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Company.PostsAndCommentsServices.Services
{
    public class AuthorizeService: IAuthorizeService
    {
        public int GetUserId(ClaimsPrincipal user)
        {
            var isParsed = int.TryParse(user.Claims.FirstOrDefault()?.Value, out var userId);
            return isParsed ? userId : throw new PacUnauthorizedAccessException();
        }

        public UserToken GetToken(User user, IEmailService emailService = null)
        {
            if (user == null || !user.IsValid())
            {
                throw new PacInvalidModelException();
            }

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromDays(AuthOptions.LifeTime));

            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            emailService?.Send(new Mail
            {
                Subject = "Login",
                Text = $"You logged in by this email at {DateTime.Now.ToString(CultureInfo.InvariantCulture)}.",
                RecipientEmail = user.Email
            });

            return new UserToken
            {
                User = user.ToView(),
                Token = "Bearer " + encodedJwt,
                DateExpires = expires
            };
        }

        public string HashPassword(string password)
        {
            if (password == null)
            {
                throw new PacInvalidModelException();
            }

            byte[] salt;
            byte[] buffer2;

            using (var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }

            var dst = new byte[0x31];

            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);

            return Convert.ToBase64String(dst);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (password == null || hashedPassword == null)
            {
                throw new PacInvalidModelException();
            }

            var src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }

            var dst = new byte[0x10];

            Buffer.BlockCopy(src, 1, dst, 0, 0x10);

            var buffer3 = new byte[0x20];

            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            byte[] buffer4;
            using (var bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return buffer3.SequenceEqual(buffer4);
        }
    }
}
