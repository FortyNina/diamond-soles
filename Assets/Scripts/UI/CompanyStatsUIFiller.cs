using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CompanyStatsUIFiller : MonoBehaviour
{

    public int playerID;

    public string ironModifier = "Iron: ";
    public string foodModifier = "Food: ";
    public string coalModifier = "Coal: ";
    public string moneyModifier = "Money: ";
    public string ratingModifier = "Company Rating: ";

    [SerializeField] private TextMeshProUGUI _ironTitle;
    [SerializeField] private TextMeshProUGUI _foodTitle;
    [SerializeField] private TextMeshProUGUI _coalTitle;
    [SerializeField] private TextMeshProUGUI _moneyTitle;
    [SerializeField] private TextMeshProUGUI _ratingTitle;







    // Update is called once per frame
    void Update()
    {
        _ironTitle.text = ironModifier + GameData.Instance.companies[playerID].oreSupplies[TileType.Iron];
        _foodTitle.text = foodModifier + GameData.Instance.companies[playerID].oreSupplies[TileType.Food];
        _coalTitle.text = coalModifier + GameData.Instance.companies[playerID].oreSupplies[TileType.Coal];
        _moneyTitle.text = moneyModifier + GameData.Instance.companies[playerID].money;
        _ratingTitle.text = ratingModifier + GameData.Instance.companies[playerID].rating.ToString();







    }
}
