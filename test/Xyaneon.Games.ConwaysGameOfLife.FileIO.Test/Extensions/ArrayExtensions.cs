namespace Xyaneon.Games.ConwaysGameOfLife.FileIO.Test.Extensions;

public static class ArrayExtensions
{
    public static bool SequenceEquals<T>(this T[,] a, T[,] b) => a.Rank == b.Rank
        && Enumerable.Range(0, a.Rank).All(d=> a.GetLength(d) == b.GetLength(d))
        && a.Cast<T>().SequenceEqual(b.Cast<T>());
}