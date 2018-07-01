using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Kelson.Common.Linq.Tests
{
    public class Extensions_Should
    {
        [Fact]
        public void CompleteActionForEachItem()
        {
            var list = new List<int>();

            ForEach.In(1, 2, 3).Do(i => list.Add(i * 2))
                .SequenceEqual(ForEach.In(1, 2, 3))
                .Should()
                .BeTrue();

            list.SequenceEqual(ForEach.In(2, 4, 6))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void FilterByType()
        {
            ForEach.In<object>(1, "2", 3)
                .WhereIs<int>()
                .SequenceEqual(ForEach.In(1, 3))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void CoalesceNullArrays()
        {
            int[] values = null;

            values.EmptyIfNull().Should().BeEmpty();

            new int[] { 1 }.EmptyIfNull().Single().Should().Be(1);
        }

        [Fact]
        public void CoalesceNullLists()
        {
            List<int> values = null;

            values.EmptyIfNull().Should().BeEmpty();

            new List<int>{ 1 }.EmptyIfNull().Single().Should().Be(1);
        }

        [Fact]
        public void PairSelectorResults()
        {
            ForEach.In(1, 2, 3)
                .PairedWith(i => i.ToString())
                .All(ir => int.Parse(ir.result) == ir.item)
                .Should()
                .BeTrue();
        }

        [Fact]
        public void AppendValues()
        {
            ForEach.In(1, 2).Append(ForEach.In(3, 4))
                .SequenceEqual(ForEach.In(1, 2, 3, 4))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void AppendValue()
        {
            ForEach.In(1, 2).Append(3)
                .SequenceEqual(ForEach.In(1, 2, 3))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void PrependValue()
        {
            1.PrependTo(ForEach.In(2, 3))
                .SequenceEqual(ForEach.In(1, 2, 3))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void SkipIndexOfArray()
        {
            new int[] { 1, 2, 3 }
                .ExceptIndex(1)
                .SequenceEqual(ForEach.In(1, 3))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void SkipIndexOfList()
        {
            new List<int> { 1, 2, 3 }
                .ExceptIndex(1)
                .SequenceEqual(ForEach.In(1, 3))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void SkipIndexOfEnumerable()
        {
            ForEach.In(1, 2, 3)
                .ExceptIndex(1)
                .SequenceEqual(ForEach.In(1, 3))
                .Should()
                .BeTrue();
        }
    }
}
