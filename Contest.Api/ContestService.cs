using System;
using System.Net;
using System.ServiceModel.Web;
using Contest.Api.Model;
using SwaggerWcf.Attributes;

namespace Contest.Api
{
  [SwaggerWcfTag("contest")]
  [SwaggerWcf("/v1/rest")]
  public sealed class ContestService : IContestService
  {
    [SwaggerWcfTag("player")]
    [SwaggerWcfResponse(HttpStatusCode.Created, "Player created, value in the response body with id updated",
      Headers = new []{ "X-Rate-Limit-Limit" })]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request", true)]
    [SwaggerWcfResponse(429, "Request Limit", true, Headers = new[] { "X-Rate-Limit-Limit" })]
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

    [SwaggerWcfTag("player")]
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

    [SwaggerWcfTag("player")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Player found, values in the response body")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player not found", true)]
    public Player GetPlayerById(string id)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("player")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Player found, values in the response body")]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player not found", true)]
    public Player UpdatePlayer(string id, Player player)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("player")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Player found, values in the response body")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player not found", true)]
    [SwaggerWcfResponse(HttpStatusCode.NoContent, "Book deleted")]
    public void DeletePlayer(string id)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("contest")]
    [SwaggerWcfResponse(HttpStatusCode.Created, "Contest created, value in the response body with id updated")]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request", true)]
    public ContestInfo CreateContest(ContestInfo contest)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("contest")]
    [SwaggerWcfResponse(HttpStatusCode.OK, "Contest and Player found, added player is in response payload")]
    [SwaggerWcfResponse(HttpStatusCode.BadRequest, "Bad request")]
    [SwaggerWcfResponse(HttpStatusCode.NotFound, "Player or contest not found", true)]
    public PlayerInContest AddPlayer(string contestId, string playerId)
    {
      throw new NotImplementedException();
    }

    [SwaggerWcfTag("contest")]
    [SwaggerWcfResponse(HttpStatusCode.OK,
      "Players found in the specified contest, all participants in response payload")]
    [SwaggerWcfResponse(HttpStatusCode.NoContent, "No players", true)]
    public PlayerInContest[] GetContestParticipants(string contestId)
    {
      throw new NotImplementedException();
    }
  }
}