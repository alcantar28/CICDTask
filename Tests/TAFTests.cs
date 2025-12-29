using CICD.Business.Pages;

namespace CICD.Tests  //Faltaba namespace
{
    [TestFixture]
    public class TAFTests: BaseTest  //clase pública
    {
        //Needs an empty constructor to avoid "OneTimeSetup: No suitable constructor was found" error.
        public TAFTests()
        { }

        [Test]
        [TestCase(".NET Developer", "All Locations")]  //Tengo que poner el mismo número de parámetros en el método de esta prueba
        public void SearchForPosition(string skill, string location) //público
        {
            CareersPage careersPage = new CareersPage();
            careersPage.GoToCareersMenu();
            careersPage.GoToDreamJobSection(skill);
            careersPage.OpenLocationDropdown();
            careersPage.SelectSpecifiedLocation(location);
            careersPage.SelectRemoteLocation();
            careersPage.ClickFindButton();
            careersPage.WaitForResultsToLoad();
            careersPage.ClickOnFirstResult();
            careersPage.AssertJobTitle(skill);
        }

        [Test]
        [TestCase("\"BLOCKCHAIN\"/\"Cloud\"/\"Automation\"")]
        public void SearchByKeywords(string keywords) //público
        {
            var searchPage = new SearchPage();
            searchPage.WaitForSearchPanelToLoad();
            searchPage.TypeKeywords(keywords);
            searchPage.SearchForKeywords();
            searchPage.AssertSearchResults(keywords);
        }
    }
}