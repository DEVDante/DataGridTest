using Prism.Commands;
using PropertyChanged.SourceGenerator;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace DataGridTest;

internal partial class ViewModel
{
    [Notify] private ObservableCollection<ObservableCollection<string>> _gridData;
    private string _cellContent;
    [Notify] private bool _textUnderline;

    public string CellContent
    {
        get => _cellContent;
        set
        {
            _cellContent = value;
            var rowCount = _gridData.Count;
            var cellCount = _gridData.FirstOrDefault()?.Count ?? 0;
            GridData = new ObservableCollection<ObservableCollection<string>>();
            for (var i = 0; i < rowCount; i++)
            {
                var row = new ObservableCollection<string>();
                for (var j = 0; j < cellCount; j++) row.Add(CellContent);
                GridData.Add(row);
            }
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CellContent)));
        }
    }

    #region Commands

    public ICommand AddColumn { get; } = new DelegateCommand<ViewModel>(vm =>
    {
        foreach (var row in vm.GridData) row.Add(vm.CellContent);
    });

    public ICommand RemoveColumn { get; } = new DelegateCommand<ViewModel>(vm =>
    {
        if (vm.GridData.FirstOrDefault()?.Count <= 0) return;
        foreach (var row in vm.GridData) row.RemoveAt(row.Count - 1);
    });

    public ICommand Underline { get; } = new DelegateCommand<ViewModel>(vm =>
    {
        vm.TextUnderline = true;
    });

    #endregion

    public ViewModel()
    {
        _gridData = new ObservableCollection<ObservableCollection<string>>
        {
            new() { "a", "a", "a" },
            new() { "a", "a", "a" },
            new() { "a", "a", "a" },
        };
        _cellContent = "null";
    }
}