using System;
namespace rfp_dates {
    /// <summary>
    /// Contains RFP information for creating a series of RFP ical events
    /// </summary>
    public abstract class RFP {

        protected string target;
        protected DateTime dueDate;
        protected string description;
        protected string ownerEmail;

        public string Target { get { return target; } }
        public DateTime DueDate { get { return dueDate; } }
        public string Description { get { return description; } }
        public string OwnerEmail { get { return ownerEmail; } }

        public RFP() {
            target = null;
            dueDate = DateTime.MinValue;
            description = null;
            ownerEmail = null;
        }
    }
}
