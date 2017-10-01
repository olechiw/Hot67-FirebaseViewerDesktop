using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BluetoothScouterPits.Interfaces;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json.Linq;

namespace BluetoothScouterPits.Model
{
    public class DataSource : IDataSourceObject
    {
        private FirebaseClient database;
        private IFirebaseSettingsObject settings;

        public DataSource(IFirebaseSettingsObject firebaseSettings = null)
        {
            SetSettings(firebaseSettings);
        }

        public void SetSettings(IFirebaseSettingsObject firebaseSettings)
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
                return new List<MatchObject>(); // Error looks like no results
            }
        }

        public async Task<List<MatchObject>> Get(string name)
        {
            try
            {
                var result = await database.Child(name).OnceAsync<JObject>();

                return result.Select(r => new MatchObject(r.Object)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<MatchObject>(); // Error looks like no results
            }
        }

        public void RefreshCredentials()
        {
            try
            {
                // Initialize connection with Firebase
                database = new FirebaseClient(settings.DatabaseUrl,
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => LoginASync()
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // The authentication method
        public async Task<string> LoginASync()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(
                    new FirebaseConfig(settings.ApiKey));
                var auth = await authProvider
                    .SignInWithEmailAndPasswordAsync(
                        settings.Email, settings.Password);

                return auth.FirebaseToken;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        public async Task<List<MatchObject>> Get(int top)
        {
            return (await Get()).Take(top).ToList();
        }
    }
}