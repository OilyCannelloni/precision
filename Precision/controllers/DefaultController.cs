﻿using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Precision.algorithm;
using Precision.deals;
using Precision.game.elements.deal;

namespace Precision.controllers;

public class DefaultController : WebApiController
{
    private readonly DealService _dealService = new();

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