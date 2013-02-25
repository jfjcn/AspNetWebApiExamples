using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace SpecsForWebApiExamples
{
    public class RestClientForTesting<T>
    {
        protected HttpClient Client;

        public RestClientForTesting(string baseAddress)
        {
            Client = new HttpClient {BaseAddress = new Uri(baseAddress)};

            // Add an Accept header for JSON format.
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public RestClientResponse<List<T>> GetMany(string resourceRelativePath)
        {
            HttpResponseMessage response = Client.GetAsync(resourceRelativePath).Result;  // Blocking call!
            var restClientReponse = new RestClientResponse<List<T>>();
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                restClientReponse.Success = true;
                restClientReponse.ReturnedObject = response.Content.ReadAsAsync<IEnumerable<T>>().Result.ToList();
            }
            else
            {
                restClientReponse.Success = false;
                restClientReponse.HttpStatusCode = (int)response.StatusCode;
                restClientReponse.HttpReasonPhrase = response.ReasonPhrase;
            }
            return restClientReponse;
        }

        public RestClientResponse<T> GetSingle(string resourceRelativePath)
        {
            HttpResponseMessage response = Client.GetAsync(resourceRelativePath).Result;  // Blocking call!
            var restClientReponse = new RestClientResponse<T>();
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                restClientReponse.Success = true;
                restClientReponse.ReturnedObject = response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                restClientReponse.Success = false;
                restClientReponse.HttpStatusCode = (int)response.StatusCode;
                restClientReponse.HttpReasonPhrase = response.ReasonPhrase;
            }
            return restClientReponse;
        }

        public RestClientResponse Post(string resourceRelativePath, T resourceToCreate)
        {
            var response = Client.PostAsJsonAsync(resourceRelativePath, resourceToCreate).Result;
            var restClientReponse = new RestClientResponse();
            if (response.IsSuccessStatusCode)
            {
                restClientReponse.Success = true;
                restClientReponse.ResourceUri = response.Headers.Location;
                var absoluteUri = response.Headers.Location.AbsoluteUri;
                var lastForwardSlashLocation = absoluteUri.LastIndexOf("/");
                var parsedId =
                    absoluteUri.Substring(
                        lastForwardSlashLocation + 1,
                        absoluteUri.Length - lastForwardSlashLocation - 1);
                restClientReponse.ResourceParsedId = parsedId;
            }
            else
            {
                restClientReponse.Success = false;
                restClientReponse.HttpStatusCode = (int)response.StatusCode;
                restClientReponse.HttpReasonPhrase = response.ReasonPhrase;
            }
            return restClientReponse;
        }

        public RestClientResponse Delete(string resourceRelativePath, int id)
        {
            var response = Client.DeleteAsync(resourceRelativePath + "/" + id).Result;
            var restClientResponse =
                new RestClientResponse
                    {
                        HttpStatusCode = (int) response.StatusCode,
                        HttpReasonPhrase = response.ReasonPhrase
                    };
            return restClientResponse;
        }
    }

    public class RestClientResponse
    {
        public bool Success { get; set; }
        public Uri ResourceUri { get; set; }
        public string ResourceParsedId { get; set; }
        public int HttpStatusCode { get; set; }
        public string HttpReasonPhrase { get; set; }
    }

    public class RestClientResponse<T> : RestClientResponse
    {
        public T ReturnedObject { get; set; }
    }
}
