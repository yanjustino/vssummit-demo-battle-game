using Domain.Entities;
using Domain.Entities.Characters;
using FluentAssertions;

namespace UnitTests.Entities.Characters;

public class CharacterNameTests
{
    [Theory]
    [InlineData("123_Name")]
    [InlineData("123Name")]
    [InlineData("Name_123")]
    [InlineData("Name123")]
    [InlineData("123_Name_123")]
    [InlineData("123Name123")]
    public void The_character_name_should_not_have_numbers(string value) =>
        Assert.Throws<EntityArgumentException>(() => CharacterName.Create(value));
    
    [Theory]
    [InlineData("!@#$%ˆ&*()-_Name")]
    [InlineData("!@#$%ˆ&*()Name")]
    [InlineData("Name_!@#$%ˆ&*()")]
    [InlineData("Name!@#$%ˆ&*()")]
    [InlineData("!@#$%ˆ&*()_Name_!@#$%ˆ&*()")]
    [InlineData("!@#$%ˆ&*()Name!@#$%ˆ&*()")]
    public void The_character_name_should_not_have_symbols(string value) =>
        Assert.Throws<EntityArgumentException>(() => CharacterName.Create(value));
    
    [Theory]
    [InlineData("first_middle_last")]
    [InlineData("first_middle_last_name")]
    public void The_character_name_should_have_a_lenght_up_to_15(string value) =>
        Assert.Throws<EntityArgumentException>(() => CharacterName.Create(value));    

    [Theory]
    [InlineData("first_last")]
    [InlineData("_Firstname")]
    [InlineData("Firstname_")]
    [InlineData("_Firstname_")]
    [InlineData("FirstLast")]
    [InlineData("First_Mid_Last")]
    public void The_character_name_should_have_words_and_underscore(string value)
    {
        var name = CharacterName.Create(value);
        name.Value.Should().Be(value);
    }
}