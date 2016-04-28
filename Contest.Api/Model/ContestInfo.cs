using System.ComponentModel;
using System.Runtime.Serialization;
using SwaggerWcf.Attributes;

namespace Contest.Api.Model
{
  [DataContract]
  [Description("A contest to be run")]
  [SwaggerWcfDefinition(ExternalDocsUrl = "http://en.wikipedia.org/wiki/Contest", ExternalDocsDescription = "Description of a contest")]

  public sealed class ContestInfo
  {
    [DataMember]
    [Description("The unique contest id")]
    public string Id { get; set; }
    [DataMember]
    [Description("The location where the contest will be held")]
    public string Location { get; set; }
  }
}
