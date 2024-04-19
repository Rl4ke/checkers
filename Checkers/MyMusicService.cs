using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.App;
using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Widget;

namespace Checkers
{
    [Service(Label = "MyMusicService", Exported = true)]   //----write service to manifest file 
    [IntentFilter(new String[] { "com.companyname.checkers.MyMusicService" })]
    public class MyMusicService : Service
    {
        IBinder binder;//null not in bagrut 
        MediaPlayer mp;
        AudioManager am;

        [Obsolete]
        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {

            //---- משמיע את קובץ השמע
            mp = MediaPlayer.Create(this, Resource.Drawable.musicCheckers);
            mp.Start();

            // ==== משמש לשליטה ברכיב קול =======

            am = (AudioManager)GetSystemService(Context.AudioService);
            am.SetStreamVolume(Stream.Music, 5, VolumeNotificationFlags.PlaySound);//Notification/VoiceCall//Alarm
            // am.SetStreamMute(Stream.Music, false);//true ==>mute
            //int maxVolume = am.GetStreamMaxVolume(Stream.Music);

            // Return the correct StartCommandResult for the type of service you are building
            //return StartCommandResult.NotSticky;//במידה ונחדש פעילות של המוזיקה היא תתחיל מהתחלה
            return StartCommandResult.Sticky;//במידה ונחדש פעילות של המוזיקה היא תמשיך מאותה נקודה
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (mp != null)
            {
                mp.Stop();
                mp.Release();
                mp = null;
            }

        }
    }
}