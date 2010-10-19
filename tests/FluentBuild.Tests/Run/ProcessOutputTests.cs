﻿using System.Diagnostics;
using FluentBuild.Core;
using FluentBuild.Tests.Samples.Run.StdOutStdError;
using NUnit.Framework;

namespace FluentBuild.Tests.Run
{
    [TestFixture]
    public class ProcessOutputTests : TestBase
    {
        private string _outputFileLocation;

        [TestFixtureSetUp]
        public void InitialSetup()
        {
            BuildFolder sourceFolder =
                new BuildFolder(Settings.PathToSamplesFolder).SubFolder("run").SubFolder("StdOutStdError");
            FileSet fs = new FileSet().Include(sourceFolder).Filter("*.cs");
            _outputFileLocation = rootFolder + "\\stdoutstderror.exe";
            Core.Build.UsingCsc.Target.Executable.AddSources(fs).OutputFileTo(_outputFileLocation).Execute();
        }

        private void ExecuteProcess(string args)
        {
            var process = new Process();
            process.StartInfo.FileName = _outputFileLocation;
            process.StartInfo.Arguments = args;

            //redirect to a stream so we can parse it and display it
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.ErrorDialog = false;

            process.ErrorDataReceived += ProcessErrorDataReceived;
            process.OutputDataReceived += ProcessOutputDataReceived;
            process.Start();
        }

        private void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Assert.That(e.Data, Is.EqualTo(Program.NormalOutput));
        }

        private void ProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Assert.That(e.Data, Is.EqualTo(Program.ErrorOutput));
        }

        [Test]
        public void ExeShouldOutputToStandardError()
        {
            ExecuteProcess(Program.ErrorFlag);
        }

        [Test]
        public void ExeShouldOutputToStandardOutput()
        {
            ExecuteProcess(Program.NormalFlag);
        }
    }
}