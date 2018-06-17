using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRAMWeb.Models
{
    /// <summary>
    /// Table containing data about game
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Game data's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Food stored in castle
        /// </summary>
        public int Food { get; set; }

        /// <summary>
        /// Wood stored in castle
        /// </summary>
        public int Wood { get; set; }

        /// <summary>
        /// Stone stored in castle
        /// </summary>
        public int Stone { get; set; }

        /// <summary>
        /// Gold stored in castle
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// Soldiers garisoned in castle
        /// </summary>
        public int Soldiers { get; set; }

        /// <summary>
        /// Castle's level
        /// </summary>
        public int CastleLevel { get; set; }

        /// <summary>
        /// Farms's level
        /// </summary>
        public int FarmsLevel { get; set; }

        /// <summary>
        /// Lumberjack's level
        /// </summary>
        public int LumberjackLevel { get; set; }

        /// <summary>
        /// Housing's level
        /// </summary>
        public int HousingLevel { get; set; }

        /// <summary>
        /// Mines's level
        /// </summary>
        public int MinesLevel { get; set; }

        /// <summary>
        /// Application User.
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Game which Game State belongs to.
        /// </summary>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Raids player sended from his castle
        /// </summary>
        public virtual IList<Raid> SentRaids { get; set; }

        /// <summary>
        /// Raids other players sended to player's castle
        /// </summary>
        public virtual IList<Raid> ReceivedRaids { get; set; }
    }
}