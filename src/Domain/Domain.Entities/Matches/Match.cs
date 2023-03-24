using System.Text.Json.Serialization;
using Domain.Entities.Characters;

namespace Domain.Entities.Matches;

public record Match(Character PlayerA, Character PlayerB)
{
    public Character[] PlayersOrder { get; private set; } = { };
    public MatchLog MatchLog { get; } = new();

    public async Task Start()
    {
        DefineFirstPlayer();
        
        while (PlayerA.IsAlive() && PlayerB.IsAlive())
        {
            var challenger = PlayersOrder[0];
            var challenged = PlayersOrder[1];

            var attack = challenger.JobClass.CalculatedAttack();
            challenged.ReceiveAttack(attack);
            
            MatchLog.LogInformation(
                $"{challenger.Name.Value} atacou {challenged.Name.Value} com {attack} de dano, " +
                $"{challenged.Name.Value} com {challenged.JobClass.Attributes.HealthPoints} pontos de vida restantes");    
            
            ChangeCharacterTurn();
            await Task.Delay(100);
        }

        var winner = PlayersOrder[1];
        
        MatchLog.LogInformation(
            $"{winner.Name.Value} venceu a batalha! " +
            $"{winner.Name.Value} ainda tem {winner.JobClass.Attributes.HealthPoints} pontos de vida restantes!");         
    }
    
    public void DefineFirstPlayer()
    {
        while (true)
        {
            var speeds = new[]
            {
                (PlayerA, PlayerA.JobClass.CalculatedSpeed()),
                (PlayerB, PlayerB.JobClass.CalculatedSpeed())
            };

            var result = Compare(speeds);
            if (!result.Any()) continue;

            PlayersOrder = new[] { result[0].character, result[1].character };

            MatchLog.LogInformation(
                $"{result[0].character.Name.Value} ({result[0].value}) foi mais veloz que " +
                $"o {result[1].character.Name.Value} ({result[1].value}), e irá começar!");
            break;
        }
    }    

    private static IReadOnlyList<(Character character, int value)> Compare(IReadOnlyList<(Character character, int value)> values)
    {
        var player1 = values[0];
        var player2 = values[1];
        
        var index = player1.value.CompareTo(player2.value);
        if (index == 0) 
            return Array.Empty<(Character character, int value)>();
    
        var result = values.OrderByDescending(x => x.value).ToArray();
        return new[] { result[0], result[1] };
    }

    private void ChangeCharacterTurn() => PlayersOrder = PlayersOrder.Any() ? new[] { PlayersOrder[1], PlayersOrder[0] } : PlayersOrder;
}