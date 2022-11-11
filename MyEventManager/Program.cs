using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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


        /*
        We want you to send an email to this customer with all events in their city
        Just call AddToEmail(customer, event) for each event you think they should get
        */
        var customerEvents = GetTopFiveClosesEventToCustomer(customer, events);
        foreach (var item in customerEvents)
        {
            string [] strings = item.ToString().Split('.');
            Event e = new Event { Name = strings[1], City = strings[2] };
            Console.WriteLine($"Name:>> {e.Name} City:>> {e.City}");
            AddToEmail(customer, e);
        }

        Console.ReadLine();
    }

    // You do not need to know how these methods work
    static async void AddToEmail(Customer c, Event e, int? price = null)
    {
        var distance = await GetDistance(c.City, e.City);
        Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
        + (distance > 0 ? $" ({distance} miles away)" : "")
        + (price.HasValue ? $" for ${price}" : ""));
    }


    static async Task<int> GetDistance(string fromCity, string toCity)
    {
        return await AlphebiticalDistance(fromCity, toCity);
    }

    static  Dictionary<string,int> GetTopFiveClosesEventToCustomer(Customer customer, List<Event> events)
    {
        dynamic? data = null;
        try
        {
                Dictionary<string, int> keyValues = new Dictionary<string, int>();

                foreach (var item in events)
                {
                    var keyPrefix = Guid.NewGuid().ToString();
                    int distance =  GetDistance(customer.City, item.City).Result;
                    keyValues.Add(keyPrefix + "." + item.Name + "." + item.City, distance);
                }

                data = keyValues.OrderBy(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);
        }
        catch(Exception ex)
        {
            //
        }
        return data;
    }

    

    private static async Task<int> AlphebiticalDistance(string s, string t)
    {
        var result = 0;
        try
        {  
            await Task.Run(() =>
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
            });
        }
        catch (Exception ex)
        {

        }
        return result;
    }
}