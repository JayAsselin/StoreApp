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
    internal class TabletsViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SmartDevice> Tablets { get; set; }

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
                this.Tablets.Clear();
                var items = await App.dbContext.GetByTypeAsync("Tablet");
                foreach (var item in items)
                {
                    this.Tablets.Add(item);
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

        public TabletsViewModel()
        {
            this.Tablets = new ObservableCollection<SmartDevice>();
            RefreshList();
        }
    }
}
