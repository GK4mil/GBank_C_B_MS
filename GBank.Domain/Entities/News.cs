using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GBank.Domain.Entities
{
    public class News
    {
        public News()
        {
            
        }
  
        public int ID { get; set; }
        public string Title{ get; set; }

        public string Content { get; set; }

        public DateTime date { get; set; }

    }
}
