using Microsoft.AspNetCore.Http;

namespace AccountingForExpirationDates.HelperClasses
{
    public class Status
    {
        public Status(int statusCode, string description) 
        {
           StatusCode = statusCode;
            Description = description;
        }
        public int StatusCode { get; set; }
        public string Description { get; set; }
    }
}
