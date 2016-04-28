using System;
using System.Net;
using System.ServiceModel.Web;
using Contest.Api.Model;
using SwaggerWcf.Attributes;

namespace Contest.Api
{
  [SwaggerWcf("/v1/rest")]
  public sealed class ContestService : IContestService
  {
    [SwaggerWcfTag("Players")]
    [SwaggerWcfResponse(HttpStatusCode.Created, "Player created, value in the response body with id updated")]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request", true)]
    [SwaggerWcfResponse(HttpStatusCode.InternalServerError,
      "Internal error (can be forced using ERROR_500 as player.FirstName)", true)]
    public Player CreatePlayer(Player player)
    {
      var woc = WebOperationContext.Current;
      if (null == woc)
      {
        return null;
      }

      if (player == null)
      {
        woc.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
        return null;
      }

      if (player.FirstName == "ERROR_500")
      {
        woc.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
        return null;
      }

      player.Id = Guid.NewGuid().ToString("N");
      woc.OutgoingResponse.StatusCode = HttpStatusCode.Created;
      return player;
    }

    [SwaggerWcfTag("Players")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Player found, value in the response body")]
    [SwaggerWcfResponse(HttpStatusCode.NoContent, "No players", true)]
    public Player[] GetPlayers()
    {
      var woc = WebOperationContext.Current;
      if (null == woc)
      {
        return null;
      }

      // Find players - if found - woc.OutgoingResponse.StatusCode = HttpStatusCode.OK; and return array

      woc.OutgoingResponse.StatusCode = HttpStatusCode.NoContent;
      return null;
    }

    [SwaggerWcfTag("Players")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Player found, values in the response body")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player not found", true)]
    public Player GetPlayerById(string id)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("Players")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Player found, values in the response body")]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player not found", true)]
    public Player UpdatePlayer(Player player)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("Players")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Player found, values in the response body")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player not found", true)]
    [SwaggerWcfResponse(HttpStatusCode.NoContent, "Book deleted")]
    public void DeletePlayer(string id)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("Contests")]
    [SwaggerWcfResponse(HttpStatusCode.Created, "Contest created, value in the response body with id updated")]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request", true)]
    public ContestInfo CreateContest(ContestInfo contest)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("Contests")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Contest and Player found, added player is in response payload")]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player or contest not found", true)]
    public PlayerInContest AddPlayer(string contestId, string playerId)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("Contests")]
    [SwaggerWcfResponse(HttpStatusCode.OK,
      "Players found in the specified contest, all participants in response payload")]
    [SwaggerWcfResponse(HttpStatusCode.NoContent, "No players", true)]
    public PlayerInContest[] GetContestParticipants(string contestId)
    {
      throw new NotImplementedException();
    }
  }
}