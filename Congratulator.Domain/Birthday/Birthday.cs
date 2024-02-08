namespace Congratulator.Domain.Birthday
{
    public class Birthday
    {
        public Guid Id { get; set; }
        public string Name { get; set;  } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }

    }
}
