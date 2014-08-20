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

using PhoneWord_TT.Objekte;
namespace PhoneWord_TT.Adapter
{
    class HistoryAdapter:BaseAdapter<TranslateObject>
    {
        List<TranslateObject> items;
        Activity context;

        public HistoryAdapter(Activity context, List<TranslateObject> items)
       : base()
        {
            this.context = context;
            this.items = items;
        }

        public override int Count
        {
            get { return items.Count; }
        }

       /* public override TranslateObject GetItem(int position)
        {
            return items.ElementAt<TranslateObject>(position);
        }*/
        public override TranslateObject this[int position]
        {
            get { return items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TranslateObject tObject = items[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);
            }

            view.FindViewById<TextView>(Resource.Id.RawText).Text = tObject.rawNumber;
            view.FindViewById<TextView>(Resource.Id.TranslatedText).Text = tObject.translatedNumber;
            
            return view;
        }
    }
}