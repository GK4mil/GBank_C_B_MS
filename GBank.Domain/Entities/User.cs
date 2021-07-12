using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace GBank.Domain.Entities
{
    public class User
    {
        public User()
        {
            this.RefreshTokensList = new Collection<RefreshTokens>();
            this.Bills = new Collection<Bill>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        [JsonIgnore]
        public virtual ICollection<RefreshTokens> RefreshTokensList { get; set; }

        [JsonIgnore]
        public virtual ICollection<Bill> Bills { get; set; }

        

    }
}
