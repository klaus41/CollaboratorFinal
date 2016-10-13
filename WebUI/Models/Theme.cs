using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebUI.Models
{
    [DataContract(IsReference = true)]
    [Table("Theme")]
    public class Theme
    {
        public Theme()
        {
            this.SearchCriterias = new HashSet<SearchCriteria>();
        }
        [Key]
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public virtual ICollection<SearchCriteria> SearchCriterias { get; set; }
        [DataMember]
        public virtual  ICollection<Email> Emails { get; set; }
        [DataMember]
        public string Title { get; set; }

    }
}