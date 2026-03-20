using System.Text.Json.Serialization;

namespace GitHubAnalyzer
{
    public class GitHubUser
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("login")]
        public string Username { get; set; }

        [JsonPropertyName("public_repos")]
        public int PublicRepos { get; set; }

        [JsonPropertyName("followers")]
        public int Followers { get; set; }
    }

    public class GitHubRepo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int Stars { get; set; }
    }
}
