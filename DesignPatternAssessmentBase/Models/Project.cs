using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternAssessmentBase.Models
{
    public class Project
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; } = new HashSet<Ticket>();

        [InverseProperty("Projects")]
        public virtual ICollection<ApplicationUser> Developers { get; set; } = new HashSet<ApplicationUser>();
    }
}
