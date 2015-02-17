namespace AlertGeneratorService
{
    public interface IAlertThreshold
    {
        int SubscriberId { get; set; }
        int AllowedVolume { get; set; }
        int AllowedNoOfFiles { get; set; }
    }
}
