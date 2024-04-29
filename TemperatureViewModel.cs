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
