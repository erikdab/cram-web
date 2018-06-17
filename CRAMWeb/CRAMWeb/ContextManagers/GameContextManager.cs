using CRAMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRAMWeb.ContextManagers
{
    public static class GameContextManager
    {
        /// <summary>
        /// Adds or updates Instructor and Courses both ways
        /// </summary>
        /// <param name="context">Used context data base</param>
        /// <param name="instructorFullName">Full name of instructor</param>
        /// <param name="courseName">Name of the course</param>
        public static void AddOrUpdateUserToGame(ApplicationDbContext database, int gameId, string userId)
        {
            Game game = database.Games.SingleOrDefault(g => g.Id == gameId);
            ApplicationUser user = game.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                game.Users.Add(database.Users.Single(u => u.Id == userId));
            }
            database.SaveChanges();
        }
    }
}