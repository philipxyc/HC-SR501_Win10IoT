using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using Windows.UI.Xaml.Media;

namespace HC_SR501
{
    /// <summary>
    /// Using your HC-SR501 sensor with Windows 10 for IoT
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Init your pin
        private const int PIN = 4;
        GpioController gpio = GpioController.GetDefault();
        GpioPin readPin;
        int? i = null;

        public MainPage()
        {
            this.InitializeComponent();
            if (gpio == null)
            {
                readPin = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }
            else GpioStatus.Text = "initializd correctly";
            readPin = gpio.OpenPin(PIN);
            readPin.SetDriveMode(GpioPinDriveMode.Input);
            //Event when value changed
            readPin.ValueChanged += (sender, args) =>
                  {
                      if (readPin.Read() == GpioPinValue.High)
                      {
                          i = 1;
                      }
                      else
                      {
                          i = 0;
                      }
                      
                  };
            switch (i)
            {
                case null:
                    break;
                case 0:
                    SayGb();
                    break;
                case 1:
                    SayHi();
                    break;
                default:
                    break;
            }
        }
        public void SayHi()
        {
            GpioStatus.Text = "Hi";
            Graph.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
        }
        public void SayGb()
        {
            GpioStatus.Text = "Nobody here";
            Graph.Fill = new SolidColorBrush(Windows.UI.Colors.LightGray);
        }
    }
}
