using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using task;

Console.OutputEncoding = Encoding.UTF8;

List<string> phrases = new List<string>();
int total = 200;

Console.WriteLine("Яку мову хочете обрати/What language you want to choose: 1 - Українська, 2 - English");
int input;
while (!int.TryParse(Console.ReadLine(), out input) || (input != 1 && input != 2))
{
    Console.WriteLine("Невірний ввід/Invalid input. Введіть 1 або 2/Enter 1 or 2.");
}
_ = input == 1 ? phrases = Languages.Ukrainian : phrases = Languages.English;

for (int i = 0; i < total; i += 20)
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
        total = usersResponse.total;
        var now = DateTime.Now;
        foreach (var user in usersResponse.data)
        {
            if (user.isOnline )
            {
                Console.WriteLine($"{user.nickname} {phrases[0]}");
            }
            else
            {
                Console.WriteLine($"{user.nickname} {phrases[1]} {UserStatus.GetStatus((DateTime)user.lastSeenDate, phrases)}");
            }
        }
    }
    return 1;
}

//string userStatus(DateTime lastSeenDate)
//{
//    TimeSpan timeDif = DateTime.Now - lastSeenDate;
//    int daysDifference = timeDif.Days;
//    int hoursDifference = timeDif.Hours;
//    int minutesDifference = timeDif.Minutes;
//    int secondsDifference = timeDif.Seconds;
//    if (daysDifference > 7 ) return phrases[2];
//    if (daysDifference > 1 ) return phrases[3];
//    if (lastSeenDate.Day != DateTime.Now.Day ) return phrases[4];
//    if (hoursDifference > 2 ) return phrases[5];
//    if (hoursDifference > 1 ) return phrases[6];
//    if (minutesDifference > 1 ) return phrases[7];
//    if (secondsDifference > 30 ) return phrases[8];
//    return phrases[9];

//}

public class UsersResponse
{
    public int total;
    public List<LastSeenUsers> data;
};
public class LastSeenUsers
{
    public string userId;
    public string nickname;
    public string firstName;
    public string lastName;
    public DateTime? registrationDate;
    public DateTime? lastSeenDate;
    public bool isOnline;
};