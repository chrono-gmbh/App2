using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using PhoneWord_TT.Adapter;
using PhoneWord_TT.Objekte;

namespace PhoneWord_TT.Acitvity
{
    [Activity(Label = "TranslateHistory")]
    public class TranslateHistoryActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TranslateHistory_Layout);

          //  TextView historyItems = FindViewById<TextView>(Resource.Id.historyItems);
          //  historyItems.Text = "Items in history - " + Convert.ToString(Core.PhoneTranslator.GetHistoryItemCount());
            // Create your application here
            ListView historyItems = FindViewById<ListView>(Resource.Id.List);
            historyItems.Adapter = new HistoryAdapter(this, Core.PhoneTranslator.GetHistoryList());
           // historyItems.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Core.PhoneTranslator.GetHistoryList());
            historyItems.ItemClick += historyItems_ItemClick ; 
        }

        private void historyItems_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ListView listView = sender as ListView;
            TranslateObject item;
            
            item = Core.PhoneTranslator.GetHistoryList()[e.Position];
            //Android.Widget.Toast.MakeText(this, item, Android.Widget.ToastLength.Short).Show();

            Intent intent = new Intent(this,typeof(MainActivity)).SetFlags(ActivityFlags.ReorderToFront);;
            SharedObjects.tObject = item;
            //intent.PutExtra(PutExtra("phoneNumber",item);
            SetResult(Result.Ok, intent);
            Finish();
          
        }

        
    }
}