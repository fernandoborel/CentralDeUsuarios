using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace CentralDeUsuarios.IntegrationTests.Helpers;

public class TestHelper
{
    public HttpClient CreateClient()
    {
        return new WebApplicationFactory<Program>().CreateClient();
    }

    public StringContent CreateContent<TCommand>(TCommand command)
    {
        return new StringContent(JsonConvert.SerializeObject(command), 
            Encoding.UTF8, "application/json");
    }
}
