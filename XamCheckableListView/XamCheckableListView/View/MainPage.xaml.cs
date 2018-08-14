using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamCheckableListView.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        List<SelectionViewModel> dogs;

        public MainPage()
        {
            InitializeComponent();

            dogs = new List<SelectionViewModel>
            {
                new SelectionViewModel { IsSelected = false, Text = "Aigi" },
                new SelectionViewModel { IsSelected = true, Text = "Spitz" },
                new SelectionViewModel { IsSelected = false, Text = "Mastiff" },
                new SelectionViewModel { IsSelected = false, Text = "Finnish Spitz" },
                new SelectionViewModel { IsSelected = false, Text = "Briard" }
            };
        }

        private void Handle_OnItemsSelected(object sender, SelectedItemEventArgs e)
        {
            string str = "";
            foreach (SelectionViewModel model in e.SelectedItemList)
            {
                str += model.Text;
                str += "\n";
            }

            lblSelectedItem.Text = str;
        }

        private void Handle_SingleSelectionView(object sender, EventArgs e)
        {
            SelectionView page = new SelectionView(dogs)
            {
                Title = "Select Single Item",
                MultipleSelection = false
            };

            page.OnItemsSelected += Handle_OnItemsSelected;

            Navigation.PushAsync(page);
        }

        private void Handle_MultipleSelectionView(object sender, EventArgs e)
        {
            SelectionView page = new SelectionView(dogs)
            {
                Title = "Select Multiple Item",
                MultipleSelection = true
            };

            page.OnItemsSelected += Handle_OnItemsSelected;

            Navigation.PushAsync(page);
        }
    }
}