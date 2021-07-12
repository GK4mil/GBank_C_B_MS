﻿using GBank.Application.Common.Interfaces;
using GBank.Application.Common.Models;
using GBank.Domain.Entities;
using GBank.Infrastructure.Persistence;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GBank.Application.Functions.Authentication.Command;

namespace GBank.Infrastructure.Services
{
     public class UserService : IUserService
    {

        private readonly GBankDbContext _context;
        private readonly ITokenService _ts;

        public UserService(GBankDbContext context, ITokenService ts)
        {
            this._context = context;
            _ts = ts;
        }

      

        public List<User> Get() =>
            _context.Users.ToList<User>();

        public User Get(int id) =>
            _context.Users.Find(id);

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public Tokens Login(Authentication authentication)
        {

            User user = _context.Users.Where(u => u.Username == authentication.username).FirstOrDefault();
                
            bool validPassword = false;

            if (user != null)
            {
                validPassword=user.password == authentication.password;
            }

            if (validPassword)
            {
                var refreshToken = _ts.GenerateRefreshToken(user);

                if (user.RefreshTokensList == null)
                    user.RefreshTokensList = new List<RefreshTokens>();

                var RT = new RefreshTokens();
                RT.RefreshToken = refreshToken.Result.Item1;
                user.RefreshTokensList.Add(RT);
                _context.SaveChanges();

                Update(user.ID.ToString(), user);
                return new Tokens
                {
                    AccessToken = _ts.GenerateAccessToken(user).Result,
                    RefreshToken = refreshToken.Result.Item2
                };
            }
            else
            {
                Console.WriteLine("Username or password incorrect");
                return null;
            }
        }

        public Tokens Refresh(Claim userClaim, String refreshTokenRequest)
        {
            refreshTokenRequest = refreshTokenRequest.Replace("Bearer ", "");
            User user = _context.Users.Where(x => x.Username == userClaim.Value).FirstOrDefault();
            RefreshTokens tokencount = _context.RefreshTokens.Where(x => x.RefreshToken == refreshTokenRequest).FirstOrDefault();

            if (user == null)
                throw new System.Exception("User doesn't exist");

            if (user.RefreshTokensList == null)
                user.RefreshTokensList = new List<RefreshTokens>();
            ////dopisac w oblusdze bazy danych




            if (tokencount != null)
            {
                var refreshToken = _ts.GenerateRefreshToken(user);

                var tok = new RefreshTokens();
                tok.RefreshToken = refreshToken.Result.Item2;

                user.RefreshTokensList.Add(tok);

                Update(user.ID.ToString(), user);


                return new Tokens
                {
                    AccessToken = _ts.GenerateAccessToken(user).Result,
                    RefreshToken = refreshToken.Result.Item2
                };
            }
            else
            {
                throw new System.Exception("Refresh token incorrect");
            }

        }

        public void Update(string id, User userIn)
        {

            User u = _context.Users.Where<User>(user => user.ID == Int32.Parse(id)).ToArray<User>()[0];

            u.password = userIn.password;

            u.RefreshTokensList = userIn.RefreshTokensList;

            u.lastname = userIn.lastname;
            u.Username = userIn.Username;
            _context.SaveChanges();
        }

        public void Remove(User userIn)
        {
            //context.Users.DeleteOne(user => user.Id == userIn.Id);
            _context.Users.Remove(_context.Users.Where<User>(user => user.ID == userIn.ID).ToArray<User>()[0]);
            _context.SaveChangesAsync();
        }

        public void Remove(string id)
        {
            _context.Users.Remove((_context.Users.Where<User>(user => user.ID == Int32.Parse(id))).ToArray<User>()[0]);
            _context.SaveChangesAsync();
        }
    }
}