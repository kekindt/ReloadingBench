namespace EtcdProvider
{
    public interface IEtcdProvider
    { 
        Node GetNode(string key, bool isDir = false);
        Node UpdateNode(string key, string value);
        Node UpdateNode(Node node);
    }
}