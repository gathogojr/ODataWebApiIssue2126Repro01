using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ODataWebApiIssue2126Repro01.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IDictionary<string, object> DynamicProperties { get; set; }
    }
}
