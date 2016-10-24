using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPI.Models
{
    [DataContract(IsReference = true)]
    [Table("EmailAccounts")]
    public class EmailAccount
    {
        [Key]
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}