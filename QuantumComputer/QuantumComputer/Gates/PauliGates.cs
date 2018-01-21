using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantumComputer.QubitComputations;

namespace QuantumComputer.Gates
{
    public class PauliX : IStage
    {
        private int _target;

        public PauliX(int target)
        {
            _target = target;
        }
        public QuantumRegister Compute(QuantumRegister register)
        {
            register.Qubits[_target] = register.Qubits[_target].PauliX();
            return register;
        }
    }
    public class PauliY : IStage
    {
        private int _target;

        public PauliY(int target)
        {
            _target = target;
        }
        public QuantumRegister Compute(QuantumRegister register)
        {
            register.Qubits[_target] = register.Qubits[_target].PauliY();
            return register;
        }
    }
    public class PauliZ : IStage
    {
        private int _target;

        public PauliZ(int target)
        {
            _target = target;
        }
        public QuantumRegister Compute(QuantumRegister register)
        {
            register.Qubits[_target] = register.Qubits[_target].PauliY();
            return register;
        }
    }
}
