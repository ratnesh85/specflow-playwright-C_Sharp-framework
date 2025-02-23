using SpecFlowPlaywrightFramework.src.test.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowPlaywrightFramework.src.test.pages
{
    public class SamplePage : PageBase
    {
        private readonly Faker faker;
        public SamplePage(Hooks hooks, ISpecFlowOutputHelper specflowOutPutHelper) : base(hooks, specflowOutPutHelper)      
        {
          faker = new Faker();
        }

        #region selectors
        private string menu_workspace = "//a[@id='testing']";
        private string link_editInput = "//a[normalize-space()='Edit']";
        private string textbox_fullName = "#fullName";

        #endregion

        public async Task ClickWorkspace()
        {
            await this.Click(this.menu_workspace);
        }

        public async Task ClickEditInput()
        {
            await this.Click(this.link_editInput);
        }

        public async Task EnterFullName()
        {
            await this.Type(this.textbox_fullName, faker.Name.FullName());
        }
    
    }  
    
}
