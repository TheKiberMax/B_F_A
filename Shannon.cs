using System;

public class Shannon
{
	static public Shannon()
	{
        static private Node[] array;

    static public Block[] Data
    {
        get { return array; }
    }
    static public Block Last
    {
        get { return array.Last(); }
    }

    static public void Add(Block bk)
    {
        if (array == null || array.Length == 0)
        {
            array = new Block[1];
            array[0] = bk;
        }
        else
        {
            Block[] tech = new Block[array.Length];
            array.CopyTo(tech, 0);
            tech[tech.Length - 1] = bk;
            array = tech;
        }
    }

    static public void Erase()
    {
        array = null;
    }
}
}
