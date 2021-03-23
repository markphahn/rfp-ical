using System;

namespace rfp_dates {
    internal class CurrentRFP : RFP {

        public CurrentRFP() : base() {
            target = "Example Target RFP";

            description = "RFP for Example";
            ownerEmail = "owner@example.com";

            var currentTime = DateTime.Now.AddDays (3);
            dueDate = new DateTime (currentTime.Year, currentTime.Month, currentTime.Day);
        }
    }
}