using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HTTP5226_Collab.Models;

namespace HTTP5226_Collab.Controllers
{
    public class DepartmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// PROVIDES ALL DEPARTMENT INFORMATION
        /// </summary>
        /// 
        /// <returns>
        /// ALL DEPARTMENT INFO IN THE DATABASE
        /// </returns>
        // GET: api/DepartmentData/ListDepartments
        [HttpGet]
        public IEnumerable<DepartmentDto> ListDepartments()
        {
            List<Department> Departments = db.Departments.ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(d => DepartmentDtos.Add(new DepartmentDto()
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName
            }));

            return DepartmentDtos;
        }

        /// <summary>
        /// PROVIDES ALL DEPARTMENT INFORMATION, associated with a DepartmentId
        /// </summary>
        /// <param name="DepartmentId">DepartmentId</param>
        /// <returns>
        /// All DEPARTMENT information in the database, associated with a DepartmentId
        /// </returns>
        // GET: api/DepartmentData/FindDepartment/5
        [ResponseType(typeof(Department))]
        [HttpGet]
        public IHttpActionResult FindDepartment(int id)
        {
            Department Department = db.Departments.Find(id);
            if (Department == null)
            {
                return NotFound();
            }

            DepartmentDto DepartmentDto = new DepartmentDto()
            {
                DepartmentId = Department.DepartmentId,
                DepartmentName = Department.DepartmentName
            };

            return Ok(DepartmentDto);
        }

        /// <summary>
        /// Updates DEPARTMENT information based on DepartmentId
        /// </summary>
        /// <param name="DepartmentId">DepartmentId</param>
        /// <param name="department">Department Model</param>
        /// <example>
        /// POST: api/DepartmentData/UpdateDepartment/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PutDepartment(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // Create New Department Based on Form Data
        // POST: api/DepartmentData/AddDepartment
        [ResponseType(typeof(Department))]
        [HttpPost]
        public IHttpActionResult AddDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.DepartmentId }, department);
        }

        // Delete Department Based on DepartmentId
        // DELETE: api/DepartmentData/DeleteDepartment/5
        [ResponseType(typeof(Department))]
        [HttpPost]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentId == id) > 0;
        }
    }
}