namespace RefReturns;

public class Box
{
    private readonly Large _large = default;

    public Large Large => _large;
}