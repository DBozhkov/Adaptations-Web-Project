namespace AspNetCoreTemplate.Web.Tests
{
    using Adaptations.Web;
    using Microsoft.AspNetCore.Hosting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;

    using Xunit;

    public class SeleniumTests : IClassFixture<SeleniumServerFactory<Startup>>
    {
        private readonly SeleniumServerFactory<Startup> server;
        private readonly IWebDriver browser;

        public SeleniumTests(SeleniumServerFactory<Startup> server)
        {
            this.server = server;
            var builder = new WebHostBuilder().UseStartup<Startup>();
            this.server.RootUri = this.server.RootUri ?? "http://localhost"; // Update with the appropriate URL
            var opts = new ChromeOptions();
            opts.AddArguments("--headless", "--ignore-certificate-errors");
            this.browser = new RemoteWebDriver(opts);
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public void FooterOfThePageContainsPrivacyLink()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);
            Assert.Contains(
                this.browser.FindElements(By.CssSelector("footer a")),
                x => x.GetAttribute("href").EndsWith("/Home/Privacy"));
        }

        [Fact]
        public void VerifySliderImages()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);

            // Check if there are images in the slider.
            var sliderImages = this.browser.FindElements(By.CssSelector(".slider img"));
            Assert.NotEmpty(sliderImages);
        }

        [Fact]
        public void VerifyMovieLinksInSlider()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);

            // Verify the links in the slider.
            var sliderLinks = this.browser.FindElements(By.CssSelector(".slider a"));
            Assert.NotEmpty(sliderLinks);

            // Click the first link and verify the page title.
            var firstSliderLink = sliderLinks[0];
            firstSliderLink.Click();
            Assert.Contains("MovieId", this.browser.Url);
        }

        [Fact]
        public void VerifyThumbnailImages()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);

            // Check if there are thumbnail images.
            var thumbnailImages = this.browser.FindElements(By.CssSelector(".latest-movie img"));
            Assert.NotEmpty(thumbnailImages);
        }

        [Fact]
        public void VerifyMovieLinksInThumbnails()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);

            // Verify the links in the thumbnails.
            var thumbnailLinks = this.browser.FindElements(By.CssSelector(".latest-movie a"));
            Assert.NotEmpty(thumbnailLinks);

            // Click the first link and verify the page title.
            var firstThumbnailLink = thumbnailLinks[0];
            firstThumbnailLink.Click();
            Assert.Contains("MovieId", this.browser.Url);
        }
    }
}
