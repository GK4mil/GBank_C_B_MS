using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Domain.Entities
{
    public class RefreshTokens
    {

        public int ID { get; set; }

        public String RefreshToken { get; set; }

        public User User { get; set; }

    }
}
