using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowProjectAPI.Support;
using System.Security.Policy;
using System.Text;
using TechTalk.SpecFlow.Infrastructure;
using static SpecFlowProjectAPI.Support.GetData;


namespace SpecFlowProjectAPI.StepDefinitions
{
    [Binding]
    public class getDefinitions
    {
        HttpClient httpClient;
        HttpResponseMessage response;
        string responseBody;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        GetData desdata;
        public getDefinitions(ISpecFlowOutputHelper _specFlowOutputHelper)
        {
            httpClient = new HttpClient();
            this._specFlowOutputHelper = _specFlowOutputHelper;
        }

        [Given(@"the user sends a get request with url as ""([^""]*)""")]
        public async Task GivenTheUserSendsAGetRequestWithUrlAs(string url)
        {
            response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();
            _specFlowOutputHelper.WriteLine(responseBody);

            desdata = JsonConvert.DeserializeObject<GetData>(responseBody);
            _specFlowOutputHelper.WriteLine("After deserialization value is: " + desdata.data.id.ToString());

        }

        [Then(@"user should get a succes response")]
        public void ThenUserShouldGetASuccesResponse()
        {
            GetData getData = new GetData
            {
                data = new GetData.Data
                {
                    id = 2,
                    email = "janet.weaver@reqres.in",
                    first_name = "Janet",
                    last_name = "Weaver",
                    avatar = "https://reqres.in/img/faces/2-image.jpg"
                },
            };
            Assert.True(response.IsSuccessStatusCode);
            Assert.AreEqual(desdata.data.id, getData.data.id);
            Assert.AreEqual(desdata.data.email, getData.data.email);
            Assert.AreEqual(desdata.data.first_name, getData.data.first_name);
            Assert.AreEqual(desdata.data.last_name, getData.data.last_name);
            Assert.AreEqual(desdata.data.avatar, getData.data.avatar);
        }





    }
}
