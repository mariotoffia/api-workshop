using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using Contest.Api;
using SwaggerWcf;

namespace ContestHost
{
  public sealed class Program
  {
    private static void Main()
    {
      var baseAddress = new Uri("http://localhost/40001");
      /*
      using (var swagger = new WebServiceHost(typeof(SwaggerWcfEndpoint), baseAddress))
      {
        swagger.Open();

        using (var host = new WebServiceHost(typeof(ContestService), baseAddress))
        {
          host.AddServiceEndpoint(typeof(IContestService), new WebHttpBinding(), "v1/rest");
          host.Description.Behaviors.Find<ServiceDebugBehavior>().HttpHelpPageEnabled = false;

          host.Open();

          Console.WriteLine("Contest Service is up and Running, hit ENTER to EXIT");
          Console.ReadLine();
        }
      }*/

      using (var swagger = new ServiceHost(typeof(SwaggerWcfEndpoint), baseAddress))
      {
        swagger.Open();

        using (var host = new ServiceHost(typeof(ContestService), baseAddress))
        {
          host.Open();

          Console.WriteLine("Contest Service is up and Running, hit ENTER to EXIT");
          Console.ReadLine();
        }
      }
    }
  }
}