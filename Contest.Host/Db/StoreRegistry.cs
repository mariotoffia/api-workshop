using Contest.Api.Model;
using Contest.Host.Model;
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

      For<Kund>().Searchable(x => x.Id);
      For<Kundnummer>().Searchable(x => x.Kod).Searchable(x => x.Kund);
    }
  }
}
