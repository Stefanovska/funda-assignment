namespace funda_assignment.Models;

public class Agent
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NumberOfProperties { get; set; }

    public Agent(int id, string name, int num)
    {
        Id = id;
        Name = name;
        NumberOfProperties = num;
    }
}
