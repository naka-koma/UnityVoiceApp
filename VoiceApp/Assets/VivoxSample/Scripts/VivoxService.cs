using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using Codice.Client.Common.GameUI;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace VivoxSample
{
    public class VivoxService : MonoBehaviour
    {
        private IVivoxService vivoxService;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        async void Start()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            vivoxService = Unity.Services.Vivox.VivoxService.Instance;
            await vivoxService?.InitializeAsync();

            if (vivoxService != null)
            {
                vivoxService.LoggedIn += OnLoggedIn;
                vivoxService.LoggedOut += OnLoggedOut;
                vivoxService.ChannelJoined += OnChannelJoined;
                vivoxService.ChannelLeft += OnChannelLeft;
            }

            // ログインする
            await LoginAsync("NakaKoma", true);

            // チャンネルへ参加する
            await JoinGroupChannelAsync("testChannel", ChatCapability.TextOnly);
        }

        async private void OnDestroy()
        {
            await LeaveChannelAsync("testChannel");
            await LogoutAsync();
        }

        async public Task LoginAsync(string displayName, bool enableTTS)
        {
            await vivoxService?.LoginAsync(
                new LoginOptions
                {
                    DisplayName = displayName,
                    EnableTTS = enableTTS
                });
        }

        async public Task LogoutAsync()
        {
            await vivoxService?.LogoutAsync();
        }

        async public Task JoinGroupChannelAsync(string channelName, ChatCapability chatCapability)
        {
            await vivoxService?.JoinGroupChannelAsync(channelName, chatCapability);
        }

        async public Task LeaveChannelAsync(string channelName)
        {
            await vivoxService?.LeaveChannelAsync(channelName);
        }

        private void OnLoggedIn()
        {
            Debug.Log("OnLoggedIn!");
        }

        private void OnLoggedOut()
        {
            Debug.Log("OnLoggedOut!");
        }

        private void OnChannelJoined(string channelName)
        {
            Debug.Log($"Joined, {channelName}");
        }

        private void OnChannelLeft(string channelName)
        {
            Debug.Log($"Left, {channelName}");
        }
    }
}
