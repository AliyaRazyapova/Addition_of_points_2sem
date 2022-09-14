using Aisd_1.Evaluator;
using Aisd_1.Formatter;
using Aisd_1.Nodes;
using Aisd_1.Parser;
using Aisd_1.Tokens;
using Aisd_1.Visitor;

const string InputFile = "data.txt";

NodeBase Parse(string input)
{
    var a = new Tokenizer().Tokenize(input);
    return new Parser(a, new ParserProvider()).Parse();
}

NodeBase GetFromFile()
{
    var input = File.ReadAllText(InputFile);
    return Parse(input);
}

void ReadAndPrint()
{
    Console.WriteLine("Выражение: {0}", GetFromFile());
}

void Evaluate()
{
    var evaluator = new Evaluator(new ConsoleDoubleProvider());
    Console.WriteLine("Результат {0}", evaluator.Evaluate(GetFromFile()));
}

void ReadAndSave()
{
    var text = new NodeFormatter().Format(GetFromFile());
    File.WriteAllText("output.txt", text);
}

void ShiftOperators()
{
    var newNodes = new SwapVisitor().Visit(GetFromFile());
    Console.WriteLine("Полученно выражение: {0}", newNodes);
    File.WriteAllText("output.txt", newNodes.ToString());
}

void ReplaceWithExpression()
{
    var newNodes = new ReplaceVarVisitor(
            new Tokenizer(),
            new Parser(new ParserProvider()),
            new ConsoleExpressionProvider())
        .Visit(GetFromFile());
    Console.WriteLine("Полученно выражение: {0}", newNodes);
}

Console.WriteLine("Выберите действие:");
Console.WriteLine("1. Построить дерево и обратно в строку");
Console.WriteLine("2. Вычислить");
Console.WriteLine("3. Сохранить в файл");
Console.WriteLine("4. Преобразовать(+ - *) и сохранить в файл");
Console.WriteLine("5. Вставить выражение вместо переменных");

var input = int.TryParse(Console.ReadLine(), out var option) ? option : throw new ArgumentException("Неверный ввод");

switch (input)
{
    case 1:
        ReadAndPrint();
        break;
    case 2:
        Evaluate();
        break;
    case 3:
        ReadAndSave();
        break;
    case 4:
        ShiftOperators();
        break;
    case 5:
        ReplaceWithExpression();
        break;
    default:
        throw new ArgumentException("Неверный ввод");
}