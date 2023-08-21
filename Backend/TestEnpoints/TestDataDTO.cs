namespace Backend.TestEnpoints
{
    public class TestDataDTO
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Name";
    }

    public class TestDataWithFileFormDTO
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Name";

        public IFormFile? FormFile { get; set; }
    }
}