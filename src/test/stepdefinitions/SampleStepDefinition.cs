using SpecFlowPlaywrightFramework.src.test.pages;
using SpecFlowPlaywrightFramework.src.test.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowPlaywrightFramework.src.test.stepdefinitions
{
    [Binding]
    public class SampleStepDefinition
    {
        private readonly SamplePage samplePage;
        private ISpecFlowOutputHelper _specFlowOutPutHelper;
        private ScenarioContext _scenarioContext;
        public SampleStepDefinition(Hooks hooks, ISpecFlowOutputHelper specFlowOutputHelper, ScenarioContext scenarioCntext)
        {
            _scenarioContext = scenarioCntext;
            samplePage = new SamplePage(hooks, specFlowOutputHelper);
            _specFlowOutPutHelper = specFlowOutputHelper;
        }

        [Given(@"User navigate to workspace page")]
        public async Task GivenUserNavigateToWorkspacePage()
        {
            await samplePage.ClickWorkspace();
            await samplePage.WaitForPageLoad();
        }

        [When(@"I click on edit in input section")]
        public async Task WhenIClickOnEditInInputSection()
        {
            await samplePage.ClickEditInput();
        }

        [When(@"I enter the random name in name textbox")]
        public async Task WhenIEnterTheRandomNameInNameTextbox()
        {
            await samplePage.EnterFullName();
        }

    }
}
