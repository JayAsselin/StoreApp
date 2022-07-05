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
    internal class PhonesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SmartDevice> Phones { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void GetList()
        {
            try
            {
                this.Phones.Clear();
                var items = await App.dbContext.GetByTypeAsync("Smart Phone");
                foreach (var item in items)
                {
                    this.Phones.Add(item);
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

        public PhonesViewModel()
        {
            this.Phones = new ObservableCollection<SmartDevice>();
            RefreshList();
        }
    }
}
