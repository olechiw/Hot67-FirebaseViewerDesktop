using System.Data;

namespace BluetoothScouterPits.Interfaces
{
    public interface ICalculatedColumnsObject
    {
        DataTable AverageColumns { get; }
        DataTable SumColumns { get; }
        DataTable MinimumColumns { get; }
        DataTable MaximumColumns { get; }
    }
}