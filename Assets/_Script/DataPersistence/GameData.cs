using System.Collections.Generic;

public class GameData
{
    public List<CompletedLevelInfo> completedLevels { get; set; }
}

public class CompletedLevelInfo
{
    public int id { get; set; }
    public int starsNumber { get; set; }
}
