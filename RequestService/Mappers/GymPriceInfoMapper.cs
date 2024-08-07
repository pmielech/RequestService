using System.Linq.Expressions;
using AutoMapper;
using RequestService.Data;

namespace RequestService.Mappers;

public class GymPriceInfoMapper
{
    private string SourceName { get; set; }
    public GymPriceInfoMapper(string name)
    {
        SourceName = name;
    }
    public GymPriceInfoType CreateFromHtmlElement(IEnumerable<HtmlElementType> htmlElementList)
    {
        decimal totalPrice = 0;
        decimal totalMonths = 0;
    
        // TODO: REFACTOR THIS
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
        // // TODO: Add the logger function
        //
        // Console.WriteLine("New Update:");
        // Console.WriteLine($"{newGymPriceInfo.GymName} \n" +
        //                   $"- Price per month: {newGymPriceInfo.PricePerMonth}\n" +
        //                   $"- Total months: {newGymPriceInfo.Span}\n" +
        //                   $"- Total price: {newGymPriceInfo.TotalPrice}\n");

        return new GymPriceInfoType()
        {
            GymName = SourceName, 
            TotalPrice = totalPrice,
            Span = totalMonths
        };
    }
}