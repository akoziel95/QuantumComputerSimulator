using Qubit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            var q1 = new qubit(alpha, beta);
            var q2 = new qubit(alpha, beta);
            var q3 = new qubit(alpha, beta);

            var qubits = new[] { q1, q2, q3 };
            
            var qr = new QuantumRegister(qubits);
            
            qr.Hadamard(0);
            qr.CNot(0, 1);
        }
    }
}
