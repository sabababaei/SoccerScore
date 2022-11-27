using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Enumarations;

[JsonConverter(typeof(StringEnumConverter))]
public enum MatchTypes
{
    [EnumMember(Value = "Friendly")]
    Friendly=1 ,
    [EnumMember(Value = "Confederation")]
    Confederation=2, 
    [EnumMember(Value = "WorldCupQualifier")]
    WorldCupQualifier=3, 
    [EnumMember(Value = "ConfederationTournament")]
    ConfederationTournament=4,
    [EnumMember(Value = "ConfederationsCup")]
    ConfederationsCup=5,
    [EnumMember(Value = "WorldCupMatches")]
    WorldCupMatches=6
}

 