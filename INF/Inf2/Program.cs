using System.Text;
using Inf2;
using Inf2.Implementations;

Console.OutputEncoding = Encoding.UTF8;

var config = new Config();
var paramTypeRegistry = ParameterTypeRegistryFactory.CreateDefault();

var testRegistry = new TestRegistry(config, new TestParser(paramTypeRegistry));
testRegistry.LoadTests();

var studentAssignmentRegistry = new StudentAssignmentRegistry(config, testRegistry);
studentAssignmentRegistry.LoadAssignments();

var testRunner = new TestRunner(config);
var testWorker = new ParallelTestWorker(config, testRunner);

var computedResult = testWorker.Compute(studentAssignmentRegistry.Assignments.ToArray());
var serializer = new ResultFormatter();
var saver = new FileResultSaver(config, serializer);

Console.WriteLine(
    string.Join(
        "\n",
        computedResult.Select(x => serializer.SerializeResults(x))));

saver.SaveAll(computedResult);

