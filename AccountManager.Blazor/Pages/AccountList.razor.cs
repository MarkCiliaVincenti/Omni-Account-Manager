﻿using AccountManager.Core.Models;
using Microsoft.JSInterop;

namespace AccountManager.Blazor.Pages
{
    public partial class AccountList
    {
        private Account? editAccountTarget;
        private bool addAccountPrompt { get; set; } = false;
        private int amountOfAccountsFilered;

        protected override async Task OnInitializedAsync()
        {
            await _appState.UpdateAccounts();
            _accountFilterService.OnFilterChanged += () => LoadList();
            amountOfAccountsFilered = _appState?.Accounts?.Count(acc => !_accountFilterService.AccountTypeFilter.Contains(acc.AccountType) || acc?.Name?.ToLower()?.Contains(_accountFilterService.AccountNameFilter.ToLower()) is false) ?? 0;
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsRuntime.InvokeVoidAsync("appendElement", "accounts-grid", "filter-indicator");
                await _jsRuntime.InvokeVoidAsync("appendElement", "accounts-grid", "new-account-placeholder");
                await _jsRuntime.InvokeVoidAsync("showElement", "new-account-placeholder");
            }
            amountOfAccountsFilered = _appState?.Accounts?.Count(acc => !_accountFilterService.AccountTypeFilter.Contains(acc.AccountType) || acc?.Name?.ToLower()?.Contains(_accountFilterService.AccountNameFilter.ToLower()) is false) ?? 0;
        }

        public void LoadList()
        {
            amountOfAccountsFilered = _appState?.Accounts?.Count(acc => !_accountFilterService.AccountTypeFilter.Contains(acc.AccountType) || acc?.Name?.ToLower()?.Contains(_accountFilterService.AccountNameFilter.ToLower()) is false) ?? 0;
            InvokeAsync(() => StateHasChanged());
        }

        public void StartAddAccount()
        {
            addAccountPrompt = true;
        }

        public void CancelAddAccount()
        {
            addAccountPrompt = false;
        }

        public void FinishAddAccount()
        {
            addAccountPrompt = false;
        }
    }
}

