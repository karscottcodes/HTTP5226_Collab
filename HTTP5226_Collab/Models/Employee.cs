using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HTTP5226_Collab.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId {  get; set; }
        public string EmployeeName { get; set; }

        //An Employee Can Belong To One Team
        [ForeignKey("Teams")]
        public int TeamId {  get; set; }
        public virtual Team Teams { get; set; }

        //An Employee Can Have Many Assignments
        //public ICollection<Assignment> Assignments { get; set; }
    }
}