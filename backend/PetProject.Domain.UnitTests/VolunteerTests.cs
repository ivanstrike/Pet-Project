using FluentAssertions;
using PetProject.Domain.VolunteerContext;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.UnitTests.Extensions;

namespace PetProject.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void Add_Pet_With_Empty_Pets_Return_Success_Result()
    {
        //arrange
        var volunteer = CreateEntities.CreateValidVolunteer();

        var pet = CreateEntities.CreateValidPet();
        var petId = pet.Id;
        //act
        var result = volunteer.AddPet(pet);

        //assert
        var addedPetResult = volunteer.GetPetById(petId);

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(pet.Id);
        addedPetResult.Value.SerialNumber.Should().Be(SerialNumber.First);
    }

    [Fact]
    public void Add_Pet_With_Other_Pets_Return_Success_Result()
    {
        //arrange
        const int petCount = 5;

        var volunteer = CreateEntities.CreateValidVolunteer();

        var pets = Enumerable.Range(1, petCount).Select(_ =>
            CreateEntities.CreateValidPet());

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var petToAdd = CreateEntities.CreateValidPet();
        var petIdToAdd = petToAdd.Id;

        //act
        var result = volunteer.AddPet(petToAdd);

        //assert
        var addedPetResult = volunteer.GetPetById(petIdToAdd);

        var serialNumber = SerialNumber.Create(petCount + 1);

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(petToAdd.Id);
        addedPetResult.Value.SerialNumber.Should().Be(serialNumber.Value);
    }

    [Fact]
    public void Move_Pet_From_5_To_2_Return_Success_Result()
    {
        //arrange
        var toSerialNumber = SerialNumber.Create(2).Value;
        var volunteer = CreateEntities.CreateValidVolunteer();

        var pets = Enumerable.Range(1, 6).Select(_ =>
            CreateEntities.CreateValidPet());

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var movedPet = volunteer.Pets.FirstOrDefault(x => x.SerialNumber.Value == 5);

        //act
        var result = volunteer.MovePet(movedPet, toSerialNumber);

        //assert
        var expectedSerialNumbers = new[] { 1, 3, 4, 5, 2, 6 };
        var isCompared = ComparePetsToExpected(volunteer.Pets, expectedSerialNumbers);

        result.IsSuccess.Should().BeTrue();
        isCompared.Should().BeTrue();
    }

    [Fact]
    public void Move_Pet_From_3_To_End_Return_Success_Result()
    {
        //arrange
        var toSerialNumber = SerialNumber.Create(6).Value;
        var volunteer = CreateEntities.CreateValidVolunteer();

        var pets = Enumerable.Range(1, 6).Select(_ =>
            CreateEntities.CreateValidPet());

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var movedPet = volunteer.Pets.FirstOrDefault(x => x.SerialNumber.Value == 3);

        //act
        var result = volunteer.MovePet(movedPet, toSerialNumber);

        //assert
        var expectedSerialNumbers = new[] { 1, 2, 6, 3, 4, 5 };
        var isCompared = ComparePetsToExpected(volunteer.Pets, expectedSerialNumbers);

        result.IsSuccess.Should().BeTrue();
        isCompared.Should().BeTrue();
    }

    [Fact]
    public void Move_Pet_From_3_To_1_Return_Success_Result()
    {
        //arrange
        var toSerialNumber = SerialNumber.Create(1).Value;
        var volunteer = CreateEntities.CreateValidVolunteer();

        var pets = Enumerable.Range(1, 6).Select(_ =>
            CreateEntities.CreateValidPet());

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var movedPet = volunteer.Pets.FirstOrDefault(x => x.SerialNumber.Value == 3);

        //act
        var result = volunteer.MovePet(movedPet, toSerialNumber);

        //assert
        var expectedSerialNumbers = new[] { 2, 3, 1, 4, 5, 6 };
        var isCompared = ComparePetsToExpected(volunteer.Pets, expectedSerialNumbers);

        result.IsSuccess.Should().BeTrue();
        isCompared.Should().BeTrue();
    }

    [Fact]
    public void Move_Pet_From_5_To_NonExistentPosition_Return_Success_Result()
    {
        //arrange
        var toSerialNumber1 = SerialNumber.Create(100).Value;
        var volunteer = CreateEntities.CreateValidVolunteer();

        var pets = Enumerable.Range(1, 6).Select(_ =>
            CreateEntities.CreateValidPet());

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var movedPet = volunteer.Pets.FirstOrDefault(x => x.SerialNumber.Value == 5);

        //act
        var result = volunteer.MovePet(movedPet, toSerialNumber1);

        //assert
        result.IsSuccess.Should().BeFalse();
    }

    private bool ComparePetsToExpected(IEnumerable<Pet> pets, int[] expectedPetsSerialNumbers)
    {
        var orderedSerialNumbers = pets
            .Select(p => p.SerialNumber.Value)
            .ToList();
        for (var i = 0; i < orderedSerialNumbers.Count; i++)
        {
            if (orderedSerialNumbers[i] != expectedPetsSerialNumbers[i])
                return false;
        }

        return true;
    }
}