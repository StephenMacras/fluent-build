﻿using System;
using System.Diagnostics;

namespace FluentBuild
{
    public class ProcessUtility
    {
        public static void StartProcess(string fileName, string args)
        {
            StartProcess(fileName, args, string.Empty);
        }

        public static void StartProcess(string fileName, string args, string workingDirectory)
        {
            MessageLogger.Write("exec", String.Format("Running '{0} {1}", fileName, args));
            var startInfo = new ProcessStartInfo(fileName);
            startInfo.UseShellExecute = false;
            startInfo.Arguments = args;

            if (!String.IsNullOrEmpty(workingDirectory))
                startInfo.WorkingDirectory = workingDirectory;

            Process process = Process.Start(startInfo);
            if (process != null)
                process.WaitForExit();
        }
    }
}