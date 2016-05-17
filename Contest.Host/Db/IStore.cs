using Marten;

namespace Contest.Host.Db
{
  public interface IStore
  {
    IDocumentSession OpenSession();
  }
}
