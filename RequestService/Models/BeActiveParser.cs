using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace RequestService.Models;

using Data;

public class BeActiveParser : IHtmlParser<GymPriceInfoType>
{
  private string Name { get; set; }
  private string Url { get; set; }
  private IEnumerable<HtmlElementType> HtmlElementList { get; set; }

  public BeActiveParser()
  {
    this.Name = "beActive";
    this.Url = "https://www.ebeactive.pl/pl/oferta-r60.html";
    // TODO: create the model to add description to specific xpath items 
    this.HtmlElementList = new List<HtmlElementType>(){
        new (){Name = "price", Xpath = "//*[@id='offer']/div/div[2]/div[2]/h3[2]/text()"},
        new (){Name = "span", Xpath = "//*[@id='offer']/div/div[2]/div[2]/h3[1]/text()" },
        new (){Name = "extras", Xpath = "//*[@id='offer']/div/div[2]/div[2]/h3[1]/span" },

    };

  }

  

  private decimal GetDecimals(string value)
  {
    string pattern = @"\d+(\.\d+)?";
    Regex reg = new Regex(pattern);
    decimal result = 0;

    var match = reg.Match(value.Trim().Replace(',', '.')); 
    if (match.Success)
    {
      result = decimal.Parse(match.Value);
      Console.WriteLine(result);
    }

    return result;
  }
// TODO: move the CreateFromHtmlElement function 
  private GymPriceInfoType CreateFromHtmlElement(IEnumerable<HtmlElementType> htmlElementList)
  {
    var newGymPriceInfo = new GymPriceInfoType();
    decimal totalPrice = 0;
    decimal totalMonths = 0;
    foreach (var element in htmlElementList)
    {
      if (element.Name == "price")
      {
        totalPrice = element.Value;
      } else if (element.Name == "months")
      {
        totalMonths += element.Value;
      }
      else
      {
        if (element.Value < 10)
        {
          totalMonths += element.Value;
        }
      }
      
    }

    newGymPriceInfo.GymName = this.Name;
    newGymPriceInfo.TotalPrice = totalPrice;
    newGymPriceInfo.PricePerMonth = Math.Round(totalPrice / totalMonths, 2);
    newGymPriceInfo.TotalMonths = totalMonths;

    // TODO: Add the logger function
    Console.WriteLine("New Update:\n");
    Console.WriteLine($"{newGymPriceInfo.GymName} \n" +
                      $"- Price per month: {newGymPriceInfo.PricePerMonth}\n" +
                      $"- Total months: {newGymPriceInfo.TotalMonths}\n" +
                      $"- Total price: {newGymPriceInfo.TotalPrice}\n");

    return newGymPriceInfo;
  }

  public async Task<GymPriceInfoType> GetValue()
  {
    var fetcher = new HtmlFetcher(Url);
    
    HtmlDocument htmlDocument = await fetcher.GetHtmlDocAsync() ?? throw new InvalidOperationException();
    
    foreach (var element in HtmlElementList)
    {
      try
      {
        var node = htmlDocument.DocumentNode
          .SelectNodes(element.Xpath)
          .SingleOrDefault(x => x.InnerText.Trim().Length > 0);
        if (node != null)
        {
          element.Value = GetDecimals(node.InnerText);
          
        }
        
      }
      catch(Exception ex)
      {
        Console.WriteLine($"Error occured: {ex.Message}");
      }
      
    }
    
    
    return CreateFromHtmlElement(HtmlElementList);
  }
}
