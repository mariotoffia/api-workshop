using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using NSwag;
using NSwag.CodeGeneration.SwaggerGenerators.WebApi;

namespace Contest.Host.Controllers.Api
{
  [RoutePrefix("api/swagger")]
  public class SwaggerApiController : ApiController
  {
    private static readonly Lazy<string> SwaggerContent = new Lazy<string>(() =>
    {
      var controllers = new[] {typeof(CustomerApiController)};
      var settings = new WebApiToSwaggerGeneratorSettings
      {
        DefaultUrlTemplate = "api/{controller}/{action}/{id}"
      };

      var generator = new WebApiToSwaggerGenerator(settings);
      var service = generator.GenerateForControllers(controllers);

      service.Info = new SwaggerInfo
      {
        Title = "Demo WebApi swagger Service",
        Description = "Demo for the Api-workshop",
        TermsOfService = "See license",
        License = new SwaggerLicense {Name = "Apache 2.0", Url = "http://www.apache.org/licenses/LICENSE-2.0"},
        Version = "1.0.0",
        Contact =
          new SwaggerContact
          {
            Email = "mario.toffia@dataductus.se",
            Name = "Mario Toffia",
            Url = "http://www.dataductus.se"
          }
      };

      service.Host = "localhost:53233";
      service.BasePath = "/";
      service.Schemes = new List<SwaggerSchema> {SwaggerSchema.Http};

      return service.ToJson();
    });

    [HttpGet, Route("")]
    public HttpResponseMessage Get()
    {
      var res = Request.CreateResponse(HttpStatusCode.OK);
      res.Content = new StringContent(SwaggerContent.Value, Encoding.UTF8, "application/json");
      return res;
    }
  }
}