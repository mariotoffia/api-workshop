using Contest.Api.Model;
using Marten;

namespace Contest.Host.Db
{
  public sealed class StoreRegistry : MartenRegistry
  {
    public StoreRegistry()
    {
      For<ContestInfo>().Searchable(x => x.Location);
      For<Player>().Searchable(x => x.FirstName).Searchable(x => x.LastName);
      For<PlayerInContest>().Searchable(x => x.FirstName).Searchable(x => x.LastName);
    }
  }
}
