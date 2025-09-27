namespace Name
{
  public class HelloController
  {
    public string SayHello(string quanDown)
    {
      return $"Hello, {quanDown}!";
    }

    public string Eat(string quanDown)
    {
        return $"{quanDown} is eating";
    }

    public string SkibidiToilet(string quanDown)
    {
        return $"{quanDown} is skibidi toilet";
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