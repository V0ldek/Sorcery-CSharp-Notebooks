namespace RefReturns;

public class RefBox
{
    private readonly Large _large = default;

    public ref readonly Large Large => ref _large;
}