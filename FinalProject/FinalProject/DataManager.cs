namespace FinalProject;

public class DataManager
{
    //FileSaver fileSaver;

    public List<Family> families;

    public DataManager()
    {
        //fileSaver = new FileSaver("families.txt");

        families = new List<Family>();
        if(!File.Exists("families.txt"))
        {
            File.Create("families.txt").Close();
        }
        var familiesFileContent = File.ReadAllLines("families.txt");
        foreach(var line in familiesFileContent)
        {
            var splitted = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
            var familyName = splitted[0];

        }
    }

    public void AddFamily(Family family)
    {
        families.Add(family);
        SynchronizeFamilies();
    }

    public void SynchronizeFamilies()
    {
        File.Delete("families.txt");
        foreach (var family in families)
        {
            File.AppendAllText("families.txt", family.ToString());
        }
    }
}