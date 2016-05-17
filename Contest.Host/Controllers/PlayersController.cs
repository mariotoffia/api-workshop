using System.Linq;
using System.Web.OData;
using Contest.Api.Model;
using Contest.Host.Db;
using Marten;

namespace Contest.Host.Controllers
{
  public sealed class PlayersController : ODataController
  {
    private readonly IDocumentSession _session;

    public PlayersController(IStore store)
    {
      _session = store.OpenSession();
    }

    [EnableQuery(PageSize = 100, MaxExpansionDepth = 10)]
    public IQueryable<Player> Get()
    {
      return _session.Query<Player>();
    }

    protected override void Dispose(bool disposing)
    {
      _session?.Dispose();
      base.Dispose(disposing);
    }
  }
}