using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantumComputer.QubitComputations;

namespace QuantumComputer.Gates
{
    public class PhaseShiftGate : IStage
    {
        private int _target;

        public PhaseShiftGate(int target)
        {
            _target = target;
        }
        private double theta => Math.PI / 2;
        private double phi => Math.PI / 3;
        public QuantumRegister Compute(QuantumRegister register)
        {
            register.Qubits[_target] = register.Qubits[_target].PhaseShift(theta, phi);
            return register;
        }
        
        
    }
}
