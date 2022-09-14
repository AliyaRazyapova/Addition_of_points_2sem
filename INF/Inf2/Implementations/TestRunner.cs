using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Inf2.Abstractions;
using Inf2.Model;
using Inf2.Utils;

namespace Inf2.Implementations;

/// <inheritdoc />
public class TestRunner : ITestRunner
{
    private readonly Config _config;

    public TestRunner(Config config)
    {
        _config = config;
    }

    /// <inheritdoc />
    public StudentAssignmentResult Check(StudentAssignment studentAssignment)
    {
        var (student, test) = studentAssignment;
        var results = test.Select(x => PerformTest(student, x)).ToArray();
        return new StudentAssignmentResult(student, results);
    }

    private TestResult PerformTest(Student student, Test test)
    {
        var (status, assembly) = FetchAssemblyForTest(student, test);
        if (status != AssemblyFetchStatus.Ok || assembly == null)
            return test.FailAllWithResult(
                status switch
                {
                    AssemblyFetchStatus.NotFound => TestCaseResults.AssemblyNotFound,
                    _ => TestCaseResults.UnhandledException,
                });

        var testCases = test.TestCases;

        if (!FindClassAndMethod(assembly, test.TestDescription, out var type, out var method))
            return test.FailAllWithResult(TestCaseResults.MismatchedName, "Не найден метод или класс");

        return new TestResult(
            test,
            testCases.Select(x => PerformTestCase(type, method, x)).ToArray());
    }

    private TestCaseResult PerformTestCase(Type targetClass, MethodInfo method, TestCase testCase)
    {
        try
        {
            var instance = targetClass.GetConstructor(Type.EmptyTypes)?.Invoke(Array.Empty<Object>());
            if (instance == null)
                return testCase.WithResult(TestCaseResults.MismatchedName, message: "Не найден конструктор");

            var result = method.InvokeWithDictionary(instance, testCase.Parameters);

            return testCase.Assert(result);
        }
        catch (Exception e)
        {
            return testCase.WithResult(TestCaseResults.UnhandledException, ex: e);
        }
    }

    private (AssemblyFetchStatus, Assembly?) FetchAssemblyForTest(Student student, Test testCase)
    {
        Assembly assignmentAssembly;
        try
        {
            assignmentAssembly = Assembly.LoadFile(
                Path.Join(
                    _config.StudentsFolder,
                    student.Name,
                    testCase.TestDescription.FileName + ".dll"));
        }
        catch (Exception e)
        {
            return (e switch
            {
                FileNotFoundException => AssemblyFetchStatus.NotFound,
                _ => AssemblyFetchStatus.Exception,
            }, null);
        }

        return (AssemblyFetchStatus.Ok, assignmentAssembly);
    }

    private bool FindClassAndMethod(
        Assembly assembly,
        TestDescription description,
        out Type? type,
        out MethodInfo? method)
    {
        type = null;
        method = null;

        var types = assembly.GetTypes().Where(t => t.Name == description.ClassName).ToArray();

        if (types.Count() is > 1 or < 0)
            return false;

        type = types.FirstOrDefault();

        if (type is null || type.IsAbstract || !type.IsClass)
            return false;

        method = type.GetMethods().FirstOrDefault(
            m =>
                m.Name == description.MethodName
                && IsValidMethod(m, description));
        return method != null;
    }

    private bool IsValidMethod(MethodInfo info, TestDescription description)
    {
        if (info.IsAbstract)
            return false;
        if (info.ReturnType != description.ReturnType)
            return false;

        return info.GetParameters()
            .All(
                x =>
                    description.Arguments.TryGetValue(x.Name, out var type)
                    && x.ParameterType == type);
    }
}