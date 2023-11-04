using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class DataAccessTests
{
    readonly Character dummy = new(0);

    [Test]
    public void CharacterRespository()
    {
        // Instantiation
        CharacterRepository repository = ScriptableObject.CreateInstance<CharacterRepository>();
        Assert.IsNotNull(repository, "null instantiation");

        // Initial state
        Assert.IsNotNull(repository.GetAllOfType(0), "new repository returns null at GetAllOfType");
        Assert.IsNotNull(repository.GetAll, "new repository returns null GetAll");
        Assert.IsTrue(repository.GetAll.Count() == 0, "new repository not empty");

        bool fail = false;
        try { repository.Get(dummy.Id); }
        catch { fail = true; }
        Assert.IsTrue(fail, "new repository doesn't fail at retrieving an invalid entity");

        // Operations with one single element
        repository.Add(dummy);
        Assert.IsTrue(repository.GetAll.Count() == 1, "repository doesn't add an element");
        Assert.AreEqual(repository.Get(dummy.Id), dummy, "repository doesn't return the same entity that gets inserted");
        Assert.AreEqual(repository.GetAll.Single(), dummy, "repository doesn't return all the entities");

        repository.Remove(dummy);
        Assert.IsTrue(repository.GetAll.Count() == 0, "repository doesn't remove elements correctly");
        Assert.IsNotNull(repository.GetAllOfType(0), "repository returns null at GetAllOfType");
        Assert.IsNotNull(repository.GetAll, "repository returns null GetAll");
        Assert.IsTrue(repository.GetAll.Count() == 0, "repository not empty after all the entities were removed");

        repository.Add(dummy);

    }

    /*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DataAccessTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
     */
}
