using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Aisd2;

class TreeNode<TKey, TValue>
{
    public TValue Value { get; set; }

    public TKey Key { get; }

    public int Hash { get; }
    
    public TreeNode<TKey, TValue>? Parent { get; set; }

    public TreeNode<TKey, TValue>? Left { get; set; }

    public TreeNode<TKey, TValue>? Right { get; set; }

    public bool IsRed { get; set; }

    public TreeNode(TKey key, TValue value)
    {
        Key = key;
        Hash = key.GetHashCode();
        Value = value;
    }
    
    public TreeNode<TKey, TValue>? Gp => Parent?.Parent;
    
    public TreeNode<TKey, TValue>? Uncle 
        => Gp == null 
        ? null
        : Parent!.IsLeft ? Gp?.Right : Gp?.Left;
    
    public TreeNode<TKey, TValue>? Sibling => Parent is null 
        ? null 
        : IsLeft ? Parent.Right : Parent.Left;
    
    public bool IsLeft => Parent?.Left == this;

    public bool HasRedChild => Left?.IsRed == true || Right?.IsRed == true;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("graph TD");


        void AppendNodeToOutput(TreeNode<TKey, TValue> parent, TreeNode<TKey, TValue>? left,
            TreeNode<TKey, TValue>? right)
        {
            if (left != null)
            {
                sb.Append($"{parent.Key} --> {left.Key}")
                    .AppendLine(left.IsRed ? ":::red" : "");
                AppendNodeToOutput(left, left.Left, left.Right);
            }

            if (right != null)
            {   
                sb.Append($"{parent.Key} --> {right.Key}")
                    .AppendLine(right.IsRed ? ":::red" : "");;
                AppendNodeToOutput(right, right.Left, right.Right);
            }
            
        }

        AppendNodeToOutput(this, Left, Right);
        sb.AppendLine("classDef red fill:#b30000;");
        return sb.ToString();
    }
}