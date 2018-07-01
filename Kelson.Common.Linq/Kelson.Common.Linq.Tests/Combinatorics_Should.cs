using System;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Kelson.Common.Linq.Tests
{
    public class Combinatorics_Should
    {        
        [Fact]
        public void GetAllPermutations()
        {
            int Factorial(int i) => i == 1 ? 1 : i * Factorial(i - 1);

            for (int i = 1; i < 7; i++)
            {
                var result = Enumerable.Range(1, i).Permutations(i).ToList();
                result.First().SequenceEqual(Enumerable.Range(1, i)).Should().BeTrue();
                result.Last().SequenceEqual(Enumerable.Range(1, i).Reverse()).Should().BeTrue();
                result.Count.Should().Be(Factorial(i));
            }
        }

        [Fact]
        public void GetAllCombinations()
        {
            for (int i = 1; i < 7; i++)
            {
                var result = Enumerable.Range(1, i).Combinations().ToList();
                result.First().Should().BeEmpty();
                result.Last().SequenceEqual(Enumerable.Range(1, i)).Should().BeTrue();
                result.Count.Should().Be((int)Math.Pow(2, i));
            }
        }
    }
}
