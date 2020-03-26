using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        SfListView ListView;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            ListView.SelectionChanging += ListView_SelectionChanging;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView.SelectionChanging -= ListView_SelectionChanging;
            ListView = null;
            base.OnDetachingFrom(bindable);
        }

        private void ListView_SelectionChanging(object sender, ItemSelectionChangingEventArgs e)
        {
            for (int i = 0; i < e.AddedItems.Count; i++)
            {
                var item = e.AddedItems[i] as Contacts;
                item.IsSelected = true;
            }

            for (int i = 0; i < e.RemovedItems.Count; i++)
            {
                var item = e.RemovedItems[i] as Contacts;
                item.IsSelected = false;
            }
        }
    }
}