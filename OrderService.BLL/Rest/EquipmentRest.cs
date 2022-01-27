using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderService.BLL.Interfaces;
using OrderService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Rest
{
    public class EquipmentRest : IEquipmentRest
    {
        public async Task<bool> SendUpdateStock(SuccessOrderModel successOrderModel)
        {

            using (var client = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(successOrderModel);
                var content = new StringContent(myContent, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://api_master:80/api/equipment/update-stock", content);
                return result.IsSuccessStatusCode;
            }
        }
    }
}
