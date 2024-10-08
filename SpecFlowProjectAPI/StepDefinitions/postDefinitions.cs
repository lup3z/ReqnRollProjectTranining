using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SpecFlowProjectAPI.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll.Infrastructure;

namespace SpecFlowProjectAPI.StepDefinitions
{
    [Binding]
    public class postDefinitions
    {

        HttpClient httpClient;
        HttpResponseMessage response;
        string responseBody;

        public postDefinitions()
        {
            httpClient = new HttpClient();
        }

        [Given(@"the user sends a post request with url as ""([^""]*)""")]
        public async Task GivenTheUserSendsAPostRequestWithUrlAs(string url)
        {
            PostData bodyData = new PostData()
            {
                username = "admin",
                password = "password123",
            };

            string data = JsonConvert.SerializeObject(bodyData);
            var contentData = new StringContent(data, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(url, contentData);

            responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("post response is: " + responseBody);

        }

        [Then(@"user should get a succes response with (.*)s code")]
        public void ThenUserShouldGetASuccesResponseWithSCode(int p0)
        {
            Assert.True(response.IsSuccessStatusCode);

            var jsonResponse = JObject.Parse(responseBody);
            Assert.IsTrue(jsonResponse["token"] != null, "El campo 'token' no se encontró en la respuesta.");

            // Verifica que 'token' sea un string y no esté vacío
            string token = jsonResponse["token"].ToString();
            Assert.IsFalse(string.IsNullOrEmpty(token), "El campo 'token' es nulo o vacío.");

            // Opcional: Imprime el token en la salida de SpecFlow
            Console.WriteLine("Token recibido: " + token);
        }
    }
}
