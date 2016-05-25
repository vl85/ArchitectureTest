namespace CommonServices
{
    public class XBoxService
    {
        public bool IsLoggedIn { get; private set; }

        public bool SilentLogIn()
        {
            return IsLoggedIn = true;
        }

        public bool LogIn()
        {
            return IsLoggedIn = true;
        }

        public bool LogOut()
        {
            return IsLoggedIn = false;
        }
    }
}
