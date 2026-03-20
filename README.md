# GitHub Profile Analyzer

A .NET console application that uses the public GitHub REST API to show statistics about any public GitHub user's portfolio.

```
=== PROFILE ANALYSIS: Microsoft ===
Followers: 115141
Public Repositories: 7687
Total Repository Stars: 12343
Most Used Language: C#
====================================
```

This project was built to demonstrate modern backend integration techniques, specifically focusing on non-blocking I/O operations, JSON deserialization, and data aggregation using LINQ.

## Features

* **Asynchronous API Consumption:** Utilizes `async/await` and a properly scoped `HttpClient` to fetch web data without blocking the main execution thread.
* **Strongly Typed Data Models:** Maps raw JSON payloads to custom C# objects (`GitHubUser`, `GitHubRepo`) using `System.Text.Json` attributes.
* **Data Aggregation via LINQ:** Processes lists of repositories on the fly to calculate total star counts and determine the user's most frequently used programming language.
* **Decoupled Architecture:** The core `GitHubApiService` operates independently of the console UI, making the logic easily transferable to a WinForms, WPF, or ASP.NET Core environment.

## Tech Stack & Concepts Demonstrated

* **Language:** C# / .NET
* **Concepts:** REST API Integration, Asynchronous Programming (`Task<T>`), HTTP Headers (User-Agent), Exception Handling for Web Requests.
