using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarTEDSystem.DAL;
using StarTEDSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarTEDSystem.BLL
{
    public class ClubServices
    {
        #region setup the context connection variable and class constructor
        private readonly StarTEDContext _context;
        public ClubServices(StarTEDContext regcontext)
        {
            _context = regcontext;
        }
        #endregion

        public List<Club> Club_ClubByStatusList(bool active)
        {
             return _context.Clubs
            .Where(x => x.Active == active)
            .OrderBy(x => x.ClubName)
            .ToList();
        }

        public Club Club_GetClubByID(string clubid)
        {
            return _context.Clubs
        .Where(x => x.ClubID.Equals(clubid))
        .FirstOrDefault();
        }

        public List<Club> Club_GetAllClubs()
        {
            return _context.Clubs
                .OrderBy(x => x.ClubName)
                .ToList();

        }

        public string Club_AddClub(Club item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Club data is missing");
            }

            Club idExists = _context.Clubs
                                .Where(x => x.ClubID == item.ClubID)
                                .FirstOrDefault();

            if (idExists != null)
            {
                throw new Exception($"{item.ClubName} with a Club ID {item.ClubID}" +
                    $" is already on file.");
            }

            Club nameExists = _context.Clubs
                               .Where(x => x.ClubName.ToUpper().Equals(item.ClubName.ToUpper()))
                               .FirstOrDefault();

            if (nameExists != null)
            {
                throw new Exception($"{item.ClubName} is already in use. Use a different club name.");
            }

            _context.Clubs.Add(item);

            _context.SaveChanges();

            return item.ClubID;
        }


        public int Club_UpdateClub(Club item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Club data is missing");
            }

            bool exists = _context.Clubs.Any(x => x.ClubID == item.ClubID);

            if (!exists)
            {
                throw new Exception($"{item.ClubName} is no longer on the system.");
            }

            EntityEntry<Club> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return _context.SaveChanges();

        }

        public int Club_DeleteClub(Club item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Club data is missing");
            }

            bool exists = _context.Clubs.Any(x => x.ClubID == item.ClubID);

            if (!exists)
            {
                throw new Exception($"{item.ClubName} is no longer on the system.");
            }
            item.Active = false;
            EntityEntry<Club> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return _context.SaveChanges();

        }

        public int Club_RestoreClub(Club item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Club data is missing");
            }

            bool exists = _context.Clubs.Any(x => x.ClubID == item.ClubID);

            if (!exists)
            {
                throw new Exception($"{item.ClubName} is no longer on the system.");
            }
            item.Active = true;
            EntityEntry<Club> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return _context.SaveChanges();

        }

    }
}
