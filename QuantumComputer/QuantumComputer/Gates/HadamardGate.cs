using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantumComputer.QubitComputations;

namespace QuantumComputer.Gates
{
    public class HadamardGate : IStage
    {
        private int _target;

        public HadamardGate(int target)
        {
           _target = target;
        }
        public QuantumRegister Compute(QuantumRegister register)
        {
            register.Qubits[_target]= register.Qubits[_target].Hadamard();
            return register;
        }

    }
}
