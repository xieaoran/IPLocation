using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using IPLocation.Annotations;
using IPLocation.Models;
using MaxMind.GeoIP2;

namespace IPLocation.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public AuthToken AuthToken { get; set; }
        public WebServiceClient WebServiceClient { get; set; }

        private QueryResult _result;

        public QueryResult Result
        {
            get => _result;
            set
            {
                if(_result == value) return;
                _result = value;
                OnPropertyChanged();
            }
        }

        private int? _queriesRemaining;

        public int? QueriesRemaining
        {
            get => _queriesRemaining;
            set
            {
                if (_queriesRemaining == value) return;
                _queriesRemaining = value;
                OnPropertyChanged();
            }
        }

        public string IpAddress { get; set; }

        public void SaveLicense()
        {
            WebServiceClient = new WebServiceClient(AuthToken.UserId, AuthToken.LicenseKey);
        }

        public async Task Locate()
        {
            var response = await WebServiceClient.InsightsAsync(IpAddress);
            Result = new QueryResult(response);
            QueriesRemaining = response.MaxMind.QueriesRemaining;
        }

        public MainViewModel()
        {
            AuthToken = new AuthToken();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
