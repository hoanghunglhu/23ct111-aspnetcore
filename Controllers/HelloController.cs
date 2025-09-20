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

    public string DiDaiDiIa(string quanDown)
    {
        return $"{quanDown} dang di skibidi";
    }
  }
}