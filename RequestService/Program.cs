using HtmlAgilityPack;
using RequestService.Models;

namespace RequestService;

class Program
{
  public static async Task Main(string[] args)
  {

    BeActiveParser gym = new BeActiveParser();
    await gym.GetValue();
    
  }
}