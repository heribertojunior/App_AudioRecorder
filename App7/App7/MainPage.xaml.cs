﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Plugin.SimpleAudioPlayer;
using Plugin.AudioRecorder;
using System.Timers;
using System.Reflection;


namespace App7
{
    

    public partial class MainPage : ContentPage
    {

        AudioRecorderService recorder;
        AudioPlayer player;
        bool isTimerRunning = false;
        int seconds = 0, minutes = 0;
        string nome ;

        public MainPage()
        {
            
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                this.Padding = new Thickness(0, 28, 0, 0);

            recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };

            player = new AudioPlayer();
            player.FinishedPlaying += Finish_Playing;


        }


        void Finish_Playing(object sender, EventArgs e)
        {
            bntRecord.IsEnabled = true;
            bntRecord.BackgroundColor = Color.FromHex("#7cbb45");
            bntPlay.IsEnabled = true;
            bntPlay.BackgroundColor = Color.FromHex("#7cbb45");
            bntStop.IsEnabled = false;
            bntStop.BackgroundColor = Color.Silver;
            lblSeconds.Text = "00";
            lblMinutes.Text = "00";
        }

        async void Record_Clicked(object sender, EventArgs e)
        {
            seconds++;

            if (!recorder.IsRecording)
            {
                seconds = 0;
                minutes = 0;
                isTimerRunning = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                    

                    if (seconds.ToString().Length == 1)
                    {
                       lblSeconds.Text = "0" + seconds.ToString();
                    }
                    else
                    {
                       lblSeconds.Text = seconds.ToString();
                    }
                    if (seconds == 60)
                    {
                        minutes++;
                        seconds = 0;

                        if (minutes.ToString().Length == 1)
                        {
                            lblMinutes.Text = "0" + minutes.ToString();
                        }
                        else
                        {
                            lblMinutes.Text = minutes.ToString();
                        }

                        lblSeconds.Text = "00";
                    }
                    return isTimerRunning;
                });

                recorder.StopRecordingOnSilence = IsSilence.IsToggled;
                var audioRecordTask = await recorder.StartRecording();

                bntRecord.IsEnabled = false;
                bntRecord.BackgroundColor = Color.Silver;
                bntPlay.IsEnabled = true;
                bntPlay.BackgroundColor = Color.Silver;
                bntStop.IsEnabled = true;
                bntStop.BackgroundColor = Color.FromHex("#7cbb45");

                
            }
        }

        async void Stop_Clicked(object sender, EventArgs e)
        {
            
            StopRecording();
            await recorder.StopRecording();
            
           
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Consumo());
        }
        void Teste(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PageConf());
        }

        void StopRecording()
        {

            isTimerRunning = false;
            bntRecord.IsEnabled = true;
            bntRecord.BackgroundColor = Color.FromHex("#7cbb45");
            bntPlay.IsEnabled = true;
            bntPlay.BackgroundColor = Color.FromHex("#7cbb45");
            bntStop.IsEnabled = false;
            bntStop.BackgroundColor = Color.Silver;
            lblSeconds.Text = "00";
            lblMinutes.Text = "00";

            

        }

        public void copyFileSound() {

            if (File.Exists(App.PastaDiretorio + "/ARS_" + nome + ".wav")) { 
                File.Delete(App.PastaDiretorio + "/ARS_" + nome + ".wav");
            }
            
            File.Copy(recorder.GetAudioFilePath(), App.PastaDiretorio + "/ARS_" + nome + ".wav");
        }

        void Add_Clicked(object sender, EventArgs e)
        {

            copyFileSound();


        }

        void Play_Clicked(object sender, EventArgs e)
        {
            try
            {
              
                var FilePath = recorder.GetAudioFilePath();
                StopRecording();
                    player.Play(FilePath);
                
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                nome = picker.Items[selectedIndex];
            }
        }

    }
}
