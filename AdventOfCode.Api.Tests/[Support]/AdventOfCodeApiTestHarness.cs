using AdventOfCode.Application.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Api.Tests;

[Collection(ApiTestCollection.Name)]
public class AdventOfCodeApiTestHarness(ApiTestContext testContext, string baseAddress = "")
{

    private string _baseAddress = baseAddress;

    protected AdventOfCodeTestAdapter CreateTestAdapter()
    {
        return new AdventOfCodeTestAdapter(testContext.ApiCallAdapter, _baseAddress);
    }

    protected void DiffAssert(string fileNameForExpectedValue, string actualValue, [CallerFilePath] string callerFilePath = "")
    {
        //var settings = new VerifySettings();
        //settings.UseFileName(fileNameForExpectedValue);
        //settings.DisableRequireUniquePrefix();

        //Verify(actualValue, settings, sourceFile: callerFilePath).GetAwaiter().GetResult();
    }

    protected HttpResponseMessage CreateApiCallResponseFromJsonFile(
        string filename,
        HttpStatusCode httpStatusCode = HttpStatusCode.OK,
        string fileExtension = ".json",
        [CallerFilePath] string callerFilePath = "")
    {
        return new();
    }

    public FileInfo GetFileInfo(
        string filename,
        string directory = "[TestFiles]",
        string fileExtension = ".json",
        [CallerFilePath] string callerFilePath = "")
    {
        var filePath = GetFilePath(callerFilePath, $"{directory.MakeSureItEndsWithSlash()}{filename}{fileExtension}");

        return new FileInfo(filePath);
    }


    public string GetContentFromJsonFile(
        string filename,
        string directory = "[TestFiles]",
        string fileExtension = ".json",
        [CallerFilePath] string callerFilePath = "")
    {
        return GetContentFromFile($"{directory.MakeSureItEndsWithSlash()}{filename}{fileExtension}", callerFilePath);
    }

    public T GetObjectFromJsonFile<T>(
        string filename,
        string directory = "[TestFiles]",
        string fileExtension = ".json",
        [CallerFilePath] string callerFilePath = "")
    {
        var content = GetContentFromFile($"{directory.MakeSureItEndsWithSlash()}{filename}{fileExtension}", callerFilePath);

        return JsonSerializerForApiTests.FromJsonString<T>(content);
    }

    private string GetFilePath(string callerFilePath, string filename)
    {
        return $"{GetPathToRootFolder(callerFilePath)}{filename}";
    }

    protected string GetPathToRootFolder(string callerFilePath)
    {
        var testProjectName = GetTestProjectNameFromFilePath();
        var parts = callerFilePath.Split(new[] { testProjectName }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
            throw new Exception("Unable to extract the path to the test folder");

        var testFilePathRelativeToProjectRoot = parts[1];
        var indexOfLastSlash = Path.DirectorySeparatorChar == '/'
            ? testFilePathRelativeToProjectRoot.LastIndexOf('/')
            : testFilePathRelativeToProjectRoot.LastIndexOf('\\');
        var relativePathWithoutFileName = testFilePathRelativeToProjectRoot.Substring(1, indexOfLastSlash);

        return relativePathWithoutFileName;

        string GetTestProjectNameFromFilePath()
        {
            const string ApiTests = "AdventOfCode.Api.Tests";
            const string ApplicationTests = "AdventOfCoe.Application.Tests";

            if (callerFilePath.Contains(ApiTests)) return ApiTests;
            if (callerFilePath.Contains(ApplicationTests)) return ApplicationTests;

            throw new ApplicationException("Unable to extract path to test folder because the file path does not contain a known test project. If you have added a new test project (or renamed an old project) you need to update the list of defined projects in this file!");
        }
    }

    private string GetContentFromFile(string filename, string callerFilePath)
    {
        var filePath = GetFilePath(callerFilePath, filename);
        try
        {
            return File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                throw new ApplicationException($"Unable to find file {filename}. Configure it to be copied to the output directory (Copy If Newer or Copy Always)! Complete path: {filePath}", ex);
            }

            throw;
        }
    }

    private class CustomDatabaseDtoResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            return objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Cast<MemberInfo>().ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var result = base.CreateProperty(member, memberSerialization);

            var propInfo = member as PropertyInfo;

            result.Writable |= propInfo != null && propInfo.CanWrite;

            return result;
        }
    }

}
