using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client_;

internal class User
{
    private static int _id = 1;
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public override string ToString()
    {
        return $"{Id}. {Name} {Surname}";
    }
}
