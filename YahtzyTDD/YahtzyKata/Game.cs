using static YahtzyKata.YatzyCategories;

namespace YahtzyKata;

public class Game
{
    public Player Player { get; }
    public List<Categories> CategoriesToPlay { get; }
    
    public Game(Player player)
    {
        Player = player;
        CategoriesToPlay = Enum.GetValues(typeof(Categories)).Cast<Categories>().Where(x => x != Categories.Played).ToList();
    }
}