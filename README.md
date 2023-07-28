SkyScraper
==========

An asynchronous web scraper / web crawler using async / await and Reactive Extensions 

Usage
- 
    var httpClient = new HttpClient {UserAgentName = "mybot"}; //optional UserAgentName
    var scraper = new Scraper(httpClient, new ScrapedUrisDictionary()); //use built in IHttpClient and IScrapedUris implementations
    var io = new ImageScraperObserver(httpClient, new FileWriter(new DirectoryInfo("c:\\temp")));
    scraper.Subscribe(io); //use built in image scraper
    scraper.Subscribe(new ConsoleWriterObserver()); //use built in console writer
    scraper.Subscribe(x => Console.WriteLine(x.Uri)); //implement your own subscriber
    scraper.MaxDepth = 2; //optional
    scraper.TimeOut = TimeSpan.FromMinutes(5); //optional
    scraper.IgnoreLinks = new Regex("spam"); //optional - ignore links in page
    scraper.IncludeLinks = new Regex("stuff"); //optional - scrape links in page
    scraper.ObserverLinkFilter = new Regex("things"); //optional - trigger observers when link matches
    scraper.DisableRobotsProtocol = true; //optional
    scraper.Scrape(new Uri("http://www.mywebsite.com/")).Wait();
