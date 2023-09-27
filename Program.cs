using Newtonsoft.Json;
using System.Net.Http;

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