using Newtonsoft.Json;
using System.Net.Http;

for (int i = 0; i < 200; i += 20)
{
    await getUsers(i);
}
async Task<int> getUsers(int offset)
{
    using (HttpClient httpClient = new HttpClient())
    {
        string url = $"https://sef.podkolzin.consulting/api/users/lastSeen?offset={offset}";
        HttpResponseMessage response = await httpClient.GetAsync(url);
        string content = await response.Content.ReadAsStringAsync();
        UsersResponse usersResponse = JsonConvert.DeserializeObject<UsersResponse>(content);
        var now = DateTime.Now;
        foreach (var user in usersResponse.data)
        {
            if (user.isOnline)
            {
                Console.WriteLine($"{user.nickname} is online");
            }
            else
            {
                Console.WriteLine($"{user.nickname} was last seen online {userStatus((DateTime)user.lastSeenDate)}");
            }
        }
    }
    return 1;
}

string userStatus(DateTime lastSeenDate)
{
    TimeSpan timeDif = DateTime.Now - lastSeenDate;
    int daysDifference = timeDif.Days;
    int hoursDifference = timeDif.Hours;
    int minutesDifference = timeDif.Minutes;
    int secondsDifference = timeDif.Seconds;
    if (daysDifference > 7) return "long time ago";
    if (daysDifference > 1) return "this week";
    if (lastSeenDate.Day != DateTime.Now.Day) return "yesterday";
    if (hoursDifference > 2) return "today";
    if (hoursDifference > 1) return "hour ago";
    if (minutesDifference > 1) return "couple of minutes ago";
    if (secondsDifference > 30) return "less than a minute ago";
    return "just now";

}
class UsersResponse
{
    public int total;
    public List<LastSeenUsers> data;
};
class LastSeenUsers
{
    public string userId;
    public string nickname;
    public string firstName;
    public string lastName;
    public DateTime? registrationDate;
    public DateTime? lastSeenDate;
    public bool isOnline;
};