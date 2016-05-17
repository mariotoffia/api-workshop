using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Contest.Api.Model;
using Microsoft.OData.Edm;
using WebApi.StructureMap;

namespace Contest.Host
{
  public sealed class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // IoC
      config.UseStructureMap<MyRegistry>();

      // ODataV4 Support
      config.MapODataServiceRoute(
        "ODataRoute",
        "odata",
        GetEdmModel());

      config.EnsureInitialized();
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