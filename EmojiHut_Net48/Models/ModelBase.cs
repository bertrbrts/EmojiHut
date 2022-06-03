using EmojiHut_Net48.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmojiHut.Models
{
    public abstract class ModelBase : IModel
    {
        private readonly string _baseURL = Settings.Default.API_Base_URL; 
        protected readonly static string AccessKey = Settings.Default.API_Access_Key; 
        protected readonly static string FallbackBasePath = Settings.Default.FallbackDataPath;

        public abstract string PrimaryQueryString { get; set; }
        public abstract string FallbackDataPath { get; set; }

        public async Task<List<T>> GetAsync<T>(string query) 
        {
            using (HttpClient client = new HttpClient()) 
            {
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage res = await client.GetAsync(query))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string response = await res.Content.ReadAsStringAsync();
                        return Deserialize<T>(response);
                    }
                }
            };

            return new List<T>();
        }

        public async Task<List<T>> GetFallbackDataAsync<T>(string dataPath)
        {
            using (var reader = new StreamReader($"{FallbackBasePath}{dataPath}")) 
            {
                string json = await reader.ReadToEndAsync();
                return Deserialize<T>(json);
            }
        }

        private static List<T> Deserialize<T>(string json) 
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException($"JsonSerializationExcpetion: {e.Message}\n" +
                                                     $"Response: {json}");
            }
        }
    }
}