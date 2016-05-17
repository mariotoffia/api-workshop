using System.ComponentModel;
using System.Runtime.Serialization;
using SwaggerWcf.Attributes;

namespace Contest.Api.Model
{
  [DataContract]
  [Description("Player for a contest")]
  [SwaggerWcfDefinition(ExternalDocsUrl = "http://en.wikipedia.org/wiki/Player", ExternalDocsDescription = "Description of a player")]
  public class Player
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
  }
}
