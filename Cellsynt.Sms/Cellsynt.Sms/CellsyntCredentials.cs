namespace Cellsynt.Sms
{
    public class CellsyntCredentials
    {
        public CellsyntCredentials()
        {
        }

        public CellsyntCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
