using System;
using Marten;
using Marten.Schema;

namespace Contest.Host.Db
{
  public sealed class DbStore : IStore, IDisposable
  {
    private const string PostgresConnectionString =
      "Server=docker;Port=40001;Userid=postgres;Password=mysecretpassword;" +
      "Pooling=false;MinPoolSize=1;MaxPoolSize=20;" +
      "Timeout=15;SslMode=Disable;Database=postgres";

    private readonly DocumentStore _store;

    public DbStore()
    {
      _store = DocumentStore.For(_ =>
      {
        _.Connection(PostgresConnectionString);
        _.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
        _.UpsertType = PostgresUpsertType.Standard;
        _.Logger(new ConsoleMartenLogger());
        _.Schema.Include<StoreRegistry>();
      });

      using (var session = OpenSession())
      {
        DbSeeder.Seed(session);
      }
    }

    public void Dispose()
    {
      _store.Dispose();
    }

    public IDocumentSession OpenSession()
    {
      return _store.OpenSession();
    }
  }
}