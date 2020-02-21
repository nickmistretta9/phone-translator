using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhoneTranslator
{
    public class OldMainPage : ContentPage
    {
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;
        string translatedNumber;

        public OldMainPage()
        {
            this.Padding = new Thickness(20, 20, 20, 20);
            var layout = new StackLayout
            {
                Spacing = 15
            };

            layout.Children.Add(new Label { 
                Text = "Enter a Phoneword:", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) 
            });

            layout.Children.Add(phoneNumberText = new Entry { 
                Text = "1-855-XAMARIN" 
            });

            layout.Children.Add(translateButton = new Button { 
                Text = "Translate"
            });

            layout.Children.Add(callButton = new Button { 
                Text = "Call", IsEnabled = false 
            });

            translateButton.Clicked += OnTranslate;
            callButton.Clicked += OnCall;

            this.Content = layout;
        }
        
        public void OnTranslate(object obj, EventArgs e)
        {
            var enteredNumber = phoneNumberText.Text;
            translatedNumber = PhoneWordTranslator.ToNumber(enteredNumber);

            if(!String.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = $"Call {translatedNumber}";
            } else
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
                } catch(ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone Number Was Not Valid", "Ok");
                } catch(FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable To Dial", "Phone Dialing Not Supported", "Ok");
                } catch(Exception)
                {
                    await DisplayAlert("Unable To Dial", "Phone Dialing Failed", "Ok");
                }
            };
        }
    }
}
