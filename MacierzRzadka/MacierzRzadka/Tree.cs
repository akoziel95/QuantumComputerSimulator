using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacierzRzadka
{
    public class Tree
    {
        public Node root;
        public int treeDepth;
        public Tree()
        {
            //root = new Node(0, v);
            root = null;
            treeDepth = 0;
        }

        public Node Insert(Node elem, int i, double val, int dep)
        {
            if (elem == null)
            {
                elem = new Node(i, val);
                elem.level = dep;
            }
            else if (elem.index < i)
                elem.right = Insert(elem.right, i, val, ++dep);
            else if (elem.index > i)
                elem.left = Insert(elem.left, i, val, ++dep);
            else 
            {
                elem.value = val;
                return elem;
            }
            
            if (dep > this.treeDepth)
                treeDepth = dep;                       
            return elem;
        }

        public Node Get(Node elem, int i)
        {
            if (elem != null)
            {
                if (elem.index < i)
                    elem = Get(elem.right, i);
                else if (elem.index > i)
                    elem = Get(elem.left, i);
            }            
            return elem;
        }


        public void Print(Node el)  // Left Visit Right, drukowanie infiksowe(in-order)
        {
            if (el != null)
            {
                Print(el.left);
                Console.WriteLine(el.value+"\t"+el.index);
                Print(el.right);
            }
        }
        public void Collect(Node el, int l, ref StringBuilder ss)//zbiera liscie w kolejności od lewej poziomu "l" drzewa
        {
            if (el != null)
            {
                Collect(el.left, l, ref ss);
                Collect(el.right, l, ref ss);
                if (el.level == l)
                {
                    //for (int i = 0; i < treeDepth/(l+1); i++)
                    //{
                    //    ss.Append(" ");
                    //}
                    ss.Append(el.index.ToString());

                    //for (int i = 0; i < treeDepth; i++)
                    //{
                    //    ss.Append(" ");
                    //}
                }
                //else
                //{
                //      for (int i = 0; i <this.treeDepth* (l + 3) / (l + 5); i++) 
                //    //for (int i = 0; i < this.treeDepth / (l + 2); i++)
                //        ss.Append(" ");
                //}
            }
            else ss.Append(" ");
           
        }

        public void StructuredPrint()//wyświetlanie "w kształcie drzewa"
        {
            for (int i = 0; i <= this.treeDepth; i++)
            {
                StringBuilder ss = new StringBuilder();
                //Console.WriteLine(Collect(root, i));
                Collect(root, i, ref ss);
                Console.WriteLine(i+" "+ss.ToString());
            }
        }
        
    }
}
