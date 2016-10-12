using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebUI.Models
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            this.Themes = new HashSet<Theme>();
        }
        public int ID { get; set; }
        public string Criteria { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
        public virtual ICollection<Email> Emails { get; set; }

    }
}
