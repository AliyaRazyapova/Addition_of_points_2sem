namespace Inf2.Utils;

public static class ArrayExtension
{
    /// <summary>
    /// Разделить массив на n частей
    /// </summary>
    /// <param name="array">Массив</param>
    /// <param name="chunkCount">Количество частей</param>
    /// <typeparam name="T">Тип массива</typeparam>
    /// <returns>Массив с частями</returns>
    /// <exception cref="ArgumentException">Неверное количество частей</exception>
    public static T[][] ToChunks<T>(this T[] array, int chunkCount)
    {
        if (chunkCount <= 0)
            throw new ArgumentException("Неверное кол-во чанков", nameof(chunkCount));

        var chunks = new T[chunkCount][];
        var chunkSize = array.Length / chunkCount;
        var additional = array.Length % chunkCount;

        var begin = 0;
        var end = 0;
        for (int i = 0; i < chunkCount; i++)
        {
            end += chunkSize;

            if(additional > 0)
            {
                end++;
                additional--;
            }

            if(end > array.Length)
                end = array.Length - 1;

            chunks[i] = array[begin..end];
            begin = end;
        }

        return chunks;
    }
}