using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Pugzor.Core.Abstractions;
using Pugzor.Core.Extensions;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "MvcBuilderExtensions")]
    public class MvcBuilderExtensionsTests : IClassFixture<MvcBuilderExtensionsTestsFixture>
    {
        public MvcBuilderExtensionsTestsFixture Fixture;

        public MvcBuilderExtensionsTests(MvcBuilderExtensionsTestsFixture fixture)
        {
            Fixture = fixture;
        }

        public static IEnumerable<object[]> NeededServices =
            new List<object[]>
            {
                new []{ typeof(INodeServices) },
                new []{ typeof(IPugRendering) },
                new []{ typeof(IPugzorViewEngine) }
            };

        [Theory]
        [MemberData(nameof(NeededServices))]
        public void MvcBuilderExtensions_AddPugzor_AddsNeededServices(Type neededService)
        {
            Assert.Contains(neededService, Fixture.Services);
        }

    }

    public class MvcBuilderExtensionsTestsFixture
    {
        public IEnumerable<Type> Services { get; }

        public MvcBuilderExtensionsTestsFixture()
        {
            var mvcBuilder = new MvcBuilder(new ServiceCollection(), new ApplicationPartManager());
            mvcBuilder.AddPugzor();
            Services = mvcBuilder.Services.Select(s => s.ServiceType).ToList();
        }
    }
}
