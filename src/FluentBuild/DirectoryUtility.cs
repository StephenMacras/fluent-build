﻿using System;
using System.IO;

namespace FluentBuild
{
    public class DirectoryUtility
    {
        public static void RecreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                MessageLogger.Write("delete", path);
                Directory.Delete(path, true);
            }
            MessageLogger.Write("mkdir", path);
            Directory.CreateDirectory(path);
        }

        public static void CopyAllFiles(string from, string to)
        {
            MessageLogger.Write("copy", String.Format("Copying all files from '{0}' to '{1}'", from, to));
            foreach (string file in Directory.GetFiles(from))
            {
                File.Copy(file, Path.Combine(to, Path.GetFileName(file)));
            }
        }

        public static void CopyAllFiles(FileSet from, string to)
        {
            MessageLogger.Write("copy", String.Format("Copying {0} files to '{1}'", from.Files.Count, to));
            foreach (string file in from.Files)
            {
                File.Copy(file, Path.Combine(to, Path.GetFileName(file)));
            }
        }
    }
}