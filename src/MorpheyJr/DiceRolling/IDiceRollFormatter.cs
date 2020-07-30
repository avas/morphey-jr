namespace DiscordBotPlayground.DiceRolling
{
    public interface IDiceRollFormatter
    {
        string Format(SummaryRollResult rollResult);
    }
}