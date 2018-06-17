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
        /// Adds or updates connection between user and game both ways
        /// </summary>
        /// <param name="database">Used database context</param>
        /// <param name="gameId">Game's Id</param>
        /// <param name="userId">User's Id</param>
        public static void AddOrUpdateUserAndGameConnection(ApplicationDbContext database, int gameId, string userId)
        {
            ApplicationUser userReceiveLink = database.Users.SingleOrDefault(u => u.Id == userId);
            Game game = userReceiveLink.Games.SingleOrDefault(g => g.Id == gameId);
            if (game == null)
            {
                userReceiveLink.Games.Add(database.Games.Single(g => g.Id == gameId));
            }

            database.SaveChanges();

            Game gameReceiveLink = database.Games.SingleOrDefault(g => g.Id == gameId);
            ApplicationUser user = gameReceiveLink.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                gameReceiveLink.Users.Add(database.Users.Single(u => u.Id == userId));
            }

            database.SaveChanges();
        }
    }
}