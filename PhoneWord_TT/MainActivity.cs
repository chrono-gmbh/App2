using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PhoneWord_TT.Acitvity;
using Android.Util;
using PhoneWord_TT.Objekte;


namespace PhoneWord_TT
{
    [Activity(Label = "PhoneWord_TT", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);

            Button translateHistory = FindViewById<Button>(Resource.Id.TranslateHistory);

            callButton.Enabled = false;
            string translatedNumer = string.Empty;

            translateButton.Click += delegate
            {
                translatedNumer = Core.PhoneTranslator.ToNumber(phoneNumberText.Text);

                if (String.IsNullOrWhiteSpace(translatedNumer))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else
                {
                    callButton.Text = "Call -" + translatedNumer;
                    callButton.Enabled = true;

                    TranslateObject to = new TranslateObject();
                    to.rawNumber = phoneNumberText.Text;
                    to.translatedNumber = translatedNumer;
                    Core.PhoneTranslator.AddCallToHistory(to);
                }
            };

            callButton.Click += delegate
            {
                AlertDialog.Builder callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumer +"?" );
                callDialog.SetNeutralButton("Call", delegate
                    {
                        Intent callIntent = new Intent(Intent.ActionCall);
                        callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumer));
                        StartActivity(callIntent);
                    });
                callDialog.SetNegativeButton("Cancel", delegate { });

                callDialog.Show();
                
            };

            translateHistory.Click += delegate
            {
                Intent intent = new Intent(this, typeof(TranslateHistoryActivity));
                StartActivityForResult(intent,0);
                
            };

           

          //  button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //string text = data.GetStringExtra("phoneNumber") ?? "";

                if (!SharedObjects.tObject.Equals(null))
                {
                    EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
                    phoneNumberText.Text = SharedObjects.tObject.rawNumber;

                    Button callButton = FindViewById<Button>(Resource.Id.CallButton);
                    callButton.Enabled = true;
                    callButton.Text = "Call -" + SharedObjects.tObject.translatedNumber;

                }
            }
        }
    }
}

