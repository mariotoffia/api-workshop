using Contest.Host.Db;
using StructureMap;

namespace Contest.Host
{
  public sealed class MyRegistry : Registry 
  {
    public MyRegistry()
    {
      For<IStore>().Use<DbStore>().Singleton();
    }
  }
}
