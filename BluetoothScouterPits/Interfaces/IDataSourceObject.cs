using System.Collections.Generic;
using System.Threading.Tasks;
using BluetoothScouterPits.Model;

namespace BluetoothScouterPits.Interfaces
{
    public interface IDataSourceObject
    { 
        // Refresh the firebase credentials
        void RefreshCredentials();
        // Get all of the values of the data source
        Task<List<MatchObject>> Get();
        // Get the value under a specific key, typically just used internally
        Task<List<MatchObject>> Get(string key);
        // Get the top X values of the top
        Task<List<MatchObject>> Get(int top);
        // Set the settings object
        void SetSettings(IFirebaseSettingsObject firebaseSettings);
    }
}