using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contest.Host.Db;
using Contest.Host.Model;
using Marten;
using NSwag.Annotations;

namespace Contest.Host.Controllers.Api
{
  [RoutePrefix("api/customer")]
  [Description("Customer Id Service.")]
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
    [ActionName("GetAllCustomers")]
    public IEnumerable<Kund> Get()
    {
      return _session.Query<Kund>().ToArray();
    }

    [HttpGet]
    [Route("{customer}")]
    [ResponseType("200", typeof(Kund))]
    [ResponseType("404", typeof(string))]
    [Description("Gets a customer based on a code")]
    public HttpResponseMessage Get(string customer)
    {
      var info = _session.Query<Kund>().SingleOrDefault(x => x.Kod == customer);
      if (null == info)
      {
        return Request.CreateResponse(HttpStatusCode.NotFound, $"Could not find Kund based on customer = {customer}");
      }

      return Request.CreateResponse(HttpStatusCode.OK, info);
    }

    [HttpGet]
    [Route("{customer}/nr/{customerId}")]
    [ResponseType("200", typeof(Kundnummer))]
    [ResponseType("404", typeof(string))]
    [Description("Gets a customer number based on a code and a customer id")]
    public HttpResponseMessage GetCustomerNumber(string customer, string customerId)
    {
      var info = _session.Query<Kundnummer>().SingleOrDefault(x => x.Kod == customer && x.Kund == customerId);
      if (null == info)
      {
        return Request.CreateResponse(HttpStatusCode.NotFound,
          $"Could not find Kundnummer based on customer = {customer} and customerId = {customerId}");
      }

      return Request.CreateResponse(HttpStatusCode.OK, info);
    }

    protected override void Dispose(bool disposing)
    {
      _session.Dispose();
      base.Dispose(disposing);
    }
  }
}