using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

// TODO: create filters (=gates) for each operation (at least Not and CNOT)
// TODO: create way to connect gates in a grid
// TODO: kolla in http://en.wikipedia.org/wiki/Quantum_gate#Pauli-X_gate
namespace QuantumComputer.QubitComputations
{
    /// <summary>
    /// Qubit class built by knowledge provided in videos by Michael Nielsen 
    /// ref. http://michaelnielsen.org/blog/quantum-computing-for-the-determined/
    /// </summary>
    public class Qubit
    {
        /// <summary>
        /// Alpha (α = |0>) component of the qubit.
        /// </summary>
        public complex Alpha;

        /// <summary>
        /// Beta (β = |1>) component of the qubit.
        /// </summary>
        public complex Beta;

        /// <summary>
        /// Constructs a qubit.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        public Qubit(complex alpha, complex beta)
        {
            Debug.Assert(Math.Pow(alpha.Abs(), 2) + Math.Pow(beta.Abs(), 2) - 1 < 0.000001);
            this.Alpha = alpha;
            this.Beta = beta;
        }
        public Qubit()
        {

        }

        /// <summary>
        /// |1>
        /// </summary>
        public static readonly Qubit One = new Qubit(complex.Zero, complex.One);    // TODO: correct?

        /// <summary>
        /// |0>
        /// </summary>
        public static readonly Qubit Zero = new Qubit(complex.One, complex.Zero);   // TODO: correct?

        public double[][] GetAsMatrix()
        {
            return new double[2][]
           {
               new  double[]{Alpha.Abs(), 0 },
                new double[]{0,Beta.Abs() }
           };
        }
        /// <summary>
        /// [ 0 1 ] [ alpha ]
        /// [ 1 0 ] [ beta  ]
        /// </summary>
        /// <returns></returns>
        public Qubit Not()
        {
            return new Qubit(Beta, Alpha);
        }

        /// <summary>
        /// Hadamard gate
        ///    1    [ 1  1 ]
        /// sqrt(2) [ 1 -1 ]
        /// </summary>
        /// <returns></returns>
        public Qubit Hadamard()
        {
            return new Qubit((Alpha + Beta) / Math.Sqrt(2), (Alpha - Beta) / Math.Sqrt(2));
        }

        /// <summary>
        /// X gate
        /// [ 0  1 ]
        /// [ 1  0 ]
        /// </summary>
        /// <returns></returns>
        public Qubit PauliX()
        {
            return Not();
        }

        /// <summary>
        /// Y gate
        /// [ 0  -i ]
        /// [ i   0 ]
        /// </summary>
        /// <returns></returns>
        public Qubit PauliY()
        {
            return new Qubit(complex.I * Alpha, -complex.I * Beta);
        }

        /// <summary>
        /// Z gate
        /// [ 1  0 ]
        /// [ 0 -1 ]
        /// </summary>
        /// <returns></returns>
        public Qubit PauliZ()
        {
            return new Qubit(Alpha, -Beta);
        }

        /// <summary>
        /// Phase shift gate
        /// [ ieΘ   0  ]
        /// [  0   ieΦ ]
        /// </summary>
        /// <param name="theta"></param>
        /// <param name="phi"></param>
        /// <returns></returns>
        public Qubit PhaseShift(double theta, double phi)
        {
            return new Qubit(complex.Exp(theta) * Alpha, complex.Exp(phi) * Beta);
        }

        /// <summary>
        /// Controlled not (CNOT) gate.
        /// [ 1 0 0 0 ]
        /// [ 0 1 0 0 ]
        /// [ 0 0 0 1 ]
        /// [ 0 0 1 0 ]
        /// 
        /// The CNOT gate works by combining the two input qubits (control and target) into a vector (of length 4, using tensor product).
        /// Then it uses the assumption that the output control qubit is constant to calculate the target output qubit.
        /// </summary>
        /// <returns>Target qubit affected by the CNOT gate. The target qubit os constant (and is therefore not returned).</returns>
        public static Qubit CNOT(Qubit control, Qubit target)   // TODO: introduce qubit pair? or Qubit vector?
        {
            // First get tensor product of the two qubits
            complex[] tensorProduct = TensorProduct(control, target);

            // Put result here
            complex[] result = new complex[4];

            // Multiply with CNOT gate matrix
            result[0] = tensorProduct[0];
            result[1] = tensorProduct[1];
            result[2] = tensorProduct[3];
            result[3] = tensorProduct[2];

            // Assumptions
            // 1) control output qubit α equals control qubit α
            // 2) control output qubit β equals control qubit β
            // =>
            // result[0] = α_control * α2 => α2 = result[0] / α_control 
            // result[1] = α_control * β2 => β2 = result[1] / α_control
            // => (or, if α_control is zero)
            // result[2] = β_control * α2 => α2 = result[2] / β_control
            // result[3] = β_control * β2 => β2 = result[3] / β_control
            if (control.Alpha != complex.Zero)
            {
                return new Qubit(result[0] / control.Alpha, result[1] / control.Alpha);
            }
            else
            {
                return new Qubit(result[2] / control.Beta, result[3] / control.Beta);
            }
        }

        /// <summary>
        /// Performs tensor product of two qubit components.
        /// See http://www.quantiki.org/wiki/Tensor_product.
        /// See http://www.cs.miami.edu/~burt/learning/Csc687.041/notes/qgates.html.
        ///                 [ α1 α2 ]
        /// [ α1 ] [ α2 ] = [ α1 β2 ]
        /// [ β1 ] [ β2 ] = [ β1 α2 ]
        ///                 [ β1 β2 ]
        /// </summary>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <returns>Returns an array of four items.</returns>
        private static complex[] TensorProduct(Qubit q1, Qubit q2)
        {
            return new complex[]
            {
                q1.Alpha * q2.Alpha,
                q1.Alpha * q2.Beta,
                q1.Beta * q2.Alpha,
                q1.Beta * q2.Beta
            };
        }

        public override string ToString()
        {
            var a = Alpha.ToString();
            var b = Beta.ToString();
            if (a.Contains('+') || a.Contains('-')) a = "(" + a + ")";
            if (b.Contains('+') || b.Contains('-')) b = "(" + b + ")";
            return a + "|0> + " + b + "|1>";
        }

        public override bool Equals(object obj)
        {
            Qubit q = (Qubit)obj;
            return q.Alpha.Equals(Alpha) && q.Beta.Equals(Beta);
        }

        public static bool operator ==(Qubit q1, Qubit q2)
        {
            return q1.Equals(q2);
        }

        public static bool operator !=(Qubit q1, Qubit q2)
        {
            return !q1.Equals(q2);
        }
    }
}
