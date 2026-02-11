using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Enums
{
    public enum Status
    {
        // All

        Active = 1,
        Inactive = 2,

        // Subscription
        [Description("Active subscription")]
        SubActive = 1,
        [Description("Inactive subscription")]
        SubInactive = 2,
        [Description("Canceled subscription")]
        SubCanceled = 3,
        [Description("Subscription in test")]
        SubTest = 4,
        [Description("Expired subscription")]
        SubExpired = 5,
        [Description("Pending subscription")]
        SubPending = 6,



    }
}
