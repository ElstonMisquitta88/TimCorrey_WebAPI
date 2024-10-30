using BlazorApiClient.Logic;
using BlazorApiClient.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlazorApiClient.Pages;

public partial class Counter
{


    private List<TodoModel>? todos;
    private HttpClient? client;
    private string? errorMessage;
    private TodoModel addTODO = new TodoModel(); // Needed to Work with Code and Design View

    [Inject] IDemoLogic DemoVal { get; set; } = default!;

    private async void FetchTodos()
    {
        //[+] Common
        client = factory.CreateClient("api");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokeninfo.Token);
        //[-] Common

        try
        {
            todos = await client.GetFromJsonAsync<List<TodoModel>>("Todos");
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        await InvokeAsync(StateHasChanged);
    }

    private async void CompleteTodo(TodoModel todo)
    {
        await client!.PutAsJsonAsync<string>($"Todos/{todo.Id}/Complete", "");
        todo.IsComplete = true;
        await InvokeAsync(StateHasChanged);
    }


    private async void Deletetodo(TodoModel todo)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"Todos/{todo.Id}");
        var response = await client!.SendAsync(request);
        response.EnsureSuccessStatusCode();
        FetchTodos();
        await InvokeAsync(StateHasChanged);
    }


    private async void SaveNewTodo()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "Todos");
        var content = new StringContent(JsonSerializer.Serialize<string>(addTODO.Task!), null, "application/json");
        request.Content = content;
        var response = await client!.SendAsync(request);
        response.EnsureSuccessStatusCode();
        FetchTodos();
        await InvokeAsync(StateHasChanged);
    }
}