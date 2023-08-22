namespace Backend.TestEnpoints
{
    public record TestDataDTO
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Name";
    }

    public record TestDataWithFileFormDTO
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Name";

        public IFormFile? FormFile { get; set; }
    }
}