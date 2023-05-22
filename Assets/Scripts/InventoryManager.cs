using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Asset
{
    public string AssetName { get; set; }
    public string AssetUrl { get; set; }

    public string AssetStyle { get; set; }

    public string AssetDescription { get; set; }
    public Asset(string assetName, string assetUrl, string assetStyle, string assetDescription)
    {
        AssetName = assetName;
        AssetUrl = assetUrl;
        AssetStyle = assetStyle;
        AssetDescription = assetDescription;
    }
};

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int coins = 0;
    public TMP_Text inventoryTextComponent;
    public Font fontSelect;
    string reneverseAssetsText;
    ReneController controller;
    //public static List<Asset> assets;

    private void AddTextComponent(string name)
    {
        GameObject tmp = new GameObject(name);
        tmp.name = name;
        Text txtTmp = tmp.AddComponent<Text>();
        txtTmp.font = fontSelect;
        txtTmp.text = name;
        txtTmp.fontSize = 20;
        txtTmp.verticalOverflow = VerticalWrapMode.Overflow;
        txtTmp.horizontalOverflow = HorizontalWrapMode.Overflow;


        tmp.transform.localScale = Vector3.one;

        tmp.transform.SetParent(inventoryTextComponent.transform);

        tmp.transform.TransformVector(0, 0, 0);

        tmp.transform.position = new Vector3(0, 0, 0);
    }

    public void AddCoins(int c)
    {
        inventoryTextComponent = GetComponentInChildren<TMP_Text>();
        coins += c;
        inventoryTextComponent.text = "Coins :" +  coins.ToString()  + reneverseAssetsText;
    } 
    void Start()
    {
        inventoryTextComponent = GetComponentInChildren<TMP_Text>();
        SyncAssets(GetAssets());
      
    }

    // Test/Stub Function
    public List<Asset> GetAssets() {

        Asset one = new Asset("Booster", "DemoURL", "DemoStyle","");
        Asset two = new Asset("Coins", "CoinsURL", "CoinsStyle", "");
        Asset three = new Asset("Guns", "GunsURL", "GunsStyle", "");

        if (ReneController.userConnected)
        {
            Debug.Log("Real stuff ");
            Debug.Log(ReneController.assetList?.Count);
            return ReneController.assetList;
        }
        
        return new List<Asset>(){ one, two , three};
    }

    /*public void AddReneverseAsset(Asset asset)
    {
        Debug.Log("Adding asset " + asset.AssetName);
        assets.Add(asset);
        
    }*/

    public void SyncAssets(List<Asset> assets)
    {
       // controller = FindObjectOfType<ReneController>();
        
        string inventory = "";
        assets?.ForEach(asset =>
        {
            string name = asset.AssetName;
            Debug.Log(name);
            
            inventory += " ->" + name + " \n";
           

        });
        reneverseAssetsText = "\nReneverse Assets: \n" + inventory;
        inventoryTextComponent.text = inventoryTextComponent.text + reneverseAssetsText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
