namespace Florive.Domains.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }  //не будем  хранить пароль в открытом виде но пока без хэширования
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } 
    }
}