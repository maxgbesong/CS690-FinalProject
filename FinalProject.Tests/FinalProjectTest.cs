namespace FinalProject.Tests;

public class FinalProjectTest
{
    DataManager testDM;

    public FinalProjectTest()
    {
        testDM = new DataManager();
    }

    [Fact]
    public void Test_DataManager()
    {
        // Test adding a new family
        List<FamilyMember> members = new List<FamilyMember>();
        FamilyMember joe = new FamilyMember("Joe", DietType.Vegan);
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
        List<FamilyMember> members = new List<FamilyMember>();
        FamilyMember joe = new FamilyMember("Joe", DietType.Vegan);
        Family smith = new Family("Smith", members);
        testDM.AddFamily(smith);

        FamilyMember sam = new FamilyMember("Sam", DietType.NutFree);
        FamilyMember ann = new FamilyMember("Ann", DietType.NoRestrictions);
        Family miller = new Family("Miller", members);
        testDM.AddFamily(miller);

        // Test CountFamilyMeals
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
