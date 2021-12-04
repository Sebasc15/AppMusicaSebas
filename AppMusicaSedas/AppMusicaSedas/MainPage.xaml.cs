using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMusicaSedas
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnElectronica_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Electronica());
        }

        private void BtnRap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Rap());
        }

        private void BtnMetal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Metal());
        }
    }
}
