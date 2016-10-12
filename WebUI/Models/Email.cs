using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Models
{
     public class Email
    {
        public Email()
        {
            this.SearchCriteria = new HashSet<SearchCriteria>();
        }
        public string ID { get; set; }
        public string Sender { get; set; }
        public string Recipiant { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public virtual ICollection<SearchCriteria> SearchCriteria{ get; set; }
    }
}