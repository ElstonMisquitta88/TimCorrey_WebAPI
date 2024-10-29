namespace BlazorApiClient.Logic;

public class DemoLogic : IDemoLogic
{
    public int Value1 { get; set; }
    public int Value2 { get; set; }


    public DemoLogic()
    {
        Value1 = Random.Shared.Next(1, 100);
        Value2 = Random.Shared.Next(200, 300);
    }
}
