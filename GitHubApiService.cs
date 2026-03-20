using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubAnalyzer
{
    public class GitHubApiService
    {
        // HttpClient is designed to be instantiated once and reused throughout the app's life
        private readonly HttpClient _httpClient;

        public GitHubApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.github.com/");

            // CRITICAL: The GitHub API will reject requests that don't have a User-Agent header!
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CSharp-Portfolio-Analyzer");
        }

        public async Task<GitHubUser> GetUserAsync(string username)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"users/{username}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new Exception("User not found.");

                throw new Exception($"API Request failed: {response.StatusCode}");
            }

            // Read the JSON string and deserialize it into C# class
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GitHubUser>(jsonResponse);
        }

        public async Task<List<GitHubRepo>> GetUserRepositoriesAsync(string username)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"users/{username}/repos?per_page=100");
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GitHubRepo>>(jsonResponse) ?? new List<GitHubRepo>();
        }

        public async Task AnalyzeProfileAsync(string username, Action<string> onProgress, Action<string> onResult)
        {
            try
            {
                onProgress($"Fetching profile data for '{username}'...");
                GitHubUser user = await GetUserAsync(username);

                onProgress($"Fetching repositories for '{username}'...");
                List<GitHubRepo> repos = await GetUserRepositoriesAsync(username);

                // Calculate statistics using LINQ
                int totalStars = repos.Sum(r => r.Stars);

                string favoriteLanguage = repos
                    .Where(r => !string.IsNullOrEmpty(r.Language))
                    .GroupBy(r => r.Language)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault() ?? "Unknown/Mixed";

                string report = $"\n=== PROFILE ANALYSIS: {user.Name ?? user.Username} ===" +
                                $"\nFollowers: {user.Followers}" +
                                $"\nPublic Repositories: {user.PublicRepos}" +
                                $"\nTotal Repository Stars: {totalStars}" +
                                $"\nMost Used Language: {favoriteLanguage}" +
                                $"\n======================================";

                onResult(report);
            }
            catch (Exception ex)
            {
                onResult($"\nERROR: {ex.Message}");
            }
        }
    }
}
