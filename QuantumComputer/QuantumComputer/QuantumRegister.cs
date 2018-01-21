
using QuantumComputer.QubitComputations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumComputer
{
    public class QuantumRegister
    {
        public int size { get; set; }
        public Qubit[] Qubits { get; set; }
        private double[] amplitudes { get; set; }
       
        public QuantumRegister(IEnumerable<Qubit> qubits)
        {
            Qubits = qubits.ToArray();
            amplitudes = new double[(2).Pow(Qubits.Length)];
            Evaluate();
        }
        public void Evaluate()
        {
            FillAmplitudes();
            amplitudes.Normalize();
        }

        private void FillAmplitudes()
        {
            var resmatrix = Qubits[0].GetAsMatrix();
            for (int i = 1; i < Qubits.Length; i++)
            {
                resmatrix = Matrix.TensorProduct(resmatrix, Qubits[i].GetAsMatrix());
            }
            for (int i = 0; i < resmatrix.Length; i++)
            {
                amplitudes[i] = resmatrix[i][i];
            }
        }
        /// <summary>
        /// Use decoherence on register
        /// </summary>
        /// <returns>String decribing final qubits state</returns>
        internal string Measure()
        {
            var rnd = new Random();
            var tobeFound = rnd.NextDouble();
            var index = -1;
            var sum = 0;
            var previous = 0D;
            var next = amplitudes[0];
            for (int i = 1; i < amplitudes.Length; i++)
            {
                if (previous <= tobeFound && tobeFound <= next)
                {
                    index = i-1;
                    break;
                }
                previous += amplitudes[i];
                if (i < amplitudes.Length)
                    next += amplitudes[i + 1];
                else
                    index = i;
            }
            return $"|{Convert.ToString(index, 2).PadLeft(Qubits.Length, '0')}>";
        }
    }
}
