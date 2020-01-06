using System.Text;
using static System.String;

namespace Search
{
    public partial class Hotel
    {
        public override string ToString()
        {
            var builder = new StringBuilder();

            if (!IsNullOrEmpty(HotelId))
            {
                builder.AppendFormat("HotelId: {0}\n", HotelId);
            }

            if (!IsNullOrEmpty(HotelName))
            {
                builder.AppendFormat("Name: {0}\n", HotelName);
            }

            if (!IsNullOrEmpty(Description))
            {
                builder.AppendFormat("Description: {0}\n", Description);
            }

            if (!IsNullOrEmpty(DescriptionFr))
            {
                builder.AppendFormat("Description (French): {0}\n", DescriptionFr);
            }

            if (!IsNullOrEmpty(Category))
            {
                builder.AppendFormat("Category: {0}\n", Category);
            }

            if (Tags != null && Tags.Length > 0)
            {
                builder.AppendFormat("Tags: [ {0} ]\n", Join(", ", Tags));
            }

            if (ParkingIncluded.HasValue)
            {
                builder.AppendFormat("Parking included: {0}\n", ParkingIncluded.Value ? "yes" : "no");
            }

            if (LastRenovationDate.HasValue)
            {
                builder.AppendFormat("Last renovated on: {0}\n", LastRenovationDate);
            }

            if (Rating.HasValue)
            {
                builder.AppendFormat("Rating: {0}\n", Rating);
            }

            return builder.ToString();
        }
    }
}