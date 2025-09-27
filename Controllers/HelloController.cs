namespace Name
{
  public class HelloController
  {
    public string SayHello(string name)
    {
      return $"chao anh em, {name}!";
    }

    public string SayGoodbye(string name)
    {
      return $"Goodbye, {name}!";
    }
  }
}