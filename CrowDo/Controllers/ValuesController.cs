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
        [HttpGet("excel/users")]
        public List<Member> PostUsersFromExcelToDB()
        {
            List<UserDTO> user = CrowDoDTO.LoadUsersFromExcel();
            List<Member> members = _context.ConvertMemberFromDTO(user);
            return members;
        }
        [HttpGet("excel/projects")]
        public List<Project> PostProjectsFromExcelToDB()
        {
            List<ProjectDTO> projects = CrowDoDTO.LoadProjectsFromExcel();
            List<Project> project = _context.ConvertProjectFromDTO(projects);
            return project;
        }
        [HttpGet("excel/funding")]
        public List<Funding> GetFundingFromExcel()
        {
            List<FundingDTO> fundings = CrowDoDTO.LoadFundingFromExcel();
            List<Funding> funding = _context.ConvertFundingFromDTO(fundings);
            return funding;
        }
        [HttpGet("excel/packages")]
        public List<Packages> GetPackagesFromExcel()
        {
            List<PackagesDTO> package = CrowDoDTO.LoadPackagesFromExcel();
            List<Packages> packages = _context.ConvertPackagesFromDTO(package);
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
        [HttpGet("projects1/{name}")]
        public List<Project> GetProjectsByText(string name)
        {
            return _context.GetProjectsFromDB(name);
        }
        [HttpGet("project/{category}")]
        public List<Project> GetProjectsByCategory(string category)
        {
            return Databases.GetProjectsFromDBByCategory(category);
        }
        //komple
        [HttpGet("fundings/{id}")]
        public List<Funding> GetFundings(int id)
        {
            return _context.ViewFundedProjects(id);
        }
        //komple
        [HttpPost("fund-project")]
        public string FundAProject(Funding funding)
        {
            return _context.FundAproject(funding);
        }
        //komple
        [HttpDelete("delete/project/{id}")]
        public string DeleteProject(int id)
        {
            return _context.DeleteProject(id);
        }
        //komple
        [HttpDelete("delete/member/{id}")]
        public string DeleteMember(int id)
        {
            return _context.DeleteUser(id);
        }
        //komple 
        [HttpPut("edit/project/{id}")]
        public string UpdateJournalist(int id, Project project)
        {
            return _context.UpdateProject(id,project);
        }
        //komple
        [HttpPost("project/new")]
        public string SetProject(Project p)
        {
            return _context.AddProject(p);
        }
        //komple
        [HttpPost("signup/member")]
        public string SignUp(Member member)
        {
            return _context.SignUp(member);
        }
        //komple
        [HttpGet("member/login/{username}/{password}")]
        public string LogIn(string username,string password)
        {
            return _context.LogIn(username,password);
        }
        //komple
        [HttpGet("member/{id}")]
        public List<Member> ShowUser(int id)
        {
            return _context.ShowUser(id);
        }
        //komple
        [HttpPut("edit/member/{id}")]
        public string EditMember(int id,Member member)
        {
            return _context.EditUser(id,member);
        }
    }
}
