using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Contest.Host.Controllers
{
  public class SwaggerController : ApiController
  {
    [Route("odata/$swagger")]
    public JObject GetSwagger()
    {
      return WebApiConfig.SwaggerOdata();
    }
  }
}