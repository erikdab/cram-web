﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRAMWeb.Models
{
    /// <summary>
    /// Table containing data about raids sended between players
    /// </summary>
    public class Raid
    {
        /// <summary>
        /// Raid Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Soldiers sended on raid
        /// </summary>
        public int Soldiers { get; set; }

        /// <summary>
        /// Food stealed from castle
        /// </summary>
        public int Food { get; set; }

        /// <summary>
        /// Wood stealed from castle
        /// </summary>
        public int Wood { get; set; }

        /// <summary>
        /// Stone stealed from castle
        /// </summary>
        public int Stone { get; set; }

        /// <summary>
        /// Gold stealed from castle
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// Game State of Player who sends raid
        /// </summary>
        [InverseProperty("SentRaids")]
        public virtual GameState AttackingGameState { get; set; }

        /// <summary>
        /// Game State of Player who will defend against raid
        /// </summary>
        [InverseProperty("ReceivedRaids")]
        public virtual GameState DefendingGameState { get; set; }
    }
}