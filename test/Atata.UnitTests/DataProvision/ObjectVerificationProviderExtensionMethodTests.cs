﻿using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Atata.UnitTests.DataProvision;

public static class ObjectVerificationProviderExtensionMethodTests
{
    public class Satisfy_Expression : ExtensionMethodTestFixture<string, Satisfy_Expression>
    {
        static Satisfy_Expression() =>
            For("abc123")
                .ThrowsArgumentNullException(should => should.Satisfy(null))
                .Pass(should => should.Satisfy(x => x.Contains("abc") && x.Contains("123")))
                .Fail(should => should.Satisfy(x => x == "xyz"));
    }

    public class Satisfy_Function : ExtensionMethodTestFixture<int, Satisfy_Function>
    {
        static Satisfy_Function() =>
            For(5)
                .ThrowsArgumentNullException(should => should.Satisfy(null, "..."))
                .Pass(should => should.Satisfy(x => x is > 1 and < 10, "..."))
                .Fail(should => should.Satisfy(x => x == 7, "..."));
    }

    public class Satisfy_IEnumerable_Expression : ExtensionMethodTestFixture<Subject<string>[], Satisfy_IEnumerable_Expression>
    {
        static Satisfy_IEnumerable_Expression() =>
            For(new[] { "a".ToSubject(), "b".ToSubject(), "c".ToSubject() })
                .ThrowsArgumentNullException(should => should.Satisfy(null as Expression<Func<IEnumerable<string>, bool>>))
                .Pass(should => should.Satisfy(x => x.Contains("a") && x.Contains("c")))
                .Fail(should => should.Satisfy((IEnumerable<string> x) => x.Any(y => y.Contains('z'))));
    }

    public class StartWith_string : ExtensionMethodTestFixture<string, StartWith_string>
    {
        static StartWith_string() =>
            For("abcdef")
                .ThrowsArgumentNullException(should => should.StartWith(null))
                .ThrowsArgumentException(should => should.StartWith())
                .Pass(should => should.StartWith("a"))
                .Pass(should => should.StartWith("abc"))
                .Fail(should => should.StartWith("zbc"))
                .Fail(should => should.StartWith("abcdefg"));
    }

    public class StartWith_string_IgnoringCase : ExtensionMethodTestFixture<string, StartWith_string_IgnoringCase>
    {
        static StartWith_string_IgnoringCase() =>
            For("aBcDeF")
                .When(should => should.IgnoringCase)
                .Pass(should => should.StartWith("abc"))
                .Pass(should => should.StartWith("ABcdEF"));
    }

    public class Match : ExtensionMethodTestFixture<string, Match>
    {
        static Match() =>
            For("abcdef")
                .ThrowsArgumentNullException(should => should.Match(null))
                .Pass(should => should.Match("bcd"))
                .Pass(should => should.Match("^abc"))
                .Pass(should => should.Match("^abcdeF$", RegexOptions.IgnoreCase))
                .Fail(should => should.Match("^abcdeF$"));
    }

    public class BeEquivalent : ExtensionMethodTestFixture<int[], BeEquivalent>
    {
        static BeEquivalent() =>
            For(new[] { 1, 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.BeEquivalent(null))
                .Pass(should => should.BeEquivalent(1, 1, 2, 3, 5))
                .Pass(should => should.BeEquivalent(5, 1, 2, 3, 1))
                .Fail(should => should.BeEquivalent())
                .Fail(should => should.BeEquivalent(1, 2, 3, 4, 5))
                .Fail(should => should.BeEquivalent(1, 1, 2, 3));
    }

    public class BeEquivalent_WhenEmpty : ExtensionMethodTestFixture<int[], BeEquivalent_WhenEmpty>
    {
        static BeEquivalent_WhenEmpty() =>
            For(new int[0])
                .Pass(should => should.BeEquivalent())
                .Fail(should => should.BeEquivalent(1));
    }

    public class EqualSequence : ExtensionMethodTestFixture<int[], EqualSequence>
    {
        static EqualSequence() =>
            For(new[] { 1, 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.EqualSequence(null))
                .Pass(should => should.EqualSequence(1, 1, 2, 3, 5))
                .Fail(should => should.EqualSequence(5, 1, 2, 3, 1))
                .Fail(should => should.EqualSequence())
                .Fail(should => should.EqualSequence(1, 2, 3, 4, 5))
                .Fail(should => should.EqualSequence(1, 1, 2, 3));
    }

    public class EqualSequence_WhenEmpty : ExtensionMethodTestFixture<int[], EqualSequence_WhenEmpty>
    {
        static EqualSequence_WhenEmpty() =>
            For(new int[0])
                .Pass(should => should.EqualSequence())
                .Fail(should => should.EqualSequence(1));
    }

    public class EqualSequence_IgnoringCase : ExtensionMethodTestFixture<string[], EqualSequence_IgnoringCase>
    {
        static EqualSequence_IgnoringCase() =>
            For(new[] { "a", "b", "c" })
                .When(x => x.IgnoringCase)
                .Pass(should => should.EqualSequence("a", "B", "c"));
    }

    public class Contain_IEnumerable : ExtensionMethodTestFixture<int[], Contain_IEnumerable>
    {
        static Contain_IEnumerable() =>
            For(new[] { 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.Contain(null as IEnumerable<int>))
                .ThrowsArgumentException(should => should.Contain())
                .Pass(should => should.Contain(2, 3))
                .Pass(should => should.Contain(5))
                .Pass(should => should.Contain(5, 5))
                .Fail(should => should.Contain(4, 6));
    }

    public class ContainAny_IEnumerable : ExtensionMethodTestFixture<int[], ContainAny_IEnumerable>
    {
        static ContainAny_IEnumerable() =>
            For(new[] { 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.ContainAny(null as IEnumerable<int>))
                .ThrowsArgumentException(should => should.ContainAny())
                .Pass(should => should.ContainAny(4, 5))
                .Pass(should => should.ContainAny(5))
                .Pass(should => should.ContainAny(5, 5))
                .Fail(should => should.ContainAny(4, 6));
    }

    public class StartWith_IEnumerable : ExtensionMethodTestFixture<int[], StartWith_IEnumerable>
    {
        static StartWith_IEnumerable() =>
            For(new[] { 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.StartWith(null as IEnumerable<int>))
                .ThrowsArgumentException(should => should.StartWith())
                .Pass(should => should.StartWith(1))
                .Pass(should => should.StartWith(1, 2, 3))
                .Fail(should => should.StartWith(1, 2, 3, 5, 6))
                .Fail(should => should.StartWith(1, 3))
                .Fail(should => should.StartWith(9));
    }

    public class StartWithAny_IEnumerable : ExtensionMethodTestFixture<int[], StartWithAny_IEnumerable>
    {
        static StartWithAny_IEnumerable() =>
            For(new[] { 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.StartWithAny(null as IEnumerable<int>))
                .ThrowsArgumentException(should => should.StartWithAny())
                .Pass(should => should.StartWithAny(1))
                .Pass(should => should.StartWithAny(8, 1, 9))
                .Fail(should => should.StartWithAny(2, 3))
                .Fail(should => should.StartWithAny(9));
    }

    public class StartWithAny_string : ExtensionMethodTestFixture<string, StartWithAny_string>
    {
        static StartWithAny_string() =>
            For("abcdef")
                .ThrowsArgumentNullException(should => should.StartWithAny(null))
                .ThrowsArgumentException(should => should.StartWithAny())
                .Pass(should => should.StartWithAny("a"))
                .Pass(should => should.StartWithAny("abc"))
                .Fail(should => should.StartWithAny("zbc"))
                .Fail(should => should.StartWithAny("z", "x"));
    }

    public class EndWith_IEnumerable : ExtensionMethodTestFixture<int[], EndWith_IEnumerable>
    {
        static EndWith_IEnumerable() =>
            For(new[] { 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.EndWith(null as IEnumerable<int>))
                .ThrowsArgumentException(should => should.EndWith())
                .Pass(should => should.EndWith(5))
                .Pass(should => should.EndWith(2, 3, 5))
                .Fail(should => should.EndWith(0, 1, 2, 3, 5))
                .Fail(should => should.EndWith(2, 5))
                .Fail(should => should.EndWith(9));
    }

    public class EndWithAny_IEnumerable : ExtensionMethodTestFixture<int[], EndWithAny_IEnumerable>
    {
        static EndWithAny_IEnumerable() =>
            For(new[] { 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.EndWithAny(null as IEnumerable<int>))
                .ThrowsArgumentException(should => should.EndWithAny())
                .Pass(should => should.EndWithAny(5))
                .Pass(should => should.EndWithAny(8, 5, 9))
                .Fail(should => should.EndWithAny(2, 3))
                .Fail(should => should.EndWithAny(9));
    }

    public class EndWithAny_string : ExtensionMethodTestFixture<string, EndWithAny_string>
    {
        static EndWithAny_string() =>
            For("abcdef")
                .ThrowsArgumentNullException(should => should.EndWithAny(null))
                .ThrowsArgumentException(should => should.EndWithAny())
                .Pass(should => should.EndWithAny("f"))
                .Pass(should => should.EndWithAny("def"))
                .Fail(should => should.EndWithAny("dea"))
                .Fail(should => should.EndWithAny("a", "b"));
    }

    public class ConsistOf : ExtensionMethodTestFixture<int[], ConsistOf>
    {
        static ConsistOf() =>
            For(new[] { 1, 2, 3 })
                .ThrowsArgumentNullException(should => should.ConsistOf(null))
                .ThrowsArgumentException(should => should.ConsistOf())
                .Pass(should => should.ConsistOf(x => x == 3, x => x > 0, x => x == 2))
                .Pass(should => should.ConsistOf(x => x < 3, x => x > 1, x => x != 2))
                .Pass(should => should.ConsistOf(x => x > 1, x => x > 0, x => x > 1))
                .Pass(should => should.ConsistOf(x => x > 0, x => x > 0, x => x > 0))
                .Fail(should => should.ConsistOf(x => x > 0))
                .Fail(should => should.ConsistOf(x => x > 0, x => x > 0, x => x > 0, x => x > 0))
                .Fail(should => should.ConsistOf(x => x == 1, x => x == 2, x => x != 3));
    }

    public class ConsistOf_WhenHas1Item : ExtensionMethodTestFixture<int[], ConsistOf_WhenHas1Item>
    {
        static ConsistOf_WhenHas1Item() =>
            For(new[] { 1 })
                .Pass(should => should.ConsistOf(x => x == 1))
                .Fail(should => should.ConsistOf(x => x == 2));
    }

    public class ConsistOnlyOf : ExtensionMethodTestFixture<int[], ConsistOnlyOf>
    {
        static ConsistOnlyOf() =>
            For(new[] { 3, 3, 3 })
                .Pass(should => should.ConsistOnlyOf(3))
                .Fail(should => should.ConsistOnlyOf(7));
    }

    public class ConsistOnlyOf_Expression : ExtensionMethodTestFixture<int[], ConsistOnlyOf_Expression>
    {
        static ConsistOnlyOf_Expression() =>
            For(new[] { 1, 2, 3, 5 })
                .ThrowsArgumentNullException(should => should.ConsistOnlyOf(null))
                .Pass(should => should.ConsistOnlyOf(x => x > 0))
                .Fail(should => should.ConsistOnlyOf(x => x > 1));
    }

    public abstract class ExtensionMethodTestFixture<TObject, TFixture>
        where TFixture : ExtensionMethodTestFixture<TObject, TFixture>
    {
        private static readonly TestSuiteData s_testSuiteData = new();

        private Subject<TObject> _sut;

        protected static TestSuiteBuilder For(TObject testObject)
        {
            s_testSuiteData.TestObject = testObject;
            return new TestSuiteBuilder(s_testSuiteData);
        }

        public static IEnumerable<TestCaseData> GetTestActions() =>
            GetTestActionGroups().SelectMany(x => x);

        private static IEnumerable<IEnumerable<TestCaseData>> GetTestActionGroups()
        {
            yield return GenerateTestCaseData(
                "Should passes",
                s_testSuiteData.PassFunctions,
                (should, function) => Assert.DoesNotThrow(() => function(should)));

            yield return GenerateTestCaseData(
                "Should fails",
                s_testSuiteData.FailFunctions,
                (should, function) => Assert.Throws<AssertionException>(() => function(should)));

            yield return GenerateTestCaseData(
                "Should.Not passes",
                s_testSuiteData.FailFunctions,
                (should, function) => Assert.DoesNotThrow(() => function(should.Not)));

            yield return GenerateTestCaseData(
                "Should.Not fails",
                s_testSuiteData.PassFunctions,
                (should, function) => Assert.Throws<AssertionException>(() => function(should.Not)));

            yield return GenerateTestCaseData(
                "Should throws ArgumentException",
                s_testSuiteData.ThrowingArgumentExceptionFunctions,
                (should, function) => Assert.Throws<ArgumentException>(() => function(should)));

            yield return GenerateTestCaseData(
                "Should throws ArgumentNullException",
                s_testSuiteData.ThrowingArgumentNullExceptionFunctions,
                (should, function) => Assert.Throws<ArgumentNullException>(() => function(should)));
        }

        private static IEnumerable<TestCaseData> GenerateTestCaseData(
            string testName,
            List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>> functions,
            Action<ObjectVerificationProvider<TObject, Subject<TObject>>, Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>> assertionFunction)
        {
            RuntimeHelpers.RunClassConstructor(typeof(TFixture).TypeHandle);

            Action<Subject<TObject>> BuildTestAction(Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>> function) =>
                sut =>
                {
                    var should = s_testSuiteData.VerifierSetup is null
                        ? sut.Should
                        : s_testSuiteData.VerifierSetup.Invoke(sut.Should);

                    assertionFunction(should, function);
                };

            return functions.Count == 1
                ? new[] { new TestCaseData(BuildTestAction(functions[0])).SetArgDisplayNames(testName) }
                : functions.Select((x, i) => new TestCaseData(BuildTestAction(x)).SetArgDisplayNames($"{testName} #{i + 1}"));
        }

        [OneTimeSetUp]
        public void SetUpFixture() =>
            _sut = s_testSuiteData.TestObject.ToSutSubject();

        [TestCaseSource(nameof(GetTestActions))]
        public void When(Action<Subject<TObject>> testAction) =>
            testAction.Invoke(_sut);

        public class TestSuiteData
        {
            public TObject TestObject { get; set; }

            public Func<ObjectVerificationProvider<TObject, Subject<TObject>>, ObjectVerificationProvider<TObject, Subject<TObject>>> VerifierSetup { get; set; }

            public List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>> PassFunctions { get; } =
                new List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>>();

            public List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>> FailFunctions { get; } =
                new List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>>();

            public List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>> ThrowingArgumentExceptionFunctions { get; } =
                new List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>>();

            public List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>> ThrowingArgumentNullExceptionFunctions { get; } =
                new List<Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>>>();
        }

        public class TestSuiteBuilder
        {
            private readonly TestSuiteData _context;

            public TestSuiteBuilder(TestSuiteData context) =>
                _context = context;

            public TestSuiteBuilder When(Func<ObjectVerificationProvider<TObject, Subject<TObject>>, ObjectVerificationProvider<TObject, Subject<TObject>>> verifierSetup)
            {
                _context.VerifierSetup = verifierSetup;
                return this;
            }

            public TestSuiteBuilder Pass(Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>> passFunction)
            {
                _context.PassFunctions.Add(passFunction);
                return this;
            }

            public TestSuiteBuilder Fail(Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>> failFunction)
            {
                _context.FailFunctions.Add(failFunction);
                return this;
            }

            public TestSuiteBuilder ThrowsArgumentException(Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>> throwingFunction)
            {
                _context.ThrowingArgumentExceptionFunctions.Add(throwingFunction);
                return this;
            }

            public TestSuiteBuilder ThrowsArgumentNullException(Func<IObjectVerificationProvider<TObject, Subject<TObject>>, Subject<TObject>> throwingFunction)
            {
                _context.ThrowingArgumentNullExceptionFunctions.Add(throwingFunction);
                return this;
            }
        }
    }
}
