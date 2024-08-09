using System.Text.RegularExpressions;
using HtmlAgilityPack;
using RequestService.Mappers;
using RequestService.Models.GymMembershipType;

namespace RequestService.Models;

using Data;

public class BeActiveParser : IHtmlParser<GymMembershipCreateVm>
{
  private string Name { get; set; }
  private string Url { get; set; }
  private GymPriceInfoMapper _mapper;
  private IEnumerable<HtmlElementType> HtmlElementList { get; set; }

  public BeActiveParser()
  {
    Name = "beActive";
    Url = "https://www.ebeactive.pl/pl/oferta-r60.html";
    _mapper = new GymPriceInfoMapper(Name);
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
    }

    return result;
  }

  private decimal ExtractNodeValue(ref HtmlDocument htmlDocument, string xpath)
  {
    var node = htmlDocument.DocumentNode
      .SelectNodes(xpath)
      .SingleOrDefault(x => x.InnerText.Trim().Length > 0);
    if (node != null)
    {
      return GetDecimals(node.InnerText);
    }
    else
    {
      return 0;
    }
  }
  public async Task<GymMembershipCreateVm> GetValue()
  {
    var fetcher = new HtmlFetcher(Url);
    HtmlDocument htmlDoc = new HtmlDocument();
    try
    {
      htmlDoc = await fetcher.GetHtmlDocAsync() ?? throw new InvalidOperationException();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error occured: {ex.Message}");

    }

    
    foreach (var element in HtmlElementList)
    {
      try
      {
        element.Value = ExtractNodeValue(ref htmlDoc, element.Xpath);

      }
      catch(Exception ex)
      {
        Console.WriteLine($"Error occured: {ex.Message}");
      }
      
    }

    return _mapper.CreateFromHtmlElement(HtmlElementList);
  }
}
