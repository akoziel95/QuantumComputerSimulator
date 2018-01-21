using QuantumComputer.Gates;
using QuantumComputer.QubitComputations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumComputer
{
    public class QuantumCircuit
    {
        private List<IStage> stages;
        private QuantumRegister _register;

        public QuantumCircuit(QuantumRegister register)
        {
            stages = new List<IStage>();
            _register = register;
        }
        /// <summary>
        /// Add stage to be performed on Compute()
        /// </summary>
        /// <param name="gate">Type of gate</param>
        /// <param name="qubitIndex">First argument - target Qubit index, optional second argument - control Qubit index</param>
        public void AddStage(Gate gate, params int[] qubitIndex)
        {
            Qubit control = null;
            int controlIndex = -1;
            var targetIndex = qubitIndex[0];
            
            if (qubitIndex.Length > 1)
            {                controlIndex = qubitIndex[1];
               
            }

            switch (gate)
            {
                case Gate.Hadamard:
                    stages.Add(new HadamardGate(targetIndex));
                    break;
                case Gate.CNot:
                    stages.Add(new CNotGate(controlIndex, targetIndex));
                    break;
                case Gate.PhaseShift:
                    stages.Add(new PhaseShiftGate(targetIndex));
                    break;
                case Gate.PauliX:
                    stages.Add(new PauliX(targetIndex));
                    break;
                case Gate.PauliY:
                    stages.Add(new PauliY(targetIndex));
                    break;
                case Gate.PauliZ:
                    stages.Add(new PauliZ(targetIndex));
                    break;
            }


        }

        public void Compute()
        {
            foreach (var stage in stages)
            {
                _register = stage.Compute(_register);
            }
        }


        public string Measure()
        {
            _register.Evaluate();
            return _register.Measure();
        }
    }
  
}
