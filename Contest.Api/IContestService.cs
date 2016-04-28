using System.ServiceModel;
using System.ServiceModel.Web;
using Contest.Api.Model;
using SwaggerWcf.Attributes;

namespace Contest.Api
{
  [ServiceContract]
  public interface IContestService
  {
    #region Players

    [SwaggerWcfPath("Create player", "Creates a player that can participate in contests")]
    [WebInvoke(UriTemplate = "/players", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    Player CreatePlayer(
      [SwaggerWcfParameter(Description = "Player to be created, the id will be set by server")] Player player);

    [SwaggerWcfPath("Get players", "Retrieve all players")]
    [WebGet(UriTemplate = "/players", BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    Player[] GetPlayers();

    [SwaggerWcfPath("Get players", "Retrieve a single player by using its player id")]
    [WebGet(UriTemplate = "/players/{id}", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    Player GetPlayerById(string id);

    [SwaggerWcfPath("Update player", "Update a player")]
    [WebInvoke(UriTemplate = "/players/{id}", BodyStyle = WebMessageBodyStyle.Bare, Method = "PUT",
      RequestFormat = WebMessageFormat.Json)]
    [OperationContract]
    Player UpdatePlayer(
      [SwaggerWcfParameter(Description = "Player to be updated, note that the id must be properly set")] Player player);

    [OperationContract]
    [WebInvoke(UriTemplate = "/players/{id}", BodyStyle = WebMessageBodyStyle.Bare, Method = "DELETE")]
    void DeletePlayer(string id);

    #endregion Players

    #region Contest

    [SwaggerWcfPath("Create constest", "Creates a contest that players can participate in")]
    [WebInvoke(UriTemplate = "/contests", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    ContestInfo CreateContest(
      [SwaggerWcfParameter(Description = "Contest to be created, the id will be set by server")] ContestInfo contest);

    [OperationContract]
    [SwaggerWcfPath("Add player", "Adds a player to a specific contest")]
    [WebInvoke(UriTemplate = "/contests/{contestId}/players/{playerId}", BodyStyle = WebMessageBodyStyle.Bare,
      Method = "POST",
      RequestFormat = WebMessageFormat.Json)]
    PlayerInContest AddPlayer(
      [SwaggerWcfParameter(Description = "The constest id to add this player to")] string contestId,
      [SwaggerWcfParameter(Description = "The player id to add to th contest")] string playerId);

    [SwaggerWcfPath("Get players in contest", "Get all players in a specific contest")]
    [WebGet(UriTemplate = "/contests/{contestId}/players", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    PlayerInContest[] GetContestParticipants(string contestId);

    #endregion
  }
}