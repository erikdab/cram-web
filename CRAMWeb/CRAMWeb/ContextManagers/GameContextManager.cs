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

            Game gameReceiveLink = database.Games.SingleOrDefault(g => g.Id == gameId);
            ApplicationUser user = game.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                game.Users.Add(database.Users.Single(u => u.Id == userId));

                GameState gameState = new GameState()
                {
                    Food = 0,
                    Wood = 0,
                    Stone = 0,
                    Gold = 0,
                    Soldiers = 0,
                    CastleLevel = 1,
                    FarmsLevel = 1,
                    LumberjackLevel = 1,
                    HousingLevel = 1,
                    MinesLevel = 1,
                    User = userReceiveLink,
                    Game = gameReceiveLink,
                    SentRaids = new List<Raid>(),
                    ReceivedRaids = new List<Raid>()
                };
                database.GameStates.Add(gameState);
            }

            database.SaveChanges();
        }
    }
}