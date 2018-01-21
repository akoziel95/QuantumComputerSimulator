using QuantumComputer.QubitComputations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuantumComputer.Gates
{
    class CNotGate : IStage
    {
        public CNotGate(int control, int target)
        {
            _control = control;
            _target = target;
        }
        public int size => 2;
        private int _control;
        private int _target;

        public QuantumRegister Compute(QuantumRegister register)
        {
            var control = register.Qubits[_control];
            var target = register.Qubits[_target];

            register.Qubits[_target]= Qubit.CNOT(control, target);
            return register;
        }
    }
}
