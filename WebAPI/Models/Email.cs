using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{

    [DataContract(IsReference = true)]
    [Table("Email")]
    public class Email
    {
        public Email()
        {
            this.SearchCriteria = new HashSet<SearchCriteria>();
        }
        [Key]
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public string Recipiant { get; set; }
        [DataMember]
        public DateTime ReceivedDate { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string BodyText { get; set; }
        [DataMember]
        public virtual ICollection<SearchCriteria> SearchCriteria{ get; set; }
    }
}