﻿using System.ServiceModel;
using System.ServiceModel.Web;
using Contest.Api.Model;
using SwaggerWcf.Attributes;

namespace Contest.Api
{
  [ServiceContract]
  public interface IContestService
  {
    #region Players

    [SwaggerWcfPath("Create Player",
      "Creates a **Player** that can participate in contests. This player resides in " +
      "the registry and will be copied each time a add player into a contest is invoked.\r\n" +
      "## Throttling\r\n" +
      "This method is throttled and thus if **HTTP** response code 429 is returned, please try again " +
      "When the player is created this limit is described in the **X-Rate-Limit-Limit** header.")]
    [WebInvoke(UriTemplate = "/players", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    Player CreatePlayer(
      [SwaggerWcfParameter(true, "Player to be created, the id will be set by server")] Player player);

    [SwaggerWcfPath("Get Players", "Retrieve all players")]
    [WebGet(UriTemplate = "/players/", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    Player[] GetPlayers();

    [SwaggerWcfPath("Get Players", "Retrieve a single player by using its player id")]
    [WebGet(UriTemplate = "/players/{id}", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    Player GetPlayerById([SwaggerWcfParameter(true, "Id of the player to get")] string id);

    [SwaggerWcfPath("Update Player", "Update a player")]
    [WebInvoke(UriTemplate = "/players/{id}", BodyStyle = WebMessageBodyStyle.Bare, Method = "PUT",
      RequestFormat = WebMessageFormat.Json)]
    [OperationContract]
    Player UpdatePlayer([SwaggerWcfParameter(true, "Player id of the player to update")] string id,
      [SwaggerWcfParameter(true, "Player to be updated, note that the id must be properly set")] Player player);

    [SwaggerWcfPath("Delete Player", "Permanently delete a player. However if he or she has participated " +
      "in one or more contest, those are persisted. This is delete from the registry of players.")]
    [WebInvoke(UriTemplate = "/players/{id}", BodyStyle = WebMessageBodyStyle.Bare, Method = "DELETE")]
    [OperationContract]
    void DeletePlayer([SwaggerWcfParameter(true, "Id of the player to delete")] string id);

    #endregion Players

    #region Contest

    [SwaggerWcfPath("Create Constest", "Creates a contest that players can participate in")]
    [WebInvoke(UriTemplate = "/contests", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    ContestInfo CreateContest(
      [SwaggerWcfParameter(true, "Contest to be created, the id will be set by server")] ContestInfo contest);

    [OperationContract]
    [SwaggerWcfPath("Add player", "Adds a player to a specific contest")]
    [WebInvoke(UriTemplate = "/contests/{contestId}/players/{playerId}", BodyStyle = WebMessageBodyStyle.Bare,
      Method = "POST",
      RequestFormat = WebMessageFormat.Json)]
    PlayerInContest AddPlayer(
      [SwaggerWcfParameter(true, "The constest id to add this player to")] string contestId,
      [SwaggerWcfParameter(true, "The player id to add to th contest")] string playerId);

    [SwaggerWcfPath("Get players in contest", "Get all players in a specific contest")]
    [WebGet(UriTemplate = "/contests/{contestId}/players", BodyStyle = WebMessageBodyStyle.Bare,
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    PlayerInContest[] GetContestParticipants([SwaggerWcfParameter(true, "Id of the contest")] string contestId);

    #endregion
  }
}