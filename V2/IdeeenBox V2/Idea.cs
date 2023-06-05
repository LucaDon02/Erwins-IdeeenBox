using System;
using System.Collections.Generic;

namespace IdeeenBox;

[Serializable]
public class Idea
{
    public User Owner { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> SharedWith { get; set; }
    public List<string> SharedWithEmail { get; set; }

    public Idea(User owner, string name, string description)
    {
        Owner = owner;
        Name = name;
        Description = description;
        SharedWith = new List<User>();
        SharedWithEmail = new List<string>();
    }
}