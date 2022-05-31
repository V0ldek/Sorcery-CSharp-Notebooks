namespace RefStructs;

static class AllocatingImpl
{
    public static long Calculate(int[] array, int length)
    {
        long result = 0;

        for (var i = 0; i + length < array.Length; i += 1)
        {
            for (var j = i + 1; j <= i + length; j += 1)
            {
                int[] subarray = array[i..j];
                result += Product(subarray);
            }
        }

        return result;
    }

    private static long Product(int[] subarray)
    {
        long result = 1;

        foreach (var x in subarray)
        {
            result *= x;
        }

        return result;
    }
}
