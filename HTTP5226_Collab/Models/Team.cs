using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HTTP5226_Collab.Models
{
    public class Team
    {
        [Key] public int TeamId { get; set; }

        public string TeamName {  get; set; }

        //Many Employees Associated With A Team
        public virtual ICollection<Employee> Employees { get; set; }

        //A Team Is Associated With One Department
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; } //Navigation Property
        public int DepartmentId { get; set; } //Foreign Key Property

    }

    public class TeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DepartmentId { get; set; }
        public Department Departments { get; set; } // Navigation Property
    }
}