namespace Name
{
  public class HelloController
  {
    public string SayHello(string name)
    {
      return $"chao , {name}!";
    }
    public string SayGoodbye(string name)
    {
      return $"Goodbye, {name}!";
    }

    public string goodbyebye(string name)
    {
      return name;
    }
  }
}