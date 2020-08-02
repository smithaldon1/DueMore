using DueMore.Model;
using DueMore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DueMore.View
{
    public partial class InboxDetailView : ContentPage
    {
        InboxDetailVM vm;

        public InboxDetailView()
        {
            InitializeComponent();

            vm = Resources["vm"] as InboxDetailVM;
        }

        public InboxDetailView(InboxItem selectedInboxItem)
        {
            InitializeComponent();

            vm = Resources["vm"] as InboxDetailVM;
            vm.InboxItem = selectedInboxItem;
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void SaveLabel_Tapped(object sender, EventArgs e)
        {

        }
    }
}