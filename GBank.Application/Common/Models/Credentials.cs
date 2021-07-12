using GBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Common.Models
{
    public enum Role
    {
        Adm, Client
    }

    public class Credentials
    {
        public Credentials(String email, String password)
        {
            this.email = email;
            this.password = password;
        }
        public int ID { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public Role role { get; set; }

        public virtual User User { get; set; }

    }
}
