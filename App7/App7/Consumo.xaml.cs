using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.SimpleAudioPlayer;
using Plugin.AudioRecorder;
using System.Timers;
using System.Reflection;
using System.Collections;

namespace App7
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Consumo : ContentPage
	{
        AudioPlayer player;
        ArrayList listaArquiv = new ArrayList();
        public Consumo ()
		{
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            //listaArquivo.ItemsSource = lista;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }


        protected override void OnAppearing()
        {
           // var meuarquivo = new List<Arquivo>();

            var arquivos = Directory.EnumerateFiles(App.PastaDiretorio, "*.wav");
            foreach (var nomearquivo in arquivos)
            {
                //File.Delete(nomearquivo);
                int indice = nomearquivo.LastIndexOf('/');
                listaArquiv.Add(nomearquivo.Substring(indice + 1));
               // meuarquivo.Add(listaArquiv);
                
               
            }
            listaArquivo.ItemsSource = listaArquiv;
            //listaArquivo.ItemsSource = App.listaRecorder;
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //var valor = sender;
            //var eh = e;

            bool delet = await DisplayAlert("Ouvir", "Dar Play?" + e.ToString() + "?", "OK", "Cancelar");

            if (delet)
            {
                string nomeArq = Path.Combine(App.PastaDiretorio, $"{e.SelectedItem.ToString()}");
                player.Play(nomeArq);

                //await Navigation.PopAsync();
                await Navigation.PushAsync(new Consumo());
            }
        }

    }
}