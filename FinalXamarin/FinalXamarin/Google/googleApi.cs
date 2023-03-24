using FinalXamarin.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinalXamarin.Google
{
    public class googleApi
    {
        public static async Task<Root> GetLocation(string address)
        {
            var dataresponse = new Root();
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=AIzaSyAdIamrxo_20mVeUmI1P87VArwT1MVgcBs";
            var http = new HttpClient();
            HttpResponseMessage response = await http.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                if (Convert.ToInt32(response.StatusCode) == 200)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    dataresponse = JsonConvert.DeserializeObject<Root>(data);
                }
            }
            return dataresponse;
        }
    }
}
