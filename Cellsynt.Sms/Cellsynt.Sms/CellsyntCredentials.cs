namespace Cellsynt.Sms
{
    /// <summary>
    /// Credentials for the Cellsynt account
    /// </summary>
    public class CellsyntCredentials
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CellsyntCredentials()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userName">The user name of the account</param>
        /// <param name="password">The password of the account</param>
        public CellsyntCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// The user name of the account
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The password of the account
        /// </summary>
        public string Password { get; set; }
    }
}
