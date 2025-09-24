namespace Avanade.DTOs.DadosLogin
{
    public class UserRep
    {
        public Users UserData(string Usuario, string Senha)
        {
            var users = new List<Users>
            {
                new Users { ID_Usuario = 1, Usuario = "admin", Senha = "admin", Role = "admin" },
                new Users { ID_Usuario = 2, Usuario = "user", Senha = "user", Role = "User" }
            };

            return users.FirstOrDefault(u => u.Usuario == Usuario && u.Senha == Senha)!;
        }
    }
}
