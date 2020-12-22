namespace TestNinja.Fundamentals
{
    public class Reservation
    {
        public User MadeBy { get; set; }

        public bool CanBeCancelledBy(User user)
        {
            return (user.IsAdmin || MadeBy == user);
            /// Before Refactoring
            //if (user.IsAdmin || MadeBy == user)
            //    return true;
            //return false;
        }
        
    }

    public class User
    {
        public bool IsAdmin { get; set; }
    }
}