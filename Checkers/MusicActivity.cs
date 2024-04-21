using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    [Activity(Label = "MusicActivity")]
    public class MusicActivity : Activity
    {
        Button btnStop;
        Button btnStart;
        Button btnMute;

        ////// --1--
        MediaPlayer mp;
        AudioManager am;

        [Obsolete]

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_music);


            // Create your application here
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnStop = FindViewById<Button>(Resource.Id.btnStop);
            btnMute = FindViewById<Button>(Resource.Id.btnMute);
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            btnMute.Click += BtnMute_Click;
        }
        int count = 0;
        private void BtnMute_Click(object sender, EventArgs e)
        {
            AudioManager am = (AudioManager)GetSystemService(Context.AudioService);
            am.SetStreamVolume(Stream.Music, 5, VolumeNotificationFlags.PlaySound);//Notification/VoiceCall//Alarm
            if (count % 2 == 0)
            {
                am.SetStreamMute(Stream.Music, true);//true ==>mute
                btnMute.Text = "Continue";
            }
            else
            {
                am.SetStreamMute(Stream.Music, false);//false ==>Unmute
                btnMute.Text = "Mute";
            }
            count++;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MyMusicService));
            StartService(intent);
        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MyMusicService));
            StopService(intent);
        }
    }
}