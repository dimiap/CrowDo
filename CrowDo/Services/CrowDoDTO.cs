using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CrowDo.Services
{
    public class UserDTO
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
    public class ProjectDTO
    {
        public string Code { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string Packages { get; set; }
        public string NumberOfRequested { get; set; }
    }
    public class FundingDTO
    {
        public string Backer { get; set; }
        public string Project { get; set; }
        public string Package { get; set; }
        public int Number { get; set; }
    }
    public class PackagesDTO
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public string Details { get; set; }
        public string Rewards { get; set; }
    }
    public class CrowDoDTO
    {
        public static List<UserDTO> LoadUsersFromExcel()
        {
            List<UserDTO> users = new List<UserDTO>();
            string filename = @"C:\Users\Δήμητρα\Desktop\accenture\demodataForCrowdo.xlsx";
            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }
            ISheet sheet = hssfwb.GetSheet("Users");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                //null is when the row only contains empty cells
                if (sheet.GetRow(row) != null)
                {
                    UserDTO user = new UserDTO
                    {
                        Code = sheet.GetRow(row).GetCell(0).StringCellValue,
                        FirstName = sheet.GetRow(row).GetCell(1).StringCellValue,
                        LastName = sheet.GetRow(row).GetCell(2).StringCellValue,
                        Address = sheet.GetRow(row).GetCell(3).StringCellValue
                    };
                    users.Add(user);
                }
            }
            return users;
        }
        public static List<ProjectDTO> LoadProjectsFromExcel()
        {
            List<ProjectDTO> projects = new List<ProjectDTO>();
            string filename = @"C:\Users\Δήμητρα\Desktop\accenture\demodataForCrowdo.xlsx";
            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }
            ISheet sheet = hssfwb.GetSheet("Projects");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null)
                {
                    ProjectDTO project = new ProjectDTO
                    {
                        Code = sheet.GetRow(row).GetCell(0).StringCellValue,
                        Creator = sheet.GetRow(row).GetCell(1).StringCellValue,
                        Title = sheet.GetRow(row).GetCell(2).StringCellValue,
                        StartDate = sheet.GetRow(row).GetCell(3).DateCellValue,
                        Packages = sheet.GetRow(row).GetCell(4).StringCellValue,
                        NumberOfRequested = sheet.GetRow(row).GetCell(5).StringCellValue
                    };
                    projects.Add(project);
                }
            }
            return projects;
        }
        public static List<FundingDTO> LoadFundingFromExcel()
        {
            List<FundingDTO> fundings = new List<FundingDTO>();
            string filename = @"C:\Users\Δήμητρα\Desktop\accenture\demodataForCrowdo.xlsx";
            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }
            ISheet sheet = hssfwb.GetSheet("Funding");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                //null is when the row only contains empty cells
                if (sheet.GetRow(row) != null)
                {
                    FundingDTO funding = new FundingDTO
                    {
                        Backer = sheet.GetRow(row).GetCell(0).StringCellValue,
                        Project = sheet.GetRow(row).GetCell(1).StringCellValue,
                        Package = sheet.GetRow(row).GetCell(2).StringCellValue,
                        Number = (int) sheet.GetRow(row).GetCell(3).NumericCellValue
                        };
                    fundings.Add(funding);
                }
            }
            return fundings;
        }
        public static List<PackagesDTO> LoadPackagesFromExcel()
        {
            List<PackagesDTO> packages = new List<PackagesDTO>();
            string filename = @"C:\Users\Δήμητρα\Desktop\accenture\demodataForCrowdo.xlsx";
            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }
            ISheet sheet = hssfwb.GetSheet("Packages");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                //null is when the row only contains empty cells
                if (sheet.GetRow(row) != null)
                {
                    DataFormatter formatter = new DataFormatter();
                    PackagesDTO package = new PackagesDTO
                    {
                        Code = sheet.GetRow(row).GetCell(0).StringCellValue,
                        Title = sheet.GetRow(row).GetCell(1).StringCellValue,
                        Cost = (int)sheet.GetRow(row).GetCell(2).NumericCellValue,
                        Details = sheet.GetRow(row).GetCell(3).StringCellValue,
                        Rewards = sheet.GetRow(row).GetCell(4).StringCellValue
                    };
                    packages.Add(package);
                }
            }
            return packages;
        }
    }
}
