using System.Linq;
using System.Web.OData;
using Contest.Api.Model;
using Contest.Host.Db;
using Marten;

namespace Contest.Host.Controllers
{
  public sealed class PlayerInContestsController : ODataController
  {
    private readonly IDocumentSession _session;

    public PlayerInContestsController(IStore store)
    {
      _session = store.OpenSession();
    }

    [EnableQuery(PageSize = 100, MaxExpansionDepth = 10)]
    public IQueryable<PlayerInContest> Get()
    {
      return _session.Query<PlayerInContest>();
    }

    protected override void Dispose(bool disposing)
    {
      _session?.Dispose();
      base.Dispose(disposing);
    }
  }
}