namespace Name
{
  public class HelloController
  {
    public string SayHello(string name)
    {
      return $"xin chao moi nguoi nha!!!!1, {name}!";
    }

    public string SayGoodbye(string name)
    {
      return $"Goodbye, {name}!";
    }
  }
}