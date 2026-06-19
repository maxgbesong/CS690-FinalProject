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
            string familyName = splitted[0];
            List<FamilyMember> members = new List<FamilyMember>();
            for (int i = 1; i < splitted.Length; i += 2)
            {
                string memberName = splitted[i];
                DietType dietType = (DietType)Enum.Parse(typeof(DietType), splitted[i+1]);
                members.Add(new FamilyMember(memberName, dietType));
            }
            families.Add(new Family(familyName, members));
        }
    }

    public void AddFamily(Family family)
    {
        families.Add(family);
        SynchronizeFamilies();
    }

    public void removeFamily(Family family)
    {
        families.Remove(family);
        SynchronizeFamilies();
    }

    public void SynchronizeFamilies()
    {
        File.Delete("families.txt");
        foreach (var family in families)
        {
            File.AppendAllText("families.txt", family.saveString() + Environment.NewLine);
        }
    }
}