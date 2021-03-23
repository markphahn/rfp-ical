using System;

namespace rfp_dates {
    internal class CurrentRFP : RFP {

        public CurrentRFP() : base() {
            target = "Example Target RFP";

            description = "RFP for Example";
            ownerEmail = "owner@example.com";

            var currentTime = new DateTime (2000, 12, 31);
        }
    }
}