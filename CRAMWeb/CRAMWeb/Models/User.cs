using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRAMWeb.Models
{
    [Table("KacperWeissErikBurnellUsers")]
    public class User
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        [Index(IsUnique=true)]
        [MaxLength(64)]
        public string Username { get; set; }

        /// <summary>
        /// Hashed password.
        /// </summary>
        public string PasswordHash { get; set; }
    }
    
    public class CreateUserViewModel
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Hashed password.
        /// </summary>
        public string Password { get; set; }
    }
}