﻿using System;
using System.IO;

namespace FluentBuild.Tests
{
    public static class Settings
    {
        public static string PathToRootFolder
        {
            get { return Path.GetFullPath(Environment.CurrentDirectory + "\\" + RelativePathToRootFolder); }
        }

        public static string PathToSamplesFolder
        {
            get { return  Path.GetFullPath(Environment.CurrentDirectory + "\\" + RelativePathToSamplesFolder); }
        }

        public static string RelativePathToRootFolder
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RelativePathToRootFolder"];
            }
        }

        public static string RelativePathToSamplesFolder
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RelativePathToSamplesFolder"];
            }
        }
    }
}