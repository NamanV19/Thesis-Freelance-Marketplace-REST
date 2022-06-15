using Common.NavModels;
using Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ocelot.Configuration;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OcelotGateway.Aggregators
{
    public class OrderDetailsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            List<FreelancerNavModel> freelancers = new List<FreelancerNavModel>();
            List<OrderViewModel> orders = new List<OrderViewModel>();
            List<CatalogViewModel> catalogs = new List<CatalogViewModel>();

            foreach (var response in responses)
            {
                string downstreamRouteKey = ((DownstreamRoute)response.Items["DownstreamRoute"]).Key;
                DownstreamResponse downstreamResponse = (DownstreamResponse)response.Items["DownstreamResponse"];
                string downstreamResponseContentString = await downstreamResponse.Content.ReadAsStringAsync();

                if (downstreamRouteKey == "orders")
                {
                    orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(downstreamResponseContentString);
                }

                if(downstreamRouteKey == "freelancersNav")
                {
                    freelancers = JsonConvert.DeserializeObject<List<FreelancerNavModel>>(downstreamResponseContentString);
                }

                if(downstreamRouteKey == "catalogs")
                {
                    catalogs = JsonConvert.DeserializeObject<List<CatalogViewModel>>(downstreamResponseContentString);
                }
            }
            return OrderDetails(orders, freelancers, catalogs);
        }

        public DownstreamResponse OrderDetails(List<OrderViewModel> orders, List<FreelancerNavModel> freelancers, List<CatalogViewModel> catalogs)
        {
            List<OrderViewModel> orderDetails = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                FreelancerNavModel freelancer = freelancers.Find(f => f.Id == order.FreelancerId);
                CatalogViewModel catalog = catalogs.Find(c => c.Id == order.CatalogId);

                order.Freelancer = freelancer;
                order.Catalog = catalog;

                orderDetails.Add(order);
            }

            var orderDetailsString = JsonConvert.SerializeObject(orderDetails);

            var stringContent = new StringContent(orderDetailsString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    } 
}
