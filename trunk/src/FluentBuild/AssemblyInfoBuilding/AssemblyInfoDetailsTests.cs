﻿using NUnit.Framework;

namespace FluentBuild.AssemblyInfoBuilding
{
    ///<summary>
    ///</summary>
    ///<summary />
    public class AssemblyInfoDetailsTests
    {
        ///<summary>
        ///</summary>
        ///<summary />
        public void ImportShouldNotAllowDuplicates()
        {
            var subject = new AssemblyInfoDetails(new CSharpAssemblyInfoBuilder());
            Assert.That(subject.Imports.Count, Is.EqualTo(0));
            subject.Import("test");
            subject.Import("test");
            Assert.That(subject.Imports.Count, Is.EqualTo(1));
        }
    }
}