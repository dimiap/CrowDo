using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Code { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string IsDeleted { get; set; }
        public List<Project> Projects { get; set; }
        public List<Funding> Fundings { get; set; }
    }
    public class Project
    {
        public int ProjectId { get; set; }
        public string Code { get; set; }
        public Member Creator { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Media { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Packages Packages { get; set; }
        public string NumberOfRequested { get; set; }
        //public int NumberOfVisits{get;set;}
        //public string isDeleted {get;set;}
    }
    public class Funding
    {
        public int FundingId { get; set; }
        public Member Backer { get; set; }
        public Project Project { get; set; }
        public Packages Package { get; set; }
        public int Number { get; set; }
    }
    public class Packages
    {
        public int PackagesId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public string Details { get; set; }
        public string Rewards { get; set; }
    }
}
