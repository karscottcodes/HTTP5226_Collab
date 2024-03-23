using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HTTP5226_Collab.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentStatus { get; set; }
        public int AssignmentValue { get; set; }

        //An Assignment Can Have One Employee

        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }
    }
}