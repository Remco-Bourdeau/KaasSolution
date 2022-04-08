using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using KaasService.Models;

namespace KaasService.Models
{
    [DataContract(Name ="kaas", Namespace ="")]
    public class Kaas
    {
        [DataMember(Name ="id", Order =1)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =1)]
        [DataMember(Name ="naam", Order =2)]
        public string Naam { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DataMember(Name ="soort", Order =3)]
        public string Soort { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DataMember(Name ="smaak", Order =4)]
        public string Smaak { get; set; }
    }
}
