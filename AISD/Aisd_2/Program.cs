using Aisd2;

var bst = new RedBlackTree<int, int>();

bst.Insert(55, 55);
bst.Insert(40, 40);
bst.Insert(65, 65);
bst.Insert(60, 60);
bst.Insert(75, 75);
bst.Insert(57, 57);

Console.WriteLine(bst);
bst.Delete(40);
Console.WriteLine(bst);