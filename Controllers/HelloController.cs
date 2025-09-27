namespace Name
{
  public class HelloController
  {
    public string SayHello(string name)
    {
      return $"Hello, {name}!";
    }

<<<<<<< HEAD
    public string goodbai(string name){
        return name;
=======
    public string SayGoodbye(string name)
    {
      return $"Goodbye, {name}!";
    }

    public string goodbyebye(string name)
    {
      return name;
>>>>>>> origin/hung_hs_bai_tap_git
    }
  }
}