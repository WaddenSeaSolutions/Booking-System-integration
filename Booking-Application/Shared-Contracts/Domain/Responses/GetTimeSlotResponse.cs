﻿using Shared_Contracts.Domain.DTOs;

namespace Shared_Contracts.Domain.Responses
{
    public class GetTimeSlotResponse
    {
        public string RequestId { get; set; }
        public List<TimeSlotData> Times { get; set; } = new ();
    }
}
