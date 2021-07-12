using GBank.Application.Common.Models;
using GBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace GBank.Application.Common.Interfaces
{
    public interface IUserService
    {
        public void Remove(string id);
        public void Remove(User userIn);
        public void Update(string id, User userIn);
        public Tokens Refresh(Claim userClaim, String refreshClaim);
        public Tokens Login(Authentication authentication);
        public List<User> Get();

        public User Get(int id);

        public User Create(User user);

    }
}
