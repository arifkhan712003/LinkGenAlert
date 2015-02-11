namespace AlertGeneratorService
{
    public interface IAlertThreshold
    {
        int TenentId { get; set; }
        int AllowedVolume { get; set; }
        int AllowedNoOfFiles { get; set; }
    }
}
