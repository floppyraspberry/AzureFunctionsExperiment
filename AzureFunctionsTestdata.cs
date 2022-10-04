#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, string customerName, ILogger log)
{
            log.LogInformation($"C# HTTP trigger function processed a request.");
            string response = String.Empty;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            //string input  = req.Query["user"];
            if (requestBody.Contains("user"))
            {
                var random = new Random();
                var credentials = new List<KeyValuePair<string, string>>();

                credentials.Add(new KeyValuePair<string, string>("bob", "c"));
                credentials.Add(new KeyValuePair<string, string>("alice", "a"));
                credentials.Add(new KeyValuePair<string, string>("charlie", "b"));
                int index = random.Next(credentials.Count);

                response = credentials[index].ToString();
            }

            string responseMessage = string.IsNullOrEmpty(requestBody)
                ? "This HTTP triggered function executed successfully. Pass a parameter in the request body to receive some testdata in the response."
                : $"{response}";

            return new OkObjectResult(responseMessage);
}