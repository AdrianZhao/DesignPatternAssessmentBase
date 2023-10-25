using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DesignPatternAssessmentBase.Models
{
    public enum Priority
    {
        low,
        medium,
        high
    }
    public enum RequestType
    {
        Information,
        Change
    }
    public abstract class Ticket
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

        public virtual int Assign {  get; set; }
        public virtual int Resolve { get; set; }
        protected IAssigne _assignBehaviour;
        protected IResolve _resolveBehaviour; 
        public virtual Ticket GetType(object input)
        {
            return this;
        }
        public virtual void SetAssign(int assigned)
        {
            Assign = assigned;
            _assignBehaviour.Assign(assigned);
        }
        public virtual int GetAssign()
        {
            return Assign;
        }
        public virtual void SetResolve(int resolved)
        {
            Resolve = resolved;
            _resolveBehaviour.Resolve(resolved);
        }
        public virtual int GetResolve()
        {
            return Resolve;
        }
        public virtual void CalculateTime()
        {
            Console.WriteLine($"Assign left hour: {Assign}, Resolve left hour: {Resolve}."); 
        }
    }   
    public abstract class TicketFactory
    {
        public Ticket makeTicket(object input)
        {
            Ticket ticket;
            ticket = CreateTicket(input);
            return ticket;
        }
        protected abstract Ticket CreateTicket(object input);
    }
    public class BugReportFactory : TicketFactory
    {
        protected override Ticket CreateTicket(object input)
        {
            Ticket ticket;
            if (input is string)
            {
                ticket = new BugReport().GetType(input);
            }
            else
            {
                throw new InvalidOperationException();
            }
            return ticket;
        }
    }
    public class RequestFactory : TicketFactory
    {
        protected override Ticket CreateTicket(object input)
        {
            Ticket ticket;
            if (input is RequestType)
            {
                ticket = new Request();
            }
            else
            {
                throw new InvalidOperationException();
            }
            return ticket;
        }
    }
    public class BugReport : Ticket
    {
        public override Ticket GetType(object input)
        {
            Ticket ticket;
            if (input is string && (string)input == "Error Codes")
            {
                ticket = new ErrorCodesBugReport();
            }
            else if (input is string && (string)input == "Error Logs")
            {
                ticket = new ErrorLogsBugReport();
            }
            else
            {
                throw new InvalidOperationException();
            }
            return ticket;
        }
        public BugReport()
        {
            _assignBehaviour = new SetAssignBehaviour();
            _resolveBehaviour = new SetResolveBehaviour();
        }
    }
    public class ErrorCodesBugReport : BugReport
    {

    }
    public class ErrorLogsBugReport : BugReport
    {

    }
    public class Request : Ticket
    {
        public override Ticket GetType(object input)
        {
            Ticket ticket;
            if (input is RequestType && (RequestType)input == RequestType.Information)
            {
                ticket = new InformationRequest();
            }
            else if (input is RequestType && (RequestType)input == RequestType.Change)
            {
                ticket = new ChangeRequest();
            }
            else
            {
                throw new InvalidOperationException();
            }
            return ticket;
        }
        public Request()
        {
            _assignBehaviour = new SetAssignBehaviour();
            _resolveBehaviour = new SetResolveBehaviour();
        }
    }
    public class InformationRequest : Request
    {

    }
    public class ChangeRequest : Request
    {

    }
    public interface IAssigne
    {
        void Assign(int assign);
    }
    public interface IResolve
    {
        void Resolve(int resolve);
    }
    public class SetAssignBehaviour: IAssigne
    {
        public void Assign(int assign)
        {
            Console.WriteLine($"The ticket assign hours left is {assign}.");
        }
    }
    public class SetResolveBehaviour : IResolve
    {
        public void Resolve(int resolve)
        {
            Console.WriteLine($"The ticket resolve hours left is {resolve}.");
        }
    }
    public abstract class ModifyHourDecorator : Ticket
    {
        public Ticket Ticket { get; set; }
        public abstract override void CalculateTime();
        public ModifyHourDecorator(Ticket ticket)
        {
            Ticket = ticket;
        }
    }
    public class TypeBugReport : ModifyHourDecorator
    {
        public TypeBugReport(Ticket ticket) : base(ticket) { }
        public override void CalculateTime()
        {
            Assign = Ticket.GetAssign() * 2;
            Resolve = Ticket.GetResolve() * 2;
            Console.WriteLine($"Assign left hour: {Assign}, Resolve left hour: {Resolve}.");
        }
    }
    public class WhiteGloveClient : ModifyHourDecorator
    {
        public WhiteGloveClient(Ticket ticket) : base(ticket) { }
        public override void CalculateTime()
        {
            Assign = Ticket.GetAssign() * 3;
            Resolve = Ticket.GetResolve() * 3;
            Console.WriteLine($"Assign left hour: {Assign}, Resolve left hour: {Resolve}.");
        }
    }
    public class BacklogReissue : ModifyHourDecorator
    {
        public BacklogReissue(Ticket ticket) : base(ticket) { }
        public override void CalculateTime()
        {
            Assign = Ticket.GetAssign() + 100;
            Resolve = Ticket.GetResolve() + 100;
            Console.WriteLine($"Assign left hour: {Assign}, Resolve left hour: {Resolve}.");
        }
    }
}
