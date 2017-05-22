using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MaxMind.GeoIP2.Responses;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace IPLocation.Models
{
    public class QueryResult
    {
        public string IsoCode { get; }
        public string Country { get; }
        public string City { get; }
        public string Postal { get; }
        public Location? Location { get; }
        public int? AccuracyRadius { get; }
        public int? AverageIncome { get; }
        public int? PopulationDensity { get; }
        public int? AsNumber { get; }
        public string AsOrganization { get; }
        public string Isp { get; }
        public string Domain { get; }
        public string Organization { get; }
        public string UserType { get; }

        private static HotSpot _ellipseHotSpot =
            new HotSpot
            {
                X = 0.5,
                Y = 0.5,
                XUnits = HotSpotUnit.Fraction,
                YUnits = HotSpotUnit.Fraction
            };

        private static SolidColorBrush _ellipseFill =
            new SolidColorBrush(Color.FromArgb(0x7f, 0xf4, 0x43, 0x36));
        public MapEllipse ToMapEllipse()
        {
            if (!Location.HasValue) return null;
            var ellipse = new MapEllipse
            {
                Location = Location.Value,
                Width = AccuracyRadius ?? 1,
                Height = AccuracyRadius ?? 1,
                Fill = _ellipseFill
            };
            MapLayer.SetHotSpot(ellipse, _ellipseHotSpot);
            return ellipse;
        }

        public QueryResult(AbstractCityResponse response)
        {
            IsoCode = response.Country.IsoCode;
            Country = response.Country.Name;
            City = response.City.Name;
            Postal = response.Postal.Code;
            if (response.Location.Longitude.HasValue && response.Location.Latitude.HasValue)
                Location = new Location(response.Location.Latitude.Value, response.Location.Longitude.Value);
            AccuracyRadius = response.Location.AccuracyRadius;
            AverageIncome = response.Location.AverageIncome;
            PopulationDensity = response.Location.PopulationDensity;
            AsNumber = response.Traits.AutonomousSystemNumber;
            AsOrganization = response.Traits.AutonomousSystemOrganization;
            Domain = response.Traits.Domain;
            Isp = response.Traits.Isp;
            Organization = response.Traits.Organization;
            UserType = response.Traits.UserType;
        }
    }
}
