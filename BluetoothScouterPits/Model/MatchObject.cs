using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BluetoothScouterPits.Model
{
    // A match object with publicly accessible matchnumber and teamnumber, 
    // in addition to existing tags. This way json ops are not exposed outside of this class
    public class MatchObject
    {
        public const string JsonMatchNumberTag = "Match Number";
        public const string JsonTeamNumberTag = "Team Number";

        private readonly JObject values;

        public MatchObject(JObject json)
        {
            values = json;
            MatchNumber = json[JsonMatchNumberTag].Value<string>();
            TeamNumber = json[JsonTeamNumberTag].Value<string>();
            json.Remove(JsonMatchNumberTag);
            json.Remove(JsonTeamNumberTag);
        }

        public string MatchNumber { get; }
        public string TeamNumber { get; }

        // GetValue a specific value based on key
        public string GetValue(string key)
        {
            return values[key].Value<string>();
        }

        // GetValue all values
        public List<string> GetAllValues()
        {
            return values.PropertyValues()
                .Select(r => r.Value<string>()).ToList();
        }

        // GetValue all keys
        public List<string> GetAllKeys()
        {
            return values.Properties().Select(p => p.Name).ToList();
        }
    }
}