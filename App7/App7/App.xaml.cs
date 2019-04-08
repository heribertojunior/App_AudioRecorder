using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App7
{
    public partial class App : Application
    {
       
        public static string PastaDiretorio { get; set; }
        public static List<string> listaRecorder = new List<string>();
        public App()
        {
          
            InitializeComponent();
            PastaDiretorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
