using Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Despatch: AuditableEntity
    {
        [Required]
        public string IndentingDept { get; set; }
        [Required]
        public string ReferenceNo { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}
