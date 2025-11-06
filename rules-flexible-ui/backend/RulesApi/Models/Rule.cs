namespace RulesApi.Models
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? Condition { get; set; }
        public string? Action { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
