using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ReneController : MonoBehaviour
{
    public static bool userConnected = false;
    public static List<Asset> assetList;
    public static ReneController controller = null;
    //static API reneApi;

    public static void InitiateAuth(API reneApi)
    {
        Debug.Log("Auth init");
        StaticCoroutine.Start(ConnectReneService(reneApi));
    }

    public static async void ConnectToReneverse(string email)
    {
        var reneApi = ReneAPIManager.API();
        if (reneApi == null)
        {
            Debug.Log("Not Initalized");
            return;
        }       
        await reneApi.Game().Connect(email);
        InitiateAuth(reneApi);
    }

    public static IEnumerator ConnectReneService(API reneApi)
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

                yield return GetUserAssetsAsync(reneApi);
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


    private static async Task GetUserAssetsAsync(API reneApi)
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
            ($" - Asset Id '{asset.NftId}' Name '{asset.Metadata.Name}"));
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
