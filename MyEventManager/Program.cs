using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MyEventManager;


public class Program
{
    static void Main(string[] args)
    {
        var events = new List<Event>{
         new Event{ Name = "Phantom of the Opera", City = "New York"},
         new Event{ Name = "Metallica", City = "Los Angeles"},
         new Event{ Name = "Metallica", City = "New York"},
         new Event{ Name = "Metallica", City = "Boston"},
         new Event{ Name = "LadyGaGa", City = "New York"},
         new Event{ Name = "LadyGaGa", City = "Boston"},
         new Event{ Name = "LadyGaGa", City = "Chicago"},
         new Event{ Name = "LadyGaGa", City = "San Francisco"},
         new Event{ Name = "LadyGaGa", City = "Washington"}
          };

        //1. find out all events that are in cities of customer
        // then add to email.
        var customer = new Customer { Name = "Mr. Fake", City = "New York" };
        var eventsInCustomerCity = events.FindAll(x => x.City == customer.City);

        // 1. TASK
        foreach (var item in eventsInCustomerCity)
        {
            AddToEmail(customer, item);
        }


        /* 2.
        We want you to send an email to this customer with all events in their city
        Just call AddToEmail(customer, event) for each event you think they should get
        */
        var customerEvents = GetTopFiveClosesEventToCustomer(customer, events);
        foreach (var item in customerEvents)
        {
          
            Event ev = item.Key.evnt;
            Console.WriteLine($"Event Name:> {ev.Name} Event City:> {ev.City}");
            AddToEmail(customer, ev);
        }

        Console.ReadLine();
    }

    // You do not need to know how these methods work
    static async void AddToEmail(Customer c, Event e, int? price = null)
    {
        var distance =  GetDistance(c.City, e.City);
        //Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
        //+ (distance > 0 ? $" ({distance} miles away)" : "")
        //+ (price.HasValue ? $" for ${price}" : ""));
    }


    static int GetDistance(string fromCity, string toCity)
    {
        return AlphebiticalDistance(fromCity, toCity);
    }

    /// <summary>
    /// Method to get the Top Five Closest Event to Customer.
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="events"></param>
    /// <returns></returns>
    static Dictionary<CityDistance, int> GetTopFiveClosesEventToCustomer(Customer customer, List<Event> events)
    {
        dynamic? data = null;
        Dictionary<CityDistance, int> keyValues = new Dictionary<CityDistance, int>();
        try
        {
            foreach (var item in events)
            {
                var cityDistance = new CityDistance();
                cityDistance.to = item.City;
                cityDistance.from = customer.City;
                cityDistance.evnt = item;

                if(!keyValues.ContainsKey(cityDistance))
                {
                    keyValues.Add(cityDistance, GetDistance(customer.City, item.City));
                }    
            }

            data = keyValues.OrderBy(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);
        }
        catch (Exception ex)
        {
            //
        }
        return data;
    }


    /// <summary>
    /// Method to get the Top Five Closest Event to Customer.
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="events"></param>
    /// <returns></returns>
    static Dictionary<CityDistance, int> GetEventToCustomerByPrice(Customer customer, List<Event> events)
    {
        dynamic? data = null;
        Dictionary<CityDistance, int> keyValues = new Dictionary<CityDistance, int>();
        try
        {
            foreach (var item in events)
            {
                var cityDistance = new CityDistance();
                cityDistance.to = item.City;
                cityDistance.from = customer.City;
                cityDistance.evnt = item;

                if (!keyValues.ContainsKey(cityDistance))
                {
                    keyValues.Add(cityDistance, GetDistance(customer.City, item.City));
                }
            }

            data = keyValues.OrderBy(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);
        }
        catch (Exception ex)
        {
            //
        }
        return data;
    }



    private static int AlphebiticalDistance(string s, string t)
    {
        var result = 0;
        try
        {  
           
                var i = 0;
                for (i = 0; i < Math.Min(s.Length, t.Length); i++)
                {
                    // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                    result += Math.Abs(s[i] - t[i]);
                }

                for (; i < Math.Max(s.Length, t.Length); i++)
                {
                    // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                    result += s.Length > t.Length ? s[i] : t[i];
                }          
        }
        catch (Exception ex)
        {

        }
        return result;
    }
}