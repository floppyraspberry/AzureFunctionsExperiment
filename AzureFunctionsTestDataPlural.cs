#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<ActionResult<List<Users>>> Run(HttpRequest req, string environment, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");
    
    var response = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(response);

    var env = environment.ToUpper();
    List<Users> users = null;

    if (env == "TST")
    {
        users = new List<Users>
        {
            new Users { Username = "username1", Password = "secret1" },
            new Users { Username = "username2", Password = "secret2" }
        };
    }
    if (env == "ACC")
    {
        users = new List<Users>
        {
            new Users { Username = "username1", Password = "secret1" },
            new Users { Username = "username2", Password = "secret2" }
        };
    }
    else
    {
        throw new Exception($"Please enter 'tst' or 'acc' as the correct environment");
    }

    log.LogInformation($"responseBody: {users}");
    var jsonResponse = JsonConvert.SerializeObject(users);

    string responseMessage = string.IsNullOrEmpty(env)
        ? "This HTTP triggered function executed successfully. Pass an environment in the query string or in the request body for a custom response."
        : jsonResponse;

    return new OkObjectResult(responseMessage);
}

public class Users
{
    public string Username;
    public string Password;
}