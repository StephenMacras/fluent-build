﻿using NUnit.Framework;

using Rhino.Mocks;

namespace FluentBuild.Database
{
    ///<summary />
    public class MsSqlCreateOrUpdateTests
    {
        ///<summary />
        public void ShouldSetPropertyOnEngine()
        {
            string pathToCreateScript = @"c:\temp";

            var engine = MockRepository.GenerateMock<IMsSqlEngine>();
            var subject = new MsSqlCreateOrUpdate(engine);
            
            MsSqlCreate createScript = subject.PathToCreateScript(pathToCreateScript);
            
            Assert.That(createScript, Is.Not.Null);
            engine.AssertWasCalled(x=>x.PathToCreateScript=pathToCreateScript);
        }
    }
}