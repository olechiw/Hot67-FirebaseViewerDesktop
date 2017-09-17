using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json.Linq;

namespace BluetoothScouterPits
{
    internal class DataSource
    {
        private const string JsonMatchNumberTag = "Match Number";
        private const string JsonTeamNumberTag = "Team Number";

        private readonly Settings settings;

        private FirebaseClient database;

        public DataSource(Settings firebaseSettings)
        {
            settings = firebaseSettings;

            RefreshCredentials();
        }


        public async Task<List<MatchObject>> Get()
        {
            try
            {
                return await Get(settings.EventName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<MatchObject>> Get(string name)
        {
            var result = await database.Child(name).OnceAsync<JObject>();
            var results = new List<MatchObject>();
            foreach (var r in result)
                results.Add(new MatchObject(r.Object));
            return results;
        }

        public void RefreshCredentials()
        {
            // Initialize connection with Firebase
            database = new FirebaseClient(settings.DatabaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => LoginASync()
                });
        }

        // The authentication method
        public async Task<string> LoginASync()
        {
            var authProvider = new FirebaseAuthProvider(
                new FirebaseConfig(settings.ApiKey));
            var auth = await authProvider
                .SignInWithEmailAndPasswordAsync(
                    settings.Username, settings.Password);

            return auth.FirebaseToken;
        }

        // A match object with publicly accessible matchnumber and teamnumber, 
        // in addition to existing tags. This way json ops are not exposed outside of this class
        public class MatchObject
        {
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
}