using System;
using System.Threading.Tasks;

namespace GitHubAnalyzer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== GitHub Profile Analyzer ===");
            GitHubApiService apiService = new GitHubApiService();

            while (true)
            {
                Console.Write("\nEnter a GitHub username (or 'q' to quit): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.ToLower() == "q") break;

                // Passing in delegates to handle UI updates, keeping the API service completely independent of the Console.
                await apiService.AnalyzeProfileAsync(
                    username: input,
                    onProgress: message => Console.WriteLine($"[*] {message}"),
                                                     onResult: report => Console.WriteLine(report)
                );
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
