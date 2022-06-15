using BrotliSharpLib;
using Common.PostModels;
using Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocelot.Configuration;
using Ocelot.DownstreamRouteFinder;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OcelotGateway.Aggregators
{
    internal class BuyersAndCatalogsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            List<BuyerViewModel> buyers = new List<BuyerViewModel>();
            List<CatalogViewModel> catalogs = new List<CatalogViewModel>();

            foreach (var response in responses)
            {
                string downstreamRouteKey = ((DownstreamRoute)response.Items["DownstreamRoute"]).Key;
                DownstreamResponse downstreamResponse = (DownstreamResponse)response.Items["DownstreamResponse"];
                string downstreamResponseContentString = await downstreamResponse.Content.ReadAsStringAsync();

                if (downstreamRouteKey == "buyers")
                {
                    buyers = JsonConvert.DeserializeObject<List<BuyerViewModel>>(downstreamResponseContentString);
                }

                if (downstreamRouteKey == "catalogs")
                {
                    catalogs = JsonConvert.DeserializeObject<List<CatalogViewModel>>(downstreamResponseContentString);
                }
            }

            return CatalogsOfBuyers(buyers, catalogs);
        }

        public DownstreamResponse CatalogsOfBuyers(List<BuyerViewModel> buyers, List<CatalogViewModel> catalogs)
        {
            List<BuyerViewModel> buyersAndCatalogs = new List<BuyerViewModel>();

            var catalogsByBuyerId = catalogs.GroupBy(n => n.BuyerId);

            foreach (var catalog in catalogsByBuyerId)
            {
                BuyerViewModel buyer = buyers.Find(n => n.Id == catalog.Key);
                // var selectCatalog = catalog.Select(n => new { n.TypeOfWork, n.TitleOfJob, n.JobCategory, n.JobDescription });
                buyer.Catalogs = catalog.ToList<CatalogViewModel>();

                buyersAndCatalogs.Add(buyer);
            }

            var catalogsOfBuyersString = JsonConvert.SerializeObject(buyersAndCatalogs);

            var stringContent = new StringContent(catalogsOfBuyersString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
