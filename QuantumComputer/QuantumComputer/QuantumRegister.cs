using Qubit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumComputer
{
    class QuantumRegister
    {
        public int size { get; set; }
        public qubit[] Qubits { get; set; }
        private complex[] stateRegister { get; set; }
        private double[] amplitudes { get; set; }
       
        public QuantumRegister(IEnumerable<qubit> qubits)
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

        private void RandomAmplitudes()
        {
            var rnd = new Random();
            amplitudes = amplitudes.Select(o => rnd.NextDouble()).ToArray();
        }

        public void Hadamard(int qubitindex)
        {
            Action hadamard = () => Qubits[qubitindex] = Qubits[qubitindex].Hadamard();
            ComputeGate(hadamard);
        }

        internal void CNot(int v1, int v2)
        {
            Action cNot = () => Qubits[v2] = qubit.CNOT(Qubits[v1], Qubits[v2]);
            ComputeGate(cNot);
        }

        internal void ComputeGate(Action gate)
        {
            gate.Invoke();
            Evaluate();
        }

        private double[][] GetHadamardMatrix(int length)
        {         
            var res = Matrix.TensorProduct(Matrix.HadamardBasic, Matrix.IdentityTwo);
            var res1 = Matrix.TensorProduct(Matrix.IdentityTwo, res);
            return Matrix.MultiplyByScalar(res, 1/Math.Sqrt(2));
        }

      
    }
}
