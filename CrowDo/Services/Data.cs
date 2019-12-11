using System.Collections.Generic;
using System.Linq;
using CrowDo.Entities;
using CrowDo.Repositories;

namespace CrowDo.Services
{
    public class Databases
    {
        public bool ConvertMemberFromDTO(List<UserDTO> userDTO)
        {
            using(var db = new CrowDoDB())
            {
                foreach (var item in userDTO)
                {
                    Member member = new Member
                    {
                        Code = item.Code,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Address = item.Address
                    };
                    members.Add(member);
                }
            }
            return true;
        }
        public bool ConvertProjectFromDTO(List<ProjectDTO> projectDTO)
        {
            using(var db = new CrowDoDB())
            {
                foreach (var item in projectDTO)
                {
                    List<string> packages = new List<string>();
                    packages = item.Packages.Split(',').ToList();
                    Project project = new Project
                    {
                        Code = item.Code,
                        Creator = new Member { Code = item.Creator },
                        Title = item.Title,
                        StartDate = item.StartDate,
                        Packages = packages,
                        NumberOfRequested = item.NumberOfRequested
                    };
                    projects.Add(project);
                }
            }
            return true;
        }
        public List<Funding> ConvertFundingFromDTO(List<FundingDTO> fundingDTO)
        {
            Member backer = new Member();
            Project project = new Project();
            List<Funding> fundings = new List<Funding>();
            foreach (var item in fundingDTO)
            { 
                Funding funding = new Funding
                {
                    Backer = new Member { Code = item.Backer },
                    Project = new Project { Code = item.Project },
                    Package = new Packages { Code = item.Package },
                    Number = item.Number
                };
                fundings.Add(funding);
            }
            return fundings;
        }
        public List<Packages> ConvertPackagesFromDTO(List<PackagesDTO> packagesDTO)
        {
            List<Packages> packages = new List<Packages>();
            foreach (var item in packagesDTO)
            {
                Packages package = new Packages
                {
                    Code = item.Code,
                    Title =item.Title,
                    Cost = item.Cost,
                    Details = item.Details,
                    Rewards = item.Rewards
                };
                packages.Add(package);
            }
            return packages;
        }
        public List<Project> GetProjectsFromDB()
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.ToList();
                //return db.Projects.Where(p => p.isDeleted != "inactive").ToList();
            }
        }
        public List<Project> GetProjectsFromDB(int id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p => p.ProjectId == id).ToList();
                //return db.Projects.Where(p => p.ProjectID == id && p.isDeleted != "inactive").ToList();
            }
        }
        public List<Project> GetProjectsFromDB(string name)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p=>p.Title==name).ToList();
                //return db.Projects.Where(p=>p.Title==name && p => p.isDeleted != "inactive").ToList();
            }
        }
        public static List<Project> GetProjectsFromDBByCategory(string category)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p => p.Category == category).ToList();
                //return db.Projects.Where(p=>p.Category==category && p => p.isDeleted != "inactive").ToList();
            }
        }
        public string DeleteProject(int id)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(a => a.ProjectId == id).First();
                if (p == null) return "not exists";
                //p.isDeleted = "inactive";
                db.SaveChanges();
            }
            return "deleted";
        }
        public string DeleteUser(int id)
        {
            using (var db = new CrowDoDB())
            {
                Member m = db.Members.Where(a => a.MemberId == id).First();
                if (m == null) return "not exists";
                m.IsDeleted = "inactive";
                db.SaveChanges();
            }
            return "deleted";
        }
        public string UpdateProject(Project project)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(a => a.ProjectId == project.ProjectId).First();
                if (p == null) return "not exists";
                p.Title = project.Title;
                p.StartDate = project.StartDate;
                //pos na to ftiakso auto
                List<string> pack = new List<string>();
                pack = project.Packages.Split(',').ToList();
                foreach(var item in pack)
                {
                    p.Packages.Code = pack[int.Parse(item)];
                }
                List<string> requested = new List<string>();
                requested = project.NumberOfRequested.Split(',').ToList();
                foreach(var item in requested)
                {
                    p.NumberOfRequested = requested[int.Parse(item)];
                }
                p.Category = project.Category;
                p.Description = project.Description;
                p.EndDate = project.EndDate;
                p.Media = project.Media;
                db.SaveChanges();
            }
            return "updated";
        }
        public void AddProject(Project p)
        {
            using (var db = new CrowDoDB())
            {
                db.Projects.Add(p);
                Member member = new Member();
                member.Projects.Add(p);
                db.SaveChanges();
            }
        }
        public void FundAproject(Funding f)
        {
            Member member = new Member();
            member.Fundings.Add(f);
        }
        public List<Funding> ViewFundedProjects(Member member)
        {
            using(var db = new CrowDoDB())
            {
                return db.Fundings
                    .Where(m => m.Backer.Code == member.Code)
                    .ToList();
            }
        }
        public string LogIn(string username,string password)
        {
            using(var db = new CrowDoDB())
            {
                var usern = db.Members.Any(x => x.Username == username);
                if (usern == true)
                {
                    if (db.Members.Any(x => x.Password == password))
                    {
                        return "Logged In";
                    }
                    else return "wrong password";
                }
                else
                {
                    string name = UsernameOfUser(username);
                    if (name.Equals("not found")) return "wrong username";
                    else return "logged in";
                }
            }
        }
        public string UsernameOfUser(string name)
        {
            using(var db = new CrowDoDB())
            {
                //string fname = db.Members.Select(m => m.FirstName).First();
                //string lname = db.Members.Select(m => m.LastName).First();
                //string n = fname + " " + lname;
                List<string> n = name.Split(" ").ToList();
                string n1 = n[0];
                string n2 = n[1];
                if (db.Members.Any(x => (x.FirstName == n1) && (x.LastName == n2)))
                {
                    return n1 + " " + n2;
                }
                else return "not found";
            }
        }
    }
}
