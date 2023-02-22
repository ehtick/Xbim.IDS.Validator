﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xbim.IDS.Validator.Core.Binders;
using Xbim.IDS.Validator.Core.Interfaces;
using Xbim.InformationSpecifications;

namespace Xbim.IDS.Validator.Core.Tests
{
    [Collection(nameof(TestEnvironment))]
    public class ServicesTests
    {
        private static readonly IServiceProvider provider;
        static ServicesTests()
        {
            provider = TestEnvironment.ServiceProvider;
        }

        [Fact]
        public void CanResolveModelBinderFactory()
        {
            var factory = provider.GetRequiredService<IIdsFacetBinderFactory>();

            factory.Should().NotBeNull();
        }

        [InlineData(typeof(IfcTypeFacet), typeof(IfcTypeFacetBinder))]
        [InlineData(typeof(AttributeFacet), typeof(AttributeFacetBinder))]
        [InlineData(typeof(IfcPropertyFacet), typeof(PsetFacetBinder))]
        [InlineData(typeof(IfcClassificationFacet), typeof(IfcClassificationFacetBinder))]
        [InlineData(typeof(MaterialFacet), typeof(MaterialFacetBinder))]
        // Docs, relations, Partof
        [Theory]
        public void ModelBinderFactoryResolvesIfcType(Type facetType, Type expectedType)
        {
            var factory = provider.GetRequiredService<IIdsFacetBinderFactory>();

            var result = factory.Create((IFacet)Activator.CreateInstance(facetType));
            result.Should().NotBeNull().And.BeOfType(expectedType);
        }

        [Fact]
        public void CanResolveModelBinder()
        {
            var modelBinder = provider.GetRequiredService<IIdsModelBinder>();

            modelBinder.Should().NotBeNull();
        }

        [Fact]
        public void CanResolveModelValidator()
        {
            var modelValidator = provider.GetRequiredService<IIdsModelValidator>();

            modelValidator.Should().NotBeNull();
        }

    }
}
