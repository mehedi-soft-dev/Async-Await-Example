using Async_Await_Example;
using System.Diagnostics;
using System.Net;

//ExecuteSysnc();
ExecuteAsync();
Console.ReadKey();

void ExecuteSysnc()
{
    var stopWatch = new Stopwatch();
    
    stopWatch.Start();
    RunDownloadSync();
    stopWatch.Stop();

    Console.WriteLine($"Total Execution Time : {stopWatch.ElapsedMilliseconds}");
}

async void ExecuteAsync()
{
    var stopWatch = new Stopwatch();

    stopWatch.Start();
    //await RunDownloadAsync();
    await RunDownloadParallenAsync();
    stopWatch.Stop();

    Console.WriteLine($"Total Execution Time : {stopWatch.ElapsedMilliseconds}");
}

void RunDownloadSync()
{
    List<string> websites = PrepData();
    foreach(string site in websites)
    {
        WebsiteDataModel result = DownloadSite(site);
        Console.WriteLine($"{result.WebsiteUrl} downloaded: {result.WebsiteData.Length} long.");
    }
}

async Task RunDownloadAsync()
{
    List<string> websites = PrepData();
    foreach (string site in websites)
    {
        WebsiteDataModel result = await Task.Run(() => DownloadSite(site));
        Console.WriteLine($"{result.WebsiteUrl} downloaded: {result.WebsiteData.Length} long.");
    }
}

async Task RunDownloadParallenAsync()
{
    List<string> websites = PrepData();
    List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
    foreach (string site in websites)
    {
        tasks.Add(Task.Run(() => DownloadSite(site)));
        
    }

    var result = await Task.WhenAll(tasks);

    foreach(var item in result)
        Console.WriteLine($"{item.WebsiteUrl} downloaded: {item.WebsiteData.Length} long.");
}

WebsiteDataModel DownloadSite(string websiteUrl)
{
    WebsiteDataModel websiteDataModel = new WebsiteDataModel();
    WebClient client = new WebClient();

    websiteDataModel.WebsiteUrl = websiteUrl;
    websiteDataModel.WebsiteData = client.DownloadString(websiteUrl);

    return websiteDataModel;
}

List<string> PrepData()
{
    List<string> output = new List<string>();

    output.Add("https://www.yahoo.com");
    output.Add("https://www.google.com");
    output.Add("https://www.facebook.com");
    output.Add("https://www.cnn.com");
    output.Add("https://www.codeproject.com");
    output.Add("https://www.prothomalo.com");

    return output;
}