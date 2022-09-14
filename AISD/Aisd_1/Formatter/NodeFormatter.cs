using System.Text;
using Aisd_1.Nodes;

namespace Aisd_1.Formatter;

public class NodeFormatter
{
    public string Format(NodeBase baseNode)
    {
        var sb = new StringBuilder();

        void FormatInternal(NodeBase node, int level)
        {
            sb.Append(' ', level * 2)
                .AppendLine(node.StringRepresentation);

            foreach (var child in node.Children)
            {
                FormatInternal(child, level + 1);
            }
        }
        
        FormatInternal(baseNode, 0);
        return sb.ToString();
    }
}