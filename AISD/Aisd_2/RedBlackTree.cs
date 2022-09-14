namespace Aisd2;

public class RedBlackTree<TKey, TValue>
{
    internal TreeNode<TKey, TValue> _root;

    public void Insert(TKey key, TValue value)
    {
        var node = new TreeNode<TKey, TValue>(key, value)
        {
            Value = value,
            IsRed = true,
        };

        TreeNode<TKey, TValue> y = default!;
        var x = _root;

        while (x != null)
        {
            y = x;
            if (node.Hash < x.Hash)
                x = x.Left;
            else
                x = x.Right;
        }

        node.Parent = y;
        if (y == null)
            _root = node;
        else if (node.Hash < y.Hash)
            y.Left = node;
        else
            y.Right = node;

        if (node.Parent == null)
        {
            node.IsRed = false;
            return;
        }

        if (node.Parent.Parent is null)
            return;

        FixInsert(node);
    }

    public bool Search(TKey key, out TValue? value)
    {
        var res = SearchInternal(_root, key);
        value = res != null ? res.Value : default;
        return res != null;
    }

    /// <summary>
    /// Заменяет поддерво U поддеревом V
    /// </summary>
    private void RbTransplant(TreeNode<TKey, TValue> u, TreeNode<TKey, TValue>? v)
    {
        if (u.Parent == null)
            _root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;

        if (v != null) v.Parent = u.Parent;
    }

    public void Delete(TKey key)
    {
        var u = SearchInternal(_root, key);
        if (u == null) throw new ArgumentException("Ключ не существует", nameof(key));
        DeleteInternal(u);
    }

    // Находим ноду которая заменит при удалении
    private TreeNode<TKey, TValue>? GetReplacementNode(TreeNode<TKey, TValue> node)
    {
        if(node.Left == null && node.Right == null)
            return null;
        if (node.Left == null)
            return node.Right;
        if (node.Right == null)
            return node.Left;

        return GetNodeMinHash(node.Right);
    }

    private void ReplaceNode(TreeNode<TKey, TValue> a, TreeNode<TKey, TValue> b)
    {
        b.Parent = a.Parent;
        b.Left = a.Left;
        b.Right = a.Right;

        if (a.Parent != null)
            if (a.IsLeft)
                a.Parent.Left = b;
            else
                a.Parent.Right = b;
        else
            _root = b;

        if (a.Left != null)
            a.Left.Parent = b;
        if (a.Right != null)
            a.Right.Parent = b;
    }
    // Меняет данные, но не меняет цвета
    internal void SwapNodesData(ref TreeNode<TKey, TValue> a, ref TreeNode<TKey, TValue> b)
    {
        var rA = new TreeNode<TKey, TValue>(b.Key, b.Value)
        {
            IsRed = a.IsRed,
        };
        
        var rB = new TreeNode<TKey, TValue>(a.Key, a.Value)
        {
            IsRed = b.IsRed,
        };
        ReplaceNode(a, rA);
        ReplaceNode(b, rB);
        a = rA;
        b = rB;
    }
    
    private void DeleteInternal(TreeNode<TKey, TValue> v)
    {
        if (v == null)
            return;

        var u = GetReplacementNode(v);

        var bb = (u is null or {IsRed: false}) && (!v.IsRed);

        // Удаляемый узел не имеет детей(лист)
        if (u == null)
        {
            // Удаляемый узел - корень
            if (u == _root)
                _root = null;
            else
                if(bb)
                    FixDelete(v);
                else if (v.Sibling != null)
                    v.Sibling.IsRed = true;
            
            if (v is { Parent: { }, IsLeft: true })
                v.Parent.Left = null;
            else if(v.Parent != null)
                v.Parent.Right = null;
            return;
        }

        if (v.Left == null || v.Right == null)
        {
            if (v == _root)
                ReplaceNode(v, new(u.Key, u.Value){Parent = v.Parent});
            else
            {
                if (v.IsLeft)
                    v.Parent.Left = u;
                else
                    v.Parent.Right = u;
                u.Parent = v.Parent;
                if(bb)
                    FixDelete(u);
                else
                    u.IsRed = false;
            }
            return;
        }
        
        SwapNodesData(ref u, ref v);
        DeleteInternal(u);
    }

    private void FixDelete(TreeNode<TKey, TValue>? x)
    {
        if(x is null)
            return;
        var sibling = x.Sibling;
        var parent = x.Parent;

        if (sibling == null)
            FixDelete(parent);
        else
        {
            if (sibling.IsRed)
            {
                parent.IsRed = true;
                sibling.IsRed = false;
                if(sibling.IsLeft)
                    RightRotate(parent);
                else
                    LeftRotate(parent);
                FixDelete(x);
            }
            else
            {
                if (sibling.HasRedChild)
                {
                    if (sibling.Left != null && sibling.Left.IsRed)
                    {
                        if (sibling.IsLeft)
                        {
                            sibling.Left.IsRed = sibling.IsRed;
                            sibling.IsRed = parent.IsRed;
                            RightRotate(parent);
                        }
                        else
                        {
                            sibling.Left.IsRed = parent.IsRed;
                            RightRotate(sibling);
                            LeftRotate(parent);
                        }
                    }
                    else
                    {
                        if (sibling.IsLeft)
                        {
                            sibling.Right.IsRed = parent.IsRed;
                            LeftRotate(sibling);
                            RightRotate(parent);
                        }
                        else
                        {
                            sibling.Right.IsRed = sibling.IsRed;
                            sibling.IsRed = parent.IsRed;
                            LeftRotate(parent);
                        }
                    }
                    parent.IsRed = false;
                }
                else
                {
                    sibling.IsRed = true;
                    if (!parent.IsRed)
                        FixDelete(parent);
                    else
                        parent.IsRed = false;
                }
            }
        }
    }

    internal TreeNode<TKey, TValue>? SearchInternal(TreeNode<TKey, TValue>? node, TKey key)
    {
        var hash = key.GetHashCode();
        while (node != null)
        {
            if (node.Hash == hash)
                return node;

            if (node.Hash <= hash)
                node = node.Right;
            else
                node = node.Left;
        }

        return node;
    }

    /// <summary>
    /// Находит узел с минимальным ключом в поддереве
    /// </summary>
    private TreeNode<TKey, TValue> GetNodeMinHash(TreeNode<TKey, TValue> node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    private void FixInsert(TreeNode<TKey, TValue> k)
    {
        while (k.Parent.IsRed)
        {
            // Находимся в правом поддереве
            if (k.Parent == k.Parent.Parent.Right)
            {
                var u = k.Parent.Parent.Left;
                if (u.IsRed)
                {
                    u.IsRed = false;
                    k.Parent.IsRed = false;
                    k.Parent.Parent.IsRed = true;
                    k = k.Parent.Parent;
                }
                else
                {
                    if (k == k.Parent.Left)
                    {
                        k = k.Parent;
                        RightRotate(k);
                    }

                    k.Parent.IsRed = false;
                    k.Parent.Parent.IsRed = true;
                    LeftRotate(k.Parent.Parent);
                }
            }
            else
            {
                var u = k.Parent.Parent.Right;
                if (u.IsRed)
                {
                    u.IsRed = false;
                    k.Parent.IsRed = false;
                    k.Parent.Parent.IsRed = true;
                    k = k.Parent.Parent;
                }
                else
                {
                    if (k == k.Parent.Right)
                    {
                        k = k.Parent;
                        LeftRotate(k);
                    }

                    k.Parent.IsRed = false;
                    k.Parent.Parent.IsRed = true;
                    RightRotate(k.Parent.Parent);
                }
            }

            if (k == _root)
                break;
        }

        _root.IsRed = false;
    }

    private void LeftRotate(TreeNode<TKey, TValue> a)
    {
        var b = a.Right;
        a.Right = b.Left;

        if (b.Left != null)
            b.Left.Parent = a;

        b.Parent = a.Parent;

        if (a.Parent == null)
            _root = b;
        else if (a == a.Parent.Left)
            a.Parent.Left = b;
        else
            a.Parent.Right = b;

        b.Left = a;
        a.Parent = b;
    }

    private void RightRotate(TreeNode<TKey, TValue> x)
    {
        var y = x.Left;
        x.Left = y.Right;

        if (y.Right != null)
            y.Right.Parent = x;

        y.Parent = x.Parent;

        if (x.Parent == null)
            _root = y;
        else if (x == x.Parent.Right)
            x.Parent.Right = y;
        else
            x.Parent.Left = y;

        y.Right = x;
        x.Parent = y;
    }

    public override string ToString()
        => _root.ToString();
}