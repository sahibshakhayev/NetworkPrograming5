using Client_;
using System.Collections.Generic;
using System.Text.Json;

HttpClient client = new HttpClient();



while (true)
{
    var message = new HttpRequestMessage
    {
        RequestUri = new Uri("http://localhost:27001/"),
    };
    Console.WriteLine("1-GET");
    Console.WriteLine("2-POST");
    Console.WriteLine("3-PUT");
    Console.WriteLine("4-DELETE");
    Int32.TryParse(Console.ReadLine(), out int choice);
    switch (choice)
    {
        case (int)methods.GET:
            message.Method = HttpMethod.Get;
            var users = await GetUsers(message);
            foreach (var user in users!)
            {
                Console.WriteLine($"    {user}");
            }
            break;
        case (int)methods.POST:
            message.Method = HttpMethod.Post;
            Console.WriteLine("Enter <Name> <Surname>");
            string[] user_data = Console.ReadLine().Split(' ');
            if (user_data.Length == 2)
            {
                var post_result = await PostUser(message, user_data);

                Console.WriteLine(post_result);
            }
            else
            {
                Console.WriteLine("Incorrect input!");
            }
            break;
        case (int)methods.PUT:
            message.Method = HttpMethod.Put;
            Console.WriteLine("Enter <id> <Name> <Surname>");
            string[] user_n_data = Console.ReadLine().Split(' ');
            if (user_n_data.Length > 1 && user_n_data.Length < 4)
            {
                var put_result = await PutUser(message, user_n_data);

                Console.WriteLine(put_result);
            }
            else
            {
                Console.WriteLine("Incorrect input!");
            }
            break;
        case (int)methods.DELETE:
            message.Method = HttpMethod.Delete;
            Console.WriteLine("Enter id:");
            
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                var delete_result = await DeleteUser(message, result);

                Console.WriteLine(delete_result);
            }
            else
            {
                Console.WriteLine("Incorrect input!");
            }
            break;
        default:
            break;
    }
    Console.WriteLine("Enter any key for continue");
    Console.ReadLine();
    Console.Clear();
}

async Task<List<User>> GetUsers(HttpRequestMessage message)
{
    var response = await client.SendAsync(message);
    var json = await response.Content.ReadAsStringAsync();
    var users = JsonSerializer.Deserialize<List<User>>(json);
    return users;
}

async Task<string> PostUser(HttpRequestMessage message, string[] user_data)
{
    var json = JsonSerializer.Serialize(user_data);
   
    
    message.Content = new StringContent(json);
    var response = await client.SendAsync(message);
    var answer = await response.Content.ReadAsStringAsync();
    return answer.ToString();
    
}


async Task<string> PutUser(HttpRequestMessage message, string[] user_data)
{
    var json = JsonSerializer.Serialize(user_data);
    message.Content = new StringContent(json);
    var response = await client.SendAsync(message);
    var answer = await response.Content.ReadAsStringAsync();
    return answer.ToString();

}

async Task<string> DeleteUser(HttpRequestMessage message, int u_id)
{
    var json = JsonSerializer.Serialize(u_id);
    message.Content = new StringContent(json);
    var response = await client.SendAsync(message);
    var answer = await response.Content.ReadAsStringAsync();
    return answer.ToString();

}


