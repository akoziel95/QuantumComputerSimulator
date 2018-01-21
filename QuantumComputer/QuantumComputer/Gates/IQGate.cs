using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantumComputer.QubitComputations;

namespace QuantumComputer.Gates
{
    public interface IStage
    {
        QuantumRegister Compute(QuantumRegister register);
    }
    public interface IQSingleGate
    {
        Qubit Compute(Qubit qubit);

    }

    public interface IQDoubleGate
    {
        Qubit Compute(Qubit control, Qubit target);

    }
    public enum Gate
    {
        Hadamard,
        PhaseShift,
        PauliX,
        PauliY,
        PauliZ,
        CNot
    }
}
