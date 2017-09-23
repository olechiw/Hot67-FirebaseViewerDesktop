namespace BluetoothScouterPits
{
    public interface IFirebaseSettingsObject
    {
        string Username { get; }
        string Password { get; }
        string ApiKey { get; }
        string EventName { get; }
        string DatabaseUrl { get; }
    }
}