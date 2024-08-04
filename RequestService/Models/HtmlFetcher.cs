using HtmlAgilityPack;
using System.Net;
namespace RequestService.Models
{
  public class HtmlFetcher
  {
    private readonly HttpClient _httpClient;
    public string? HtmlStringContent { get; set; }
    private string Url { get; set; }
    
    public HtmlDocument? HtmlDoc { get; set; }

    public HtmlFetcher(string url)
    {
      this.Url = url;
      _httpClient = new HttpClient();
      HtmlDoc = new HtmlDocument();
    }

    public async Task FetchHtmlAsync()
    {

      var response = await _httpClient.GetAsync(Url);

      if (response.StatusCode == HttpStatusCode.OK)
      {
        HtmlStringContent = await response.Content.ReadAsStringAsync();
        HtmlDoc.LoadHtml(HtmlStringContent);

      }
      else
      {
        this.HtmlStringContent = String.Empty;
      }


    }

    public async Task<HtmlDocument?> GetHtmlDocAsync()
    {
      await FetchHtmlAsync();
      return this.HtmlDoc;
    }


  }

}
