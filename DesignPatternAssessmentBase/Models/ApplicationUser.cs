using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternAssessmentBase.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [InverseProperty("Developers")]
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        [InverseProperty("Developers")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

        [InverseProperty("TaskOwners")]
        public virtual ICollection<Ticket> OwnedTickets { get; set; } = new HashSet<Ticket>();

        [InverseProperty("TaskWatchers")]
        public virtual ICollection<Ticket> WatchedTickets { get; set; } = new HashSet<Ticket>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
