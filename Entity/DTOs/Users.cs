using Microsoft.AspNetCore.Authorization;

namespace Avanade.DTOs
{
    [AllowAnonymous]
    public class Users
    {
        public required int ID_Usuario { get; set; }
        public required string Usuario { get; set; }
        public required string Senha { get; set; }
        public required string Role { get; set; }

    }
}
