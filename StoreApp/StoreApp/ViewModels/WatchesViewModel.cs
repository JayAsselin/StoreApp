using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace StoreApp.ViewModels
{
    internal class WatchesViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SmartDevice> Watches { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void GetList()
        {
            try
            {
                this.Watches.Clear();
                var items = await App.dbContext.GetByTypeAsync("Smart Watch");
                foreach (var item in items)
                {
                    this.Watches.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void RefreshList()
        {
            GetList();
        }

        public WatchesViewModel()
        {
            this.Watches = new ObservableCollection<SmartDevice>();
            RefreshList();
        }
    }
}
