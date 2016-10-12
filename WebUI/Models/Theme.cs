using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebUI.Models
{
    public class Theme
    {
        public Theme()
        {
            this.SearchCriterias = new HashSet<SearchCriteria>();
        }
        public int ID { get; set; }
        public virtual ICollection<SearchCriteria> SearchCriterias { get; set; }
        public string Title { get; set; }

    }
}