﻿using Blazored.LocalStorage;
using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Blogger.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Blogger.WebUI
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly IJwtAuthenticationManagerService _jwtAuthenticationManagerService;
        private readonly ILocalStorageService _localStorageService;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly IHttpClientFactory _httpClientFactory;
        public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService,
            IJwtAuthenticationManagerService jwtAuthenticationManagerService)
        {
            _localStorageService = localStorageService;
            _jwtAuthenticationManagerService = jwtAuthenticationManagerService;
            _httpClientFactory = httpClientFactory;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User user = await GetUserByJWTAsync();

            if (user != null && user.Email != null)
            {
                //create a claims
                var claimEmailAddress = new Claim(ClaimTypes.Name, user.Email);
                var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id));
                var claimFirstName = new Claim("FirstName", user.FirstName);
                var claimLastName = new Claim("LastName", user.LastName);
                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier, claimFirstName, claimLastName }, "jwtAuth");
				foreach (var role in user.UserRoles)
				{
					var claim = new Claim(ClaimTypes.Role, role.Role.Name);
					claimsIdentity.AddClaim(claim);
				}
				//create claimsPrincipal
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));// new AuthenticationState(claimsPrincipal);
            }
            else
                return await Task.FromResult(new AuthenticationState(_anonymous));
        }

        public async Task<User> GetUserByJWTAsync()
        {
            //pulling the token from localStorage
            var jwtToken = await _localStorageService.GetItemAsync<string>("jwt_token");
            if (jwtToken == null) return null;

            var principle = _jwtAuthenticationManagerService.GetPrincipalFromExpiredToken(jwtToken);
            var email = principle.FindFirst(ClaimTypes.Email)?.Value;
            
            var httpClient = _httpClientFactory.CreateClient("blog");
            var response = await httpClient.PostAsJsonAsync<string>("/api/User/getuserbyemail", email);
            var user = await response.Content.ReadFromJsonAsync<User>();
            return user;
        }
        public void NotifyAuthState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
