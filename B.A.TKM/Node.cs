using System.Linq;

namespace TKM_B_F_A
{
    class Node
    {
        public string[,][] connections;
        public int line = 1;
        public string[] name;

        public Node(string[] ne, string[] cs, string wt)
        {
            name = ne;
            Add(cs, wt);
        }

        public Node(string[] ne, string[] cs, string wt, int le)
        {
            name = ne;
            line = le;
            Add(cs, wt);
        }
        public Node(string[] ne, int le)
        {
            name = ne;
            line = le;
        }

        public void Add(string[] cn, string wt)
        {
            if (connections == null || connections.Length == 0)
            {
                connections = new string[1,2][];
                connections[0,0] = cn;
                connections[0, 1] = wt.ToCharArray().Select(c => c.ToString()).ToArray(); ;
            }
            else
            {
                if (!Search(cn))
                {
                    string[,][] tech = new string[connections.GetLength(0)+1, connections.GetLength(1)][];
                    for(int i = 0; i < connections.GetLength(0); i++)
                    {
                        for(int j = 0; j < connections.GetLength(1); j++)
                        {
                            tech[i, j] = connections[i, j];
                        }
                    }
                    //connections.CopyTo(tech, 0);

                    tech[tech.GetLength(0) - 1, 0] = cn;
                    tech[tech.GetLength(0) - 1, 1] = wt.ToCharArray().Select(c => c.ToString()).ToArray();
                    connections = tech;
                }
            }
        }

        public bool Search(string[] cn)
        {
            for(int i=0;i< connections.GetLength(0); i++)
            {
                if (Shannon.ParseToStr(connections[i,0]) == Shannon.ParseToStr(cn))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
