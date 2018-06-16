using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRAMWeb.Models
{
    /// <summary>
    /// Table containing games
    /// </summary>
    [Table("KacperWeissErikBurnellGames")]
    public class Game
    {
        /// <summary>
        /// Game Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Game's name.
        /// </summary>
        [MaxLength(32)]
        public string GameName { get; set; }

        /// <summary>
        /// Max players that can participate in game - 4 is default.
        /// </summary>
        public int MaxPlayers { get; set; } = 4;

        /// <summary>
        /// List of players
        /// </summary>
        public virtual IList<User> Users { get; set; }


    }
}