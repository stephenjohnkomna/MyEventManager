using System;
namespace MyEventManager
{
    public class Event
    {
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }


    public class CityDistance
    {
        public string from { get; set; }
        public string to { get; set; }
        public Event evnt { get; set; }

        public override bool Equals(object? obj)
        {
            if (to + from != from + to)
            {
                return obj is CityDistance search &&
                   to == search.to &&
                   from == search.from;
            }
            else
            {
                return obj is CityDistance search &&
                   from == search.from &&
                   to == search.to;
            }
        }

        public override int GetHashCode()
        {
            if (to + from != from + to)
            {
                return HashCode.Combine(to, from);
            }
            else
            {
                return HashCode.Combine(from, to);
            }
        }
    }
}


