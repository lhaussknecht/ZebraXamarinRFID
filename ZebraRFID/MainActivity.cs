using Android.App;
using Android.Widget;
using Android.OS;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.ScanAndPair;

namespace ZebraRFID
{
    [Activity(Label = "ZebraRFID", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, EMDKManager.IEMDKListener
    {
        private EMDKManager _emdkManager;
        private ScanAndPairManager _scanAndPair;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            EMDKManager.GetEMDKManager(Application.Context, this);
        }

        public void OnClosed()
        {
            if (_emdkManager == null) return;
            _emdkManager.Release();
            _emdkManager = null;
        }

        public void OnOpened(EMDKManager emdkManager)
        {
            _emdkManager = emdkManager;

            _scanAndPair = (ScanAndPairManager) _emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Scanandpair);
            _scanAndPair.StatusEvent += (sender, args) =>
            {
                System.Diagnostics.Debug.WriteLine(args.P0);
            };

            _scanAndPair.ScanAndPair("1234");
            
        }
    }
}

