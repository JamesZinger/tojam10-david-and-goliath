
public class Edge
{
	public Node Node1;
	public Node Node2;
	public bool HasConnectedPath;

	public Edge( Node node1, Node node2, bool hasConectedPath )
	{
		Node1 = node1;
		Node2 = node2;
		HasConnectedPath = hasConectedPath;
	}
}
