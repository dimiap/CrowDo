using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Entities;
using CrowDo.Repositories;
using CrowDo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrowDo.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Databases _context;
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(Databases context, ILogger<ValuesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET api/values
        [HttpGet("excel/users")]
        public void PostUsersFromExcelToDB()
        {
            using(var db = new CrowDoDB())
            {
                List<UserDTO> user = CrowDoDTO.LoadUsersFromExcel();
                List<Member> members = _context.ConvertMemberFromDTO(user);
                db.Members.AddRange(members);
                db.SaveChanges();
            }
        }
        [HttpGet("excel/projects")]
        public List<Project> PostProjectsFromExcelToDB()
        {
            List<Project> project = new List<Project>();
            using (var db = new CrowDoDB())
            {
                List<ProjectDTO> projects = CrowDoDTO.LoadProjectsFromExcel();
                project= _context.ConvertProjectFromDTO(projects);
                db.Projects.AddRange(project);
                db.SaveChanges();
            }
            return project;
        }
        [HttpGet("excel/funding")]
        public List<Funding> GetFundingFromExcel()
        {
            List<Funding> project = new List<Funding>();
            using (var db = new CrowDoDB())
            {
                List<FundingDTO> projects = CrowDoDTO.LoadFundingFromExcel();
                project = _context.ConvertFundingFromDTO(projects);
                db.Fundings.AddRange(project);
                db.SaveChanges();
            }
            return project;
        }
        [HttpGet("excel/packages")]
        public List<Packages> GetPackagesFromExcel()
        {
            List<Packages> packages = new List<Packages>();
            using (var db = new CrowDoDB())
            {
                List<PackagesDTO> package = CrowDoDTO.LoadPackagesFromExcel();
                packages = _context.ConvertPackagesFromDTO(package);
                db.Packages.AddRange(packages);
                db.SaveChanges();
            }
            return packages;
        }
        [HttpGet("projects")]
        public List<Project> GetAllProjects()
        {
            return _context.GetProjectsFromDB();
        }
        [HttpGet("projects/{id}")]
        public List<Project> GetProjectById(int id)
        {
            return _context.GetProjectsFromDB(id);
        }
        [HttpGet("projects/{name}")]
        public List<Project> GetProjectsByText(string name)
        {
            return _context.GetProjectsFromDB(name);
        }
        [HttpGet("projects/{category}")]
        public List<Project> GetProjectsByCategory(string category)
        {
            return Databases.GetProjectsFromDBByCategory(category);
        }
        [HttpGet("fundings")]
        public List<Funding> GetFundings(Member member)
        {
            return _context.ViewFundedProjects(member);
        }
        [HttpPost("fund-project/{id}")]
        public void FundAProject(Funding funding)
        {
            _context.FundAproject(funding);
        }

        [HttpDelete("delete/project/{id}")]
        public string DeleteProject(int id)
        {
            return _context.DeleteProject(id);
        }
        [HttpDelete("delete/member/{id}")]
        public string DeleteMember(int id)
        {
            return _context.DeleteUser(id);
        }
        [HttpPut("edit/project/{id}/{title}/{startdate}/{packages}/{norequested}/{category}/{description}/{enddate}/{media}")]
        public string UpdateJournalist(Project project)
        {
            return _context.UpdateProject(project);
        }
        [HttpPost("project/new")]
        public string SetProject(Project p)
        {
            _context.AddProject(p);
            return "project has been added";
        }
    }
}
