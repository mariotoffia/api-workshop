using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Contest.Api.Model;
using Contest.Host.Db;
using Marten;

namespace Contest.Host.Controllers.Api
{
  [RoutePrefix("api/contests")] 
  public sealed class ContestApiController : ApiController
  {
    private readonly IDocumentSession _session;

    public ContestApiController(IStore store)
    {
      _session = store.OpenSession();
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<ContestInfo> Get()
    {
      return _session.Query<ContestInfo>().ToArray();
    }

    [HttpGet]
    [Route("{id}")]
    public ContestInfo Get(string id)
    {
      var info = _session.Query<ContestInfo>().SingleOrDefault(x => x.Id == id);
      if (null == info)
      {
        NotFound();
        return null;
      }

      return info;
    }

    [Route("{value}")]
    [HttpPost]
    public void Post([FromBody]ContestInfo value)
    {
      if (string.IsNullOrEmpty(value.Id))
      {
        value.Id = Guid.NewGuid().ToString();
      }

      _session.Store(value);
      _session.SaveChanges();
    }

    [HttpDelete]
    [Route("{id}")]
    public void Delete(int id)
    {
      _session.Delete<ContestInfo>(id);
      _session.SaveChanges();
    }

    protected override void Dispose(bool disposing)
    {
      _session.Dispose();
      base.Dispose(disposing);
    }
  }
}
