using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarTEDSystem.BLL;
using StarTEDSystem.Entities;
using System.Runtime.CompilerServices;

namespace StarTEDA02.Pages.Samples
{
    public class QueryModel : PageModel
    {
        private readonly ClubServices _clubServices;
        private readonly EmployeeServices _employeeServices;

        public QueryModel(ClubServices clubServices, EmployeeServices employeeServices)
        {
            _clubServices = clubServices;
            _employeeServices = employeeServices;
        }

        [BindProperty]
        public string StatusCat { get; set; }
        public List<Club> ClubList { get; set; }
        public List<Employee> EmployeeList { get; set; }

        public void PopulateLists()
        {
            bool.TryParse(StatusCat, out bool status);
            ClubList = _clubServices.Club_ClubByStatusList(status);
            EmployeeList = _employeeServices.Employee_GetClubEmployeesList();
        }
        public void OnGet()
        {

        }

        public void OnPostSearch()
        {
            PopulateLists();
        }
        public IActionResult OnPostAddClub()
        {
            return RedirectToPage("./CRUD");
        }
    }
}
