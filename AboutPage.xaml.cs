using System;
using Microsoft.Maui.Controls;

namespace ListView_Euroopa_riigid
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void LearnMore_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Learn More", "This is where you can learn more about the app.", "OK");
        }
    }
}