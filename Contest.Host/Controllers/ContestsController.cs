using System.Linq;
using System.Web.OData;
using Contest.Api.Model;
using Contest.Host.Db;
using Marten;

namespace Contest.Host.Controllers
{
  public sealed class ContestsController : ODataController
  {
    private readonly IDocumentSession _session;

    public ContestsController(IStore store)
    {
      _session = store.OpenSession();
    }

    [EnableQuery(PageSize = 100, MaxExpansionDepth = 10)]
    public IQueryable<ContestInfo> Get()
    {
      return _session.Query<ContestInfo>();
    }

    protected override void Dispose(bool disposing)
    {
      _session?.Dispose();
      base.Dispose(disposing);
    }
  }
}