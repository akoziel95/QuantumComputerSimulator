using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumComputer.Gates
{
    interface IQGate
    {
        QuantumRegister Compute(QuantumRegister register);

    }
}
