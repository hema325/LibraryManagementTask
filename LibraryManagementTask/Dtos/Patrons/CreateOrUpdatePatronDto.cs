namespace LibraryManagementTask.Dtos.Patrons
{
    public class CreateOrUpdatePatronDto
    {
        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public string? Notes { get; set; }
    }
}
