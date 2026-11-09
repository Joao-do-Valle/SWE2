namespace GerenciadorLivros.Utils;

/// <summary>
/// Funcionalidades relacionadas ao ISBN (International Standard Book Number).
/// Suporta os formatos ISBN-10 e ISBN-13.
/// </summary>
public static class IsbnValidador
{
    /// <summary>Remove hífens, espaços e converte para maiúsculas.</summary>
    public static string Normalizar(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return string.Empty;

        return new string(isbn.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();
    }

    /// <summary>Retorna true se o ISBN (10 ou 13 caracteres) for válido.</summary>
    public static bool EhValido(string? isbn)
    {
        var limpo = Normalizar(isbn);

        return limpo.Length switch
        {
            10 => ValidarIsbn10(limpo),
            13 => ValidarIsbn13(limpo),
            _ => false
        };
    }

    /// <summary>Formata o ISBN para exibição (ex.: 978-8535902774).</summary>
    public static string Formatar(string? isbn)
    {
        var limpo = Normalizar(isbn);

        return limpo.Length switch
        {
            13 => $"{limpo[..3]}-{limpo[3..]}",
            10 => $"{limpo[..1]}-{limpo[1..]}",
            _ => limpo
        };
    }

    private static bool ValidarIsbn10(string isbn)
    {
        var soma = 0;

        for (var i = 0; i < 10; i++)
        {
            int valor;

            if (i == 9 && isbn[i] == 'X')
                valor = 10;
            else if (char.IsDigit(isbn[i]))
                valor = isbn[i] - '0';
            else
                return false;

            soma += valor * (10 - i);
        }

        return soma % 11 == 0;
    }

    private static bool ValidarIsbn13(string isbn)
    {
        if (!isbn.All(char.IsDigit))
            return false;

        var soma = 0;

        for (var i = 0; i < 13; i++)
        {
            var digito = isbn[i] - '0';
            soma += i % 2 == 0 ? digito : digito * 3;
        }

        return soma % 10 == 0;
    }
}
