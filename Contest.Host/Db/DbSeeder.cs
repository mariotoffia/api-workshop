using System;
using System.Diagnostics;
using Contest.Api.Model;
using Marten;

namespace Contest.Host.Db
{
  internal static class DbSeeder
  {
    private static int _idx = 1;

    internal static void Seed(IDocumentSession session)
    {
      try
      {
        _idx = 1;
        session.Store(new ContestInfo {Id = NextIntId(), Location = "Skelleftea"},
          new ContestInfo {Id = NextIntId(), Location = "Stockholm"},
          new ContestInfo {Id = NextIntId(), Location = "Linkoping"},
          new ContestInfo {Id = NextIntId(), Location = "Malmo"});

        _idx = 1;
        session.Store(new Player {Id = NextIntId(), FirstName = "Mario", LastName = "Toffia"},
          new Player {Id = NextIntId(), FirstName = "Johannes", LastName = "Svensson"},
          new Player {Id = NextIntId(), FirstName = "Johan", LastName = "Zenk"});

        _idx = 2;
        session.Store(
          new PlayerInContest
          {
            Id = NextIntId(),
            ContestId = 1.ToString(),
            FirstName = "Mario",
            LastName = "Toffia",
            Weight = 80
          },
          new PlayerInContest
          {
            Id = NextIntId(),
            ContestId = 1.ToString(),
            FirstName = "Johannes",
            LastName = "Svensson",
            Weight = 84
          },
          new PlayerInContest
          {
            Id = NextIntId(),
            ContestId = 3.ToString(),
            FirstName = "Johan",
            LastName = "Zenk",
            Weight = 76
          });

        session.SaveChanges();
      }
      catch (Exception e)
      {
        Debug.WriteLine($"Error While initializing the Database msg = {e.Message}");
        throw;
      }
    }

    private static string NextIntId()
    {
      return _idx++.ToString();
    }
  }
}