namespace BlazorApp.Entities
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public int EmpSalary { get; set; }
        public string EmpAddress { get; set; } = string.Empty;
    }

    public class UserInfo
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class TokenInfo
    {
        public string token { get; set; } = string.Empty;
    }

    public class HttpClientFactory
    {
        public static HttpClient CreateHttp(string baseAddres, string token)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddres);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            return httpClient;
        }
    }
}