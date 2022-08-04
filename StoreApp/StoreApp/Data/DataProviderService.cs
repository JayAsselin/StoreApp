using Newtonsoft.Json;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StoreApp.Data
{
    //Jerome Asselin 2195077
    public class DataProviderService
    {
        private static string BaseAddress = 
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8085" : "http://127.0.0.1:8085";
        private HttpClient httpClient;

        public DataProviderService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(DataProviderService.BaseAddress);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                    Items = JsonConvert.DeserializeObject<List<SmartDevice>>(content);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<SmartDevice> GetDeviceByIdAsync(int id)
        {
            SmartDevice device = new SmartDevice();

            try
            {
                Uri uri = new Uri($"{BaseAddress}/api/SmartDevices/{id}");

                HttpResponseMessage response = await this.httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    device = JsonConvert.DeserializeObject<SmartDevice>(content);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return device;
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            List<Invoice> Items = new List<Invoice>();

            try
            {
                Uri uri = new Uri($"{BaseAddress}/api/Factures");
                
                HttpResponseMessage response = await this.httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Invoice>>(content);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<Invoice> AddInvoiceAsync(Invoice item)
        {
            Uri uri = new Uri($"{BaseAddress}/api/Factures");

            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string repcontent = await response.Content.ReadAsStringAsync();
                    Invoice invoice = JsonConvert.DeserializeObject<Invoice>(repcontent);
                    return invoice;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            Invoice invoice = new Invoice();

            try
            {
                Uri uri = new Uri($"{BaseAddress}/api/Factures/{id}");
                
                HttpResponseMessage response = await this.httpClient.GetAsync(uri);
                
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    invoice = JsonConvert.DeserializeObject<Invoice>(content);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return invoice;
        }
    }
}
