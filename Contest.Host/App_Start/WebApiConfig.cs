using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Contest.Api.Model;
using Microsoft.OData.Edm;
using Newtonsoft.Json.Linq;
using WebApi.StructureMap;

namespace Contest.Host
{
  public sealed class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // IoC
      config.UseStructureMap<MyRegistry>();
      config.MapHttpAttributeRoutes();

      // ODataV4 Support
      var model = GetEdmModel();
      config.MapODataServiceRoute(
        "ODataRoute",
        "odata",
        model);

      config.EnsureInitialized();
    }

    public static JObject SwaggerOdata()
    {
      return SwaggerFromModel(GetEdmModel());
    }

    private static JObject SwaggerFromModel(IEdmModel model)
    {
      var converter = new ODataSwaggerConverter(model);
      return converter.GetSwaggerModel();
    }

    private static IEdmModel GetEdmModel()
    {
      ODataModelBuilder builder = new ODataConventionModelBuilder();

      builder.Namespace = "Ductus";
      builder.ContainerName = "DefaultContainer";

      builder.EntitySet<ContestInfo>("Contests");
      builder.EntitySet<Player>("Players");
      builder.EntitySet<PlayerInContest>("PlayerInContests");


      return builder.GetEdmModel();
    }
  }
}