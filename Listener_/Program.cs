using Listener_;
using System.Net;
using System.Text.Json;

List<User> users = new List<User>()
{
    new User(name:"Nadir", surname:"Zamanov"),
    new User(name:"Ali", surname:"Valiyev"),
    new User(name:"Alim", surname:"Qasimov"),
    new User(name:"Sahib", surname:"Shakhayev"),
    new User(name:"Jahid", surname:"Huseynli"),
    new User(name:"Justin", surname:"Timberlake")
};

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:27001/");

while (true)
{
    listener.Start();
    var context = listener.GetContext();
    var request = context.Request;
    var response = context.Response;
    //response.ContentType = "application/json";
    StreamWriter sw = new StreamWriter(response.OutputStream);
    StreamReader sr = new StreamReader(request.InputStream);
    switch (request.HttpMethod)
    {
        case "GET":
            sw.WriteAsync(JsonSerializer.Serialize(users));
            Console.WriteLine("GET");
            break;
        case "POST":
            var user_data = JsonSerializer.Deserialize<string[]>(sr.ReadToEndAsync().Result);
            users.Add(new User(user_data[0],user_data[1]));
            sw.WriteAsync("User Added");
            break;
        case "PUT":
            var user_n_data = JsonSerializer.Deserialize<string[]>(sr.ReadToEndAsync().Result);
            int.TryParse(user_n_data[0], out int id);
            foreach (var user_item in users)
            {
                if (user_item.Id == id)
                {
                    users[users.IndexOf(user_item)] = new User(user_n_data[1], user_n_data[2]) { Id = id };
                    break;
                }
            }
            sw.WriteAsync("User Updated");
            break;
        case "DELETE":
            var u_id = JsonSerializer.Deserialize<int>(sr.ReadToEndAsync().Result);
            foreach (var user_item in users)
            {
                if (user_item.Id == u_id)
                {
                    users.Remove(user_item);
                    break;
                }
            }
            sw.WriteAsync("User Deleted");
            break;
    }

    sw.Close();
}
