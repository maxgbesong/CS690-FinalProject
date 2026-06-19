namespace FinalProject;

public class FamilyMember
{
    public string name;
    public DietType dietType;

    public FamilyMember(string name, DietType dietType)
    {
        this.name = name;
        this.dietType = dietType;
    }
}

public class Family
{
    public string name;
    public List<FamilyMember> members;
    public Boolean assignedMeal;

    public Family(string name, List<FamilyMember> members)
    {
        this.name = name;
        this.members = members;
        this.assignedMeal = false;
    }

    public override string ToString()
    {
        return this.name;
    }
    
    public string saveString()
    {
        string textLine = name;
        foreach (var member in members)
        {
            textLine += ":" + member.name + ":" + member.dietType.ToString();
        }
        return textLine;
    }

    public string listMemberNames()
    {
        string names = "";
        for (int i = 0; i < members.Count; i++)
        {
            names += members[i].name;
            if (i < members.Count - 1)
            {
                names += ", ";
            }
        }
        return names;
    }
}

public class Ingredient
{
    public string name;
    public string amount;
    public DateTime expDate;

    public Ingredient(string name, string amount, DateTime expDate)
    {
        this.name = name;
        this.amount = amount;
        this.expDate = expDate;
    }
}

public enum DietType
{
    NoRestrictions,
    LactoseFree,
    GlutenFree,
    NutFree,
    Vegetarian,
    Vegan,
    Kosher
}

public enum Role
{
    Volunteer,
    Manager
}