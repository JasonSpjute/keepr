using System;

namespace Party_Planner.Exceptions
{
    public class NotAuthorized : Exception
    {
        public NotAuthorized(string message) : base(message)
        {
        }
    }
}