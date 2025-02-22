﻿using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace NLog.Extensions.Logging.Tests
{
    public class ConfigSettingLayoutRendererTests
    {
        [Fact]
        public void ConfigSettingFallbackDefaultLookup()
        {
            ConfigSettingLayoutRenderer.DefaultConfiguration = null;
            var layoutRenderer = new ConfigSettingLayoutRenderer { Item = "Options.TableName", Default = "MyTableName" };
            var result = layoutRenderer.Render(LogEventInfo.CreateNullEvent());
            Assert.Equal("MyTableName", result);
        }

        [Fact]
        public void ConfigSettingGlobalConfigLookup()
        {
            var memoryConfig = new Dictionary<string, string>();
            memoryConfig["Mode"] = "Test";
            ConfigSettingLayoutRenderer.DefaultConfiguration = new ConfigurationBuilder().AddInMemoryCollection(memoryConfig).Build();
            var layoutRenderer = new ConfigSettingLayoutRenderer { Item = "Mode" };
            var result = layoutRenderer.Render(LogEventInfo.CreateNullEvent());
            Assert.Equal("Test", result);
        }

        [Fact]
        public void ConfigSettingGlobalConfigEscapeLookup()
        {
            var memoryConfig = new Dictionary<string, string>();
            memoryConfig["Microsoft.Logging"] = "Test";
            ConfigSettingLayoutRenderer.DefaultConfiguration = new ConfigurationBuilder().AddInMemoryCollection(memoryConfig).Build();
            var layoutRenderer = new ConfigSettingLayoutRenderer { Item = @"Microsoft\.Logging" };
            var result = layoutRenderer.Render(LogEventInfo.CreateNullEvent());
            Assert.Equal("Test", result);
        }
    }
}
