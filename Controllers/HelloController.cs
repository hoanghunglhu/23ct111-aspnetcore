namespace Name
{
  public class HelloController
  {
    public string SayHello(string name)
    {
      return $"IT thích nuôi cá, {name}!";
    }

    public string SayGoodbye(string name)
    {
      return $"Goodbye, {name}!";
    }
  }
}