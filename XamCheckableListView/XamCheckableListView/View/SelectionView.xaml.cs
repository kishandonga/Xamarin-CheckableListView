using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XamCheckableListView
{
    public partial class SelectionView : ContentPage
    {
        public event EventHandler<SelectedItemEventArgs> OnItemsSelected;
        private ObservableCollection<SelectionViewModel> models;
        private bool mSelection = false;
        private bool InWork = false;
       
        public SelectionView(List<SelectionViewModel> list)
        {
            InitializeComponent();
            
            models = new ObservableCollection<SelectionViewModel>();
            listView.ItemsSource = models;
            setItems(list);
        }

        public bool MultipleSelection
        {
            get
            {
                return mSelection;
            }
            set
            {
                mSelection = value;
            }
         }

        public void setItems(List<SelectionViewModel> list)
        {
            foreach (SelectionViewModel item in list)
            {
                models.Add(item);
            }
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!InWork)
            {
                InWork = true;
                if (e.Item == null)
                {
                    return;
                }

                ListView lv = sender as ListView;
                int index = models.IndexOf(lv.SelectedItem);
                lv.SelectedItem = null;

                if (!MultipleSelection)
                {
                    for (int i = 0; i < models.Count; i++)
                    {
                        SelectionViewModel m = models[i];
                        models[i] = new SelectionViewModel { IsSelected = false, Text = m.Text };
                    }
                }

                SelectionViewModel model = models[index];
                models[index] = new SelectionViewModel { IsSelected = !model.IsSelected, Text = model.Text };
                InWork = false;
            }
        }

        private async void Clicked_MenuItemDone(object sender, System.EventArgs e)
        {
            List<SelectionViewModel> selectedItemList = new List<SelectionViewModel>();
            foreach (SelectionViewModel m in models)
            {
                if (m.IsSelected)
                {
                    selectedItemList.Add(m);
                }
            }

            await Task.Delay(500);
            await Navigation.PopAsync();
            SelectedItemEventArgs args = new SelectedItemEventArgs();
            args.SelectedItemList = selectedItemList;
            OnItemsSelected?.Invoke(this, args);
        }
    }

    public class SelectionViewModel
    {
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }

    public class SelectedItemEventArgs : EventArgs
    {
        public List<SelectionViewModel> SelectedItemList { get; set; }
    }
}
