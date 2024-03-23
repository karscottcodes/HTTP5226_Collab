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
    public class TeamDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// PROVIDES ALL Team INFORMATION
        /// </summary>
        /// 
        /// <returns>
        /// ALL Team INFO IN THE DATABASE
        /// </returns>
        // GET: api/TeamData/ListTeams
        [HttpGet]
        public IEnumerable<TeamDto> ListTeams()
        {
            List<Team> Teams = db.Teams.ToList();
            List<TeamDto> TeamDtos = new List<TeamDto>();

            Teams.ForEach(d => TeamDtos.Add(new TeamDto()
            {
                TeamId = d.TeamId,
                TeamName = d.TeamName
            }));

            return TeamDtos;
        }

        /// <summary>
        /// PROVIDES ALL Team INFORMATION, associated with a TeamId
        /// </summary>
        /// <param name="TeamId">TeamId</param>
        /// <returns>
        /// All Team information in the database, associated with a TeamId
        /// </returns>
        // GET: api/TeamData/FindTeam/5
        [ResponseType(typeof(Team))]
        [HttpGet]
        public IHttpActionResult FindTeam(int id)
        {
            Team Team = db.Teams.Find(id);
            if (Team == null)
            {
                return NotFound();
            }

            TeamDto TeamDto = new TeamDto()
            {
                TeamId = Team.TeamId,
                TeamName = Team.TeamName
            };

            return Ok(TeamDto);
        }

        /// <summary>
        /// Updates Team information based on TeamId
        /// </summary>
        /// <param name="TeamId">TeamId</param>
        /// <param name="Team">Team Model</param>
        /// <example>
        /// POST: api/TeamData/UpdateTeam/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PutTeam(int id, Team Team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Team.TeamId)
            {
                return BadRequest();
            }

            db.Entry(Team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // Create New Team Based on Form Data
        // POST: api/TeamData/AddTeam
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult AddTeam(Team Team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(Team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Team.TeamId }, Team);
        }

        // Delete Team Based on TeamId
        // DELETE: api/TeamData/DeleteTeam/5
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult DeleteTeam(int id)
        {
            Team Team = db.Teams.Find(id);
            if (Team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(Team);
            db.SaveChanges();

            return Ok(Team);
        }

        /// <summary>
        /// Provides ALL TEAMS Associated with a DepartmentId
        /// </summary>
        /// <returns>
        /// all TEAM INFO, that is associated with a specific DepartmentId
        /// </returns>
        /// <param name="DepartmentId">DepartmentId</param>
        /// <example>
        /// GET: api/TeamData/ListTeamsForDepartment/2
        /// </example>
        /// 

        [HttpGet]
        [ResponseType(typeof(TeamDto))]

        public IHttpActionResult ListTeamsForDepartment(int id)
        {
            List<Team> Teams = db.Teams.Include(t => t.Department).Where(t => t.TeamId == id).ToList();
            List<TeamDto> TeamDtos = new List<TeamDto>();

            Teams.ForEach(t => TeamDtos.Add(new TeamDto()
            {
                TeamId = t.TeamId,
                TeamName = t.TeamName
            }));


            return Ok(TeamDtos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.TeamId == id) > 0;
        }
    }
}