namespace RequestService.Models.GymMembershipType;

public class GymMembershipCreateVm
{
    public uint? Id { get; set; }
    public string Name { get; set; }
    public decimal PricePerMonth { get; set; }
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
    
    public GymMembershipCreateVm()
    {
        GetDate();
    }
    private void CalculatePricePerMonth()
    {
        if (Span > 0)
        {
            PricePerMonth = Decimal.Round(TotalPrice / Span, 2);

        }
      
    }

    private void GetDate()
    {
        var now = DateTimeOffset.Now;
        Date = now.ToString("yyyy-MM-dd HH:mm:ss");

    }
}