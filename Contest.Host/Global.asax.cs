using System;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Contest.Api;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using SwaggerWcf;

namespace Contest.Host
{
  public class Global : HttpApplication
  {
    private IContainerService _postgres;

    protected void Application_Start(object sender, EventArgs e)
    {
      var task = new Task<IContainerService>(() =>
      {
        try
        {
          return new Builder().UseContainer()
            .UseImage("kiasaki/alpine-postgres:latest")
            .ExposePort(40001, 5432)
            .WithEnvironment("POSTGRES_PASSWORD=mysecretpassword")
            .WithName("apiworkshop")
            .Build().Start();
        }
        catch (Exception)
        {
          return null;
        }
      });

      var res = task.ContinueWith(a => { _postgres = a.Result; });
      task.Start();
      res.Wait();

      WebApiConfig.Register(GlobalConfiguration.Configuration);

      RouteTable.Routes.Add(new ServiceRoute("v1/rest", new WebServiceHostFactory(), typeof(ContestService)));
      RouteTable.Routes.Add(new ServiceRoute("api-docs", new WebServiceHostFactory(), typeof(SwaggerWcfEndpoint)));
    }

    protected void Session_Start(object sender, EventArgs e)
    {
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
    }

    protected void Application_Error(object sender, EventArgs e)
    {
    }

    protected void Session_End(object sender, EventArgs e)
    {
    }

    protected void Application_End(object sender, EventArgs e)
    {
      _postgres?.Dispose();
    }
  }
}