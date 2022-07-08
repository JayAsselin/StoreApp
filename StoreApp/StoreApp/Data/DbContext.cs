using SQLite;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data
{
    public class DbContext
    {
        private const SQLite.SQLiteOpenFlags openFlags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;

        private readonly SQLiteAsyncConnection database;

        public DbContext(string dbName)
        {
            string baseDbPath = Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Combine(baseDbPath, dbName);
            database = new SQLiteAsyncConnection(dbPath, openFlags);
            var res = database.CreateTableAsync<SmartDevice>().Result;
            if (res == CreateTableResult.Created)
                PopulateDb();
        }

        private async void PopulateDb()
        {
            try
            {
                SmartDevice sd;
                sd = new SmartDevice()
                {
                    Modele = "Galaxy S10",
                    Fabriquant = "Samsung",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 282.47,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Galaxy A32",
                    Fabriquant = "Samsung",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 276.98,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Galaxy s20",
                    Fabriquant = "Samsung",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 509.98,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Galaxy S22",
                    Fabriquant = "Samsung",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 1579.99,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Pixel 5",
                    Fabriquant = "Google",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 520,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Pixel 6",
                    Fabriquant = "Google",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 700.99,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Pixel 6 pro",
                    Fabriquant = "Google",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 1129.99,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "P20",
                    Fabriquant = "Huawei",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 389.99,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "One Vision",
                    Fabriquant = "Motorola",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 198.99,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Moto G Power",
                    Fabriquant = "Motorola",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 319.99,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Velvet",
                    Fabriquant = "LG",
                    PlateForme = "Android",
                    Type = "Smart Phone",
                    Prix = 425.82,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPhone 13 Pro",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Smart Phone",
                    Prix = 1399.99,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPhone XR",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Smart Phone",
                    Prix = 301.16,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPhone 8",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Smart Phone",
                    Prix = 239.98,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPhone 11 Pro",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Smart Phone",
                    Prix = 768.98,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPhone 11",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Smart Phone",
                    Prix = 465.66,
                    Image = "phoneImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Galaxy Tab S6",
                    Fabriquant = "Samsung",
                    PlateForme = "Android",
                    Type = "Tablet",
                    Prix = 330.44,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Galaxy Tab A7",
                    Fabriquant = "Samsung",
                    PlateForme = "Android",
                    Type = "Tablet",
                    Prix = 210.44,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Galaxy Tab S8",
                    Fabriquant = "Samsung",
                    PlateForme = "Android",
                    Type = "Tablet",
                    Prix = 1300.44,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Fire HD 10",
                    Fabriquant = "Amazon",
                    PlateForme = "Android",
                    Type = "Tablet",
                    Prix = 260.44,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Yoga Tab",
                    Fabriquant = "Lenovo",
                    PlateForme = "Android",
                    Type = "Tablet",
                    Prix = 300.55,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Tab P11",
                    Fabriquant = "Lenovo",
                    PlateForme = "Android",
                    Type = "Tablet",
                    Prix = 270.53,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Fire HD 8",
                    Fabriquant = "Amazon",
                    PlateForme = "Android",
                    Type = "Tablet",
                    Prix = 110.23,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPad (9th Gen)",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Tablet",
                    Prix = 430.44,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPad (4th Gen)",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Tablet",
                    Prix = 910.82,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPad Air",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Tablet",
                    Prix = 749.99,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPad Pro (3rd Gen)",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Tablet",
                    Prix = 1000.82,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPad Mini",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Tablet",
                    Prix = 135,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "iPad (8th Gen)",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Tablet",
                    Prix = 369,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Surface Pro 8",
                    Fabriquant = "Microsoft",
                    PlateForme = "Windows",
                    Type = "Tablet",
                    Prix = 1300.74,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Surface Go 3",
                    Fabriquant = "Microsoft",
                    PlateForme = "Windows",
                    Type = "Tablet",
                    Prix = 520.44,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Surface Pro X",
                    Fabriquant = "Microsoft",
                    PlateForme = "Windows",
                    Type = "Tablet",
                    Prix = 719.99,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Surface Pro 7",
                    Fabriquant = "Microsoft",
                    PlateForme = "Windows",
                    Type = "Tablet",
                    Prix = 800.44,
                    Image = "tabletImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Galaxy Watch 4",
                    Fabriquant = "Samsung",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 270.77,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Versa 3",
                    Fabriquant = "Fitbit",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 220.34,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Series 6",
                    Fabriquant = "Apple",
                    PlateForme = "IOS",
                    Type = "Smart Watch",
                    Prix = 329.99,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Lily Sport Edition",
                    Fabriquant = "Garmin",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 200.29,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "GT 2 Pro",
                    Fabriquant = "Huawei",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 208,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "CZ",
                    Fabriquant = "Citizen",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 450.49,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Band 6 Fitness",
                    Fabriquant = "Huawei",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 67.89,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Instinct Solar",
                    Fabriquant = "Garmin",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 427.15,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Instinct",
                    Fabriquant = "Garmin",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 350.23,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Suunto 7",
                    Fabriquant = "Suunto",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 1000.82,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Suunto 9",
                    Fabriquant = "Suunto",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 379,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Suunto 9 Peak",
                    Fabriquant = "Suunto",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 729.99,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
                sd = new SmartDevice()
                {
                    Modele = "Suunto 9 Baro",
                    Fabriquant = "Suunto",
                    PlateForme = "Wear OS",
                    Type = "Smart Watch",
                    Prix = 649.99,
                    Image = "watchImage.png"
                };
                await database.InsertAsync(sd);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        public Task<List<Panier>> GetAllAsync()
        {
            // Shows all database entries into a list
            return database.Table<Panier>().ToListAsync();
        }
        public Task<List<SmartDevice>> GetByTypeAsync(string type)
        {
            // Shows all database entries into a list
            return database.Table<SmartDevice>().Where(t => t.Type == type).ToListAsync();
        }

        public Task<SmartDevice> GetItemByIdAsync(int id)
        {
            // Gets a specific item by using the id
            return database.Table<SmartDevice>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertAsync(SmartDevice sd)
        {
            // inserts a invoice to the invoice table
            return database.InsertAsync(sd);
        }
        public Task<int> UpdateAsync(SmartDevice sd)
        {
            // Might not need it
            return database.UpdateAsync(sd);
        }

        public Task<int> DeleteAsync(SmartDevice sd)
        {
            // Might not need it
            return database.DeleteAsync(sd);
        }

        public Task<int> DeleteAsyncById(int id)
        {
            // Might not need it
            return database.DeleteAsync(id);
        }
    }
}
