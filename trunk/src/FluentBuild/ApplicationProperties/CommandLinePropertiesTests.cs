﻿using FluentBuild.Core;
using NUnit.Framework;

namespace FluentBuild.ApplicationProperties
{
    ///<summary>
    ///</summary>
    ///<summary />
    public class CommandLinePropertiesTests
    {
        ///<summary>
        ///</summary>
        ///<summary />
        public void ShouldConstructWithProperties()
        {
            Assert.That(Properties.CommandLineProperties.Properties, Is.Not.Null);
        }

        ///<summary>
        ///</summary>
        ///<summary />
        public void ShouldGetAndSetProperly()
        {
            var value = "testvalue";
            var name = "testname";
            Properties.CommandLineProperties.Add(name, value);
            Assert.That(Properties.CommandLineProperties.Properties[name], Is.EqualTo(value));
        }

    }
}