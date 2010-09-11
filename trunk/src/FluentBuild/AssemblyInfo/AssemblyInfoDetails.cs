using System;
using System.Collections.Generic;
using System.IO;

namespace FluentBuild
{
    public class AssemblyInfoDetails
    {
        internal readonly List<String> _imports = new List<string>();
        internal readonly IAssemblyInfoBuilder AssemblyInfoBuilder;

        internal string AssemblyCopyright;
        internal string _assemblyDescription;
        internal string _assemblyTitle;
        internal string _assemblyVersion;
        internal bool _clsCompliant;
        internal bool _clsCompliantSet;
        internal bool _comVisible;
        internal bool _comVisibleSet;
        internal string _company;
        internal string _product;

        internal AssemblyInfoDetails(IAssemblyInfoBuilder assemblyInfoBuilder)
        {
            AssemblyInfoBuilder = assemblyInfoBuilder;
        }

        public AssemblyInfoDetails Import(params string[] files)
        {
            foreach (string import in files)
                ImportDropIfDuplicate(import);
            return this;
        }

        private void ImportDropIfDuplicate(string file)
        {
            if (!_imports.Contains(file.Trim()))
                _imports.Add(file.Trim());
        }


        public AssemblyInfoDetails ComVisible(bool visible)
        {
            ImportDropIfDuplicate("System.Runtime.InteropServices");
            _comVisibleSet = true;
            _comVisible = visible;
            return this;
        }

        public AssemblyInfoDetails ClsCompliant(bool compliant)
        {
            ImportDropIfDuplicate("System");
            _clsCompliantSet = true;
            _clsCompliant = compliant;
            return this;
        }

        public AssemblyInfoDetails Version(string value)
        {
            ImportDropIfDuplicate("System.Reflection");
            _assemblyVersion = value;
            return this;
        }

        public AssemblyInfoDetails Title(string value)
        {
            ImportDropIfDuplicate("System.Reflection");
            _assemblyTitle = value;
            return this;
        }


        public AssemblyInfoDetails Description(string value)
        {
            ImportDropIfDuplicate("System.Reflection");
            _assemblyDescription = value;
            return this;
        }


        public AssemblyInfoDetails Copyright(string value)
        {
            ImportDropIfDuplicate("System.Reflection");
            AssemblyCopyright = value;
            return this;
        }
           

        public AssemblyInfoDetails Company(string value)
        {
            _company = value;
            return this;
        }

        public AssemblyInfoDetails Product(string value)
        {
            _product = value;
            return this;
        }


        public void OutputTo(BuildArtifact artifactLocation)
        {
            OutputTo(artifactLocation.ToString());
        }

        public void OutputTo(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(AssemblyInfoBuilder.Build(this));
            }
        }
 
    }
}