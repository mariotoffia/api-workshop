using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Contest.Host.Db;
using Contest.Host.Model;
using Marten;

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
    public IEnumerable<Kund> Get()
    {
      return _session.Query<Kund>().ToArray();
    }

    [HttpGet]
    [Route("{customer}")]
    public Kund Get(string customer)
    {
      var info = _session.Query<Kund>().SingleOrDefault(x => x.Kod == customer);
      if (null == info)
      {
        NotFound();
        return null;
      }

      return info;
    }

    [HttpGet]
    [Route("{customer}/nr/{customerId}")]
    public Kundnummer GetCustomerNumber(string customer, string customerId)
    {
      var info = _session.Query<Kundnummer>().SingleOrDefault(x => x.Kod == customer && x.Kund == customerId);
      if (null == info)
      {
        NotFound();
        return null;
      }

      return info;
    }

    protected override void Dispose(bool disposing)
    {
      _session.Dispose();
      base.Dispose(disposing);
    }
  }
}