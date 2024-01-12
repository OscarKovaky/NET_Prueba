using System.Collections;

namespace TTP.Contract.Auth;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public IEnumerable<Slug> Tenants { get; set; } = Enumerable.Empty<Slug>();
}

public class Slug
{
    public string SlugTenant { get; set; }
}