﻿using Breach;

using FluentAssertions;

using NUnit.Framework;

using System;

namespace BreachTest
{
    public class StackSetTest
    {
        StackSet<char> Subject()
        {
            var subject = new StackSet<char>();
            subject.Push('a');
            subject.Push('b');
            subject.Push('c');
            subject.Push('d');
            return subject;
        }

        [Test]
        public void ItCanPush()
        {
            var subject = new StackSet<char>();
            subject.Push('a').Should().Be(1);
        }

        [Test]
        public void ItHasCount()
        {
            Subject().Count.Should().Be(4);
        }

        [Test]
        public void ItCanPop()
        {
            Subject().Pop().Should().Be('d');
        }

        [Test]
        public void ItCanPushAndPop()
        {
            var subject = Subject();

            new[]
            {
                subject.Pop(), subject.Pop(), subject.Pop(), subject.Pop()
            }
            .Should()
            .BeEquivalentTo(new[] { 'd', 'c', 'b', 'a' });
        }

        [Test]
        public void ItHasConstantTimeContains()
        {
            var subject = Subject();
            subject.Contains('a').Should().BeTrue();
            subject.Contains('z').Should().BeFalse();

            var d = subject.Pop();
            subject.Contains(d).Should().BeFalse();
        }

        [Test]
        public void AddingSomethingAlreadyThereThrows()
        {
            var subject = Subject();
            var startCount = subject.Count;
            Action addDuplicate = () => subject.Push('a');
            addDuplicate.Should().Throw<AlreadyInStackSetException>();
            subject.Count.Should().Be(startCount);
        }
    }
}