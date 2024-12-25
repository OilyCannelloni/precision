using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Precision.algorithm;
using Precision.deals;
using Precision.models;

namespace Precision.controllers;

public class DefaultController : WebApiController
{
    private readonly DealService _dealService = new (new DealGenerator());
    
    [Route(HttpVerbs.Get, "/")]
    public string GetRoot()
    {
        return "hello";
    }

    [Route(HttpVerbs.Get, "/random_deal")]
    public Deal GetRandomDeal()
    {
        return _dealService.GetRandomDeal();
    }
}