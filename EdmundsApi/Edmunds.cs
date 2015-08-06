﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using EdmundsApi.Responses;
using EdmundsApi.Requests;
using EdmundsApi.Models;

namespace EdmundsApi
{
    public class Edmunds
    {
        private readonly string _apiKey;
        private const string _baseUrl = "https://api.edmunds.com";

        public Edmunds(string apiKey)
        {
            Contract.Requires(apiKey != null);

            _apiKey = apiKey;
        }

        public async Task<dynamic> Call(string endpoint, object endpointParams = null, IDictionary<string, object> queryParams = null)
        {
            Contract.Requires(endpoint != null);

            var url = BuildUrl(endpoint, endpointParams, queryParams);

            return await url.GetJsonAsync();
        }

        public async Task<T> Call<T>(string endpoint, object endpointParams = null, IDictionary<string, object> queryParams = null)
        {
            Contract.Requires(endpoint != null);

            var url = BuildUrl(endpoint, endpointParams, queryParams);

            return await url.GetJsonAsync<T>();
        }
        
        public async Task<List<Make>> GetMakeList(State state = State.New, int? year = null, View view = View.Basic)
        {
            var queryParams = new Dictionary<string, object> { 
                { "state", state.ToString().ToLower() },
                { "year", year },
                { "view", view.ToString().ToLower() },
            };

            return await Call<List<Make>>("/api/vehicle/v2/makes", queryParams: queryParams);
        }

        //Helpers
        private Url BuildUrl(string endpoint, object endpointParams, IDictionary<string, object> queryParams)
        {
            Contract.Requires(endpoint != null);

            if (queryParams == null)
            {
                queryParams = new Dictionary<string, object>();
            }
            queryParams.Add("fmt", "json");
            queryParams.Add("api_key", _apiKey);

            var endpointParts = endpoint.Split('?');
            if (endpointParts.Length > 1)
            {
                endpoint = endpointParts[0];
                var queryStr = endpointParts[1];
                AddQueryParamsFromString(queryParams, queryStr);
            }

            var url = new Url(_baseUrl)
                .AppendPathSegment(NamedFormatter.Format(endpoint, endpointParams))
                .SetQueryParams(queryParams);

            return url;
        }

        private static void AddQueryParamsFromString(IDictionary<string, object> queryParams, string queryStr)
        {
            if (!String.IsNullOrEmpty(queryStr))
            {
                var queryStrParams = queryStr.Split(new[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
                string keyName = null;
                foreach (var p in queryStrParams)
                {
                    if (keyName == null)
                    {
                        keyName = p;
                    }
                    else
                    {
                        queryParams.Add(keyName, p);
                    }
                }
            }
        }
    }
}
