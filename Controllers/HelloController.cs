namespace Name
{
  public class HelloController
  {
    public string SayHello(string name)
    {
      return $"Hello, {name}!";
    }
    public string SayBye(string name)
    {
        return $"Bai, {name}|";
    }
  }
}