using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Listener_;

internal class User
{
    private static int _id = 1;
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public User(string? name, string? surname)
    {
        Id = _id++;
        Name = name;
        Surname = surname;
    }
}
