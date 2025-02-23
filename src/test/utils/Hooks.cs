using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowPlaywrightFramework.src.test.utils
{
    [Binding]
    public class Hooks : CommonMethods
    {
        public IPage page { get; set; } = null!;
        public IBrowserContext context;
        public static int numberOfFailedTests;
        private ScenarioContext _scenarioContext;
        private IPlaywright playwright;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Timeout = 10000,
                SlowMo = 50,
                Args = new[] { "--window-position=-5,-5" }
            });

            context = await browser.NewContextAsync();
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            page = await context.NewPageAsync();
            await page.GotoAsync("https://letcode.in/");
            await page.WaitForLoadStateAsync();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            string uniqueString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            uniqueString = uniqueString.Replace("/", "");
            uniqueString = uniqueString.Replace(" ", "");
            uniqueString = uniqueString.Replace(":", "");
            string failPath = GetCurrentProjectDirectory() + "Screenshot\\Failed\\" + uniqueString + $"{_scenarioContext.ScenarioInfo.Title}.png";
            string passPath = GetCurrentProjectDirectory() + "Screenshot\\Passed\\" + uniqueString + $"{_scenarioContext.ScenarioInfo.Title}.png";
            var scenarioInfo = ScenarioContext.Current.ScenarioInfo;
            var tags = scenarioInfo.Tags;
            var scenarioName = scenarioInfo.Title;
            MatchCollection matches = Regex.Matches(scenarioName, @"C\d+");

            if (_scenarioContext.TestError != null && _scenarioContext.ScenarioExecutionStatus != ScenarioExecutionStatus.OK)
            {
                numberOfFailedTests++;
                await context.Tracing.StopAsync(new()
                {
                    Path = $"{_scenarioContext.ScenarioInfo.Title}_{numberOfFailedTests}_trace.zip"
                });

                await page.ScreenshotAsync(new()
                {
                    Path = failPath,
                    FullPage = true
                });
            }

            else
            {
                await page.ScreenshotAsync(new()
                {
                    Path = passPath,
                    FullPage = true
                });
            }
            await page.CloseAsync();
            await context.CloseAsync();
            playwright.Dispose();
        }
    }
}
