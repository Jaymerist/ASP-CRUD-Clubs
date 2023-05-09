using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StarTEDSystem.BLL;
using StarTEDSystem.Entities;
using System.Runtime.CompilerServices;


namespace StarTEDA02.Pages.Samples
{
    public class CRUDModel : PageModel
    {
        private readonly ClubServices _clubServices;
        private readonly EmployeeServices _employeeServices;

        public CRUDModel(ClubServices clubServices, EmployeeServices employeeServices)
        {
            _clubServices = clubServices;
            _employeeServices = employeeServices;
        }

        [BindProperty(SupportsGet = true)]
        public string? clubid { get; set; }
        [BindProperty]
        public Club clubInfo { get; set; }
        public List<Club> clubsList { get; set; }
        public List<Employee> employeeList { get; set; }
        public bool HasFeedback { get { return !string.IsNullOrWhiteSpace(Feedback); } }
        public string Feedback { get; set; }
        public void OnGet()
        {
             if (!string.IsNullOrEmpty(clubid))
                {
                    clubInfo = _clubServices.Club_GetClubByID(clubid);
            }
            PopulateSupportLists();
        }

        public void PopulateSupportLists()
        {
            clubsList = _clubServices.Club_GetAllClubs();
            employeeList = _employeeServices.Employee_GetClubEmployeesList();
        }

        public IActionResult OnPostSearch()
        {

            return RedirectToPage("./Query");
        }

        public IActionResult OnPostClear()
        {
            ModelState.Clear();
            Feedback = "";
            clubid = (string?)null;
            return RedirectToPage(new { clubid = clubid });
        }



        public IActionResult OnPostAdd()
        {
            if (clubInfo.Fee == null)
            {
                clubInfo.Fee = 0;
            }

            clubInfo.Active = true;
            if (string.IsNullOrWhiteSpace(clubInfo.ClubID))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select a Club ID for the club. Leave no spaces.");

            }
            if (string.IsNullOrEmpty(clubInfo.ClubName))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubName), "Choose a name for the club");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clubServices.Club_AddClub(clubInfo);

                    Feedback = $"club (id: {clubInfo.ClubID}) has been added to the system";
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
            }

            PopulateSupportLists();
            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            if(clubInfo.Fee == null)
            {
                clubInfo.Fee = 0;
            }
  
            if (string.IsNullOrEmpty(clubInfo.ClubID))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select an ID for the club");

            }
            if (string.IsNullOrEmpty(clubInfo.ClubName))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select a name for the club");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    int rowsaffected = _clubServices.Club_UpdateClub(clubInfo);
                    if (rowsaffected > 0)
                    {
                        Feedback = $"Club (id: {clubInfo.ClubID}) has been updated";
                    }
                    else
                    {
                        Feedback = $"No changes have been made.";
                    }
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }

            }

            PopulateSupportLists();

            return Page();
        }

        public IActionResult OnPostDelete()
        {
            if (string.IsNullOrEmpty(clubInfo.ClubID))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select an ID for the club");

            }
            if (string.IsNullOrEmpty(clubInfo.ClubName))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select a name for the club");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    int rowsaffected = _clubServices.Club_DeleteClub(clubInfo);
                    if (rowsaffected > 0)
                    {
                        Feedback = $"Club (id: {clubInfo.ClubID}) has been discontinued";
                    }
                    else
                    {
                        Feedback = $"Club (id: {clubInfo.ClubID}) has been remove from the system. Return to the search page.";
                    }
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
            }

            PopulateSupportLists();

            return Page();
        }


        public IActionResult OnPostRestore()
        {
            if (string.IsNullOrEmpty(clubInfo.ClubID))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select an ID for the club");

            }
            if (string.IsNullOrEmpty(clubInfo.ClubName))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select a name for the club");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    int rowsaffected = _clubServices.Club_RestoreClub(clubInfo);
                    if (rowsaffected > 0)
                    {
                        Feedback = $"Club (id: {clubInfo.ClubID}) has been set to active.";
                    }
                    else
                    {
                        Feedback = $"Club (id: {clubInfo.ClubID}) has been remove from the system. Return to the search page.";
                    }

                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                }
            }

            PopulateSupportLists();

            return Page();
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

    }
}
