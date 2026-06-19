namespace FinalProject.Tests;

public class FinalProjectTest
{
    [Fact]
    public void Test_Domain()
    {
        List<FamilyMember> membersMiller = new List<FamilyMember>();
        FamilyMember sam = new FamilyMember("Sam", DietType.NutFree);
        membersMiller.Add(sam);
        FamilyMember ann = new FamilyMember("Ann", DietType.NoRestrictions);
        membersMiller.Add(ann);
        Family miller = new Family("Miller", membersMiller);

        // Test family saveString
        Assert.Equal("Miller:Sam:NutFree:Ann:NoRestrictions", miller.saveString());

        // Test family listMemberNames
        Assert.Equal("Sam, Ann", miller.listMemberNames());
    }

    [Fact]
    public void Test_DataManager()
    {
        File.Delete("families.txt");
        DataManager testDM = new DataManager();

        // Test adding a new family
        List<FamilyMember> members = new List<FamilyMember>();
        FamilyMember joe = new FamilyMember("Joe", DietType.Vegan);
        members.Add(joe);
        Family smith = new Family("Smith", members);
        testDM.AddFamily(smith);
        var familiesFileContent = File.ReadAllLines("families.txt");
        Assert.Equal("Smith:Joe:Vegan", familiesFileContent[0]);

        // Test removing a family
        testDM.RemoveFamily(smith);
        Assert.False(File.Exists("families.txt"));
    }

    [Fact]
    public void Test_Reporter()
    {
        File.Delete("families.txt");
        DataManager testDM = new DataManager();

        List<FamilyMember> membersSmith = new List<FamilyMember>();
        FamilyMember joe = new FamilyMember("Joe", DietType.Vegan);
        membersSmith.Add(joe);
        Family smith = new Family("Smith", membersSmith);
        testDM.AddFamily(smith);

        List<FamilyMember> membersMiller = new List<FamilyMember>();
        FamilyMember sam = new FamilyMember("Sam", DietType.NutFree);
        membersMiller.Add(sam);
        FamilyMember ann = new FamilyMember("Ann", DietType.NoRestrictions);
        membersMiller.Add(ann);
        Family miller = new Family("Miller", membersMiller);
        testDM.AddFamily(miller);

        // Test CountFamilyMeals
        Console.WriteLine(testDM.families[0].name);
        Assert.Equal(1, Reporter.CountFamilyMeals(testDM.families[0]));

        Assert.Equal(2, Reporter.CountFamilyMeals(testDM.families[1]));

        // Test CountFamilyMealsForDietType
        Assert.Equal(1, Reporter.CountFamilyMealsForDietType(testDM.families[0], DietType.Vegan));

        Assert.Equal(0, Reporter.CountFamilyMealsForDietType(testDM.families[0], DietType.LactoseFree));

        // Test CountTotalMeals
        Assert.Equal(3, Reporter.CountTotalMeals(testDM.families));

        // Test CountTotalMealsForDietType
        Assert.Equal(1, Reporter.CountTotalMealsForDietType(testDM.families, DietType.NoRestrictions));

        Assert.Equal(0, Reporter.CountTotalMealsForDietType(testDM.families, DietType.Kosher));
    }
}
