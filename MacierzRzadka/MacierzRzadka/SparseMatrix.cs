using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacierzRzadka
{
    public class SparseMatrix
    {
        public int rows;
        public int columns;
        public Vector []wektor;
        public SparseMatrix (int numberOfRows, int numberOfColumns)
        {
            rows = numberOfRows;
            columns = numberOfColumns;
            wektor = new Vector[rows];
            for (int i = 0; i < wektor.Length; i++)
            {
                wektor[i] = new Vector();
            }
        }
        public SparseMatrix(string filename)
        {
            string []lines=File.ReadAllLines(filename);

            string []line = lines[0].Split(new char[] { ' ' });
            //Console.WriteLine(lines.Length);
            if (!Int32.TryParse(line[0], out rows))
            {
                Console.WriteLine("Error while reading number of rows");
                return;
            }
            if (!Int32.TryParse(line[1], out columns))
            {
                Console.WriteLine("Error while reading number of columns");
                return;
            }
                

            wektor = new Vector[rows];
            int trow, tcol;
            double tvalue;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(new char[] { ' ' });
                
                if (data.Length!=3||!(Int32.TryParse(data[0], out trow) && Int32.TryParse(data[1], out tcol) && Double.TryParse(data[2], out tvalue)))
                {
                    Console.WriteLine("Błąd odczytu danych!");
                    return;
                }
                CheckRange(trow, tcol);

                if (wektor[trow] == null)
                    wektor[trow] = new Vector();
                wektor[trow].Insert(tcol, tvalue);
            }

            
        }
        public void Insert(int r, int c, double value)
        {
            CheckRange(r, c);
            wektor[r].Insert(c, value);
        }
        public double Get(int row, int col)
        {
            CheckRange(row, col);
            return wektor[row].GetAt(col);
        }
        public void CheckRange(int r, int c)
        {
            if (r > rows || c > columns)
                throw new ElementPozaZakresem("Element odwołuje się do komórki o indeksie nieistniejącym w macierzy");
            return;
        }

        public static SparseMatrix operator +(SparseMatrix m1, SparseMatrix m2)
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns) 
                throw new ZlyRozmiarException("Zły rozmiar macierzy, dodawanie niemożliwe");
            SparseMatrix c = new SparseMatrix(m1.rows, m1.columns);
            int[] avaibleIndexes;
            for (int i = 0; i < m1.rows; i++)
            {
                c.wektor[i] = new Vector();
                avaibleIndexes=m1.wektor[i].avaibleIndex;//wektor niepustych indeksów  rzędu i-tego macierzy m1
                for (int j = 0; j < m1.wektor[i].numberOfElements; j++)
                {
                    c.wektor[i].Insert(avaibleIndexes[j], m1.wektor[i].GetAt(avaibleIndexes[j]) + m2.wektor[i].GetAt(avaibleIndexes[j]));
                    //w odpowiednią komórkę macierzy c wstawiam sumę tych samych komórek m1 i m2
                }
                avaibleIndexes=m2.wektor[i].avaibleIndex;//wektor niepustych indeksów  rzędu i-tego macierzy m2
                for (int j = 0; j < m2.wektor[i].numberOfElements; j++)
                {
                    if (c.wektor[i].GetAt(avaibleIndexes[j]) != 0) // sprawdzam czy w poprzedniej pętli nie było już wykonywane działanie
                        continue;
                    else
                        c.wektor[i].Insert(avaibleIndexes[j], m1.wektor[i].GetAt(avaibleIndexes[j]) + m2.wektor[i].GetAt(avaibleIndexes[j]));
                }

            }
            return c;
        }
        public static SparseMatrix operator -(SparseMatrix m1, SparseMatrix m2)
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
                throw new ZlyRozmiarException("Zły rozmiar macierzy, odejmowanie niemożliwe");
            SparseMatrix c = new SparseMatrix(m1.rows, m1.columns);
            int[] avaibleIndexes;
            for (int i = 0; i < m1.rows; i++)
            {
                c.wektor[i] = new Vector();
                avaibleIndexes = m1.wektor[i].avaibleIndex;//wektor niepustych indeksów  rzędu i-tego macierzy m1
                for (int j = 0; j < m1.wektor[i].numberOfElements; j++)
                {
                    c.wektor[i].Insert(avaibleIndexes[j], m1.wektor[i].GetAt(avaibleIndexes[j]) - m2.wektor[i].GetAt(avaibleIndexes[j]));
                    //w odpowiednią komórkę macierzy c wstawiam różnicę tych samych komórek m1 i m2
                }
                avaibleIndexes = m2.wektor[i].avaibleIndex;//wektor niepustych indeksów  rzędu i-tego macierzy m2
                for (int j = 0; j < m2.wektor[i].numberOfElements; j++)
                {
                    if (c.wektor[i].GetAt(avaibleIndexes[j]) != 0) // sprawdzam czy w poprzedniej pętli nie było już wykonywane działanie
                        continue;
                    else
                        c.wektor[i].Insert(avaibleIndexes[j], m1.wektor[i].GetAt(avaibleIndexes[j]) - m2.wektor[i].GetAt(avaibleIndexes[j]));
                }

            }
            return c;
        }
        public static SparseMatrix operator *(SparseMatrix m1, SparseMatrix m2)
        {
            if (m1.columns != m2.rows)
                throw new ZlyRozmiarException("Zły rozmiar macierzy, mnożenie niemożliwe");
            SparseMatrix c = new SparseMatrix(m1.rows, m2.columns);
            int []avaibleIndexes;
            double temp;
            for (int i = 0; i < m1.rows; i++) //pętla po rzędach
            {
                c.wektor[i] = new Vector();
                avaibleIndexes=m1.wektor[i].avaibleIndex;
                for (int k = 0; k < m2.columns; k++)//pętla po kolumnach
                {


                    for (int j = 0; j < m1.wektor[i].numberOfElements; j++) //pętla mnożąca m1[i][j] przez m2[j][k]
                    {

                        temp = m1.wektor[i].GetAt(avaibleIndexes[j]) * m2.wektor[avaibleIndexes[j]].GetAt(k);
                        //if (i == 2)
                        //{
                        //    Console.WriteLine("trzecia linia" + temp);
                        //}
                        c.wektor[i].Insert(k, c.wektor[i].GetAt(k) + temp);
                    }
                }
            }

            return c;
        }

	public void PrintMatrix()
        {
            
            if (this == null)
            {
                Console.WriteLine("Nie mogę wydrukować pustej macierzy");
                return;
            }
            double temp;
        for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    temp = wektor[i].GetAt(j);
                    if (temp == 0.00)
                        Console.Write("---\t");
                    else Console.Write(temp.ToString("F") + "\t");
                }
                Console.WriteLine("");
            }
        }
    private void Decompose(out SparseMatrix L)
    {
        SparseMatrix A = this;
        if (A.rows != A.columns)
        {
            Console.WriteLine("Macierz nie jest kwadratowa, niemożliwa dekompozycja");
        }

        L = new SparseMatrix(A.rows, A.columns);//dolnotrójkątna z diagonalą niezerową
        //U = new SparseMatrix(A.rows, A.columns);//górnotrójkątna z diagonalą zerową
    
        double temp;
        for (int i = 0; i < L.rows; i++)//uzupełniam diagonalę L jedynkami
        {
            L.Insert(i, i, 1);
        }
       

        //Gauss
        double k;
        for (int m = 0; m < A.rows; m++)
        {

            
            for (int i = m + 1; i < A.rows; i++)//redukcja i-tych wierszy za pomocą m-tego wiersza
            {
                k = A.Get(i, m) / A.Get(m, m);
                    for (int j = 0; j < A.columns; j++)//redukcja dwóch konkretnych wierszy
                {
                    temp = A.Get(i, j) - k * A.Get(m, j);
                    A.Insert(i, j, temp);
                }
                L.Insert(i, m, k);
                
                //odejmij wiersze
                //zapisz współczynnik
            }
            
        }
    }
    private void WybierzElemGlowny(ref SparseMatrix y)
    {
        for (int i = 0; i < rows; i++)
        {
            if(Get(i, i)==0)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (Get(j, i) != 0 && Get(j, i) != 0)
                    {
                        Vector temp = this.wektor[i];
                        this.wektor[i] = wektor[j];
                        wektor[j] = temp;

                        temp = y.wektor[i];
                        y.wektor[i] = y.wektor[j];
                        y.wektor[j] = temp;

                    }
                }
            }
        }
    }
           
        public SparseMatrix ObliczUklad(SparseMatrix y)
        {
            SparseMatrix L;
            WybierzElemGlowny( ref y);
            Decompose(out L);
            
            //czas obliczyć wektor Z
           SparseMatrix Z= new SparseMatrix(rows, 1);
           double temp = y.Get(0, 0);
           Z.Insert(0, 0, temp); //pierwszy element zawsze równy temu z wektora y

            for (int i = 1; i < L.rows; i++)//licze elementy wektora z
            {
                temp = y.Get(i, 0);
                for (int j = 0 ; j < i+1; j++)//licze wartosc i-tego elementu wektora z
                {
                    temp -= Z.Get(j ,0) * L.Get(i, j);
                }
                Z.Insert(i, 0, temp);
            }
            //obliczam ostatni wektor X
            SparseMatrix X = new SparseMatrix(rows, 1);
            temp = Z.Get(rows-1, 0);
            X.Insert(rows-1, 0 , temp/this.Get(rows-1, columns-1));
            for (int i = this.rows-2; i >= 0; i--)//licze elementy od dołu "this"
            {
                temp = Z.Get(i, 0);
                for (int j = columns-1 ; j >= i  ; j--)
                {
                    temp -= X.Get(j, 0) * this.Get(i, j);
                }
                    temp/=this.Get(i,i);
                    X.Insert(i, 0, temp);

            }
            this.DoPliku("U.txt");
            L.DoPliku("L.txt");
            X.DoPliku("X.txt");
            Console.WriteLine("Macierz U:");
            PrintMatrix();
            Console.WriteLine("\nMacierz L:");
            L.PrintMatrix();
            return X;
        }
        
        public void DoPliku(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                
                sw.WriteLine(rows + " " + columns);
                for (int i = 0; i < rows; i++)
                {
                    int[] indexes = wektor[i].avaibleIndex;
                    for (int j = 0; j < wektor[i].numberOfElements; j++)
                    {
                        sw.WriteLine(i +" "+ indexes[j]+" "+ Get(i, indexes[j]));
                    }
                }

            }
        }    
    }
    
    public class ElementPozaZakresem : Exception
    { 
        public ElementPozaZakresem()
    {
    }

    public ElementPozaZakresem(string message)
        : base(message)
    {
    }

    }
    public class ZlyRozmiarException : Exception
    {
        public ZlyRozmiarException()
        {
        }

        public ZlyRozmiarException(string message)
            : base(message)
        {
        }

    }
}
