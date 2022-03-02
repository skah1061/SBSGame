using UnityEngine;

[System.Serializable]
public class Node : INode<Node>
{
    //�θ� ���
    public Node Parent;

    public int X; // { get; set; }
    public int Y;// { get; set; }
    public Vector3 Position;// { get; set; }
    public bool Walkable; // { get; set; }

    public int Gcost { get; set; } 
    public int Hcost { get; set; } 
    public int Fcost { get => Gcost + Hcost; }

    public int HeapIndex { get; set; }

    public Node(Vector3 position, int x, int y, bool walkable)
    {
        Position = position;
        X = x;
        Y = y;
        Walkable = walkable;
    }

    public int CompareTo(Node other)
    {
        //�� Fcost ���ڰ� ������ 1
        //������ 0
        //�� Fcost ���ڰ� ������ -1
        //���� ������ �켱������ ����
        int compare = Fcost.CompareTo(other.Fcost);

        //Fcost�� ���������
        if (compare == 0)
        {
            compare = Hcost.CompareTo(other.Hcost);
        }


        return compare;
    }
}