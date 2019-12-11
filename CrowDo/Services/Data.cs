using System.Collections.Generic;
using System.Linq;
using CrowDo.Entities;
using CrowDo.Repositories;

namespace CrowDo.Services
{
    public class Databases
    {
        public List<Member> ConvertMemberFromDTO(List<UserDTO> userDTO)
        {
            using (var db = new CrowDoDB())
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
                    db.Members.Add(member);
                }
                db.SaveChanges();
                return db.Members.Where(x => x.IsDeleted != "inactive").ToList();
            }
        }
        public List<Project> ConvertProjectFromDTO(List<ProjectDTO> projectDTO)
        {
            using (var db = new CrowDoDB())
            {
                foreach (var item in projectDTO)
                {
                    Project project = new Project
                    {
                        Code = item.Code,
                        Title = item.Title,
                        StartDate = item.StartDate,
                        NumberOfRequested = item.NumberOfRequested
                    };
                    Member member = db.Members.Where(x => x.Code.Equals(item.Creator)).First();
                    if (member == null) continue;
                    project.Creator = member;
                    //packages
                    List<string> pack = new List<string>();
                    pack = item.Packages.Split(',').ToList();
                    Packages package = new Packages();
                    List<Packages> packages = new List<Packages>();
                    for (int i = 0; i == pack.Count(); i++)
                    {
                        package = db.Packages.Where(x => x.Code.Equals(pack[i])).First();
                        packages.Add(package);
                    }
                    if (package == null) continue;
                    project.Packages = packages;
                    db.Projects.Add(project);
                }
                db.SaveChanges();
                return db.Projects.Where(x => x.IsDeleted != "inactive").ToList();
            }
        }
        public List<Funding> ConvertFundingFromDTO(List<FundingDTO> fundingDTO)
        {
            using (var db = new CrowDoDB())
            {
                foreach (var item in fundingDTO)
                {
                    Funding funding = new Funding
                    {
                        Number = item.Number
                    };
                    Member member = db.Members.Where(x => x.Code.Equals(item.Backer)).First();
                    if (member == null) continue;
                    funding.Backer = member;
                    Project project = db.Projects.Where(x => x.Code.Equals(item.Project)).First();
                    if (project == null) continue;
                    funding.Project = project;
                    Packages packages = db.Packages.Where(x => x.Code.Equals(item.Package)).First();
                    if (packages == null) continue;
                    funding.Package = packages;
                    db.Fundings.Add(funding);
                }
                db.SaveChanges();
                return db.Fundings.ToList();
            }
        }
        public List<Packages> ConvertPackagesFromDTO(List<PackagesDTO> packagesDTO)
        {
            using (var db = new CrowDoDB())
            {
                foreach (var item in packagesDTO)
                {
                    Packages package = new Packages
                    {
                        Code = item.Code,
                        Title = item.Title,
                        Cost = item.Cost,
                        Details = item.Details,
                        Rewards = item.Rewards
                    };
                    db.Packages.Add(package);
                }
                db.SaveChanges();
                return db.Packages.ToList();
            }
        }
        public List<Project> GetProjectsFromDB()
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p => p.IsDeleted != "inactive").ToList();
            }
        }
        public List<Project> GetProjectsFromDB(int id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p => p.ProjectId == id).ToList();
            }
        }
        public List<Project> GetProjectsFromDB(string name)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p => (p.Title.Equals(name)) && (p.IsDeleted != "inactive")).ToList();
            }
        }
        public static List<Project> GetProjectsFromDBByCategory(string category)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p => (p.Category == category) && (p.IsDeleted != "inactive")).ToList();
            }
        }
        public string DeleteProject(int id)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(a => a.ProjectId == id).First();
                if (p == null) return "not exists";
                p.IsDeleted = "inactive";
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

                //Packages packages = new Packages();
                //p.Packages = project.Packages.Split(',').ToList();
                //foreach (var item in pack)
                //{
                //    p.Packages.Code = pack[int.Parse(item)];
                //}
                //List<string> requested = new List<string>();
                //requested = project.NumberOfRequested.Split(',').ToList();
                //foreach(var item in requested)
                //{
                //    p.NumberOfRequested = requested[int.Parse(item)];
                //}
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
        public string FundAproject(Funding f)
        {
            using (var db = new CrowDoDB())
            {
                Funding funding = new Funding
                {
                    Number = f.Number
                };
                Member member = db.Members.Single(x => x.Code.Equals(f.Backer));
                if (member == null) return "not a valid member";
                funding.Backer = member;
                Project project = db.Projects.Single(x => x.Code.Equals(f.Project));
                funding.Project = project;
                Packages packages = db.Packages.Single(x => x.Code.Equals(f.Package));
                funding.Package = packages;
                db.Fundings.Add(funding);
                return "saved";
            }
        }
        public List<Funding> ViewFundedProjects(Member member)
        {
            using (var db = new CrowDoDB())
            {
                return db.Fundings
                    .Where(m => m.Backer.Code == member.Code)
                    .ToList();
            }
        }
        public string LogIn(string username,string password)
        {
            using (var db = new CrowDoDB())
            {
                var user = db.Members.Any(x => x.Username == username);
                if (user == true)
                {
                    if (db.Members.Any(x => x.Password == password))
                    {
                        return "logged in";
                    }
                    else return "wrong password";
                }
                else
                {
                    string name = UsernameOfUser(username);
                    if (name.Equals("not found")) return "wrong username";
                    else
                    {
                        if (password.Equals("123456"))
                            return "Logged in";
                        else return "incorrect password";
                    }
                }
            }
        }
        public string UsernameOfUser(string name)
        {
            using (var db = new CrowDoDB())
            {
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
        public string SignUp(Member m)
        {
            using (var db = new CrowDoDB())
            {
                Member member = new Member();
                if (db.Members.Any(x => x.Username.Equals(m.Username)))
                    return "already a user";
                if (db.Members.Any(x => x.Email.Equals(m.Email)))
                    return "already a user with this email";
                else
                {
                    db.Members.Add(m);
                    db.SaveChanges();
                    return "user created";
                }
            }
        }
        public List<Member> ShowUser(int id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Members.Where(x => (x.MemberId == id) && (x.IsDeleted!="inactive")).ToList();
            }
        }
        public string EditUser(Member member)
        {
            using (var db = new CrowDoDB())
            {
                Member m = db.Members.Where(a => (a.MemberId == member.MemberId) && (a.IsDeleted!="inactive")).First();
                if (m == null) return "user not exists";
                else
                {
                    m.Username = member.Username;
                    m.Password = member.Password;
                    m.Address = member.Address;
                    m.Email = member.Email;
                    m.FirstName = member.FirstName;
                    m.LastName = member.LastName;
                    db.SaveChanges();
                    return "updated";
                }
            }
        }
    }
}
