using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneTranslator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public const double MyBorderWidth = 3.5;

        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        public void OnTranslate(object obj, EventArgs e)
        {
            var enteredNumber = phoneNumberText.Text;
            translatedNumber = PhoneWordTranslator.ToNumber(enteredNumber);

            if (!String.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = $"Call {translatedNumber}";
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        public async void OnCall(object obj, EventArgs e)
        {
            if (await DisplayAlert(
                "Dial a Number",
                $"Would You Like To Call {translatedNumber}?",
                "Yes",
                "No"))
            {
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone Number Was Not Valid", "Ok");
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable To Dial", "Phone Dialing Not Supported", "Ok");
                }
                catch (Exception)
                {
                    await DisplayAlert("Unable To Dial", "Phone Dialing Failed", "Ok");
                }
            };
        }

        private void callButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}