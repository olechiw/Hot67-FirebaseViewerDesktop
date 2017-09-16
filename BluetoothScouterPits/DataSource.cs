using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json.Linq;

namespace BluetoothScouterPits
{
    class DataSource
    {
        private const string ApiKey = "AIzaSyB8lwXAKsGt4mKZB-3aFyPbxEXDDA6HSTE";
        private readonly FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        private FirebaseClient database;

        private const string jsonMatchNumberTag = "matchNumber";

        private string username = "";
        private string password = "";
        public void SetUsername(string user)
        {
            username = user;
        }
        public void SetPassword(string pass)
        {
            password = pass;
        }

        private string databaseName = "";
        public void SetDatabase(string data)
        {
            databaseName = data;
        }

        public async Task<List<JObject>> Get()
        {
            return await Get(databaseName);
        }

        public async Task<List<JObject>> Get(string databaseName)
        {
            var result = (await database.Child("Values").OnceAsync<JObject>());
            List<JObject> results = new List<JObject>();
            foreach (var r in result)
            {
                results.Add(r.Object);
            }
            return results;
        }

        public DataSource(string user, string pass)
        {
            username = user;
            password = pass;

            SetupDatabase();
        }

        private async void SetupDatabase()
        {
            // Initialize connection with Firebase
            database = new FirebaseClient("https://hot-67-scouting.firebaseio.com",
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => LoginASync()
                });
        }

        public async Task<String> LoginASync()
        {
            var authProvider = new FirebaseAuthProvider(
                new FirebaseConfig("AIzaSyB8lwXAKsGt4mKZB-3aFyPbxEXDDA6HSTE"));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(username, password);
            return auth.FirebaseToken;
        }
    }
}