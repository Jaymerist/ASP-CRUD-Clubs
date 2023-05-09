using StarTEDSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarTEDSystem.Entities;

namespace StarTEDSystem.BLL
{
    public class EmployeeServices
    {

        #region setup the context connection variable and class constructor
        private readonly StarTEDContext _context;
        public EmployeeServices(StarTEDContext regcontext)
        {
            _context = regcontext;
        }
        #endregion

        public List<Employee> Employee_GetAvailableClubEmployees()
        {
            return _context.Employees
        .Where(x => x.PositionID == 3 || x.PositionID == 4 || x.PositionID == 5)
        .OrderBy(x => x.LastName)
        .ToList();
        }

        public List<Employee> Employee_GetClubEmployeesList()
        {
            return _context.Employees
                .Where(x => x.Clubs.Any(y => x.EmployeeID == y.EmployeeID))
                .OrderBy(x => x.LastName)
                .ToList();
        }

    }
}
