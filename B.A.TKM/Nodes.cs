using System.Linq;

namespace TKM_B_F_A
{
    class Nodes
    {
        static private Node[] nodes;
        public Node[] Data
        {
            get { return nodes; }
        }
        public Nodes(string[] func)
        {
            string[] str = new string[1];
            str[0] = "1";
            Node ne = new Node(str);
            Add(ne);
            ne = new Node(func);
            Add(ne);
        }

        public void Add(Node ne)
        {
            if (nodes == null || nodes.Length == 0)
            {
                nodes = new Node[1];
                nodes[0] = ne;
            }
            else
            {
                Node[] tech = new Node[nodes.Length + 1];
                nodes.CopyTo(tech, 0);
                tech[tech.Length - 1] = ne;
                nodes = tech;
            }
        }

        static public void Erase()
        {
            nodes = null;
        }

        public bool Search(string[] cn)
        {
            if (nodes == null || nodes.Length == 0)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (Shannon.ParseToStr(nodes[i].name) == Shannon.ParseToStr(cn))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int SearchIndex(string[] cn)
        {
            if (!(nodes == null || nodes.Length == 0))
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (Shannon.ParseToStr(nodes[i].name) == Shannon.ParseToStr(cn))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}