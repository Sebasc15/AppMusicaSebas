using AppMusicaSedas.Models;
using AppMusicaSedas.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMusicaSedas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Electronica : ContentPage
    {
        List<Cancion> canciones = new List<Cancion>();
        public Electronica()
        {
            InitializeComponent();
            CargarCanciones();
        }
        
        private async void CargarCanciones()
        {
            HttpClient cliente = new HttpClient();
            string URL = "https://apimusicasebas.azurewebsites.net/api/Canciones";

            var resultdados = await cliente.GetAsync(URL);

            var json = resultdados.Content.ReadAsStringAsync().Result;

            canciones = Cancion.FromJson(json);

            var layout = new StackLayout { Padding = new Thickness(5,5,5,5)};
            
            if(canciones.Count > 0)
            {
                int imagenfila = 0;
                int columna = 0;
                int nombrefila = 1;
                

                gridcanciones.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridcanciones.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridcanciones.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridcanciones.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridcanciones.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                List<Cancion> cancionesElectronica = new List<Cancion>();

                foreach(var item in canciones)
                {
                    if(item.Genero == "Electronica")
                    {
                        cancionesElectronica.Add(item);
                    }
                }

                if(cancionesElectronica.Count > 0)
                {
                    var imagen = new Image();
                    foreach (var item in cancionesElectronica)
                    {

                        imagen = new Image { Source = item.UrlImagen, WidthRequest = 150, HeightRequest = 200, Aspect = Aspect.AspectFit };
                        imagen.ClassId = item.Id.ToString();
                        var NombreCancion = new Label { Text = item.NombreCancion.ToString() };
                        NombreCancion.HorizontalOptions = LayoutOptions.Center;
                        NombreCancion.VerticalOptions = LayoutOptions.Center;

                        if (columna > 0)
                        {
                            gridcanciones.Children.Add(imagen, columna, imagenfila);
                            //NombreCancion.Padding = new Thickness(70,15,15,0);
                            gridcanciones.Children.Add(NombreCancion, columna, nombrefila);

                            columna = 0;
                            imagenfila += 2;
                            nombrefila += 2;
                            gridcanciones.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                            gridcanciones.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                        }
                        else
                        {
                            gridcanciones.Children.Add(imagen, columna, imagenfila);
                            //NombreCancion.Padding = new Thickness(50, 15, 15, 0);
                            gridcanciones.Children.Add(NombreCancion, columna, nombrefila);
                            columna++;
                        }
                        var tapCancion = new TapGestureRecognizer();
                        tapCancion.Tapped += TapCancion_Tapped;
                        imagen.GestureRecognizers.Add(tapCancion);
                    }

                    layout.Children.Add(gridcanciones);
                    this.Content = layout;
                }
                else
                {
                    Cargandoinfo.Text = "No se encontraron canciones de Electronica";
                    Indicator.IsRunning = false;
                    Indicator.IsVisible = false;
                }
                

            }
            else
            {
                Cargandoinfo.Text = "No se encontro informacion en la API";
                Indicator.IsRunning = false;
                Indicator.IsVisible = false;
            }
        }

        private async void TapCancion_Tapped(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            long index = Int32.Parse(image.ClassId);
            await Navigation.PushAsync(new ReproducirMusica(index));
        }
    }
}