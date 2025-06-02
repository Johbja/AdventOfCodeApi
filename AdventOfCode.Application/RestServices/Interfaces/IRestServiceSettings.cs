namespace AdventOfCode.Application.RestServices.Interfaces;

public interface IRestServiceSettings
{
    public string BaseClientUrl { get; }
    IReadOnlyCollection<Header> DefaultHeaders { get; }
}


public interface IRestServiceWithBaseAuthSettings : IRestServiceSettings
{
    string Username { get; }
    string Password { get; }
}