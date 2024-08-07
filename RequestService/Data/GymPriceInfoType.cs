
namespace RequestService.Data
{
  public class GymPriceInfoType
  {
    public uint? Id { get; set; }
    public string GymName { get; set; } = String.Empty;
    public decimal PricePerMonth { get; private set; } = 0;
    public string PromoDescription { get; set; } = String.Empty;
    public Int64 UnixTimestamp { get; set; }
    public string Date { get; set; }

    private decimal _totalPrice;
    public decimal TotalPrice
    {
      get => _totalPrice;
      set { _totalPrice = value; CalculatePricePerMonth();}

    }

    private decimal _span;
    public decimal Span
    {
      get => _span;
      set
      {
        _span = value;
        CalculatePricePerMonth();
      }
    }

    public GymPriceInfoType()
    {
      GetTimestamp();
    }
    private void CalculatePricePerMonth()
    {
      if (Span > 0)
      {
        PricePerMonth = Decimal.Round(TotalPrice / Span, 2);

      }
      
    }

    private void GetTimestamp()
    {
      var now = DateTimeOffset.Now;
      UnixTimestamp = now.ToUnixTimeSeconds();
      Date = now.ToString("yyyy-MM-dd HH:mm:ss");

    }
    
  }

}
