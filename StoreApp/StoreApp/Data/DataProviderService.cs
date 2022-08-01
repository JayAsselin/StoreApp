using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StoreApp.Data
{
    public class DataProviderService
    {
        private static string BaseAddress = 
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8085" : "http://127.0.0.1:8085";
        private HttpClient httpClient;
        private JsonSerializerOptions serializerOptions;

        public DataProviderService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(DataProviderService.BaseAddress);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        }

        public async Task<List<SmartDevice>> GetAllDevicesAsync()
        {
            List<SmartDevice> Items = new List<SmartDevice>();

            try
            {
                Uri uri = new Uri($"{BaseAddress}/api/SmartDevices");
                
                HttpResponseMessage response = await this.httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<SmartDevice>>(content, serializerOptions);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        //public async Task<User> GetUserByIdAsync(int id)
        //{
        //    User user = new User();

        //    try
        //    {
        //        //construire l'uri de la ressource
        //        Uri uri = new Uri($"{BaseAddress}/users/{id}");
        //        //Lancer la requete http a l'api web
        //        HttpResponseMessage response = await this.httpClient.GetAsync(uri);
        //        //string jsonrep=await this.httpClient.GetStringAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync();
        //            user = JsonSerializer.Deserialize<User>(content, serializerOptions);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }

        //    return user;
        //}

        public async Task<Invoice> AddInvoiceAsync(Invoice item)
        {
            Uri uri = new Uri($"{BaseAddress}/api/Factures");

            try
            {
                string json = JsonSerializer.Serialize<Invoice>(item, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string repcontent = await response.Content.ReadAsStringAsync();
                    Invoice invoice = JsonSerializer.Deserialize<Invoice>(repcontent, serializerOptions);
                    return invoice;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }
    }
}
