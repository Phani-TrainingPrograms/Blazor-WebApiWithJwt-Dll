using BlazorApp.Entities;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BlazorApp.Services
{
    public interface IEmpUIService
    {
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto> GetById(int id);
        Task<HttpResponseMessage> AddEmployee(EmployeeDto employee);
        Task<HttpResponseMessage> DeleteEmployee(int id);
        Task<HttpResponseMessage> UpdateEmployee(EmployeeDto employee);
    }

    public class AuthService
    {
        private readonly HttpClient http;
        public AuthService()
        {
            http = new HttpClient();
            http.BaseAddress =   new Uri(Program.Configuration["AuthUrl"]!);
            
        }

        public async Task<string> GetJwtToken(UserInfo user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await http.PostAsync("", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
    public class EmpUIService : IEmpUIService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public EmpUIService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
            
        }

        
        public async Task<HttpResponseMessage> AddEmployee(EmployeeDto employee)
        {
            var json = JsonSerializer.Serialize(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("AddNew", content);
        }

        public async Task<HttpResponseMessage> DeleteEmployee(int id)
        {
            return await _httpClient.DeleteAsync($"Delete/{id}");
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            var token = await _authService.GetJwtToken(new UserInfo { Username = "test", Password = "password" });
            Console.WriteLine(token);
            var data = JsonSerializer.Deserialize<TokenInfo>(token);
            Console.WriteLine(data.token);
            var http = HttpClientFactory.CreateHttp("http://localhost:5020/api/Employee/", data.token);
            return await http.GetFromJsonAsync<List<EmployeeDto>>("All");
        }

        public async Task<EmployeeDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDto>($"All/{id}");
        }

        public async Task<HttpResponseMessage> UpdateEmployee(EmployeeDto employee)
        {
            var json = JsonSerializer.Serialize(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync("Update", content);
        }
    }

   
}