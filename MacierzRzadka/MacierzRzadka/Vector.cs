using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacierzRzadka
{
    public class Vector
    {
        public Tree t;
        public int[] avaibleIndex;
        private int numberofEl;
        public int numberOfElements { get { return numberofEl; } }
        public Vector()
        {
            t = new Tree();
            avaibleIndex = new int[32];
            numberofEl = 0;
        }
        public void Insert(int index, double value)
        {
            if (!this.avaibleIndex.Contains(index)||numberofEl==0)
            {
                if (avaibleIndex.Length == this.numberofEl)
                    Array.Resize(ref avaibleIndex, avaibleIndex.Length * 2);
                avaibleIndex[numberofEl] = index;
                numberofEl++;
            }
            t.root=t.Insert(t.root, index, value, 0);
        }

        public double GetAt(int index)
        {
            Node temp = t.Get(t.root, index);
            if (temp != null)
                return t.Get(t.root, index).value;
            return 0;
        }
        public void Print()
        {
            Console.WriteLine("Value\tIndex");
            t.Print(t.root);
        }
    }
}
