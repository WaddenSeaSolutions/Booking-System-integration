using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Contracts.Domain.Requests
{
    public class GetTimeSlotRequest
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
    }
}
