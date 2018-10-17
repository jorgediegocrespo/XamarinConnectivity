using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace XamarinConnectivity
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _networkAccess;
        private string _profile;

        public string NetworkAccess
        {
            get { return _networkAccess; }
            set
            {
                _networkAccess = value;
                OnPropertyChanged();
            }
        }

        public string Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged();
            }
        }

        public ICommand TestConnectivityCommand { get; set; }
        public ICommand NotifyConnectivityChangesCommand { get; set; }

        public MainPageViewModel()
        {
            CreateCommands();
        }

        private void CreateCommands()
        {
            TestConnectivityCommand = new Command(TestConnectivity);
            NotifyConnectivityChangesCommand = new Command(NotifyConnectivityChanges);
        }

        private void TestConnectivity()
        {
            SetNetworkAccess();
            SetNetworkProfile();
        }

        private void SetNetworkAccess()
        {
            var currentNetworkAccess = Connectivity.NetworkAccess;

            switch (currentNetworkAccess)
            {
                case Xamarin.Essentials.NetworkAccess.Internet:
                    NetworkAccess = "Local and internet access";
                    break;
                case Xamarin.Essentials.NetworkAccess.ConstrainedInternet:
                    //Limited internet access. Indicates captive portal connectivity, where local access to a web portal is provided, but access to the Internet requires that specific credentials are provided via a portal.
                    NetworkAccess = "Limited internet connectivity";
                    break;
                case Xamarin.Essentials.NetworkAccess.Local:
                    NetworkAccess = "Local connectivity";
                    break;
                case Xamarin.Essentials.NetworkAccess.None:
                    NetworkAccess = "No connectivity";
                    break;
                case Xamarin.Essentials.NetworkAccess.Unknown:
                default:
                    NetworkAccess = "Unknown connectivity";
                    break;
            }
        }

        private void SetNetworkProfile()
        {
            var currentProfiles = Connectivity.Profiles;

            //Possible values: Bluetooth, Cellular, Ethernet, Wifi, WiMAX, Other
            Profile = string.Join(", ", currentProfiles);
        }

        private void NotifyConnectivityChanges(object obj)
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            TestConnectivity();
        }
    }
}
