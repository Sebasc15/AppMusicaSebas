using AppMusicaSedas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMusicaSedas.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReproducirMusica : ContentPage
    {
        long id;
        public ReproducirMusica(long index)
        {
            id = index;
            Device.SetFlags(new string[] { "MediaElement_Experimental" });
            InitializeComponent();
            Informacion();
            
        }

        private async void Informacion()
        {
            try
            {


                HttpClient httpClient = new HttpClient();

                string URL = $"https://apimusicasebas.azurewebsites.net/api/Canciones/{id}";

                var resultado = await httpClient.GetAsync(URL);

                var json = resultado.Content.ReadAsStringAsync().Result;
                Cancion cancion = new Cancion();
                cancion = JsonConvert.DeserializeObject<Cancion>(json);

                RueditaCargar.IsVisible = false;
                RueditaCargar.IsRunning = false;

                ArtistaLbl.Text = "Artista: " + cancion.Artista;
                Titulo_Cancionlbl.Text = "Cancion: " + cancion.NombreCancion;
                AlbumLbl.Text = "Album: " + cancion.Album;
                GeneroLbl.Text = "Genero: " + cancion.Genero;
                pausebuton.IsVisible = true;

                this.BindingContext = cancion;
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void Playbuton_Clicked(object sender, EventArgs e)
        {
            try
            {
                playbuton.IsVisible = false;
                pausebuton.IsVisible = true;
                mediaelement.Play();
            }catch (Exception ex)
            {
                Console.Write (ex.Message);
            }
        }

        private void Pausebuton_Clicked(object sender, EventArgs e)
        {
            try
            {
                playbuton.IsVisible = true;
                pausebuton.IsVisible = false;
                mediaelement.Pause();
               
            }catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}