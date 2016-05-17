using System.ComponentModel;
using System.Runtime.Serialization;
using SwaggerWcf.Attributes;

namespace Contest.Api.Model
{
  [DataContract]
  [Description("Denotes a player participating in a specific contest")]
  [SwaggerWcfDefinition(ExternalDocsUrl = "http://en.wikipedia.org/wiki/PlayerInContest", ExternalDocsDescription = "Description of a player in contest")]

  public sealed class PlayerInContest
  {
    [DataMember(IsRequired = true)]
    [Description("A unique id of the player")]
    public string Id { get; set; }
    [DataMember]
    [Description("The players first name, if multiple separate with spaces")]
    public string FirstName { get; set; }
    [DataMember]
    [Description("The players last name")]
    public string LastName { get; set; }
    [DataMember]
    [Description("The id of the contest where this player will or participates in")]
    public string ContestId { get; set; }
    [DataMember]
    [Description("The weight when competeing")]
    public int Weight { get; set; }
  }
}
