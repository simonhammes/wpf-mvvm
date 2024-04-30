namespace MVVM;

using System;
using System.ComponentModel;
using System.Windows.Input;

public class TemperatureViewModel : INotifyPropertyChanged
{
    private double _celsius;
    private double _fahrenheit;
    private double _kelvin;
    
    public double Celsius
    {
        get => _celsius;
        set { _celsius = value; OnPropertyChanged(nameof(Celsius)); }
    }

    public double Fahrenheit
    {
        get => _fahrenheit;
        set { _fahrenheit = value; OnPropertyChanged(nameof(Fahrenheit)); }
    }

    public double Kelvin
    {
        get => _kelvin;
        set { _kelvin = value; OnPropertyChanged(nameof(Kelvin)); }
    }

    // TODO: Use ObservableCollection?
    private List<string> _comboBox1Options = ["All Values", "ABC", "DEF"];
    public List<string> ComboBox1Options
    {
        get => _comboBox1Options;
        set { _comboBox1Options = value; OnPropertyChanged(nameof(ComboBox1Options)); }
    }

    private string _selectedOption1 = "All Values";

    public string SelectedOption1
    {
        get => _selectedOption1;
        set
        {
            _selectedOption1 = value;
            UpdateFilterValues();
            OnPropertyChanged(nameof(SelectedOption1));
        }
    }

    // TODO: Use ObservableCollection?
    private List<string> _comboBox2Options = ["1", "2", "3", "4", "5", "6"];
    public List<string> ComboBox2Options
    {
        get => _comboBox2Options;
        set { _comboBox2Options = value; OnPropertyChanged(nameof(ComboBox2Options)); }
    }

    public string SelectedOption2 { get; set; } = "1";

    public ICommand ConvertCommand { get; private set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public TemperatureViewModel()
    {
        ConvertCommand = new RelayCommand(ConvertTemperatures);
    }

    private void ConvertTemperatures(object obj)
    {
        Fahrenheit = (Celsius * 9.0 / 5.0) + 32;
        Kelvin = Celsius + 273.15;
    }

    private void OnPropertyChanged(string name)
    {
        // Console.WriteLine($"{name} changed");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private void UpdateFilterValues()
    {
        ComboBox2Options = _selectedOption1 switch
        {
            "All Values" => ["1", "2", "3", "4", "5", "6"],
            "ABC" => ["1", "2", "3"],
            "DEF" => ["4", "5", "6"],
            _ => ComboBox2Options
        };
    }
}

public class RelayCommand(Action<object> executeAction) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        Console.WriteLine(parameter);
        executeAction(parameter!);
    }
}
