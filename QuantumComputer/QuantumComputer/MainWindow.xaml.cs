using QuantumComputer.Gates;
using QuantumComputer.QubitComputations;
using System;
using System.Windows;

namespace QuantumComputer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var alpha = 0.8;
            var beta = 0.6;

            var q1 = new Qubit(0.8, 0.6);
            var q2 = new Qubit(0.1, 0.9);
            var q3 = new Qubit(0.5, 0.5);

            var qubits = new[] { q1, q2, q3 };
            
            var qr = new QuantumRegister(qubits);


            var circuit = new QuantumCircuit(qr);
            circuit.AddStage(Gate.Hadamard, 1);
            circuit.AddStage(Gate.CNot, 1, 2);
            circuit.AddStage(Gate.Hadamard, 0);
            circuit.AddStage(Gate.CNot, 0, 1);
            circuit.AddStage(Gate.PhaseShift, 2);
            circuit.AddStage(Gate.Hadamard, 2);
            circuit.Compute();
            var resultState = circuit.Measure();
            var resultState1 = circuit.Measure();
            var resultState2 = circuit.Measure();
            var resultState3 = circuit.Measure();




        }
    }
}
