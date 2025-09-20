namespace Name
{
  public class HelloController
  {
    public string SayHello(string name)
    {
      return $"Hello, {name}!";
    }

    public string SayGoodBye(string name)
    {
        return $"Goodbye, {name}!";
    }
  }
}