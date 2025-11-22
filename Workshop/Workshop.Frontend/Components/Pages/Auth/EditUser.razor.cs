using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics.Metrics;
using System.Net;
using Workshop.Frontend.Repositories;
using Workshop.Frontend.Services;
using Workshop.Shared.DTOs;
using Workshop.Shared.Entities;

namespace Workshop.Frontend.Components.Pages.Auth;

[Authorize]
public partial class EditUser
{
    private User? user;
    private bool loading = true;
    private string? imageUrl;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadUserAsyc();

        if (!string.IsNullOrEmpty(user!.Photo))
        {
            imageUrl = user.Photo;
            user.Photo = null;
        }
    }

    private void ShowModal()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
        DialogService.Show<ChangePassword>("Cambiar Contraseña", closeOnEscapeKey);
    }

    private async Task LoadUserAsyc()
    {
        var responseHttp = await Repository.GetAsync<User>($"/api/accounts");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError!, Severity.Error);
            return;
        }
        user = responseHttp.Response;
        loading = false;
    }

    private void ImageSelected(string imagenBase64)
    {
        user!.Photo = imagenBase64;
        imageUrl = null;
    }

    private async Task SaveUserAsync()
    {
        var responseHttp = await Repository.PutAsync<User, TokenDTO>("/api/accounts", user!);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }

        await LoginService.LoginAsync(responseHttp.Response!.Token);
        Snackbar.Add("Usuario modificado con éxito.", Severity.Success);
        NavigationManager.NavigateTo("/");
    }

    private void ReturnAction()
    {
        NavigationManager.NavigateTo("/");
    }

    private void InvalidForm()
    {
        Snackbar.Add("Por favor llena todos los campos del formulario.", Severity.Warning);
    }
}