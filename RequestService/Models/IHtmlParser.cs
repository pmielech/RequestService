namespace RequestService.Models;

public interface IHtmlParser<T>
{
  Task<T> GetValue();

}
