using System.Collections;
using System.Threading.Tasks;
using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using UnityEngine;

namespace ReneVerse.Demo
{
    public class ReneVerseServiceExample : MonoBehaviour
    {
        [SerializeField] private ReneverseUIExample reneVerseUI;
        [SerializeField] [Range(0, 60)] private int secondsToWait = 30;


        private void OnEnable()
        {
            reneVerseUI.OnReneVerseConnectClicked += ReneVerseConnectClicked;
        }

        private void OnDisable()
        {
            reneVerseUI.OnReneVerseConnectClicked -= ReneVerseConnectClicked;
        }

        private async void ReneVerseConnectClicked(string email)
        {
            var reneApi = ReneAPIManager.API();

            var connected = await reneApi.Game().Connect(email);
            StartCoroutine(ConnectReneService(reneApi));
        }

        private IEnumerator ConnectReneService(API reneApi)
        {
            var counter = 0;
            var userConnected = false;
            var secondsToIncrement = 1;
            while (counter <= secondsToWait && !userConnected)
            {
                print($"{counter}");
                if (reneApi.IsAuthorized())
                {
                    print("User successfully connected!");
                    yield return GetUserAssetsAsync(reneApi);
                    userConnected = true;
                }

                yield return new WaitForSeconds(secondsToIncrement);
                counter += secondsToIncrement;
            }
        }

        private async Task GetUserAssetsAsync(API reneApi)
        {
            AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();
            userAssets?.Items.ForEach
            (asset => Debug.Log
                ($" - Asset Id '{asset.NftId}' Name '{asset.Metadata.Name}"));


            userAssets?.Items.ForEach(asset =>
            {
                string assetName = asset.Metadata.Name;
                string assetImageUrl = asset.Metadata.Image;
                string assetStyle = "";
                asset.Metadata?.Attributes?.ForEach(attribute =>
                {
                    print($"TraitType {attribute.TraitType} has value of {attribute.Value}");
                });
            });
        }
    }
}