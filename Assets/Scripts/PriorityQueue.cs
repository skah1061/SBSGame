using System;
public interface INode<T> : IComparable<T>
{
    int HeapIndex { get; set; } // ���° �ڸ��� �ִ���
}

public class PriorityQueue<T> where T : INode<T>
{
    private readonly T[] nodes;
    private int nodeIndex;
    public int Count { get => nodeIndex - 1; }

    public PriorityQueue(int maxHeapSize)
    {
        nodeIndex = 1;
        nodes = new T[maxHeapSize + 1];
    }

    public void Push(T node)
    {
        node.HeapIndex = nodeIndex;
        nodes[nodeIndex] = node;
        SortUp(node);
        nodeIndex++;
    }

    private void SortUp(T node)
    {
        int parentIndex = 0;

        while ((parentIndex = GetParentIndex(node.HeapIndex)) != 0)
        {
            T parentNode = nodes[parentIndex];

            if (node.CompareTo(parentNode) < 0)
            {
                Swap(node, parentNode);
            }
            else 
            {
                break;
            }
        }
    }

    public T Pop()
    {
        T firstNode = nodes[1];
        nodeIndex--;
        nodes[1] = nodes[nodeIndex];
        nodes[1].HeapIndex = 1;

        SortDown(nodes[1]);

        return firstNode;
    }

    private void SortDown(T node)
    {
        int childIndex = 0;

        //�ڽ��� ������ ���� while��
        while ((childIndex = GetChildIndex(node.HeapIndex)) != 0)
        {
            //�ڽ��� �� ������� �켱������ ���� ����
            //�ڽ��� �ϳ� ������� ���� �ڽ� ���
            T childNode = nodes[childIndex];

            //���� �켱������ ���� ��� ������
            if (node.CompareTo(childNode) > 0)
            {
                //�ڸ� �ٲ�
                Swap(node, childNode);
            }
            else // ���� �켱������ ���ų� ������
            {
                //while�� ���� ����
                break;
            }
        }
    }

    private void Swap(T nodeA, T nodeB)
    {
        nodes[nodeA.HeapIndex] = nodeB;
        nodes[nodeB.HeapIndex] = nodeA;

        int tempIndex = nodeA.HeapIndex;
        nodeA.HeapIndex = nodeB.HeapIndex;
        nodeB.HeapIndex = tempIndex;
    }

    private int GetParentIndex(int selfIndex)
    {
        return selfIndex / 2;
    }

    private int GetChildIndex(int selfIndex)
    {
        if (GetLeftChildIndex(selfIndex) > Count)
        {
            return 0;
        }
        else if (GetLeftChildIndex(selfIndex) == Count)
        {
            return GetLeftChildIndex(selfIndex);
        }
        else
        {
            T leftChildNode = nodes[GetLeftChildIndex(selfIndex)];
            T rightChildNode = nodes[GetRightChildIndex(selfIndex)];

            if ((leftChildNode.CompareTo(rightChildNode)) < 0)
            {
                return GetLeftChildIndex(selfIndex);
            }
            else 
            {
                return GetRightChildIndex(selfIndex);
            }
        }
    }

    private int GetLeftChildIndex(int selfIndex)
    {
        return selfIndex * 2;
    }

    private int GetRightChildIndex(int selfIndex)
    {
        return selfIndex * 2 + 1;
    }

    //nodes�� �ֳ� ����
    public bool Contains(T node)
    {
        //nodes�� ���� node�� ��
        return Equals(nodes[node.HeapIndex], node);
    }
}
