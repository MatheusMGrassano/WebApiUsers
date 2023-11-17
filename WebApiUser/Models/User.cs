using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace WebApiUser.Models
{
    public class User
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime SignUpDate { get; private set; } = DateTime.Now;

    }
}
