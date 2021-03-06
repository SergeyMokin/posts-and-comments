﻿using System.Text.RegularExpressions;

namespace Company.PostsAndCommentsModels.Extensions
{
    public static class Validator
    {
        private const string PasswordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        private const string EmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public static bool IsPassword(this string pass) => pass != null && Regex.IsMatch(pass, PasswordPattern);

        public static bool IsEmail(this string email) => email != null && Regex.IsMatch(email, EmailPattern);
    }
}