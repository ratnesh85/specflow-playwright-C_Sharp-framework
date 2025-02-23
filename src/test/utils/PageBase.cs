using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowPlaywrightFramework.src.test.utils
{
    public class PageBase
    {
        private static IPage page;
        public ISpecFlowOutputHelper _specflowOutPutHelper;
        public IBrowserContext context;
        private Hooks _hooks;
        public PageBase(Hooks hooks, ISpecFlowOutputHelper specflowOutPutHelper)
        {
            page = hooks.page;
            _specflowOutPutHelper = specflowOutPutHelper;
            context = hooks.context;
            _hooks = hooks;
        }

        public async Task PressEsc()
        {
            await page.Keyboard.PressAsync("Escape");
        }
        public async Task ClearTextField(String locator)
        {
            await Click(locator);
            await page.Keyboard.PressAsync("Control+A");
            await page.Keyboard.PressAsync("Backspace");
        }
        public async Task WaitUntilVisible(string locator, int timeout)
        {
            await page.WaitForSelectorAsync(locator, new PageWaitForSelectorOptions
            { Timeout = timeout, State = WaitForSelectorState.Visible });
        }



        public async Task WaitUntilHidden(string locator, int timeout)
        {
            await page.WaitForSelectorAsync(locator, new PageWaitForSelectorOptions
            { Timeout = timeout, State = WaitForSelectorState.Hidden });
        }
        public async Task Click(String locator)
        {
            await page.ClickAsync(locator);
        }

        public async Task Enter(String locator, String val)
        {
            await page.FillAsync(locator, val);
            _specflowOutPutHelper.WriteLine("Entered the value " + val);
        }


        public async Task Sleep(int timeOut) //(float timeOut, int v)
        {
            await page.WaitForTimeoutAsync(timeOut);
        }

        public async Task Type(String locator, String val)
        {
            await page.TypeAsync(locator, val);
        }


        public async Task NewPage(String locator)
        {
            var newPage = await context.RunAndWaitForPageAsync(async () =>
            {
                await page.ClickAsync(locator);
            });
            await newPage.WaitForLoadStateAsync();
            _specflowOutPutHelper.WriteLine(await newPage.TitleAsync());
            page = _hooks.page = newPage;
            await page.BringToFrontAsync();
        }

        public async Task SwitchToDefaultPage()
        {
            var allPages = context.Pages;
            page = _hooks.page = allPages.First();

        }

        public async Task SelectNewPageWithTitle(String title)
        {
            var allPages = context.Pages;
            for (int i = 0; i < allPages.Count; i++)
            {
                var pageTitle = await allPages[i].TitleAsync();
                if (pageTitle == title)
                {
                    page = _hooks.page = allPages[i];
                    await page.BringToFrontAsync();
                    break;
                }
            }
        }

        public async Task<String> GetText(String locator)
        {
            return await page.TextContentAsync(locator);
        }

        public async Task Navigate(String url)
        {
            await page.GotoAsync(url);
        }

        public async Task Close()
        {
            await page.CloseAsync();
        }

        public async Task SelectByValue(String locator, String value)
        {
            await page.SelectOptionAsync(locator, value);
        }

        public async Task SelectMultipleValue(String locator, String[] value)
        {
            await page.SelectOptionAsync(locator, value);
        }

        public async Task<bool> IsChecked(String locator)
        {
            return await page.IsCheckedAsync(locator);
        }

        public async Task<bool> IsEnabled(String locator)
        {
            return await page.IsEnabledAsync(locator);
        }

        public async Task<bool> IsDisabled(String locator)
        {
            return await page.IsDisabledAsync(locator);
        }

        public async Task<bool> IsVisible(String locator)
        {

            return await page.IsVisibleAsync(locator);
        }

        public async Task Reload()
        {
            await page.ReloadAsync();
            await page.WaitForLoadStateAsync();
        }

        public async Task<String> GetTitle()
        {
            return await page.TitleAsync();

        }

        public async Task PressKey(String keyName)
        {
            await page.Keyboard.PressAsync(keyName);

        }

        public async Task<String> GetInputValue(String locator)
        {
            return await page.InputValueAsync(locator);

        }

        public async Task SelectDropDown(String dropDownLocator, String dropDownOptionLocator, String valueTobeSelected)
        {
            await page.ClickAsync(dropDownLocator);
            await page.Locator(dropDownOptionLocator).Filter(new() { HasText = valueTobeSelected }).ClickAsync();
        }

        public async Task<string> GetUrl()
        {
            var pageURL = page.Url;
            return pageURL;
        }

        public async Task WaitForUrl(String urlString)
        {
            await page.WaitForURLAsync(urlString);
        }

        public async Task<String> GetAttribute(string locator, string attribute)
        {
            return await page.GetAttributeAsync(locator, attribute);
        }

        public async Task<String> GetBorderColor(string locator)
        {
            var element = await page.QuerySelectorAsync(locator);

            // Get the computed style of the border color
            var computedStyle = await element.EvaluateAsync<string>(@"(element) => {
            const styles = getComputedStyle(element);
            return styles.borderColor;
              }");

            return computedStyle;
        }

        //public async Task WaitUntilVisible(string locator, int timeout)
        //{
        //     await page.WaitForSelectorAsync(locator, new PageWaitForSelectorOptions {Timeout = timeout, State = WaitForSelectorState.Visible});
        //}

        //public async Task WaitUntilHidden(string locator, int timeout)
        //{
        //    await page.WaitForSelectorAsync(locator, new PageWaitForSelectorOptions { Timeout = timeout, State = WaitForSelectorState.Hidden});
        //}

        public async Task ToggleCheckbox(string locator, bool enable)
        {
            bool status = await IsChecked(locator);
            if ((status == false && enable) || (status == true && (!enable)))
            {
                await Click(locator);
            }
        }

        public async Task<List<string>> GetTextInList(String locator)
        {
            List<String> list = new List<String>();
            var dropdownOptions = await page.QuerySelectorAllAsync(locator);
            foreach (var option in dropdownOptions)
            {
                // Extract the text content of each option
                var optionText = await option.TextContentAsync();
                list.Add(optionText.Trim());
            }
            return list;
        }

        public async Task NavigateSecondPage(string url)
        {
            var newTab = await context.NewPageAsync();
            await newTab.WaitForLoadStateAsync();
            await newTab.GotoAsync(url);
            page = _hooks.page = newTab;
            await page.BringToFrontAsync();

        }

        public async Task WaitForSelector(string locators, int timeout)
        {
            await page.WaitForSelectorAsync(locators, new PageWaitForSelectorOptions { Timeout = timeout });
        }

        public async Task SelectValue(string loc, string value)
        {
            var locator = loc + "//*[contains(text(), 'value')]";
            await page.ClickAsync(locator.Replace("value", value));

        }

        public async Task WaitForLoader(string locators, int time)
        {

            try
            {
                await WaitUntilVisible(locators, time);
                await WaitUntilHidden(locators, time);
            }
            catch (Exception)
            {
                _specflowOutPutHelper.WriteLine("No loader appeaared");

            }

        }

        public async Task WaitForPageLoad()
        {
            await page.WaitForLoadStateAsync();
        }

        public async Task UploadFile(string file, string locator)
        {
            await page.SetInputFilesAsync(locator, file);
            await Sleep(5000);
        }

        public async Task DoubleClick(String locator)
        {
            await page.DblClickAsync(locator);
        }

        public async Task<string> GetInnerText(String locator)
        {
            return await page.InnerTextAsync(locator, new PageInnerTextOptions { Timeout = 60000 });
        }

        public async Task Download(string savePath, string locator)
        {
            var waitForDownloadTask = page.WaitForDownloadAsync();
            await page.ClickAsync(locator);
            var download = await waitForDownloadTask;

            // Wait for the download process to complete and save the downloaded file somewhere
            await download.SaveAsAsync(savePath);
        }

        public async Task<String> GetSelectedDropdownValue(string locator)
        {
            var dropdown = await page.QuerySelectorAsync(locator);

            string selectedOption = await dropdown.EvalOnSelectorAsync<string>(
                "option:checked",
                "option => option.textContent"
            );

            return selectedOption;
        }

        public async Task<IFrame?> SelectFrame(String locator)
        {
            var iframe = await page.WaitForSelectorAsync(locator);
            var frame = await iframe.ContentFrameAsync();
            return frame;
        }
        public async Task<IFrame?> SelectChildFrame(String parentLocator)
        {
            var frame1 = await SelectFrame(parentLocator);
            //var iframe = await page.WaitForSelectorAsync(parentLocator);
            //var frame = await iframe.ContentFrameAsync();
            var frame2 = frame1.ChildFrames.First();
            return frame2;
        }


        public async Task WaitForElement(string locators, int time)
        {

            try
            {
                await WaitUntilVisible(locators, time);
                await WaitUntilHidden(locators, time);
            }
            catch (Exception)
            {
                _specflowOutPutHelper.WriteLine("No element appeaared");

            }

        }

        public async Task<List<string>> GetInputValueInList(String locator)
        {
            List<String> list = new List<String>();
            var dropdownOptions = await page.QuerySelectorAllAsync(locator);
            foreach (var option in dropdownOptions)
            {
                // Extract the text content of each option
                var optionText = await option.InputValueAsync();
                list.Add(optionText.Trim());
            }
            return list;
        }

        public async Task<List<string>> GetSelectedDropdownValueInList(String locator)
        {
            List<String> list = new List<String>();
            var dropdownOptions = await page.QuerySelectorAllAsync(locator);
            foreach (var option in dropdownOptions)
            {
                // Extract the text content of each option
                var optionText = await option.EvalOnSelectorAsync<string>(
                "option:checked",
                "option => option.textContent"
            );
                list.Add(optionText.Trim());
            }
            return list;
        }

        public async Task GoBack()
        {
            await page.GoBackAsync();
        }

        public async Task NewPageUrl(String url)
        {
            var newPage = await context.NewPageAsync();
            page = _hooks.page = newPage;
            await page.GotoAsync(url);
            await page.WaitForLoadStateAsync();
            _specflowOutPutHelper.WriteLine(await newPage.TitleAsync());
            await page.BringToFrontAsync();
        }
        public async Task ResizeWindow(int h, int w)
        {
            await page.SetViewportSizeAsync(h, w);
        }
       
    }
}
