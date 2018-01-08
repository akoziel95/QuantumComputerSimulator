using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumComputer.Gates
{
    class CNotGate : IQGate
    {
        public int size => 2;
        private double[][] _matrix;
        public CNotGate()
        {
            // 1 0 0 0
            // 0 1 0 0
            // 0 0 0 1
            // 0 0 1 0
            _matrix = new double[4][] {
                new double[] {1,0,0,0},
                new double[] {1,1,0,0},
                new double[] {1,0,0,1 },
                new double[] {0,0,1,0 }
            };
        }

        public QuantumRegister Compute(QuantumRegister register)
        {
            throw new NotImplementedException();
        }
    }
}
