public class ResourceManager
{
    private static ResourceManager instance;
    private ResourceManager () {}

    public static ResourceManager GetInstance () {
        if (instance == null)
        {
            instance = new ResourceManager();
        }
        return instance;
    }

    private int _gold = 500;
    
    // for call method use: ResourceManager.GetInstance().Method()
    public int getCountGold () {
        return _gold;
    }

    public bool checkAndBuyGold (int cost) {
        if (_gold >= cost) {
            _gold -= cost;
            return true;
        }
        else {
            return false;
        }
    }

    public void addGold (int count) {
        _gold += count;
    }
}