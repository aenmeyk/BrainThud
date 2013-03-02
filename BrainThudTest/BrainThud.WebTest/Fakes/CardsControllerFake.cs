﻿using System.Collections.Generic;
using System.Net.Http;
using BrainThud.Core.Models;
using BrainThud.Web.Api.Controllers;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class CardsControllerFake : CardsController
    {
        public CardsControllerFake(
            ITableStorageContextFactory tableStorageContextFactory, 
            IAuthenticationHelper authenticationHelper)
            : base(tableStorageContextFactory, authenticationHelper) { }

        public string RouteName { get; set; }
        public object RouteValues { get; set; }

        public override string GetLink(string routeName, object routeValues)
        {
            this.RouteValues = routeValues;
            this.RouteName = routeName;
            return TestUrls.LOCALHOST;
        }
    }
}