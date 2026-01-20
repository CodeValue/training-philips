using Microsoft.AspNetCore.Mvc.Testing;

namespace OrganLens.Api.Tests.Integration;

/// <summary>
/// Boots the real API in memory. Each factory instance has its own in-memory store.
/// </summary>
public class ApiFactory : WebApplicationFactory<Program>
{
}
