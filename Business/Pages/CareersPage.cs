using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Reflection;
using CICD.Core.Factory;
using CICD.Core.Utilities;

namespace CICD.Business.Pages
{
    public class CareersPage : BasePage
    {
        private readonly By careersLocator = By.LinkText("Careers");
        private readonly By jobSearchKeywordLocator = By.Id("new_form_job_search-keyword");
        private readonly By locationDropdownLocator = By.ClassName("select2-selection__arrow");
        private readonly By remoteCheckboxLocator = By.XPath("//label[contains(.,'Remote')]");
        private readonly By findButtonLocator = By.CssSelector("button[type='submit']");
        private readonly By dateLabelLocator = By.XPath("//label[contains(.,'Date')]");
        private readonly By viewAndApplyButtonLocator = By.CssSelector(".search-result__item:nth-child(1) .button-text");
        private readonly string specifiedLocationLocator = "//li[contains(text(),\"{location}\")]/parent::ul";
        private readonly By jobTitleLocator = By.CssSelector("h1:nth-child(1)");
        private Actions action;

        public CareersPage() : base(DriverManager.GetInstance())
        { }

        public void GoToCareersMenu()
        {
            try
            {
                ElementInteractions.GetElement(driver, careersLocator).Click();
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }

        public void GoToDreamJobSection(string skill)
        {
            try
            {
                ElementInteractions.GetElement(driver, jobSearchKeywordLocator).SendKeys(skill);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }

        public void OpenLocationDropdown()
        {
            try
            {
                action = new Actions(driver);
                var dropdownElement = ElementInteractions.GetElement(driver, locationDropdownLocator);
                action.Click(dropdownElement).Pause(TimeSpan.FromSeconds(1)).Perform();
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }

        public void SelectSpecifiedLocation(string location)
        {
            try
            {
                
                action = new Actions(driver);

                // <ul> arriba de <li> porque estoy usando nth-child para llegar al <li>
                var dropdownAllLocations = ElementInteractions.GetElement(driver, By.XPath(specifiedLocationLocator.Replace("{location}", location)));
                action.Click(dropdownAllLocations).Perform();
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }

        public void SelectRemoteLocation()
        {
            try
            {
                IWebElement remoteCheckBox = ElementInteractions.GetElement(driver, remoteCheckboxLocator);
                ElementInteractions.JsClickOnElement(driver, remoteCheckBox);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }

        public void ClickFindButton()
        {
            try
            {
                ElementInteractions.GetElement(driver, findButtonLocator).Click();
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }

        public void WaitForResultsToLoad()
        {
            try
            {
                var dateLink = ElementWaits.WaitForClickableElement(driver, dateLabelLocator);
                ElementInteractions.MoveToElement(driver, dateLink);
                ElementInteractions.JsClickOnElement(driver, dateLink);
                Thread.Sleep(1000); //Esperar a que se ordenen los resultados	
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public void ClickOnFirstResult()
        {
            try
            {
                var viewAndApplyButton = ElementWaits.WaitForClickableElement(driver, viewAndApplyButtonLocator);
                ElementInteractions.MoveToElement(driver, viewAndApplyButton);
                ElementInteractions.JsClickOnElement(driver, viewAndApplyButton);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public void AssertJobTitle(string skill)
        {
            try
            {
                var jobTitle = ElementInteractions.GetElement(driver, jobTitleLocator);
                Assert.That(jobTitle.Text.Contains(skill, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
    }
}
