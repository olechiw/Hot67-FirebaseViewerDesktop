namespace BluetoothScouterPits.Interfaces
{
    public interface IFirebaseSettingsObject
    {
        string Email { get; }
        string Password { get; }
        string ApiKey { get; }
        string EventName { get; }
        string DatabaseUrl { get; }
    }
}