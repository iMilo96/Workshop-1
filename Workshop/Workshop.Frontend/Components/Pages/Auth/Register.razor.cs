using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics.Metrics;
using Workshop.Frontend.Repositories;
using Workshop.Frontend.Services;
using Workshop.Shared.DTOs;
using Workshop.Shared.Enums;

namespace Workshop.Frontend.Components.Pages.Auth;

public partial class Register
{
    private UserDTO userDTO = new();
    private bool loading;
    private string? imageUrl;
    private string? titleLabel;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public bool IsAdmin { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        titleLabel = IsAdmin ? "Registro de Administrador" : "Registro de Usuario";
    }

    private void ImageSelected(string imageBase64)
    {
        userDTO.Photo = imageBase64;
        imageUrl = null;
    }

    private void ReturnAction()
    {
        NavigationManager.NavigateTo("/");
    }

    private void InvalidForm()
    {
        Snackbar.Add("Por favor llena todos los campos del formulario.", Severity.Warning);
    }

    private async Task CreateUserAsync()
    {
        if (userDTO.Email is null || userDTO.PhoneNumber is null)
        {
            InvalidForm();
            return;
        }

        userDTO.UserType = UserType.User;
        userDTO.UserName = userDTO.Email;

        if (IsAdmin)
        {
            userDTO.UserType = UserType.Admin;
        }

        loading = true;
        var responseHttp = await Repository.PostAsync<UserDTO, TokenDTO>("/api/accounts/CreateUser", userDTO);
        loading = false;
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }

        await LoginService.LoginAsync(responseHttp.Response!.Token);
        NavigationManager.NavigateTo("/");
    }
}