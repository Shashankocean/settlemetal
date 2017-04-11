using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace settleMetal
{
    class request
    {
        HttpClient client;
        public async Task<string> requestSettle(string page,string json)
        {
            client=new HttpClient();
            try
            {
                var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://settlemetal.com/SM_DEV/app/" + page, httpcontent);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<Categories>> getCategory(string page )
        {
            client = new HttpClient();
            try
            {
                var uri = new Uri(string.Format("http://settlemetal.com/SM_DEV/app/getCategory.php", string.Empty));

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<List<Categories>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<City>> getCity()
        {
            client = new HttpClient();
            try
            {
                var uri = new Uri(string.Format("http://settlemetal.com/SM_DEV/app/mcity.php", string.Empty));

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<List<City>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<Location>> getlocation(string json)
        {
            client = new HttpClient();
            try
            {
                var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://settlemetal.com/SM_DEV/app/mlocation.php", httpcontent);
                if (response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<List<Location>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<StoreAddress>> getAddress(string json)
        {
            client = new HttpClient();
            try
            {
                var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://settlemetal.com/SM_DEV/app/maddress.php", httpcontent);
                if (response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<List<StoreAddress>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
