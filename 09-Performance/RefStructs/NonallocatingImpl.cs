namespace RefStructs;

static class NonallocatingImpl
{
    public static long Calculate(int[] array, int length)
    {
        long result = 0;

        for (var i = 0; i + length < array.Length; i += 1)
        {
            for (var j = i + 1; j <= i + length; j += 1)
            {
                Memory<int> subarray = array.AsMemory()[i..j];
                result += Product(subarray);
            }
        }

        return result;
    }


    private static long Product(Memory<int> subslice)
    {
        long result = 1;

        foreach (var x in subslice.Span)
        {
            result *= x;
        }

        return result;
    }
}