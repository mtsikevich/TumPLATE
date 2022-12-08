using System.Net.Http.Json;
using FluentAssertions;
using TumPLATE.Tests.Acceptance.Payloads;

namespace TumPLATE.Tests.Acceptance.Steps;

[Binding]
public class SampleStepDefinitions
{
    private readonly HttpClient _httpClient;
    private readonly ScenarioContext _scenarioContext;

    public SampleStepDefinitions(HttpClient client, ScenarioContext scenarioContext)
    {
        _httpClient = client;
        _scenarioContext = scenarioContext;
    }
    
    [When(@"when I call the root")]
    public async Task WhenWhenICallTheRoot()
    {
        var response = await _httpClient.GetAsync("/items");
        _scenarioContext.Add("response", await response.Content.ReadFromJsonAsync<List<string>>());
    }

    [Then(@"a response with an array of fruits, environment and sentinel should be returned")]
    public void ThenAResponseWithAnArrayOfFruitsEnvironmentAndSentinelShouldBeReturned()
    {
        var response = _scenarioContext.Get<List<string>>("response");
        response.Should().NotBeNull();
    }
}