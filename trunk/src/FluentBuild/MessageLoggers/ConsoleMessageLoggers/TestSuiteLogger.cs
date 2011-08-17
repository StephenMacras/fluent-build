﻿using System;
using FluentBuild.Core;

namespace FluentBuild.MessageLoggers.ConsoleMessageLoggers
{
    internal class TestSuiteLogger : ITestSuiteMessageLogger
    {
        private readonly int _indentation;
        public TestSuiteLogger(int indentation, string name)
        {
            _indentation = indentation;
           MessageLogger.Write("TEST", "".PadLeft(indentation, ' ') + name);
        }

        public ITestSuiteMessageLogger WriteTestSuiteStared(string name)
        {
            return new TestSuiteLogger(_indentation+1, name);
        }

        public void WriteTestSuiteFinished()
        {
        }

        public ITestLogger WriteTestStarted(string testName)
        {
            return new TestLogger(_indentation+1, testName);
        }
    }
}