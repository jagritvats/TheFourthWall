using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ReneController : MonoBehaviour
{
    public static bool userConnected = false;
    public static List<Asset> assetList;
    public static ReneController controller = null;
    public static API reneApi = null;

    public static string BOOSTER_TEMPLATE_ID = "8137f252-183b-44db-9e8d-57ce81edad01";
    public static string COIN_TEMPLATE_ID = "0e7f37bd-7075-4cc1-a927-935ed9cd51a7";

    public static async void ConnectToReneverse(string email)
    {
        reneApi = ReneAPIManager.API();

        if (reneApi == null)
        {
            Debug.Log("Not Initalized");
            return;
        }       
        await reneApi.Game().Connect(email);
        StaticCoroutine.Start(ConnectReneService());
    }

    public static async void MintBooster()
    {

        await reneApi.Game().AssetMint(BOOSTER_TEMPLATE_ID);
        Debug.Log("Booster minted!");
    }

    public static async void MintCoin()
    {

        await reneApi.Game().AssetMint(COIN_TEMPLATE_ID);
        Debug.Log("Coin minted!");
    }

    public static async Task SearchForPlayers()
    {
        if(userConnected)
        {
            var ans = await reneApi.User().Search("jagritvats6@gmail.com");
            ans.Items.ForEach(item =>
            {
                Debug.Log(item.Email);
            });

        }
    }

    private static IEnumerator ConnectReneService()
    {
        var counter = 0;
        //Interval how often the code checks that user accepted to log in
        var secondsToIncrement = 1;
        int secondsToWait = 30;
        while (counter <= secondsToWait && !userConnected)
        {
            print(counter);
            if (reneApi.IsAuthorized())
            {


                //Here can be added any extra logic once the user logged in

                Debug.Log("Authorized !");

                yield return GetUserAssetsAsync();
                userConnected = true;
               /* if (reneverseConnectionStatus)
                {
                    reneverseConnectionStatus.text = "Connected to Reneverse :) !";
                }*/
            }
            
            yield return new WaitForSeconds(secondsToIncrement);
            counter += secondsToIncrement;
        }
    }


    private static async Task GetUserAssetsAsync()
    {
        Debug.Log("Getting Assets!");
        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();
        var items = userAssets.Items;
        assetList = new List<Asset>();
        Debug.Log(items + " Count:"  + userAssets?.Items?.Count);
        //return userAssets;
        //By this way you could check in the Unity console your NFT assets
        userAssets?.Items.ForEach
        (asset => Debug.Log
            ($" - Asset Id '{asset.NftId}' Name '{asset.Metadata.Name}' Asset template id' {asset.AssetTemplateId}"));
        userAssets?.Items.ForEach(asset =>
        {
            Debug.Log("Assest " + asset.ToString());
            string assetName = asset.Metadata.Name;
            string assetImageUrl = asset.Metadata.Image;
            string assetDescription = asset.Metadata.Description;
            string assetStyle = "";
            Asset assetObj = new(assetName, assetImageUrl, assetStyle, assetDescription);
            //inventoryManager.AddReneverseAsset(assetObj);
            assetList.Add(assetObj);
            asset.Metadata?.Attributes?.ForEach(attribute =>
            {
                
                //Keep in mind that this TraitType should be preset in your Reneverse Account
                if (attribute.TraitType == "Style")
                {
                    assetStyle = attribute.Value;
                }
            });
            /*
            asset.Metadata?.Attributes?.ForEach(
                attribute =>
                {
                    Debug.Log(attribute.TraitType + "  " + attribute.Value);
                });
            */
            
            //An example of how you could keep retrieved information
           /* Asset assetObj = new Asset(assetName, assetImageUrl, assetStyle);
            //one of many ways to add it to the game logic 
            _assetManager.userAssets.Add(assetObj);*/

            
        });
        
        
        Debug.Log("Synced Assets!");
    }



}
