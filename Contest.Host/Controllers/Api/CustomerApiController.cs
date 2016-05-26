using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Contest.Host.Db;
using Contest.Host.Model;
using Marten;
using NSwag.Annotations;
using NSwag.CodeGeneration.SwaggerGenerators.WebApi;

namespace Contest.Host.Controllers.Api
{
  [RoutePrefix("api/customer")]
  public sealed class CustomerApiController : ApiController
  {
    private readonly IDocumentSession _session;

    public CustomerApiController(IStore store)
    {
      _session = store.OpenSession();
    }

    [HttpGet]
    [Route("")]
    [ResponseType("200", typeof(Kund))]
    [Description("Gets all customers")]
    public IEnumerable<Kund> Get()
    {
      return _session.Query<Kund>().ToArray();
    }

    [HttpGet]
    [Route("{customer}")]
    [ResponseType("200", typeof(Kund))]
    [ResponseType("500", typeof(ArgumentException))]
    [Description("Gets a customer based on a code")]
    public Kund Get(string customer)
    {
      var info = _session.Query<Kund>().SingleOrDefault(x => x.Kod == customer);
      if (null == info)
      {
        throw new ArgumentException($"Customer with Kod = {customer} cannot be found.");
      }

      return info;
    }

    [HttpGet]
    [Route("{customer}/nr/{customerId}")]
    [ResponseType("200", typeof(Kund))]
    [ResponseType("500", typeof(ArgumentException))]
    [Description("Gets a customer number based on a code and a customer id")]
    public Kundnummer GetCustomerNumber(string customer, string customerId)
    {
      var info = _session.Query<Kundnummer>().SingleOrDefault(x => x.Kod == customer && x.Kund == customerId);
      if (null == info)
      {
        throw new ArgumentException($"Customer number with Kod = {customer} and Kund = {customerId} cannot be found.");
      }

      return info;
    }

    private static readonly Lazy<string> SwaggerContent = new Lazy<string>(() =>
    {
      var settings = new WebApiToSwaggerGeneratorSettings
      {
        DefaultUrlTemplate = "api/{controller}/{action}/{id}"
      };

      var generator = new WebApiToSwaggerGenerator(settings);
      var service = generator.GenerateForController(typeof(CustomerApiController), excludedMethodName: "Swagger");
      return service.ToJson();
    });

    [HttpGet, Route("swagger")]
    public HttpResponseMessage Swagger()
    {
      var res = Request.CreateResponse(HttpStatusCode.OK);
      res.Content = new StringContent(SwaggerContent.Value, Encoding.UTF8, "application/json");
      return res;
    }

    protected override void Dispose(bool disposing)
    {
      _session.Dispose();
      base.Dispose(disposing);
    }
  }
}