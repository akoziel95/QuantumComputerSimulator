using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacierzRzadka
{
    class Program
    {
        static void Main(string[] args)
        {
            #region testDlaDziałaniaNaDwóchMacierzach
            //if (args.Length != 3)
            //{
            //    Console.WriteLine("Błędne parametry wejściowe");
            //    return;
            //}
            //SparseMatrix A = new SparseMatrix(args[0]);
            //SparseMatrix B = new SparseMatrix(args[2]);
            //char action = args[1][0];
            //try
            //{
            //    SparseMatrix C;
            //    switch (action)
            //    {
            //        case '*':
            //            C = A * B;
            //            break;
            //        case '+':
            //            C = A + B;
            //            break;
            //        case '-':
            //            C = A - B;
            //            break;
            //        default:
            //            Console.WriteLine("Błędna operacja arytmetyczna");
            //            C = null;
            //            break;
            //    }
            //    Console.WriteLine("Oto wynikowa macierz:"); 
            #endregion

            try{
               SparseMatrix mat=new SparseMatrix("AX.txt");
               SparseMatrix Y = new SparseMatrix("YX.txt");
                SparseMatrix X;
                //Console.WriteLine("Macierz D:");
                //mat.PrintMatrix();
                //Console.WriteLine("\nWektor Y1");
                //Y.PrintMatrix();
                X = mat.ObliczUklad(Y);
               // Console.WriteLine("Wektor wynikowy X:");
                X.PrintMatrix();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);
            }
            
        }
    }
}
