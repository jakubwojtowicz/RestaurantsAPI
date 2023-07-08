using System;

namespace RestaurantAPI.Exceptions
{
    public class NotFoundException:Exception
    {
        //base(message) - wywołanie konstruktora bazowego
        public NotFoundException(string message) : base (message)
        {
            
        }
    }
}
