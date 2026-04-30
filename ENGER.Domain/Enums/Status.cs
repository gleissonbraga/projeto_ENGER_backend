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
        SubActive = 3,
        [Description("Inactive subscription")]
        SubInactive = 4,
        [Description("Canceled subscription")]
        SubCancelled = 5,
        [Description("Subscription in test")]
        SubTest = 6,
        [Description("Expired subscription")]
        SubExpired = 7,
        [Description("Pending subscription")]
        SubPending = 8,
        [Description("In Proccess subscription")]
        SubInProcess = 9,
        [Description("Subscription Rejected")]
        SubRejected = 10,
        [Description("Subscription Refunded")]
        SubRefunded = 11,

        // Budget
        [Description("Pending budget")]
        BudPending = 12,
        [Description("Approved budget")]
        BudApproved = 13,
        [Description("Rejected budget")]
        BudRejected = 14,
        [Description("Budget in progress")]
        BudInProgress = 15,
        [Description("Completed budget")]
        BudCompleted = 16,
        [Description("Canceled budget")]
        BudCanceled = 17,
        [Description("Budget on hold")]
        BudOnHold = 18,
        [Description("Budget under review")]
        BudInReview = 19,

        [Description("Email send")]
        EmailSent = 20,
        [Description("Email not send")]
        EmailNotSent = 21,

    }
}
