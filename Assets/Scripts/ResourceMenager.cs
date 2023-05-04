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

    private int _gold = 1000;
    private int MaxHumans = 5;
    private int HumansUsed;
    
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

    public bool checkGold (int cost)
    {
        if (_gold >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void addGold (int count) {
        _gold += count;
    }

    public void setGold(int count)
    {
        _gold = count;
    }

    public void setMaxHumansCount(int count)
    {
        MaxHumans = count;
    }

    public int usedHumansCount()
    {
        return HumansUsed;
    }

    public int maxHumansCount()
    {
        return MaxHumans;
    }

    public void addMaxHumans(int count)
    {
        MaxHumans += count;
    }

    public void useHuman(int count)
    {
        HumansUsed += count;
    }
}