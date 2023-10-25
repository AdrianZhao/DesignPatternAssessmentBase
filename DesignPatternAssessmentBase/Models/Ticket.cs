using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternAssessmentBase.Models
{
    public enum Priority
    {
        low,
        medium,
        high
    }

    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public bool Completed { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
        [MaxLength(200, ErrorMessage = "Name must be less than or equal to 200 characters.")]
        public string Name { get; set; }

        [Required]
        [Range(1, 999)]
        public int Hours { get; set; }
        public Priority Priority { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [InverseProperty("Tickets")]
        public ICollection<ApplicationUser> Developers { get; set; } = new HashSet<ApplicationUser>();

        [InverseProperty("OwnedTickets")]
        public virtual ICollection<ApplicationUser> TaskOwners { get; set; } = new HashSet<ApplicationUser>();

        [InverseProperty("WatchedTickets")]
        public virtual ICollection<ApplicationUser> TaskWatchers { get; set; } = new HashSet<ApplicationUser>();
        public virtual ICollection<Comment> Comment { get; set; } = new HashSet<Comment>();
    }
}
