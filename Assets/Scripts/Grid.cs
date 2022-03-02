using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public Vector2 gridWorldSize;
    public LayerMask block;

    [SerializeField] private Node[,] grid;
    public float nodeRadius;
    [SerializeField] private float nodeDiameter;
    [SerializeField] private int nodeXCount, nodeYCount;

    //����� �� ����
    public int GridCount { get => nodeXCount * nodeYCount; }

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2f;

        nodeXCount = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        nodeYCount = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        grid = new Node[nodeXCount, nodeYCount]; 

        Vector3 bottomLeft = transform.position - new Vector3(gridWorldSize.x * 0.5f, 0f, gridWorldSize.y * 0.5f);

        for (int x = 0; x < nodeXCount; x++)
        {
            for (int y = 0; y < nodeYCount; y++)
            {
                Vector3 worldPositon = bottomLeft + new Vector3((x * nodeDiameter + nodeRadius), 0f, (y * nodeDiameter + nodeRadius));

                bool walkable = !(Physics.CheckSphere(worldPositon, nodeRadius, block));

                grid[x, y] = new Node(worldPositon, x, y, walkable);
            }
        }
    }

    //���� ��� �������� ���ؼ�
    public List<Node> GetNeighbors(Node currentNode)
    {
        //��ȯ���� Node ���� ����
        List<Node> neighbors = new List<Node>();

        //-1~1
        for (int x = -1; x <= 1; x++)
        {
            //-1~1
            for (int y = -1; y <= 1 ; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                //���ִ� ������ Ÿ�ϸ��� ���� ����� �־��ָ� ū�ϳ���
                int checkX = currentNode.X + x;
                int checkY = currentNode.Y + y;

                //0~29 [0~29][0~29]
                if ((0 <= checkX && checkX < nodeXCount) && (0 <= checkY && checkY < nodeYCount))
                {
                    //�ش� checkX,checkY ��带 checkNode�� ĳ��
                    Node checkNode = grid[checkX, checkY];
                    //���࿡ �̵��� ������ �����
                    if (checkNode.Walkable)
                    {
                        //�̿��� �־���
                        neighbors.Add(checkNode);
                    }
                }
            }
        }

        //��ȯ
        return neighbors;
    }

    
    public Node GetNodeFromPosition(Vector3 pos)
    {
        float percentX = ((pos.x - nodeRadius) / gridWorldSize.x + 0.5f);
        float percentY = ((pos.z - nodeRadius) / gridWorldSize.y + 0.5f);

        int x = Mathf.RoundToInt((gridWorldSize.x) * percentX);
        int y = Mathf.RoundToInt((gridWorldSize.y) * percentY);

        x = Mathf.Clamp(x, 0, nodeXCount - 1);
        y = Mathf.Clamp(y, 0, nodeYCount - 1);

        return grid[x, y];
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0.5f, gridWorldSize.y));

        if (grid != null)
        {
            foreach(var node in grid)
            {
                Gizmos.color = node.Walkable ?  Color.white : Color.black;

                Gizmos.DrawCube(node.Position, new Vector3(0.9f, 0.1f, 0.9f));
            }
        }
    }


}
