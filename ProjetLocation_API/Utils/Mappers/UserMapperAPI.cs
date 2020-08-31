﻿using Microsoft.AspNetCore.Server.IIS.Core;
using Api = ProjetLocation.API.Models.User;
using Dal = DAL.Models;

namespace ProjetLocation.API.Utils.Mappers
{
    internal static class UserMapperAPI
    {
        internal static Api.User DALUserToAPI(this Dal.User user)
        {
            if (!(user is null))
            {
                return new Api.User()
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Birthdate = user.Birthdate,
                    Email = user.Email,
                    Passwd = user.Passwd,
                    Street = user.Street,
                    Number = user.Number,
                    Box = user.Box,
                    PostCode = user.PostCode,
                    City = user.City,
                    Phone1 = user.Phone1,
                    Phone2 = user.Phone2,
                    Picture = user.Picture,
                    IsActive = user.IsActive,
                    IsBanned = user.IsBanned,
                    RoleId = user.RoleId,
                    Token = user.Token
                };
            }
            else
                return null;
        }

        internal static Dal.User APIUserToDAL(this Api.User user)
        {
            return new Dal.User()
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Passwd = user.Passwd,
                Street = user.Street,
                Number = user.Number,
                Box = user.Box,
                PostCode = user.PostCode,
                City = user.City,
                Phone1 = user.Phone1,
                Phone2 = user.Phone2,
                Picture = user.Picture,
                IsActive = user.IsActive,
                IsBanned = user.IsBanned,
                RoleId = user.RoleId,
                Token = user.Token
            };
        }

        internal static Api.UserLogin DALUserLoginToAPI(this Dal.User user)
        {
            return new Api.UserLogin()
            {
                Email = user.Email,
                Passwd = user.Passwd
            };
        }

        internal static Dal.User APIUserLoginToDAL(this Api.UserLogin user)
        {
            return new Dal.User()
            {
                Email = user.Email,
                Passwd = user.Passwd
            };
        }

        internal static Api.UserRegister DALUserRegisterToAPI(this Dal.User user)
        {
            return new Api.UserRegister()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Passwd = user.Passwd
            };
        }

        internal static Dal.User APIUserRegisterToDAL(this Api.UserRegister user)
        {
            return new Dal.User()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Passwd = user.Passwd
            };
        }

        internal static Api.UserInfo DALUserInfoToAPI(this Dal.User user)
        {
            return new Api.UserInfo()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Birthdate = user.Birthdate,
                Street = user.Street,
                Number = user.Number,
                Box = user.Box,
                PostCode = user.PostCode,
                City = user.City,
                Phone1 = user.Phone1,
                Phone2 = user.Phone2,
                Picture = user.Picture
            };
        }

        internal static Dal.User APIUserInfoToDAL(this Api.UserInfo user)
        {
            return new Dal.User()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Birthdate = user.Birthdate,
                Street = user.Street,
                Number = user.Number,
                Box = user.Box,
                PostCode = user.PostCode,
                City = user.City,
                Phone1 = user.Phone1,
                Phone2 = user.Phone2,
                Picture = user.Picture
            };
        }

        internal static Api.UserPassword DALUserPasswordToAPI(this Dal.User user)
        {
            return new Api.UserPassword()
            {
                Passwd = user.Passwd
            };
        }

        internal static Dal.User APIUserPasswordToDAL(this Api.UserPassword user)
        {
            return new Dal.User()
            {
                Passwd = user.Passwd
            };
        }
    }
}
