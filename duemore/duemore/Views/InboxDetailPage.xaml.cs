using DueMore.Models;
using DueMore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DueMore.Views
{
    public partial class InboxDetailPage : ContentPage
    {

        InboxDetailVM vm;

        public InboxDetailPage()
        {
            InitializeComponent();

            vm = Resources["vm"] as InboxDetailVM;
        }
        public InboxDetailPage(InboxItems selectedInboxItem)
        {
            InitializeComponent();
            vm = Resources["vm"] as InboxDetailVM;
            vm.InboxItems = selectedInboxItem;
        }
    }
}