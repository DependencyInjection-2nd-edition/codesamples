using System;

namespace Ploeh.Samples.Commerce.SqlDataAccess
{
    // This entity is only for technical purposes, and is therefore places in this data access library.
    public class AuditEntry
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime TimeOfExecution { get; set; }
        public string Operation { get; set; }
        public string Data { get; set; }
    }
}