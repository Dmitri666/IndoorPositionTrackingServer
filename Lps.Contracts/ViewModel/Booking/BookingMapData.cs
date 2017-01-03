namespace Lps.Contracts.ViewModel.Booking
{
    using System;
    using System.Collections.Generic;

    public class ReservationData
    {        
        public string selected_date { get; set; }
      
        public int selected_capacity { get; set; }

        public List<int> capacities { get; set; }

        public int reservation_allowed_advance { get; set; }

        //public List<DateTime> reservation_restricted_weekdays { get; set; }

        //public List<DateTime> reservation_restricted_days { get; set; }

        //public List<DateTime> reservation_allowed_days { get; set; }

        public List<TimeObject> times { get; set; }
    }
}
