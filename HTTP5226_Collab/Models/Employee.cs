using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HTTP5226_Collab.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId {  get; set; }
    }
}