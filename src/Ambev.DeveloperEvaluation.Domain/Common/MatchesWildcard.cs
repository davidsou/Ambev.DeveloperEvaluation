namespace Ambev.DeveloperEvaluation.Domain.Common;

public static class StringExtensions
{
    private static string NormalizeLikePattern(string filter)
    {
        if (string.IsNullOrEmpty(filter)) return filter;

        // Se não contém wildcard, assume busca parcial
        if (!filter.Contains('*'))
            return $"%{filter}%";

        return filter.Replace('*', '%');
    }
}
